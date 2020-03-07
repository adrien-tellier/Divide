using System;
using UnityEngine;
using UnityEngine.Events;
using static Game.Event;


namespace Game
{
	[Serializable]
	public class EventLayer
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField] private Event			m_event = null;
		[SerializeField] private LayerMask		m_layer = 0;










		//********************************************************************************
		//***** Functions ****************************************************************

		public void Raise(GameObject t_obj)
		{
			if (Utility.IsInLayer(t_obj, m_layer))
				m_event.Raise();
		}


		public void Subscribe(UnityEvent t_event)
		{
			m_event.Subscribe(t_event);
		}


		public void Subscribe(EventDelegate t_action)
		{
			m_event.Subscribe(t_action);
		}


		public void Unsubscribe(UnityEvent t_event)
		{
			m_event.Unsubscribe(t_event);
		}


		public void Unsubscribe(EventDelegate t_action)
		{
			m_event.Unsubscribe(t_action);
		}
	}
}