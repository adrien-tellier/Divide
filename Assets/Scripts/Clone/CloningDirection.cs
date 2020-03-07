using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	[RequireComponent(typeof(LineRenderer))]
	public class CloningDirection : MonoBehaviour
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		[Required("Inputs required")]
		private InputHandler m_inputs = null;

		[SerializeField]
		[Required("Camera required")]
		private Camera m_camera = null;

		[SerializeField]
		[Required("Clone required")]
		private GameObject m_cloneObj = null;

		[SerializeField]
		[Range(0, 5)]
		private float m_radius = 5;

		[SerializeField]
		private Canvas m_canvas = null;

		private LineRenderer m_line = null;
		private List<Material> m_materials = null;

		private Vector2 m_joystickOffSet = Vector2.zero;

		private Vector3 m_direction = Vector3.zero;
		private Vector3 m_cloneSpawn = Vector3.zero;
		
		private bool m_joystickMove = false;

		private Clone m_clone = null;
		private Scrollbar m_scrollbar = null;

		public Vector3 CloneSpawn
		{
			get{ return m_cloneSpawn; }
		}

		//**************************************************************************
		//***** Functions : Unity **************************************************

		private void Start()
		{
			m_line = gameObject.GetComponent<LineRenderer>();
			m_line.useWorldSpace = true;

			// we need 2 points for draw the direction;
			m_line.positionCount = 2;

			// save materials
			m_materials = new List<Material>();
			m_materials.Add(m_line.materials[0]);
			m_materials.Add(m_line.materials[1]);

			// Get scrollbar
			m_clone = m_cloneObj.GetComponent<Clone>();
			m_scrollbar = m_canvas.GetComponentInChildren<Scrollbar>();

			InitEvent(ref m_inputs.Inputs);
		}

		private void Update()
		{
			if (m_joystickMove)
			{
				ConvertJoystickMovement();
				m_joystickMove = false;

				DrawDirectionAxis();

				m_line.enabled = true;
			}
			else
				m_line.enabled = false;

			// Freeze canvas rotation
			m_canvas.transform.eulerAngles = Vector3.zero;

			// Update cool down bar
			m_scrollbar.size = m_clone.CoolDownPercent;
			m_scrollbar.gameObject.SetActive(0 < m_clone.CoolDownPercent && m_clone.CoolDownPercent < 1);
		}

		//**************************************************************************
		//***** Functions **********************************************************

		private void InitEvent(ref List<InputEvent> inputs)
        {
            foreach (InputEvent ie in inputs)
            {
                switch (ie.Action)
                {
					case EInputEventAction.CloneDirectionJoystick:	ie.m_event += OnCloneDirectionJoystick; break;
					default:																				break;
                }
            }
        }

		private void OnCloneDirectionJoystick(InputEventContext context)
		{
			if (context is InputEventContextVector2)
			{
				m_joystickOffSet = context.ReadValue<Vector2>();
				m_joystickMove = true; 
			}
		}

		private void ConvertJoystickMovement()
		{
			m_direction.x = m_joystickOffSet.x;
			m_direction.z = m_joystickOffSet.y;
		}

		private void DrawDirectionAxis()
		{
			// Set color line
			if (m_cloneObj.GetComponent<Clone>().CoolDownRecovered)
				m_line.material = m_materials[0];
			else
				m_line.material = m_materials[1];

			// Origin position (player)
			m_line.SetPosition(0, transform.position);

			// Mouse position (already convert)
			m_direction.y = 0f;
			m_cloneSpawn = (m_direction.normalized * m_radius) + transform.position;

			m_line.SetPosition(1, m_cloneSpawn);
		}
	}
}