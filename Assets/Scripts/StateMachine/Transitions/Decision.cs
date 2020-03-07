using UnityEngine;


namespace Game
{
	public abstract class Decision : ScriptableObject
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Hide
		protected Data		m_data = null;










		//********************************************************************************
		//***** Functions ****************************************************************

		public void SetData(StateMachine t_fsm)
		{
			// Set Data
			m_data = t_fsm.Data;
		}










		//********************************************************************************
		//***** Functions : Virtual (Enter/Exit) *****************************************

		public virtual void Enter_Decision() {}
		public virtual void Exit_Decision() {}










		//********************************************************************************
		//***** Functions : Virtual (Update) *********************************************

		public virtual bool Update_Decision() { return false; }
	}
}