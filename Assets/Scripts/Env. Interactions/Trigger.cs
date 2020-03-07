using System.Collections.Generic;
using UnityEngine;


namespace Game
{
	[RequireComponent(typeof(BoxCollider))]
	public class Trigger : MonoBehaviour
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		[SerializeField]
		private Events		    m_activeEvents = null;

		[SerializeField]
		private Events		    m_desactiveEvents = null;

		[SerializeField]
		private bool		    m_autoActive = true;

		[SerializeField]
		private InputHandler    m_inputs = null;

		[SerializeField]
		private bool		    m_autoDesactive = false;

		[SerializeField, Range(0f, 60f)]
		private float		    m_timer = 0f;


		private bool		    m_activeState = false;
        private bool            m_interact = false;
		private float		    m_time = 0f;


        //********************************************************************************
        //***** Functions : Unity ********************************************************

        private void Start()
        {
            InitEvent(ref m_inputs.Inputs);
        }

		private void Update()
		{
			if (!m_activeState && m_autoDesactive)
			{
				m_time += Time.deltaTime;

				if (m_time > m_timer)
				{
					m_time = 0f;
					Raise();
				}
			}
		}


		private void OnTriggerEnter(Collider other)
		{
            if (((m_activeState) || (!m_activeState && !m_autoDesactive)) && (m_autoActive))
                Raise();
		}


		private void OnTriggerStay(Collider other)
		{
			if (((m_activeState) || (!m_activeState && !m_autoDesactive)) && (!m_autoActive))
			{
				if (m_interact)
					Raise();
			}
		}





        //********************************************************************************
        //***** Functions ****************************************************************

        private void InitEvent(ref List<InputEvent> inputs)
        {
            foreach (InputEvent ie in inputs)
            {
                switch (ie.Action)
                {
                    case EInputEventAction.Interact:        ie.m_event += OnInteract;       break;
                    case EInputEventAction.StopInteract:    ie.m_event += OnStopInteract;   break;
                    default: break;
                }
            }
        }

        private void OnInteract(InputEventContext context)
        {
            m_interact = true;
        }

        private void OnStopInteract(InputEventContext context)
        {
            m_interact = false;
        }

        private void Raise()
		{
			if (m_activeState)
				m_activeEvents?.Raise();
			else
				m_desactiveEvents?.Raise();

			m_activeState = !m_activeState;
		}
	}
}