using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class CollectiblesMgr : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		private Collectible[] m_collectibles = null;

		//**************************************************************************
		//***** Functions **********************************************************

		public void EnableCollectibles()
		{
			foreach (Collectible c in m_collectibles)
				c.Enable();
		}

		public void DisableCollectibles()
		{
			foreach (Collectible c in m_collectibles)
				c.Disable();
		}

		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void Awake()
		{
			m_collectibles = GetComponentsInChildren<Collectible>();
		}
	}
}