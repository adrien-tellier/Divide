using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class PauseMenu
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private Animator m_pauseAnim = null;
		[SerializeField]
		private Animator m_optionsAnim = null;

		[SerializeField]
		[Rename("First button of pause menu")]
		private GameObject m_button = null;

		[SerializeField]
		private EventSystem m_eventSystem = null;

		//**************************************************************************
		//***** Functions **********************************************************

		public void InitPauseMenu(string trigger)
		{
			m_pauseAnim.SetTrigger(trigger);
			m_eventSystem.SetSelectedGameObject(m_button);

			if (trigger == "Close")
				m_eventSystem.SetSelectedGameObject(null);
		}
	}
}