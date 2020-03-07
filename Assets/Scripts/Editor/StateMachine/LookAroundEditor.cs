using UnityEditor;
using UnityEngine;


namespace Game
{
	[CustomEditor(typeof(LookAround))]
	public class LookAroundEditor : Editor
	{
		//**************************************************************************
		//***** Fields *************************************************************

		private SerializedProperty m_rotation = null;
		private SerializedProperty m_speed = null;
		private SerializedProperty m_orientation = null;





		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void OnEnable()
		{
			m_rotation = serializedObject.FindProperty("m_rotation");
			m_orientation = serializedObject.FindProperty("m_orientation");
			m_speed = serializedObject.FindProperty("m_speed");
		}


		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

			GUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(m_orientation, new GUIContent("Rotation"));
			EditorGUILayout.PropertyField(m_rotation, GUIContent.none);
			GUILayout.EndHorizontal();

			EditorGUILayout.PropertyField(m_speed);

			serializedObject.ApplyModifiedProperties();
		}
	}
}