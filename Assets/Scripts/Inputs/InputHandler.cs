using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class InputHandler : MonoBehaviour
    {
        //**************************************************************************
        //***** Fields *************************************************************

        [SerializeField]
		private List<InputEvent> m_inputs = new List<InputEvent>();

        //**************************************************************************
        //***** Properties *********************************************************

        public ref List<InputEvent> Inputs
        {
            get { return ref m_inputs; }
        }

        //**************************************************************************
        //***** Functions : Unity **************************************************

        // Start is called before the first frame update
        private void Awake()
        {
			foreach (InputEvent ie in m_inputs)
				ie.InitContext();
		}

		// Update is called once per frame
		private void Update()
        {
            foreach (InputEvent ie in m_inputs)
                ie.ProcessEvent();
        }
    }
}