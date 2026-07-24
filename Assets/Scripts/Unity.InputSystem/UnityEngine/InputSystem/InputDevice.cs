namespace UnityEngine.InputSystem
{
	public class InputDevice : global::UnityEngine.InputSystem.InputControl
	{
		[global::System.Serializable]
		[global::System.Flags]
		internal enum DeviceFlags
		{
			UpdateBeforeRender = 1,
			HasStateCallbacks = 2,
			HasControlsWithDefaultState = 4,
			HasDontResetControls = 0x400,
			HasEventMerger = 0x2000,
			HasEventPreProcessor = 0x4000,
			Remote = 8,
			Native = 0x10,
			DisabledInFrontend = 0x20,
			DisabledInRuntime = 0x80,
			DisabledWhileInBackground = 0x100,
			DisabledStateHasBeenQueriedFromRuntime = 0x40,
			CanRunInBackground = 0x800,
			CanRunInBackgroundHasBeenQueried = 0x1000
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		internal struct ControlBitRangeNode
		{
			public ushort endBitOffset;

			public short leftChildIndex;

			public ushort controlStartIndex;

			public byte controlCount;

			public ControlBitRangeNode(ushort endOffset)
			{
				controlStartIndex = 0;
				controlCount = 0;
				endBitOffset = endOffset;
				leftChildIndex = -1;
			}
		}

		public const int InvalidDeviceId = 0;

		internal const int kLocalParticipantId = 0;

		internal const int kInvalidDeviceIndex = -1;

		internal global::UnityEngine.InputSystem.InputDevice.DeviceFlags m_DeviceFlags;

		internal int m_DeviceId;

		internal int m_ParticipantId;

		internal int m_DeviceIndex;

		internal uint m_CurrentProcessedEventBytesOnUpdate;

		internal global::UnityEngine.InputSystem.Layouts.InputDeviceDescription m_Description;

		internal double m_LastUpdateTimeInternal;

		internal uint m_CurrentUpdateStepCount;

		internal global::UnityEngine.InputSystem.Utilities.InternedString[] m_AliasesForEachControl;

		internal global::UnityEngine.InputSystem.Utilities.InternedString[] m_UsagesForEachControl;

		internal global::UnityEngine.InputSystem.InputControl[] m_UsageToControl;

		internal global::UnityEngine.InputSystem.InputControl[] m_ChildrenForEachControl;

		internal global::System.Collections.Generic.HashSet<int> m_UpdatedButtons;

		internal global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Controls.ButtonControl> m_ButtonControlsCheckingPressState;

		internal bool m_UseCachePathForButtonPresses;

		internal uint[] m_StateOffsetToControlMap;

		internal global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode[] m_ControlTreeNodes;

		internal ushort[] m_ControlTreeIndices;

		internal const int kControlIndexBits = 10;

		internal const int kStateOffsetBits = 13;

		internal const int kStateSizeBits = 9;

		public global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description => m_Description;

		public bool enabled
		{
			get
			{
				if ((m_DeviceFlags & (global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInFrontend | global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledWhileInBackground)) != 0)
				{
					return false;
				}
				return QueryEnabledStateFromRuntime();
			}
		}

		public bool canRunInBackground => canDeviceRunInBackground;

		internal bool canDeviceRunInBackground
		{
			get
			{
				if ((m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried) != 0)
				{
					return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.CanRunInBackground) != 0;
				}
				global::UnityEngine.InputSystem.LowLevel.QueryCanRunInBackground command = global::UnityEngine.InputSystem.LowLevel.QueryCanRunInBackground.Create();
				m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried;
				if (ExecuteCommand(ref command) >= 0 && command.canRunInBackground)
				{
					m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.CanRunInBackground;
					return true;
				}
				m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.CanRunInBackground;
				return false;
			}
		}

		public bool added => m_DeviceIndex != -1;

		public bool remote => (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Remote) == global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Remote;

		public bool native => (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Native) == global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Native;

		public bool updateBeforeRender => (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.UpdateBeforeRender) == global::UnityEngine.InputSystem.InputDevice.DeviceFlags.UpdateBeforeRender;

		public int deviceId => m_DeviceId;

		public double lastUpdateTime => m_LastUpdateTimeInternal - global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;

		public bool wasUpdatedThisFrame => m_CurrentUpdateStepCount == global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> allControls => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl>(m_ChildrenForEachControl);

		public override global::System.Type valueType => typeof(byte[]);

		public override int valueSizeInBytes => (int)m_StateBlock.alignedSizeInBytes;

		[global::System.Obsolete("Use 'InputSystem.devices' instead. (UnityUpgradable) -> InputSystem.devices", false)]
		public static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> all => global::UnityEngine.InputSystem.InputSystem.devices;

		internal bool disabledInFrontend
		{
			get
			{
				return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInFrontend) != 0;
			}
			set
			{
				if (value)
				{
					m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInFrontend;
				}
				else
				{
					m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInFrontend;
				}
			}
		}

		internal bool disabledInRuntime
		{
			get
			{
				return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInRuntime) != 0;
			}
			set
			{
				if (value)
				{
					m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInRuntime;
				}
				else
				{
					m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInRuntime;
				}
			}
		}

		internal bool disabledWhileInBackground
		{
			get
			{
				return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledWhileInBackground) != 0;
			}
			set
			{
				if (value)
				{
					m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledWhileInBackground;
				}
				else
				{
					m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledWhileInBackground;
				}
			}
		}

		internal bool hasControlsWithDefaultState
		{
			get
			{
				return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasControlsWithDefaultState) == global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasControlsWithDefaultState;
			}
			set
			{
				if (value)
				{
					m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasControlsWithDefaultState;
				}
				else
				{
					m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasControlsWithDefaultState;
				}
			}
		}

		internal bool hasDontResetControls
		{
			get
			{
				return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasDontResetControls) == global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasDontResetControls;
			}
			set
			{
				if (value)
				{
					m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasDontResetControls;
				}
				else
				{
					m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasDontResetControls;
				}
			}
		}

		internal bool hasStateCallbacks
		{
			get
			{
				return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasStateCallbacks) == global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasStateCallbacks;
			}
			set
			{
				if (value)
				{
					m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasStateCallbacks;
				}
				else
				{
					m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasStateCallbacks;
				}
			}
		}

		internal bool hasEventMerger
		{
			get
			{
				return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasEventMerger) == global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasEventMerger;
			}
			set
			{
				if (value)
				{
					m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasEventMerger;
				}
				else
				{
					m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasEventMerger;
				}
			}
		}

		internal bool hasEventPreProcessor
		{
			get
			{
				return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasEventPreProcessor) == global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasEventPreProcessor;
			}
			set
			{
				if (value)
				{
					m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasEventPreProcessor;
				}
				else
				{
					m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.HasEventPreProcessor;
				}
			}
		}

		public InputDevice()
		{
			m_DeviceId = 0;
			m_ParticipantId = 0;
			m_DeviceIndex = -1;
		}

		public unsafe override object ReadValueFromBufferAsObject(void* buffer, int bufferSize)
		{
			throw new global::System.NotImplementedException();
		}

		public unsafe override object ReadValueFromStateAsObject(void* statePtr)
		{
			if (m_DeviceIndex == -1)
			{
				return null;
			}
			uint alignedSizeInBytes = base.stateBlock.alignedSizeInBytes;
			byte[] array = new byte[alignedSizeInBytes];
			fixed (byte* destination = array)
			{
				byte* source = (byte*)statePtr + m_StateBlock.byteOffset;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, alignedSizeInBytes);
			}
			return array;
		}

		public unsafe override void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize)
		{
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			if (bufferPtr == null)
			{
				throw new global::System.ArgumentNullException("bufferPtr");
			}
			if (bufferSize < valueSizeInBytes)
			{
				throw new global::System.ArgumentException($"Buffer too small (expected: {valueSizeInBytes}, actual: {bufferSize}");
			}
			byte* source = (byte*)statePtr + m_StateBlock.byteOffset;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(bufferPtr, source, m_StateBlock.alignedSizeInBytes);
		}

		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			if (firstStatePtr == null)
			{
				throw new global::System.ArgumentNullException("firstStatePtr");
			}
			if (secondStatePtr == null)
			{
				throw new global::System.ArgumentNullException("secondStatePtr");
			}
			byte* ptr = (byte*)firstStatePtr + m_StateBlock.byteOffset;
			byte* ptr2 = (byte*)firstStatePtr + m_StateBlock.byteOffset;
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr, ptr2, m_StateBlock.alignedSizeInBytes) == 0;
		}

		internal void NotifyConfigurationChanged()
		{
			base.isConfigUpToDate = false;
			for (int i = 0; i < m_ChildrenForEachControl.Length; i++)
			{
				m_ChildrenForEachControl[i].isConfigUpToDate = false;
			}
			m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
			OnConfigurationChanged();
		}

		public virtual void MakeCurrent()
		{
		}

		protected virtual void OnAdded()
		{
		}

		protected virtual void OnRemoved()
		{
		}

		protected virtual void OnConfigurationChanged()
		{
		}

		public unsafe long ExecuteCommand<TCommand>(ref TCommand command) where TCommand : struct, global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
		{
			global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand* command2 = (global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref command);
			global::UnityEngine.InputSystem.InputManager s_Manager = global::UnityEngine.InputSystem.InputSystem.s_Manager;
			s_Manager.m_DeviceCommandCallbacks.LockForChanges();
			for (int i = 0; i < s_Manager.m_DeviceCommandCallbacks.length; i++)
			{
				try
				{
					long? num = s_Manager.m_DeviceCommandCallbacks[i](this, command2);
					if (num.HasValue)
					{
						return num.Value;
					}
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing 'InputSystem.onDeviceCommand' callbacks");
					global::UnityEngine.Debug.LogException(ex);
				}
			}
			s_Manager.m_DeviceCommandCallbacks.UnlockForChanges();
			return ExecuteCommand((global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref command));
		}

		protected unsafe virtual long ExecuteCommand(global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand* commandPtr)
		{
			return global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.DeviceCommand(deviceId, commandPtr);
		}

		internal bool QueryEnabledStateFromRuntime()
		{
			if ((m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime) == 0)
			{
				global::UnityEngine.InputSystem.LowLevel.QueryEnabledStateCommand command = global::UnityEngine.InputSystem.LowLevel.QueryEnabledStateCommand.Create();
				if (ExecuteCommand(ref command) >= 0)
				{
					if (command.isEnabled)
					{
						m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInRuntime;
					}
					else
					{
						m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInRuntime;
					}
				}
				else
				{
					m_DeviceFlags &= ~global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInRuntime;
				}
				m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
			}
			return (m_DeviceFlags & global::UnityEngine.InputSystem.InputDevice.DeviceFlags.DisabledInRuntime) == 0;
		}

		internal static uint EncodeStateOffsetToControlMapEntry(uint controlIndex, uint stateOffsetInBits, uint stateSizeInBits)
		{
			return (stateOffsetInBits << 19) | (stateSizeInBits << 10) | controlIndex;
		}

		internal static void DecodeStateOffsetToControlMapEntry(uint entry, out uint controlIndex, out uint stateOffset, out uint stateSize)
		{
			controlIndex = entry & 0x3FF;
			stateOffset = entry >> 19;
			stateSize = (entry >> 10) & 0x1FF;
		}

		internal void AddDeviceUsage(global::UnityEngine.InputSystem.Utilities.InternedString usage)
		{
			int count = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_UsageToControl) + m_UsageCount;
			if (m_UsageCount == 0)
			{
				m_UsageStartIndex = count;
			}
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_UsagesForEachControl, ref count, usage);
			m_UsageCount++;
		}

		internal void RemoveDeviceUsage(global::UnityEngine.InputSystem.Utilities.InternedString usage)
		{
			int count = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_UsageToControl) + m_UsageCount;
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfValue(m_UsagesForEachControl, usage, m_UsageStartIndex, count);
			if (num != -1)
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(m_UsagesForEachControl, ref count, num);
				m_UsageCount--;
				if (m_UsageCount == 0)
				{
					m_UsageStartIndex = 0;
				}
			}
		}

		internal void ClearDeviceUsages()
		{
			for (int i = m_UsageStartIndex; i < m_UsageCount; i++)
			{
				m_UsagesForEachControl[i] = default(global::UnityEngine.InputSystem.Utilities.InternedString);
			}
			m_UsageCount = 0;
		}

		internal bool RequestSync()
		{
			SetOptimizedControlDataTypeRecursively();
			global::UnityEngine.InputSystem.LowLevel.RequestSyncCommand command = global::UnityEngine.InputSystem.LowLevel.RequestSyncCommand.Create();
			return base.device.ExecuteCommand(ref command) >= 0;
		}

		internal bool RequestReset()
		{
			SetOptimizedControlDataTypeRecursively();
			global::UnityEngine.InputSystem.LowLevel.RequestResetCommand command = global::UnityEngine.InputSystem.LowLevel.RequestResetCommand.Create();
			return base.device.ExecuteCommand(ref command) >= 0;
		}

		internal bool ExecuteEnableCommand()
		{
			SetOptimizedControlDataTypeRecursively();
			global::UnityEngine.InputSystem.LowLevel.EnableDeviceCommand command = global::UnityEngine.InputSystem.LowLevel.EnableDeviceCommand.Create();
			return base.device.ExecuteCommand(ref command) >= 0;
		}

		internal bool ExecuteDisableCommand()
		{
			global::UnityEngine.InputSystem.LowLevel.DisableDeviceCommand command = global::UnityEngine.InputSystem.LowLevel.DisableDeviceCommand.Create();
			return base.device.ExecuteCommand(ref command) >= 0;
		}

		internal void NotifyAdded()
		{
			OnAdded();
		}

		internal void NotifyRemoved()
		{
			OnRemoved();
		}

		internal static TDevice Build<TDevice>(string layoutName = null, string layoutVariants = null, global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription = default(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription), bool noPrecompiledLayouts = false) where TDevice : global::UnityEngine.InputSystem.InputDevice
		{
			global::UnityEngine.InputSystem.Utilities.InternedString key = new global::UnityEngine.InputSystem.Utilities.InternedString(layoutName);
			if (key.IsEmpty())
			{
				key = global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.TryFindLayoutForType(typeof(TDevice));
				if (key.IsEmpty())
				{
					key = new global::UnityEngine.InputSystem.Utilities.InternedString(typeof(TDevice).Name);
				}
			}
			if (!noPrecompiledLayouts && string.IsNullOrEmpty(layoutVariants) && global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.precompiledLayouts.TryGetValue(key, out var value))
			{
				return (TDevice)value.factoryMethod();
			}
			using (global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder.Ref())
			{
				global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder.instance.Setup(key, new global::UnityEngine.InputSystem.Utilities.InternedString(layoutVariants), deviceDescription);
				global::UnityEngine.InputSystem.InputDevice inputDevice = global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder.instance.Finish();
				if (!(inputDevice is TDevice result))
				{
					throw new global::System.ArgumentException("Expected device of type '" + typeof(TDevice).Name + "' but got device of type '" + inputDevice.GetType().Name + "' instead", "TDevice");
				}
				return result;
			}
		}

		internal unsafe void WriteChangedControlStates(byte* deviceStateBuffer, void* statePtr, uint stateSizeInBytes, uint stateOffsetInDevice)
		{
			if (m_ControlTreeNodes.Length == 0)
			{
				return;
			}
			m_UpdatedButtons.Clear();
			if (m_StateBlock.sizeInBits != stateSizeInBytes * 8)
			{
				if (m_ControlTreeNodes[0].leftChildIndex != -1)
				{
					WritePartialChangedControlStatesInternal(stateSizeInBytes * 8, stateOffsetInDevice * 8, m_ControlTreeNodes[0], 0u);
				}
			}
			else if (m_ControlTreeNodes[0].leftChildIndex != -1)
			{
				WriteChangedControlStatesInternal(statePtr, deviceStateBuffer, m_ControlTreeNodes[0], 0u);
			}
		}

		private void WritePartialChangedControlStatesInternal(uint stateSizeInBits, uint stateOffsetInDeviceInBits, global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode, uint startOffset)
		{
			global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode2 = m_ControlTreeNodes[parentNode.leftChildIndex];
			if (global::System.Math.Max(stateOffsetInDeviceInBits, startOffset) <= global::System.Math.Min(stateOffsetInDeviceInBits + stateSizeInBits, parentNode2.endBitOffset))
			{
				int num = parentNode2.controlStartIndex + parentNode2.controlCount;
				for (int i = parentNode2.controlStartIndex; i < num; i++)
				{
					ushort num2 = m_ControlTreeIndices[i];
					global::UnityEngine.InputSystem.InputControl inputControl = m_ChildrenForEachControl[num2];
					inputControl.MarkAsStale();
					if (inputControl.isButton && ((global::UnityEngine.InputSystem.Controls.ButtonControl)inputControl).needsToCheckFramePress)
					{
						m_UpdatedButtons.Add(num2);
					}
				}
				if (parentNode2.leftChildIndex != -1)
				{
					WritePartialChangedControlStatesInternal(stateSizeInBits, stateOffsetInDeviceInBits, parentNode2, startOffset);
				}
			}
			global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode3 = m_ControlTreeNodes[parentNode.leftChildIndex + 1];
			if (global::System.Math.Max(stateOffsetInDeviceInBits, parentNode2.endBitOffset) > global::System.Math.Min(stateOffsetInDeviceInBits + stateSizeInBits, parentNode3.endBitOffset))
			{
				return;
			}
			int num3 = parentNode3.controlStartIndex + parentNode3.controlCount;
			for (int j = parentNode3.controlStartIndex; j < num3; j++)
			{
				ushort num4 = m_ControlTreeIndices[j];
				global::UnityEngine.InputSystem.InputControl inputControl2 = m_ChildrenForEachControl[num4];
				inputControl2.MarkAsStale();
				if (inputControl2.isButton && ((global::UnityEngine.InputSystem.Controls.ButtonControl)inputControl2).needsToCheckFramePress)
				{
					m_UpdatedButtons.Add(num4);
				}
			}
			if (parentNode3.leftChildIndex != -1)
			{
				WritePartialChangedControlStatesInternal(stateSizeInBits, stateOffsetInDeviceInBits, parentNode3, parentNode2.endBitOffset);
			}
		}

		private void DumpControlBitRangeNode(int nodeIndex, global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode node, uint startOffset, uint sizeInBits, global::System.Collections.Generic.List<string> output)
		{
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			for (int i = 0; i < node.controlCount; i++)
			{
				ushort num = m_ControlTreeIndices[node.controlStartIndex + i];
				global::UnityEngine.InputSystem.InputControl inputControl = m_ChildrenForEachControl[num];
				list.Add(inputControl.path);
			}
			string text = string.Join(", ", list);
			string text2 = ((node.leftChildIndex != -1) ? $" <{node.leftChildIndex}, {node.leftChildIndex + 1}>" : "");
			output.Add($"{nodeIndex} [{startOffset}, {startOffset + sizeInBits}]{text2}->{text}");
		}

		private void DumpControlTree(global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode, uint startOffset, global::System.Collections.Generic.List<string> output)
		{
			global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode controlBitRangeNode = m_ControlTreeNodes[parentNode.leftChildIndex];
			global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode controlBitRangeNode2 = m_ControlTreeNodes[parentNode.leftChildIndex + 1];
			DumpControlBitRangeNode(parentNode.leftChildIndex, controlBitRangeNode, startOffset, controlBitRangeNode.endBitOffset - startOffset, output);
			DumpControlBitRangeNode(parentNode.leftChildIndex + 1, controlBitRangeNode2, controlBitRangeNode.endBitOffset, (uint)(controlBitRangeNode2.endBitOffset - controlBitRangeNode.endBitOffset), output);
			if (controlBitRangeNode.leftChildIndex != -1)
			{
				DumpControlTree(controlBitRangeNode, startOffset, output);
			}
			if (controlBitRangeNode2.leftChildIndex != -1)
			{
				DumpControlTree(controlBitRangeNode2, controlBitRangeNode.endBitOffset, output);
			}
		}

		internal string DumpControlTree()
		{
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			DumpControlTree(m_ControlTreeNodes[0], 0u, list);
			return string.Join("\n", list);
		}

		private unsafe void WriteChangedControlStatesInternal(void* statePtr, byte* deviceStatePtr, global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode, uint startOffset)
		{
			global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode2 = m_ControlTreeNodes[parentNode.leftChildIndex];
			if (HasDataChangedInRange(deviceStatePtr, statePtr, startOffset, parentNode2.endBitOffset - startOffset + 1))
			{
				int num = parentNode2.controlStartIndex + parentNode2.controlCount;
				for (int i = parentNode2.controlStartIndex; i < num; i++)
				{
					ushort num2 = m_ControlTreeIndices[i];
					global::UnityEngine.InputSystem.InputControl inputControl = m_ChildrenForEachControl[num2];
					if (!inputControl.CompareState(deviceStatePtr - m_StateBlock.byteOffset, (byte*)statePtr - m_StateBlock.byteOffset, null))
					{
						inputControl.MarkAsStale();
						if (inputControl.isButton && ((global::UnityEngine.InputSystem.Controls.ButtonControl)inputControl).needsToCheckFramePress)
						{
							m_UpdatedButtons.Add(num2);
						}
					}
				}
				if (parentNode2.leftChildIndex != -1)
				{
					WriteChangedControlStatesInternal(statePtr, deviceStatePtr, parentNode2, startOffset);
				}
			}
			global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parentNode3 = m_ControlTreeNodes[parentNode.leftChildIndex + 1];
			if (!HasDataChangedInRange(deviceStatePtr, statePtr, parentNode2.endBitOffset, (uint)(parentNode3.endBitOffset - parentNode2.endBitOffset + 1)))
			{
				return;
			}
			int num3 = parentNode3.controlStartIndex + parentNode3.controlCount;
			for (int j = parentNode3.controlStartIndex; j < num3; j++)
			{
				ushort num4 = m_ControlTreeIndices[j];
				global::UnityEngine.InputSystem.InputControl inputControl2 = m_ChildrenForEachControl[num4];
				if (!inputControl2.CompareState(deviceStatePtr - m_StateBlock.byteOffset, (byte*)statePtr - m_StateBlock.byteOffset, null))
				{
					inputControl2.MarkAsStale();
					if (inputControl2.isButton && ((global::UnityEngine.InputSystem.Controls.ButtonControl)inputControl2).needsToCheckFramePress)
					{
						m_UpdatedButtons.Add(num4);
					}
				}
			}
			if (parentNode3.leftChildIndex != -1)
			{
				WriteChangedControlStatesInternal(statePtr, deviceStatePtr, parentNode3, parentNode2.endBitOffset);
			}
		}

		private unsafe static bool HasDataChangedInRange(byte* deviceStatePtr, void* statePtr, uint startOffset, uint sizeInBits)
		{
			if (sizeInBits == 1)
			{
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(deviceStatePtr, startOffset) != global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(statePtr, startOffset);
			}
			return !global::UnityEngine.InputSystem.Utilities.MemoryHelpers.MemCmpBitRegion(deviceStatePtr, statePtr, startOffset, sizeInBits, null);
		}
	}
}
