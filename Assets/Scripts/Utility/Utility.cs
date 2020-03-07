using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
	public class Utility
	{
		//******************************************************************************
		//***** Functions **************************************************************

		public static bool IsInLayer(GameObject t_obj, LayerMask t_layer)
		{
            if (t_obj == null)
                return false;
			return (t_layer.value & 1 << t_obj.layer) > 0;
		}


		public static bool CompareMagnitude(Vector3 t_vec, float t_val, bool t_smallerThan = true)
		{
			if (t_smallerThan)
				return Vector3.SqrMagnitude(t_vec) < Mathf.Pow(t_val, 2f);
			return Vector3.SqrMagnitude(t_vec) >= Mathf.Pow(t_val, 2f);
		}










		//******************************************************************************
		//***** Functions : Custom Mesh ************************************************

		public static void CustomMesh(ref Mesh t_mesh, List<Vector3> t_vertices)
		{
			int len = t_vertices.Count;
			Vector3[] vertices = t_vertices.ToArray();
			int[] triangles = new int[(len - 2) * 3];

			for (int i = 0; i < len - 2; ++i)
			{
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}

			t_mesh.Clear();
			t_mesh.vertices = vertices;
			t_mesh.triangles = triangles;
			t_mesh.RecalculateNormals();
		}


		public static void LocalVertices(ref List<Vector3> t_vertices, Transform t_tranform)
		{
			for (int i = 0; i < t_vertices.Count; ++i)
				t_vertices[i] = t_tranform.InverseTransformPoint(t_vertices[i]);

			t_vertices.Insert(0, Vector3.zero);
		}


		public static void LocalVertices(ref List<Vector3> t_vertices, Transform t_tranform,
			Vector3 t_origin)
		{
			for (int i = 0; i < t_vertices.Count; ++i)
				t_vertices[i] = t_tranform.InverseTransformPoint(t_vertices[i]);

			t_vertices.Insert(0, t_origin);
		}


		public static Vector3 DirFromAngle(Transform t_transform, float t_angle,
			bool t_isGlobal)
		{
			if (t_isGlobal)
				t_angle += t_transform.eulerAngles.y;

			t_angle *= Mathf.Deg2Rad;
			return new Vector3(Mathf.Sin(t_angle), 0, Mathf.Cos(t_angle));
		}
	}
}