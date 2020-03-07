using UnityEditor;
using UnityEngine;


namespace Game
{
	[CustomEditor(typeof(Event))]
	public class EventEditor : Editor
	{
		//**************************************************************************
		//***** Functions : Unity **************************************************

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUI.enabled = Application.isPlaying;

			Event e = target as Event;
			if (GUILayout.Button("Raise"))
				e.Raise();
		}
	}
}