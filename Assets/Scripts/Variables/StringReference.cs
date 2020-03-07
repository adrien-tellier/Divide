using System;
using UnityEngine;


namespace Game
{
	[Serializable]
	public class StringReference
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private short				m_type = 0;

		[SerializeField]
		private string				m_value = "";

		[SerializeField]
		private StringVariable		m_variable = null;

		[SerializeField]
		private string				m_constValue = "";

		[SerializeField]
		private StringVariable		m_constVariable = null;







		//**************************************************************************
		//***** Properties *********************************************************

		public string Value
		{
			get
			{
				if (m_type == 0)
					return m_value;
				else if (m_type == 1 && m_variable != null)
					return m_variable;
				else if (m_type == 2)
					return m_constValue;
				else if (m_type == 3 && m_constVariable != null)
					return m_constVariable;
				else
					return "";
			}

			set
			{
				if (m_type == 0)
					m_value = value;
				else if (m_type == 1 && m_variable != null)
					m_variable.Value = value;
			}
		}





		//**************************************************************************
		//***** Operator ***********************************************************

		public static implicit operator string(StringReference t_reference)
		{
			return t_reference.Value;
		}
	}
}
