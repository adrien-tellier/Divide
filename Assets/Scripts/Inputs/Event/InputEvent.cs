using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[System.Serializable]
    public class InputEvent
    {
		//**************************************************************************
		//***** Fields *************************************************************

		[SerializeField]
		private EInputEventAction m_action = new EInputEventAction();

		[SerializeField]
		private EInputType m_inputType = new EInputType();

		[SerializeField]
		protected List<string> m_buttonNames = new List<string>();

		public delegate void m_eventHandler(InputEventContext context);
        public event m_eventHandler m_event;
		protected InputEventContext m_context;

        //**************************************************************************
        //***** Constructors *******************************************************

        public InputEvent()
        {
            m_action = EInputEventAction.None;
			m_inputType = EInputType.None;
            m_buttonNames = new List<string>();
		}

		public InputEvent(EInputEventAction action, EInputType inputType, string buttonName)
        {
            m_action = action;
			m_inputType = inputType;
            m_buttonNames = new List<string>    { buttonName };
		}

		public InputEvent(EInputEventAction action, EInputType inputType, string buttonName1, string buttonName2)
		{
			m_action = action;
			m_inputType = inputType;
			m_buttonNames = new List<string> { buttonName1, buttonName2 };
		}

		//**************************************************************************
		//***** Properties *********************************************************

		public EInputEventAction Action
        {
            get { return m_action; }
        }

        public List<string> ButtonNames
        {
            get { return m_buttonNames; }
        }

		public EInputType InputType
		{
			get { return m_inputType; }
		}

		//**************************************************************************
		//***** Functions **********************************************************

		public void InitContext()
		{
			if (m_inputType == EInputType.Axis)
				m_context = new InputEventContextAxis();
			if (m_inputType == EInputType.Vector)
				m_context = new InputEventContextVector2();
			if (m_inputType == EInputType.MousePos)
				m_context = new InputEventContextVector3();
			if (m_inputType == EInputType.Button || m_inputType == EInputType.ButtonDown || m_inputType == EInputType.ButtonUp)
				m_context = new InputEventContext();
		}

		public void RaiseEvent()
		{
            if(m_event != null)
                m_event.Invoke(m_context);
        }

        public void ProcessEvent()
		{
			switch (m_inputType)
			{
				case EInputType.Axis:			GetAxis();			break;
				case EInputType.Vector:			GetVector();		break;
				case EInputType.MousePos:		GetMousePos();		break;
				case EInputType.Button:			GetButton();		break;
				case EInputType.ButtonDown:		GetButtonDown();	break;
				case EInputType.ButtonUp:		GetButtonUp();		break;
				default:											break;
			}
		}

		private void GetAxis()
		{
			foreach (string buttonName in m_buttonNames)
			{
				float value = Input.GetAxisRaw(buttonName);

				if (value != m_context.ReadValue<float>())
				{
					m_context.SetValue<float>(value);
					RaiseEvent();
				}
			}
		}

		private void GetVector()
		{
			m_context.SetValue<Vector2>(new Vector2(Input.GetAxisRaw(m_buttonNames[0]), Input.GetAxisRaw(m_buttonNames[1])));

			if (m_context.ReadValue<Vector2>() != Vector2.zero)
				RaiseEvent();
		}

		private void GetMousePos()
		{
			Vector3 mousPos = Input.mousePosition;

			if (mousPos != m_context.ReadValue<Vector3>())
			{
				m_context.SetValue<Vector3>(mousPos);
				RaiseEvent();
			}
		}

		private void GetButton()
		{
			foreach (string buttonName in m_buttonNames)
			{
				if (Input.GetButton(buttonName))
					RaiseEvent();
			}
		}

		private void GetButtonDown()
		{
			foreach (string buttonName in m_buttonNames)
			{
				if (Input.GetButtonDown(buttonName))
					RaiseEvent();
			}
		}

		private void GetButtonUp()
		{
			foreach (string buttonName in m_buttonNames)
			{
				if (Input.GetButtonUp(buttonName))
					RaiseEvent();
			}
		}
    }
}