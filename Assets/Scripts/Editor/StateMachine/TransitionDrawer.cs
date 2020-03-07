using UnityEditor;
using UnityEngine;


namespace Game
{
	[CustomPropertyDrawer(typeof(Transition))]
	public class TransitionDrawer : PropertyDrawer
	{
		//**************************************************************************
		//***** Functions : Unity **************************************************

		public override void OnGUI(Rect t_rect, SerializedProperty t_property, GUIContent t_label)
		{
			//begin drawer
			EditorGUI.BeginProperty(t_rect, t_label, t_property);
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			float space = 5;

			t_rect.width = (t_rect.width - 2 * space) / 3f;
			EditorGUI.PropertyField(t_rect, t_property.FindPropertyRelative("m_decision"), GUIContent.none);

			t_rect.x += t_rect.width + space;
			EditorGUI.PropertyField(t_rect, t_property.FindPropertyRelative("m_decisionTrueState"), GUIContent.none);

			t_rect.x += t_rect.width + space;
			EditorGUI.PropertyField(t_rect, t_property.FindPropertyRelative("m_decisionFalseState"), GUIContent.none);

			//end drawer
			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}