using UnityEngine;
using UnityEditor;


namespace Game
{
	[CustomPropertyDrawer(typeof(RenameAttribute))]
	public class RenameAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect t_position, SerializedProperty t_property, GUIContent t_label)
		{
			EditorGUI.PropertyField(t_position, t_property, new GUIContent((attribute as RenameAttribute).m_name));
		}
	}
}