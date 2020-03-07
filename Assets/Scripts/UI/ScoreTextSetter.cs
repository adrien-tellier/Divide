using UnityEngine;
using UnityEngine.UI;


namespace Game
{
	[RequireComponent(typeof(Text))]
	public class ScoreTextSetter : MonoBehaviour
	{
		//******************************************************************************
		//***** Fields *****************************************************************

		// Serialized
		[SerializeField] private string				m_title = "Coins";
		[SerializeField] private GameManager		m_gameMgr = null;
		[SerializeField] private Color				m_color = new Color(0f, 0f, 0f);

		// Hide
		private Text		m_text = null;
		private ScoreMgr	m_scoreMgr = null;
		private int			m_currentScore = -1;
		private int			m_scoreMax = 0;





		//******************************************************************************
		//***** Functions : Unity ******************************************************

		private void Start()
		{
			m_text = GetComponent<Text>();
			m_text.color = m_color;
			m_scoreMgr = m_gameMgr.ScoreMgr;
			m_scoreMax = m_scoreMgr.ScoreMax;
		}


		private void Update()
		{
			if (m_currentScore != m_scoreMgr.Score)
			{
				m_text.text = m_title + " : " + m_scoreMgr.Score.ToString() + " / " + m_scoreMax;
				m_currentScore = m_scoreMgr.Score;
			}
		}
	}
}