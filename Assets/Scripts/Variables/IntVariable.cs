using UnityEngine;


namespace Game
{
	[CreateAssetMenu]
	public class IntVariable : ScriptableObject
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private int			m_value = 0;





		//**************************************************************************
		//***** Properties *********************************************************

		public int Value
		{
			get { return m_value; }
			set { m_value = value; }
		}





		//**************************************************************************
		//***** Operator ***********************************************************

		public static implicit operator int(IntVariable t_variable)
		{
			return t_variable.Value;
		}
	}
}
