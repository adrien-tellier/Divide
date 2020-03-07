using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Arrival : MonoBehaviour
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		[SerializeField]
		private LayerMask m_layers;

		[SerializeField]
		private Events m_events = null;

		//********************************************************************************
		//***** Functions : Unity ********************************************************

		private void OnTriggerEnter(Collider other)
		{
			if ((m_layers.value & 1 << other.gameObject.layer) > 0)
			{
				m_events?.Raise();
			}
		}
	}
}