using UnityEngine;


namespace Game
{
	[CreateAssetMenu(menuName = "Actions/Rotate")]
	public class Rotate : Action
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		[Header("Settings")]
		[SerializeField] private ERotation					m_rotation = ERotation.None;
		[SerializeField, Range(0.01f, 10f)] private float	m_speed = 2f;










		//********************************************************************************
		//***** Functions : Override *****************************************************

		public override void Update_Action()
		{
			Transform nav = m_data.NavMeshAgent.transform;
			nav.rotation = Quaternion.Slerp(nav.rotation, GetRotation(), m_speed * Time.deltaTime);
		}










		//********************************************************************************
		//***** Functions ****************************************************************

		private Quaternion GetRotation()
		{
			switch (m_rotation)
			{
				case ERotation.InitialRotation:
					return m_data.InitialRotation;
				default:
					return m_data.Transform.rotation;
			}
		}
	}
}