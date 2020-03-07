using UnityEditor;
using UnityEngine;


namespace Game
{
	[CustomEditor(typeof(Trigger))]
	public class TriggerEditor : Editor
	{
		//**************************************************************************
		//***** Fields *************************************************************

		private SerializedProperty m_activeEvents = null;
		private SerializedProperty m_desactiveEvents = null;
		private SerializedProperty m_autoActive = null;
		private SerializedProperty m_inputs = null;
		private SerializedProperty m_autoDesactive = null;
		private SerializedProperty m_timer = null;





		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void OnEnable()
		{
			m_activeEvents = serializedObject.FindProperty("m_activeEvents");
			m_desactiveEvents = serializedObject.FindProperty("m_desactiveEvents");
			m_autoActive = serializedObject.FindProperty("m_autoActive");
			m_inputs = serializedObject.FindProperty("m_inputs");
			m_autoDesactive = serializedObject.FindProperty("m_autoDesactive");
			m_timer = serializedObject.FindProperty("m_timer");
		}


		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(m_activeEvents);
			EditorGUILayout.PropertyField(m_desactiveEvents);

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

			GUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(m_autoActive);
			if (!m_autoActive.boolValue)
				EditorGUILayout.PropertyField(m_inputs, GUIContent.none);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(m_autoDesactive);
			if (m_autoDesactive.boolValue)
				EditorGUILayout.PropertyField(m_timer, GUIContent.none);
			GUILayout.EndHorizontal();

			serializedObject.ApplyModifiedProperties();
		}
	}
}