using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
	public class MyAudioMixer : MonoBehaviour
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		[SerializeField]
		private AudioMixer m_audioMixer = null;


		//********************************************************************************
		//***** Properties ***************************************************************

		public float GetMasterVolume()
		{
			m_audioMixer.GetFloat("MasterVolume", out float volume);
			return volume;
		}

		public float GetMusicVolume()
		{
			m_audioMixer.GetFloat("MusicVolume", out float volume);
			return volume;
		}

		public float GetSFXVolume()
		{
			m_audioMixer.GetFloat("SFXVolume", out float volume);
			return volume;
		}

		//********************************************************************************
		//***** Functions ****************************************************************

		public void SetMasterVolume(float volume)
		{
			if (volume <= -20)
				volume = -80;

			m_audioMixer.SetFloat("MasterVolume", volume);
		}

		public void SetMusicVolume(float volume)
		{
			if (volume <= -20)
				volume = -80;

			m_audioMixer.SetFloat("MusicVolume", volume);
		}

		public void SetSFXVolume(float volume)
		{
			if (volume <= -20)
				volume = -80;

			m_audioMixer.SetFloat("SFXVolume", volume);
		}
	}
}