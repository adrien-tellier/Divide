using UnityEngine;


namespace Game
{
	[CreateAssetMenu(menuName = "Decisions/ArrivedAt")]
	public class ArrivedAt : Decision
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField, Range(0f, 1f)] private float		m_distance = 0.5f;










		//********************************************************************************
		//***** Functions : Override *****************************************************

		public override bool Update_Decision()
		{
			// Vars
			bool pathPending = m_data.NavMeshAgent.pathPending;
			bool isNear = m_data.NavMeshAgent.remainingDistance < m_distance;

			// Check if is arrived
			return !pathPending && isNear;
		}
	}
}