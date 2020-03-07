using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class VolumeSlider : MonoBehaviour
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		[SerializeField]
		private MyAudioMixer m_myAudioMixer = null;

		[SerializeField]
		private EVolumeSliderType m_type = EVolumeSliderType.None;

		private Slider m_slider = null;

		//********************************************************************************
		//***** Functions : Unity ********************************************************

		private void Awake()
		{
			m_slider = GetComponent<Slider>();
		}

		// Start is called before the first frame update
		void Start()
		{
			float volume = 0;
			switch (m_type)
			{
				case EVolumeSliderType.Master:	volume = m_myAudioMixer.GetMasterVolume();	break;
				case EVolumeSliderType.Music:	volume = m_myAudioMixer.GetMusicVolume();	break;
				case EVolumeSliderType.SFX:		volume = m_myAudioMixer.GetSFXVolume();		break;
				default:																	break;
			}

			m_slider.value = volume;
		}
	}
}