namespace UnityEngine.InputSystem.UI
{
	internal class ExtendedAxisEventData : global::UnityEngine.EventSystems.AxisEventData, global::UnityEngine.InputSystem.UI.INavigationEventData
	{
		public global::UnityEngine.InputSystem.InputDevice device { get; set; }

		public ExtendedAxisEventData(global::UnityEngine.EventSystems.EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		public override string ToString()
		{
			return $"MoveDir: {base.moveDir}\nMoveVector: {base.moveVector}";
		}
	}
}
