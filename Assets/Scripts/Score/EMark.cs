using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class Mark : IComparable<Mark>
	{
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private string m_name = "";

		[SerializeField]
		private int m_scale = 0;

		//**************************************************************************
		//***** Properties *********************************************************

		public string Name
		{
			get { return m_name; }
		}

		//**************************************************************************
		//***** Functions **********************************************************

		public bool CheckMark(int alerts)
		{
			if (alerts <= m_scale)
				return true;
			return false;
		}

		public int CompareTo(Mark other)
		{
			return m_scale.CompareTo(other.m_scale);
		}
	}
}