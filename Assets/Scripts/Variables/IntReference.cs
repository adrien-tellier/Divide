using System;
using UnityEngine;


namespace Game
{
	[Serializable]
	public class IntReference
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private short				m_type = 0;

		[SerializeField]
		private int					m_value = 0;

		[SerializeField]
		private IntVariable			m_variable = null;

		[SerializeField]
		private int					m_constValue = 0;

		[SerializeField]
		private IntVariable			m_constVariable = null;







		//**************************************************************************
		//***** Properties *********************************************************

		public int Value
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
					return 0;
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

		public static implicit operator int(IntReference t_reference)
		{
			return t_reference.Value;
		}





		//**************************************************************************
		//***** Functions **********************************************************

		public void Clamp(int t_min, int t_max)
		{
			if (Value < t_min) Value = t_min;
			if (Value > t_max) Value = t_max;
		}
	}
}
