namespace UnityEngine.EventSystems
{
	public class AxisEventData : global::UnityEngine.EventSystems.BaseEventData
	{
		public global::UnityEngine.Vector2 moveVector { get; set; }

		public global::UnityEngine.EventSystems.MoveDirection moveDir { get; set; }

		public AxisEventData(global::UnityEngine.EventSystems.EventSystem eventSystem)
			: base(eventSystem)
		{
			moveVector = global::UnityEngine.Vector2.zero;
			moveDir = global::UnityEngine.EventSystems.MoveDirection.None;
		}
	}
}
