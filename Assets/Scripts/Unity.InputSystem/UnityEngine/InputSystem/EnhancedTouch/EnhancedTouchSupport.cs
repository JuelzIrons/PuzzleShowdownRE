namespace UnityEngine.InputSystem.EnhancedTouch
{
	public static class EnhancedTouchSupport
	{
		private static int s_Enabled;

		private static global::UnityEngine.InputSystem.InputSettings.UpdateMode s_UpdateMode;

		public static bool enabled => s_Enabled > 0;

		public static void Enable()
		{
			s_Enabled++;
			if (s_Enabled <= 1)
			{
				global::UnityEngine.InputSystem.InputSystem.onDeviceChange += OnDeviceChange;
				global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate += global::UnityEngine.InputSystem.EnhancedTouch.Touch.BeginUpdate;
				global::UnityEngine.InputSystem.InputSystem.onSettingsChange += OnSettingsChange;
				SetUpState();
			}
		}

		public static void Disable()
		{
			if (enabled)
			{
				s_Enabled--;
				if (s_Enabled <= 0)
				{
					global::UnityEngine.InputSystem.InputSystem.onDeviceChange -= OnDeviceChange;
					global::UnityEngine.InputSystem.InputSystem.onBeforeUpdate -= global::UnityEngine.InputSystem.EnhancedTouch.Touch.BeginUpdate;
					global::UnityEngine.InputSystem.InputSystem.onSettingsChange -= OnSettingsChange;
					TearDownState();
				}
			}
		}

		internal static void Reset()
		{
			global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.touchscreens = default(global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Touchscreen>);
			global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.playerState.Destroy();
			global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.playerState = default(global::UnityEngine.InputSystem.EnhancedTouch.Touch.FingerAndTouchState);
			s_Enabled = 0;
		}

		private static void SetUpState()
		{
			global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.playerState.updateMask = global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Dynamic | global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Fixed | global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Manual;
			s_UpdateMode = global::UnityEngine.InputSystem.InputSystem.settings.updateMode;
			foreach (global::UnityEngine.InputSystem.InputDevice device in global::UnityEngine.InputSystem.InputSystem.devices)
			{
				OnDeviceChange(device, global::UnityEngine.InputSystem.InputDeviceChange.Added);
			}
		}

		internal static void TearDownState()
		{
			foreach (global::UnityEngine.InputSystem.InputDevice device in global::UnityEngine.InputSystem.InputSystem.devices)
			{
				OnDeviceChange(device, global::UnityEngine.InputSystem.InputDeviceChange.Removed);
			}
			global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.playerState.Destroy();
			global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.playerState = default(global::UnityEngine.InputSystem.EnhancedTouch.Touch.FingerAndTouchState);
		}

		private static void OnDeviceChange(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputDeviceChange change)
		{
			switch (change)
			{
			case global::UnityEngine.InputSystem.InputDeviceChange.Added:
				if (device is global::UnityEngine.InputSystem.Touchscreen screen2)
				{
					global::UnityEngine.InputSystem.EnhancedTouch.Touch.AddTouchscreen(screen2);
				}
				break;
			case global::UnityEngine.InputSystem.InputDeviceChange.Removed:
				if (device is global::UnityEngine.InputSystem.Touchscreen screen)
				{
					global::UnityEngine.InputSystem.EnhancedTouch.Touch.RemoveTouchscreen(screen);
				}
				break;
			}
		}

		private static void OnSettingsChange()
		{
			global::UnityEngine.InputSystem.InputSettings.UpdateMode updateMode = global::UnityEngine.InputSystem.InputSystem.settings.updateMode;
			if (s_UpdateMode != updateMode)
			{
				TearDownState();
				SetUpState();
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal static void CheckEnabled()
		{
			if (!enabled)
			{
				throw new global::System.InvalidOperationException("EnhancedTouch API is not enabled; call EnhancedTouchSupport.Enable()");
			}
		}
	}
}
