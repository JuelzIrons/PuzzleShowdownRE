namespace UnityEngine.InputSystem.LowLevel
{
	internal class NativeInputRuntime : global::UnityEngine.InputSystem.LowLevel.IInputRuntime
	{
		public static readonly global::UnityEngine.InputSystem.LowLevel.NativeInputRuntime instance = new global::UnityEngine.InputSystem.LowLevel.NativeInputRuntime();

		private bool m_RunInBackground;

		private global::System.Action m_ShutdownMethod;

		private global::UnityEngine.InputSystem.LowLevel.InputUpdateDelegate m_OnUpdate;

		private global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputUpdateType> m_OnBeforeUpdate;

		private global::System.Func<global::UnityEngine.InputSystem.LowLevel.InputUpdateType, bool> m_OnShouldRunUpdate;

		private bool m_DidCallOnShutdown;

		private global::System.Action<bool> m_FocusChangedMethod;

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputUpdateDelegate onUpdate
		{
			get
			{
				return m_OnUpdate;
			}
			set
			{
				if (value != null)
				{
					global::UnityEngineInternal.Input.NativeInputSystem.onUpdate = delegate(global::UnityEngineInternal.Input.NativeInputUpdateType updateType, global::UnityEngineInternal.Input.NativeInputEventBuffer* eventBufferPtr)
					{
						global::UnityEngine.InputSystem.LowLevel.InputEventBuffer eventBuffer = new global::UnityEngine.InputSystem.LowLevel.InputEventBuffer((global::UnityEngine.InputSystem.LowLevel.InputEvent*)eventBufferPtr->eventBuffer, eventBufferPtr->eventCount, eventBufferPtr->sizeInBytes, eventBufferPtr->capacityInBytes);
						try
						{
							value((global::UnityEngine.InputSystem.LowLevel.InputUpdateType)updateType, ref eventBuffer);
						}
						catch (global::System.Exception ex)
						{
							global::UnityEngine.Debug.LogException(ex);
							global::UnityEngine.Debug.LogError($"{ex.GetType().Name} during event processing of {updateType} update; resetting event buffer");
							eventBuffer.Reset();
						}
						if (eventBuffer.eventCount > 0)
						{
							eventBufferPtr->eventCount = eventBuffer.eventCount;
							eventBufferPtr->sizeInBytes = (int)eventBuffer.sizeInBytes;
							eventBufferPtr->capacityInBytes = (int)eventBuffer.capacityInBytes;
							eventBufferPtr->eventBuffer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(eventBuffer.data);
						}
						else
						{
							eventBufferPtr->eventCount = 0;
							eventBufferPtr->sizeInBytes = 0;
						}
					};
				}
				else
				{
					global::UnityEngineInternal.Input.NativeInputSystem.onUpdate = null;
				}
				m_OnUpdate = value;
			}
		}

		public global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputUpdateType> onBeforeUpdate
		{
			get
			{
				return m_OnBeforeUpdate;
			}
			set
			{
				if (value != null)
				{
					global::UnityEngineInternal.Input.NativeInputSystem.onBeforeUpdate = delegate(global::UnityEngineInternal.Input.NativeInputUpdateType updateType)
					{
						value((global::UnityEngine.InputSystem.LowLevel.InputUpdateType)updateType);
					};
				}
				else
				{
					global::UnityEngineInternal.Input.NativeInputSystem.onBeforeUpdate = null;
				}
				m_OnBeforeUpdate = value;
			}
		}

		public global::System.Func<global::UnityEngine.InputSystem.LowLevel.InputUpdateType, bool> onShouldRunUpdate
		{
			get
			{
				return m_OnShouldRunUpdate;
			}
			set
			{
				if (value != null)
				{
					global::UnityEngineInternal.Input.NativeInputSystem.onShouldRunUpdate = (global::UnityEngineInternal.Input.NativeInputUpdateType updateType) => value((global::UnityEngine.InputSystem.LowLevel.InputUpdateType)updateType);
				}
				else
				{
					global::UnityEngineInternal.Input.NativeInputSystem.onShouldRunUpdate = null;
				}
				m_OnShouldRunUpdate = value;
			}
		}

		public global::System.Action<int, string> onDeviceDiscovered
		{
			get
			{
				return global::UnityEngineInternal.Input.NativeInputSystem.onDeviceDiscovered;
			}
			set
			{
				global::UnityEngineInternal.Input.NativeInputSystem.onDeviceDiscovered = value;
			}
		}

		public global::System.Action onShutdown
		{
			get
			{
				return m_ShutdownMethod;
			}
			set
			{
				if (value == null)
				{
					global::UnityEngine.Application.quitting -= OnShutdown;
				}
				else if (m_ShutdownMethod == null)
				{
					global::UnityEngine.Application.quitting += OnShutdown;
				}
				m_ShutdownMethod = value;
			}
		}

		public global::System.Action<bool> onPlayerFocusChanged
		{
			get
			{
				return m_FocusChangedMethod;
			}
			set
			{
				if (value == null)
				{
					global::UnityEngine.Application.focusChanged -= OnFocusChanged;
				}
				else if (m_FocusChangedMethod == null)
				{
					global::UnityEngine.Application.focusChanged += OnFocusChanged;
				}
				m_FocusChangedMethod = value;
			}
		}

		public bool isPlayerFocused => global::UnityEngine.Application.isFocused;

		public float pollingFrequency
		{
			get
			{
				return global::UnityEngineInternal.Input.NativeInputSystem.GetPollingFrequency();
			}
			set
			{
				global::UnityEngineInternal.Input.NativeInputSystem.SetPollingFrequency(value);
			}
		}

		public double currentTime => global::UnityEngineInternal.Input.NativeInputSystem.currentTime;

		public double currentTimeForFixedUpdate => (double)global::UnityEngine.Time.fixedUnscaledTime + currentTimeOffsetToRealtimeSinceStartup;

		public double currentTimeOffsetToRealtimeSinceStartup => global::UnityEngineInternal.Input.NativeInputSystem.currentTimeOffsetToRealtimeSinceStartup;

		public float unscaledGameTime => global::UnityEngine.Time.unscaledTime;

		public bool runInBackground
		{
			get
			{
				if (!global::UnityEngine.Application.runInBackground)
				{
					return m_RunInBackground;
				}
				return true;
			}
			set
			{
				m_RunInBackground = value;
			}
		}

		public global::UnityEngine.Vector2 screenSize => new global::UnityEngine.Vector2(global::UnityEngine.Screen.width, global::UnityEngine.Screen.height);

		public global::UnityEngine.ScreenOrientation screenOrientation => global::UnityEngine.Screen.orientation;

		public bool normalizeScrollWheelDelta
		{
			get
			{
				return global::UnityEngineInternal.Input.NativeInputSystem.normalizeScrollWheelDelta;
			}
			set
			{
				global::UnityEngineInternal.Input.NativeInputSystem.normalizeScrollWheelDelta = value;
			}
		}

		public float scrollWheelDeltaPerTick => global::UnityEngineInternal.Input.NativeInputSystem.GetScrollWheelDeltaPerTick();

		public int AllocateDeviceId()
		{
			return global::UnityEngineInternal.Input.NativeInputSystem.AllocateDeviceId();
		}

		public void Update(global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			global::UnityEngineInternal.Input.NativeInputSystem.Update((global::UnityEngineInternal.Input.NativeInputUpdateType)updateType);
		}

		public unsafe void QueueEvent(global::UnityEngine.InputSystem.LowLevel.InputEvent* ptr)
		{
			global::UnityEngineInternal.Input.NativeInputSystem.QueueInputEvent((global::System.IntPtr)ptr);
		}

		public unsafe long DeviceCommand(int deviceId, global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand* commandPtr)
		{
			if (commandPtr == null)
			{
				throw new global::System.ArgumentNullException("commandPtr");
			}
			return global::UnityEngineInternal.Input.NativeInputSystem.IOCTL(deviceId, commandPtr->type, new global::System.IntPtr(commandPtr->payloadPtr), commandPtr->payloadSizeInBytes);
		}

		private void OnShutdown()
		{
			m_ShutdownMethod();
		}

		private bool OnWantsToShutdown()
		{
			if (!m_DidCallOnShutdown)
			{
				OnShutdown();
				m_DidCallOnShutdown = true;
			}
			return true;
		}

		private void OnFocusChanged(bool focus)
		{
			m_FocusChangedMethod(focus);
		}
	}
}
