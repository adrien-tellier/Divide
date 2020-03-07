using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


namespace Game
{
	[CreateAssetMenu(menuName = "Actions/Detection")]
	public class Detection : Action
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		[Space, SerializeField] private EventsLayer			m_lifeZoneEvents = null;
		[Space, SerializeField] private EventsLayer			m_viewZoneEvents = null;
		[Space, SerializeField] private EventsLayer			m_deathZoneEvents = null;

		[Header("Settings")]
		[SerializeField] private LayerMask					m_layer = 0;
		[SerializeField] private LayerMask					m_priorityLayer = 0;
		[SerializeField] private LayerMask					m_drawIgnoredLayer = 0;
		[SerializeField] private LayerMask					m_wallLayer = 0;
		[SerializeField, Range(0f, 10f)] private float		m_length = 5f;
		[SerializeField, Range(0f, 10f)] private float		m_deathZoneRadius = 2f;
		[SerializeField, Range(0f, 10f)] private float		m_LifeZoneRadius = 2f;
		[SerializeField, Range(0f, 360f)] private float		m_angle = 60f;
		[SerializeField, Range(0f, 5f)] private float		m_height = 1f;
		[SerializeField] private Vector3					m_offset = Vector3.zero;
		[SerializeField] private bool						m_debug = false;

		[Header("Draw")]
		[SerializeField] private bool						m_isDraw = true;
		[SerializeField, Range(0f, 5f)] private float		m_drawHeight = 1.5f;










		//********************************************************************************
		//***** Functions : Override *****************************************************

		public override void Enter_Action()
		{
			// Init collider
			Mesh mesh = CreateColider();
			m_data.MeshCollider.sharedMesh = mesh;
			m_data.SphereCollider.radius = m_LifeZoneRadius;

			// Init renderer
			m_data.MeshRenderer.receiveShadows = false;
			m_data.MeshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			m_data.MeshRenderer.material.SetFloat("_Distance", m_deathZoneRadius);
		}


		public override void OnTriggerStay_Action(Collider t_other)
		{
			if (IsInView(t_other.transform))
			{
				if (t_other.transform != m_data.Target)
					NewTargetInViewZone(t_other.gameObject);
			}
			else
			{
				if (t_other.transform != m_data.Target)
					NewTargetInLifeZone(t_other.gameObject);
			}
		}


		public override void OnTriggerExit_Action(Collider t_other)
		{
			// Lost Target
			if (t_other.transform == m_data.Target)
				LostTarget();
		}


		public override void Update_Action()
		{
			// Lost Target
			if (m_data.Target != null && Vector3.Distance(m_data.Target.position, m_data.Transform.position) > m_length + 1)
				LostTarget();

			// Draw
			if (m_isDraw)
				DrawViewZone();

			// Test
			Transform tranform = m_data.Transform;
			Transform target = m_data.Target;
			if (target != null)
			{
				if (IsInView(target))
					DetectInViewZone();
				else if (Vector3.Distance(target.position, tranform.position) < m_LifeZoneRadius)
					DetectInLifeZone();
				RaiseEvents();
			}
		}


		public override void Exit_Action()
		{
			// Remove MeshCollider
			m_data.MeshCollider.sharedMesh = null;
		}










		//********************************************************************************
		//***** Functions : Detection ****************************************************

		private Mesh CreateColider()
		{
			// Vars
			Vector3 forward = Vector3.forward * m_length / Mathf.Cos(m_angle / 2 * Mathf.Deg2Rad);
			Quaternion left = Quaternion.Euler(0f, m_angle * 0.5f, 0f);
			Quaternion right = Quaternion.Euler(0f, -m_angle * 0.5f, 0f);
			Vector3 up = Vector3.up * m_height;

			// Create vertices
			Vector3[] vertices = new Vector3[6];
			vertices[0] = m_offset;
			vertices[1] = left * forward + m_offset;
			vertices[2] = right * forward + m_offset;
			vertices[3] = vertices[0] + up;
			vertices[4] = vertices[1] + up;
			vertices[5] = vertices[2] + up;

			// Create triangles
			int[] triangles =
			{
				0, 2, 1,	5, 4, 3,
				1, 0, 3,	1, 3, 4,
				2, 1, 4,	2, 4, 5,
				0, 2, 5,	0, 5, 3
			};

			// Create Mesh
			Mesh mesh = new Mesh();
			mesh.name = "ViewZone";
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.RecalculateNormals();

			return mesh;
		}





		//********************************************************************************
		//***** Functions : New Target ***************************************************

		private void NewTargetInViewZone(GameObject t_obj)
		{
			// Vars
			bool haveTarget = m_data.Target != null;
			bool isTarget = Utility.IsInLayer(t_obj, m_layer);
			bool TargetisPriority = Utility.IsInLayer(t_obj, m_priorityLayer);

			// New target test
			if ((haveTarget && TargetisPriority) || (!haveTarget && isTarget))
				if (DetectTarget(t_obj.transform))
					NewTarget(t_obj);
		}


		private void NewTargetInLifeZone(GameObject t_obj)
		{
			// Vars
			bool haveTarget = m_data.Target != null;
			bool isTarget = Utility.IsInLayer(t_obj, m_layer);
			bool isSneak = GetMoveState(t_obj.transform) == EMovementState.Sneak;

			// New target test
			if ((!haveTarget && isTarget && !isSneak))
			{
				NewTarget(t_obj);
			}
		}


		private void NewTarget(GameObject t_obj)
		{
			m_data.Target = t_obj.transform;
			m_data.IsDetected = false;
			m_data.IsDetectedOnce = false;
			m_data.IsDetectedOnce_EventRaise = false;
		}




