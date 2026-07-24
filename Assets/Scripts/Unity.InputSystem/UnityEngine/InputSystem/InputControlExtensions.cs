namespace UnityEngine.InputSystem
{
	public static class InputControlExtensions
	{
		[global::System.Flags]
		public enum Enumerate
		{
			IgnoreControlsInDefaultState = 1,
			IgnoreControlsInCurrentState = 2,
			IncludeSyntheticControls = 4,
			IncludeNoisyControls = 8,
			IncludeNonLeafControls = 0x10
		}

		public struct InputEventControlCollection : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputControl>, global::System.Collections.IEnumerable
		{
			internal global::UnityEngine.InputSystem.InputDevice m_Device;

			internal global::UnityEngine.InputSystem.LowLevel.InputEventPtr m_EventPtr;

			internal global::UnityEngine.InputSystem.InputControlExtensions.Enumerate m_Flags;

			internal float m_MagnitudeThreshold;

			public global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr => m_EventPtr;

			public global::UnityEngine.InputSystem.InputControlExtensions.InputEventControlEnumerator GetEnumerator()
			{
				return new global::UnityEngine.InputSystem.InputControlExtensions.InputEventControlEnumerator(m_EventPtr, m_Device, m_Flags, m_MagnitudeThreshold);
			}

			global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.InputControl> global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputControl>.GetEnumerator()
			{
				return GetEnumerator();
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		public struct InputEventControlEnumerator : global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.InputControl>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private global::UnityEngine.InputSystem.InputControlExtensions.Enumerate m_Flags;

			private readonly global::UnityEngine.InputSystem.InputDevice m_Device;

			private readonly uint[] m_StateOffsetToControlIndex;

			private readonly int m_StateOffsetToControlIndexLength;

			private readonly global::UnityEngine.InputSystem.InputControl[] m_AllControls;

			private unsafe byte* m_DefaultState;

			private unsafe byte* m_CurrentState;

			private unsafe byte* m_NoiseMask;

			private global::UnityEngine.InputSystem.LowLevel.InputEventPtr m_EventPtr;

			private global::UnityEngine.InputSystem.InputControl m_CurrentControl;

			private int m_CurrentIndexInStateOffsetToControlIndexMap;

			private uint m_CurrentControlStateBitOffset;

			private unsafe byte* m_EventState;

			private uint m_CurrentBitOffset;

			private uint m_EndBitOffset;

			private float m_MagnitudeThreshold;

			public global::UnityEngine.InputSystem.InputControl Current => m_CurrentControl;

			object global::System.Collections.IEnumerator.Current => Current;

			internal unsafe InputEventControlEnumerator(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputControlExtensions.Enumerate flags, float magnitudeThreshold = 0f)
			{
				m_Device = device;
				m_StateOffsetToControlIndex = device.m_StateOffsetToControlMap;
				m_StateOffsetToControlIndexLength = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_StateOffsetToControlIndex);
				m_AllControls = device.m_ChildrenForEachControl;
				m_EventPtr = eventPtr;
				m_Flags = flags;
				m_CurrentControl = null;
				m_CurrentIndexInStateOffsetToControlIndexMap = 0;
				m_CurrentControlStateBitOffset = 0u;
				m_EventState = default(byte*);
				m_CurrentBitOffset = 0u;
				m_EndBitOffset = 0u;
				m_MagnitudeThreshold = magnitudeThreshold;
				if ((flags & global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IncludeNoisyControls) == 0)
				{
					m_NoiseMask = (byte*)device.noiseMaskPtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					m_NoiseMask = default(byte*);
				}
				if ((flags & global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IgnoreControlsInDefaultState) != 0)
				{
					m_DefaultState = (byte*)device.defaultStatePtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					m_DefaultState = default(byte*);
				}
				if ((flags & global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IgnoreControlsInCurrentState) != 0)
				{
					m_CurrentState = (byte*)device.currentStatePtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					m_CurrentState = default(byte*);
				}
				Reset();
			}

			private unsafe bool CheckDefault(uint numBits)
			{
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.MemCmpBitRegion(m_EventState, m_DefaultState, m_CurrentBitOffset, numBits, m_NoiseMask);
			}

			private unsafe bool CheckCurrent(uint numBits)
			{
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.MemCmpBitRegion(m_EventState, m_CurrentState, m_CurrentBitOffset, numBits, m_NoiseMask);
			}

			public unsafe bool MoveNext()
			{
				if (!m_EventPtr.valid)
				{
					throw new global::System.ObjectDisposedException("Enumerator has already been disposed");
				}
				if (m_CurrentControl != null && (m_Flags & global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IncludeNonLeafControls) != 0)
				{
					global::UnityEngine.InputSystem.InputControl parent = m_CurrentControl.parent;
					if (parent != m_Device)
					{
						m_CurrentControl = parent;
						return true;
					}
				}
				bool flag = m_DefaultState != null;
				bool flag2 = m_CurrentState != null;
				while (true)
				{
					m_CurrentControl = null;
					if (flag2 || flag)
					{
						if ((m_CurrentBitOffset & 7) != 0)
						{
							uint num = (m_CurrentBitOffset + 8) & 7;
							if ((flag2 && CheckCurrent(num)) || (flag && CheckDefault(num)))
							{
								m_CurrentBitOffset += num;
							}
						}
						while (m_CurrentBitOffset < m_EndBitOffset)
						{
							uint num2 = m_CurrentBitOffset >> 3;
							byte b = m_EventState[num2];
							int num3 = ((m_NoiseMask != null) ? m_NoiseMask[num2] : byte.MaxValue);
							if (flag2 && (m_CurrentState[num2] & num3) == (b & num3))
							{
								m_CurrentBitOffset += 8u;
								continue;
							}
							if (!flag || (m_DefaultState[num2] & num3) != (b & num3))
							{
								break;
							}
							m_CurrentBitOffset += 8u;
						}
					}
					if (m_CurrentBitOffset >= m_EndBitOffset || m_CurrentIndexInStateOffsetToControlIndexMap >= m_StateOffsetToControlIndexLength)
					{
						return false;
					}
					for (; m_CurrentIndexInStateOffsetToControlIndexMap < m_StateOffsetToControlIndexLength; m_CurrentIndexInStateOffsetToControlIndexMap++)
					{
						global::UnityEngine.InputSystem.InputDevice.DecodeStateOffsetToControlMapEntry(m_StateOffsetToControlIndex[m_CurrentIndexInStateOffsetToControlIndexMap], out var controlIndex, out var stateOffset, out var stateSize);
						if (stateOffset < m_CurrentControlStateBitOffset || m_CurrentBitOffset >= stateOffset + stateSize - m_CurrentControlStateBitOffset)
						{
							continue;
						}
						if (stateOffset - m_CurrentControlStateBitOffset >= m_CurrentBitOffset + 8)
						{
							m_CurrentBitOffset = stateOffset - m_CurrentControlStateBitOffset;
							break;
						}
						if (stateOffset + stateSize - m_CurrentControlStateBitOffset > m_EndBitOffset)
						{
							continue;
						}
						if ((stateOffset & 7) == 0 && (stateSize & 7) == 0)
						{
							m_CurrentControl = m_AllControls[controlIndex];
						}
						else
						{
							if ((flag2 && global::UnityEngine.InputSystem.Utilities.MemoryHelpers.MemCmpBitRegion(m_EventState, m_CurrentState, stateOffset - m_CurrentControlStateBitOffset, stateSize, m_NoiseMask)) || (flag && global::UnityEngine.InputSystem.Utilities.MemoryHelpers.MemCmpBitRegion(m_EventState, m_DefaultState, stateOffset - m_CurrentControlStateBitOffset, stateSize, m_NoiseMask)))
							{
								continue;
							}
							m_CurrentControl = m_AllControls[controlIndex];
						}
						if ((m_Flags & global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IncludeNoisyControls) == 0 && m_CurrentControl.noisy)
						{
							m_CurrentControl = null;
							continue;
						}
						if ((m_Flags & global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IncludeSyntheticControls) == 0 && (m_CurrentControl.m_ControlFlags & (global::UnityEngine.InputSystem.InputControl.ControlFlags.IsSynthetic | global::UnityEngine.InputSystem.InputControl.ControlFlags.UsesStateFromOtherControl)) != 0)
						{
							m_CurrentControl = null;
							continue;
						}
						m_CurrentIndexInStateOffsetToControlIndexMap++;
						break;
					}
					if (m_CurrentControl != null)
					{
						if (m_MagnitudeThreshold == 0f)
						{
							break;
						}
						byte* statePtr = m_EventState - (m_CurrentControlStateBitOffset >> 3) - m_Device.m_StateBlock.byteOffset;
						float num4 = m_CurrentControl.EvaluateMagnitude(statePtr);
						if (!(num4 >= 0f) || !(num4 < m_MagnitudeThreshold))
						{
							break;
						}
					}
				}
				return true;
			}

			public unsafe void Reset()
			{
				if (!m_EventPtr.valid)
				{
					throw new global::System.ObjectDisposedException("Enumerator has already been disposed");
				}
				global::UnityEngine.InputSystem.Utilities.FourCC type = m_EventPtr.type;
				global::UnityEngine.InputSystem.Utilities.FourCC stateFormat;
				if (type == 1398030676)
				{
					global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(m_EventPtr);
					m_EventState = (byte*)ptr->state;
					m_EndBitOffset = ptr->stateSizeInBytes * 8;
					m_CurrentBitOffset = 0u;
					stateFormat = ptr->stateFormat;
				}
				else
				{
					if (!(type == 1145852993))
					{
						throw new global::System.NotSupportedException($"Cannot iterate over controls in event of type '{type}'");
					}
					global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent* ptr2 = global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent.FromUnchecked(m_EventPtr);
					m_EventState = (byte*)ptr2->deltaState - ptr2->stateOffset;
					m_CurrentBitOffset = ptr2->stateOffset * 8;
					m_EndBitOffset = m_CurrentBitOffset + ptr2->deltaStateSizeInBytes * 8;
					stateFormat = ptr2->stateFormat;
				}
				m_CurrentIndexInStateOffsetToControlIndexMap = 0;
				m_CurrentControlStateBitOffset = 0u;
				m_CurrentControl = null;
				if (!(stateFormat != m_Device.m_StateBlock.format))
				{
					return;
				}
				uint offset = 0u;
				if (m_Device.hasStateCallbacks && ((global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver)m_Device).GetStateOffsetForEvent(null, m_EventPtr, ref offset))
				{
					m_CurrentControlStateBitOffset = offset * 8;
					if (m_CurrentState != null)
					{
						m_CurrentState += offset;
					}
					if (m_DefaultState != null)
					{
						m_DefaultState += offset;
					}
					if (m_NoiseMask != null)
					{
						m_NoiseMask += offset;
					}
				}
				else if (!(m_Device is global::UnityEngine.InputSystem.Touchscreen) || !m_EventPtr.IsA<global::UnityEngine.InputSystem.LowLevel.StateEvent>() || !(global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(m_EventPtr)->stateFormat == global::UnityEngine.InputSystem.LowLevel.TouchState.Format))
				{
					throw new global::System.InvalidOperationException($"{type} event with state format {stateFormat} cannot be used with device '{m_Device}'");
				}
			}

			public void Dispose()
			{
				m_EventPtr = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr);
			}
		}

		public struct ControlBuilder
		{
			public global::UnityEngine.InputSystem.InputControl control { get; internal set; }

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder At(global::UnityEngine.InputSystem.InputDevice device, int index)
			{
				device.m_ChildrenForEachControl[index] = control;
				control.m_Device = device;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithParent(global::UnityEngine.InputSystem.InputControl parent)
			{
				control.m_Parent = parent;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithName(string name)
			{
				control.m_Name = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithDisplayName(string displayName)
			{
				control.m_DisplayNameFromLayout = new global::UnityEngine.InputSystem.Utilities.InternedString(displayName);
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithShortDisplayName(string shortDisplayName)
			{
				control.m_ShortDisplayNameFromLayout = new global::UnityEngine.InputSystem.Utilities.InternedString(shortDisplayName);
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithLayout(global::UnityEngine.InputSystem.Utilities.InternedString layout)
			{
				control.m_Layout = layout;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithUsages(int startIndex, int count)
			{
				control.m_UsageStartIndex = startIndex;
				control.m_UsageCount = count;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithAliases(int startIndex, int count)
			{
				control.m_AliasStartIndex = startIndex;
				control.m_AliasCount = count;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithChildren(int startIndex, int count)
			{
				control.m_ChildStartIndex = startIndex;
				control.m_ChildCount = count;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithStateBlock(global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock)
			{
				control.m_StateBlock = stateBlock;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithDefaultState(global::UnityEngine.InputSystem.Utilities.PrimitiveValue value)
			{
				control.m_DefaultState = value;
				control.m_Device.hasControlsWithDefaultState = true;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithMinAndMax(global::UnityEngine.InputSystem.Utilities.PrimitiveValue min, global::UnityEngine.InputSystem.Utilities.PrimitiveValue max)
			{
				control.m_MinValue = min;
				control.m_MaxValue = max;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder WithProcessor<TProcessor, TValue>(TProcessor processor) where TProcessor : global::UnityEngine.InputSystem.InputProcessor<TValue> where TValue : struct
			{
				((global::UnityEngine.InputSystem.InputControl<TValue>)control).m_ProcessorStack.Append(processor);
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder IsNoisy(bool value)
			{
				control.noisy = value;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder IsSynthetic(bool value)
			{
				control.synthetic = value;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder DontReset(bool value)
			{
				control.dontReset = value;
				if (value)
				{
					control.m_Device.hasDontResetControls = true;
				}
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder IsButton(bool value)
			{
				control.isButton = value;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public void Finish()
			{
				control.isSetupFinished = true;
			}
		}

		public struct DeviceBuilder
		{
			public global::UnityEngine.InputSystem.InputDevice device { get; internal set; }

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithName(string name)
			{
				device.m_Name = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithDisplayName(string displayName)
			{
				device.m_DisplayNameFromLayout = new global::UnityEngine.InputSystem.Utilities.InternedString(displayName);
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithShortDisplayName(string shortDisplayName)
			{
				device.m_ShortDisplayNameFromLayout = new global::UnityEngine.InputSystem.Utilities.InternedString(shortDisplayName);
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithLayout(global::UnityEngine.InputSystem.Utilities.InternedString layout)
			{
				device.m_Layout = layout;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithChildren(int startIndex, int count)
			{
				device.m_ChildStartIndex = startIndex;
				device.m_ChildCount = count;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithStateBlock(global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock)
			{
				device.m_StateBlock = stateBlock;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder IsNoisy(bool value)
			{
				device.noisy = value;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithControlUsage(int controlIndex, global::UnityEngine.InputSystem.Utilities.InternedString usage, global::UnityEngine.InputSystem.InputControl control)
			{
				device.m_UsagesForEachControl[controlIndex] = usage;
				device.m_UsageToControl[controlIndex] = control;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithControlAlias(int controlIndex, global::UnityEngine.InputSystem.Utilities.InternedString alias)
			{
				device.m_AliasesForEachControl[controlIndex] = alias;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithStateOffsetToControlIndexMap(uint[] map)
			{
				device.m_StateOffsetToControlMap = map;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public unsafe global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder WithControlTree(byte[] controlTreeNodes, ushort[] controlTreeIndicies)
			{
				int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode>();
				int num2 = controlTreeNodes.Length / num;
				device.m_ControlTreeNodes = new global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode[num2];
				fixed (byte* ptr = controlTreeNodes)
				{
					for (int i = 0; i < num2; i++)
					{
						device.m_ControlTreeNodes[i] = *(global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode*)(ptr + i * num);
					}
				}
				device.m_ControlTreeIndices = controlTreeIndicies;
				return this;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public void Finish()
			{
				int num = 0;
				foreach (global::UnityEngine.InputSystem.InputControl allControl in device.allControls)
				{
					if (allControl is global::UnityEngine.InputSystem.Controls.ButtonControl)
					{
						num++;
					}
				}
				device.m_ButtonControlsCheckingPressState = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Controls.ButtonControl>(num);
				device.m_UpdatedButtons = new global::System.Collections.Generic.HashSet<int>(num);
				device.isSetupFinished = true;
			}
		}

		public static TControl FindInParentChain<TControl>(this global::UnityEngine.InputSystem.InputControl control) where TControl : global::UnityEngine.InputSystem.InputControl
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			for (global::UnityEngine.InputSystem.InputControl inputControl = control; inputControl != null; inputControl = inputControl.parent)
			{
				if (inputControl is TControl result)
				{
					return result;
				}
			}
			return null;
		}

		public static bool IsPressed(this global::UnityEngine.InputSystem.InputControl control, float buttonPressPoint = 0f)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (global::UnityEngine.Mathf.Approximately(0f, buttonPressPoint))
			{
				buttonPressPoint = ((!(control is global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl)) ? global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint : buttonControl.pressPointOrDefault);
			}
			return control.IsActuated(buttonPressPoint);
		}

		public static bool IsActuated(this global::UnityEngine.InputSystem.InputControl control, float threshold = 0f)
		{
			if (control.CheckStateIsAtDefault())
			{
				return false;
			}
			float magnitude = control.magnitude;
			if (magnitude < 0f)
			{
				if (global::UnityEngine.Mathf.Approximately(threshold, 0f))
				{
					return true;
				}
				return false;
			}
			if (global::UnityEngine.Mathf.Approximately(threshold, 0f))
			{
				return magnitude > 0f;
			}
			return magnitude >= threshold;
		}

		public unsafe static object ReadValueAsObject(this global::UnityEngine.InputSystem.InputControl control)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			return control.ReadValueFromStateAsObject(control.currentStatePtr);
		}

		public unsafe static void ReadValueIntoBuffer(this global::UnityEngine.InputSystem.InputControl control, void* buffer, int bufferSize)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (buffer == null)
			{
				throw new global::System.ArgumentNullException("buffer");
			}
			control.ReadValueFromStateIntoBuffer(control.currentStatePtr, buffer, bufferSize);
		}

		public unsafe static object ReadDefaultValueAsObject(this global::UnityEngine.InputSystem.InputControl control)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			return control.ReadValueFromStateAsObject(control.defaultStatePtr);
		}

		public static TValue ReadValueFromEvent<TValue>(this global::UnityEngine.InputSystem.InputControl<TValue> control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (!control.ReadValueFromEvent(inputEvent, out var value))
			{
				return default(TValue);
			}
			return value;
		}

		public unsafe static bool ReadValueFromEvent<TValue>(this global::UnityEngine.InputSystem.InputControl<TValue> control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent, out TValue value) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtrFromStateEvent == null)
			{
				value = control.ReadDefaultValue();
				return false;
			}
			value = control.ReadValueFromState(statePtrFromStateEvent);
			return true;
		}

		public unsafe static object ReadValueFromEventAsObject(this global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtrFromStateEvent == null)
			{
				return control.ReadDefaultValueAsObject();
			}
			return control.ReadValueFromStateAsObject(statePtrFromStateEvent);
		}

		public static TValue ReadUnprocessedValueFromEvent<TValue>(this global::UnityEngine.InputSystem.InputControl<TValue> control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			TValue value = default(TValue);
			control.ReadUnprocessedValueFromEvent(eventPtr, out value);
			return value;
		}

		public unsafe static bool ReadUnprocessedValueFromEvent<TValue>(this global::UnityEngine.InputSystem.InputControl<TValue> control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEvent, out TValue value) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtrFromStateEvent == null)
			{
				value = control.ReadDefaultValue();
				return false;
			}
			value = control.ReadUnprocessedValueFromState(statePtrFromStateEvent);
			return true;
		}

		public unsafe static void WriteValueFromObjectIntoEvent(this global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, object value)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent != null)
			{
				control.WriteValueFromObjectIntoState(value, statePtrFromStateEvent);
			}
		}

		public unsafe static void WriteValueIntoState(this global::UnityEngine.InputSystem.InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			int valueSizeInBytes = control.valueSizeInBytes;
			void* ptr = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(valueSizeInBytes, 8, global::Unity.Collections.Allocator.Temp);
			try
			{
				control.ReadValueFromStateIntoBuffer(control.currentStatePtr, ptr, valueSizeInBytes);
				control.WriteValueFromBufferIntoState(ptr, valueSizeInBytes, statePtr);
			}
			finally
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(ptr, global::Unity.Collections.Allocator.Temp);
			}
		}

		public unsafe static void WriteValueIntoState<TValue>(this global::UnityEngine.InputSystem.InputControl control, TValue value, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (!(control is global::UnityEngine.InputSystem.InputControl<TValue> inputControl))
			{
				throw new global::System.ArgumentException("Expecting control of type '" + typeof(TValue).Name + "' but got '" + control.GetType().Name + "'");
			}
			inputControl.WriteValueIntoState(value, statePtr);
		}

		public unsafe static void WriteValueIntoState<TValue>(this global::UnityEngine.InputSystem.InputControl<TValue> control, TValue value, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			void* bufferPtr = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref value);
			int bufferSize = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>();
			control.WriteValueFromBufferIntoState(bufferPtr, bufferSize, statePtr);
		}

		public unsafe static void WriteValueIntoState<TValue>(this global::UnityEngine.InputSystem.InputControl<TValue> control, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			control.WriteValueIntoState(control.ReadValue(), statePtr);
		}

		public unsafe static void WriteValueIntoState<TValue, TState>(this global::UnityEngine.InputSystem.InputControl<TValue> control, TValue value, ref TState state) where TValue : struct where TState : struct, global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TState>();
			if (control.stateOffsetRelativeToDeviceRoot + control.m_StateBlock.alignedSizeInBytes >= num)
			{
				throw new global::System.ArgumentException($"Control {control.path} with offset {control.stateOffsetRelativeToDeviceRoot} and size of {control.m_StateBlock.sizeInBits} bits is out of bounds for state of type {typeof(TState).Name} with size {num}", "state");
			}
			byte* statePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref state);
			control.WriteValueIntoState(value, statePtr);
		}

		public static void WriteValueIntoEvent<TValue>(this global::UnityEngine.InputSystem.InputControl control, TValue value, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new global::System.ArgumentNullException("eventPtr");
			}
			if (!(control is global::UnityEngine.InputSystem.InputControl<TValue> control2))
			{
				throw new global::System.ArgumentException("Expecting control of type '" + typeof(TValue).Name + "' but got '" + control.GetType().Name + "'");
			}
			control2.WriteValueIntoEvent(value, eventPtr);
		}

		public unsafe static void WriteValueIntoEvent<TValue>(this global::UnityEngine.InputSystem.InputControl<TValue> control, TValue value, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new global::System.ArgumentNullException("eventPtr");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent != null)
			{
				control.WriteValueIntoState(value, statePtrFromStateEvent);
			}
		}

		public unsafe static void CopyState(this global::UnityEngine.InputSystem.InputDevice device, void* buffer, int bufferSizeInBytes)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (bufferSizeInBytes <= 0)
			{
				throw new global::System.ArgumentException("bufferSizeInBytes must be positive", "bufferSizeInBytes");
			}
			global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = device.m_StateBlock;
			long size = global::System.Math.Min(bufferSizeInBytes, stateBlock.alignedSizeInBytes);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(buffer, (byte*)device.currentStatePtr + stateBlock.byteOffset, size);
		}

		public unsafe static void CopyState<TState>(this global::UnityEngine.InputSystem.InputDevice device, out TState state) where TState : struct, global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			state = default(TState);
			if (device.stateBlock.format != state.format)
			{
				throw new global::System.ArgumentException($"Struct '{typeof(TState).Name}' has state format '{state.format}' which doesn't match device '{device}' with state format '{device.stateBlock.format}'", "TState");
			}
			int bufferSizeInBytes = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TState>();
			void* buffer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref state);
			device.CopyState(buffer, bufferSizeInBytes);
		}

		public unsafe static bool CheckStateIsAtDefault(this global::UnityEngine.InputSystem.InputControl control)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			return control.CheckStateIsAtDefault(control.currentStatePtr, null);
		}

		public unsafe static bool CheckStateIsAtDefault(this global::UnityEngine.InputSystem.InputControl control, void* statePtr, void* maskPtr = null)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			return control.CompareState(statePtr, control.defaultStatePtr, maskPtr);
		}

		public unsafe static bool CheckStateIsAtDefaultIgnoringNoise(this global::UnityEngine.InputSystem.InputControl control)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			return control.CheckStateIsAtDefaultIgnoringNoise(control.currentStatePtr);
		}

		public unsafe static bool CheckStateIsAtDefaultIgnoringNoise(this global::UnityEngine.InputSystem.InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			return control.CheckStateIsAtDefault(statePtr, global::UnityEngine.InputSystem.LowLevel.InputStateBuffers.s_NoiseMaskBuffer);
		}

		public unsafe static bool CompareStateIgnoringNoise(this global::UnityEngine.InputSystem.InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			return control.CompareState(control.currentStatePtr, statePtr, control.noiseMaskPtr);
		}

		public unsafe static bool CompareState(this global::UnityEngine.InputSystem.InputControl control, void* firstStatePtr, void* secondStatePtr, void* maskPtr = null)
		{
			byte* ptr = (byte*)firstStatePtr + (int)control.m_StateBlock.byteOffset;
			byte* ptr2 = (byte*)secondStatePtr + (int)control.m_StateBlock.byteOffset;
			byte* ptr3 = ((maskPtr != null) ? ((byte*)maskPtr + (int)control.m_StateBlock.byteOffset) : null);
			if (control.m_StateBlock.sizeInBits == 1)
			{
				if (ptr3 != null && global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr3, control.m_StateBlock.bitOffset))
				{
					return true;
				}
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr2, control.m_StateBlock.bitOffset) == global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr, control.m_StateBlock.bitOffset);
			}
			return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.MemCmpBitRegion(ptr, ptr2, control.m_StateBlock.bitOffset, control.m_StateBlock.sizeInBits, ptr3);
		}

		public unsafe static bool CompareState(this global::UnityEngine.InputSystem.InputControl control, void* statePtr, void* maskPtr = null)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			return control.CompareState(control.currentStatePtr, statePtr, maskPtr);
		}

		public unsafe static bool HasValueChangeInState(this global::UnityEngine.InputSystem.InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new global::System.ArgumentNullException("statePtr");
			}
			return control.CompareValue(control.currentStatePtr, statePtr);
		}

		public unsafe static bool HasValueChangeInEvent(this global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new global::System.ArgumentNullException("eventPtr");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent == null)
			{
				return false;
			}
			return control.CompareValue(control.currentStatePtr, statePtrFromStateEvent);
		}

		public unsafe static void* GetStatePtrFromStateEvent(this global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new global::System.ArgumentNullException("eventPtr");
			}
			return control.GetStatePtrFromStateEventUnchecked(eventPtr, eventPtr.type);
		}

		internal unsafe static void* GetStatePtrFromStateEventUnchecked(this global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.Utilities.FourCC eventType)
		{
			global::UnityEngine.InputSystem.Utilities.FourCC stateFormat;
			uint num;
			void* ptr;
			uint offset;
			if (eventType == 1398030676)
			{
				global::UnityEngine.InputSystem.LowLevel.StateEvent* intPtr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr);
				offset = 0u;
				stateFormat = intPtr->stateFormat;
				num = intPtr->stateSizeInBytes;
				ptr = intPtr->state;
			}
			else
			{
				if (!(eventType == 1145852993))
				{
					throw new global::System.ArgumentException($"Event must be a StateEvent or DeltaStateEvent but is a {eventType} instead", "eventPtr");
				}
				global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent* intPtr2 = global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent.FromUnchecked(eventPtr);
				offset = intPtr2->stateOffset;
				stateFormat = intPtr2->stateFormat;
				num = intPtr2->deltaStateSizeInBytes;
				ptr = intPtr2->deltaState;
			}
			global::UnityEngine.InputSystem.InputDevice device = control.device;
			if (stateFormat != device.m_StateBlock.format && (!device.hasStateCallbacks || !((global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver)device).GetStateOffsetForEvent(control, eventPtr, ref offset)))
			{
				return null;
			}
			offset += device.m_StateBlock.byteOffset;
			ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = ref control.m_StateBlock;
			long num2 = (int)stateBlock.effectiveByteOffset - offset;
			if (num2 < 0 || num2 + stateBlock.alignedSizeInBytes > num)
			{
				return null;
			}
			return (byte*)ptr - (int)offset;
		}

		public unsafe static bool ResetToDefaultStateInEvent(this global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new global::System.ArgumentNullException("eventPtr");
			}
			global::UnityEngine.InputSystem.Utilities.FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				throw new global::System.ArgumentException("Given event is not a StateEvent or a DeltaStateEvent", "eventPtr");
			}
			byte* statePtrFromStateEvent = (byte*)control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent == null)
			{
				return false;
			}
			byte* defaultStatePtr = (byte*)control.defaultStatePtr;
			ref global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = ref control.m_StateBlock;
			uint byteOffset = stateBlock.byteOffset;
			global::UnityEngine.InputSystem.Utilities.MemoryHelpers.MemCpyBitRegion(statePtrFromStateEvent + byteOffset, defaultStatePtr + byteOffset, stateBlock.bitOffset, stateBlock.sizeInBits);
			return true;
		}

		public static void QueueValueChange<TValue>(this global::UnityEngine.InputSystem.InputControl<TValue> control, TValue value, double time = -1.0) where TValue : struct
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr;
			using (global::UnityEngine.InputSystem.LowLevel.StateEvent.From(control.device, out eventPtr))
			{
				if (time >= 0.0)
				{
					eventPtr.time = time;
				}
				control.WriteValueIntoEvent(value, eventPtr);
				global::UnityEngine.InputSystem.InputSystem.QueueEvent(eventPtr);
			}
		}

		public unsafe static void AccumulateValueInEvent(this global::UnityEngine.InputSystem.InputControl<float> control, void* currentStatePtr, global::UnityEngine.InputSystem.LowLevel.InputEventPtr newState)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (control.ReadUnprocessedValueFromEvent(newState, out var value))
			{
				float num = control.ReadUnprocessedValueFromState(currentStatePtr);
				control.WriteValueIntoEvent(num + value, newState);
			}
		}

		internal unsafe static void AccumulateValueInEvent(this global::UnityEngine.InputSystem.InputControl<global::UnityEngine.Vector2> control, void* currentStatePtr, global::UnityEngine.InputSystem.LowLevel.InputEventPtr newState)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (control.ReadUnprocessedValueFromEvent(newState, out var value))
			{
				global::UnityEngine.Vector2 vector = control.ReadUnprocessedValueFromState(currentStatePtr);
				control.WriteValueIntoEvent(vector + value, newState);
			}
		}

		public static void FindControlsRecursive<TControl>(this global::UnityEngine.InputSystem.InputControl parent, global::System.Collections.Generic.IList<TControl> controls, global::System.Func<TControl, bool> predicate) where TControl : global::UnityEngine.InputSystem.InputControl
		{
			if (parent == null)
			{
				throw new global::System.ArgumentNullException("parent");
			}
			if (controls == null)
			{
				throw new global::System.ArgumentNullException("controls");
			}
			if (predicate == null)
			{
				throw new global::System.ArgumentNullException("predicate");
			}
			if (parent is TControl val && predicate(val))
			{
				controls.Add(val);
			}
			int count = parent.children.Count;
			for (int i = 0; i < count; i++)
			{
				parent.children[i].FindControlsRecursive(controls, predicate);
			}
		}

		internal static string BuildPath(this global::UnityEngine.InputSystem.InputControl control, string deviceLayout, global::System.Text.StringBuilder builder = null)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (string.IsNullOrEmpty(deviceLayout))
			{
				throw new global::System.ArgumentNullException("deviceLayout");
			}
			if (builder == null)
			{
				builder = new global::System.Text.StringBuilder();
			}
			global::UnityEngine.InputSystem.InputDevice device = control.device;
			builder.Append('<');
			builder.Append(global::UnityEngine.InputSystem.Utilities.StringHelpers.Escape(deviceLayout, "\\>", "\\>"));
			builder.Append('>');
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString> usages = device.usages;
			for (int i = 0; i < usages.Count; i++)
			{
				builder.Append('{');
				builder.Append(global::UnityEngine.InputSystem.Utilities.StringHelpers.Escape(usages[i].ToString(), "\\}", "\\}"));
				builder.Append('}');
			}
			builder.Append('/');
			string text = device.path.Replace("\\", "\\\\");
			string text2 = control.path.Replace("\\", "\\\\");
			builder.Append(text2, text.Length + 1, text2.Length - text.Length - 1);
			return builder.ToString();
		}

		public static global::UnityEngine.InputSystem.InputControlExtensions.InputEventControlCollection EnumerateControls(this global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.InputControlExtensions.Enumerate flags, global::UnityEngine.InputSystem.InputDevice device = null, float magnitudeThreshold = 0f)
		{
			if (!eventPtr.valid)
			{
				throw new global::System.ArgumentNullException("eventPtr", "Given event pointer must not be null");
			}
			global::UnityEngine.InputSystem.Utilities.FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				throw new global::System.ArgumentException($"Event must be a StateEvent or DeltaStateEvent but is a {type} instead", "eventPtr");
			}
			if (device == null)
			{
				int deviceId = eventPtr.deviceId;
				device = global::UnityEngine.InputSystem.InputSystem.GetDeviceById(deviceId);
				if (device == null)
				{
					throw new global::System.ArgumentException($"Cannot find device with ID {deviceId} referenced by event", "eventPtr");
				}
			}
			return new global::UnityEngine.InputSystem.InputControlExtensions.InputEventControlCollection
			{
				m_Device = device,
				m_EventPtr = eventPtr,
				m_Flags = flags,
				m_MagnitudeThreshold = magnitudeThreshold
			};
		}

		public static global::UnityEngine.InputSystem.InputControlExtensions.InputEventControlCollection EnumerateChangedControls(this global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.InputDevice device = null, float magnitudeThreshold = 0f)
		{
			return eventPtr.EnumerateControls(global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IgnoreControlsInCurrentState, device, magnitudeThreshold);
		}

		public static bool HasButtonPress(this global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			return eventPtr.GetFirstButtonPressOrNull(magnitude, buttonControlsOnly) != null;
		}

		public static global::UnityEngine.InputSystem.InputControl GetFirstButtonPressOrNull(this global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			if (eventPtr.type != 1398030676 && eventPtr.type != 1145852993)
			{
				return null;
			}
			if (magnitude < 0f)
			{
				magnitude = global::UnityEngine.InputSystem.InputSystem.settings.defaultButtonPressPoint;
			}
			foreach (global::UnityEngine.InputSystem.InputControl item in eventPtr.EnumerateControls(global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, null, magnitude))
			{
				if (item.HasValueChangeInEvent(eventPtr) && (!buttonControlsOnly || item.isButton))
				{
					return item;
				}
			}
			return null;
		}

		public static global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputControl> GetAllButtonPresses(this global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			if (eventPtr.type != 1398030676 && eventPtr.type != 1145852993)
			{
				yield break;
			}
			if (magnitude < 0f)
			{
				magnitude = global::UnityEngine.InputSystem.InputSystem.settings.defaultButtonPressPoint;
			}
			foreach (global::UnityEngine.InputSystem.InputControl item in eventPtr.EnumerateControls(global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, null, magnitude))
			{
				if (!buttonControlsOnly || item.isButton)
				{
					yield return item;
				}
			}
		}

		public static global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder Setup(this global::UnityEngine.InputSystem.InputControl control)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			if (control.isSetupFinished)
			{
				throw new global::System.InvalidOperationException($"The setup of {control} cannot be modified; control is already in use");
			}
			return new global::UnityEngine.InputSystem.InputControlExtensions.ControlBuilder
			{
				control = control
			};
		}

		public static global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder Setup(this global::UnityEngine.InputSystem.InputDevice device, int controlCount, int usageCount, int aliasCount)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (device.isSetupFinished)
			{
				throw new global::System.InvalidOperationException($"The setup of {device} cannot be modified; control is already in use");
			}
			if (controlCount < 1)
			{
				throw new global::System.ArgumentOutOfRangeException("controlCount");
			}
			if (usageCount < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("usageCount");
			}
			if (aliasCount < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("aliasCount");
			}
			device.m_Device = device;
			device.m_ChildrenForEachControl = new global::UnityEngine.InputSystem.InputControl[controlCount];
			if (usageCount > 0)
			{
				device.m_UsagesForEachControl = new global::UnityEngine.InputSystem.Utilities.InternedString[usageCount];
				device.m_UsageToControl = new global::UnityEngine.InputSystem.InputControl[usageCount];
			}
			if (aliasCount > 0)
			{
				device.m_AliasesForEachControl = new global::UnityEngine.InputSystem.Utilities.InternedString[aliasCount];
			}
			return new global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder
			{
				device = device
			};
		}
	}
}
