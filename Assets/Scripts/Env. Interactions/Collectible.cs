using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Collectible : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private LayerMask m_layers;

		[SerializeField]
		private Events m_events = null;

		private MeshRenderer m_renderer = null;
		private MeshCollider m_collider = null;
		private Collectible m_collectible = null;
		private Animation m_animation = null;

		//**************************************************************************
		//***** Properties *********************************************************


		//**************************************************************************
		//***** Functions **********************************************************

		public void Disable()
		{
			m_renderer.enabled = false;
			m_collider.enabled = false;
			//m_animation.enabled = false;
			m_collectible.enabled = false;
		}

		public void Enable()
		{
			m_renderer.enabled = true;
			m_collider.enabled = true;
			//m_animation.enabled = true;
			m_collectible.enabled = true;
		}

		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void Awake()
		{
			m_renderer = GetComponent<MeshRenderer>();
			m_collider = GetComponent<MeshCollider>();
			m_animation = GetComponent<Animation>();
			m_collectible = GetComponent<Collectible>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((m_layers.value & 1 << other.gameObject.layer) > 0)
			{
				m_events?.Raise();
				Disable();
			}
		}
	}
}
