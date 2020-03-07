using System;
using UnityEngine;


namespace Game
{
	[Serializable]
	public class Actions
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField] private Action[]	m_actions = null;










		//********************************************************************************
		//***** Functions : Enter/Exit ***************************************************

		public void Enter_Actions(StateMachine t_fsm)
		{
			// Enter actions
			for (int i = 0; i < m_actions.Length; ++i)
			{
				if (m_actions[i] != null)
				{
					m_actions[i].SetData(t_fsm);
					m_actions[i].Enter_Action();
				}
			}
		}


		public void Exit_Actions(StateMachine t_fsm)
		{
			// Exit actions
			for (int i = 0; i < m_actions.Length; ++i)
			{
				if (m_actions[i] != null)
				{
					m_actions[i].SetData(t_fsm);
					m_actions[i].Exit_Action();
				}
			}
		}










		//********************************************************************************
		//***** Functions : Trigger ******************************************************

		public virtual void OnTriggerEnter_Actions(Collider t_other, StateMachine t_fsm)
		{
			// OnTriggerEnter actions
			for (int i = 0; i < m_actions.Length; ++i)
			{
				if (m_actions[i] != null)
				{
					m_actions[i].SetData(t_fsm);
					m_actions[i].OnTriggerEnter_Action(t_other);
				}
			}
		}


		public virtual void OnTriggerStay_Actions(Collider t_other, StateMachine t_fsm)
		{
			// OnTriggerStay actions
			for (int i = 0; i < m_actions.Length; ++i)
			{
				if (m_actions[i] != null)
				{
					m_actions[i].SetData(t_fsm);
					m_actions[i].OnTriggerStay_Action(t_other);
				}
			}
		}


		public virtual void OnTriggerExit_Actions(Collider t_other, StateMachine t_fsm)
		{
			// OnTriggerExit actions
			for (int i = 0; i < m_actions.Length; ++i)
			{
				if (m_actions[i] != null)
				{
					m_actions[i].SetData(t_fsm);
					m_actions[i].OnTriggerExit_Action(t_other);
				}
			}
		}










		//********************************************************************************
		//***** Functions : Update *******************************************************

		public void FixedUpdate_Actions(StateMachine t_fsm)
		{
			// FixedUpdate actions
			for (int i = 0; i < m_actions.Length; ++i)
			{
				if (m_actions[i] != null)
				{
					m_actions[i].SetData(t_fsm);
					m_actions[i].FixedUpdate_Action();
				}
			}
		}


		public void Update_Actions(StateMachine t_fsm)
		{
			// Update actions
			for (int i = 0; i < m_actions.Length; ++i)
			{
				if (m_actions[i] != null)
				{
					m_actions[i].SetData(t_fsm);
					m_actions[i].Update_Action();
				}
			}
		}


		public void LateUpdate_Actions(StateMachine t_fsm)
		{
			// LateUpdate actions
			for (int i = 0; i < m_actions.Length; ++i)
			{
				if (m_actions[i] != null)
				{
					m_actions[i].SetData(t_fsm);
					m_actions[i].LateUpdate_Action();
				}
			}
		}
	}
}