using UnityEngine;


namespace Game
{
	[CreateAssetMenu(menuName = "Actions/GoTo")]
	public class GoTo : Action
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		[Header("Settings")]
		[SerializeField] private EDestination				m_destination = EDestination.None;
		[SerializeField, Range(0f, 10f)] private float		m_speed = 2f;










		//********************************************************************************
		//***** Functions : Override *****************************************************

		public override void Enter_Action()
		{
			// Init NavMeshAgent
			m_data.NavMeshAgent.speed = m_speed;
			m_data.NavMeshAgent.destination = GetDestination();
		}










		//********************************************************************************
		//***** Functions ****************************************************************

		private Vector3 GetDestination()
		{
			switch (m_destination)
			{
				case EDestination.InitialPosition:
					return m_data.InitialPosition;
				case EDestination.TargetLastPosition:
					return m_data.TargetLastPosition;
				case EDestination.PatrolLastPosition:
					return m_data.PatrolLastPosition;
				case EDestination.NearestPatrolPosition:
					return GetNearestPatrolPosition();
				default:
					return m_data.Transform.position;
			}
		}










		//********************************************************************************
		//***** Functions : GteDestination ***********************************************

		private Vector3 GetNearestPatrolPosition()
		{
			// Vars
			Vector3 nearest = m_data.PatrolLastPosition;
			Vector3 pos = m_data.Transform.position;
			float nearestPowDistance = Vector3.SqrMagnitude(nearest - pos);
			float powDistance = 0f;

			// Search nearest
			for (int i = 0; i < m_data.Patrol.Count; ++i)
			{
				// Calcul current point distance
				powDistance = Vector3.SqrMagnitude(m_data.Patrol[i] - pos);

				// Compare with nearest one
				if (powDistance < nearestPowDistance)
				{
					nearestPowDistance = powDistance;
					nearest = m_data.Patrol[i];
					m_data.Patrol_Next = i;
				}
			}

			return nearest;
		}
	}
}