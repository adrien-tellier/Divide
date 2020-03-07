using UnityEngine;


namespace Game
{
	[CreateAssetMenu]
	public class FloatVariable : ScriptableObject
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private float		m_value = 0f;





		//**************************************************************************
		//***** Properties *********************************************************

		public float Value
		{
			get { return m_value; }
			set { m_value = value; }
		}





		//**************************************************************************
		//***** Operator ***********************************************************

		public static implicit operator float(FloatVariable t_variable)
		{
			return t_variable.Value;
		}
	}
}
