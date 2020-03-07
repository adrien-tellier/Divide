using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Game
{
	[CreateAssetMenu]
	public class Event : ScriptableObject
	{
		//**************************************************************************
		//***** Fields *************************************************************

		// Hide
		private List<UnityEvent>	m_events = new List<UnityEvent>();	

		public delegate void EventDelegate();
		private event EventDelegate EventAction;





		//**************************************************************************
		//***** Functions **********************************************************

		public void Raise()
		{
			for (int i = 0; i < m_events.Count; ++i)
				m_events[i].Invoke();
			EventAction?.Invoke();
		}


		public void Subscribe(UnityEvent t_event)
		{
			if (!m_events.Contains(t_event))
				m_events.Add(t_event);
		}

		public void Subscribe(EventDelegate t_action)
		{
			EventAction += t_action;
		}


		public void Unsubscribe(UnityEvent t_event)
		{
			if (m_events.Contains(t_event))
				m_events.Remove(t_event);
		}

		public void Unsubscribe(EventDelegate t_action)
		{
			EventAction -= t_action;
		}
	}
}