using UnityEngine;
using UnityEngine.Events;


namespace Game
{
	public class CDetectable : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private Events		m_events = null;

		[Space()]

		[SerializeField]
		private UnityEvent	m_actions = null;




		//**************************************************************************
		//***** Functions **********************************************************

		public void Detected()
		{
			m_events?.Raise();
			m_actions?.Invoke();
		}
	}
}