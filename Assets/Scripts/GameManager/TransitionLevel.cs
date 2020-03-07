using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


namespace Game
{
	public class TransitionLevel : MonoBehaviour
	{
		[Required("Load Level animation required")]
		[SerializeField]
		private Animator m_loadLevelAnim = null;

		[Required("Try Again animation required")]
		[SerializeField]
		private Animator m_tryAgainAnim = null;

		[Required("Respawn animation required")]
		[SerializeField]
		private Animator m_respawnAnim = null;

		[Required("Return Menu animation required")]
		[SerializeField]
		private Animator m_returnMenuAnim = null;

		[Required("Quit Game animation required")]
		[SerializeField]
		private Animator m_quitGameAnim = null;

		private int m_nbScene = 1;
		private int m_currentScene = 0;

		public void RespawnLevel()
		{
			LoadLevel(m_currentScene, m_respawnAnim);
		}

		public void TryAgainLevel()
		{
			LoadLevel(m_currentScene, m_tryAgainAnim);
		}

		public void LoadNextLevel()
		{
			if (m_currentScene != m_nbScene - 1)
				LoadLevel(m_currentScene + 1, m_loadLevelAnim);
			else
				ReturnMenu();
		}

		public void LoadLevelSelection(int level)
		{
			LoadLevel(level, m_loadLevelAnim);
		}

		private void LoadLevel(int level, Animator anim)
		{
			StartCoroutine(LoadSceneAnim(level, anim));
		}

		public void ReturnMenu()
		{
			StartCoroutine(LoadSceneAnim(0, m_returnMenuAnim));
		}

		public void QuitGame()
		{
			StartCoroutine(QuitAnim(m_quitGameAnim));
		}

		private void LoadScene(int level)
		{
			SceneManager.LoadSceneAsync(level);
		}

		private IEnumerator LoadSceneAnim(int level, Animator anim)
		{
			anim.SetTrigger("Fade");
			yield return new WaitForSecondsRealtime(1);

			while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
				yield return null;

			// Reset the time
			Time.timeScale = 1;

			LoadScene(level);
		}

		private IEnumerator QuitAnim(Animator anim)
		{
			anim.SetTrigger("Fade");
			yield return new WaitForSeconds(1);

			while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
				yield return null;

			Quit();
		}

		private void Quit()
		{
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}

		private void Start()
		{
			m_currentScene = SceneManager.GetActiveScene().buildIndex;
			m_nbScene = SceneManager.sceneCountInBuildSettings;
		}
	}
}
