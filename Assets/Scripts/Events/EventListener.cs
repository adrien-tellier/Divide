using UnityEngine;
using UnityEngine.Events;


namespace Game
{
	public class EventListener : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private Events			m_events = null;

		[Space(5)]

		[SerializeField]
		private UnityEvent		m_actions = null;





		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void OnEnable()
		{
			m_events?.Subscribe(m_actions);
		}


		private void OnDisable()
		{
			m_events?.Unsubscribe(m_actions);
		}
	}
}