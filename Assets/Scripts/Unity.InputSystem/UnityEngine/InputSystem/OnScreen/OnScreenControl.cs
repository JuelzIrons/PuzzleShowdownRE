namespace UnityEngine.InputSystem.OnScreen
{
	public abstract class OnScreenControl : global::UnityEngine.MonoBehaviour
	{
		private struct OnScreenDeviceInfo
		{
			public global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr;

			public global::Unity.Collections.NativeArray<byte> buffer;

			public global::UnityEngine.InputSystem.InputDevice device;

			public global::UnityEngine.InputSystem.OnScreen.OnScreenControl firstControl;

			public global::UnityEngine.InputSystem.OnScreen.OnScreenControl.OnScreenDeviceInfo AddControl(global::UnityEngine.InputSystem.OnScreen.OnScreenControl control)
			{
				control.m_NextControlOnDevice = firstControl;
				firstControl = control;
				return this;
			}

			public global::UnityEngine.InputSystem.OnScreen.OnScreenControl.OnScreenDeviceInfo RemoveControl(global::UnityEngine.InputSystem.OnScreen.OnScreenControl control)
			{
				if (firstControl == control)
				{
					firstControl = control.m_NextControlOnDevice;
				}
				else
				{
					global::UnityEngine.InputSystem.OnScreen.OnScreenControl nextControlOnDevice = firstControl.m_NextControlOnDevice;
					global::UnityEngine.InputSystem.OnScreen.OnScreenControl onScreenControl = firstControl;
					while (nextControlOnDevice != null)
					{
						if (!(nextControlOnDevice != control))
						{
							onScreenControl.m_NextControlOnDevice = nextControlOnDevice.m_NextControlOnDevice;
							break;
						}
						onScreenControl = nextControlOnDevice;
						nextControlOnDevice = nextControlOnDevice.m_NextControlOnDevice;
					}
				}
				control.m_NextControlOnDevice = null;
				return this;
			}

			public void Destroy()
			{
				if (buffer.IsCreated)
				{
					buffer.Dispose();
				}
				if (device != null)
				{
					global::UnityEngine.InputSystem.InputSystem.RemoveDevice(device);
				}
				device = null;
				buffer = default(global::Unity.Collections.NativeArray<byte>);
			}
		}

		private global::UnityEngine.InputSystem.InputControl m_Control;

		private global::UnityEngine.InputSystem.OnScreen.OnScreenControl m_NextControlOnDevice;

		private global::UnityEngine.InputSystem.LowLevel.InputEventPtr m_InputEventPtr;

		private static int s_nbActiveInstances;

		private static global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.OnScreen.OnScreenControl.OnScreenDeviceInfo> s_OnScreenDevices;

		public string controlPath
		{
			get
			{
				return controlPathInternal;
			}
			set
			{
				controlPathInternal = value;
				if (base.isActiveAndEnabled)
				{
					SetupInputControl();
				}
			}
		}

		public global::UnityEngine.InputSystem.InputControl control => m_Control;

		protected abstract string controlPathInternal { get; set; }

		internal static bool HasAnyActive => s_nbActiveInstances != 0;

		private void SetupInputControl()
		{
			string text = controlPathInternal;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			string text2 = global::UnityEngine.InputSystem.InputControlPath.TryGetDeviceLayout(text);
			if (text2 == null)
			{
				global::UnityEngine.Debug.LogError("Cannot determine device layout to use based on control path '" + text + "' used in " + GetType().Name + " component", this);
				return;
			}
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(text2);
			int num = -1;
			for (int i = 0; i < s_OnScreenDevices.length; i++)
			{
				if (s_OnScreenDevices[i].device.m_Layout == internedString)
				{
					num = i;
					break;
				}
			}
			global::UnityEngine.InputSystem.InputDevice device;
			if (num == -1)
			{
				try
				{
					device = global::UnityEngine.InputSystem.InputSystem.AddDevice(text2);
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogError("Could not create device with layout '" + text2 + "' used in '" + GetType().Name + "' component");
					global::UnityEngine.Debug.LogException(exception);
					return;
				}
				global::UnityEngine.InputSystem.InputSystem.AddDeviceUsage(device, "OnScreen");
				global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr;
				global::Unity.Collections.NativeArray<byte> buffer = global::UnityEngine.InputSystem.LowLevel.StateEvent.From(device, out eventPtr, global::Unity.Collections.Allocator.Persistent);
				num = s_OnScreenDevices.Append(new global::UnityEngine.InputSystem.OnScreen.OnScreenControl.OnScreenDeviceInfo
				{
					eventPtr = eventPtr,
					buffer = buffer,
					device = device
				});
			}
			else
			{
				device = s_OnScreenDevices[num].device;
			}
			m_Control = global::UnityEngine.InputSystem.InputControlPath.TryFindControl(device, text);
			if (m_Control == null)
			{
				global::UnityEngine.Debug.LogError("Cannot find control with path '" + text + "' on device of type '" + text2 + "' referenced by component '" + GetType().Name + "'", this);
				if (s_OnScreenDevices[num].firstControl == null)
				{
					s_OnScreenDevices[num].Destroy();
					s_OnScreenDevices.RemoveAt(num);
				}
			}
			else
			{
				m_InputEventPtr = s_OnScreenDevices[num].eventPtr;
				s_OnScreenDevices[num] = s_OnScreenDevices[num].AddControl(this);
			}
		}

		protected void SendValueToControl<TValue>(TValue value) where TValue : struct
		{
			if (m_Control != null)
			{
				if (!(m_Control is global::UnityEngine.InputSystem.InputControl<TValue> inputControl))
				{
					throw new global::System.ArgumentException("The control path " + controlPath + " yields a control of type " + m_Control.GetType().Name + " which is not an InputControl with value type " + typeof(TValue).Name, "value");
				}
				m_InputEventPtr.internalTime = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime;
				inputControl.WriteValueIntoEvent(value, m_InputEventPtr);
				global::UnityEngine.InputSystem.InputSystem.QueueEvent(m_InputEventPtr);
			}
		}

		protected void SentDefaultValueToControl()
		{
			if (m_Control != null)
			{
				m_InputEventPtr.internalTime = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime;
				m_Control.ResetToDefaultStateInEvent(m_InputEventPtr);
				global::UnityEngine.InputSystem.InputSystem.QueueEvent(m_InputEventPtr);
			}
		}

		protected virtual void OnEnable()
		{
			s_nbActiveInstances++;
			SetupInputControl();
			if (m_Control == null || s_nbActiveInstances != 1 || !global::UnityEngine.InputSystem.PlayerInput.isSinglePlayer)
			{
				return;
			}
			global::UnityEngine.InputSystem.PlayerInput playerByIndex = global::UnityEngine.InputSystem.PlayerInput.GetPlayerByIndex(0);
			if ((object)playerByIndex == null || playerByIndex.neverAutoSwitchControlSchemes)
			{
				return;
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> devices = playerByIndex.devices;
			bool flag = false;
			foreach (global::UnityEngine.InputSystem.InputDevice item in devices)
			{
				if (m_Control.device.deviceId == item.deviceId)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				playerByIndex.SwitchCurrentControlScheme(m_Control.device);
			}
		}

		protected virtual void OnDisable()
		{
			s_nbActiveInstances--;
			if (m_Control == null)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputDevice device = m_Control.device;
			for (int i = 0; i < s_OnScreenDevices.length; i++)
			{
				if (s_OnScreenDevices[i].device != device)
				{
					continue;
				}
				global::UnityEngine.InputSystem.OnScreen.OnScreenControl.OnScreenDeviceInfo value = s_OnScreenDevices[i].RemoveControl(this);
				if (value.firstControl == null)
				{
					s_OnScreenDevices[i].Destroy();
					s_OnScreenDevices.RemoveAt(i);
				}
				else
				{
					s_OnScreenDevices[i] = value;
					if (!m_Control.CheckStateIsAtDefault())
					{
						SentDefaultValueToControl();
					}
				}
				m_Control = null;
				m_InputEventPtr = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr);
				break;
			}
		}

		internal string GetWarningMessage()
		{
			return $"{GetType()} needs to be attached as a child to a UI Canvas and have a RectTransform component to function properly.";
		}
	}
}
