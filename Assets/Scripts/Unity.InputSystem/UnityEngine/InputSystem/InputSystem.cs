namespace UnityEngine.InputSystem
{
	public static class InputSystem
	{
		private struct StateEventBuffer
		{
			public global::UnityEngine.InputSystem.LowLevel.StateEvent stateEvent;

			public const int kMaxSize = 512;

			public unsafe fixed byte data[511];
		}

		private struct DeltaStateEventBuffer
		{
			public global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent stateEvent;

			public const int kMaxSize = 512;

			public unsafe fixed byte data[511];
		}

		internal const string kAssemblyVersion = "1.18.0";

		internal const string kDocUrl = "https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18";

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputResetMarker;

		internal static global::UnityEngine.InputSystem.InputManager s_Manager;

		internal static global::UnityEngine.InputSystem.InputRemoting s_Remote;

		public static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> devices => s_Manager.devices;

		public static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> disconnectedDevices => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>(s_Manager.m_DisconnectedDevices, 0, s_Manager.m_DisconnectedDevicesCount);

		public static float pollingFrequency
		{
			get
			{
				return s_Manager.pollingFrequency;
			}
			set
			{
				s_Manager.pollingFrequency = value;
			}
		}

		internal static bool isProcessingEvents => s_Manager.isProcessingEvents;

		public static global::UnityEngine.InputSystem.LowLevel.InputEventListener onEvent
		{
			get
			{
				return default(global::UnityEngine.InputSystem.LowLevel.InputEventListener);
			}
			set
			{
			}
		}

		public static global::System.IObservable<global::UnityEngine.InputSystem.InputControl> onAnyButtonPress => global::UnityEngine.InputSystem.Utilities.Observable.Where(global::UnityEngine.InputSystem.Utilities.Observable.Select(onEvent, (global::UnityEngine.InputSystem.LowLevel.InputEventPtr e) => e.GetFirstButtonPressOrNull()), (global::UnityEngine.InputSystem.InputControl c) => c != null);

		public static global::UnityEngine.InputSystem.InputSettings settings
		{
			get
			{
				return s_Manager.settings;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				if (!(s_Manager.m_Settings == value))
				{
					s_Manager.settings = value;
				}
			}
		}

		public static global::UnityEngine.InputSystem.InputActionAsset actions
		{
			get
			{
				return s_Manager?.actions;
			}
			set
			{
				if (global::UnityEngine.Application.isPlaying)
				{
					throw new global::System.Exception("Attempted to set property InputSystem.actions during Play-mode which is not supported. Assigning this property is only allowed in Edit-mode.");
				}
				if ((object)s_Manager.actions != value)
				{
					_ = value != null;
					s_Manager.actions = value;
				}
			}
		}

		public static global::UnityEngine.InputSystem.InputRemoting remoting => s_Remote;

		public static global::System.Version version => new global::System.Version("1.18.0");

		public static bool runInBackground
		{
			get
			{
				return s_Manager.m_Runtime.runInBackground;
			}
			set
			{
				s_Manager.m_Runtime.runInBackground = value;
			}
		}

		internal static float scrollWheelDeltaPerTick => global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.scrollWheelDeltaPerTick;

		public static global::UnityEngine.InputSystem.LowLevel.InputMetrics metrics => s_Manager.metrics;

		public static event global::System.Action<string, global::UnityEngine.InputSystem.InputControlLayoutChange> onLayoutChange
		{
			add
			{
				lock (s_Manager)
				{
					s_Manager.onLayoutChange += value;
				}
			}
			remove
			{
				lock (s_Manager)
				{
					s_Manager.onLayoutChange -= value;
				}
			}
		}

		public static event global::System.Action<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.InputDeviceChange> onDeviceChange
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				lock (s_Manager)
				{
					s_Manager.onDeviceChange += value;
				}
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				lock (s_Manager)
				{
					s_Manager.onDeviceChange -= value;
				}
			}
		}

		public static event global::UnityEngine.InputSystem.LowLevel.InputDeviceCommandDelegate onDeviceCommand
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				lock (s_Manager)
				{
					s_Manager.onDeviceCommand += value;
				}
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				lock (s_Manager)
				{
					s_Manager.onDeviceCommand -= value;
				}
			}
		}

		public static event global::UnityEngine.InputSystem.Layouts.InputDeviceFindControlLayoutDelegate onFindLayoutForDevice
		{
			add
			{
				lock (s_Manager)
				{
					s_Manager.onFindControlLayoutForDevice += value;
				}
			}
			remove
			{
				lock (s_Manager)
				{
					s_Manager.onFindControlLayoutForDevice -= value;
				}
			}
		}

		public static event global::System.Action onBeforeUpdate
		{
			add
			{
				lock (s_Manager)
				{
					s_Manager.onBeforeUpdate += value;
				}
			}
			remove
			{
				lock (s_Manager)
				{
					s_Manager.onBeforeUpdate -= value;
				}
			}
		}

		public static event global::System.Action onAfterUpdate
		{
			add
			{
				lock (s_Manager)
				{
					s_Manager.onAfterUpdate += value;
				}
			}
			remove
			{
				lock (s_Manager)
				{
					s_Manager.onAfterUpdate -= value;
				}
			}
		}

		public static event global::System.Action onSettingsChange
		{
			add
			{
				s_Manager.onSettingsChange += value;
			}
			remove
			{
				s_Manager.onSettingsChange -= value;
			}
		}

		public static event global::System.Action onActionsChange
		{
			add
			{
				s_Manager.onActionsChange += value;
			}
			remove
			{
				s_Manager.onActionsChange -= value;
			}
		}

		public static event global::System.Action<object, global::UnityEngine.InputSystem.InputActionChange> onActionChange
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				global::UnityEngine.InputSystem.InputActionState.s_GlobalState.onActionChange.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				global::UnityEngine.InputSystem.InputActionState.s_GlobalState.onActionChange.RemoveCallback(value);
			}
		}

		public static void RegisterLayout(global::System.Type type, string name = null, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher? matches = null)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
			}
			s_Manager.RegisterControlLayout(name, type);
			if (matches.HasValue)
			{
				s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		public static void RegisterLayout<T>(string name = null, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher? matches = null) where T : global::UnityEngine.InputSystem.InputControl
		{
			RegisterLayout(typeof(T), name, matches);
		}

		public static void RegisterLayout(string json, string name = null, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher? matches = null)
		{
			s_Manager.RegisterControlLayout(json, name);
			if (matches.HasValue)
			{
				s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		public static void RegisterLayoutOverride(string json, string name = null)
		{
			s_Manager.RegisterControlLayout(json, name, isOverride: true);
		}

		public static void RegisterLayoutMatcher(string layoutName, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
		{
			s_Manager.RegisterControlLayoutMatcher(layoutName, matcher);
		}

		public static void RegisterLayoutMatcher<TDevice>(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher) where TDevice : global::UnityEngine.InputSystem.InputDevice
		{
			s_Manager.RegisterControlLayoutMatcher(typeof(TDevice), matcher);
		}

		public static void RegisterLayoutBuilder(global::System.Func<global::UnityEngine.InputSystem.Layouts.InputControlLayout> buildMethod, string name, string baseLayout = null, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher? matches = null)
		{
			if (buildMethod == null)
			{
				throw new global::System.ArgumentNullException("buildMethod");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			s_Manager.RegisterControlLayoutBuilder(buildMethod, name, baseLayout);
			if (matches.HasValue)
			{
				s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		public static void RegisterPrecompiledLayout<TDevice>(string metadata) where TDevice : global::UnityEngine.InputSystem.InputDevice, new()
		{
			s_Manager.RegisterPrecompiledLayout<TDevice>(metadata);
		}

		public static void RemoveLayout(string name)
		{
			s_Manager.RemoveControlLayout(name);
		}

		public static string TryFindMatchingLayout(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription)
		{
			return s_Manager.TryFindMatchingControlLayout(ref deviceDescription);
		}

		public static global::System.Collections.Generic.IEnumerable<string> ListLayouts()
		{
			return s_Manager.ListControlLayouts();
		}

		public static global::System.Collections.Generic.IEnumerable<string> ListLayoutsBasedOn(string baseLayout)
		{
			if (string.IsNullOrEmpty(baseLayout))
			{
				throw new global::System.ArgumentNullException("baseLayout");
			}
			return s_Manager.ListControlLayouts(baseLayout);
		}

		public static global::UnityEngine.InputSystem.Layouts.InputControlLayout LoadLayout(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			return s_Manager.TryLoadControlLayout(new global::UnityEngine.InputSystem.Utilities.InternedString(name));
		}

		public static global::UnityEngine.InputSystem.Layouts.InputControlLayout LoadLayout<TControl>() where TControl : global::UnityEngine.InputSystem.InputControl
		{
			return s_Manager.TryLoadControlLayout(typeof(TControl));
		}

		public static string GetNameOfBaseLayout(string layoutName)
		{
			if (string.IsNullOrEmpty(layoutName))
			{
				throw new global::System.ArgumentNullException("layoutName");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString key = new global::UnityEngine.InputSystem.Utilities.InternedString(layoutName);
			if (global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(key, out var value))
			{
				return value;
			}
			return null;
		}

		public static bool IsFirstLayoutBasedOnSecond(string firstLayoutName, string secondLayoutName)
		{
			if (string.IsNullOrEmpty(firstLayoutName))
			{
				throw new global::System.ArgumentNullException("firstLayoutName");
			}
			if (string.IsNullOrEmpty(secondLayoutName))
			{
				throw new global::System.ArgumentNullException("secondLayoutName");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(firstLayoutName);
			global::UnityEngine.InputSystem.Utilities.InternedString internedString2 = new global::UnityEngine.InputSystem.Utilities.InternedString(secondLayoutName);
			if (internedString == internedString2)
			{
				return true;
			}
			return global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.IsBasedOn(internedString2, internedString);
		}

		public static void RegisterProcessor(global::System.Type type, string name = null)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Processor"))
				{
					name = name.Substring(0, name.Length - "Processor".Length);
				}
			}
			global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts = s_Manager.m_Layouts.precompiledLayouts;
			foreach (global::UnityEngine.InputSystem.Utilities.InternedString item in new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.InternedString>(precompiledLayouts.Keys))
			{
				if (global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(precompiledLayouts[item].metadata, name, ';'))
				{
					s_Manager.m_Layouts.precompiledLayouts.Remove(item);
				}
			}
			s_Manager.processors.AddTypeRegistration(name, type);
		}

		public static void RegisterProcessor<T>(string name = null)
		{
			RegisterProcessor(typeof(T), name);
		}

		public static global::System.Type TryGetProcessor(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			return s_Manager.processors.LookupTypeRegistration(name);
		}

		public static global::System.Collections.Generic.IEnumerable<string> ListProcessors()
		{
			return s_Manager.processors.names;
		}

		public static global::UnityEngine.InputSystem.InputDevice AddDevice(string layout, string name = null, string variants = null)
		{
			if (string.IsNullOrEmpty(layout))
			{
				throw new global::System.ArgumentNullException("layout");
			}
			return s_Manager.AddDevice(layout, name, new global::UnityEngine.InputSystem.Utilities.InternedString(variants));
		}

		public static TDevice AddDevice<TDevice>(string name = null) where TDevice : global::UnityEngine.InputSystem.InputDevice
		{
			global::UnityEngine.InputSystem.InputDevice inputDevice = s_Manager.AddDevice(typeof(TDevice), name);
			TDevice obj = inputDevice as TDevice;
			if (obj == null)
			{
				if (inputDevice != null)
				{
					RemoveDevice(inputDevice);
				}
				throw new global::System.InvalidOperationException("Layout registered for type '" + typeof(TDevice).Name + "' did not produce a device of that type; layout probably has been overridden");
			}
			return obj;
		}

		public static global::UnityEngine.InputSystem.InputDevice AddDevice(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description)
		{
			if (description.empty)
			{
				throw new global::System.ArgumentException("Description must not be empty", "description");
			}
			return s_Manager.AddDevice(description);
		}

		public static void AddDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			s_Manager.AddDevice(device);
		}

		public static void RemoveDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			s_Manager.RemoveDevice(device);
		}

		public static void FlushDisconnectedDevices()
		{
			s_Manager.FlushDisconnectedDevices();
		}

		public static global::UnityEngine.InputSystem.InputDevice GetDevice(string nameOrLayout)
		{
			return s_Manager.TryGetDevice(nameOrLayout);
		}

		public static TDevice GetDevice<TDevice>() where TDevice : global::UnityEngine.InputSystem.InputDevice
		{
			return (TDevice)GetDevice(typeof(TDevice));
		}

		public static global::UnityEngine.InputSystem.InputDevice GetDevice(global::System.Type type)
		{
			global::UnityEngine.InputSystem.InputDevice inputDevice = null;
			double num = -1.0;
			foreach (global::UnityEngine.InputSystem.InputDevice device in devices)
			{
				if (type.IsInstanceOfType(device) && (inputDevice == null || device.m_LastUpdateTimeInternal > num))
				{
					inputDevice = device;
					num = inputDevice.m_LastUpdateTimeInternal;
				}
			}
			return inputDevice;
		}

		public static TDevice GetDevice<TDevice>(global::UnityEngine.InputSystem.Utilities.InternedString usage) where TDevice : global::UnityEngine.InputSystem.InputDevice
		{
			TDevice val = null;
			double num = -1.0;
			foreach (global::UnityEngine.InputSystem.InputDevice device in devices)
			{
				if (device is TDevice val2 && global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.Contains(val2.usages, usage) && (val == null || val2.m_LastUpdateTimeInternal > num))
				{
					val = val2;
					num = val.m_LastUpdateTimeInternal;
				}
			}
			return val;
		}

		public static TDevice GetDevice<TDevice>(string usage) where TDevice : global::UnityEngine.InputSystem.InputDevice
		{
			return GetDevice<TDevice>(new global::UnityEngine.InputSystem.Utilities.InternedString(usage));
		}

		public static global::UnityEngine.InputSystem.InputDevice GetDeviceById(int deviceId)
		{
			return s_Manager.TryGetDeviceById(deviceId);
		}

		public static global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputDeviceDescription> GetUnsupportedDevices()
		{
			global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputDeviceDescription> list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputDeviceDescription>();
			GetUnsupportedDevices(list);
			return list;
		}

		public static int GetUnsupportedDevices(global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputDeviceDescription> descriptions)
		{
			return s_Manager.GetUnsupportedDevices(descriptions);
		}

		public static void EnableDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			s_Manager.EnableOrDisableDevice(device, enable: true);
		}

		public static void DisableDevice(global::UnityEngine.InputSystem.InputDevice device, bool keepSendingEvents = false)
		{
			s_Manager.EnableOrDisableDevice(device, enable: false, keepSendingEvents ? global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.InFrontendOnly : global::UnityEngine.InputSystem.InputManager.DeviceDisableScope.Everywhere);
		}

		public static bool TrySyncDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new global::System.InvalidOperationException($"Device '{device}' has not been added");
			}
			return device.RequestSync();
		}

		public static void ResetDevice(global::UnityEngine.InputSystem.InputDevice device, bool alsoResetDontResetControls = false)
		{
			s_Manager.ResetDevice(device, alsoResetDontResetControls);
		}

		[global::System.Obsolete("Use 'ResetDevice' instead.", false)]
		public static bool TryResetDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			return device.RequestReset();
		}

		public static void PauseHaptics()
		{
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> readOnlyArray = devices;
			int count = readOnlyArray.Count;
			for (int i = 0; i < count; i++)
			{
				if (readOnlyArray[i] is global::UnityEngine.InputSystem.Haptics.IHaptics haptics)
				{
					haptics.PauseHaptics();
				}
			}
		}

		public static void ResumeHaptics()
		{
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> readOnlyArray = devices;
			int count = readOnlyArray.Count;
			for (int i = 0; i < count; i++)
			{
				if (readOnlyArray[i] is global::UnityEngine.InputSystem.Haptics.IHaptics haptics)
				{
					haptics.ResumeHaptics();
				}
			}
		}

		public static void ResetHaptics()
		{
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> readOnlyArray = devices;
			int count = readOnlyArray.Count;
			for (int i = 0; i < count; i++)
			{
				if (readOnlyArray[i] is global::UnityEngine.InputSystem.Haptics.IHaptics haptics)
				{
					haptics.ResetHaptics();
				}
			}
		}

		public static void SetDeviceUsage(global::UnityEngine.InputSystem.InputDevice device, string usage)
		{
			SetDeviceUsage(device, new global::UnityEngine.InputSystem.Utilities.InternedString(usage));
		}

		public static void SetDeviceUsage(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.Utilities.InternedString usage)
		{
			s_Manager.SetDeviceUsage(device, usage);
		}

		public static void AddDeviceUsage(global::UnityEngine.InputSystem.InputDevice device, string usage)
		{
			s_Manager.AddDeviceUsage(device, new global::UnityEngine.InputSystem.Utilities.InternedString(usage));
		}

		public static void AddDeviceUsage(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.Utilities.InternedString usage)
		{
			s_Manager.AddDeviceUsage(device, usage);
		}

		public static void RemoveDeviceUsage(global::UnityEngine.InputSystem.InputDevice device, string usage)
		{
			s_Manager.RemoveDeviceUsage(device, new global::UnityEngine.InputSystem.Utilities.InternedString(usage));
		}

		public static void RemoveDeviceUsage(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.Utilities.InternedString usage)
		{
			s_Manager.RemoveDeviceUsage(device, usage);
		}

		public static global::UnityEngine.InputSystem.InputControl FindControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new global::System.ArgumentNullException("path");
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> readOnlyArray = s_Manager.devices;
			int count = readOnlyArray.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.InputSystem.InputControl inputControl = global::UnityEngine.InputSystem.InputControlPath.TryFindControl(readOnlyArray[i], path);
				if (inputControl != null)
				{
					return inputControl;
				}
			}
			return null;
		}

		public static global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> FindControls(string path)
		{
			return FindControls<global::UnityEngine.InputSystem.InputControl>(path);
		}

		public static global::UnityEngine.InputSystem.InputControlList<TControl> FindControls<TControl>(string path) where TControl : global::UnityEngine.InputSystem.InputControl
		{
			global::UnityEngine.InputSystem.InputControlList<TControl> controls = default(global::UnityEngine.InputSystem.InputControlList<TControl>);
			FindControls(path, ref controls);
			return controls;
		}

		public static int FindControls<TControl>(string path, ref global::UnityEngine.InputSystem.InputControlList<TControl> controls) where TControl : global::UnityEngine.InputSystem.InputControl
		{
			return s_Manager.GetControls(path, ref controls);
		}

		public static void QueueEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				throw new global::System.ArgumentException("Received a null event pointer", "eventPtr");
			}
			s_Manager.QueueEvent(eventPtr);
		}

		public static void QueueEvent<TEvent>(ref TEvent inputEvent) where TEvent : struct, global::UnityEngine.InputSystem.LowLevel.IInputEventTypeInfo
		{
			s_Manager.QueueEvent(ref inputEvent);
		}

		public unsafe static void QueueStateEvent<TState>(global::UnityEngine.InputSystem.InputDevice device, TState state, double time = -1.0) where TState : struct, global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (device.m_DeviceIndex == -1)
			{
				throw new global::System.InvalidOperationException($"Cannot queue state event for device '{device}' because device has not been added to system");
			}
			uint num = (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TState>();
			if (num > 512)
			{
				throw new global::System.ArgumentException($"Size of '{typeof(TState).Name}' exceeds maximum supported state size of {512}", "state");
			}
			long num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.LowLevel.StateEvent>() + num - 1;
			time = ((!(time < 0.0)) ? (time + global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup) : global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime);
			global::UnityEngine.InputSystem.InputSystem.StateEventBuffer stateEventBuffer = default(global::UnityEngine.InputSystem.InputSystem.StateEventBuffer);
			stateEventBuffer.stateEvent = new global::UnityEngine.InputSystem.LowLevel.StateEvent
			{
				baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1398030676, (int)num2, device.deviceId, time),
				stateFormat = state.format
			};
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(stateEventBuffer.stateEvent.stateData, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref state), num);
			s_Manager.QueueEvent(ref stateEventBuffer.stateEvent);
		}

		public unsafe static void QueueDeltaStateEvent<TDelta>(global::UnityEngine.InputSystem.InputControl control, TDelta delta, double time = -1.0) where TDelta : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (control.stateBlock.bitOffset != 0)
			{
				throw new global::System.InvalidOperationException($"Cannot send delta state events against bitfield controls: {control}");
			}
			global::UnityEngine.InputSystem.InputDevice device = control.device;
			if (device.m_DeviceIndex == -1)
			{
				throw new global::System.InvalidOperationException($"Cannot queue state event for control '{control}' on device '{device}' because device has not been added to system");
			}
			time = ((!(time < 0.0)) ? (time + global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup) : global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime);
			uint num = (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TDelta>();
			if (num > 512)
			{
				throw new global::System.ArgumentException($"Size of state delta '{typeof(TDelta).Name}' exceeds maximum supported state size of {512}", "delta");
			}
			if (num != control.stateBlock.alignedSizeInBytes)
			{
				throw new global::System.ArgumentException($"Size {num} of delta state of type {typeof(TDelta).Name} provided for control '{control}' does not match size {control.stateBlock.alignedSizeInBytes} of control", "delta");
			}
			long num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent>() + num - 1;
			global::UnityEngine.InputSystem.InputSystem.DeltaStateEventBuffer deltaStateEventBuffer = default(global::UnityEngine.InputSystem.InputSystem.DeltaStateEventBuffer);
			deltaStateEventBuffer.stateEvent = new global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent
			{
				baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1145852993, (int)num2, device.deviceId, time),
				stateFormat = device.stateBlock.format,
				stateOffset = control.m_StateBlock.byteOffset - device.m_StateBlock.byteOffset
			};
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(deltaStateEventBuffer.stateEvent.stateData, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref delta), num);
			s_Manager.QueueEvent(ref deltaStateEventBuffer.stateEvent);
		}

		public static void QueueConfigChangeEvent(global::UnityEngine.InputSystem.InputDevice device, double time = -1.0)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (device.deviceId == 0)
			{
				throw new global::System.InvalidOperationException("Device has not been added");
			}
			time = ((!(time < 0.0)) ? (time + global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup) : global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime);
			global::UnityEngine.InputSystem.LowLevel.DeviceConfigurationEvent inputEvent = global::UnityEngine.InputSystem.LowLevel.DeviceConfigurationEvent.Create(device.deviceId, time);
			s_Manager.QueueEvent(ref inputEvent);
		}

		public static void QueueTextEvent(global::UnityEngine.InputSystem.InputDevice device, char character, double time = -1.0)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (device.deviceId == 0)
			{
				throw new global::System.InvalidOperationException("Device has not been added");
			}
			time = ((!(time < 0.0)) ? (time + global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup) : global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime);
			global::UnityEngine.InputSystem.LowLevel.TextEvent inputEvent = global::UnityEngine.InputSystem.LowLevel.TextEvent.Create(device.deviceId, character, time);
			s_Manager.QueueEvent(ref inputEvent);
		}

		public static void Update()
		{
			s_Manager.Update();
		}

		internal static void Update(global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			if (updateType != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None && (s_Manager.updateMask & updateType) == 0)
			{
				throw new global::System.InvalidOperationException($"'{updateType}' updates are not enabled; InputSystem.settings.updateMode is set to '{settings.updateMode}'");
			}
			s_Manager.Update(updateType);
		}

		private static void EnableActions()
		{
			if (!(actions == null))
			{
				actions.Enable();
			}
		}

		private static void DisableActions(bool triggerSetupChanged = false)
		{
			global::UnityEngine.InputSystem.InputActionAsset inputActionAsset = actions;
			if (!(inputActionAsset == null))
			{
				inputActionAsset.Disable();
				if (triggerSetupChanged)
				{
					inputActionAsset.OnSetupChanged();
				}
			}
		}

		public static void RegisterInteraction(global::System.Type type, string name = null)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Interaction"))
				{
					name = name.Substring(0, name.Length - "Interaction".Length);
				}
			}
			s_Manager.interactions.AddTypeRegistration(name, type);
		}

		public static void RegisterInteraction<T>(string name = null)
		{
			RegisterInteraction(typeof(T), name);
		}

		public static global::System.Type TryGetInteraction(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			return s_Manager.interactions.LookupTypeRegistration(name);
		}

		public static global::System.Collections.Generic.IEnumerable<string> ListInteractions()
		{
			return s_Manager.interactions.names;
		}

		public static void RegisterBindingComposite(global::System.Type type, string name)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Composite"))
				{
					name = name.Substring(0, name.Length - "Composite".Length);
				}
			}
			s_Manager.composites.AddTypeRegistration(name, type);
		}

		public static void RegisterBindingComposite<T>(string name = null)
		{
			RegisterBindingComposite(typeof(T), name);
		}

		public static global::System.Type TryGetBindingComposite(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			return s_Manager.composites.LookupTypeRegistration(name);
		}

		public static void DisableAllEnabledActions()
		{
			global::UnityEngine.InputSystem.InputActionState.DisableAllActions();
		}

		public static global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputAction> ListEnabledActions()
		{
			global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputAction> result = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputAction>();
			ListEnabledActions(result);
			return result;
		}

		public static int ListEnabledActions(global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputAction> actions)
		{
			if (actions == null)
			{
				throw new global::System.ArgumentNullException("actions");
			}
			return global::UnityEngine.InputSystem.InputActionState.FindAllEnabledActions(actions);
		}

		static InputSystem()
		{
			k_InputResetMarker = new global::Unity.Profiling.ProfilerMarker("InputSystem.Reset");
			InitializeInPlayer();
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RunInitializeInPlayer()
		{
			if (s_Manager == null)
			{
				InitializeInPlayer();
			}
		}

		internal static void EnsureInitialized()
		{
		}

		private static void InitializeInPlayer(global::UnityEngine.InputSystem.LowLevel.IInputRuntime runtime = null, global::UnityEngine.InputSystem.InputSettings settings = null)
		{
			if (settings == null)
			{
				settings = global::System.Linq.Enumerable.FirstOrDefault(global::UnityEngine.Resources.FindObjectsOfTypeAll<global::UnityEngine.InputSystem.InputSettings>()) ?? global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.InputSystem.InputSettings>();
			}
			s_Manager = new global::UnityEngine.InputSystem.InputManager();
			s_Manager.Initialize(runtime ?? global::UnityEngine.InputSystem.LowLevel.NativeInputRuntime.instance, settings);
			PerformDefaultPluginInitialization();
			EnableActions();
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RunInitialUpdate()
		{
			Update(global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None);
		}

		private static void PerformDefaultPluginInitialization()
		{
			UISupport.Initialize();
			global::UnityEngine.InputSystem.XInput.XInputSupport.Initialize();
			global::UnityEngine.InputSystem.DualShock.DualShockSupport.Initialize();
			global::UnityEngine.InputSystem.HID.HIDSupport.Initialize();
			global::UnityEngine.InputSystem.Switch.SwitchSupportHID.Initialize();
			global::UnityEngine.InputSystem.XR.XRSupport.Initialize();
		}
	}
}
