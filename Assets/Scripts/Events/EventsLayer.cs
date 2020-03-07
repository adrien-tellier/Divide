using System;
using UnityEngine;
using UnityEngine.Events;
using static Game.Event;


namespace Game
{
	[Serializable]
	public class EventsLayer
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField] private EventLayer[]	m_events = null;










		//********************************************************************************
		//***** Functions ****************************************************************

		public void Raise(GameObject t_obj)
		{
			for (int i = 0; i < m_events.Length; ++i)
				m_events[i]?.Raise(t_obj);
		}


		public void Subscribe(UnityEvent t_event)
		{
			for (int i = 0; i < m_events.Length; ++i)
				m_events[i]?.Subscribe(t_event);
		}


		public void Subscribe(EventDelegate t_action)
		{
			for (int i = 0; i < m_events.Length; ++i)
				m_events[i]?.Subscribe(t_action);
		}


		public void Unsubscribe(UnityEvent t_event)
		{
			for (int i = 0; i < m_events.Length; ++i)
				m_events[i]?.Unsubscribe(t_event);
		}


		public void Unsubscribe(EventDelegate t_action)
		{
			for (int i = 0; i < m_events.Length; ++i)
				m_events[i]?.Unsubscribe(t_action);
		}
	}
}