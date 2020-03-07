using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class CameraMenu : MonoBehaviour
	{
		//******************************************************************************
		//***** Fields *****************************************************************

		[Header("Initialization")]
		[SerializeField, Rename("Offset")]
		private Vector3 m_initialOffset = Vector3.zero;

		[Header("Settings")]
		[SerializeField, Rename("Target"), Required("Target required")]
		private GameObject m_targetObject = null;

		[SerializeField, Range(1f, 360f)]
		private float m_angularSpeed = 90f;

		private Transform m_transform = null;



		//******************************************************************************
		//***** Functions : Unity ******************************************************

		private void Start()
		{
			m_transform = transform;

			// Init camera angle/position
			m_transform.LookAt(m_targetObject.transform);
			m_transform.position = m_targetObject.transform.position + m_initialOffset;
		}


		private void FixedUpdate()
		{
			transform.RotateAround(m_targetObject.transform.position, new Vector3(0, 1, 0), m_angularSpeed * Time.fixedDeltaTime);
		}
	}
}