using UnityEngine;


namespace Game
{
	public class CameraFollowTarget : MonoBehaviour
	{
		//******************************************************************************
		//***** Fields *****************************************************************

		// Serialized
		[Header("Initialization")]
		[SerializeField] private Vector3					m_initialOffset = Vector3.zero;
		[SerializeField, Range(0.01f, 1f)] private float	m_startSmoothSpeed = 0.05f;
		[SerializeField, Range(0f, 60f)] private float		m_startTimer = 5f;

		[Header("Play")]
		[SerializeField, Required] private GameObject		m_targetObject = null;
		[SerializeField] private Vector3					m_offset = Vector3.zero;
		[SerializeField, Range(0.01f, 1f)] private float	m_smoothSpeed = 0f;

		[Header("End")]
		[SerializeField] private Vector3					m_endOffset = Vector3.zero;
		[SerializeField, Range(0.01f, 1f)] private float	m_endSmoothSpeed = 0f;

		[Header("Events")]
		[SerializeField] private Event						m_playerDeathEvent = null;
		[SerializeField] private Event						m_endLevelEvent = null;

		// Hide
		private Transform	m_transform = null;
		private Transform	m_target = null;
		private float		m_time = 0f;
		private bool		m_endGame = false;
		private bool		m_endLevelRaised = false;





		//******************************************************************************
		//***** Functions : Unity ******************************************************

		private void Start()
		{
			// Get fields
			m_target = GameObject.Find(m_targetObject.name).transform;
			m_transform = transform;

			// Init camera angle/position
			m_transform.position = m_target.position + m_offset;
			m_transform.LookAt(m_target);
			m_transform.position = m_target.position + m_initialOffset;

			// Subscribe to death event
			m_playerDeathEvent.Subscribe(PlayerDeathCamera);
		}


		private void FixedUpdate()
		{
			if (!m_endGame)
			{
				// Vars
				Vector3 pos = m_transform.position;
				Vector3 dest = m_target.position + m_offset;
				float smoothSpeed = m_time > m_startTimer ? m_smoothSpeed : m_startSmoothSpeed;

				// Lerp to target pos
				m_transform.position = Vector3.Lerp(pos, dest, smoothSpeed);
			}
		}


		private void Update()
		{
			// update time
			if (m_time < m_startTimer)
				m_time += Time.deltaTime;

			// lerp at end level event
			if (m_endGame)
			{
				Vector3 pos = m_transform.position;
				Vector3 dest = m_target.position + m_endOffset;

				// Lerp to target pos
				m_transform.position = Vector3.Lerp(pos, dest,
					m_endSmoothSpeed * Time.unscaledDeltaTime * 50);

				// Check if arrived at dest
				if (Vector3.SqrMagnitude(dest - pos) < 0.5 && !m_endLevelRaised)
				{
					m_endLevelEvent?.Raise();
					m_endLevelRaised = true;
				}
			}
		}





		//******************************************************************************
		//***** Functions : Events *****************************************************

		private void PlayerDeathCamera()
		{
			// Pass to camera end behaviours
			m_endGame = true;
		}
	}
}