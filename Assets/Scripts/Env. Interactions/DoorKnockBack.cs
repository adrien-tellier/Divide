using UnityEngine;


namespace Game
{
	public class DoorKnockBack : MonoBehaviour
	{
		//********************************************************************************
		//***** Fields *******************************************************************

		// Serialized
		[SerializeField] private LayerMask m_layer = 0;
		[SerializeField] private Vector3 m_knockback = Vector3.zero;





		//********************************************************************************
		//***** Functions : Unity ********************************************************

		private void OnTriggerEnter(Collider t_other)
		{
			// Knock back collide object if in layer to const dest
			if (Utility.IsInLayer(t_other.gameObject, m_layer))
			{
				Vector3 dest = transform.position + m_knockback;
				dest.y = t_other.transform.parent.position.y;
				t_other.transform.parent.position = dest;
			}
		}
	}
}