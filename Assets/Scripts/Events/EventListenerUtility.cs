using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListenerUtility : MonoBehaviour
{
	//**************************************************************************
	//***** Fields *************************************************************

	private Animation		m_animation = null;
	private AudioSource		m_audioSource = null;





	//**************************************************************************
	//***** Functions : Utility ************************************************

	public void PlayAnimation(AnimationClip t_clip)
	{
		if (m_animation == null)
			m_animation = GetComponent<Animation>();

		m_animation?.Play(t_clip.name);
	}


	public void PlaySound()
	{
		if (m_audioSource == null)
			m_audioSource = GetComponent<AudioSource>();

		m_audioSource.Play(0);
	}

	public void TurnOnLight(Light light)
	{
		if (light != null)
			light.enabled = true;
	}

	public void TurnOffLight(Light light)
	{
		if (light != null)
			light.enabled = false;
	}
}