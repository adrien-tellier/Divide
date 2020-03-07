using UnityEngine;


namespace Game
{
	[CreateAssetMenu(menuName = "Actions/LookAround")]
	public class LookAround : Action
	{
		//******************************************************************************
		//***** Fields *****************************************************************

		[SerializeField] private bool						m_orientation = true;
		[SerializeField, Range(0f, 180f)] private float		m_rotation = 45f;
		[SerializeField, Range(0f, 10f)] private float		m_speed = 1f;










		//******************************************************************************
		//***** Functions : Override ***************************************************

		public override void FixedUpdate_Action()
		{
			// Update rotation
			m_data.Transform.eulerAngles += GetRotation() * Time.fixedDeltaTime * 50;
		}










		//******************************************************************************
		//***** Functions **************************************************************

		private Vector3 GetRotation()
		{
			// Vars
			float rotation = m_speed;

			// Calcul rotation
			if (m_rotation < 180f)
			{
				// Overtaking
				if (m_speed + m_data.LookAround_CurrentRotation > m_rotation)
				{
					rotation = Mathf.Abs(2 * (m_rotation - m_data.LookAround_CurrentRotation) - m_speed);
					m_orientation = false;
				}
				else if (m_speed - m_data.LookAround_CurrentRotation > m_rotation)
				{
					rotation = Mathf.Abs(2 * (-m_rotation - m_data.LookAround_CurrentRotation) - m_speed);
					m_orientation = true;
				}
				// Direction
				if (!m_orientation)
					rotation *= -1f;
			}

			// Update currentRotation
			m_data.LookAround_CurrentRotation += rotation;
			m_data.LookAround_CurrentRotation %= 360f;
			return new Vector3(0f, rotation, 0f);
		}
	}
}