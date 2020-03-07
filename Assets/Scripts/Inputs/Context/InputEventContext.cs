using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class InputEventContext
    {
        //**************************************************************************
        //***** Properties *********************************************************

        public virtual T ReadValue<T>()
        {
            return default;
        }

        public virtual void SetValue<T>(T value)
        {
            return;
        }
    }
}