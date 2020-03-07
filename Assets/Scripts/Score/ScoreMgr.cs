using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class ScoreMgr
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private List<Mark> m_marks = null;

		[SerializeField]
		private string m_perfectMarkName = "";

		[SerializeField]
		private int m_numberCollectibles = 0;

		private string m_mark = "";

		private int m_collectibles = 0;
		private int m_alerts = 0;

		//**************************************************************************
		//***** Properties *********************************************************

		public string Mark
		{
			get { return m_mark; }
		}

		public int ScoreMax
		{
			get { return m_numberCollectibles; }
		}

		public int Score
		{
			get { return m_collectibles; }
		}

		//**************************************************************************
		//***** Functions : Unity **************************************************

		// Start is called before the first frame update
		public void Init()
		{
			m_marks.Sort();
			m_marks.Reverse();
			m_mark = m_marks[0].Name;
		}

		//**************************************************************************
		//***** Functions **********************************************************

		public void OnCollect()
		{
			m_collectibles++;
		}

		public void OnAlert()
		{
			m_alerts++;
		}

		public void ResetScore()
		{
			m_collectibles = 0;
			m_alerts = 0;
			m_mark = m_marks[0].Name;
		}

		public void CalculateMark()
		{
			foreach (Mark m in m_marks)
			{
				if (m.CheckMark(m_alerts))
					m_mark = m.Name;
			}

			if (m_mark == m_marks[m_marks.Count - 1].Name && m_collectibles >= m_numberCollectibles)
				m_mark = m_perfectMarkName;
		}
	}
}