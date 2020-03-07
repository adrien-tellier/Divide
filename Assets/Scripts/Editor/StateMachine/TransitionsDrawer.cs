﻿using UnityEditor;
using UnityEngine;


namespace Game
{
	[CustomPropertyDrawer(typeof(Transitions))]
	public class TransitionsDrawer : PropertyDrawer
	{
		//**************************************************************************
		//***** Functions : Unity **************************************************

		public override void OnGUI(Rect t_position, SerializedProperty t_property,
			GUIContent t_label)
		{
			// Begin drawer
			EditorGUI.BeginProperty(t_position, t_label, t_property);
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Get property
			SerializedProperty transitions = t_property.FindPropertyRelative("m_transitions");
			EditorUtility.DrawArray(t_position, transitions, t_label);

			// End drawer
			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}


		public override float GetPropertyHeight(SerializedProperty t_property, GUIContent t_label)
		{
			// Get property height
			SerializedProperty events = t_property.FindPropertyRelative("m_transitions");
			return EditorUtility.ArrayHeight(events);
		}
	}
}