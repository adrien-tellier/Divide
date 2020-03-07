using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


namespace Game
{
	[Serializable]
	public class Data
	{
		//********************************************************************************
		//***** Properties ***************************************************************

		// Transform
		public Transform		Transform { get; set; }
		public Transform		Target { get; set; }

		// Component
		public NavMeshAgent		NavMeshAgent { get; set; }
		public SphereCollider	SphereCollider { get; set; }
		public MeshCollider		MeshCollider { get; set; }
		public MeshFilter		MeshFilter { get; set; }
		public MeshRenderer		MeshRenderer { get; set; }
		public Canvas			Canvas { get; set; }
		public Scrollbar		DetectionBar { get; set; }

		// Vector3
		public Vector3			InitialPosition { get; set; }
		public Vector3			TargetLastPosition { get; set; }
		public Vector3			PatrolLastPosition { get; set; }
		public List<Vector3>	Patrol { get; set; }

		// Quaternions
		public Quaternion		InitialRotation { get; set; }

		// Int
		public int				Patrol_Next { get; set; }

		// Float
		public float			LookAround_CurrentRotation { get; set; }
		public float			IsDetected_Time { get; set; }
		public float			IsDetected_PrevTime { get; set; }
		public float			Timer_Time { get; set; }

		// Bool
		public bool				IsDetected { get; set; }
		public bool				IsDetectedOnce { get; set; }
		public bool				IsDetectedOnce_EventRaise { get; set; }
		public bool				IsDetectedOnce_DeathEventRaise { get; set; }
		public bool				IsDetected_WasDisplay { get; set; }
		public bool				Timer_IsTimeElapsed { get; set; }












		//********************************************************************************
		//***** Constructor **************************************************************

		public Data(Transform t_tranform)
		{
			// Transforms
			Transform = t_tranform;
			Target = null;

			// Components
			NavMeshAgent = t_tranform.GetComponent<NavMeshAgent>();
			SphereCollider = t_tranform.GetComponent<SphereCollider>();
			MeshCollider = t_tranform.GetComponent<MeshCollider>();
			MeshFilter = t_tranform.GetComponent<MeshFilter>();
			MeshRenderer = t_tranform.GetComponent<MeshRenderer>();
			Canvas = t_tranform.GetComponentInChildren<Canvas>();
			DetectionBar = t_tranform.GetComponentInChildren<Scrollbar>();

			// Vector3
			InitialPosition = t_tranform.position;
			TargetLastPosition = InitialPosition;
			PatrolLastPosition = InitialPosition;
			Patrol = GetPatrol();

			// Quaternion
			InitialRotation = t_tranform.rotation;

			// Int
			Patrol_Next = 0;

			// Float
			LookAround_CurrentRotation = 0f;
			IsDetected_Time = 0f;
			IsDetected_PrevTime = 0f;
			Timer_Time = 0f;

			// Bool
			IsDetected = false;
			IsDetectedOnce = false;
			IsDetectedOnce_EventRaise = false;
			IsDetectedOnce_DeathEventRaise = false;
			IsDetected_WasDisplay = true;
			Timer_IsTimeElapsed = false;
		}










		//********************************************************************************
		//***** Functions : Constructor **************************************************

		private List<Vector3> GetPatrol()
		{
			// Vars
			Transform patrol = Transform.Find("Patrol");
			List<Vector3> positions = new List<Vector3>();

			// Get patrol positions
			if (patrol != null)
				foreach (Transform child in patrol)
					positions.Add(child.position);

			return positions;
		}
	}
}