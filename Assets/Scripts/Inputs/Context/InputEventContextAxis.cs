using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class InputEventContextAxis : InputEventContext
    {
        //**************************************************************************
        //***** Fields *************************************************************

        private float m_axis;

        //**************************************************************************
        //***** Properties *********************************************************

        public override T ReadValue<T>()
        {
            return (T)Convert.ChangeType(m_axis, typeof(T));
        }

        public override void SetValue<T>(T value)
        {
            if (value is float)
                m_axis = (float)Convert.ChangeType(value, typeof(float));
        }
    }
}