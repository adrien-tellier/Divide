using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Game
{
	[RequireComponent(typeof(Button))]
	public class ButtonSetter : MonoBehaviour
	{
		//******************************************************************************
		//***** Fields *****************************************************************

		[SerializeField]
		private EButtonType		m_type = EButtonType.None;

		[SerializeField]
		private Object			m_scene = null;


		private Button			m_button = null;





		//******************************************************************************
		//***** Functions : Unity ******************************************************

		private void Start()
		{
			m_button = GetComponent<Button>();

			MethodInfo action = GetType().GetMethod(m_type.ToString(),
				BindingFlags.NonPublic | BindingFlags.Instance);

			if (action != null)
				m_button.onClick.AddListener( () => action.Invoke(this, null) );
		}





		//******************************************************************************
		//***** Functions **************************************************************

		private void LoadScene()
		{
			if (m_scene != null)
				SceneManager.LoadScene(m_scene.name);
		}


		private void Exit()
		{
			Application.Quit();
		}
	}
}