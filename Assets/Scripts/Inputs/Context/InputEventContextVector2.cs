using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class InputEventContextVector2 : InputEventContext
    {
        //**************************************************************************
        //***** Fields *************************************************************

        private Vector2 m_vector;

        //**************************************************************************
        //***** Properties *********************************************************

        public override T ReadValue<T>()
        {
            return (T)Convert.ChangeType(m_vector, typeof(T));
        }
        
        public override void SetValue<T>(T value)
        {;
            if (value is Vector2)
                m_vector = (Vector2)Convert.ChangeType(value, typeof(Vector2));
        }
    }
}