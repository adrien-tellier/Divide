using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Clone : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		[Required("Copied object required")]
		private GameObject m_copiedObject = null;

		[SerializeField]
		private GameObject m_depot = null;

		[SerializeField]
		[Required("Inputs required")]
		private InputHandler m_inputs = null;

		[SerializeField]
		[Range(0, 500)]
		private float m_speed = 200f;

		[SerializeField]
		[Range(0, 10)]
		private float m_lifeTime = 4f;

		[SerializeField]
		[Range(0, 20)]
		private float m_coolDown = 10f;

		[SerializeField]
		private Event m_spawnCloneEvent = null;

		[SerializeField]
		private Event m_dispawnCloneEvent = null;

		private Transform m_transform = null;
		private MeshRenderer[] m_renderClone = null;
		private Rigidbody m_rigidbody = null;
		private CapsuleCollider m_capsuleCollider = null;
		private CloningDirection m_cloningDirection = null;

		private Vector3 m_move = Vector3.zero;
		private Vector3 m_direction = Vector3.zero;

		private float m_lifeTimeSpent = 0f;
		private float m_coolDownRecovery = 0f;

		private bool m_isVisible = false;
		private bool m_isPressingKey = false;
		private bool m_joystickMove = false;
		private bool m_coolDownRecovered = false;

		//**************************************************************************
		//***** Properties *********************************************************

		public bool CoolDownRecovered
		{
			get { return m_coolDownRecovered; }
		}


		public float CoolDownPercent
		{
			get { return m_coolDownRecovery / m_coolDown; }
		}



		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void Awake()
		{
			// Find the file of the direction of the clone
			m_cloningDirection = m_copiedObject.GetComponentInChildren<CloningDirection>();
			m_rigidbody = GetComponent<Rigidbody>();
			m_renderClone = gameObject.GetComponentsInChildren<MeshRenderer>();
			m_capsuleCollider = GetComponentInChildren<CapsuleCollider>();

			m_transform = transform;
			m_coolDownRecovery = m_coolDown;
		}

		private void Start()
		{
			InitEvent(ref m_inputs.Inputs);

			CloneVisible(false);
		}

		private void Update()
		{
			CoolDownManagement();

			// Press cloning input
			if (m_coolDownRecovered)
			{
				if (m_isPressingKey && m_joystickMove && !IsColliding())
				{
					SetUpClone();
					m_isPressingKey = false;
				}
			}

			m_isPressingKey = false;
			m_joystickMove = false;
		}

		private void FixedUpdate()
		{
			// Update the clone
			if (m_isVisible)
			{
				CloneBehaviour();
				LifeTimeManagement();
			}
			else
			{
				PutAwayClone();
				CloneVisible(false);
			}
		}

		//**************************************************************************
		//***** Functions **********************************************************

		private void InitEvent(ref List<InputEvent> inputs)
        {
            foreach (InputEvent ie in inputs)
            {
                switch (ie.Action)
                {
					case EInputEventAction.SpawnClone: ie.m_event += OnSpawnClone; break;
					case EInputEventAction.CloneDirectionJoystick:	ie.m_event += OnCloneDirectionJoystick; break;
                    default: break;
				}
            }
        }

		private void OnSpawnClone(InputEventContext context)
		{
			m_isPressingKey = true;
		}

		private void OnCloneDirectionJoystick(InputEventContext context)
		{
			if (context is InputEventContextVector2)
				m_joystickMove = true;
		}

		private void InitClone()
		{
			// Set the clone forward the copied object
			if (m_cloningDirection != null)
			{
				Vector3 tmp = m_cloningDirection.CloneSpawn;
				tmp.y = m_copiedObject.transform.position.y;

				m_transform.position = tmp;
			}

			// Set the clone visible
			CloneVisible(true);

			m_rigidbody.useGravity = true;
			m_capsuleCollider.enabled = true;
		}

		private bool IsColliding()
		{
			Collider[] colliders = Physics.OverlapSphere(m_cloningDirection.CloneSpawn, 0.5f);
			
			return (colliders.Length > 0);
		}

		private void SetUpClone()
		{
			// Setup the clone if the cool down recovered
			InitClone();
			SaveDirectionCopiedObject();
			m_spawnCloneEvent?.Raise();

			// Reset the cool down
			m_coolDownRecovery = 0f;
			m_coolDownRecovered = false;
		}

		public void PutAwayClone()
		{
			Vector3 move = m_depot.transform.position;
			move.y = m_depot.transform.position.y + m_depot.transform.localScale.y;

			m_transform.position = move;
			m_rigidbody.useGravity = false;
			m_capsuleCollider.enabled = false;
		}

		private void CloneVisible(bool isVisible)
		{
			// Set the clone visible or invisible
			foreach (MeshRenderer mr in m_renderClone)
				mr.enabled = isVisible;

			m_isVisible = isVisible;
		}

		private void CloneBehaviour()
		{
			m_rigidbody.velocity = new Vector3(	m_direction.normalized.x * m_speed * Time.fixedDeltaTime,
												m_rigidbody.velocity.y,
												m_direction.normalized.z * m_speed* Time.fixedDeltaTime);
		}

		private void SaveDirectionCopiedObject()
		{
			m_direction = m_transform.position - m_copiedObject.transform.position;
			m_transform.forward = m_direction;
		}


		private void LifeTimeManagement()
		{
			if (m_lifeTimeSpent >= m_lifeTime)
			{
				m_dispawnCloneEvent?.Raise();
				m_isVisible = false;
				m_lifeTimeSpent = 0f;
			}
			else
				m_lifeTimeSpent += Time.deltaTime;
		}


		private void CoolDownManagement()
		{
			// clone's cool down
			if (m_coolDownRecovery >= m_coolDown)
				m_coolDownRecovered = true;
			else if (!m_isVisible)
			{
				m_coolDownRecovered = false;
				m_coolDownRecovery += Time.deltaTime;
			}
		}
	}
}