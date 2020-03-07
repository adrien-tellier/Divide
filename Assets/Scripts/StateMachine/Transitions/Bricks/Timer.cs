using UnityEngine;


namespace Game
{
	[CreateAssetMenu(menuName = "Decisions/Timer")]
	public class Timer : Decision
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField, Range(0f, 10f)] private float		m_timer = 0f;
		









		//********************************************************************************
		//***** Functions : Override *****************************************************

		public override void Enter_Decision()
		{
			ResetTimer();
		}


		public override bool Update_Decision()
		{
			UpdateTime();

			return CheckTime();
		}










		//********************************************************************************
		//***** Functions : Enter ********************************************************

		private void ResetTimer()
		{
			// Reset timer
			m_data.Timer_Time = 0f;
			m_data.Timer_IsTimeElapsed = false;
		}










		//********************************************************************************
		//***** Functions : Update *******************************************************

		private void UpdateTime()
		{
			// Update time
			if (!m_data.Timer_IsTimeElapsed)
				m_data.Timer_Time += Time.deltaTime;
		}


		private bool CheckTime()
		{
			// Check if time elapsed
			if (!m_data.Timer_IsTimeElapsed && m_data.Timer_Time > m_timer)
				m_data.Timer_IsTimeElapsed = true;

			return m_data.Timer_IsTimeElapsed;
		}
	}
}