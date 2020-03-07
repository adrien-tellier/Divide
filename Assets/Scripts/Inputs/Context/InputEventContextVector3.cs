using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class InputEventContextVector3 : InputEventContext
	{
		//**************************************************************************
		//***** Fields *************************************************************

		private Vector3 m_vector;

		//**************************************************************************
		//***** Properties *********************************************************

		public override T ReadValue<T>()
		{
			return (T)Convert.ChangeType(m_vector, typeof(T));
		}

		public override void SetValue<T>(T value)
		{
			if (value is Vector3)
				m_vector = (Vector3)Convert.ChangeType(value, typeof(Vector3));
		}
	}
}