using UnityEngine;
using UnityEditor;


namespace Game
{
	[CustomPropertyDrawer(typeof(RequiredAttribute))]
	public class RequiredAttributeDrawer : PropertyDrawer
	{
		//**************************************************************************
		//***** Fields *************************************************************

		private float		m_helpBoxHeight = 40f;
		private float		m_separatorHeight = 2f;
		private float		m_fieldHeight = 16f;





		//**************************************************************************
		//***** Functions : Unity **************************************************

		public override void OnGUI(Rect t_position, SerializedProperty t_property,
			GUIContent t_label)
		{
			//begin drawer
			EditorGUI.BeginProperty(t_position, t_label, t_property);
			RequiredAttribute m_required = attribute as RequiredAttribute;

			//save indentation
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			//draw
			if (t_property.objectReferenceValue == null)
			{
				PropertyHelpBoxAndField(t_position, t_property, m_required.m_message);
				EnableScript(t_property, false);
			}
			else
				EditorGUI.PropertyField(t_position, t_property);

			//end drawer
			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}


		public override float GetPropertyHeight(SerializedProperty t_property,
			GUIContent t_label)
		{
			if (t_property.objectReferenceValue == null)
				return EditorGUI.GetPropertyHeight(t_property, t_label, true) +
					m_separatorHeight + m_helpBoxHeight;

			return EditorGUI.GetPropertyHeight(t_property, t_label, true);
		}





		//**************************************************************************
		//***** Functions **********************************************************

		private void EnableScript(SerializedProperty t_property, bool t_enable)
		{
			MonoBehaviour script = t_property.serializedObject.targetObject as MonoBehaviour;
			script.enabled = t_enable;
		}


		private void PropertyHelpBoxAndField(Rect t_position, SerializedProperty t_property,
			string t_message)
		{
			Rect helpBoxdRect = new Rect(t_position);
			helpBoxdRect.height = m_helpBoxHeight;

			EditorGUI.HelpBox(helpBoxdRect, t_message, MessageType.Warning);

			Rect fieldRect = new Rect(t_position);
			fieldRect.yMin = t_position.position.y + m_helpBoxHeight +
				m_separatorHeight;
			fieldRect.height = m_fieldHeight;

			EditorGUI.PropertyField(fieldRect, t_property);
		}
	}
}