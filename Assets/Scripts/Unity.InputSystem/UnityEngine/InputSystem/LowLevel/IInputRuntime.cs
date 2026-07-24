namespace UnityEngine.InputSystem.LowLevel
{
	internal interface IInputRuntime
	{
		global::UnityEngine.InputSystem.LowLevel.InputUpdateDelegate onUpdate { get; set; }

		global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputUpdateType> onBeforeUpdate { get; set; }

		global::System.Func<global::UnityEngine.InputSystem.LowLevel.InputUpdateType, bool> onShouldRunUpdate { get; set; }

		global::System.Action<int, string> onDeviceDiscovered { get; set; }

		global::System.Action<bool> onPlayerFocusChanged { get; set; }

		bool isPlayerFocused { get; }

		global::System.Action onShutdown { get; set; }

		float pollingFrequency { get; set; }

		double currentTime { get; }

		double currentTimeForFixedUpdate { get; }

		float unscaledGameTime { get; }

		double currentTimeOffsetToRealtimeSinceStartup { get; }

		bool runInBackground { get; set; }

		global::UnityEngine.Vector2 screenSize { get; }

		global::UnityEngine.ScreenOrientation screenOrientation { get; }

		bool normalizeScrollWheelDelta { get; set; }

		float scrollWheelDeltaPerTick { get; }

		int AllocateDeviceId();

		void Update(global::UnityEngine.InputSystem.LowLevel.InputUpdateType type);

		unsafe void QueueEvent(global::UnityEngine.InputSystem.LowLevel.InputEvent* ptr);

		unsafe long DeviceCommand(int deviceId, global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand* commandPtr);
	}
}
