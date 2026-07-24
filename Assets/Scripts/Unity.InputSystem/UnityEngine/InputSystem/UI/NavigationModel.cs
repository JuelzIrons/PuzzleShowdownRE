namespace UnityEngine.InputSystem.UI
{
	internal struct NavigationModel
	{
		public global::UnityEngine.Vector2 move;

		public int consecutiveMoveCount;

		public global::UnityEngine.EventSystems.MoveDirection lastMoveDirection;

		public float lastMoveTime;

		public global::UnityEngine.EventSystems.AxisEventData eventData;

		public global::UnityEngine.InputSystem.InputDevice device;

		public void Reset()
		{
			move = global::UnityEngine.Vector2.zero;
		}
	}
}
