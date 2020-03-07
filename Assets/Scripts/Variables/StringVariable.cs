using UnityEngine;


namespace Game
{
	[CreateAssetMenu]
	public class StringVariable : ScriptableObject
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private string		m_value = "";





		//**************************************************************************
		//***** Properties *********************************************************

		public string Value
		{
			get { return m_value; }
			set { m_value = value; }
		}





		//**************************************************************************
		//***** Operator ***********************************************************

		public static implicit operator string(StringVariable t_variable)
		{
			return t_variable.Value;
		}
	}
}
