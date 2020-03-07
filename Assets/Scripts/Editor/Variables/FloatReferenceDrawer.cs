using UnityEditor;
using UnityEngine;


namespace Game
{
	[CustomPropertyDrawer(typeof(FloatReference))]
	public class FloatReferenceDrawer : PropertyDrawer
	{
		//**************************************************************************
		//***** Fields *************************************************************

		private readonly string[]	m_popupOptions = { "Float", "FloatVariable", "const Float", "const FloatVariable" };
		private GUIStyle			m_popupStyle = null;





		//**************************************************************************
		//***** Functions : Unity **************************************************

		public override void OnGUI(Rect t_position, SerializedProperty t_property,
			GUIContent t_label)
		{
			//load popup style
			if (m_popupStyle == null)
			{
				m_popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
				m_popupStyle.imagePosition = ImagePosition.ImageOnly;
			}

			//begin drawer
			EditorGUI.BeginProperty(t_position, t_label, t_property);
			t_position = EditorGUI.PrefixLabel(t_position, t_label);

			//save indentation
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			//calculate rect
			Rect popupRect = new Rect(t_position);
			popupRect.yMin += m_popupStyle.margin.top;
			popupRect.width = m_popupStyle.fixedWidth + m_popupStyle.margin.right;

			//update position rect
			t_position.xMin = popupRect.xMax;

			//get properties
			SerializedProperty type = t_property.FindPropertyRelative("m_type");
			SerializedProperty value = t_property.FindPropertyRelative("m_value");
			SerializedProperty variable = t_property.FindPropertyRelative("m_variable");
			SerializedProperty constValue = t_property.FindPropertyRelative("m_constValue");
			SerializedProperty constVariable = t_property.FindPropertyRelative("m_constVariable");

			//draw popup
			type.intValue = EditorGUI.Popup(popupRect, type.intValue, m_popupOptions, m_popupStyle);

			//draw fields
			if (type.intValue == 0)
				EditorGUI.PropertyField(t_position, value, GUIContent.none);
			else if (type.intValue == 1)
				EditorGUI.PropertyField(t_position, variable, GUIContent.none);
			else if (type.intValue == 2)
				EditorGUI.PropertyField(t_position, constValue, GUIContent.none);
			else if (type.intValue == 3)
				EditorGUI.PropertyField(t_position, constVariable, GUIContent.none);

			//end drawer
			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}