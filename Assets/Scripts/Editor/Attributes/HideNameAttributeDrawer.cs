using UnityEngine;
using UnityEditor;


namespace Game
{
	[CustomPropertyDrawer(typeof(HideNameAttribute))]
	public class HideNameAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect t_position, SerializedProperty t_property, GUIContent t_label)
		{
			EditorGUI.PropertyField(t_position, t_property, GUIContent.none);
		}
	}
}