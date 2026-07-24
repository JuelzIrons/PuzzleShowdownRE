namespace UnityEngine.InputSystem.EnhancedTouch
{
	[global::UnityEngine.AddComponentMenu("Input/Debug/Touch Simulation")]
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/Touch.html#touch-simulation")]
	public class TouchSimulation : global::UnityEngine.MonoBehaviour, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor
	{
		[global::System.NonSerialized]
		private int m_NumPointers;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Pointer[] m_Pointers;

		[global::System.NonSerialized]
		private global::UnityEngine.Vector2[] m_CurrentPositions;

		[global::System.NonSerialized]
		private int[] m_CurrentDisplayIndices;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Controls.ButtonControl[] m_Touches;

		[global::System.NonSerialized]
		private int[] m_TouchIds;

		[global::System.NonSerialized]
		private int m_LastTouchId;

		[global::System.NonSerialized]
		private global::System.Action<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.InputDeviceChange> m_OnDeviceChange;

		[global::System.NonSerialized]
		private global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice> m_OnEvent;

		internal static global::UnityEngine.InputSystem.EnhancedTouch.TouchSimulation s_Instance;

		public global::UnityEngine.InputSystem.Touchscreen simulatedTouchscreen { get; private set; }

		public static global::UnityEngine.InputSystem.EnhancedTouch.TouchSimulation instance => s_Instance;

		public static void Enable()
		{
			if (instance == null)
			{
				global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject();
				obj.SetActive(value: false);
				obj.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				s_Instance = obj.AddComponent<global::UnityEngine.InputSystem.EnhancedTouch.TouchSimulation>();
				instance.gameObject.SetActive(value: true);
			}
			instance.enabled = true;
		}

		public static void Disable()
		{
			if (instance != null)
			{
				instance.enabled = false;
			}
		}

		public static void Destroy()
		{
			Disable();
			if (s_Instance != null)
			{
				global::UnityEngine.Object.Destroy(s_Instance.gameObject);
				s_Instance = null;
			}
		}

		protected void AddPointer(global::UnityEngine.InputSystem.Pointer pointer)
		{
			if (pointer == null)
			{
				throw new global::System.ArgumentNullException("pointer");
			}
			if (!global::UnityEngine.InputSystem.Utilities.ArrayHelpers.ContainsReference(m_Pointers, m_NumPointers, pointer))
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_Pointers, ref m_NumPointers, pointer);
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref m_CurrentPositions, default(global::UnityEngine.Vector2));
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref m_CurrentDisplayIndices, 0);
				global::UnityEngine.InputSystem.InputSystem.DisableDevice(pointer, keepSendingEvents: true);
			}
		}

		protected void RemovePointer(global::UnityEngine.InputSystem.Pointer pointer)
		{
			if (pointer == null)
			{
				throw new global::System.ArgumentNullException("pointer");
			}
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(m_Pointers, pointer, m_NumPointers);
			if (num == -1)
			{
				return;
			}
			for (int i = 0; i < m_Touches.Length; i++)
			{
				global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = m_Touches[i];
				if (buttonControl == null || buttonControl.device == pointer)
				{
					UpdateTouch(i, num, global::UnityEngine.InputSystem.TouchPhase.Canceled);
				}
			}
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(m_Pointers, ref m_NumPointers, num);
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAt(ref m_CurrentPositions, num);
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAt(ref m_CurrentDisplayIndices, num);
			if (pointer.added)
			{
				global::UnityEngine.InputSystem.InputSystem.EnableDevice(pointer);
			}
		}

		private unsafe void OnEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == simulatedTouchscreen)
			{
				return;
			}
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(m_Pointers, device, m_NumPointers);
			if (num < 0)
			{
				return;
			}
			global::UnityEngine.InputSystem.Utilities.FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				return;
			}
			global::UnityEngine.InputSystem.Pointer obj = m_Pointers[num];
			global::UnityEngine.InputSystem.Controls.Vector2Control position = obj.position;
			void* statePtrFromStateEventUnchecked = position.GetStatePtrFromStateEventUnchecked(eventPtr, type);
			if (statePtrFromStateEventUnchecked != null)
			{
				m_CurrentPositions[num] = position.ReadValueFromState(statePtrFromStateEventUnchecked);
			}
			global::UnityEngine.InputSystem.Controls.IntegerControl displayIndex = obj.displayIndex;
			void* statePtrFromStateEventUnchecked2 = displayIndex.GetStatePtrFromStateEventUnchecked(eventPtr, type);
			if (statePtrFromStateEventUnchecked2 != null)
			{
				m_CurrentDisplayIndices[num] = displayIndex.ReadValueFromState(statePtrFromStateEventUnchecked2);
			}
			for (int i = 0; i < m_Touches.Length; i++)
			{
				global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = m_Touches[i];
				if (buttonControl == null || buttonControl.device != device)
				{
					continue;
				}
				void* statePtrFromStateEventUnchecked3 = buttonControl.GetStatePtrFromStateEventUnchecked(eventPtr, type);
				if (statePtrFromStateEventUnchecked3 == null)
				{
					if (statePtrFromStateEventUnchecked != null)
					{
						UpdateTouch(i, num, global::UnityEngine.InputSystem.TouchPhase.Moved, eventPtr);
					}
				}
				else if (buttonControl.ReadValueFromState(statePtrFromStateEventUnchecked3) < global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint * global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonReleaseThreshold)
				{
					UpdateTouch(i, num, global::UnityEngine.InputSystem.TouchPhase.Ended, eventPtr);
				}
			}
			foreach (global::UnityEngine.InputSystem.InputControl item in eventPtr.EnumerateControls(global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, device))
			{
				if (!item.isButton)
				{
					continue;
				}
				void* statePtrFromStateEventUnchecked4 = item.GetStatePtrFromStateEventUnchecked(eventPtr, type);
				float output = 0f;
				item.ReadValueFromStateIntoBuffer(statePtrFromStateEventUnchecked4, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), 4);
				if (output <= global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint)
				{
					continue;
				}
				int num2 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(m_Touches, item);
				if (num2 < 0)
				{
					num2 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference<global::UnityEngine.InputSystem.Controls.ButtonControl, global::UnityEngine.InputSystem.Controls.ButtonControl>(m_Touches, null);
					if (num2 >= 0)
					{
						m_Touches[num2] = (global::UnityEngine.InputSystem.Controls.ButtonControl)item;
						UpdateTouch(num2, num, global::UnityEngine.InputSystem.TouchPhase.Began, eventPtr);
					}
				}
				else
				{
					UpdateTouch(num2, num, global::UnityEngine.InputSystem.TouchPhase.Moved, eventPtr);
				}
			}
			eventPtr.handled = true;
		}

		private void OnDeviceChange(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputDeviceChange change)
		{
			if (device == simulatedTouchscreen && change == global::UnityEngine.InputSystem.InputDeviceChange.Removed)
			{
				Disable();
				return;
			}
			switch (change)
			{
			case global::UnityEngine.InputSystem.InputDeviceChange.Added:
				if (device is global::UnityEngine.InputSystem.Pointer pointer2 && !(device is global::UnityEngine.InputSystem.Touchscreen))
				{
					AddPointer(pointer2);
				}
				break;
			case global::UnityEngine.InputSystem.InputDeviceChange.Removed:
				if (device is global::UnityEngine.InputSystem.Pointer pointer)
				{
					RemovePointer(pointer);
				}
				break;
			}
		}

		protected void OnEnable()
		{
			if (simulatedTouchscreen != null)
			{
				if (!simulatedTouchscreen.added)
				{
					global::UnityEngine.InputSystem.InputSystem.AddDevice(simulatedTouchscreen);
				}
			}
			else
			{
				simulatedTouchscreen = global::UnityEngine.InputSystem.InputSystem.GetDevice("Simulated Touchscreen") as global::UnityEngine.InputSystem.Touchscreen;
				if (simulatedTouchscreen == null)
				{
					simulatedTouchscreen = global::UnityEngine.InputSystem.InputSystem.AddDevice<global::UnityEngine.InputSystem.Touchscreen>("Simulated Touchscreen");
				}
			}
			if (m_Touches == null)
			{
				m_Touches = new global::UnityEngine.InputSystem.Controls.ButtonControl[simulatedTouchscreen.touches.Count];
			}
			if (m_TouchIds == null)
			{
				m_TouchIds = new int[simulatedTouchscreen.touches.Count];
			}
			foreach (global::UnityEngine.InputSystem.InputDevice device in global::UnityEngine.InputSystem.InputSystem.devices)
			{
				OnDeviceChange(device, global::UnityEngine.InputSystem.InputDeviceChange.Added);
			}
			if (m_OnDeviceChange == null)
			{
				m_OnDeviceChange = OnDeviceChange;
			}
			if (m_OnEvent == null)
			{
				m_OnEvent = OnEvent;
			}
			global::UnityEngine.InputSystem.InputSystem.onDeviceChange += m_OnDeviceChange;
			global::UnityEngine.InputSystem.InputSystem.onEvent += m_OnEvent;
		}

		protected void OnDisable()
		{
			if (simulatedTouchscreen != null && simulatedTouchscreen.added)
			{
				global::UnityEngine.InputSystem.InputSystem.RemoveDevice(simulatedTouchscreen);
			}
			for (int i = 0; i < m_NumPointers; i++)
			{
				global::UnityEngine.InputSystem.InputSystem.EnableDevice(m_Pointers[i]);
			}
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Clear(m_Pointers, m_NumPointers);
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Clear(m_Touches);
			m_NumPointers = 0;
			m_LastTouchId = 0;
			global::UnityEngine.InputSystem.InputSystem.onDeviceChange -= m_OnDeviceChange;
			global::UnityEngine.InputSystem.InputSystem.onEvent -= m_OnEvent;
		}

		private void UpdateTouch(int touchIndex, int pointerIndex, global::UnityEngine.InputSystem.TouchPhase phase, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr))
		{
			global::UnityEngine.Vector2 vector = m_CurrentPositions[pointerIndex];
			byte displayIndex = (byte)m_CurrentDisplayIndices[pointerIndex];
			global::UnityEngine.InputSystem.LowLevel.TouchState state = new global::UnityEngine.InputSystem.LowLevel.TouchState
			{
				phase = phase,
				position = vector,
				displayIndex = displayIndex
			};
			if (phase == global::UnityEngine.InputSystem.TouchPhase.Began)
			{
				state.startTime = (eventPtr.valid ? eventPtr.time : global::UnityEngine.InputSystem.LowLevel.InputState.currentTime);
				state.startPosition = vector;
				state.touchId = ++m_LastTouchId;
				m_TouchIds[touchIndex] = m_LastTouchId;
			}
			else
			{
				state.touchId = m_TouchIds[touchIndex];
			}
			global::UnityEngine.InputSystem.InputSystem.QueueStateEvent(simulatedTouchscreen, state);
			if (phase.IsEndedOrCanceled())
			{
				m_Touches[touchIndex] = null;
			}
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyControlStateChanged(global::UnityEngine.InputSystem.InputControl control, double time, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, long monitorIndex)
		{
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyTimerExpired(global::UnityEngine.InputSystem.InputControl control, double time, long monitorIndex, int timerIndex)
		{
		}

		protected void InstallStateChangeMonitors(int startIndex = 0)
		{
		}

		protected void OnSourceControlChangedValue(global::UnityEngine.InputSystem.InputControl control, double time, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, long sourceDeviceAndButtonIndex)
		{
		}

		protected void UninstallStateChangeMonitors(int startIndex = 0)
		{
		}
	}
}
