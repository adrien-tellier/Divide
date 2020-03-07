using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class MovementHandler : MonoBehaviour
    {
		//**************************************************************************
		//***** Fields *************************************************************

		[Required("InputHandler required")]
		[SerializeField]
		private InputHandler m_inputs = null;


		[SerializeField]
		[Range(0f, 500f)]
		private float m_sneakSpeed = 200f;

		[SerializeField]
		[Range(0f, 500f)]
		private float m_walkSpeed = 200f;

		[SerializeField]
		[Range(0f, 500f)]
		private float m_runSpeed = 200f;

		[SerializeField]
		[Range(0f, 2f)]
		[Tooltip("The limit movement of the joystick before walk")]
		private float m_maxOffSetWalking = 0.1f;

		[SerializeField]
		[Range(0f, 0.7f)]
		[Tooltip("The limit movment of the joystick under which you sneak")]
		private float m_sneakStep = 0.80f;

		[Range(0, 1080)]
		[SerializeField]
		private float m_rotationSpeed = 500f;

		[SerializeField]
		[Range(0, 1)]
		[Tooltip("The smooth speed of the scaling when the player sneak")]
		private float m_smoothBodyScale = 1.0f;

		[SerializeField]
		[Tooltip("The scale of the body when the player is sneaking")]
		private Vector3 m_sneakBodyScale = new Vector3(1, 0.8f, 1);

		[SerializeField]
		[Tooltip("Choose between 2 rotation modes")]
		private bool m_smoothRotation = true;

		[SerializeField]
		private LayerMask m_cantStandUpUnder;

		[SerializeField]
		private Event m_sneakEvent = null;

		[SerializeField]
		private Event m_walkEvent = null;

		[SerializeField]
		private Event m_runEvent = null;


		private EMovementState m_movementState = new EMovementState();
	
		private Transform m_transform = null;
		private Rigidbody m_rigidBody = null;

		private Vector3 m_direction = Vector3.zero;
		private Vector3 m_lastDirection = Vector3.zero;
		private Vector3 m_looktAt = Vector3.zero;
		private Vector3 m_normalBodyScale = Vector3.zero;

		private Vector2 m_movement = Vector2.zero;
		private Vector2 m_lastMovement = Vector2.zero;

		private float m_currSpeed = 0f;
		private float m_directionSpeed = 0f;
		private float m_bodyHeight = 0f;

		private bool m_isMoving = false;
		private bool m_canStandUp = true;

		//**************************************************************************
		//***** Properties *********************************************************

		public EMovementState MoveState
		{
			get { return m_movementState; }
		}

		//**************************************************************************
		//***** Functions **********************************************************

		private void InitEvent(ref List<InputEvent> inputs)
        {
            foreach (InputEvent ie in inputs)
            {
                switch (ie.Action)
                {
					case EInputEventAction.Move:	ie.m_event += OnMove;	break;
                    case EInputEventAction.Run:		ie.m_event += OnRun;	break;
                    default:                                                break;
                }
            }
        }

		private void OnMove(InputEventContext context)
		{
			if (context is InputEventContextVector2)
			{
				m_movement = context.ReadValue<Vector2>();
				m_direction.x = m_movement.x;
				m_direction.z = m_movement.y;

				m_directionSpeed = Math.Min(context.ReadValue<Vector2>().sqrMagnitude, 1f);

				m_isMoving = true;
			}
		}

		private void OnRun(InputEventContext context)
		{
			if (context.ReadValue<float>() > 0f && m_canStandUp)
			{
				m_movementState = EMovementState.Run;
				m_runEvent?.Raise();
			}
			else if (m_canStandUp)
			{
				m_movementState = EMovementState.Walk;
				m_walkEvent?.Raise();
			}
			else
			{
				m_movementState = EMovementState.Sneak;
				m_sneakEvent?.Raise();
			}
		}

		private void UpdateMovement()
		{
			if (m_isMoving)
			{
				m_rigidBody.velocity = new Vector3(	m_direction.normalized.x * m_currSpeed * Time.fixedDeltaTime,
													m_rigidBody.velocity.y,
													m_direction.normalized.z * m_currSpeed * Time.fixedDeltaTime);

				if (m_direction != Vector3.zero)
					m_lastDirection = m_direction;

				m_isMoving = false;
				m_direction = Vector3.zero;
			}
			else
				m_rigidBody.velocity = new Vector3(0, m_rigidBody.velocity.y, 0);
		}

		private void UpdateWalkingAndSneaking()
		{
			Vector2 offSet = m_lastMovement - m_movement;

			if (m_movementState == EMovementState.Sneak && offSet.sqrMagnitude > m_maxOffSetWalking && m_canStandUp)
			{
				m_movementState = EMovementState.Walk;
				m_walkEvent?.Raise();
			}
			else if (m_movementState == EMovementState.Walk && m_directionSpeed < m_sneakStep)
			{
				m_movementState = EMovementState.Sneak;
				m_sneakEvent?.Raise();
			}

			if (!m_isMoving && m_movementState != EMovementState.Run)
			{
				m_movementState = EMovementState.Sneak;
				m_sneakEvent?.Raise();
			}

				m_lastMovement = m_movement;
		}

		private void UpdateSpeed()
		{
			switch (m_movementState)
			{
				case EMovementState.Sneak:	m_currSpeed = m_sneakSpeed * m_directionSpeed;	break;
				case EMovementState.Walk:	m_currSpeed = m_walkSpeed;						break;
				case EMovementState.Run:	m_currSpeed = m_runSpeed;						break;
				default:																	break;
			}
		}

		private void UpdateLooktAt()
		{
			if (m_lastDirection != Vector3.zero)
			{
				if (m_smoothRotation)
				{
					float angle = Vector3.SignedAngle(m_looktAt, m_lastDirection, Vector3.up);

					if (angle > 0)
					{
						if (m_rotationSpeed * Time.fixedDeltaTime < angle)
							m_looktAt = Quaternion.AngleAxis(m_rotationSpeed * Time.fixedDeltaTime, Vector3.up) * m_looktAt;
						else
							m_looktAt = m_lastDirection;
					}
					else
					{
						if (-m_rotationSpeed * Time.fixedDeltaTime > angle)
							m_looktAt = Quaternion.AngleAxis(m_rotationSpeed * Time.fixedDeltaTime, Vector3.down) * m_looktAt;
						else
							m_looktAt = m_lastDirection;
					}
					m_transform.forward = m_looktAt;
				}
				else
					m_transform.forward = m_lastDirection;
			}
		}

		private void UpdateSneak()
		{
			if (m_movementState == EMovementState.Sneak)
			{
				m_transform.localScale = Vector3.Lerp(m_transform.localScale, m_sneakBodyScale, m_smoothBodyScale);
				RaycastHit hit;
				float height = (m_bodyHeight / 2) + (m_bodyHeight * m_transform.lossyScale.y) / 2;
				if (Physics.Raycast(m_transform.position, new Vector3(0, 1, 0), out hit, height, m_cantStandUpUnder))
					m_canStandUp = false;
				else
					m_canStandUp = true;
			}
			else
				m_transform.localScale = Vector3.Lerp(m_transform.localScale, m_normalBodyScale, m_smoothBodyScale);
		}

		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void Awake()
		{
			m_transform = transform;
			m_normalBodyScale = transform.localScale;
			m_rigidBody = GetComponent<Rigidbody>();
			m_bodyHeight = GetComponentInChildren<CapsuleCollider>().height;
		}

		private void Start()
		{
			InitEvent(ref m_inputs.Inputs);
			m_movementState = EMovementState.Sneak;
		}

		private void FixedUpdate()
		{
			UpdateWalkingAndSneaking();
			UpdateSpeed();
			UpdateMovement();
			UpdateLooktAt();
			UpdateSneak();
		}
	}
}
