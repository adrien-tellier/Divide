using System;
using UnityEngine;


namespace Game
{
	[Serializable]
	public class Transitions
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField] private Transition[]	m_transitions = null;










		//********************************************************************************
		//***** Functions : Enter/Exit ***************************************************

		public void Enter_Transitions(StateMachine t_fsm)
		{
			// Enter transitions
			for (int i = 0; i < m_transitions.Length; ++i)
				m_transitions[i].Enter_Transition(t_fsm);
		}


		public void Exit_Transitions(StateMachine t_fsm)
		{
			// Exit transitions
			for (int i = 0; i < m_transitions.Length; ++i)
				m_transitions[i].Exit_Transition(t_fsm);
		}










		//********************************************************************************
		//***** Functions : Update *******************************************************

		public void Update_Transitions(StateMachine t_fsm)
		{
			// Update transitions
			for (int i = 0; i < m_transitions.Length; ++i)
				m_transitions[i].Update_Transition(t_fsm);
		}
	}
}