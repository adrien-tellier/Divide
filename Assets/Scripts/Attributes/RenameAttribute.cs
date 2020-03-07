using UnityEngine;


namespace Game
{
	public class RenameAttribute : PropertyAttribute
	{
		//**************************************************************************
		//***** Fields *************************************************************

		public string	m_name;





		//**************************************************************************
		//***** Constructors *******************************************************

		public RenameAttribute(string t_name)
		{
			this.m_name = t_name;
		}
	}
}