namespace UnityEngine.InputSystem.UI
{
	internal class ExtendedSubmitCancelEventData : global::UnityEngine.EventSystems.BaseEventData, global::UnityEngine.InputSystem.UI.INavigationEventData
	{
		public global::UnityEngine.InputSystem.InputDevice device { get; set; }

		public ExtendedSubmitCancelEventData(global::UnityEngine.EventSystems.EventSystem eventSystem)
			: base(eventSystem)
		{
		}
	}
}
