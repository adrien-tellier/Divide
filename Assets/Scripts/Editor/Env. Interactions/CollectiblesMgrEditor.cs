using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game
{
	[CustomEditor(typeof(CollectiblesMgr))]
	public class CollectiblesMgrEditor : Editor
	{
		//**************************************************************************
		//***** Functions : Unity **************************************************

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			{
				base.OnInspectorGUI();

				GUI.enabled = Application.isPlaying;

				CollectiblesMgr cMgr = target as CollectiblesMgr;

				if (GUILayout.Button("Enable Collectibles"))
					cMgr.EnableCollectibles();

				if (GUILayout.Button("Disable Collectibles"))
					cMgr.DisableCollectibles();
			}
		}
	}
}