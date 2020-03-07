using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	[RequireComponent(typeof(Image))]
	public class ActiveUnactive : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField] private Event m_active = null;
		[SerializeField] private Event m_unactive = null;
		[SerializeField] private bool m_isActiveAtStart = false;

		private Image m_image = null;






		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void Awake()
		{
			m_image = GetComponent<Image>();
		}


		private void Start()
		{
			m_active?.Subscribe(Active);
			m_unactive?.Subscribe(Unactive);
			if (m_image != null)
				m_image.enabled = m_isActiveAtStart;
		}





		//**************************************************************************
		//***** Functions **********************************************************

		private void Active()
		{
			if (m_image != null)
				m_image.enabled = true;
		}


		private void Unactive()
		{
			if (m_image != null)
				m_image.enabled = false;
		}
	}
}