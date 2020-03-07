using System;
using UnityEngine;


namespace Game
{
	[Serializable]
	public class Transition
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField] private Decision	m_decision = null;
		[SerializeField] private State		m_decisionTrueState = null;
		[SerializeField] private State		m_decisionFalseState = null;










		//********************************************************************************
		//***** Functions : Enter/Exit ***************************************************

		public void Enter_Transition(StateMachine t_fsm)
		{
			// Enter decision
			if (m_decision != null)
			{
				m_decision.SetData(t_fsm);
				m_decision.Enter_Decision();
			}
		}


		public void Exit_Transition(StateMachine t_fsm)
		{
			// Exit decision
			if (m_decision != null)
			{
				m_decision.SetData(t_fsm);
				m_decision.Exit_Decision();
			}
		}










		//********************************************************************************
		//***** Functions : Update *******************************************************

		public void Update_Transition(StateMachine t_fsm)
		{
			// Change state if needed
			if (m_decision != null)
			{
				m_decision.SetData(t_fsm);
				bool decision = m_decision.Update_Decision();

				if (decision && m_decisionTrueState != null)
					t_fsm.ChangeState(m_decisionTrueState);
				if (!decision && m_decisionFalseState != null)
					t_fsm.ChangeState(m_decisionFalseState);
			}
		}
	}
}