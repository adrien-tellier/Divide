using UnityEngine;


namespace Game
{
	[CreateAssetMenu(menuName = "Decisions/IsDetected")]
	public class IsDetected : Decision
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField, Range(0.01f, 2.5f)] private float		m_ReactionTime = 1f;
		[SerializeField] private bool							m_display = true;


		







		//********************************************************************************
		//***** Functions : Override *****************************************************

		public override void Enter_Decision()
		{
			GetTime();
		}


		public override bool Update_Decision()
		{
			UpdateTime();
			UpdateDetectionBar();

			return CheckTime();
		}


		public override void Exit_Decision()
		{
			SaveTime();
		}










		//********************************************************************************
		//***** Functions : Enter ********************************************************

		private void GetTime()
		{
			// Get time in data
			m_data.IsDetected_Time = m_data.IsDetected_Time * m_ReactionTime;
		}










		//********************************************************************************
		//***** Functions : Update *******************************************************

		private void UpdateTime()
		{
			// Vars
			float factor = m_data.IsDetected ? 1 : -1;

			// Update time
			m_data.IsDetected_PrevTime = m_data.IsDetected_Time;
			m_data.IsDetected_Time += Time.deltaTime * factor;
			m_data.IsDetected_Time = Mathf.Clamp(m_data.IsDetected_Time, 0f, m_ReactionTime);
		}


		private void UpdateDetectionBar()
		{
			// Vars
			bool IsEmpty = m_data.IsDetected_Time == 0f;
			bool isDisplay = m_display && !IsEmpty;
			bool isTimeChanged = m_data.IsDetected_Time != m_data.IsDetected_PrevTime;

			// Update detection bar
			if (isDisplay && isTimeChanged)
				m_data.DetectionBar.size = m_data.IsDetected_Time / m_ReactionTime;

			// Active/Desactive detection bar
			if (isDisplay != m_data.IsDetected_WasDisplay)
			{
				m_data.DetectionBar.gameObject.SetActive(isDisplay);
				m_data.IsDetected_WasDisplay = isDisplay;
			}

			// Lock canvas rotation
			if (isDisplay)
				m_data.Canvas.transform.eulerAngles = Vector3.zero;
		}


		private bool CheckTime()
		{
			//debug
			if (m_data.Target != null && !m_data.Target.gameObject.activeInHierarchy)
				return false;

			// Check if time elapsed
			if (m_data.IsDetected_Time == m_ReactionTime)
			{
				m_data.IsDetectedOnce = true;
				return true;
			}
			return false;
		}










		//********************************************************************************
		//***** Functions : Exit *********************************************************

		private void SaveTime()
		{
			// Set time in data
			m_data.IsDetected_Time = m_data.IsDetected_Time / m_ReactionTime;
		}
	}
}