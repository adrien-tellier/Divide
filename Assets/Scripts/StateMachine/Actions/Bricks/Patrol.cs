using UnityEngine;


namespace Game
{
	[CreateAssetMenu(menuName = "Actions/Patrol")]
	public class Patrol : Action
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		[Header("Settings")]
		[SerializeField, Range(0f, 10f)] private float		m_speed = 2f;
		[SerializeField] private bool						m_random = false;










		//********************************************************************************
		//***** Functions : Override *****************************************************

		public override void Enter_Action()
		{
			// Init NavMeshAgent
			m_data.NavMeshAgent.speed = m_speed;
			NextDestination();
		}


		public override void Update_Action()
		{
			// Vars
			bool pathPending = m_data.NavMeshAgent.pathPending;
			bool isNear = m_data.NavMeshAgent.remainingDistance < 0.5f;

			// Update NavMeshAgent destination
			if (!pathPending && isNear)
				NextDestination();
		}


		public override void Exit_Action()
		{
			// Save position
			m_data.PatrolLastPosition = m_data.Transform.position;
		}










		//********************************************************************************
		//***** Functions ****************************************************************

		private void NextDestination()
		{
			// Set new destination
			m_data.NavMeshAgent.destination = m_data.Patrol[m_data.Patrol_Next];

			// Get next destination
			int len = m_data.Patrol.Count;
			m_data.Patrol_Next = m_random ? Random.Range(0, len) : (m_data.Patrol_Next + 1) % len;
		}
	}
}