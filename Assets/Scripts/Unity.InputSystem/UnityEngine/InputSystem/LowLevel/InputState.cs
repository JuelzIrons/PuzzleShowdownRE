namespace UnityEngine.InputSystem.LowLevel
{
	public static class InputState
	{
		private class StateChangeMonitorDelegate : global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor
		{
			public global::System.Action<global::UnityEngine.InputSystem.InputControl, double, global::UnityEngine.InputSystem.LowLevel.InputEventPtr, long> valueChangeCallback;

			public global::System.Action<global::UnityEngine.InputSystem.InputControl, double, long, int> timerExpiredCallback;

			public void NotifyControlStateChanged(global::UnityEngine.InputSystem.InputControl control, double time, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, long monitorIndex)
			{
				valueChangeCallback(control, time, eventPtr, monitorIndex);
			}

			public void NotifyTimerExpired(global::UnityEngine.InputSystem.InputControl control, double time, long monitorIndex, int timerIndex)
			{
				timerExpiredCallback?.Invoke(control, time, monitorIndex, timerIndex);
			}
		}

		public static global::UnityEngine.InputSystem.LowLevel.InputUpdateType currentUpdateType => global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_LatestUpdateType;

		public static uint updateCount => global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;

		public static double currentTime => global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime - global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;

		public static event global::System.Action<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.LowLevel.InputEventPtr> onChange
		{
			add
			{
				global::UnityEngine.InputSystem.InputSystem.s_Manager.onDeviceStateChange += value;
			}
			remove
			{
				global::UnityEngine.InputSystem.InputSystem.s_Manager.onDeviceStateChange -= value;
			}
		}

		public unsafe static void Change(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType = global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (!eventPtr.valid)
			{
				throw new global::System.ArgumentNullException("eventPtr");
			}
			global::UnityEngine.InputSystem.Utilities.FourCC type = eventPtr.type;
			global::UnityEngine.InputSystem.Utilities.FourCC stateFormat;
			if (type == 1398030676)
			{
				stateFormat = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr)->stateFormat;
			}
			else
			{
				if (!(type == 1145852993))
				{
					return;
				}
				stateFormat = global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent.FromUnchecked(eventPtr)->stateFormat;
			}
			if (stateFormat != device.stateBlock.format)
			{
				throw new global::System.ArgumentException($"State format {stateFormat} from event does not match state format {device.stateBlock.format} of device {device}", "eventPtr");
			}
			global::UnityEngine.InputSystem.InputSystem.s_Manager.UpdateState(device, eventPtr, (updateType != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None) ? updateType : global::UnityEngine.InputSystem.InputSystem.s_Manager.defaultUpdateType);
		}

		public static void Change<TState>(global::UnityEngine.InputSystem.InputControl control, TState state, global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType = global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr)) where TState : struct
		{
			Change(control, ref state, updateType, eventPtr);
		}

		public unsafe static void Change<TState>(global::UnityEngine.InputSystem.InputControl control, ref TState state, global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType = global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr)) where TState : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (control.stateBlock.bitOffset != 0 || control.stateBlock.sizeInBits % 8 != 0)
			{
				throw new global::System.ArgumentException($"Cannot change state of bitfield control '{control}' using this method", "control");
			}
			global::UnityEngine.InputSystem.InputDevice device = control.device;
			long num = global::System.Math.Min(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TState>(), control.m_StateBlock.alignedSizeInBytes);
			void* statePtr = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref state);
			uint stateOffsetInDevice = control.stateBlock.byteOffset - device.stateBlock.byteOffset;
			global::UnityEngine.InputSystem.InputSystem.s_Manager.UpdateState(device, (updateType != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None) ? updateType : global::UnityEngine.InputSystem.InputSystem.s_Manager.defaultUpdateType, statePtr, stateOffsetInDevice, (uint)num, eventPtr.valid ? eventPtr.internalTime : global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime, eventPtr);
		}

		public static bool IsIntegerFormat(this global::UnityEngine.InputSystem.Utilities.FourCC format)
		{
			if (!(format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit) && !(format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatInt) && !(format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatByte) && !(format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatShort) && !(format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatSBit) && !(format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatUInt) && !(format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatUShort) && !(format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatLong))
			{
				return format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatULong;
			}
			return true;
		}

		public static void AddChangeMonitor(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, long monitorIndex = -1L, uint groupIndex = 0u)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (monitor == null)
			{
				throw new global::System.ArgumentNullException("monitor");
			}
			if (!control.device.added)
			{
				throw new global::System.ArgumentException($"Device for control '{control}' has not been added to system");
			}
			global::UnityEngine.InputSystem.InputSystem.s_Manager.AddStateChangeMonitor(control, monitor, monitorIndex, groupIndex);
		}

		public static global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor AddChangeMonitor(global::UnityEngine.InputSystem.InputControl control, global::System.Action<global::UnityEngine.InputSystem.InputControl, double, global::UnityEngine.InputSystem.LowLevel.InputEventPtr, long> valueChangeCallback, int monitorIndex = -1, global::System.Action<global::UnityEngine.InputSystem.InputControl, double, long, int> timerExpiredCallback = null)
		{
			if (valueChangeCallback == null)
			{
				throw new global::System.ArgumentNullException("valueChangeCallback");
			}
			global::UnityEngine.InputSystem.LowLevel.InputState.StateChangeMonitorDelegate stateChangeMonitorDelegate = new global::UnityEngine.InputSystem.LowLevel.InputState.StateChangeMonitorDelegate
			{
				valueChangeCallback = valueChangeCallback,
				timerExpiredCallback = timerExpiredCallback
			};
			AddChangeMonitor(control, stateChangeMonitorDelegate, monitorIndex);
			return stateChangeMonitorDelegate;
		}

		public static void RemoveChangeMonitor(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, long monitorIndex = -1L)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (monitor == null)
			{
				throw new global::System.ArgumentNullException("monitor");
			}
			global::UnityEngine.InputSystem.InputSystem.s_Manager.RemoveStateChangeMonitor(control, monitor, monitorIndex);
		}

		public static void AddChangeMonitorTimeout(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, double time, long monitorIndex = -1L, int timerIndex = -1)
		{
			if (monitor == null)
			{
				throw new global::System.ArgumentNullException("monitor");
			}
			global::UnityEngine.InputSystem.InputSystem.s_Manager.AddStateChangeMonitorTimeout(control, monitor, time, monitorIndex, timerIndex);
		}

		public static void RemoveChangeMonitorTimeout(global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, long monitorIndex = -1L, int timerIndex = -1)
		{
			if (monitor == null)
			{
				throw new global::System.ArgumentNullException("monitor");
			}
			global::UnityEngine.InputSystem.InputSystem.s_Manager.RemoveStateChangeMonitorTimeout(monitor, monitorIndex, timerIndex);
		}
	}
}
