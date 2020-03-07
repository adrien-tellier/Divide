using UnityEngine;


namespace Game
{
	[CreateAssetMenu]
	public class State : ScriptableObject
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField] private Actions		m_actions = null;
		[SerializeField] private Transitions	m_transitions = null;










		//********************************************************************************
		//***** Functions : Enter/Exit ***************************************************

		public void Enter_State(StateMachine t_fsm)
		{
			// Enter action/transitions
			m_actions.Enter_Actions(t_fsm);
			m_transitions.Enter_Transitions(t_fsm);
		}


		public void Exit_State(StateMachine t_fsm)
		{
			// Exit actions/transitions
			m_actions.Exit_Actions(t_fsm);
			m_transitions.Exit_Transitions(t_fsm);
		}










		//********************************************************************************
		//***** Functions : Trigger ******************************************************

		public void OnTriggerEnter_State(Collider t_other, StateMachine t_fsm)
		{
			// OnTriggerEnter actions
			m_actions.OnTriggerEnter_Actions(t_other, t_fsm);
		}


		public void OnTriggerStay_State(Collider t_other, StateMachine t_fsm)
		{
			// OnTriggerStay actions
			m_actions.OnTriggerStay_Actions(t_other, t_fsm);
		}


		public void OnTriggerExit_State(Collider t_other, StateMachine t_fsm)
		{
			// OnTriggerExit actions
			m_actions.OnTriggerExit_Actions(t_other, t_fsm);
		}










		//********************************************************************************
		//***** Functions : Update *******************************************************

		public void FixedUpdate_State(StateMachine t_fsm)
		{
			// FixedUpdate actions
			m_actions.FixedUpdate_Actions(t_fsm);
		}


		public void Update_State(StateMachine t_fsm)
		{
			// Update actions/transitions
			m_actions.Update_Actions(t_fsm);
			m_transitions.Update_Transitions(t_fsm);
		}


		public void LateUpdate_State(StateMachine t_fsm)
		{
			// LateUpdate actions
			m_actions.LateUpdate_Actions(t_fsm);
		}
	}
}