using UnityEngine;


namespace Game
{
	public class RequiredAttribute : PropertyAttribute
	{
		//**************************************************************************
		//***** Fields *************************************************************
	
		public string m_message;





		//**************************************************************************
		//***** Constructors *******************************************************

		public RequiredAttribute()
		{
			this.m_message = "Required";
		}


		public RequiredAttribute(string t_message)
		{
			this.m_message = t_message;
		}
	}
}