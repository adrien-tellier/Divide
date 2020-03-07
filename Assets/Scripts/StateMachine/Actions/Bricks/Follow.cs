using UnityEngine;
using UnityEngine.AI;


namespace Game
{
	[CreateAssetMenu(menuName = "Actions/Follow")]
	public class Follow : Action
	{
		//******************************************************************************
		//***** Fields *****************************************************************

		[Header("Settings")]
		[SerializeField, Range(0f, 10f)] private float		m_speed = 4f;
		[SerializeField, Range(0f, 10f)] private float		m_rotationSpeed = 2f;
		[SerializeField, Range(0f, 10f)] private float		m_minDistance = 2f;










		//******************************************************************************
		//***** Functions : Override ***************************************************

		public override void Enter_Action()
		{
			// Init NavMeshAgent
			m_data.NavMeshAgent.speed = m_speed;
			if (m_data.Target != null)
				m_data.NavMeshAgent.destination = m_data.Target.position;
			m_data.NavMeshAgent.stoppingDistance = m_minDistance;
		}


		public override void Update_Action()
		{
			// Update NavMeshAgent destination
			if (m_data.Target != null)
			{
				m_data.NavMeshAgent.destination = m_data.Target.position;

				if (m_data.NavMeshAgent.remainingDistance < m_minDistance)
				{
					Transform nav = m_data.NavMeshAgent.transform;
					Quaternion target = Quaternion.LookRotation(m_data.Target.position - nav.position);
					m_data.Transform.rotation = Quaternion.RotateTowards(nav.rotation, target,
						Time.deltaTime * m_rotationSpeed * 50f);
				}
			}
		}


		public override void Exit_Action()
		{
			// Reset stopping distance
			m_data.NavMeshAgent.stoppingDistance = 0f;
		}
	}
}