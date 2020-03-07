using UnityEditor;
using UnityEngine;


namespace Game
{
	[CustomPropertyDrawer(typeof(Actions))]
	public class ActionsDrawer : PropertyDrawer
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
			SerializedProperty actions = t_property.FindPropertyRelative("m_actions");
			EditorUtility.DrawArray(t_position, actions, t_label);

			// End drawer
			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}


		public override float GetPropertyHeight(SerializedProperty t_property, GUIContent t_label)
		{
			// Get property height
			SerializedProperty events = t_property.FindPropertyRelative("m_actions");
			return EditorUtility.ArrayHeight(events);
		}
	}
}