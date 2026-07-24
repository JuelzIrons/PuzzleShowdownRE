namespace UnityEngine.InputSystem
{
	internal class InputManager
	{
		internal enum DeviceDisableScope
		{
			Everywhere = 0,
			InFrontendOnly = 1,
			TemporaryWhilePlayerIsInBackground = 2
		}

		[global::System.Serializable]
		internal struct AvailableDevice
		{
			public global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description;

			public int deviceId;

			public bool isNative;

			public bool isRemoved;
		}

		private struct StateChangeMonitorTimeout
		{
			public global::UnityEngine.InputSystem.InputControl control;

			public double time;

			public global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor;

			public long monitorIndex;

			public int timerIndex;
		}

		internal struct StateChangeMonitorListener
		{
			public global::UnityEngine.InputSystem.InputControl control;

			public global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor;

			public long monitorIndex;

			public uint groupIndex;
		}

		internal struct StateChangeMonitorsForDevice
		{
			public global::UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion[] memoryRegions;

			public global::UnityEngine.InputSystem.InputManager.StateChangeMonitorListener[] listeners;

			public global::UnityEngine.InputSystem.DynamicBitfield signalled;

			public bool needToUpdateOrderingOfMonitors;

			public bool needToCompactArrays;

			public int count => signalled.length;

			public void Add(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, long monitorIndex, uint groupIndex)
			{
				int length = signalled.length;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref listeners, ref length, new global::UnityEngine.InputSystem.InputManager.StateChangeMonitorListener
				{
					monitor = monitor,
					monitorIndex = monitorIndex,
					groupIndex = groupIndex,
					control = control
				});
				ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = ref control.m_StateBlock;
				int length2 = signalled.length;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref memoryRegions, ref length2, new global::UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion(stateBlock.byteOffset - control.device.stateBlock.byteOffset, stateBlock.bitOffset, stateBlock.sizeInBits));
				signalled.SetLength(signalled.length + 1);
				needToUpdateOrderingOfMonitors = true;
			}

			public void Remove(global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, long monitorIndex, bool deferRemoval)
			{
				if (listeners == null)
				{
					return;
				}
				for (int i = 0; i < signalled.length; i++)
				{
					if (listeners[i].monitor == monitor && listeners[i].monitorIndex == monitorIndex)
					{
						if (deferRemoval)
						{
							listeners[i] = default(global::UnityEngine.InputSystem.InputManager.StateChangeMonitorListener);
							memoryRegions[i] = default(global::UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion);
							signalled.ClearBit(i);
							needToCompactArrays = true;
						}
						else
						{
							RemoveAt(i);
						}
						break;
					}
				}
			}

			public void Clear()
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Clear(listeners, count);
				signalled.SetLength(0);
				needToCompactArrays = false;
			}

			public void CompactArrays()
			{
				for (int num = count - 1; num >= 0; num--)
				{
					if (memoryRegions[num].sizeInBits == 0)
					{
						RemoveAt(num);
					}
				}
				needToCompactArrays = false;
			}

			private void RemoveAt(int i)
			{
				int num = count;
				int num2 = count;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(listeners, ref num, i);
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(memoryRegions, ref num2, i);
				signalled.SetLength(count - 1);
			}

			public void SortMonitorsByIndex()
			{
				for (int i = 1; i < signalled.length; i++)
				{
					for (int num = i; num > 0; num--)
					{
						int complexityFromMonitorIndex = global::UnityEngine.InputSystem.InputActionState.GetComplexityFromMonitorIndex(listeners[num - 1].monitorIndex);
						int complexityFromMonitorIndex2 = global::UnityEngine.InputSystem.InputActionState.GetComplexityFromMonitorIndex(listeners[num].monitorIndex);
						if (complexityFromMonitorIndex >= complexityFromMonitorIndex2)
						{
							break;
						}
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.SwapElements(listeners, num, num - 1);
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.SwapElements(memoryRegions, num, num - 1);
					}
				}
				needToUpdateOrderingOfMonitors = false;
			}
		}

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputUpdateProfilerMarker = new global::Unity.Profiling.ProfilerMarker("InputUpdate");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputTryFindMatchingControllerMarker = new global::Unity.Profiling.ProfilerMarker("InputSystem.TryFindMatchingControlLayout");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputAddDeviceMarker = new global::Unity.Profiling.ProfilerMarker("InputSystem.AddDevice");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputRestoreDevicesAfterReloadMarker = new global::Unity.Profiling.ProfilerMarker("InputManager.RestoreDevicesAfterDomainReload");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputRegisterCustomTypesMarker = new global::Unity.Profiling.ProfilerMarker("InputManager.RegisterCustomTypes");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnBeforeUpdateMarker = new global::Unity.Profiling.ProfilerMarker("InputSystem.onBeforeUpdate");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnAfterUpdateMarker = new global::Unity.Profiling.ProfilerMarker("InputSystem.onAfterUpdate");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnSettingsChangeMarker = new global::Unity.Profiling.ProfilerMarker("InputSystem.onSettingsChange");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnDeviceSettingsChangeMarker = new global::Unity.Profiling.ProfilerMarker("InputSystem.onDeviceSettingsChange");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnEventMarker = new global::Unity.Profiling.ProfilerMarker("InputSystem.onEvent");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnLayoutChangeMarker = new global::Unity.Profiling.ProfilerMarker("InputSystem.onLayoutChange");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnDeviceChangeMarker = new global::Unity.Profiling.ProfilerMarker("InpustSystem.onDeviceChange");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnActionsChangeMarker = new global::Unity.Profiling.ProfilerMarker("InpustSystem.onActionsChange");

		private bool m_CustomTypesRegistered;

		internal int m_LayoutRegistrationVersion;

		private global::UnityEngine.InputSystem.LowLevel.InputEventHandledPolicy m_InputEventHandledPolicy;

		internal global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection m_Layouts;

		private global::UnityEngine.InputSystem.Utilities.TypeTable m_Processors;

		private global::UnityEngine.InputSystem.Utilities.TypeTable m_Interactions;

		private global::UnityEngine.InputSystem.Utilities.TypeTable m_Composites;

		private int m_DevicesCount;

		private global::UnityEngine.InputSystem.InputDevice[] m_Devices;

		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.InputSystem.InputDevice> m_DevicesById;

		internal int m_AvailableDeviceCount;

		internal global::UnityEngine.InputSystem.InputManager.AvailableDevice[] m_AvailableDevices;

		internal int m_DisconnectedDevicesCount;

		internal global::UnityEngine.InputSystem.InputDevice[] m_DisconnectedDevices;

		internal global::UnityEngine.InputSystem.LowLevel.InputUpdateType m_UpdateMask;

		private global::UnityEngine.InputSystem.LowLevel.InputUpdateType m_CurrentUpdate;

		internal global::UnityEngine.InputSystem.LowLevel.InputStateBuffers m_StateBuffers;

		private global::UnityEngine.InputSystem.InputSettings.ScrollDeltaBehavior m_ScrollDeltaBehavior;

		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.InputDeviceChange>> m_DeviceChangeListeners;

		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.LowLevel.InputEventPtr>> m_DeviceStateChangeListeners;

		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::UnityEngine.InputSystem.Layouts.InputDeviceFindControlLayoutDelegate> m_DeviceFindLayoutCallbacks;

		internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::UnityEngine.InputSystem.LowLevel.InputDeviceCommandDelegate> m_DeviceCommandCallbacks;

		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<string, global::UnityEngine.InputSystem.InputControlLayoutChange>> m_LayoutChangeListeners;

		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice>> m_EventListeners;

		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action> m_BeforeUpdateListeners;

		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action> m_AfterUpdateListeners;

		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action> m_SettingsChangedListeners;

		private global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action> m_ActionsChangedListeners;

		private bool m_NativeBeforeUpdateHooked;

		private bool m_HaveDevicesWithStateCallbackReceivers;

		private bool m_HasFocus;

		private bool m_DiscardOutOfFocusEvents;

		private double m_FocusRegainedTime;

		private global::UnityEngine.InputSystem.LowLevel.InputEventStream m_InputEventStream;

		private global::UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate m_DeviceFindExecuteCommandDelegate;

		private int m_DeviceFindExecuteCommandDeviceId;

		internal global::UnityEngine.InputSystem.LowLevel.IInputRuntime m_Runtime;

		internal global::UnityEngine.InputSystem.LowLevel.InputMetrics m_Metrics;

		internal global::UnityEngine.InputSystem.InputSettings m_Settings;

		private bool m_OptimizedControlsFeatureEnabled;

		private bool m_ReadValueCachingFeatureEnabled;

		private bool m_ParanoidReadValueCachingChecksEnabled;

		private global::UnityEngine.InputSystem.InputActionAsset m_Actions;

		private bool m_ShouldMakeCurrentlyUpdatingDeviceCurrent;

		internal global::UnityEngine.InputSystem.InputManager.StateChangeMonitorsForDevice[] m_StateChangeMonitors;

		private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.InputManager.StateChangeMonitorTimeout> m_StateChangeMonitorTimeouts;

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> devices => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>(m_Devices, 0, m_DevicesCount);

		public global::UnityEngine.InputSystem.Utilities.TypeTable processors => m_Processors;

		public global::UnityEngine.InputSystem.Utilities.TypeTable interactions => m_Interactions;

		public global::UnityEngine.InputSystem.Utilities.TypeTable composites => m_Composites;

		public global::UnityEngine.InputSystem.LowLevel.InputMetrics metrics
		{
			get
			{
				global::UnityEngine.InputSystem.LowLevel.InputMetrics result = m_Metrics;
				result.currentNumDevices = m_DevicesCount;
				result.currentStateSizeInBytes = (int)m_StateBuffers.totalSize;
				result.currentControlCount = m_DevicesCount;
				for (int i = 0; i < m_DevicesCount; i++)
				{
					result.currentControlCount += m_Devices[i].allControls.Count;
				}
				result.currentLayoutCount = m_Layouts.layoutTypes.Count;
				result.currentLayoutCount += m_Layouts.layoutStrings.Count;
				result.currentLayoutCount += m_Layouts.layoutBuilders.Count;
				result.currentLayoutCount += m_Layouts.layoutOverrides.Count;
				return result;
			}
		}

		public global::UnityEngine.InputSystem.InputSettings settings
		{
			get
			{
				return m_Settings;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				if (!(m_Settings == value))
				{
					m_Settings = value;
					ApplySettings();
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionAsset actions
		{
			get
			{
				return m_Actions;
			}
			set
			{
				m_Actions = value;
				ApplyActions();
			}
		}

		public global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateMask
		{
			get
			{
				return m_UpdateMask;
			}
			set
			{
				if (m_UpdateMask != value)
				{
					m_UpdateMask = value;
					if (m_DevicesCount > 0)
					{
						ReallocateStateBuffers();
					}
				}
			}
		}

		public global::UnityEngine.InputSystem.LowLevel.InputUpdateType defaultUpdateType
		{
			get
			{
				if (m_CurrentUpdate != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None)
				{
					return m_CurrentUpdate;
				}
				return global::UnityEngine.InputSystem.LowLevel.InputUpdate.GetUpdateTypeForPlayer(m_UpdateMask);
			}
		}

		public global::UnityEngine.InputSystem.InputSettings.ScrollDeltaBehavior scrollDeltaBehavior
		{
			get
			{
				return m_ScrollDeltaBehavior;
			}
			set
			{
				if (m_ScrollDeltaBehavior != value)
				{
					m_ScrollDeltaBehavior = value;
					global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.normalizeScrollWheelDelta = m_ScrollDeltaBehavior == global::UnityEngine.InputSystem.InputSettings.ScrollDeltaBehavior.UniformAcrossAllPlatforms;
				}
			}
		}

		public float pollingFrequency
		{
			get
			{
				return m_Runtime.pollingFrequency;
			}
			set
			{
				if (value <= 0f)
				{
					throw new global::System.ArgumentException("Polling frequency must be greater than zero", "value");
				}
				m_Runtime.pollingFrequency = value;
			}
		}

		internal global::UnityEngine.InputSystem.LowLevel.InputEventHandledPolicy inputEventHandledPolicy
		{
			get
			{
				return m_InputEventHandledPolicy;
			}
			set
			{
				if ((uint)value <= 1u)
				{
					m_InputEventHandledPolicy = value;
					return;
				}
				throw new global::System.ArgumentOutOfRangeException($"Unsupported input event handling policy: {value}");
			}
		}

		public bool isProcessingEvents => m_InputEventStream.isOpen;

		private bool gameIsPlaying => true;

		private bool gameHasFocus
		{
			get
			{
				if (!m_HasFocus)
				{
					return gameShouldGetInputRegardlessOfFocus;
				}
				return true;
			}
		}

		private bool gameShouldGetInputRegardlessOfFocus => m_Settings.backgroundBehavior == global::UnityEngine.InputSystem.InputSettings.BackgroundBehavior.IgnoreFocus;

		internal bool optimizedControlsFeatureEnabled
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_OptimizedControlsFeatureEnabled;
			}
			set
			{
				m_OptimizedControlsFeatureEnabled = value;
			}
		}

		internal bool readValueCachingFeatureEnabled
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_ReadValueCachingFeatureEnabled;
			}
			set
			{
				m_ReadValueCachingFeatureEnabled = value;
			}
		}

		internal bool paranoidReadValueCachingChecksEnabled
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_ParanoidReadValueCachingChecksEnabled;
			}
			set
			{
				m_ParanoidReadValueCachingChecksEnabled = value;
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.InputDeviceChange> onDeviceChange
		{
			add
			{
				m_DeviceChangeListeners.AddCallback(value);
			}
			remove
			{
				m_DeviceChangeListeners.RemoveCallback(value);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.LowLevel.InputEventPtr> onDeviceStateChange
		{
			add
			{
				m_DeviceStateChangeListeners.AddCallback(value);
			}
			remove
			{
				m_DeviceStateChangeListeners.RemoveCallback(value);
			}
		}

		public event global::UnityEngine.InputSystem.LowLevel.InputDeviceCommandDelegate onDeviceCommand
		{
			add
			{
				m_DeviceCommandCallbacks.AddCallback(value);
			}
			remove
			{
				m_DeviceCommandCallbacks.RemoveCallback(value);
			}
		}

		public event global::UnityEngine.InputSystem.Layouts.InputDeviceFindControlLayoutDelegate onFindControlLayoutForDevice
		{
			add
			{
				m_DeviceFindLayoutCallbacks.AddCallback(value);
				AddAvailableDevicesThatAreNowRecognized();
			}
			remove
			{
				m_DeviceFindLayoutCallbacks.RemoveCallback(value);
			}
		}

		public event global::System.Action<string, global::UnityEngine.InputSystem.InputControlLayoutChange> onLayoutChange
		{
			add
			{
				m_LayoutChangeListeners.AddCallback(value);
			}
			remove
			{
				m_LayoutChangeListeners.RemoveCallback(value);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice> onEvent
		{
			add
			{
				m_EventListeners.AddCallback(value);
			}
			remove
			{
				m_EventListeners.RemoveCallback(value);
			}
		}

		public event global::System.Action onBeforeUpdate
		{
			add
			{
				InstallBeforeUpdateHookIfNecessary();
				m_BeforeUpdateListeners.AddCallback(value);
			}
			remove
			{
				m_BeforeUpdateListeners.RemoveCallback(value);
			}
		}

		public event global::System.Action onAfterUpdate
		{
			add
			{
				m_AfterUpdateListeners.AddCallback(value);
			}
			remove
			{
				m_AfterUpdateListeners.RemoveCallback(value);
			}
		}

		public event global::System.Action onSettingsChange
		{
			add
			{
				m_SettingsChangedListeners.AddCallback(value);
			}
			remove
			{
				m_SettingsChangedListeners.RemoveCallback(value);
			}
		}

		public event global::System.Action onActionsChange
		{
			add
			{
				m_ActionsChangedListeners.AddCallback(value);
			}
			remove
			{
				m_ActionsChangedListeners.RemoveCallback(value);
			}
		}

		public void RegisterControlLayout(string name, global::System.Type type)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			bool flag = typeof(global::UnityEngine.InputSystem.InputDevice).IsAssignableFrom(type);
			bool flag2 = typeof(global::UnityEngine.InputSystem.InputControl).IsAssignableFrom(type);
			if (!flag && !flag2)
			{
				throw new global::System.ArgumentException("Types used as layouts have to be InputControls or InputDevices; '" + type.Name + "' is a '" + type.BaseType.Name + "'", "type");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
			bool isReplacement = m_Layouts.HasLayout(internedString);
			m_Layouts.layoutTypes[internedString] = type;
			string text = null;
			global::System.Type baseType = type.BaseType;
			while (text == null && baseType != typeof(global::UnityEngine.InputSystem.InputControl))
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Type> layoutType in m_Layouts.layoutTypes)
				{
					if (layoutType.Value == baseType)
					{
						text = layoutType.Key;
						break;
					}
				}
				baseType = baseType.BaseType;
			}
			PerformLayoutPostRegistration(internedString, new global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Utilities.InternedString>(new global::UnityEngine.InputSystem.Utilities.InternedString(text)), isReplacement, flag);
		}

		public void RegisterControlLayout(string json, string name = null, bool isOverride = false)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new global::System.ArgumentNullException("json");
			}
			global::UnityEngine.InputSystem.Layouts.InputControlLayout.ParseHeaderFieldsFromJson(json, out var name2, out var baseLayouts, out var deviceMatcher);
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
			if (internedString.IsEmpty())
			{
				internedString = name2;
				if (internedString.IsEmpty())
				{
					throw new global::System.ArgumentException("Layout name has not been given and is not set in JSON layout", "name");
				}
			}
			if (isOverride && baseLayouts.length == 0)
			{
				throw new global::System.ArgumentException($"Layout override '{internedString}' must have 'extend' property mentioning layout to which to apply the overrides", "json");
			}
			bool flag = m_Layouts.HasLayout(internedString);
			if (flag && isOverride && !m_Layouts.layoutOverrideNames.Contains(internedString))
			{
				throw new global::System.ArgumentException($"Failed to register layout override '{internedString}'" + $"since a layout named '{internedString}' already exist. Layout overrides must " + "have unique names with respect to existing layouts.");
			}
			m_Layouts.layoutStrings[internedString] = json;
			if (isOverride)
			{
				m_Layouts.layoutOverrideNames.Add(internedString);
				for (int i = 0; i < baseLayouts.length; i++)
				{
					global::UnityEngine.InputSystem.Utilities.InternedString key = baseLayouts[i];
					m_Layouts.layoutOverrides.TryGetValue(key, out var value);
					if (!flag)
					{
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref value, internedString);
					}
					m_Layouts.layoutOverrides[key] = value;
				}
			}
			PerformLayoutPostRegistration(internedString, baseLayouts, flag, isKnownToBeDeviceLayout: false, isOverride);
			if (!deviceMatcher.empty)
			{
				RegisterControlLayoutMatcher(internedString, deviceMatcher);
			}
		}

		public void RegisterControlLayoutBuilder(global::System.Func<global::UnityEngine.InputSystem.Layouts.InputControlLayout> method, string name, string baseLayout = null)
		{
			if (method == null)
			{
				throw new global::System.ArgumentNullException("method");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
			global::UnityEngine.InputSystem.Utilities.InternedString value = new global::UnityEngine.InputSystem.Utilities.InternedString(baseLayout);
			bool isReplacement = m_Layouts.HasLayout(internedString);
			m_Layouts.layoutBuilders[internedString] = method;
			PerformLayoutPostRegistration(internedString, new global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Utilities.InternedString>(value), isReplacement);
		}

		private void PerformLayoutPostRegistration(global::UnityEngine.InputSystem.Utilities.InternedString layoutName, global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Utilities.InternedString> baseLayouts, bool isReplacement, bool isKnownToBeDeviceLayout = false, bool isOverride = false)
		{
			m_LayoutRegistrationVersion++;
			global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_CacheInstance.Clear();
			if (!isOverride && baseLayouts.length > 0)
			{
				if (baseLayouts.length > 1)
				{
					throw new global::System.NotSupportedException($"Layout '{layoutName}' has multiple base layouts; this is only supported on layout overrides");
				}
				global::UnityEngine.InputSystem.Utilities.InternedString value = baseLayouts[0];
				if (!value.IsEmpty())
				{
					m_Layouts.baseLayoutTable[layoutName] = value;
				}
			}
			m_Layouts.precompiledLayouts.Remove(layoutName);
			if (m_Layouts.precompiledLayouts.Count > 0)
			{
				global::UnityEngine.InputSystem.Utilities.InternedString[] array = global::System.Linq.Enumerable.ToArray(m_Layouts.precompiledLayouts.Keys);
				foreach (global::UnityEngine.InputSystem.Utilities.InternedString internedString in array)
				{
					string metadata = m_Layouts.precompiledLayouts[internedString].metadata;
					if (isOverride)
					{
						for (int j = 0; j < baseLayouts.length; j++)
						{
							if (internedString == baseLayouts[j] || global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(metadata, baseLayouts[j], ';'))
							{
								m_Layouts.precompiledLayouts.Remove(internedString);
							}
						}
					}
					else if (global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(metadata, layoutName, ';'))
					{
						m_Layouts.precompiledLayouts.Remove(internedString);
					}
				}
			}
			if (isOverride)
			{
				for (int k = 0; k < baseLayouts.length; k++)
				{
					RecreateDevicesUsingLayout(baseLayouts[k], isKnownToBeDeviceLayout);
				}
			}
			else
			{
				RecreateDevicesUsingLayout(layoutName, isKnownToBeDeviceLayout);
			}
			global::UnityEngine.InputSystem.InputControlLayoutChange argument = (isReplacement ? global::UnityEngine.InputSystem.InputControlLayoutChange.Replaced : global::UnityEngine.InputSystem.InputControlLayoutChange.Added);
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_LayoutChangeListeners, layoutName.ToString(), argument, k_InputOnLayoutChangeMarker, "InputSystem.onLayoutChange");
		}

		public void RegisterPrecompiledLayout<TDevice>(string metadata) where TDevice : global::UnityEngine.InputSystem.InputDevice, new()
		{
			if (metadata == null)
			{
				throw new global::System.ArgumentNullException("metadata");
			}
			global::System.Type baseType = typeof(TDevice).BaseType;
			global::UnityEngine.InputSystem.Utilities.InternedString key = FindOrRegisterDeviceLayoutForType(baseType);
			m_Layouts.precompiledLayouts[key] = new global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection.PrecompiledLayout
			{
				factoryMethod = () => new TDevice(),
				metadata = metadata
			};
		}

		private void RecreateDevicesUsingLayout(global::UnityEngine.InputSystem.Utilities.InternedString layout, bool isKnownToBeDeviceLayout = false)
		{
			if (m_DevicesCount == 0)
			{
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputDevice> list = null;
			for (int i = 0; i < m_DevicesCount; i++)
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = m_Devices[i];
				if ((!isKnownToBeDeviceLayout) ? IsControlOrChildUsingLayoutRecursive(inputDevice, layout) : IsControlUsingLayout(inputDevice, layout))
				{
					if (list == null)
					{
						list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputDevice>();
					}
					list.Add(inputDevice);
				}
			}
			if (list == null)
			{
				return;
			}
			using (global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder.Ref())
			{
				for (int j = 0; j < list.Count; j++)
				{
					global::UnityEngine.InputSystem.InputDevice inputDevice2 = list[j];
					RecreateDevice(inputDevice2, inputDevice2.m_Layout);
				}
			}
		}

		private bool IsControlOrChildUsingLayoutRecursive(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.Utilities.InternedString layout)
		{
			if (IsControlUsingLayout(control, layout))
			{
				return true;
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> children = control.children;
			for (int i = 0; i < children.Count; i++)
			{
				if (IsControlOrChildUsingLayoutRecursive(children[i], layout))
				{
					return true;
				}
			}
			return false;
		}

		private bool IsControlUsingLayout(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.Utilities.InternedString layout)
		{
			if (control.layout == layout)
			{
				return true;
			}
			global::UnityEngine.InputSystem.Utilities.InternedString value = control.m_Layout;
			while (m_Layouts.baseLayoutTable.TryGetValue(value, out value))
			{
				if (value == layout)
				{
					return true;
				}
			}
			return false;
		}

		public void RegisterControlLayoutMatcher(string layoutName, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
		{
			if (string.IsNullOrEmpty(layoutName))
			{
				throw new global::System.ArgumentNullException("layoutName");
			}
			if (matcher.empty)
			{
				throw new global::System.ArgumentException("Matcher cannot be empty", "matcher");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString layout = new global::UnityEngine.InputSystem.Utilities.InternedString(layoutName);
			m_Layouts.AddMatcher(layout, matcher);
			RecreateDevicesUsingLayoutWithInferiorMatch(matcher);
			AddAvailableDevicesMatchingDescription(matcher, layout);
		}

		public void RegisterControlLayoutMatcher(global::System.Type type, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (matcher.empty)
			{
				throw new global::System.ArgumentException("Matcher cannot be empty", "matcher");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = m_Layouts.TryFindLayoutForType(type);
			if (internedString.IsEmpty())
			{
				throw new global::System.ArgumentException("Type '" + type.Name + "' has not been registered as a control layout", "type");
			}
			RegisterControlLayoutMatcher(internedString, matcher);
		}

		private void RecreateDevicesUsingLayoutWithInferiorMatch(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher deviceMatcher)
		{
			if (m_DevicesCount == 0)
			{
				return;
			}
			using (global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder.Ref())
			{
				int num = m_DevicesCount;
				for (int i = 0; i < num; i++)
				{
					global::UnityEngine.InputSystem.InputDevice inputDevice = m_Devices[i];
					global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription = inputDevice.description;
					if (!deviceDescription.empty && deviceMatcher.MatchPercentage(deviceDescription) > 0f)
					{
						global::UnityEngine.InputSystem.Utilities.InternedString internedString = TryFindMatchingControlLayout(ref deviceDescription, inputDevice.deviceId);
						if (internedString != inputDevice.m_Layout)
						{
							inputDevice.m_Description = deviceDescription;
							RecreateDevice(inputDevice, internedString);
							i--;
							num--;
						}
					}
				}
			}
		}

		private void RecreateDevice(global::UnityEngine.InputSystem.InputDevice oldDevice, global::UnityEngine.InputSystem.Utilities.InternedString newLayout)
		{
			RemoveDevice(oldDevice, keepOnListOfAvailableDevices: true);
			global::UnityEngine.InputSystem.InputDevice inputDevice = global::UnityEngine.InputSystem.InputDevice.Build<global::UnityEngine.InputSystem.InputDevice>(newLayout, oldDevice.m_Variants, oldDevice.m_Description);
			inputDevice.m_DeviceId = oldDevice.m_DeviceId;
			inputDevice.m_Description = oldDevice.m_Description;
			if (oldDevice.native)
			{
				inputDevice.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Native;
			}
			if (oldDevice.remote)
			{
				inputDevice.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Remote;
			}
			if (!oldDevice.enabled)
			{
				inputDevice.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
				inputDevice.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInFrontend;
			}
			AddDevice(inputDevice);
		}

		private void AddAvailableDevicesMatchingDescription(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher, global::UnityEngine.InputSystem.Utilities.InternedString layout)
		{
			for (int i = 0; i < m_AvailableDeviceCount; i++)
			{
				if (m_AvailableDevices[i].isRemoved)
				{
					continue;
				}
				int deviceId = m_AvailableDevices[i].deviceId;
				if (TryGetDeviceById(deviceId) == null && matcher.MatchPercentage(m_AvailableDevices[i].description) > 0f)
				{
					try
					{
						AddDevice(layout, deviceId, null, m_AvailableDevices[i].description, m_AvailableDevices[i].isNative ? global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Native : ((global::UnityEngine.InputSystem.InputDevice.DeviceFlags)0));
					}
					catch (global::System.Exception ex)
					{
						global::UnityEngine.Debug.LogError($"Layout '{layout}' matches existing device '{m_AvailableDevices[i].description}' but failed to instantiate: {ex}");
						global::UnityEngine.Debug.LogException(ex);
						continue;
					}
					global::UnityEngine.InputSystem.LowLevel.EnableDeviceCommand command = global::UnityEngine.InputSystem.LowLevel.EnableDeviceCommand.Create();
					global::UnityEngine.InputSystem.LowLevel.InputRuntimeExtensions.DeviceCommand(m_Runtime, deviceId, ref command);
				}
			}
		}

		public void RemoveControlLayout(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
			int num = 0;
			while (num < m_DevicesCount)
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = m_Devices[num];
				if (IsControlOrChildUsingLayoutRecursive(inputDevice, internedString))
				{
					RemoveDevice(inputDevice, keepOnListOfAvailableDevices: true);
				}
				else
				{
					num++;
				}
			}
			m_Layouts.layoutTypes.Remove(internedString);
			m_Layouts.layoutStrings.Remove(internedString);
			m_Layouts.layoutBuilders.Remove(internedString);
			m_Layouts.baseLayoutTable.Remove(internedString);
			m_LayoutRegistrationVersion++;
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_LayoutChangeListeners, name, global::UnityEngine.InputSystem.InputControlLayoutChange.Removed, k_InputOnLayoutChangeMarker, "InputSystem.onLayoutChange");
		}

		public global::UnityEngine.InputSystem.Layouts.InputControlLayout TryLoadControlLayout(global::System.Type type)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (!typeof(global::UnityEngine.InputSystem.InputControl).IsAssignableFrom(type))
			{
				throw new global::System.ArgumentException("Type '" + type.Name + "' is not an InputControl", "type");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString name = m_Layouts.TryFindLayoutForType(type);
			if (name.IsEmpty())
			{
				throw new global::System.ArgumentException("Type '" + type.Name + "' has not been registered as a control layout", "type");
			}
			return m_Layouts.TryLoadLayout(name);
		}

		public global::UnityEngine.InputSystem.Layouts.InputControlLayout TryLoadControlLayout(global::UnityEngine.InputSystem.Utilities.InternedString name)
		{
			return m_Layouts.TryLoadLayout(name);
		}

		public global::UnityEngine.InputSystem.Utilities.InternedString TryFindMatchingControlLayout(ref global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription, int deviceId = 0)
		{
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(string.Empty);
			try
			{
				internedString = m_Layouts.TryFindMatchingLayout(deviceDescription);
				if (internedString.IsEmpty() && !string.IsNullOrEmpty(deviceDescription.deviceClass))
				{
					global::UnityEngine.InputSystem.Utilities.InternedString layoutName = new global::UnityEngine.InputSystem.Utilities.InternedString(deviceDescription.deviceClass);
					global::System.Type controlTypeForLayout = m_Layouts.GetControlTypeForLayout(layoutName);
					if (controlTypeForLayout != null && typeof(global::UnityEngine.InputSystem.InputDevice).IsAssignableFrom(controlTypeForLayout))
					{
						internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(deviceDescription.deviceClass);
					}
				}
				if (m_DeviceFindLayoutCallbacks.length > 0)
				{
					if (m_DeviceFindExecuteCommandDelegate == null)
					{
						m_DeviceFindExecuteCommandDelegate = delegate(ref global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand commandRef)
						{
							return (m_DeviceFindExecuteCommandDeviceId == 0) ? (-1) : global::UnityEngine.InputSystem.LowLevel.InputRuntimeExtensions.DeviceCommand(m_Runtime, m_DeviceFindExecuteCommandDeviceId, ref commandRef);
						};
					}
					m_DeviceFindExecuteCommandDeviceId = deviceId;
					bool flag = false;
					m_DeviceFindLayoutCallbacks.LockForChanges();
					for (int num = 0; num < m_DeviceFindLayoutCallbacks.length; num++)
					{
						try
						{
							string text = m_DeviceFindLayoutCallbacks[num](ref deviceDescription, internedString, m_DeviceFindExecuteCommandDelegate);
							if (!string.IsNullOrEmpty(text) && !flag)
							{
								internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(text);
								flag = true;
							}
						}
						catch (global::System.Exception ex)
						{
							global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing 'InputSystem.onFindLayoutForDevice' callbacks");
							global::UnityEngine.Debug.LogException(ex);
						}
					}
					m_DeviceFindLayoutCallbacks.UnlockForChanges();
				}
			}
			finally
			{
			}
			return internedString;
		}

		private global::UnityEngine.InputSystem.Utilities.InternedString FindOrRegisterDeviceLayoutForType(global::System.Type type)
		{
			global::UnityEngine.InputSystem.Utilities.InternedString result = m_Layouts.TryFindLayoutForType(type);
			if (result.IsEmpty() && result.IsEmpty())
			{
				result = new global::UnityEngine.InputSystem.Utilities.InternedString(type.Name);
				RegisterControlLayout(type.Name, type);
			}
			return result;
		}

		private bool IsDeviceLayoutMarkedAsSupportedInSettings(global::UnityEngine.InputSystem.Utilities.InternedString layoutName)
		{
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<string> supportedDevices = m_Settings.supportedDevices;
			if (supportedDevices.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < supportedDevices.Count; i++)
			{
				global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(supportedDevices[i]);
				if (layoutName == internedString || m_Layouts.IsBasedOn(internedString, layoutName))
				{
					return true;
				}
			}
			return false;
		}

		public global::System.Collections.Generic.IEnumerable<string> ListControlLayouts(string basedOn = null)
		{
			if (!string.IsNullOrEmpty(basedOn))
			{
				global::UnityEngine.InputSystem.Utilities.InternedString internedBasedOn = new global::UnityEngine.InputSystem.Utilities.InternedString(basedOn);
				foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Type> layoutType in m_Layouts.layoutTypes)
				{
					if (m_Layouts.IsBasedOn(internedBasedOn, layoutType.Key))
					{
						yield return layoutType.Key;
					}
				}
				foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, string> layoutString in m_Layouts.layoutStrings)
				{
					if (m_Layouts.IsBasedOn(internedBasedOn, layoutString.Key))
					{
						yield return layoutString.Key;
					}
				}
				foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Func<global::UnityEngine.InputSystem.Layouts.InputControlLayout>> layoutBuilder in m_Layouts.layoutBuilders)
				{
					if (m_Layouts.IsBasedOn(internedBasedOn, layoutBuilder.Key))
					{
						yield return layoutBuilder.Key;
					}
				}
				yield break;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Type> layoutType2 in m_Layouts.layoutTypes)
			{
				yield return layoutType2.Key;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, string> layoutString2 in m_Layouts.layoutStrings)
			{
				yield return layoutString2.Key;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Func<global::UnityEngine.InputSystem.Layouts.InputControlLayout>> layoutBuilder2 in m_Layouts.layoutBuilders)
			{
				yield return layoutBuilder2.Key;
			}
		}

		public int GetControls<TControl>(string path, ref global::UnityEngine.InputSystem.InputControlList<TControl> controls) where TControl : global::UnityEngine.InputSystem.InputControl
		{
			if (string.IsNullOrEmpty(path))
			{
				return 0;
			}
			if (m_DevicesCount == 0)
			{
				return 0;
			}
			int devicesCount = m_DevicesCount;
			int num = 0;
			for (int i = 0; i < devicesCount; i++)
			{
				global::UnityEngine.InputSystem.InputDevice control = m_Devices[i];
				num += global::UnityEngine.InputSystem.InputControlPath.TryFindControls(control, path, 0, ref controls);
			}
			return num;
		}

		public void SetDeviceUsage(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.Utilities.InternedString usage)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if ((device.usages.Count != 1 || !(device.usages[0] == usage)) && (device.usages.Count != 0 || !usage.IsEmpty()))
			{
				device.ClearDeviceUsages();
				if (!usage.IsEmpty())
				{
					device.AddDeviceUsage(usage);
				}
				NotifyUsageChanged(device);
			}
		}

		public void AddDeviceUsage(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.Utilities.InternedString usage)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (usage.IsEmpty())
			{
				throw new global::System.ArgumentException("Usage string cannot be empty", "usage");
			}
			if (!global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.Contains(device.usages, usage))
			{
				device.AddDeviceUsage(usage);
				NotifyUsageChanged(device);
			}
		}

		public void RemoveDeviceUsage(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.Utilities.InternedString usage)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (usage.IsEmpty())
			{
				throw new global::System.ArgumentException("Usage string cannot be empty", "usage");
			}
			if (global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.Contains(device.usages, usage))
			{
				device.RemoveDeviceUsage(usage);
				NotifyUsageChanged(device);
			}
		}

		private void NotifyUsageChanged(global::UnityEngine.InputSystem.InputDevice device)
		{
			global::UnityEngine.InputSystem.InputActionState.OnDeviceChange(device, global::UnityEngine.InputSystem.InputDeviceChange.UsageChanged);
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceChangeListeners, device, global::UnityEngine.InputSystem.InputDeviceChange.UsageChanged, k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange");
			device.MakeCurrent();
		}

		internal bool HasDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device.m_DeviceIndex < m_DevicesCount)
			{
				return m_Devices[device.m_DeviceIndex] == device;
			}
			return false;
		}

		public global::UnityEngine.InputSystem.InputDevice AddDevice(global::System.Type type, string name = null)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = FindOrRegisterDeviceLayoutForType(type);
			return AddDevice(internedString, name);
		}

		public global::UnityEngine.InputSystem.InputDevice AddDevice(string layout, string name = null, global::UnityEngine.InputSystem.Utilities.InternedString variants = default(global::UnityEngine.InputSystem.Utilities.InternedString))
		{
			if (string.IsNullOrEmpty(layout))
			{
				throw new global::System.ArgumentNullException("layout");
			}
			global::UnityEngine.InputSystem.InputDevice inputDevice = global::UnityEngine.InputSystem.InputDevice.Build<global::UnityEngine.InputSystem.InputDevice>(layout, variants);
			if (!string.IsNullOrEmpty(name))
			{
				inputDevice.m_Name = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
			}
			AddDevice(inputDevice);
			return inputDevice;
		}

		private global::UnityEngine.InputSystem.InputDevice AddDevice(global::UnityEngine.InputSystem.Utilities.InternedString layout, int deviceId, string deviceName = null, global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription = default(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription), global::UnityEngine.InputSystem.InputDevice.DeviceFlags deviceFlags = (global::UnityEngine.InputSystem.InputDevice.DeviceFlags)0, global::UnityEngine.InputSystem.Utilities.InternedString variants = default(global::UnityEngine.InputSystem.Utilities.InternedString))
		{
			global::UnityEngine.InputSystem.InputDevice inputDevice = global::UnityEngine.InputSystem.InputDevice.Build<global::UnityEngine.InputSystem.InputDevice>(new global::UnityEngine.InputSystem.Utilities.InternedString(layout), deviceDescription: deviceDescription, layoutVariants: variants);
			inputDevice.m_DeviceId = deviceId;
			inputDevice.m_Description = deviceDescription;
			inputDevice.m_DeviceFlags |= deviceFlags;
			if (!string.IsNullOrEmpty(deviceName))
			{
				inputDevice.m_Name = new global::UnityEngine.InputSystem.Utilities.InternedString(deviceName);
			}
			if (!string.IsNullOrEmpty(deviceDescription.product))
			{
				inputDevice.m_DisplayName = deviceDescription.product;
			}
			AddDevice(inputDevice);
			return inputDevice;
		}

		public void AddDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (string.IsNullOrEmpty(device.layout))
			{
				throw new global::System.InvalidOperationException("Device has no associated layout");
			}
			if (global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Contains(m_Devices, device))
			{
				return;
			}
			MakeDeviceNameUnique(device);
			AssignUniqueDeviceId(device);
			device.m_DeviceIndex = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_Devices, ref m_DevicesCount, device);
			m_DevicesById[device.deviceId] = device;
			device.m_StateBlock.byteOffset = uint.MaxValue;
			ReallocateStateBuffers();
			InitializeDeviceState(device);
			m_Metrics.maxNumDevices = global::UnityEngine.Mathf.Max(m_DevicesCount, m_Metrics.maxNumDevices);
			m_Metrics.maxStateSizeInBytes = global::UnityEngine.Mathf.Max((int)m_StateBuffers.totalSize, m_Metrics.maxStateSizeInBytes);
			for (int i = 0; i < m_AvailableDeviceCount; i++)
			{
				if (m_AvailableDevices[i].deviceId == device.deviceId)
				{
					m_AvailableDevices[i].isRemoved = false;
				}
			}
			if (true && !gameHasFocus && m_Settings.backgroundBehavior != global::UnityEngine.InputSystem.InputSettings.BackgroundBehavior.IgnoreFocus && m_Runtime.runInBackground && device.QueryEnabledStateFromRuntime() && !ShouldRunDeviceInBackground(device))
			{
				EnableOrDisableDevice(device, enable: false, global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground);
			}
			global::UnityEngine.InputSystem.InputActionState.OnDeviceChange(device, global::UnityEngine.InputSystem.InputDeviceChange.Added);
			if (device is global::UnityEngine.InputSystem.LowLevel.IInputUpdateCallbackReceiver inputUpdateCallbackReceiver)
			{
				onBeforeUpdate += inputUpdateCallbackReceiver.OnUpdate;
			}
			if (device is global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver)
			{
				InstallBeforeUpdateHookIfNecessary();
				device.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasStateCallbacks;
				m_HaveDevicesWithStateCallbackReceivers = true;
			}
			if (device is global::UnityEngine.InputSystem.LowLevel.IEventMerger)
			{
				device.hasEventMerger = true;
			}
			if (device is global::UnityEngine.InputSystem.LowLevel.IEventPreProcessor)
			{
				device.hasEventPreProcessor = true;
			}
			if (device.updateBeforeRender)
			{
				updateMask |= global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender;
			}
			device.NotifyAdded();
			device.MakeCurrent();
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceChangeListeners, device, global::UnityEngine.InputSystem.InputDeviceChange.Added, k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange");
			if (device.enabled)
			{
				device.RequestSync();
			}
			device.SetOptimizedControlDataTypeRecursively();
		}

		public global::UnityEngine.InputSystem.InputDevice AddDevice(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description)
		{
			return AddDevice(description, throwIfNoLayoutFound: true);
		}

		public global::UnityEngine.InputSystem.InputDevice AddDevice(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description, bool throwIfNoLayoutFound, string deviceName = null, int deviceId = 0, global::UnityEngine.InputSystem.InputDevice.DeviceFlags deviceFlags = (global::UnityEngine.InputSystem.InputDevice.DeviceFlags)0)
		{
			global::UnityEngine.InputSystem.Utilities.InternedString layout = TryFindMatchingControlLayout(ref description, deviceId);
			if (layout.IsEmpty())
			{
				if (throwIfNoLayoutFound)
				{
					throw new global::System.ArgumentException($"Cannot find layout matching device description '{description}'", "description");
				}
				if (deviceId != 0)
				{
					global::UnityEngine.InputSystem.LowLevel.DisableDeviceCommand command = global::UnityEngine.InputSystem.LowLevel.DisableDeviceCommand.Create();
					global::UnityEngine.InputSystem.LowLevel.InputRuntimeExtensions.DeviceCommand(m_Runtime, deviceId, ref command);
				}
				return null;
			}
			global::UnityEngine.InputSystem.InputDevice inputDevice = AddDevice(layout, deviceId, deviceName, description, deviceFlags);
			inputDevice.m_Description = description;
			return inputDevice;
		}

		public global::UnityEngine.InputSystem.InputDevice AddDevice(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description, global::UnityEngine.InputSystem.Utilities.InternedString layout, string deviceName = null, int deviceId = 0, global::UnityEngine.InputSystem.InputDevice.DeviceFlags deviceFlags = (global::UnityEngine.InputSystem.InputDevice.DeviceFlags)0)
		{
			try
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = AddDevice(layout, deviceId, deviceName, description, deviceFlags);
				inputDevice.m_Description = description;
				return inputDevice;
			}
			finally
			{
			}
		}

		public void RemoveDevice(global::UnityEngine.InputSystem.InputDevice device, bool keepOnListOfAvailableDevices = false)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (device.m_DeviceIndex == -1)
			{
				return;
			}
			RemoveStateChangeMonitors(device);
			int deviceIndex = device.m_DeviceIndex;
			int deviceId = device.deviceId;
			if (deviceIndex < global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_StateChangeMonitors))
			{
				int count = m_StateChangeMonitors.Length;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(m_StateChangeMonitors, ref count, deviceIndex);
			}
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(m_Devices, ref m_DevicesCount, deviceIndex);
			m_DevicesById.Remove(deviceId);
			if (m_Devices != null)
			{
				ReallocateStateBuffers();
			}
			else
			{
				m_StateBuffers.FreeAll();
			}
			for (int i = deviceIndex; i < m_DevicesCount; i++)
			{
				m_Devices[i].m_DeviceIndex--;
			}
			device.m_DeviceIndex = -1;
			for (int j = 0; j < m_AvailableDeviceCount; j++)
			{
				if (m_AvailableDevices[j].deviceId == deviceId)
				{
					if (keepOnListOfAvailableDevices)
					{
						m_AvailableDevices[j].isRemoved = true;
					}
					else
					{
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(m_AvailableDevices, ref m_AvailableDeviceCount, j);
					}
					break;
				}
			}
			device.BakeOffsetIntoStateBlockRecursive((uint)(0uL - (ulong)device.m_StateBlock.byteOffset));
			global::UnityEngine.InputSystem.InputActionState.OnDeviceChange(device, global::UnityEngine.InputSystem.InputDeviceChange.Removed);
			if (device is global::UnityEngine.InputSystem.LowLevel.IInputUpdateCallbackReceiver inputUpdateCallbackReceiver)
			{
				onBeforeUpdate -= inputUpdateCallbackReceiver.OnUpdate;
			}
			if (device.updateBeforeRender)
			{
				bool flag = false;
				for (int k = 0; k < m_DevicesCount; k++)
				{
					if (m_Devices[k].updateBeforeRender)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					updateMask &= ~global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender;
				}
			}
			device.NotifyRemoved();
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceChangeListeners, device, global::UnityEngine.InputSystem.InputDeviceChange.Removed, k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange");
			global::UnityEngine.InputSystem.InputSystem.GetDevice(device.GetType())?.MakeCurrent();
		}

		public void FlushDisconnectedDevices()
		{
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Clear(m_DisconnectedDevices, m_DisconnectedDevicesCount);
			m_DisconnectedDevicesCount = 0;
		}

		public unsafe void ResetDevice(global::UnityEngine.InputSystem.InputDevice device, bool alsoResetDontResetControls = false, bool? issueResetCommand = null)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new global::System.InvalidOperationException($"Device '{device}' has not been added to the system");
			}
			bool flag = alsoResetDontResetControls || !device.hasDontResetControls;
			global::UnityEngine.InputSystem.InputDeviceChange inputDeviceChange = (flag ? global::UnityEngine.InputSystem.InputDeviceChange.HardReset : global::UnityEngine.InputSystem.InputDeviceChange.SoftReset);
			global::UnityEngine.InputSystem.InputActionState.OnDeviceChange(device, inputDeviceChange);
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceChangeListeners, device, inputDeviceChange, k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange");
			if (!alsoResetDontResetControls && device is global::UnityEngine.InputSystem.LowLevel.ICustomDeviceReset customDeviceReset)
			{
				customDeviceReset.Reset();
			}
			else
			{
				void* defaultStatePtr = device.defaultStatePtr;
				uint alignedSizeInBytes = device.stateBlock.alignedSizeInBytes;
				using global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>((int)(24 + alignedSizeInBytes), global::Unity.Collections.Allocator.Temp);
				global::UnityEngine.InputSystem.LowLevel.StateEvent* unsafePtr = (global::UnityEngine.InputSystem.LowLevel.StateEvent*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
				void* state = unsafePtr->state;
				double currentTime = m_Runtime.currentTime;
				ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = ref device.m_StateBlock;
				unsafePtr->baseEvent.type = 1398030676;
				unsafePtr->baseEvent.sizeInBytes = 24 + alignedSizeInBytes;
				unsafePtr->baseEvent.time = currentTime;
				unsafePtr->baseEvent.deviceId = device.deviceId;
				unsafePtr->baseEvent.eventId = -1;
				unsafePtr->stateFormat = device.m_StateBlock.format;
				if (flag)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(state, (byte*)defaultStatePtr + stateBlock.byteOffset, alignedSizeInBytes);
				}
				else
				{
					void* currentStatePtr = device.currentStatePtr;
					void* resetMaskBuffer = m_StateBuffers.resetMaskBuffer;
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(state, (byte*)currentStatePtr + stateBlock.byteOffset, alignedSizeInBytes);
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.MemCpyMasked(state, (byte*)defaultStatePtr + stateBlock.byteOffset, (int)alignedSizeInBytes, (byte*)resetMaskBuffer + stateBlock.byteOffset);
				}
				UpdateState(device, defaultUpdateType, state, 0u, alignedSizeInBytes, currentTime, new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)unsafePtr));
			}
			bool flag2 = flag;
			if (issueResetCommand.HasValue)
			{
				flag2 = issueResetCommand.Value;
			}
			if (flag2)
			{
				device.RequestReset();
			}
		}

		public global::UnityEngine.InputSystem.InputDevice TryGetDevice(string nameOrLayout)
		{
			if (string.IsNullOrEmpty(nameOrLayout))
			{
				throw new global::System.ArgumentException("Name is null or empty.", "nameOrLayout");
			}
			if (m_DevicesCount == 0)
			{
				return null;
			}
			string text = nameOrLayout.ToLower();
			for (int i = 0; i < m_DevicesCount; i++)
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = m_Devices[i];
				if (inputDevice.m_Name.ToLower() == text || inputDevice.m_Layout.ToLower() == text)
				{
					return inputDevice;
				}
			}
			return null;
		}

		public global::UnityEngine.InputSystem.InputDevice GetDevice(string nameOrLayout)
		{
			return TryGetDevice(nameOrLayout) ?? throw new global::System.ArgumentException("Cannot find device with name or layout '" + nameOrLayout + "'", "nameOrLayout");
		}

		public global::UnityEngine.InputSystem.InputDevice TryGetDevice(global::System.Type layoutType)
		{
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = m_Layouts.TryFindLayoutForType(layoutType);
			if (internedString.IsEmpty())
			{
				return null;
			}
			return TryGetDevice(internedString);
		}

		public global::UnityEngine.InputSystem.InputDevice TryGetDeviceById(int id)
		{
			if (m_DevicesById.TryGetValue(id, out var value))
			{
				return value;
			}
			return null;
		}

		public int GetUnsupportedDevices(global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputDeviceDescription> descriptions)
		{
			if (descriptions == null)
			{
				throw new global::System.ArgumentNullException("descriptions");
			}
			int num = 0;
			for (int i = 0; i < m_AvailableDeviceCount; i++)
			{
				if (TryGetDeviceById(m_AvailableDevices[i].deviceId) == null)
				{
					descriptions.Add(m_AvailableDevices[i].description);
					num++;
				}
			}
			return num;
		}

		public void EnableOrDisableDevice(global::UnityEngine.InputSystem.InputDevice device, bool enable, global::UnityEngine.InputSystem.InputManager.DeviceDisableScope scope = global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.Everywhere)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (enable)
			{
				switch (scope)
				{
				case global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.Everywhere:
					device.disabledWhileInBackground = false;
					if (!device.disabledInFrontend && !device.disabledInRuntime)
					{
						return;
					}
					if (device.disabledInRuntime)
					{
						device.ExecuteEnableCommand();
						device.disabledInRuntime = false;
					}
					if (device.disabledInFrontend)
					{
						if (!device.RequestSync())
						{
							ResetDevice(device);
						}
						device.disabledInFrontend = false;
					}
					break;
				case global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.InFrontendOnly:
					device.disabledWhileInBackground = false;
					if (!device.disabledInFrontend && device.disabledInRuntime)
					{
						return;
					}
					if (!device.disabledInRuntime)
					{
						device.ExecuteDisableCommand();
						device.disabledInRuntime = true;
					}
					if (device.disabledInFrontend)
					{
						if (!device.RequestSync())
						{
							ResetDevice(device);
						}
						device.disabledInFrontend = false;
					}
					break;
				case global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground:
					if (device.disabledWhileInBackground)
					{
						if (device.disabledInRuntime)
						{
							device.ExecuteEnableCommand();
							device.disabledInRuntime = false;
						}
						if (!device.RequestSync())
						{
							ResetDevice(device);
						}
						device.disabledWhileInBackground = false;
					}
					break;
				}
			}
			else
			{
				switch (scope)
				{
				case global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.Everywhere:
					device.disabledWhileInBackground = false;
					if (device.disabledInFrontend && device.disabledInRuntime)
					{
						return;
					}
					if (!device.disabledInRuntime)
					{
						device.ExecuteDisableCommand();
						device.disabledInRuntime = true;
					}
					if (!device.disabledInFrontend)
					{
						ResetDevice(device, alsoResetDontResetControls: false, false);
						device.disabledInFrontend = true;
					}
					break;
				case global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.InFrontendOnly:
					device.disabledWhileInBackground = false;
					if (!device.disabledInRuntime && device.disabledInFrontend)
					{
						return;
					}
					if (device.disabledInRuntime)
					{
						device.ExecuteEnableCommand();
						device.disabledInRuntime = false;
					}
					if (!device.disabledInFrontend)
					{
						ResetDevice(device, alsoResetDontResetControls: false, false);
						device.disabledInFrontend = true;
					}
					break;
				case global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground:
					if (device.disabledInFrontend || device.disabledWhileInBackground)
					{
						return;
					}
					device.disabledWhileInBackground = true;
					ResetDevice(device, alsoResetDontResetControls: false, false);
					device.ExecuteDisableCommand();
					device.disabledInRuntime = true;
					break;
				}
			}
			global::UnityEngine.InputSystem.InputDeviceChange argument = (enable ? global::UnityEngine.InputSystem.InputDeviceChange.Enabled : global::UnityEngine.InputSystem.InputDeviceChange.Disabled);
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceChangeListeners, device, argument, k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange");
		}

		private unsafe void QueueEvent(global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
		{
			if (m_InputEventStream.isOpen)
			{
				m_InputEventStream.Write(eventPtr);
			}
			else
			{
				m_Runtime.QueueEvent(eventPtr);
			}
		}

		public unsafe void QueueEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr ptr)
		{
			QueueEvent(ptr.data);
		}

		public unsafe void QueueEvent<TEvent>(ref TEvent inputEvent) where TEvent : struct, global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
		{
			QueueEvent((global::UnityEngine.InputSystem.LowLevel.InputEvent*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref inputEvent));
		}

		public void Update()
		{
			Update(defaultUpdateType);
		}

		public void Update(global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			m_Runtime.Update(updateType);
		}

		internal void Initialize(global::UnityEngine.InputSystem.LowLevel.IInputRuntime runtime, global::UnityEngine.InputSystem.InputSettings settings)
		{
			m_Settings = settings;
			InitializeActions();
			InitializeData();
			InstallRuntime(runtime);
			InstallGlobals();
			ApplySettings();
			ApplyActions();
		}

		internal void Destroy()
		{
			for (int i = 0; i < m_DevicesCount; i++)
			{
				m_Devices[i].NotifyRemoved();
			}
			m_StateBuffers.FreeAll();
			UninstallGlobals();
			if (m_Settings != null && m_Settings.hideFlags == global::UnityEngine.HideFlags.HideAndDontSave)
			{
				global::UnityEngine.Object.DestroyImmediate(m_Settings);
			}
		}

		private void InitializeActions()
		{
			m_Actions = null;
			global::UnityEngine.InputSystem.InputActionAsset[] array = global::UnityEngine.Resources.FindObjectsOfTypeAll<global::UnityEngine.InputSystem.InputActionAsset>();
			foreach (global::UnityEngine.InputSystem.InputActionAsset inputActionAsset in array)
			{
				if (inputActionAsset.m_IsProjectWide)
				{
					m_Actions = inputActionAsset;
					break;
				}
			}
		}

		internal void InitializeData()
		{
			m_Layouts.Allocate();
			m_Processors.Initialize(this);
			m_Interactions.Initialize(this);
			m_Composites.Initialize(this);
			m_DevicesById = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.InputSystem.InputDevice>();
			m_UpdateMask = global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Dynamic | global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Fixed;
			m_HasFocus = global::UnityEngine.Application.isFocused;
			m_ScrollDeltaBehavior = global::UnityEngine.InputSystem.InputSettings.ScrollDeltaBehavior.UniformAcrossAllPlatforms;
			m_InputEventHandledPolicy = global::UnityEngine.InputSystem.LowLevel.InputEventHandledPolicy.SuppressStateUpdates;
			RegisterControlLayout("Axis", typeof(global::UnityEngine.InputSystem.Controls.AxisControl));
			RegisterControlLayout("Button", typeof(global::UnityEngine.InputSystem.Controls.ButtonControl));
			RegisterControlLayout("DiscreteButton", typeof(global::UnityEngine.InputSystem.Controls.DiscreteButtonControl));
			RegisterControlLayout("Key", typeof(global::UnityEngine.InputSystem.Controls.KeyControl));
			RegisterControlLayout("Analog", typeof(global::UnityEngine.InputSystem.Controls.AxisControl));
			RegisterControlLayout("Integer", typeof(global::UnityEngine.InputSystem.Controls.IntegerControl));
			RegisterControlLayout("Digital", typeof(global::UnityEngine.InputSystem.Controls.IntegerControl));
			RegisterControlLayout("Double", typeof(global::UnityEngine.InputSystem.Controls.DoubleControl));
			RegisterControlLayout("Vector2", typeof(global::UnityEngine.InputSystem.Controls.Vector2Control));
			RegisterControlLayout("Vector3", typeof(global::UnityEngine.InputSystem.Controls.Vector3Control));
			RegisterControlLayout("Delta", typeof(global::UnityEngine.InputSystem.Controls.DeltaControl));
			RegisterControlLayout("Quaternion", typeof(global::UnityEngine.InputSystem.Controls.QuaternionControl));
			RegisterControlLayout("Stick", typeof(global::UnityEngine.InputSystem.Controls.StickControl));
			RegisterControlLayout("Dpad", typeof(global::UnityEngine.InputSystem.Controls.DpadControl));
			RegisterControlLayout("DpadAxis", typeof(global::UnityEngine.InputSystem.Controls.DpadControl.DpadAxisControl));
			RegisterControlLayout("AnyKey", typeof(global::UnityEngine.InputSystem.Controls.AnyKeyControl));
			RegisterControlLayout("Touch", typeof(global::UnityEngine.InputSystem.Controls.TouchControl));
			RegisterControlLayout("TouchPhase", typeof(global::UnityEngine.InputSystem.Controls.TouchPhaseControl));
			RegisterControlLayout("TouchPress", typeof(global::UnityEngine.InputSystem.Controls.TouchPressControl));
			RegisterControlLayout("Gamepad", typeof(global::UnityEngine.InputSystem.Gamepad));
			RegisterControlLayout("Joystick", typeof(global::UnityEngine.InputSystem.Joystick));
			RegisterControlLayout("Keyboard", typeof(global::UnityEngine.InputSystem.Keyboard));
			RegisterControlLayout("Pointer", typeof(global::UnityEngine.InputSystem.Pointer));
			RegisterControlLayout("Mouse", typeof(global::UnityEngine.InputSystem.Mouse));
			RegisterControlLayout("Pen", typeof(global::UnityEngine.InputSystem.Pen));
			RegisterControlLayout("Touchscreen", typeof(global::UnityEngine.InputSystem.Touchscreen));
			RegisterControlLayout("Sensor", typeof(global::UnityEngine.InputSystem.Sensor));
			RegisterControlLayout("Accelerometer", typeof(global::UnityEngine.InputSystem.Accelerometer));
			RegisterControlLayout("Gyroscope", typeof(global::UnityEngine.InputSystem.Gyroscope));
			RegisterControlLayout("GravitySensor", typeof(global::UnityEngine.InputSystem.GravitySensor));
			RegisterControlLayout("AttitudeSensor", typeof(global::UnityEngine.InputSystem.AttitudeSensor));
			RegisterControlLayout("LinearAccelerationSensor", typeof(global::UnityEngine.InputSystem.LinearAccelerationSensor));
			RegisterControlLayout("MagneticFieldSensor", typeof(global::UnityEngine.InputSystem.MagneticFieldSensor));
			RegisterControlLayout("LightSensor", typeof(global::UnityEngine.InputSystem.LightSensor));
			RegisterControlLayout("PressureSensor", typeof(global::UnityEngine.InputSystem.PressureSensor));
			RegisterControlLayout("HumiditySensor", typeof(global::UnityEngine.InputSystem.HumiditySensor));
			RegisterControlLayout("AmbientTemperatureSensor", typeof(global::UnityEngine.InputSystem.AmbientTemperatureSensor));
			RegisterControlLayout("StepCounter", typeof(global::UnityEngine.InputSystem.StepCounter));
			RegisterControlLayout("TrackedDevice", typeof(global::UnityEngine.InputSystem.TrackedDevice));
			RegisterPrecompiledLayout<global::UnityEngine.InputSystem.FastKeyboard>(";AnyKey;Button;Axis;Key;DiscreteButton;Keyboard");
			RegisterPrecompiledLayout<global::UnityEngine.InputSystem.FastTouchscreen>("AutoWindowSpace;Touch;Vector2;Delta;Analog;TouchPress;Button;Axis;Integer;TouchPhase;Double;Touchscreen;Pointer");
			RegisterPrecompiledLayout<global::UnityEngine.InputSystem.FastMouse>("AutoWindowSpace;Vector2;Delta;Button;Axis;Digital;Integer;Mouse;Pointer");
			processors.AddTypeRegistration("Invert", typeof(global::UnityEngine.InputSystem.Processors.InvertProcessor));
			processors.AddTypeRegistration("InvertVector2", typeof(global::UnityEngine.InputSystem.Processors.InvertVector2Processor));
			processors.AddTypeRegistration("InvertVector3", typeof(global::UnityEngine.InputSystem.Processors.InvertVector3Processor));
			processors.AddTypeRegistration("Clamp", typeof(global::UnityEngine.InputSystem.Processors.ClampProcessor));
			processors.AddTypeRegistration("Normalize", typeof(global::UnityEngine.InputSystem.Processors.NormalizeProcessor));
			processors.AddTypeRegistration("NormalizeVector2", typeof(global::UnityEngine.InputSystem.Processors.NormalizeVector2Processor));
			processors.AddTypeRegistration("NormalizeVector3", typeof(global::UnityEngine.InputSystem.Processors.NormalizeVector3Processor));
			processors.AddTypeRegistration("Scale", typeof(global::UnityEngine.InputSystem.Processors.ScaleProcessor));
			processors.AddTypeRegistration("ScaleVector2", typeof(global::UnityEngine.InputSystem.Processors.ScaleVector2Processor));
			processors.AddTypeRegistration("ScaleVector3", typeof(global::UnityEngine.InputSystem.Processors.ScaleVector3Processor));
			processors.AddTypeRegistration("StickDeadzone", typeof(global::UnityEngine.InputSystem.Processors.StickDeadzoneProcessor));
			processors.AddTypeRegistration("AxisDeadzone", typeof(global::UnityEngine.InputSystem.Processors.AxisDeadzoneProcessor));
			processors.AddTypeRegistration("CompensateDirection", typeof(global::UnityEngine.InputSystem.Processors.CompensateDirectionProcessor));
			processors.AddTypeRegistration("CompensateRotation", typeof(global::UnityEngine.InputSystem.Processors.CompensateRotationProcessor));
			interactions.AddTypeRegistration("Hold", typeof(global::UnityEngine.InputSystem.Interactions.HoldInteraction));
			interactions.AddTypeRegistration("Tap", typeof(global::UnityEngine.InputSystem.Interactions.TapInteraction));
			interactions.AddTypeRegistration("SlowTap", typeof(global::UnityEngine.InputSystem.Interactions.SlowTapInteraction));
			interactions.AddTypeRegistration("MultiTap", typeof(global::UnityEngine.InputSystem.Interactions.MultiTapInteraction));
			interactions.AddTypeRegistration("Press", typeof(global::UnityEngine.InputSystem.Interactions.PressInteraction));
			composites.AddTypeRegistration("1DAxis", typeof(global::UnityEngine.InputSystem.Composites.AxisComposite));
			composites.AddTypeRegistration("2DVector", typeof(global::UnityEngine.InputSystem.Composites.Vector2Composite));
			composites.AddTypeRegistration("3DVector", typeof(global::UnityEngine.InputSystem.Composites.Vector3Composite));
			composites.AddTypeRegistration("Axis", typeof(global::UnityEngine.InputSystem.Composites.AxisComposite));
			composites.AddTypeRegistration("Dpad", typeof(global::UnityEngine.InputSystem.Composites.Vector2Composite));
			composites.AddTypeRegistration("ButtonWithOneModifier", typeof(global::UnityEngine.InputSystem.Composites.ButtonWithOneModifier));
			composites.AddTypeRegistration("ButtonWithTwoModifiers", typeof(global::UnityEngine.InputSystem.Composites.ButtonWithTwoModifiers));
			composites.AddTypeRegistration("OneModifier", typeof(global::UnityEngine.InputSystem.Composites.OneModifierComposite));
			composites.AddTypeRegistration("TwoModifiers", typeof(global::UnityEngine.InputSystem.Composites.TwoModifiersComposite));
		}

		private static void RegisterCustomTypes(global::System.Type[] types)
		{
			foreach (global::System.Type type in types)
			{
				if (type.IsClass && !type.IsAbstract && !type.IsGenericType)
				{
					if (typeof(global::UnityEngine.InputSystem.InputProcessor).IsAssignableFrom(type))
					{
						global::UnityEngine.InputSystem.InputSystem.RegisterProcessor(type);
					}
					else if (typeof(global::UnityEngine.InputSystem.IInputInteraction).IsAssignableFrom(type))
					{
						global::UnityEngine.InputSystem.InputSystem.RegisterInteraction(type);
					}
					else if (typeof(global::UnityEngine.InputSystem.InputBindingComposite).IsAssignableFrom(type))
					{
						global::UnityEngine.InputSystem.InputSystem.RegisterBindingComposite(type, null);
					}
				}
			}
		}

		internal bool RegisterCustomTypes()
		{
			if (m_CustomTypesRegistered)
			{
				return false;
			}
			m_CustomTypesRegistered = true;
			global::System.Reflection.Assembly assembly = typeof(global::UnityEngine.InputSystem.InputProcessor).Assembly;
			string name = assembly.GetName().Name;
			global::System.Reflection.Assembly[] assemblies = global::System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (global::System.Reflection.Assembly assembly2 in assemblies)
			{
				try
				{
					if (assembly2 == assembly)
					{
						continue;
					}
					global::System.Reflection.AssemblyName[] referencedAssemblies = assembly2.GetReferencedAssemblies();
					for (int j = 0; j < referencedAssemblies.Length; j++)
					{
						if (referencedAssemblies[j].Name == name)
						{
							RegisterCustomTypes(assembly2.GetTypes());
							break;
						}
					}
				}
				catch (global::System.Reflection.ReflectionTypeLoadException)
				{
				}
			}
			return true;
		}

		internal void InstallRuntime(global::UnityEngine.InputSystem.LowLevel.IInputRuntime runtime)
		{
			if (m_Runtime != null)
			{
				m_Runtime.onUpdate = null;
				m_Runtime.onBeforeUpdate = null;
				m_Runtime.onDeviceDiscovered = null;
				m_Runtime.onPlayerFocusChanged = null;
				m_Runtime.onShouldRunUpdate = null;
			}
			m_Runtime = runtime;
			m_Runtime.onUpdate = OnUpdate;
			m_Runtime.onDeviceDiscovered = OnNativeDeviceDiscovered;
			m_Runtime.onPlayerFocusChanged = OnFocusChanged;
			m_Runtime.onShouldRunUpdate = ShouldRunUpdate;
			m_Runtime.pollingFrequency = pollingFrequency;
			m_HasFocus = m_Runtime.isPlayerFocused;
			if (m_BeforeUpdateListeners.length > 0 || m_HaveDevicesWithStateCallbackReceivers)
			{
				m_Runtime.onBeforeUpdate = OnBeforeUpdate;
				m_NativeBeforeUpdateHooked = true;
			}
		}

		internal unsafe void InstallGlobals()
		{
			global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts = m_Layouts;
			global::UnityEngine.InputSystem.InputProcessor.s_Processors = m_Processors;
			global::UnityEngine.InputSystem.InputInteraction.s_Interactions = m_Interactions;
			global::UnityEngine.InputSystem.InputBindingComposite.s_Composites = m_Composites;
			global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance = m_Runtime;
			global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup = m_Runtime.currentTimeOffsetToRealtimeSinceStartup;
			global::UnityEngine.InputSystem.LowLevel.InputUpdate.Restore(default(global::UnityEngine.InputSystem.LowLevel.InputUpdate.SerializedState));
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.SwitchTo(m_StateBuffers, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Dynamic);
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_DefaultStateBuffer = m_StateBuffers.defaultStateBuffer;
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_NoiseMaskBuffer = m_StateBuffers.noiseMaskBuffer;
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_ResetMaskBuffer = m_StateBuffers.resetMaskBuffer;
		}

		internal void UninstallGlobals()
		{
			if (global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.baseLayoutTable == m_Layouts.baseLayoutTable)
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts = default(global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection);
			}
			if (global::UnityEngine.InputSystem.InputProcessor.s_Processors.table == m_Processors.table)
			{
				global::UnityEngine.InputSystem.InputProcessor.s_Processors = default(global::UnityEngine.InputSystem.Utilities.TypeTable);
			}
			if (global::UnityEngine.InputSystem.InputInteraction.s_Interactions.table == m_Interactions.table)
			{
				global::UnityEngine.InputSystem.InputInteraction.s_Interactions = default(global::UnityEngine.InputSystem.Utilities.TypeTable);
			}
			if (global::UnityEngine.InputSystem.InputBindingComposite.s_Composites.table == m_Composites.table)
			{
				global::UnityEngine.InputSystem.InputBindingComposite.s_Composites = default(global::UnityEngine.InputSystem.Utilities.TypeTable);
			}
			global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_CacheInstance = default(global::UnityEngine.InputSystem.Layouts.InputControlLayout.Cache);
			global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_CacheInstanceRef = 0;
			m_CustomTypesRegistered = false;
			if (m_Runtime != null)
			{
				m_Runtime.onUpdate = null;
				m_Runtime.onDeviceDiscovered = null;
				m_Runtime.onBeforeUpdate = null;
				m_Runtime.onPlayerFocusChanged = null;
				m_Runtime.onShouldRunUpdate = null;
				if (global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance == m_Runtime)
				{
					global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance = null;
				}
			}
		}

		private void MakeDeviceNameUnique(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (m_DevicesCount != 0)
			{
				string text = global::UnityEngine.InputSystem.Utilities.StringHelpers.MakeUniqueName(device.name, m_Devices, (global::UnityEngine.InputSystem.InputDevice x) => (x == null) ? string.Empty : x.name);
				if (text != device.name)
				{
					ResetControlPathsRecursive(device);
					device.m_Name = new global::UnityEngine.InputSystem.Utilities.InternedString(text);
				}
			}
		}

		private static void ResetControlPathsRecursive(global::UnityEngine.InputSystem.InputControl control)
		{
			control.m_Path = null;
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> children = control.children;
			int count = children.Count;
			for (int i = 0; i < count; i++)
			{
				ResetControlPathsRecursive(children[i]);
			}
		}

		private void AssignUniqueDeviceId(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device.deviceId != 0)
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = TryGetDeviceById(device.deviceId);
				if (inputDevice != null)
				{
					throw new global::System.InvalidOperationException($"Duplicate device ID {device.deviceId} detected for devices '{device.name}' and '{inputDevice.name}'");
				}
			}
			else
			{
				device.m_DeviceId = m_Runtime.AllocateDeviceId();
			}
		}

		private unsafe void ReallocateStateBuffers()
		{
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers stateBuffers = m_StateBuffers;
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers stateBuffers2 = default(global::UnityEngine.InputSystem.LowLevel.InputStateBuffers);
			stateBuffers2.AllocateAll(m_Devices, m_DevicesCount);
			stateBuffers2.MigrateAll(m_Devices, m_DevicesCount, stateBuffers);
			stateBuffers.FreeAll();
			m_StateBuffers = stateBuffers2;
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_DefaultStateBuffer = stateBuffers2.defaultStateBuffer;
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_NoiseMaskBuffer = stateBuffers2.noiseMaskBuffer;
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_ResetMaskBuffer = stateBuffers2.resetMaskBuffer;
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.SwitchTo(m_StateBuffers, (global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_LatestUpdateType != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None) ? global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_LatestUpdateType : defaultUpdateType);
		}

		private unsafe void InitializeDefaultState(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (!device.hasControlsWithDefaultState)
			{
				return;
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> allControls = device.allControls;
			int count = allControls.Count;
			void* defaultStateBuffer = m_StateBuffers.defaultStateBuffer;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.InputSystem.InputControl inputControl = allControls[i];
				if (inputControl.hasDefaultState)
				{
					inputControl.m_StateBlock.Write(defaultStateBuffer, inputControl.m_DefaultState);
				}
			}
			global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = device.m_StateBlock;
			int deviceIndex = device.m_DeviceIndex;
			if (m_StateBuffers.m_PlayerStateBuffers.valid)
			{
				stateBlock.CopyToFrom(m_StateBuffers.m_PlayerStateBuffers.GetFrontBuffer(deviceIndex), defaultStateBuffer);
				stateBlock.CopyToFrom(m_StateBuffers.m_PlayerStateBuffers.GetBackBuffer(deviceIndex), defaultStateBuffer);
			}
		}

		private unsafe void InitializeDeviceState(global::UnityEngine.InputSystem.InputDevice device)
		{
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> allControls = device.allControls;
			int count = allControls.Count;
			void* resetMaskBuffer = m_StateBuffers.resetMaskBuffer;
			bool hasControlsWithDefaultState = device.hasControlsWithDefaultState;
			void* noiseMaskBuffer = m_StateBuffers.noiseMaskBuffer;
			global::UnityEngine.InputSystem.Utilities.MemoryHelpers.SetBitsInBuffer(noiseMaskBuffer, (int)device.stateBlock.byteOffset, 0, (int)device.stateBlock.sizeInBits, value: false);
			global::UnityEngine.InputSystem.Utilities.MemoryHelpers.SetBitsInBuffer(resetMaskBuffer, (int)device.stateBlock.byteOffset, 0, (int)device.stateBlock.sizeInBits, value: true);
			void* defaultStateBuffer = m_StateBuffers.defaultStateBuffer;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.InputSystem.InputControl inputControl = allControls[i];
				if (inputControl.usesStateFromOtherControl)
				{
					continue;
				}
				if (!inputControl.noisy || inputControl.dontReset)
				{
					ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = ref inputControl.m_StateBlock;
					if (!inputControl.noisy)
					{
						global::UnityEngine.InputSystem.Utilities.MemoryHelpers.SetBitsInBuffer(noiseMaskBuffer, (int)stateBlock.byteOffset, (int)stateBlock.bitOffset, (int)stateBlock.sizeInBits, value: true);
					}
					if (inputControl.dontReset)
					{
						global::UnityEngine.InputSystem.Utilities.MemoryHelpers.SetBitsInBuffer(resetMaskBuffer, (int)stateBlock.byteOffset, (int)stateBlock.bitOffset, (int)stateBlock.sizeInBits, value: false);
					}
				}
				if (hasControlsWithDefaultState && inputControl.hasDefaultState)
				{
					inputControl.m_StateBlock.Write(defaultStateBuffer, inputControl.m_DefaultState);
				}
			}
			if (hasControlsWithDefaultState)
			{
				ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock2 = ref device.m_StateBlock;
				int deviceIndex = device.m_DeviceIndex;
				if (m_StateBuffers.m_PlayerStateBuffers.valid)
				{
					stateBlock2.CopyToFrom(m_StateBuffers.m_PlayerStateBuffers.GetFrontBuffer(deviceIndex), defaultStateBuffer);
					stateBlock2.CopyToFrom(m_StateBuffers.m_PlayerStateBuffers.GetBackBuffer(deviceIndex), defaultStateBuffer);
				}
			}
		}

		private void OnNativeDeviceDiscovered(int deviceId, string deviceDescriptor)
		{
			RestoreDevicesAfterDomainReloadIfNecessary();
			global::UnityEngine.InputSystem.InputDevice inputDevice = TryMatchDisconnectedDevice(deviceDescriptor);
			global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription = inputDevice?.description ?? global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.FromJson(deviceDescriptor);
			bool isRemoved = false;
			try
			{
				if (m_Settings.supportedDevices.Count > 0)
				{
					global::UnityEngine.InputSystem.Utilities.InternedString layoutName = inputDevice?.m_Layout ?? TryFindMatchingControlLayout(ref deviceDescription, deviceId);
					if (!IsDeviceLayoutMarkedAsSupportedInSettings(layoutName))
					{
						isRemoved = true;
						return;
					}
				}
				if (inputDevice != null)
				{
					inputDevice.m_DeviceId = deviceId;
					inputDevice.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Native;
					inputDevice.m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInFrontend;
					inputDevice.m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledWhileInBackground;
					inputDevice.m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
					AddDevice(inputDevice);
					global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceChangeListeners, inputDevice, global::UnityEngine.InputSystem.InputDeviceChange.Reconnected, k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange");
				}
				else
				{
					AddDevice(deviceDescription, throwIfNoLayoutFound: false, null, deviceId, global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Native);
				}
			}
			catch (global::System.Exception arg)
			{
				global::UnityEngine.Debug.LogError($"Could not create a device for '{deviceDescription}' (exception: {arg})");
			}
			finally
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_AvailableDevices, ref m_AvailableDeviceCount, new global::UnityEngine.InputSystem.InputManager.AvailableDevice
				{
					description = deviceDescription,
					deviceId = deviceId,
					isNative = true,
					isRemoved = isRemoved
				});
			}
		}

		private global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString MakeEscapedJsonString(string theString)
		{
			if (string.IsNullOrEmpty(theString))
			{
				return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString
				{
					text = string.Empty,
					hasEscapes = false
				};
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			int length = theString.Length;
			bool hasEscapes = false;
			for (int i = 0; i < length; i++)
			{
				char c = theString[i];
				if (c == '\\' || c == '"')
				{
					stringBuilder.Append('\\');
					hasEscapes = true;
				}
				stringBuilder.Append(c);
			}
			return new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString
			{
				text = stringBuilder.ToString(),
				hasEscapes = hasEscapes
			};
		}

		private global::UnityEngine.InputSystem.InputDevice TryMatchDisconnectedDevice(string deviceDescriptor)
		{
			for (int i = 0; i < m_DisconnectedDevicesCount; i++)
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = m_DisconnectedDevices[i];
				global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description = inputDevice.description;
				if (global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.ComparePropertyToDeviceDescriptor("interface", description.interfaceName, deviceDescriptor) && global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.ComparePropertyToDeviceDescriptor("product", description.product, deviceDescriptor) && global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.ComparePropertyToDeviceDescriptor("manufacturer", description.manufacturer, deviceDescriptor) && global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.ComparePropertyToDeviceDescriptor("type", description.deviceClass, deviceDescriptor) && global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.ComparePropertyToDeviceDescriptor("capabilities", MakeEscapedJsonString(description.capabilities), deviceDescriptor) && global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.ComparePropertyToDeviceDescriptor("serial", description.serial, deviceDescriptor))
				{
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(m_DisconnectedDevices, ref m_DisconnectedDevicesCount, i);
					return inputDevice;
				}
			}
			return null;
		}

		private void InstallBeforeUpdateHookIfNecessary()
		{
			if (!m_NativeBeforeUpdateHooked && m_Runtime != null)
			{
				m_Runtime.onBeforeUpdate = OnBeforeUpdate;
				m_NativeBeforeUpdateHooked = true;
			}
		}

		private void RestoreDevicesAfterDomainReloadIfNecessary()
		{
		}

		private void WarnAboutDevicesFailingToRecreateAfterDomainReload()
		{
		}

		private void OnBeforeUpdate(global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			RestoreDevicesAfterDomainReloadIfNecessary();
			if ((updateType & m_UpdateMask) == 0)
			{
				return;
			}
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.SwitchTo(m_StateBuffers, updateType);
			global::UnityEngine.InputSystem.LowLevel.InputUpdate.OnBeforeUpdate(updateType);
			if (m_HaveDevicesWithStateCallbackReceivers && updateType != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender)
			{
				for (int i = 0; i < m_DevicesCount; i++)
				{
					global::UnityEngine.InputSystem.InputDevice inputDevice = m_Devices[i];
					if (inputDevice.hasStateCallbacks)
					{
						((global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver)inputDevice).OnNextUpdate();
					}
				}
			}
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_BeforeUpdateListeners, k_InputOnBeforeUpdateMarker, "InputSystem.onBeforeUpdate");
		}

		internal void ApplySettings()
		{
			global::UnityEngine.InputSystem.LowLevel.InputUpdateType inputUpdateType = global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Editor;
			if ((m_UpdateMask & global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender) != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None)
			{
				inputUpdateType |= global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender;
			}
			if (m_Settings.updateMode == (global::UnityEngine.InputSystem.InputSettings.UpdateMode)0)
			{
				m_Settings.updateMode = global::UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
			}
			updateMask = m_Settings.updateMode switch
			{
				global::UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsInDynamicUpdate => inputUpdateType | global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Dynamic, 
				global::UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsInFixedUpdate => inputUpdateType | global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Fixed, 
				global::UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsManually => inputUpdateType | global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Manual, 
				_ => throw new global::System.NotSupportedException("Invalid input update mode: " + m_Settings.updateMode), 
			};
			scrollDeltaBehavior = m_Settings.scrollDeltaBehavior;
			AddAvailableDevicesThatAreNowRecognized();
			if (settings.supportedDevices.Count > 0)
			{
				for (int i = 0; i < m_DevicesCount; i++)
				{
					global::UnityEngine.InputSystem.InputDevice inputDevice = m_Devices[i];
					global::UnityEngine.InputSystem.Utilities.InternedString layout = inputDevice.m_Layout;
					bool flag = false;
					for (int j = 0; j < m_AvailableDeviceCount; j++)
					{
						if (m_AvailableDevices[j].deviceId == inputDevice.deviceId)
						{
							flag = true;
							break;
						}
					}
					if (flag && !IsDeviceLayoutMarkedAsSupportedInSettings(layout))
					{
						RemoveDevice(inputDevice, keepOnListOfAvailableDevices: true);
						i--;
					}
				}
			}
			if (m_Settings.m_FeatureFlags != null)
			{
				m_ReadValueCachingFeatureEnabled = m_Settings.IsFeatureEnabled("USE_READ_VALUE_CACHING");
				m_OptimizedControlsFeatureEnabled = m_Settings.IsFeatureEnabled("USE_OPTIMIZED_CONTROLS");
				m_ParanoidReadValueCachingChecksEnabled = m_Settings.IsFeatureEnabled("PARANOID_READ_VALUE_CACHING_CHECKS");
			}
			global::UnityEngine.InputSystem.Touchscreen.s_TapTime = settings.defaultTapTime;
			global::UnityEngine.InputSystem.Touchscreen.s_TapDelayTime = settings.multiTapDelayTime;
			global::UnityEngine.InputSystem.Touchscreen.s_TapRadiusSquared = settings.tapRadius * settings.tapRadius;
			global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint = global::UnityEngine.Mathf.Clamp(settings.defaultButtonPressPoint, 0.0001f, float.MaxValue);
			global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonReleaseThreshold = settings.buttonReleaseThreshold;
			foreach (global::UnityEngine.InputSystem.InputDevice device in devices)
			{
				device.SetOptimizedControlDataTypeRecursively();
			}
			foreach (global::UnityEngine.InputSystem.InputDevice device2 in devices)
			{
				device2.MarkAsStaleRecursively();
			}
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_SettingsChangedListeners, k_InputOnSettingsChangeMarker, "InputSystem.onSettingsChange");
		}

		internal void ApplyActions()
		{
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_ActionsChangedListeners, k_InputOnActionsChangeMarker, "InputSystem.onActionsChange");
		}

		internal unsafe long ExecuteGlobalCommand<TCommand>(ref TCommand command) where TCommand : struct, global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
		{
			global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand* commandPtr = (global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref command);
			return global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.DeviceCommand(0, commandPtr);
		}

		internal void AddAvailableDevicesThatAreNowRecognized()
		{
			for (int i = 0; i < m_AvailableDeviceCount; i++)
			{
				int deviceId = m_AvailableDevices[i].deviceId;
				if (TryGetDeviceById(deviceId) != null)
				{
					continue;
				}
				global::UnityEngine.InputSystem.Utilities.InternedString internedString = TryFindMatchingControlLayout(ref m_AvailableDevices[i].description, deviceId);
				if (!IsDeviceLayoutMarkedAsSupportedInSettings(internedString))
				{
					continue;
				}
				if (internedString.IsEmpty())
				{
					if (deviceId != 0)
					{
						global::UnityEngine.InputSystem.LowLevel.DisableDeviceCommand command = global::UnityEngine.InputSystem.LowLevel.DisableDeviceCommand.Create();
						global::UnityEngine.InputSystem.LowLevel.InputRuntimeExtensions.DeviceCommand(m_Runtime, deviceId, ref command);
					}
				}
				else
				{
					try
					{
						AddDevice(m_AvailableDevices[i].description, internedString, null, deviceId, m_AvailableDevices[i].isNative ? global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Native : ((global::UnityEngine.InputSystem.InputDevice.DeviceFlags)0));
					}
					catch (global::System.Exception)
					{
					}
				}
			}
		}

		private bool ShouldRunDeviceInBackground(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (m_Settings.backgroundBehavior != global::UnityEngine.InputSystem.InputSettings.BackgroundBehavior.ResetAndDisableAllDevices)
			{
				return device.canRunInBackground;
			}
			return false;
		}

		internal void OnFocusChanged(bool focus)
		{
			bool runInBackground = m_Runtime.runInBackground;
			if (m_Settings.backgroundBehavior == global::UnityEngine.InputSystem.InputSettings.BackgroundBehavior.IgnoreFocus && runInBackground)
			{
				m_HasFocus = focus;
				return;
			}
			if (!focus)
			{
				if (runInBackground)
				{
					for (int i = 0; i < m_DevicesCount; i++)
					{
						global::UnityEngine.InputSystem.InputDevice inputDevice = m_Devices[i];
						if (inputDevice.enabled && !ShouldRunDeviceInBackground(inputDevice))
						{
							EnableOrDisableDevice(inputDevice, enable: false, global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground);
							int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(m_Devices, inputDevice, m_DevicesCount);
							i = ((num != -1) ? num : (i - 1));
						}
					}
				}
			}
			else
			{
				m_DiscardOutOfFocusEvents = true;
				m_FocusRegainedTime = m_Runtime.currentTime;
				for (int j = 0; j < m_DevicesCount; j++)
				{
					global::UnityEngine.InputSystem.InputDevice inputDevice2 = m_Devices[j];
					if (inputDevice2.disabledWhileInBackground)
					{
						EnableOrDisableDevice(inputDevice2, enable: true, global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground);
					}
					else if (inputDevice2.enabled && !runInBackground && !inputDevice2.RequestSync())
					{
						ResetDevice(inputDevice2);
					}
				}
			}
			m_HasFocus = focus;
		}

		internal bool ShouldRunUpdate(global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			if (updateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None)
			{
				return true;
			}
			global::UnityEngine.InputSystem.LowLevel.InputUpdateType inputUpdateType = m_UpdateMask;
			return (updateType & inputUpdateType) != 0;
		}

		private unsafe void OnUpdate(global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType, ref global::UnityEngine.InputSystem.LowLevel.InputEventBuffer eventBuffer)
		{
			if (m_InputEventStream.isOpen)
			{
				throw new global::System.InvalidOperationException("Already have an event buffer set! Was OnUpdate() called recursively?");
			}
			RestoreDevicesAfterDomainReloadIfNecessary();
			if ((updateType & m_UpdateMask) == 0)
			{
				return;
			}
			WarnAboutDevicesFailingToRecreateAfterDomainReload();
			ref global::UnityEngine.InputSystem.LowLevel.InputMetrics reference = ref m_Metrics;
			int totalUpdateCount = reference.totalUpdateCount + 1;
			reference.totalUpdateCount = totalUpdateCount;
			global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup = m_Runtime.currentTimeOffsetToRealtimeSinceStartup;
			global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.SwitchTo(m_StateBuffers, updateType);
			m_CurrentUpdate = updateType;
			global::UnityEngine.InputSystem.LowLevel.InputUpdate.OnUpdate(updateType);
			bool flag = global::UnityEngine.InputSystem.LowLevel.InputUpdate.IsPlayerUpdate(updateType) && gameIsPlaying;
			double num = ((updateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Fixed) ? m_Runtime.currentTimeForFixedUpdate : m_Runtime.currentTime);
			bool flag2 = (updateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Fixed || updateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender) && global::UnityEngine.InputSystem.InputSystem.settings.updateMode == global::UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
			bool flag3 = ShouldFlushEventBuffer();
			if (eventBuffer.eventCount == 0 || flag3 || ShouldExitEarlyFromEventProcessing(updateType))
			{
				if (flag)
				{
					ProcessStateChangeMonitorTimeouts();
				}
				InvokeAfterUpdateCallback(updateType);
				if (flag3)
				{
					eventBuffer.Reset();
				}
				m_CurrentUpdate = global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None;
				return;
			}
			long timestamp = global::System.Diagnostics.Stopwatch.GetTimestamp();
			double num2 = 0.0;
			try
			{
				m_InputEventStream = new global::UnityEngine.InputSystem.LowLevel.InputEventStream(ref eventBuffer, m_Settings.maxQueuedEventsPerUpdate);
				uint num3 = 0u;
				global::UnityEngine.InputSystem.LowLevel.InputEvent* ptr = null;
				while (m_InputEventStream.remainingEventCount > 0)
				{
					global::UnityEngine.InputSystem.InputDevice inputDevice = null;
					global::UnityEngine.InputSystem.LowLevel.InputEvent* ptr2 = m_InputEventStream.currentEventPtr;
					if (updateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender)
					{
						while (m_InputEventStream.remainingEventCount > 0)
						{
							inputDevice = TryGetDeviceById(ptr2->deviceId);
							if (inputDevice != null && inputDevice.updateBeforeRender && (ptr2->type == 1398030676 || ptr2->type == 1145852993))
							{
								break;
							}
							ptr2 = m_InputEventStream.Advance(leaveEventInBuffer: true);
						}
					}
					if (m_InputEventStream.remainingEventCount == 0)
					{
						break;
					}
					double internalTime = ptr2->internalTime;
					global::UnityEngine.InputSystem.Utilities.FourCC type = ptr2->type;
					if (flag2 && internalTime >= num)
					{
						m_InputEventStream.Advance(leaveEventInBuffer: true);
						continue;
					}
					if (inputDevice == null)
					{
						inputDevice = TryGetDeviceById(ptr2->deviceId);
					}
					if (inputDevice == null)
					{
						m_InputEventStream.Advance(leaveEventInBuffer: false);
						continue;
					}
					if (!inputDevice.enabled && type != 1146242381 && type != 1145259591 && (inputDevice.m_DeviceFlags & (global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInRuntime | global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledWhileInBackground)) != 0)
					{
						m_InputEventStream.Advance(leaveEventInBuffer: false);
						continue;
					}
					if (!settings.disableRedundantEventsMerging && inputDevice.hasEventMerger && ptr2 != ptr)
					{
						global::UnityEngine.InputSystem.LowLevel.InputEvent* ptr3 = m_InputEventStream.Peek();
						if (ptr3 != null && ptr2->deviceId == ptr3->deviceId && (!flag2 || ptr3->internalTime < num))
						{
							if (((global::UnityEngine.InputSystem.LowLevel.IEventMerger)inputDevice).MergeForward(ptr2, ptr3))
							{
								m_InputEventStream.Advance(leaveEventInBuffer: false);
								continue;
							}
							ptr = ptr3;
						}
					}
					if (inputDevice.hasEventPreProcessor && !((global::UnityEngine.InputSystem.LowLevel.IEventPreProcessor)inputDevice).PreProcessEvent(ptr2))
					{
						m_InputEventStream.Advance(leaveEventInBuffer: false);
						continue;
					}
					if (m_EventListeners.length > 0)
					{
						global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_EventListeners, new global::UnityEngine.InputSystem.LowLevel.InputEventPtr(ptr2), inputDevice, k_InputOnEventMarker, "InputSystem.onEvent");
						if (m_InputEventHandledPolicy == global::UnityEngine.InputSystem.LowLevel.InputEventHandledPolicy.SuppressStateUpdates && ptr2->handled)
						{
							m_InputEventStream.Advance(leaveEventInBuffer: false);
							continue;
						}
					}
					if (internalTime <= num)
					{
						num2 += num - internalTime;
					}
					ref global::UnityEngine.InputSystem.LowLevel.InputMetrics reference2 = ref m_Metrics;
					totalUpdateCount = reference2.totalEventCount + 1;
					reference2.totalEventCount = totalUpdateCount;
					m_Metrics.totalEventBytes += (int)ptr2->sizeInBytes;
					switch (type)
					{
					case 1145852993:
					case 1398030676:
					{
						global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEventPtr = new global::UnityEngine.InputSystem.LowLevel.InputEventPtr(ptr2);
						bool hasStateCallbacks = inputDevice.hasStateCallbacks;
						if (internalTime < inputDevice.m_LastUpdateTimeInternal && (!hasStateCallbacks || !(inputDevice.stateBlock.format != inputEventPtr.stateFormat)))
						{
							break;
						}
						bool flag4 = true;
						if (hasStateCallbacks)
						{
							m_ShouldMakeCurrentlyUpdatingDeviceCurrent = true;
							((global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver)inputDevice).OnStateEvent(inputEventPtr);
							flag4 = m_ShouldMakeCurrentlyUpdatingDeviceCurrent;
						}
						else
						{
							if (inputDevice.stateBlock.format != inputEventPtr.stateFormat)
							{
								break;
							}
							flag4 = UpdateState(inputDevice, inputEventPtr, updateType);
						}
						num3 += inputEventPtr.sizeInBytes;
						inputDevice.m_CurrentProcessedEventBytesOnUpdate += inputEventPtr.sizeInBytes;
						if (inputDevice.m_LastUpdateTimeInternal <= inputEventPtr.internalTime)
						{
							inputDevice.m_LastUpdateTimeInternal = inputEventPtr.internalTime;
						}
						if (flag4)
						{
							inputDevice.MakeCurrent();
						}
						break;
					}
					case 1413830740:
					{
						global::UnityEngine.InputSystem.LowLevel.TextEvent* ptr4 = (global::UnityEngine.InputSystem.LowLevel.TextEvent*)ptr2;
						if (inputDevice is global::UnityEngine.InputSystem.LowLevel.ITextInputReceiver textInputReceiver)
						{
							int character = ptr4->character;
							if (character >= 65536)
							{
								character -= 65536;
								int num4 = 55296 + ((character >> 10) & 0x3FF);
								int num5 = 56320 + (character & 0x3FF);
								textInputReceiver.OnTextInput((char)num4);
								textInputReceiver.OnTextInput((char)num5);
							}
							else
							{
								textInputReceiver.OnTextInput((char)character);
							}
						}
						break;
					}
					case 1229800787:
					{
						global::UnityEngine.InputSystem.LowLevel.IMECompositionEvent* ptr5 = (global::UnityEngine.InputSystem.LowLevel.IMECompositionEvent*)ptr2;
						(inputDevice as global::UnityEngine.InputSystem.LowLevel.ITextInputReceiver)?.OnIMECompositionChanged(ptr5->compositionString);
						break;
					}
					case 1146242381:
						RemoveDevice(inputDevice);
						if (inputDevice.native && !inputDevice.description.empty)
						{
							global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_DisconnectedDevices, ref m_DisconnectedDevicesCount, inputDevice);
							global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceChangeListeners, inputDevice, global::UnityEngine.InputSystem.InputDeviceChange.Disconnected, k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange");
						}
						break;
					case 1145259591:
						inputDevice.NotifyConfigurationChanged();
						global::UnityEngine.InputSystem.InputActionState.OnDeviceChange(inputDevice, global::UnityEngine.InputSystem.InputDeviceChange.ConfigurationChanged);
						global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceChangeListeners, inputDevice, global::UnityEngine.InputSystem.InputDeviceChange.ConfigurationChanged, k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange");
						break;
					case 1146245972:
						ResetDevice(inputDevice, ((global::UnityEngine.InputSystem.LowLevel.DeviceResetEvent*)ptr2)->hardReset);
						break;
					}
					m_InputEventStream.Advance(leaveEventInBuffer: false);
					if (AreMaximumEventBytesPerUpdateExceeded(num3))
					{
						break;
					}
				}
				m_Metrics.totalEventProcessingTime += (double)(global::System.Diagnostics.Stopwatch.GetTimestamp() - timestamp) / (double)global::System.Diagnostics.Stopwatch.Frequency;
				m_Metrics.totalEventLagTime += num2;
				ResetCurrentProcessedEventBytesForDevices();
				m_InputEventStream.Close(ref eventBuffer);
			}
			catch (global::System.Exception)
			{
				m_InputEventStream.CleanUpAfterException();
				throw;
			}
			m_DiscardOutOfFocusEvents = false;
			if (flag)
			{
				ProcessStateChangeMonitorTimeouts();
			}
			InvokeAfterUpdateCallback(updateType);
			m_CurrentUpdate = global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None;
		}

		private bool ShouldFlushEventBuffer()
		{
			if (!gameHasFocus && !m_Runtime.runInBackground)
			{
				return true;
			}
			return false;
		}

		private bool ShouldExitEarlyFromEventProcessing(global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			return false;
		}

		private bool AreMaximumEventBytesPerUpdateExceeded(uint totalEventBytesProcessed)
		{
			if (m_Settings.maxEventBytesPerUpdate > 0 && totalEventBytesProcessed >= m_Settings.maxEventBytesPerUpdate)
			{
				string text = string.Empty;
				if (global::UnityEngine.Debug.isDebugBuild)
				{
					text = "Total events processed by devices in last update call:\n" + MakeStringWithEventsProcessedByDevice();
				}
				global::UnityEngine.Debug.LogError("Exceeded budget for maximum input event throughput per InputSystem.Update(). Discarding remaining events. Increase InputSystem.settings.maxEventBytesPerUpdate or set it to 0 to remove the limit.\n" + text);
				return true;
			}
			return false;
		}

		private string MakeStringWithEventsProcessedByDevice()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (int i = 0; i < m_DevicesCount; i++)
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = devices[i];
				if (inputDevice != null && inputDevice.m_CurrentProcessedEventBytesOnUpdate != 0)
				{
					stringBuilder.Append($" - {inputDevice.m_CurrentProcessedEventBytesOnUpdate} bytes processed by {inputDevice}\n");
				}
			}
			return stringBuilder.ToString();
		}

		private void ResetCurrentProcessedEventBytesForDevices()
		{
			if (!global::UnityEngine.Debug.isDebugBuild)
			{
				return;
			}
			for (int i = 0; i < m_DevicesCount; i++)
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = m_Devices[i];
				if (inputDevice != null && inputDevice.m_CurrentProcessedEventBytesOnUpdate != 0)
				{
					inputDevice.m_CurrentProcessedEventBytesOnUpdate = 0u;
				}
			}
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckAllDevicesOptimizedControlsHaveValidState()
		{
			if (!global::UnityEngine.InputSystem.InputSystem.s_Manager.m_OptimizedControlsFeatureEnabled)
			{
				return;
			}
			foreach (global::UnityEngine.InputSystem.InputDevice device in devices)
			{
				_ = device;
			}
		}

		private void InvokeAfterUpdateCallback(global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			if (updateType != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Editor || !gameIsPlaying)
			{
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_AfterUpdateListeners, k_InputOnAfterUpdateMarker, "InputSystem.onAfterUpdate");
			}
		}

		internal void DontMakeCurrentlyUpdatingDeviceCurrent()
		{
			m_ShouldMakeCurrentlyUpdatingDeviceCurrent = false;
		}

		internal unsafe bool UpdateState(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr, global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = device.m_StateBlock;
			uint num = stateBlock.sizeInBits / 8;
			uint num2 = 0u;
			byte* statePtr;
			uint num3;
			if (eventPtr->type == 1398030676)
			{
				_ = *(global::UnityEngine.InputSystem.LowLevel.StateEvent*)eventPtr;
				uint stateSizeInBytes = ((global::UnityEngine.InputSystem.LowLevel.StateEvent*)eventPtr)->stateSizeInBytes;
				statePtr = (byte*)((global::UnityEngine.InputSystem.LowLevel.StateEvent*)eventPtr)->state;
				num3 = stateSizeInBytes;
				if (num3 > num)
				{
					num3 = num;
				}
			}
			else
			{
				_ = *(global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent*)eventPtr;
				uint deltaStateSizeInBytes = ((global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent*)eventPtr)->deltaStateSizeInBytes;
				statePtr = (byte*)((global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent*)eventPtr)->deltaState;
				num2 = ((global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent*)eventPtr)->stateOffset;
				num3 = deltaStateSizeInBytes;
				if (num2 + num3 > num)
				{
					if (num2 >= num)
					{
						return false;
					}
					num3 = num - num2;
				}
			}
			return UpdateState(device, updateType, statePtr, num2, num3, eventPtr->internalTime, eventPtr);
		}

		internal unsafe bool UpdateState(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType, void* statePtr, uint stateOffsetInDevice, uint stateSize, double internalTime, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr))
		{
			int deviceIndex = device.m_DeviceIndex;
			ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = ref device.m_StateBlock;
			byte* frontBufferForDevice = (byte*)global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.GetFrontBufferForDevice(deviceIndex);
			SortStateChangeMonitorsIfNecessary(deviceIndex);
			bool flag = ProcessStateChangeMonitors(deviceIndex, statePtr, frontBufferForDevice + stateBlock.byteOffset, stateSize, stateOffsetInDevice);
			uint num = device.m_StateBlock.byteOffset + stateOffsetInDevice;
			byte* ptr = frontBufferForDevice + num;
			byte* mask = (device.noisy ? ((byte*)global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_NoiseMaskBuffer + num) : null);
			bool flag2 = !global::UnityEngine.InputSystem.Utilities.MemoryHelpers.MemCmpBitRegion(ptr, statePtr, 0u, stateSize * 8, mask);
			bool flippedBuffers = FlipBuffersForDeviceIfNecessary(device, updateType);
			WriteStateChange(m_StateBuffers.m_PlayerStateBuffers, deviceIndex, ref stateBlock, stateOffsetInDevice, statePtr, stateSize, flippedBuffers);
			if (flag2)
			{
				if (global::UnityEngine.InputSystem.InputSystem.s_Manager.m_ReadValueCachingFeatureEnabled || device.m_UseCachePathForButtonPresses)
				{
					foreach (int updatedButton in device.m_UpdatedButtons)
					{
						((global::UnityEngine.InputSystem.Controls.ButtonControl)device.allControls[updatedButton]).UpdateWasPressed();
					}
				}
				else
				{
					int num2 = 0;
					foreach (global::UnityEngine.InputSystem.Controls.ButtonControl item in device.m_ButtonControlsCheckingPressState)
					{
						item.UpdateWasPressed();
						num2++;
					}
					if (num2 > 45)
					{
						device.m_UseCachePathForButtonPresses = true;
					}
				}
			}
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref m_DeviceStateChangeListeners, device, eventPtr, k_InputOnDeviceSettingsChangeMarker, "InputSystem.onDeviceStateChange");
			if (flag)
			{
				FireStateChangeNotifications(deviceIndex, internalTime, eventPtr);
			}
			return flag2;
		}

		private unsafe void WriteStateChange(global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.DoubleBuffers buffers, int deviceIndex, ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock deviceStateBlock, uint stateOffsetInDevice, void* statePtr, uint stateSizeInBytes, bool flippedBuffers)
		{
			void* frontBuffer = buffers.GetFrontBuffer(deviceIndex);
			uint num = deviceStateBlock.sizeInBits / 8;
			if (flippedBuffers && num != stateSizeInBytes)
			{
				void* backBuffer = buffers.GetBackBuffer(deviceIndex);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy((byte*)frontBuffer + deviceStateBlock.byteOffset, (byte*)backBuffer + deviceStateBlock.byteOffset, num);
			}
			if (global::UnityEngine.InputSystem.InputSystem.s_Manager.m_ReadValueCachingFeatureEnabled || m_Devices[deviceIndex].m_UseCachePathForButtonPresses)
			{
				byte* ptr = (byte*)frontBuffer;
				if (flippedBuffers && num == stateSizeInBytes)
				{
					ptr = (byte*)buffers.GetBackBuffer(deviceIndex);
				}
				m_Devices[deviceIndex].WriteChangedControlStates(ptr + deviceStateBlock.byteOffset, statePtr, stateSizeInBytes, stateOffsetInDevice);
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy((byte*)frontBuffer + deviceStateBlock.byteOffset + stateOffsetInDevice, statePtr, stateSizeInBytes);
		}

		private bool FlipBuffersForDeviceIfNecessary(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			if (updateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender)
			{
				return false;
			}
			if (device.m_CurrentUpdateStepCount != global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount)
			{
				m_StateBuffers.m_PlayerStateBuffers.SwapBuffers(device.m_DeviceIndex);
				device.m_CurrentUpdateStepCount = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
				return true;
			}
			return false;
		}

		public void AddStateChangeMonitor(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, long monitorIndex, uint groupIndex)
		{
			if (m_DevicesCount > 0)
			{
				int deviceIndex = control.device.m_DeviceIndex;
				if (m_StateChangeMonitors == null)
				{
					m_StateChangeMonitors = new global::UnityEngine.InputSystem.InputManager.StateChangeMonitorsForDevice[m_DevicesCount];
				}
				else if (m_StateChangeMonitors.Length <= deviceIndex)
				{
					global::System.Array.Resize(ref m_StateChangeMonitors, m_DevicesCount);
				}
				if (!isProcessingEvents && m_StateChangeMonitors[deviceIndex].needToCompactArrays)
				{
					m_StateChangeMonitors[deviceIndex].CompactArrays();
				}
				m_StateChangeMonitors[deviceIndex].Add(control, monitor, monitorIndex, groupIndex);
			}
		}

		private void RemoveStateChangeMonitors(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (m_StateChangeMonitors == null)
			{
				return;
			}
			int deviceIndex = device.m_DeviceIndex;
			if (deviceIndex >= m_StateChangeMonitors.Length)
			{
				return;
			}
			m_StateChangeMonitors[deviceIndex].Clear();
			for (int i = 0; i < m_StateChangeMonitorTimeouts.length; i++)
			{
				if (m_StateChangeMonitorTimeouts[i].control?.device == device)
				{
					m_StateChangeMonitorTimeouts[i] = default(global::UnityEngine.InputSystem.InputManager.StateChangeMonitorTimeout);
				}
			}
		}

		public void RemoveStateChangeMonitor(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, long monitorIndex)
		{
			if (m_StateChangeMonitors == null)
			{
				return;
			}
			int deviceIndex = control.device.m_DeviceIndex;
			if (deviceIndex == -1 || deviceIndex >= m_StateChangeMonitors.Length)
			{
				return;
			}
			m_StateChangeMonitors[deviceIndex].Remove(monitor, monitorIndex, isProcessingEvents);
			for (int i = 0; i < m_StateChangeMonitorTimeouts.length; i++)
			{
				if (m_StateChangeMonitorTimeouts[i].monitor == monitor && m_StateChangeMonitorTimeouts[i].monitorIndex == monitorIndex)
				{
					m_StateChangeMonitorTimeouts[i] = default(global::UnityEngine.InputSystem.InputManager.StateChangeMonitorTimeout);
				}
			}
		}

		public void AddStateChangeMonitorTimeout(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, double time, long monitorIndex, int timerIndex)
		{
			m_StateChangeMonitorTimeouts.Append(new global::UnityEngine.InputSystem.InputManager.StateChangeMonitorTimeout
			{
				control = control,
				time = time,
				monitor = monitor,
				monitorIndex = monitorIndex,
				timerIndex = timerIndex
			});
		}

		public void RemoveStateChangeMonitorTimeout(global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor, long monitorIndex, int timerIndex)
		{
			int length = m_StateChangeMonitorTimeouts.length;
			for (int i = 0; i < length; i++)
			{
				if (m_StateChangeMonitorTimeouts[i].monitor == monitor && m_StateChangeMonitorTimeouts[i].monitorIndex == monitorIndex && m_StateChangeMonitorTimeouts[i].timerIndex == timerIndex)
				{
					m_StateChangeMonitorTimeouts[i] = default(global::UnityEngine.InputSystem.InputManager.StateChangeMonitorTimeout);
					break;
				}
			}
		}

		private void SortStateChangeMonitorsIfNecessary(int deviceIndex)
		{
			if (m_StateChangeMonitors != null && deviceIndex < m_StateChangeMonitors.Length && m_StateChangeMonitors[deviceIndex].needToUpdateOrderingOfMonitors)
			{
				m_StateChangeMonitors[deviceIndex].SortMonitorsByIndex();
			}
		}

		public void SignalStateChangeMonitor(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor monitor)
		{
			int deviceIndex = control.device.m_DeviceIndex;
			ref global::UnityEngine.InputSystem.InputManager.StateChangeMonitorsForDevice reference = ref m_StateChangeMonitors[deviceIndex];
			for (int i = 0; i < reference.signalled.length; i++)
			{
				SortStateChangeMonitorsIfNecessary(i);
				ref global::UnityEngine.InputSystem.InputManager.StateChangeMonitorListener reference2 = ref reference.listeners[i];
				if (reference2.control == control && reference2.monitor == monitor)
				{
					reference.signalled.SetBit(i);
				}
			}
		}

		public unsafe void FireStateChangeNotifications()
		{
			double currentTime = m_Runtime.currentTime;
			int num = global::System.Math.Min(global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_StateChangeMonitors), m_DevicesCount);
			for (int i = 0; i < num; i++)
			{
				FireStateChangeNotifications(i, currentTime, null);
			}
		}

		private unsafe bool ProcessStateChangeMonitors(int deviceIndex, void* newStateFromEvent, void* oldStateOfDevice, uint newStateSizeInBytes, uint newStateOffsetInBytes)
		{
			if (m_StateChangeMonitors == null)
			{
				return false;
			}
			if (deviceIndex >= m_StateChangeMonitors.Length)
			{
				return false;
			}
			global::UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion[] memoryRegions = m_StateChangeMonitors[deviceIndex].memoryRegions;
			if (memoryRegions == null)
			{
				return false;
			}
			int num = m_StateChangeMonitors[deviceIndex].count;
			bool result = false;
			global::UnityEngine.InputSystem.DynamicBitfield signalled = m_StateChangeMonitors[deviceIndex].signalled;
			bool flag = false;
			global::UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion bitRegion = new global::UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion(newStateOffsetInBytes, 0u, newStateSizeInBytes * 8);
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion other = memoryRegions[i];
				if (other.sizeInBits == 0)
				{
					int count = num;
					int count2 = num;
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(m_StateChangeMonitors[deviceIndex].listeners, ref count, i);
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(memoryRegions, ref count2, i);
					signalled.SetLength(num - 1);
					flag = true;
					num--;
					i--;
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.BitRegion region = bitRegion.Overlap(other);
					if (!region.isEmpty && !global::UnityEngine.InputSystem.Utilities.MemoryHelpers.Compare(oldStateOfDevice, (byte*)newStateFromEvent - newStateOffsetInBytes, region))
					{
						signalled.SetBit(i);
						flag = true;
						result = true;
					}
				}
			}
			if (flag)
			{
				m_StateChangeMonitors[deviceIndex].signalled = signalled;
			}
			m_StateChangeMonitors[deviceIndex].needToCompactArrays = false;
			return result;
		}

		internal unsafe void FireStateChangeNotifications(int deviceIndex, double internalTime, global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
		{
			if (m_StateChangeMonitors == null || m_StateChangeMonitors.Length <= deviceIndex)
			{
				return;
			}
			ref global::UnityEngine.InputSystem.DynamicBitfield signalled = ref m_StateChangeMonitors[deviceIndex].signalled;
			if (signalled.AnyBitIsSet() && m_StateChangeMonitors[deviceIndex].listeners == null)
			{
				return;
			}
			ref global::UnityEngine.InputSystem.InputManager.StateChangeMonitorListener[] listeners = ref m_StateChangeMonitors[deviceIndex].listeners;
			double time = internalTime - global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			global::UnityEngine.InputSystem.LowLevel.InputEvent output = new global::UnityEngine.InputSystem.LowLevel.InputEvent(new global::UnityEngine.InputSystem.Utilities.FourCC('F', 'A', 'K', 'E'), 20, -1, internalTime);
			if (eventPtr == null)
			{
				eventPtr = (global::UnityEngine.InputSystem.LowLevel.InputEvent*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output);
			}
			bool handled = eventPtr->handled;
			for (int i = 0; i < signalled.length; i++)
			{
				if (!signalled.TestBit(i))
				{
					continue;
				}
				global::UnityEngine.InputSystem.InputManager.StateChangeMonitorListener stateChangeMonitorListener = listeners[i];
				try
				{
					stateChangeMonitorListener.monitor.NotifyControlStateChanged(stateChangeMonitorListener.control, time, eventPtr, stateChangeMonitorListener.monitorIndex);
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogError($"Exception '{ex.GetType().Name}' thrown from state change monitor '{stateChangeMonitorListener.monitor.GetType().Name}' on '{stateChangeMonitorListener.control}'");
					global::UnityEngine.Debug.LogException(ex);
				}
				if (!handled && eventPtr->handled)
				{
					uint groupIndex = listeners[i].groupIndex;
					for (int j = i + 1; j < signalled.length; j++)
					{
						if (listeners[j].groupIndex == groupIndex && listeners[j].monitor == stateChangeMonitorListener.monitor)
						{
							signalled.ClearBit(j);
						}
					}
				}
				if (eventPtr->handled)
				{
					eventPtr->handled = handled;
				}
				signalled.ClearBit(i);
			}
		}

		private void ProcessStateChangeMonitorTimeouts()
		{
			if (m_StateChangeMonitorTimeouts.length == 0)
			{
				return;
			}
			double num = m_Runtime.currentTime - global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			int num2 = 0;
			for (int i = 0; i < m_StateChangeMonitorTimeouts.length; i++)
			{
				if (m_StateChangeMonitorTimeouts[i].control == null)
				{
					continue;
				}
				if (m_StateChangeMonitorTimeouts[i].time <= num)
				{
					global::UnityEngine.InputSystem.InputManager.StateChangeMonitorTimeout stateChangeMonitorTimeout = m_StateChangeMonitorTimeouts[i];
					stateChangeMonitorTimeout.monitor.NotifyTimerExpired(stateChangeMonitorTimeout.control, num, stateChangeMonitorTimeout.monitorIndex, stateChangeMonitorTimeout.timerIndex);
					continue;
				}
				if (i != num2)
				{
					m_StateChangeMonitorTimeouts[num2] = m_StateChangeMonitorTimeouts[i];
				}
				num2++;
			}
			m_StateChangeMonitorTimeouts.SetLength(num2);
		}
	}
}
