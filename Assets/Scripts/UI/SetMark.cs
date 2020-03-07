using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace Game
{
	public class SetMark : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private Text m_markText = null;

		[SerializeField]
		private GameObject m_firstButtonSelected = null;

		[SerializeField]
		private EventSystem m_eventSystem = null;

		[SerializeField]
		private Animator m_anim = null;


		public void SetUp(string mark)
		{
			// Set the mark in the text
			m_markText.text = mark;

			// Animation of end menu
			m_anim.SetTrigger("Open");

			// Set the first button selected
			m_eventSystem.SetSelectedGameObject(m_firstButtonSelected);
		}
	}
}