		//********************************************************************************
		//***** Functions : Detection ****************************************************

		private void DetectInViewZone()
		{
			if (DetectTarget(m_data.Target))
			{
				switch (GetMoveState(m_data.Target))
				{
					case EMovementState.Sneak:
						m_data.IsDetected = true; break;
					case EMovementState.Walk:
					case EMovementState.Run:
						m_data.IsDetected = true;
						m_data.IsDetected_Time += 100f;
						break;
				}
				SaveTargetLastPosition();
			}
			else
				LostTarget();
		}


		private void DetectInLifeZone()
		{
			switch (GetMoveState(m_data.Target))
			{
				case EMovementState.Sneak:
					LostTarget(); break;
				case EMovementState.Walk:
					m_data.IsDetected = true; break;
				case EMovementState.Run:
					m_data.IsDetected = true;
					m_data.IsDetected_Time += 100f;
					break;
			}
			SaveTargetLastPosition();
		}


		private void LostTarget()
		{
			m_data.Target = null;
			m_data.IsDetected = false;
			m_data.IsDetectedOnce = false;
			m_data.IsDetectedOnce_EventRaise = false;
		}


		private void SaveTargetLastPosition()
		{
			if (m_data.Target != null && m_data.IsDetected)
				m_data.TargetLastPosition = m_data.Target.position;
		}





		//********************************************************************************
		//***** Functions : Detection ****************************************************

		private bool DetectTarget(Transform t_target)
		{
            // No target case
            if (t_target == null)
                return false;

			// Raycast target
			if (RaycastTarget(t_target))
			{
				return true;
			}

			// Raycast target children
			foreach (Transform child in t_target)
				if (RaycastTarget(child))
				{
					return true;
				}

			return false;
		}


		private bool RaycastTarget(Transform t_target)
		{
			// Vars
			Vector3 pos = m_data.Transform.position;
			Vector3 dir = t_target.position - pos;

			// Raycast
			if (Physics.Raycast(pos, dir, out RaycastHit hit, dir.magnitude, m_wallLayer))
			{
				// Debug line
				if (m_debug)
					Debug.DrawLine(pos, hit.point);
				return false;
			}

			return true;
		}


		private bool IsInView(Transform t_target)
		{
			// Vars
			Vector3 targetDir = t_target.position - m_data.Transform.position;
			targetDir.y = 0;
			float angle = Vector3.Angle(m_data.Transform.forward, targetDir);

			// Detect if in view zone
			return Mathf.Abs(angle) < m_angle / 2f;
		}










		//********************************************************************************
		//***** Functions : Draw *********************************************************

		private void DrawViewZone()
		{
			// Vars
			List<Vector3> vertices = new List<Vector3>();
			int step = Mathf.RoundToInt(m_angle);
			float stepSize = m_angle / step;
			Vector3 offset = new Vector3(0f, m_drawHeight, 0f);
			Mesh mesh = m_data.MeshFilter.mesh;

			// Calcul direction for Raycast
			for (int i = 0; i <= step; ++i)
			{
				float angle = m_data.Transform.eulerAngles.y - m_angle / 2 + stepSize * i;
				Vector3 dir = Utility.DirFromAngle(m_data.Transform, angle, false);
				vertices.Add(RaycastDir(dir, stepSize * i - m_angle / 2));
			}

			// Update Mesh
			Utility.LocalVertices(ref vertices, m_data.Transform, offset);
			Utility.CustomMesh(ref mesh, vertices);
		}


		private Vector3 RaycastDir(Vector3 t_dir, float t_angle)
		{
			// Vars
			RaycastHit hit;
			Vector3 pos = m_data.Transform.position + new Vector3(0f, m_drawHeight, 0f);
			float length = m_length / Mathf.Cos(t_angle * Mathf.Deg2Rad);

			// Raycast from position to direction in radius
			if (Physics.Raycast(pos, t_dir, out hit, length))
			{
				if (Utility.IsInLayer(hit.transform.gameObject, m_drawIgnoredLayer))
					return pos + t_dir * length;
				return hit.point;
			}
			else
				return pos + t_dir * length;

		}










		//********************************************************************************
		//***** Functions : Events *******************************************************

		private void RaiseEvents()
		{
			if (m_data.Target == null)
				return;
			// Vars
			Vector3 pos = m_data.Target.position - m_data.Transform.position;
			bool canRaise = m_data.IsDetectedOnce && !m_data.IsDetectedOnce_EventRaise;

			// Raise events
			if (IsInView(m_data.Target))
			{
                // death zone event
                if (Utility.CompareMagnitude(pos, m_deathZoneRadius) && DetectTarget(m_data.Target) &&
					!m_data.IsDetectedOnce_DeathEventRaise)
				{
					m_deathZoneEvents?.Raise(m_data.Target.gameObject);
					if (Utility.IsInLayer(m_data.Target.gameObject, m_priorityLayer))
						m_data.IsDetectedOnce_DeathEventRaise = true;
				}
				else if (canRaise && m_data.IsDetected)
				{
					m_viewZoneEvents?.Raise(m_data.Target.gameObject);
					m_data.IsDetectedOnce_EventRaise = true;
				}
			}
			else if (canRaise)
			{
				m_lifeZoneEvents?.Raise(m_data.Target.gameObject);
				m_data.IsDetectedOnce_EventRaise = true;
			}
		}










		//********************************************************************************
		//***** Functions ****************************************************************

		private EMovementState GetMoveState(Transform t_target)
		{
			MovementHandler m_moveHandler = t_target.GetComponentInParent<MovementHandler>();
			return m_moveHandler != null ? m_moveHandler.MoveState : EMovementState.Run;
		}
	}
}
