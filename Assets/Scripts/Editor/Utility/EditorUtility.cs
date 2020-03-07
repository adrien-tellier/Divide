using UnityEditor;
using UnityEngine;


namespace Game
{
	public class EditorUtility
	{
		//**************************************************************************
		//***** Functions : List ***************************************************

		public static void DrawArray(Rect t_rect, SerializedProperty t_array, GUIContent t_label)
		{
			//label field
			Rect labelRect = new Rect(t_rect.x, t_rect.y, t_rect.width, EditorGUIUtility.singleLineHeight);
			EditorGUI.LabelField(labelRect, t_label, EditorStyles.boldLabel);

			//elements fields
			Rect rect = new Rect(t_rect.x, t_rect.y, t_rect.width - 75, 15);
			t_array.isExpanded = true;
			for (int i = 0; i < t_array.arraySize; ++i)
			{
				//property field
				rect.y += rect.height;
				EditorGUI.PropertyField(rect, t_array.GetArrayElementAtIndex(i), GUIContent.none, true);

				//move button
				Rect buttonRect = rect;
				buttonRect.width = 20;
				buttonRect.x += rect.width + 10;
				if (GUI.Button(buttonRect, new GUIContent("\u21b4", "move down"), EditorStyles.miniButtonLeft))
					t_array.MoveArrayElement(i, i + 1);

				//add button
				Rect buttonRect2 = buttonRect;
				buttonRect2.position += new Vector2(buttonRect.width, 0);
				if (GUI.Button(buttonRect2, new GUIContent("+", "add element"), EditorStyles.miniButtonMid))
					t_array.InsertArrayElementAtIndex(i);

				//reomve button
				Rect buttonRect3 = buttonRect2;
				buttonRect3.position += new Vector2(buttonRect2.width, 0);
				if (GUI.Button(buttonRect3, new GUIContent("-", "delete"), EditorStyles.miniButtonRight))
				{
					int oldSize = t_array.arraySize;
					t_array.DeleteArrayElementAtIndex(i);
					if (t_array.arraySize == oldSize)
					{
						t_array.DeleteArrayElementAtIndex(i);
					}
				}
			}

			//add button
			if (t_array.arraySize == 0)
			{
				Rect buttonRect = new Rect(t_rect.x, t_rect.y + 15, t_rect.width, 15);
				if (GUI.Button(buttonRect, new GUIContent("+", "add element")))
					t_array.InsertArrayElementAtIndex(0);
			}
		}

		public static float ArrayHeight(SerializedProperty t_array)
		{
			int size = t_array.arraySize;
			return EditorGUIUtility.singleLineHeight + 17 * (size == 0 ? 1 : size);
		}
	}
}