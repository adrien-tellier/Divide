using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game
{
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(SphereCollider))]
	[RequireComponent(typeof(MeshCollider))]
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class StateMachine : MonoBehaviour
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField] private State			m_currentState = null;
		[SerializeField] private Events			m_events = null;

		// Hide
		private bool		m_isActive = true;
		public Data			Data { get; private set; }











		//********************************************************************************
		//***** Functions : Unity (Start) ************************************************

		private void Start()
		{
			// Start data
			Data = new Data(transform);

			// Start first state
			m_currentState?.Enter_State(this);

			// Subscribe to event
			m_events.Subscribe( () => { m_isActive = false; });
		}










		//********************************************************************************
		//***** Functions : Unity (Trigger) **********************************************

		private void OnTriggerEnter(Collider t_other)
		{
			// OnTriggerEnter current state
			if (m_isActive)
				m_currentState?.OnTriggerEnter_State(t_other, this);
		}


		private void OnTriggerStay(Collider t_other)
		{
			// OnTriggerStay current state
			if (m_isActive)
				m_currentState?.OnTriggerStay_State(t_other, this);
		}


		private void OnTriggerExit(Collider t_other)
		{
			// OnTriggerExit current state
			if (m_isActive)
				m_currentState?.OnTriggerExit_State(t_other, this);
		}










		//********************************************************************************
		//***** Functions : Unity (Update) ***********************************************

		private void FixedUpdate()
		{
			// FixedUpdate current state
			if (m_isActive)
				m_currentState?.FixedUpdate_State(this);
		}


		private void Update()
		{
			// Update current state
			if (m_isActive)
				m_currentState?.Update_State(this);
		}


		private void LateUpdate()
		{
			// LateUpdate current state
			if (m_isActive)
				m_currentState?.LateUpdate_State(this);
		}










		//********************************************************************************
		//***** Functions : StateMachine *************************************************

		public void ChangeState(State t_state)
		{
			// Exit current state
			m_currentState?.Exit_State(this);

			// Enter new state
			m_currentState = t_state;
			m_currentState?.Enter_State(this);
		}
	}
}