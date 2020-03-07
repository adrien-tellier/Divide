using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Game
{
	public class GameManager : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private InputHandler m_inputs = null;

		[SerializeField]
		private SetMark m_setMark = null;

		[SerializeField]
		private ScoreMgr m_scoreMgr = new ScoreMgr();

		[SerializeField]
		private PauseMenu m_pauseMenu = new PauseMenu();

		private TransitionLevel m_transitionLevel = null;
		private bool m_inPause = false;

		//**************************************************************************
		//***** Properties *********************************************************

		public ScoreMgr ScoreMgr
		{
			get { return m_scoreMgr; }
		}



		//**************************************************************************
		//***** Functions **********************************************************

		private void InitEvents(ref List<InputEvent> inputs)
		{
			foreach (InputEvent ie in inputs)
			{
				switch (ie.Action)
				{
					case EInputEventAction.Pause: ie.m_event += OnPause; break;
					default: break;
				}
			}
		}

		private void OnPause(InputEventContext context)
		{
			if (!m_inPause && Time.timeScale == 1)
				SetUpPauseMenu(true);
		}

		public void SetUpPauseMenu(bool pause)
		{
			if (pause)
			{
				m_inPause = true;
				m_pauseMenu.InitPauseMenu("Open");
			}
			else
			{
				m_inPause = false;
				m_pauseMenu.InitPauseMenu("Close");
			}

			SetPause(pause);
		}

		public void OnPlayerWin()
		{
			SetPause(true);

			m_scoreMgr.CalculateMark();

			m_setMark.SetUp(m_scoreMgr.Mark);
		}

		public void OnPlayerDie()
		{
			SetPause(true);
		}

		public void OnEndLevel()
		{
			m_transitionLevel.RespawnLevel();
		}

		public void OnAlert()
		{
			m_scoreMgr.OnAlert();
		}

		public void OnCollect()
		{
			m_scoreMgr.OnCollect();
		}

		public void SetPause(bool pause)
		{
			if (pause)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		}

		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void Awake()
		{
			m_transitionLevel = GetComponent<TransitionLevel>();
		}

		// Start is called before the first frame update
		private void Start()
		{
			m_scoreMgr.Init();
			if (m_inputs != null)
				InitEvents(ref m_inputs.Inputs);
		}
	}
}