using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(LineRenderer))]
	public class CloningRange : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		[Required("Inputs required")]
		private InputHandler m_inputs = null;

		[SerializeField]
		[Range(0, 50)]
		private int m_segments = 50;

		[SerializeField]
		[Range(0, 5)]
		private float m_radius = 5;

		private LineRenderer m_line = null;

		private bool m_joystickMove = false;

		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void Start()
		{
			m_line = gameObject.GetComponent<LineRenderer>();

			m_line.positionCount = m_segments + 1;
			m_line.useWorldSpace = false;

			InitEvent(ref m_inputs.Inputs);
		}

		private void Update()
		{
			if (m_joystickMove)
			{
				m_joystickMove = false;

				DrawCircle();

				m_line.enabled = true;
			}
			else
				m_line.enabled = false;
		}

		//**************************************************************************
		//***** Functions **********************************************************

		private void InitEvent(ref List<InputEvent> inputs)
        {
            foreach (InputEvent ie in inputs)
            {
                switch (ie.Action)
                {
					case EInputEventAction.CloneDirectionJoystick: ie.m_event += OnCloneDirectionJoystick; break;
					default: break;
                }
            }
        }

		private void OnCloneDirectionJoystick(InputEventContext context)
		{
			if (context is InputEventContextVector2)
				m_joystickMove = true;
		}

		private void DrawCircle()
		{
			float x;
			float z;

			float angle = 20f;

			for (int i = 0; i <= m_segments; i++)
			{
				x = Mathf.Sin(Mathf.Deg2Rad * angle) * m_radius;
				z = Mathf.Cos(Mathf.Deg2Rad * angle) * m_radius;

				m_line.SetPosition(i, new Vector3(x, 0, z));

				angle += (360f / m_segments);
			}
		}
	}
}