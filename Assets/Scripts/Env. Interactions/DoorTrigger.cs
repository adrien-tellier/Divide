using System.Collections.Generic;
using UnityEngine;


namespace Game
{
	[RequireComponent(typeof(BoxCollider))]
	public class DoorTrigger : MonoBehaviour
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[Header("Settings")]
		[SerializeField] private Animator					m_animator = null;
		[SerializeField, Required] private InputHandler		m_inputs = null;
		[SerializeField, Range(0.1f, 2f)] private float		m_lockTime = 1f;
		[SerializeField] private LayerMask					m_instantActiveLayer = 0;
		[SerializeField] private LayerMask					m_playerLayer = 0;
		[SerializeField] private bool						m_isOpen = false;

		[Header("Display")]
		[SerializeField] private Material					m_openMaterial = null;
		[SerializeField] private Material					m_closeMaterial = null;

		[Header("Event")]
		[SerializeField] private Event						m_TriggerEnter = null;
		[SerializeField] private Event						m_TriggerExit = null;
		[SerializeField] private Events						m_click = null;

		// Hide
		private MeshRenderer	m_meshRenderer = null;
		private bool			m_interact = false;
		private float			m_time = 0f;










		//********************************************************************************
		//***** Functions : Unity ********************************************************

		private void Start()
		{
			// Get component
			m_meshRenderer = GetComponent<MeshRenderer>();

			// Initilize
			InitEvent(ref m_inputs.Inputs);
			m_animator.SetBool("IsOpen", m_isOpen);
			m_meshRenderer.material = m_isOpen ? m_openMaterial : m_closeMaterial;
		}


		private void OnTriggerEnter(Collider t_other)
		{
			// Condition for objects in instant active layer (ex: clone)
			if (Utility.IsInLayer(t_other.gameObject, m_instantActiveLayer))
				ActiveTrigger();

			if (Utility.IsInLayer(t_other.gameObject, m_playerLayer))
				m_TriggerEnter?.Raise();
		}


		private void OnTriggerStay(Collider t_other)
		{
			// Time elapsed & inputs triggered
			if (m_time > m_lockTime)
				if (Utility.IsInLayer(t_other.gameObject, m_playerLayer) && m_interact)
					ActiveTrigger();
		}


		private void OnTriggerExit(Collider t_other)
		{
			// Raise trigger
			m_TriggerExit?.Raise();
		}


		private void Update()
		{
			// Update timer
			if (m_time < m_lockTime)
				m_time += Time.deltaTime;
		}










		//********************************************************************************
		//***** Functions : Active Trigger ***********************************************

		private void ActiveTrigger()
		{
			// Update door status boolean (local and in given animator)
			m_isOpen = !m_isOpen;
			m_animator.SetBool("IsOpen", m_isOpen);
			m_meshRenderer.material = m_isOpen ? m_openMaterial : m_closeMaterial;

			// Reset time
			m_time = 0f;

			// Raise trigger
			m_click?.Raise();
		}










		//********************************************************************************
		//***** Functions : InputHandler *************************************************

		private void InitEvent(ref List<InputEvent> inputs)
		{
			// Subscribe local action functions in inputHandler
			foreach (InputEvent ie in inputs)
			{
				switch (ie.Action)
				{
					case EInputEventAction.Interact: ie.m_event += OnInteract; break;
					case EInputEventAction.StopInteract: ie.m_event += OnStopInteract; break;
					default: break;
				}
			}
		}


		private void OnInteract(InputEventContext context)
		{
			// Input action on
			m_interact = true;
		}

		private void OnStopInteract(InputEventContext context)
		{
			// input action off
			m_interact = false;
		}
	}
}