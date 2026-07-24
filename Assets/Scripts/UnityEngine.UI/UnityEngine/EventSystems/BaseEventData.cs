namespace UnityEngine.EventSystems
{
	public class BaseEventData : global::UnityEngine.EventSystems.AbstractEventData
	{
		private readonly global::UnityEngine.EventSystems.EventSystem m_EventSystem;

		public global::UnityEngine.EventSystems.BaseInputModule currentInputModule => m_EventSystem.currentInputModule;

		public global::UnityEngine.GameObject selectedObject
		{
			get
			{
				return m_EventSystem.currentSelectedGameObject;
			}
			set
			{
				m_EventSystem.SetSelectedGameObject(value, this);
			}
		}

		public BaseEventData(global::UnityEngine.EventSystems.EventSystem eventSystem)
		{
			m_EventSystem = eventSystem;
		}
	}
}
