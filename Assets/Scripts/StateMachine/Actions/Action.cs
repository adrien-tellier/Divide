using UnityEngine;


namespace Game
{
	public abstract class Action : ScriptableObject
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Hide
		protected Data		m_data = null;










		//********************************************************************************
		//***** Functions : SetData ******************************************************

		public void SetData(StateMachine t_fsm)
		{
			// Set Data
			m_data = t_fsm.Data;
		}










		//********************************************************************************
		//***** Functions : Virtual (Enter/Exit) *****************************************

		public virtual void Enter_Action() {}
		public virtual void Exit_Action() {}










		//********************************************************************************
		//***** Functions : Virtual (Trigger) ********************************************

		public virtual void OnTriggerEnter_Action(Collider t_other) {}
		public virtual void OnTriggerStay_Action(Collider t_other) {}
		public virtual void OnTriggerExit_Action(Collider t_other) {}










		//********************************************************************************
		//***** Functions : Virtual (Update) *********************************************

		public virtual void FixedUpdate_Action() {}
		public virtual void Update_Action() {}
		public virtual void LateUpdate_Action() {}
	}
}