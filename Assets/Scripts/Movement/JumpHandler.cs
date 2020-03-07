using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class JumpHandler : MonoBehaviour
    {
        //**************************************************************************
        //***** Fields *************************************************************

        [SerializeField]
        [Required("InputHandler required")]
        private InputHandler m_inputs = null;

        [Range(0, 500)]
        [SerializeField]
        private float m_impulse = 5f;

        [Range(0, 1)]
        [SerializeField]
        private float m_jumpRate = 0f;
        
        private Transform m_transform = null;
        private CapsuleCollider m_collider = null;
        private Rigidbody m_rigidbody = null;

        private float m_lastJump = 0f;

        private bool m_wantToJump = false;

		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void Awake()
		{
			m_transform = transform;
			m_rigidbody = GetComponent<Rigidbody>();
			m_collider = GetComponentInChildren<CapsuleCollider>();
		}

		private void Start()
		{
			InitEvent(ref m_inputs.Inputs);
		}

		// Update is called once per frame
		void FixedUpdate()
		{
			if (m_wantToJump && IsGrounded())
				if (Time.fixedTime - m_lastJump > m_jumpRate)
				{
					m_rigidbody.AddForce(new Vector3(0, m_impulse, 0), ForceMode.Impulse);
					m_lastJump = Time.fixedTime;
				}
			m_wantToJump = false;
		}

		//**************************************************************************
		//***** Functions *********************************************************

		private void InitEvent(ref List<InputEvent> inputs)
        {
            foreach (InputEvent ie in inputs)
            {
                switch (ie.Action)
                {
                    case EInputEventAction.Jump:    ie.m_event += OnJump;   break;
                    default:                                                break;
                }
            }
        }

        private void OnJump(InputEventContext context)
        {
            m_wantToJump = true;
        }

        private bool IsGrounded()
        {
            RaycastHit hit;
            return Physics.Raycast(m_transform.position, -Vector3.up, out hit, m_collider.bounds.extents.y + 0.1f);
        }

    }
}
