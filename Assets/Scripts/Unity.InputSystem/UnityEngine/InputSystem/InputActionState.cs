namespace UnityEngine.InputSystem
{
	internal class InputActionState : global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor, global::System.ICloneable, global::System.IDisposable
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 48)]
		internal struct InteractionState
		{
			[global::System.Flags]
			private enum Flags
			{
				TimerRunning = 1
			}

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			private ushort m_TriggerControlIndex;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			private byte m_Phase;

			[global::System.Runtime.InteropServices.FieldOffset(3)]
			private byte m_Flags;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			private float m_TimerDuration;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			private double m_StartTime;

			[global::System.Runtime.InteropServices.FieldOffset(16)]
			private double m_TimerStartTime;

			[global::System.Runtime.InteropServices.FieldOffset(24)]
			private double m_PerformedTime;

			[global::System.Runtime.InteropServices.FieldOffset(32)]
			private float m_TotalTimeoutCompletionTimeDone;

			[global::System.Runtime.InteropServices.FieldOffset(36)]
			private float m_TotalTimeoutCompletionTimeRemaining;

			[global::System.Runtime.InteropServices.FieldOffset(40)]
			private long m_TimerMonitorIndex;

			public int triggerControlIndex
			{
				get
				{
					if (m_TriggerControlIndex == ushort.MaxValue)
					{
						return -1;
					}
					return m_TriggerControlIndex;
				}
				set
				{
					if (value == -1)
					{
						m_TriggerControlIndex = ushort.MaxValue;
						return;
					}
					if (value < 0 || value >= 65535)
					{
						throw new global::System.NotSupportedException("More than ushort.MaxValue-1 controls in a single InputActionState");
					}
					m_TriggerControlIndex = (ushort)value;
				}
			}

			public double startTime
			{
				get
				{
					return m_StartTime;
				}
				set
				{
					m_StartTime = value;
				}
			}

			public double performedTime
			{
				get
				{
					return m_PerformedTime;
				}
				set
				{
					m_PerformedTime = value;
				}
			}

			public double timerStartTime
			{
				get
				{
					return m_TimerStartTime;
				}
				set
				{
					m_TimerStartTime = value;
				}
			}

			public float timerDuration
			{
				get
				{
					return m_TimerDuration;
				}
				set
				{
					m_TimerDuration = value;
				}
			}

			public float totalTimeoutCompletionDone
			{
				get
				{
					return m_TotalTimeoutCompletionTimeDone;
				}
				set
				{
					m_TotalTimeoutCompletionTimeDone = value;
				}
			}

			public float totalTimeoutCompletionTimeRemaining
			{
				get
				{
					return m_TotalTimeoutCompletionTimeRemaining;
				}
				set
				{
					m_TotalTimeoutCompletionTimeRemaining = value;
				}
			}

			public long timerMonitorIndex
			{
				get
				{
					return m_TimerMonitorIndex;
				}
				set
				{
					m_TimerMonitorIndex = value;
				}
			}

			public bool isTimerRunning
			{
				get
				{
					return (m_Flags & 1) == 1;
				}
				set
				{
					if (value)
					{
						m_Flags |= 1;
						return;
					}
					global::UnityEngine.InputSystem.InputActionState.InteractionState.Flags flags = ~global::UnityEngine.InputSystem.InputActionState.InteractionState.Flags.TimerRunning;
					m_Flags &= (byte)flags;
				}
			}

			public global::UnityEngine.InputSystem.InputActionPhase phase
			{
				get
				{
					return (global::UnityEngine.InputSystem.InputActionPhase)m_Phase;
				}
				set
				{
					m_Phase = (byte)value;
				}
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 32)]
		internal struct BindingState
		{
			[global::System.Flags]
			public enum Flags
			{
				ChainsWithNext = 1,
				EndOfChain = 2,
				Composite = 4,
				PartOfComposite = 8,
				InitialStateCheckPending = 0x10,
				WantsInitialStateCheck = 0x20
			}

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			private byte m_ControlCount;

			[global::System.Runtime.InteropServices.FieldOffset(1)]
			private byte m_InteractionCount;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			private byte m_ProcessorCount;

			[global::System.Runtime.InteropServices.FieldOffset(3)]
			private byte m_MapIndex;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			private byte m_Flags;

			[global::System.Runtime.InteropServices.FieldOffset(5)]
			private byte m_PartIndex;

			[global::System.Runtime.InteropServices.FieldOffset(6)]
			private ushort m_ActionIndex;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			private ushort m_CompositeOrCompositeBindingIndex;

			[global::System.Runtime.InteropServices.FieldOffset(10)]
			private ushort m_ProcessorStartIndex;

			[global::System.Runtime.InteropServices.FieldOffset(12)]
			private ushort m_InteractionStartIndex;

			[global::System.Runtime.InteropServices.FieldOffset(14)]
			private ushort m_ControlStartIndex;

			[global::System.Runtime.InteropServices.FieldOffset(16)]
			private double m_PressTime;

			[global::System.Runtime.InteropServices.FieldOffset(24)]
			private int m_TriggerEventIdForComposite;

			[global::System.Runtime.InteropServices.FieldOffset(28)]
			private int __padding;

			public int controlStartIndex
			{
				get
				{
					return m_ControlStartIndex;
				}
				set
				{
					if (value >= 65535)
					{
						throw new global::System.NotSupportedException("Total control count in state cannot exceed byte.MaxValue=" + ushort.MaxValue);
					}
					m_ControlStartIndex = (ushort)value;
				}
			}

			public int controlCount
			{
				get
				{
					return m_ControlCount;
				}
				set
				{
					if (value >= 255)
					{
						throw new global::System.NotSupportedException("Control count per binding cannot exceed byte.MaxValue=" + byte.MaxValue);
					}
					m_ControlCount = (byte)value;
				}
			}

			public int interactionStartIndex
			{
				get
				{
					if (m_InteractionStartIndex == ushort.MaxValue)
					{
						return -1;
					}
					return m_InteractionStartIndex;
				}
				set
				{
					if (value == -1)
					{
						m_InteractionStartIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new global::System.NotSupportedException("Interaction count cannot exceed ushort.MaxValue=" + ushort.MaxValue);
					}
					m_InteractionStartIndex = (ushort)value;
				}
			}

			public int interactionCount
			{
				get
				{
					return m_InteractionCount;
				}
				set
				{
					if (value >= 255)
					{
						throw new global::System.NotSupportedException("Interaction count per binding cannot exceed byte.MaxValue=" + byte.MaxValue);
					}
					m_InteractionCount = (byte)value;
				}
			}

			public int processorStartIndex
			{
				get
				{
					if (m_ProcessorStartIndex == ushort.MaxValue)
					{
						return -1;
					}
					return m_ProcessorStartIndex;
				}
				set
				{
					if (value == -1)
					{
						m_ProcessorStartIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new global::System.NotSupportedException("Processor count cannot exceed ushort.MaxValue=" + ushort.MaxValue);
					}
					m_ProcessorStartIndex = (ushort)value;
				}
			}

			public int processorCount
			{
				get
				{
					return m_ProcessorCount;
				}
				set
				{
					if (value >= 255)
					{
						throw new global::System.NotSupportedException("Processor count per binding cannot exceed byte.MaxValue=" + byte.MaxValue);
					}
					m_ProcessorCount = (byte)value;
				}
			}

			public int actionIndex
			{
				get
				{
					if (m_ActionIndex == ushort.MaxValue)
					{
						return -1;
					}
					return m_ActionIndex;
				}
				set
				{
					if (value == -1)
					{
						m_ActionIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new global::System.NotSupportedException("Action count cannot exceed ushort.MaxValue=" + ushort.MaxValue);
					}
					m_ActionIndex = (ushort)value;
				}
			}

			public int mapIndex
			{
				get
				{
					return m_MapIndex;
				}
				set
				{
					if (value >= 255)
					{
						throw new global::System.NotSupportedException("Map count cannot exceed byte.MaxValue=" + byte.MaxValue);
					}
					m_MapIndex = (byte)value;
				}
			}

			public int compositeOrCompositeBindingIndex
			{
				get
				{
					if (m_CompositeOrCompositeBindingIndex == ushort.MaxValue)
					{
						return -1;
					}
					return m_CompositeOrCompositeBindingIndex;
				}
				set
				{
					if (value == -1)
					{
						m_CompositeOrCompositeBindingIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new global::System.NotSupportedException("Composite count cannot exceed ushort.MaxValue=" + ushort.MaxValue);
					}
					m_CompositeOrCompositeBindingIndex = (ushort)value;
				}
			}

			public int triggerEventIdForComposite
			{
				get
				{
					return m_TriggerEventIdForComposite;
				}
				set
				{
					m_TriggerEventIdForComposite = value;
				}
			}

			public double pressTime
			{
				get
				{
					return m_PressTime;
				}
				set
				{
					m_PressTime = value;
				}
			}

			public global::UnityEngine.InputSystem.InputActionState.BindingState.Flags flags
			{
				get
				{
					return (global::UnityEngine.InputSystem.InputActionState.BindingState.Flags)m_Flags;
				}
				set
				{
					m_Flags = (byte)value;
				}
			}

			public bool chainsWithNext
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.ChainsWithNext) == global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.ChainsWithNext;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.ChainsWithNext;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.ChainsWithNext;
					}
				}
			}

			public bool isEndOfChain
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.EndOfChain) == global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.EndOfChain;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.EndOfChain;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.EndOfChain;
					}
				}
			}

			public bool isPartOfChain
			{
				get
				{
					if (!chainsWithNext)
					{
						return isEndOfChain;
					}
					return true;
				}
			}

			public bool isComposite
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.Composite) == global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.Composite;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.Composite;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.Composite;
					}
				}
			}

			public bool isPartOfComposite
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.PartOfComposite) == global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.PartOfComposite;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.PartOfComposite;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.PartOfComposite;
					}
				}
			}

			public bool initialStateCheckPending
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.InitialStateCheckPending) != 0;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.InitialStateCheckPending;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.InitialStateCheckPending;
					}
				}
			}

			public bool wantsInitialStateCheck
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.WantsInitialStateCheck) != 0;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.WantsInitialStateCheck;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.BindingState.Flags.WantsInitialStateCheck;
					}
				}
			}

			public int partIndex
			{
				get
				{
					return m_PartIndex;
				}
				set
				{
					if (partIndex < 0)
					{
						throw new global::System.ArgumentOutOfRangeException("value", "Part index must not be negative");
					}
					if (partIndex > 255)
					{
						throw new global::System.InvalidOperationException("Part count must not exceed byte.MaxValue=" + byte.MaxValue);
					}
					m_PartIndex = (byte)value;
				}
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 56)]
		public struct TriggerState
		{
			[global::System.Flags]
			public enum Flags
			{
				HaveMagnitude = 1,
				PassThrough = 2,
				MayNeedConflictResolution = 4,
				HasMultipleConcurrentActuations = 8,
				InProcessing = 0x10,
				Button = 0x20,
				Pressed = 0x40
			}

			public const int kMaxNumMaps = 255;

			public const int kMaxNumControls = 65535;

			public const int kMaxNumBindings = 65535;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			private byte m_Phase;

			[global::System.Runtime.InteropServices.FieldOffset(1)]
			private byte m_Flags;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			private byte m_MapIndex;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			private ushort m_ControlIndex;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			private double m_Time;

			[global::System.Runtime.InteropServices.FieldOffset(16)]
			private double m_StartTime;

			[global::System.Runtime.InteropServices.FieldOffset(24)]
			private ushort m_BindingIndex;

			[global::System.Runtime.InteropServices.FieldOffset(26)]
			private ushort m_InteractionIndex;

			[global::System.Runtime.InteropServices.FieldOffset(28)]
			private float m_Magnitude;

			[global::System.Runtime.InteropServices.FieldOffset(32)]
			private uint m_LastPerformedInUpdate;

			[global::System.Runtime.InteropServices.FieldOffset(36)]
			private uint m_LastCanceledInUpdate;

			[global::System.Runtime.InteropServices.FieldOffset(40)]
			private uint m_PressedInUpdate;

			[global::System.Runtime.InteropServices.FieldOffset(44)]
			private uint m_ReleasedInUpdate;

			[global::System.Runtime.InteropServices.FieldOffset(48)]
			private uint m_LastCompletedInUpdate;

			[global::System.Runtime.InteropServices.FieldOffset(52)]
			internal int framePerformed;

			[global::System.Runtime.InteropServices.FieldOffset(56)]
			internal int framePressed;

			[global::System.Runtime.InteropServices.FieldOffset(60)]
			internal int frameReleased;

			[global::System.Runtime.InteropServices.FieldOffset(64)]
			internal int frameCompleted;

			public global::UnityEngine.InputSystem.InputActionPhase phase
			{
				get
				{
					return (global::UnityEngine.InputSystem.InputActionPhase)m_Phase;
				}
				set
				{
					m_Phase = (byte)value;
				}
			}

			public bool isDisabled => phase == global::UnityEngine.InputSystem.InputActionPhase.Disabled;

			public bool isWaiting => phase == global::UnityEngine.InputSystem.InputActionPhase.Waiting;

			public bool isStarted => phase == global::UnityEngine.InputSystem.InputActionPhase.Started;

			public bool isPerformed => phase == global::UnityEngine.InputSystem.InputActionPhase.Performed;

			public bool isCanceled => phase == global::UnityEngine.InputSystem.InputActionPhase.Canceled;

			public double time
			{
				get
				{
					return m_Time;
				}
				set
				{
					m_Time = value;
				}
			}

			public double startTime
			{
				get
				{
					return m_StartTime;
				}
				set
				{
					m_StartTime = value;
				}
			}

			public float magnitude
			{
				get
				{
					return m_Magnitude;
				}
				set
				{
					flags |= global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.HaveMagnitude;
					m_Magnitude = value;
				}
			}

			public bool haveMagnitude => (flags & global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.HaveMagnitude) != 0;

			public int mapIndex
			{
				get
				{
					return m_MapIndex;
				}
				set
				{
					if (value < 0 || value > 255)
					{
						throw new global::System.NotSupportedException("More than byte.MaxValue InputActionMaps in a single InputActionState");
					}
					m_MapIndex = (byte)value;
				}
			}

			public int controlIndex
			{
				get
				{
					if (m_ControlIndex == ushort.MaxValue)
					{
						return -1;
					}
					return m_ControlIndex;
				}
				set
				{
					if (value == -1)
					{
						m_ControlIndex = ushort.MaxValue;
						return;
					}
					if (value < 0 || value >= 65535)
					{
						throw new global::System.NotSupportedException("More than ushort.MaxValue-1 controls in a single InputActionState");
					}
					m_ControlIndex = (ushort)value;
				}
			}

			public int bindingIndex
			{
				get
				{
					return m_BindingIndex;
				}
				set
				{
					if (value < 0 || value > 65535)
					{
						throw new global::System.NotSupportedException("More than ushort.MaxValue bindings in a single InputActionState");
					}
					m_BindingIndex = (ushort)value;
				}
			}

			public int interactionIndex
			{
				get
				{
					if (m_InteractionIndex == ushort.MaxValue)
					{
						return -1;
					}
					return m_InteractionIndex;
				}
				set
				{
					if (value == -1)
					{
						m_InteractionIndex = ushort.MaxValue;
						return;
					}
					if (value < 0 || value >= 65535)
					{
						throw new global::System.NotSupportedException("More than ushort.MaxValue-1 interactions in a single InputActionState");
					}
					m_InteractionIndex = (ushort)value;
				}
			}

			public uint lastPerformedInUpdate
			{
				get
				{
					return m_LastPerformedInUpdate;
				}
				set
				{
					m_LastPerformedInUpdate = value;
				}
			}

			public uint lastCompletedInUpdate
			{
				get
				{
					return m_LastCompletedInUpdate;
				}
				set
				{
					m_LastCompletedInUpdate = value;
				}
			}

			public uint lastCanceledInUpdate
			{
				get
				{
					return m_LastCanceledInUpdate;
				}
				set
				{
					m_LastCanceledInUpdate = value;
				}
			}

			public uint pressedInUpdate
			{
				get
				{
					return m_PressedInUpdate;
				}
				set
				{
					m_PressedInUpdate = value;
				}
			}

			public uint releasedInUpdate
			{
				get
				{
					return m_ReleasedInUpdate;
				}
				set
				{
					m_ReleasedInUpdate = value;
				}
			}

			public bool isPassThrough
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.PassThrough) != 0;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.PassThrough;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.PassThrough;
					}
				}
			}

			public bool isButton
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.Button) != 0;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.Button;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.Button;
					}
				}
			}

			public bool isPressed
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.Pressed) != 0;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.Pressed;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.Pressed;
					}
				}
			}

			public bool mayNeedConflictResolution
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.MayNeedConflictResolution) != 0;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.MayNeedConflictResolution;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.MayNeedConflictResolution;
					}
				}
			}

			public bool hasMultipleConcurrentActuations
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.HasMultipleConcurrentActuations) != 0;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.HasMultipleConcurrentActuations;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.HasMultipleConcurrentActuations;
					}
				}
			}

			public bool inProcessing
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.InProcessing) != 0;
				}
				set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.InProcessing;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.InProcessing;
					}
				}
			}

			public global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags flags
			{
				get
				{
					return (global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags)m_Flags;
				}
				set
				{
					m_Flags = (byte)value;
				}
			}
		}

		public struct ActionMapIndices
		{
			public int actionStartIndex;

			public int actionCount;

			public int controlStartIndex;

			public int controlCount;

			public int bindingStartIndex;

			public int bindingCount;

			public int interactionStartIndex;

			public int interactionCount;

			public int processorStartIndex;

			public int processorCount;

			public int compositeStartIndex;

			public int compositeCount;
		}

		public struct UnmanagedMemory : global::System.IDisposable
		{
			public unsafe void* basePtr;

			public int mapCount;

			public int actionCount;

			public int interactionCount;

			public int bindingCount;

			public int controlCount;

			public int compositeCount;

			public unsafe global::UnityEngine.InputSystem.InputActionState.TriggerState* actionStates;

			public unsafe global::UnityEngine.InputSystem.InputActionState.BindingState* bindingStates;

			public unsafe global::UnityEngine.InputSystem.InputActionState.InteractionState* interactionStates;

			public unsafe float* controlMagnitudes;

			public unsafe float* compositeMagnitudes;

			public unsafe int* enabledControls;

			public unsafe ushort* actionBindingIndicesAndCounts;

			public unsafe ushort* actionBindingIndices;

			public unsafe int* controlIndexToBindingIndex;

			public unsafe ushort* controlGroupingAndComplexity;

			public bool controlGroupingInitialized;

			public unsafe global::UnityEngine.InputSystem.InputActionState.ActionMapIndices* mapIndices;

			public unsafe bool isAllocated => basePtr != null;

			public unsafe int sizeInBytes => mapCount * sizeof(global::UnityEngine.InputSystem.InputActionState.ActionMapIndices) + actionCount * sizeof(global::UnityEngine.InputSystem.InputActionState.TriggerState) + bindingCount * sizeof(global::UnityEngine.InputSystem.InputActionState.BindingState) + interactionCount * sizeof(global::UnityEngine.InputSystem.InputActionState.InteractionState) + controlCount * 4 + compositeCount * 4 + controlCount * 4 + controlCount * 2 * 2 + actionCount * 2 * 2 + bindingCount * 2 + (controlCount + 31) / 32 * 4;

			private unsafe static byte* AllocFromBlob(ref byte* top, int size)
			{
				if (size == 0)
				{
					return null;
				}
				byte* result = top;
				top += size;
				return result;
			}

			public unsafe void Allocate(int mapCount, int actionCount, int bindingCount, int controlCount, int interactionCount, int compositeCount)
			{
				this.mapCount = mapCount;
				this.actionCount = actionCount;
				this.interactionCount = interactionCount;
				this.bindingCount = bindingCount;
				this.controlCount = controlCount;
				this.compositeCount = compositeCount;
				int num = sizeInBytes;
				byte* top = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(num, 8, global::Unity.Collections.Allocator.Persistent);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(top, num);
				basePtr = top;
				actionStates = (global::UnityEngine.InputSystem.InputActionState.TriggerState*)AllocFromBlob(ref top, actionCount * sizeof(global::UnityEngine.InputSystem.InputActionState.TriggerState));
				interactionStates = (global::UnityEngine.InputSystem.InputActionState.InteractionState*)AllocFromBlob(ref top, interactionCount * sizeof(global::UnityEngine.InputSystem.InputActionState.InteractionState));
				bindingStates = (global::UnityEngine.InputSystem.InputActionState.BindingState*)AllocFromBlob(ref top, bindingCount * sizeof(global::UnityEngine.InputSystem.InputActionState.BindingState));
				mapIndices = (global::UnityEngine.InputSystem.InputActionState.ActionMapIndices*)AllocFromBlob(ref top, mapCount * sizeof(global::UnityEngine.InputSystem.InputActionState.ActionMapIndices));
				controlMagnitudes = (float*)AllocFromBlob(ref top, controlCount * 4);
				compositeMagnitudes = (float*)AllocFromBlob(ref top, compositeCount * 4);
				controlIndexToBindingIndex = (int*)AllocFromBlob(ref top, controlCount * 4);
				controlGroupingAndComplexity = (ushort*)AllocFromBlob(ref top, controlCount * 2 * 2);
				actionBindingIndicesAndCounts = (ushort*)AllocFromBlob(ref top, actionCount * 2 * 2);
				actionBindingIndices = (ushort*)AllocFromBlob(ref top, bindingCount * 2);
				enabledControls = (int*)AllocFromBlob(ref top, (controlCount + 31) / 32 * 4);
			}

			public unsafe void Dispose()
			{
				if (basePtr != null)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(basePtr, global::Unity.Collections.Allocator.Persistent);
					basePtr = null;
					actionStates = null;
					interactionStates = null;
					bindingStates = null;
					mapIndices = null;
					controlMagnitudes = null;
					compositeMagnitudes = null;
					controlIndexToBindingIndex = null;
					controlGroupingAndComplexity = null;
					actionBindingIndices = null;
					actionBindingIndicesAndCounts = null;
					mapCount = 0;
					actionCount = 0;
					bindingCount = 0;
					controlCount = 0;
					interactionCount = 0;
					compositeCount = 0;
				}
			}

			public unsafe void CopyDataFrom(global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory memory)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(mapIndices, memory.mapIndices, memory.mapCount * sizeof(global::UnityEngine.InputSystem.InputActionState.ActionMapIndices));
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(actionStates, memory.actionStates, memory.actionCount * sizeof(global::UnityEngine.InputSystem.InputActionState.TriggerState));
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(bindingStates, memory.bindingStates, memory.bindingCount * sizeof(global::UnityEngine.InputSystem.InputActionState.BindingState));
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(interactionStates, memory.interactionStates, memory.interactionCount * sizeof(global::UnityEngine.InputSystem.InputActionState.InteractionState));
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(controlMagnitudes, memory.controlMagnitudes, memory.controlCount * 4);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(compositeMagnitudes, memory.compositeMagnitudes, memory.compositeCount * 4);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(controlIndexToBindingIndex, memory.controlIndexToBindingIndex, memory.controlCount * 4);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(controlGroupingAndComplexity, memory.controlGroupingAndComplexity, memory.controlCount * 2 * 2);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(actionBindingIndicesAndCounts, memory.actionBindingIndicesAndCounts, memory.actionCount * 2 * 2);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(actionBindingIndices, memory.actionBindingIndices, memory.bindingCount * 2);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(enabledControls, memory.enabledControls, (memory.controlCount + 31) / 32 * 4);
			}

			public global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory Clone()
			{
				if (!isAllocated)
				{
					return default(global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory);
				}
				global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory result = default(global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory);
				result.Allocate(mapCount, actionCount, controlCount: controlCount, bindingCount: bindingCount, interactionCount: interactionCount, compositeCount: compositeCount);
				result.CopyDataFrom(this);
				return result;
			}
		}

		internal struct GlobalState
		{
			internal global::UnityEngine.InputSystem.Utilities.InlinedArray<global::System.Runtime.InteropServices.GCHandle> globalList;

			internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<object, global::UnityEngine.InputSystem.InputActionChange>> onActionChange;

			internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<object>> onActionControlsChanged;
		}

		public const int kInvalidIndex = -1;

		public global::UnityEngine.InputSystem.InputActionMap[] maps;

		public global::UnityEngine.InputSystem.InputControl[] controls;

		public global::UnityEngine.InputSystem.IInputInteraction[] interactions;

		public global::UnityEngine.InputSystem.InputProcessor[] processors;

		public global::UnityEngine.InputSystem.InputBindingComposite[] composites;

		public int totalProcessorCount;

		public global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory memory;

		private bool m_OnBeforeUpdateHooked;

		private bool m_OnAfterUpdateHooked;

		private bool m_InProcessControlStateChange;

		private bool m_Suppressed;

		private global::UnityEngine.InputSystem.LowLevel.InputEventPtr m_CurrentlyProcessingThisEvent;

		private global::System.Action m_OnBeforeUpdateDelegate;

		private global::System.Action m_OnAfterUpdateDelegate;

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputInitialActionStateCheckMarker = new global::Unity.Profiling.ProfilerMarker("InitialActionStateCheck");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputActionResolveConflictMarker = new global::Unity.Profiling.ProfilerMarker("InputActionResolveConflict");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputActionCallbackMarker = new global::Unity.Profiling.ProfilerMarker("InputActionCallback");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnActionChangeMarker = new global::Unity.Profiling.ProfilerMarker("InpustSystem.onActionChange");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputOnDeviceChangeMarker = new global::Unity.Profiling.ProfilerMarker("InpustSystem.onDeviceChange");

		internal static global::UnityEngine.InputSystem.InputActionState.GlobalState s_GlobalState;

		public int totalCompositeCount => memory.compositeCount;

		public int totalMapCount => memory.mapCount;

		public int totalActionCount => memory.actionCount;

		public int totalBindingCount => memory.bindingCount;

		public int totalInteractionCount => memory.interactionCount;

		public int totalControlCount => memory.controlCount;

		public unsafe global::UnityEngine.InputSystem.InputActionState.ActionMapIndices* mapIndices => memory.mapIndices;

		public unsafe global::UnityEngine.InputSystem.InputActionState.TriggerState* actionStates => memory.actionStates;

		public unsafe global::UnityEngine.InputSystem.InputActionState.BindingState* bindingStates => memory.bindingStates;

		public unsafe global::UnityEngine.InputSystem.InputActionState.InteractionState* interactionStates => memory.interactionStates;

		public unsafe int* controlIndexToBindingIndex => memory.controlIndexToBindingIndex;

		public unsafe ushort* controlGroupingAndComplexity => memory.controlGroupingAndComplexity;

		public unsafe float* controlMagnitudes => memory.controlMagnitudes;

		public unsafe uint* enabledControls => (uint*)memory.enabledControls;

		public bool isProcessingControlStateChange => m_InProcessControlStateChange;

		public bool IsSuppressed => m_Suppressed;

		public void Initialize(global::UnityEngine.InputSystem.InputBindingResolver resolver)
		{
			ClaimDataFrom(resolver);
			AddToGlobalList();
		}

		private unsafe void ComputeControlGroupingIfNecessary()
		{
			if (memory.controlGroupingInitialized)
			{
				return;
			}
			bool flag = !global::UnityEngine.InputSystem.InputSystem.settings.shortcutKeysConsumeInput;
			uint num = 1u;
			for (int i = 0; i < totalControlCount; i++)
			{
				global::UnityEngine.InputSystem.InputControl inputControl = controls[i];
				int num2 = controlIndexToBindingIndex[i];
				ref global::UnityEngine.InputSystem.InputActionState.BindingState reference = ref bindingStates[num2];
				int num3 = 1;
				if (reference.isPartOfComposite && !flag)
				{
					int compositeOrCompositeBindingIndex = reference.compositeOrCompositeBindingIndex;
					for (int j = compositeOrCompositeBindingIndex + 1; j < totalBindingCount; j++)
					{
						ref global::UnityEngine.InputSystem.InputActionState.BindingState reference2 = ref bindingStates[j];
						if (!reference2.isPartOfComposite || reference2.compositeOrCompositeBindingIndex != compositeOrCompositeBindingIndex)
						{
							break;
						}
						num3++;
					}
				}
				controlGroupingAndComplexity[i * 2 + 1] = (ushort)num3;
				if (controlGroupingAndComplexity[i * 2] != 0)
				{
					continue;
				}
				if (!flag)
				{
					for (int k = 0; k < totalControlCount; k++)
					{
						global::UnityEngine.InputSystem.InputControl inputControl2 = controls[k];
						if (inputControl == inputControl2)
						{
							controlGroupingAndComplexity[k * 2] = (ushort)num;
						}
					}
				}
				controlGroupingAndComplexity[i * 2] = (ushort)num;
				num++;
			}
			memory.controlGroupingInitialized = true;
		}

		public void ClaimDataFrom(global::UnityEngine.InputSystem.InputBindingResolver resolver)
		{
			totalProcessorCount = resolver.totalProcessorCount;
			maps = resolver.maps;
			interactions = resolver.interactions;
			processors = resolver.processors;
			composites = resolver.composites;
			controls = resolver.controls;
			memory = resolver.memory;
			resolver.memory = default(global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory);
			ComputeControlGroupingIfNecessary();
		}

		~InputActionState()
		{
			Destroy(isFinalizing: true);
		}

		public void Dispose()
		{
			Destroy();
		}

		private unsafe void Destroy(bool isFinalizing = false)
		{
			if (!isFinalizing)
			{
				for (int i = 0; i < totalMapCount; i++)
				{
					global::UnityEngine.InputSystem.InputActionMap inputActionMap = maps[i];
					if (inputActionMap.enabled)
					{
						DisableControls(i, mapIndices[i].controlStartIndex, mapIndices[i].controlCount);
					}
					if (inputActionMap.m_Asset != null)
					{
						inputActionMap.m_Asset.m_SharedStateForAllMaps = null;
					}
					inputActionMap.m_State = null;
					inputActionMap.m_MapIndexInState = -1;
					inputActionMap.m_EnabledActionsCount = 0;
					global::UnityEngine.InputSystem.InputAction[] actions = inputActionMap.m_Actions;
					if (actions != null)
					{
						for (int j = 0; j < actions.Length; j++)
						{
							actions[j].m_ActionIndexInState = -1;
						}
					}
				}
				RemoveMapFromGlobalList();
			}
			memory.Dispose();
		}

		public global::UnityEngine.InputSystem.InputActionState Clone()
		{
			return new global::UnityEngine.InputSystem.InputActionState
			{
				maps = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Copy(maps),
				controls = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Copy(controls),
				interactions = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Copy(interactions),
				processors = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Copy(processors),
				composites = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Copy(composites),
				totalProcessorCount = totalProcessorCount,
				memory = memory.Clone()
			};
		}

		object global::System.ICloneable.Clone()
		{
			return Clone();
		}

		private bool IsUsingDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			bool flag = false;
			for (int i = 0; i < totalMapCount; i++)
			{
				global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? devices = maps[i].devices;
				if (!devices.HasValue)
				{
					flag = true;
				}
				else if (global::System.Linq.Enumerable.Contains(devices.Value, device))
				{
					return true;
				}
			}
			if (!flag)
			{
				return false;
			}
			for (int j = 0; j < totalControlCount; j++)
			{
				if (controls[j].device == device)
				{
					return true;
				}
			}
			return false;
		}

		private bool CanUseDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			bool flag = false;
			for (int i = 0; i < totalMapCount; i++)
			{
				global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? devices = maps[i].devices;
				if (!devices.HasValue)
				{
					flag = true;
				}
				else if (global::System.Linq.Enumerable.Contains(devices.Value, device))
				{
					return true;
				}
			}
			if (!flag)
			{
				return false;
			}
			for (int j = 0; j < totalMapCount; j++)
			{
				global::UnityEngine.InputSystem.InputBinding[] bindings = maps[j].m_Bindings;
				if (bindings == null)
				{
					continue;
				}
				int num = bindings.Length;
				for (int k = 0; k < num; k++)
				{
					if (global::UnityEngine.InputSystem.InputControlPath.TryFindControl(device, bindings[k].effectivePath) != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool HasEnabledActions()
		{
			for (int i = 0; i < totalMapCount; i++)
			{
				if (maps[i].enabled)
				{
					return true;
				}
			}
			return false;
		}

		private unsafe void FinishBindingCompositeSetups()
		{
			for (int i = 0; i < totalBindingCount; i++)
			{
				ref global::UnityEngine.InputSystem.InputActionState.BindingState reference = ref bindingStates[i];
				if (reference.isComposite && reference.compositeOrCompositeBindingIndex != -1)
				{
					global::UnityEngine.InputSystem.InputBindingComposite obj = composites[reference.compositeOrCompositeBindingIndex];
					global::UnityEngine.InputSystem.InputBindingCompositeContext context = new global::UnityEngine.InputSystem.InputBindingCompositeContext
					{
						m_State = this,
						m_BindingIndex = i
					};
					obj.CallFinishSetup(ref context);
				}
			}
		}

		internal unsafe void PrepareForBindingReResolution(bool needFullResolve, ref global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> activeControls, ref bool hasEnabledActions)
		{
			bool flag = false;
			for (int i = 0; i < totalMapCount; i++)
			{
				global::UnityEngine.InputSystem.InputActionMap inputActionMap = maps[i];
				if (inputActionMap.enabled)
				{
					hasEnabledActions = true;
					if (needFullResolve)
					{
						DisableAllActions(inputActionMap);
					}
					else
					{
						foreach (global::UnityEngine.InputSystem.InputAction action in inputActionMap.actions)
						{
							if (!action.phase.IsInProgress())
							{
								continue;
							}
							if (action.ActiveControlIsValid(action.activeControl))
							{
								if (!flag)
								{
									activeControls = new global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl>(global::Unity.Collections.Allocator.Temp);
									activeControls.Resize(totalControlCount);
									flag = true;
								}
								ref global::UnityEngine.InputSystem.InputActionState.TriggerState reference = ref actionStates[action.m_ActionIndexInState];
								int controlIndex = reference.controlIndex;
								activeControls[controlIndex] = controls[controlIndex];
								global::UnityEngine.InputSystem.InputActionState.BindingState bindingState = bindingStates[reference.bindingIndex];
								for (int j = 0; j < bindingState.interactionCount; j++)
								{
									int num = bindingState.interactionStartIndex + j;
									if (interactionStates[num].phase.IsInProgress())
									{
										controlIndex = interactionStates[num].triggerControlIndex;
										if (action.ActiveControlIsValid(controls[controlIndex]))
										{
											activeControls[controlIndex] = controls[controlIndex];
										}
										else
										{
											ResetInteractionState(num);
										}
									}
								}
							}
							else
							{
								ResetActionState(action.m_ActionIndexInState);
							}
						}
						DisableControls(inputActionMap);
					}
				}
				inputActionMap.ClearCachedActionData(!needFullResolve);
			}
			NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.BoundControlsAboutToChange);
		}

		public void FinishBindingResolution(bool hasEnabledActions, global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory oldMemory, global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> activeControls, bool isFullResolve)
		{
			FinishBindingCompositeSetups();
			if (hasEnabledActions)
			{
				RestoreActionStatesAfterReResolvingBindings(oldMemory, activeControls, isFullResolve);
			}
			else
			{
				NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.BoundControlsChanged);
			}
		}

		private unsafe void RestoreActionStatesAfterReResolvingBindings(global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory oldState, global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> activeControls, bool isFullResolve)
		{
			for (int i = 0; i < totalActionCount; i++)
			{
				ref global::UnityEngine.InputSystem.InputActionState.TriggerState reference = ref oldState.actionStates[i];
				ref global::UnityEngine.InputSystem.InputActionState.TriggerState reference2 = ref actionStates[i];
				reference2.lastCanceledInUpdate = reference.lastCanceledInUpdate;
				reference2.lastPerformedInUpdate = reference.lastPerformedInUpdate;
				reference2.lastCompletedInUpdate = reference.lastCompletedInUpdate;
				reference2.pressedInUpdate = reference.pressedInUpdate;
				reference2.releasedInUpdate = reference.releasedInUpdate;
				reference2.startTime = reference.startTime;
				reference2.framePerformed = reference.framePerformed;
				reference2.frameCompleted = reference.frameCompleted;
				reference2.framePressed = reference.framePressed;
				reference2.frameReleased = reference.frameReleased;
				reference2.bindingIndex = reference.bindingIndex;
				if (reference.phase != global::UnityEngine.InputSystem.InputActionPhase.Disabled)
				{
					reference2.phase = global::UnityEngine.InputSystem.InputActionPhase.Waiting;
					if (isFullResolve)
					{
						maps[reference2.mapIndex].m_EnabledActionsCount++;
					}
				}
			}
			for (int j = 0; j < totalBindingCount; j++)
			{
				ref global::UnityEngine.InputSystem.InputActionState.BindingState reference3 = ref memory.bindingStates[j];
				if (reference3.isPartOfComposite)
				{
					continue;
				}
				if (reference3.isComposite)
				{
					int compositeOrCompositeBindingIndex = reference3.compositeOrCompositeBindingIndex;
					if (oldState.compositeMagnitudes != null)
					{
						memory.compositeMagnitudes[compositeOrCompositeBindingIndex] = oldState.compositeMagnitudes[compositeOrCompositeBindingIndex];
					}
				}
				int actionIndex = reference3.actionIndex;
				if (actionIndex == -1)
				{
					continue;
				}
				ref global::UnityEngine.InputSystem.InputActionState.TriggerState reference4 = ref actionStates[actionIndex];
				if (reference4.isDisabled)
				{
					continue;
				}
				reference3.initialStateCheckPending = reference3.wantsInitialStateCheck;
				EnableControls(reference3.mapIndex, reference3.controlStartIndex, reference3.controlCount);
				if (isFullResolve)
				{
					continue;
				}
				ref global::UnityEngine.InputSystem.InputActionState.BindingState reference5 = ref memory.bindingStates[j];
				reference3.triggerEventIdForComposite = reference5.triggerEventIdForComposite;
				ref global::UnityEngine.InputSystem.InputActionState.TriggerState reference6 = ref oldState.actionStates[actionIndex];
				if (j != reference6.bindingIndex || !reference6.phase.IsInProgress() || activeControls.Count <= 0 || activeControls[reference6.controlIndex] == null)
				{
					continue;
				}
				global::UnityEngine.InputSystem.InputControl control = activeControls[reference6.controlIndex];
				int num = FindControlIndexOnBinding(j, control);
				if (num != -1)
				{
					reference4.phase = reference6.phase;
					reference4.controlIndex = num;
					reference4.magnitude = reference6.magnitude;
					reference4.interactionIndex = reference6.interactionIndex;
					memory.controlMagnitudes[num] = reference6.magnitude;
				}
				for (int k = 0; k < reference3.interactionCount; k++)
				{
					ref global::UnityEngine.InputSystem.InputActionState.InteractionState reference7 = ref oldState.interactionStates[reference5.interactionStartIndex + k];
					if (!reference7.phase.IsInProgress())
					{
						continue;
					}
					control = activeControls[reference7.triggerControlIndex];
					if (control != null)
					{
						num = FindControlIndexOnBinding(j, control);
						ref global::UnityEngine.InputSystem.InputActionState.InteractionState reference8 = ref interactionStates[reference3.interactionStartIndex + k];
						reference8.phase = reference7.phase;
						reference8.performedTime = reference7.performedTime;
						reference8.startTime = reference7.startTime;
						reference8.triggerControlIndex = num;
						if (reference7.isTimerRunning)
						{
							global::UnityEngine.InputSystem.InputActionState.TriggerState trigger = new global::UnityEngine.InputSystem.InputActionState.TriggerState
							{
								mapIndex = reference3.mapIndex,
								controlIndex = num,
								bindingIndex = j,
								time = reference7.timerStartTime,
								interactionIndex = reference3.interactionStartIndex + k
							};
							StartTimeout(reference7.timerDuration, ref trigger);
							reference8.totalTimeoutCompletionDone = reference7.totalTimeoutCompletionDone;
							reference8.totalTimeoutCompletionTimeRemaining = reference7.totalTimeoutCompletionTimeRemaining;
						}
					}
				}
			}
			HookOnBeforeUpdate();
			NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.BoundControlsChanged);
			if (!isFullResolve || s_GlobalState.onActionChange.length <= 0)
			{
				return;
			}
			for (int l = 0; l < totalMapCount; l++)
			{
				global::UnityEngine.InputSystem.InputActionMap inputActionMap = maps[l];
				if (inputActionMap.m_SingletonAction == null && inputActionMap.m_EnabledActionsCount == global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(inputActionMap.m_Actions))
				{
					NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.ActionMapEnabled, inputActionMap);
					continue;
				}
				foreach (global::UnityEngine.InputSystem.InputAction action in inputActionMap.actions)
				{
					if (action.enabled)
					{
						NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.ActionEnabled, action);
					}
				}
			}
		}

		private unsafe bool IsActiveControl(int bindingIndex, int controlIndex)
		{
			ref global::UnityEngine.InputSystem.InputActionState.BindingState reference = ref bindingStates[bindingIndex];
			int actionIndex = reference.actionIndex;
			if (actionIndex == -1)
			{
				return false;
			}
			if (actionStates[actionIndex].controlIndex == controlIndex)
			{
				return true;
			}
			for (int i = 0; i < reference.interactionCount; i++)
			{
				if (interactionStates[bindingStates->interactionStartIndex + i].triggerControlIndex == controlIndex)
				{
					return true;
				}
			}
			return false;
		}

		private unsafe int FindControlIndexOnBinding(int bindingIndex, global::UnityEngine.InputSystem.InputControl control)
		{
			int controlStartIndex = bindingStates[bindingIndex].controlStartIndex;
			int controlCount = bindingStates[bindingIndex].controlCount;
			for (int i = 0; i < controlCount; i++)
			{
				if (control == controls[controlStartIndex + i])
				{
					return controlStartIndex + i;
				}
			}
			return -1;
		}

		private unsafe void ResetActionStatesDrivenBy(global::UnityEngine.InputSystem.InputDevice device)
		{
			using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolution())
			{
				for (int i = 0; i < totalActionCount; i++)
				{
					global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = actionStates + i;
					if (ptr->phase == global::UnityEngine.InputSystem.InputActionPhase.Waiting || ptr->phase == global::UnityEngine.InputSystem.InputActionPhase.Disabled)
					{
						continue;
					}
					if (ptr->isPassThrough)
					{
						if (!IsActionBoundToControlFromDevice(device, i))
						{
							continue;
						}
					}
					else
					{
						int controlIndex = ptr->controlIndex;
						if (controlIndex == -1 || controls[controlIndex].device != device)
						{
							continue;
						}
					}
					ResetActionState(i);
				}
			}
		}

		private unsafe bool IsActionBoundToControlFromDevice(global::UnityEngine.InputSystem.InputDevice device, int actionIndex)
		{
			bool result = false;
			ushort bindingCount;
			ushort actionBindingStartIndexAndCount = GetActionBindingStartIndexAndCount(actionIndex, out bindingCount);
			for (int i = 0; i < bindingCount; i++)
			{
				ushort num = memory.actionBindingIndices[actionBindingStartIndexAndCount + i];
				int controlCount = bindingStates[(int)num].controlCount;
				int controlStartIndex = bindingStates[(int)num].controlStartIndex;
				for (int j = 0; j < controlCount; j++)
				{
					if (controls[controlStartIndex + j].device == device)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public unsafe void ResetActionState(int actionIndex, global::UnityEngine.InputSystem.InputActionPhase toPhase = global::UnityEngine.InputSystem.InputActionPhase.Waiting, bool hardReset = false)
		{
			global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = actionStates + actionIndex;
			if (ptr->phase != global::UnityEngine.InputSystem.InputActionPhase.Waiting && ptr->phase != global::UnityEngine.InputSystem.InputActionPhase.Disabled)
			{
				ptr->time = global::UnityEngine.InputSystem.LowLevel.InputState.currentTime;
				if (ptr->interactionIndex != -1)
				{
					int bindingIndex = ptr->bindingIndex;
					if (bindingIndex != -1)
					{
						int mapIndex = ptr->mapIndex;
						int interactionCount = bindingStates[bindingIndex].interactionCount;
						int interactionStartIndex = bindingStates[bindingIndex].interactionStartIndex;
						for (int i = 0; i < interactionCount; i++)
						{
							int interactionIndex = interactionStartIndex + i;
							ResetInteractionStateAndCancelIfNecessary(mapIndex, bindingIndex, interactionIndex, toPhase);
						}
					}
				}
				else if (ptr->phase != global::UnityEngine.InputSystem.InputActionPhase.Canceled)
				{
					ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Canceled, ref actionStates[actionIndex], toPhase);
				}
			}
			ptr->phase = toPhase;
			ptr->controlIndex = -1;
			ushort num = memory.actionBindingIndicesAndCounts[actionIndex];
			ptr->bindingIndex = ((memory.actionBindingIndices != null) ? memory.actionBindingIndices[(int)num] : 0);
			ptr->interactionIndex = -1;
			ptr->startTime = 0.0;
			ptr->time = 0.0;
			ptr->hasMultipleConcurrentActuations = false;
			ptr->inProcessing = false;
			ptr->isPressed = false;
			if (hardReset)
			{
				ptr->lastCanceledInUpdate = 0u;
				ptr->lastPerformedInUpdate = 0u;
				ptr->lastCompletedInUpdate = 0u;
				ptr->pressedInUpdate = 0u;
				ptr->releasedInUpdate = 0u;
				ptr->framePerformed = 0;
				ptr->frameCompleted = 0;
				ptr->framePressed = 0;
				ptr->frameReleased = 0;
			}
		}

		public unsafe ref global::UnityEngine.InputSystem.InputActionState.TriggerState FetchActionState(global::UnityEngine.InputSystem.InputAction action)
		{
			return ref actionStates[action.m_ActionIndexInState];
		}

		public unsafe global::UnityEngine.InputSystem.InputActionState.ActionMapIndices FetchMapIndices(global::UnityEngine.InputSystem.InputActionMap map)
		{
			return mapIndices[map.m_MapIndexInState];
		}

		public unsafe void EnableAllActions(global::UnityEngine.InputSystem.InputActionMap map)
		{
			EnableControls(map);
			int mapIndexInState = map.m_MapIndexInState;
			int actionCount = mapIndices[mapIndexInState].actionCount;
			int actionStartIndex = mapIndices[mapIndexInState].actionStartIndex;
			for (int i = 0; i < actionCount; i++)
			{
				int num = actionStartIndex + i;
				global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = actionStates + num;
				if (ptr->isDisabled)
				{
					ptr->phase = global::UnityEngine.InputSystem.InputActionPhase.Waiting;
				}
				ptr->inProcessing = false;
			}
			map.m_EnabledActionsCount = actionCount;
			HookOnBeforeUpdate();
			if (map.m_SingletonAction != null)
			{
				NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.ActionEnabled, map.m_SingletonAction);
			}
			else
			{
				NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.ActionMapEnabled, map);
			}
		}

		private unsafe void EnableControls(global::UnityEngine.InputSystem.InputActionMap map)
		{
			int mapIndexInState = map.m_MapIndexInState;
			int controlCount = mapIndices[mapIndexInState].controlCount;
			int controlStartIndex = mapIndices[mapIndexInState].controlStartIndex;
			if (controlCount > 0)
			{
				EnableControls(mapIndexInState, controlStartIndex, controlCount);
			}
		}

		public unsafe void EnableSingleAction(global::UnityEngine.InputSystem.InputAction action)
		{
			EnableControls(action);
			int actionIndexInState = action.m_ActionIndexInState;
			actionStates[actionIndexInState].phase = global::UnityEngine.InputSystem.InputActionPhase.Waiting;
			action.m_ActionMap.m_EnabledActionsCount++;
			HookOnBeforeUpdate();
			NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.ActionEnabled, action);
		}

		private unsafe void EnableControls(global::UnityEngine.InputSystem.InputAction action)
		{
			int actionIndexInState = action.m_ActionIndexInState;
			int mapIndexInState = action.m_ActionMap.m_MapIndexInState;
			int bindingStartIndex = mapIndices[mapIndexInState].bindingStartIndex;
			int bindingCount = mapIndices[mapIndexInState].bindingCount;
			global::UnityEngine.InputSystem.InputActionState.BindingState* ptr = memory.bindingStates;
			for (int i = 0; i < bindingCount; i++)
			{
				int num = bindingStartIndex + i;
				global::UnityEngine.InputSystem.InputActionState.BindingState* ptr2 = ptr + num;
				if (ptr2->actionIndex == actionIndexInState && !ptr2->isPartOfComposite)
				{
					int controlCount = ptr2->controlCount;
					if (controlCount != 0)
					{
						EnableControls(mapIndexInState, ptr2->controlStartIndex, controlCount);
					}
				}
			}
		}

		public unsafe void DisableAllActions(global::UnityEngine.InputSystem.InputActionMap map)
		{
			DisableControls(map);
			int mapIndexInState = map.m_MapIndexInState;
			int actionStartIndex = mapIndices[mapIndexInState].actionStartIndex;
			int actionCount = mapIndices[mapIndexInState].actionCount;
			bool flag = map.m_EnabledActionsCount == actionCount;
			for (int i = 0; i < actionCount; i++)
			{
				int num = actionStartIndex + i;
				if (actionStates[num].phase != global::UnityEngine.InputSystem.InputActionPhase.Disabled)
				{
					ResetActionState(num, global::UnityEngine.InputSystem.InputActionPhase.Disabled);
					if (!flag)
					{
						NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.ActionDisabled, map.m_Actions[i]);
					}
				}
			}
			map.m_EnabledActionsCount = 0;
			if (map.m_SingletonAction != null)
			{
				NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.ActionDisabled, map.m_SingletonAction);
			}
			else if (flag)
			{
				NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.ActionMapDisabled, map);
			}
		}

		public unsafe void DisableControls(global::UnityEngine.InputSystem.InputActionMap map)
		{
			int mapIndexInState = map.m_MapIndexInState;
			int controlCount = mapIndices[mapIndexInState].controlCount;
			int controlStartIndex = mapIndices[mapIndexInState].controlStartIndex;
			if (controlCount > 0)
			{
				DisableControls(mapIndexInState, controlStartIndex, controlCount);
			}
		}

		public void DisableSingleAction(global::UnityEngine.InputSystem.InputAction action)
		{
			DisableControls(action);
			ResetActionState(action.m_ActionIndexInState, global::UnityEngine.InputSystem.InputActionPhase.Disabled);
			action.m_ActionMap.m_EnabledActionsCount--;
			NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange.ActionDisabled, action);
		}

		private unsafe void DisableControls(global::UnityEngine.InputSystem.InputAction action)
		{
			int actionIndexInState = action.m_ActionIndexInState;
			int mapIndexInState = action.m_ActionMap.m_MapIndexInState;
			int bindingStartIndex = mapIndices[mapIndexInState].bindingStartIndex;
			int bindingCount = mapIndices[mapIndexInState].bindingCount;
			global::UnityEngine.InputSystem.InputActionState.BindingState* ptr = memory.bindingStates;
			for (int i = 0; i < bindingCount; i++)
			{
				int num = bindingStartIndex + i;
				global::UnityEngine.InputSystem.InputActionState.BindingState* ptr2 = ptr + num;
				if (ptr2->actionIndex == actionIndexInState && !ptr2->isPartOfComposite)
				{
					int controlCount = ptr2->controlCount;
					if (controlCount != 0)
					{
						DisableControls(mapIndexInState, ptr2->controlStartIndex, controlCount);
					}
				}
			}
		}

		private unsafe void EnableControls(int mapIndex, int controlStartIndex, int numControls)
		{
			global::UnityEngine.InputSystem.InputManager s_Manager = global::UnityEngine.InputSystem.InputSystem.s_Manager;
			for (int i = 0; i < numControls; i++)
			{
				int num = controlStartIndex + i;
				if (!IsControlEnabled(num))
				{
					int num2 = controlIndexToBindingIndex[num];
					long monitorIndex = ToCombinedMapAndControlAndBindingIndex(mapIndex, num, num2);
					global::UnityEngine.InputSystem.InputActionState.BindingState* ptr = bindingStates + num2;
					if (ptr->wantsInitialStateCheck)
					{
						SetInitialStateCheckPending(ptr, value: true);
					}
					s_Manager.AddStateChangeMonitor(controls[num], this, monitorIndex, controlGroupingAndComplexity[num * 2]);
					SetControlEnabled(num, state: true);
				}
			}
		}

		private unsafe void DisableControls(int mapIndex, int controlStartIndex, int numControls)
		{
			global::UnityEngine.InputSystem.InputManager s_Manager = global::UnityEngine.InputSystem.InputSystem.s_Manager;
			for (int i = 0; i < numControls; i++)
			{
				int num = controlStartIndex + i;
				if (IsControlEnabled(num))
				{
					int num2 = controlIndexToBindingIndex[num];
					long monitorIndex = ToCombinedMapAndControlAndBindingIndex(mapIndex, num, num2);
					global::UnityEngine.InputSystem.InputActionState.BindingState* ptr = bindingStates + num2;
					if (ptr->wantsInitialStateCheck)
					{
						SetInitialStateCheckPending(ptr, value: false);
					}
					s_Manager.RemoveStateChangeMonitor(controls[num], this, monitorIndex);
					ptr->pressTime = 0.0;
					SetControlEnabled(num, state: false);
				}
			}
		}

		public unsafe void SetInitialStateCheckPending(int actionIndex, bool value = true)
		{
			int mapIndex = actionStates[actionIndex].mapIndex;
			int bindingStartIndex = mapIndices[mapIndex].bindingStartIndex;
			int bindingCount = mapIndices[mapIndex].bindingCount;
			for (int i = 0; i < bindingCount; i++)
			{
				ref global::UnityEngine.InputSystem.InputActionState.BindingState reference = ref bindingStates[bindingStartIndex + i];
				if (reference.actionIndex == actionIndex && !reference.isPartOfComposite)
				{
					reference.initialStateCheckPending = value;
				}
			}
		}

		private unsafe void SetInitialStateCheckPending(global::UnityEngine.InputSystem.InputActionState.BindingState* bindingStatePtr, bool value)
		{
			if (bindingStatePtr->isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = bindingStatePtr->compositeOrCompositeBindingIndex;
				bindingStates[compositeOrCompositeBindingIndex].initialStateCheckPending = value;
			}
			else
			{
				bindingStatePtr->initialStateCheckPending = value;
			}
		}

		private unsafe bool IsControlEnabled(int controlIndex)
		{
			int num = controlIndex / 32;
			uint num2 = (uint)(1 << controlIndex % 32);
			return (enabledControls[num] & num2) != 0;
		}

		private unsafe void SetControlEnabled(int controlIndex, bool state)
		{
			int num = controlIndex / 32;
			uint num2 = (uint)(1 << controlIndex % 32);
			if (state)
			{
				enabledControls[num] |= num2;
			}
			else
			{
				enabledControls[num] &= ~num2;
			}
		}

		private void HookOnBeforeUpdate()
		{
			if (!m_OnBeforeUpdateHooked)
			{
				if (m_OnBeforeUpdateDelegate == null)
				{
					m_OnBeforeUpdateDelegate = OnBeforeInitialUpdate;
				}
				global::UnityEngine.InputSystem.InputSystem.s_Manager.onBeforeUpdate += m_OnBeforeUpdateDelegate;
				m_OnBeforeUpdateHooked = true;
			}
		}

		private void UnhookOnBeforeUpdate()
		{
			if (m_OnBeforeUpdateHooked)
			{
				global::UnityEngine.InputSystem.InputSystem.s_Manager.onBeforeUpdate -= m_OnBeforeUpdateDelegate;
				m_OnBeforeUpdateHooked = false;
			}
		}

		private unsafe void OnBeforeInitialUpdate()
		{
			if (global::UnityEngine.InputSystem.LowLevel.InputState.currentUpdateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender)
			{
				return;
			}
			UnhookOnBeforeUpdate();
			double currentTime = global::UnityEngine.InputSystem.LowLevel.InputState.currentTime;
			global::UnityEngine.InputSystem.InputManager s_Manager = global::UnityEngine.InputSystem.InputSystem.s_Manager;
			for (int i = 0; i < totalBindingCount; i++)
			{
				ref global::UnityEngine.InputSystem.InputActionState.BindingState reference = ref bindingStates[i];
				if (!reference.initialStateCheckPending)
				{
					continue;
				}
				reference.initialStateCheckPending = false;
				int controlStartIndex = reference.controlStartIndex;
				int controlCount = reference.controlCount;
				bool isComposite = reference.isComposite;
				bool flag = false;
				for (int j = 0; j < controlCount; j++)
				{
					int num = controlStartIndex + j;
					global::UnityEngine.InputSystem.InputControl inputControl = controls[num];
					if (!IsActiveControl(i, num) && !inputControl.CheckStateIsAtDefault())
					{
						if (inputControl.IsValueConsideredPressed(inputControl.magnitude) && (reference.pressTime == 0.0 || reference.pressTime > currentTime))
						{
							reference.pressTime = currentTime;
						}
						if (!(isComposite && flag))
						{
							s_Manager.SignalStateChangeMonitor(inputControl, this);
							flag = true;
						}
					}
				}
			}
			s_Manager.FireStateChangeNotifications();
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyControlStateChanged(global::UnityEngine.InputSystem.InputControl control, double time, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, long mapControlAndBindingIndex)
		{
			SplitUpMapAndControlAndBindingIndex(mapControlAndBindingIndex, out var mapIndex, out var controlIndex, out var bindingIndex);
			ProcessControlStateChange(mapIndex, controlIndex, bindingIndex, time, eventPtr);
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateChangeMonitor.NotifyTimerExpired(global::UnityEngine.InputSystem.InputControl control, double time, long mapControlAndBindingIndex, int interactionIndex)
		{
			SplitUpMapAndControlAndBindingIndex(mapControlAndBindingIndex, out var mapIndex, out var controlIndex, out var bindingIndex);
			ProcessTimeout(time, mapIndex, controlIndex, bindingIndex, interactionIndex);
		}

		private unsafe long ToCombinedMapAndControlAndBindingIndex(int mapIndex, int controlIndex, int bindingIndex)
		{
			ushort num = controlGroupingAndComplexity[controlIndex * 2 + 1];
			return controlIndex | ((long)bindingIndex << 24) | ((long)mapIndex << 40) | (long)((ulong)num << 48);
		}

		private void SplitUpMapAndControlAndBindingIndex(long mapControlAndBindingIndex, out int mapIndex, out int controlIndex, out int bindingIndex)
		{
			controlIndex = (int)(mapControlAndBindingIndex & 0xFFFFFF);
			bindingIndex = (int)((mapControlAndBindingIndex >> 24) & 0xFFFF);
			mapIndex = (int)((mapControlAndBindingIndex >> 40) & 0xFF);
		}

		internal static int GetComplexityFromMonitorIndex(long mapControlAndBindingIndex)
		{
			return (int)((mapControlAndBindingIndex >> 48) & 0xFF);
		}

		private unsafe void ProcessControlStateChange(int mapIndex, int controlIndex, int bindingIndex, double time, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolution())
			{
				m_InProcessControlStateChange = true;
				m_CurrentlyProcessingThisEvent = eventPtr;
				try
				{
					global::UnityEngine.InputSystem.InputActionState.BindingState* ptr = bindingStates + bindingIndex;
					int actionIndex = ptr->actionIndex;
					global::UnityEngine.InputSystem.InputActionState.TriggerState trigger = new global::UnityEngine.InputSystem.InputActionState.TriggerState
					{
						mapIndex = mapIndex,
						controlIndex = controlIndex,
						bindingIndex = bindingIndex,
						interactionIndex = -1,
						time = time,
						startTime = time,
						isPassThrough = (actionIndex != -1 && actionStates[actionIndex].isPassThrough),
						isButton = (actionIndex != -1 && actionStates[actionIndex].isButton)
					};
					if (m_OnBeforeUpdateHooked)
					{
						ptr->initialStateCheckPending = false;
					}
					global::UnityEngine.InputSystem.InputControl inputControl = controls[controlIndex];
					trigger.magnitude = (inputControl.CheckStateIsAtDefault() ? 0f : inputControl.magnitude);
					controlMagnitudes[controlIndex] = trigger.magnitude;
					if (inputControl.IsValueConsideredPressed(trigger.magnitude) && (ptr->pressTime == 0.0 || ptr->pressTime > trigger.time))
					{
						ptr->pressTime = trigger.time;
					}
					bool flag = false;
					if (ptr->isPartOfComposite)
					{
						int compositeOrCompositeBindingIndex = ptr->compositeOrCompositeBindingIndex;
						global::UnityEngine.InputSystem.InputActionState.BindingState* ptr2 = bindingStates + compositeOrCompositeBindingIndex;
						if (ShouldIgnoreInputOnCompositeBinding(ptr2, eventPtr))
						{
							return;
						}
						int compositeOrCompositeBindingIndex2 = bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
						global::UnityEngine.InputSystem.InputBindingCompositeContext context = new global::UnityEngine.InputSystem.InputBindingCompositeContext
						{
							m_State = this,
							m_BindingIndex = compositeOrCompositeBindingIndex
						};
						trigger.magnitude = composites[compositeOrCompositeBindingIndex2].EvaluateMagnitude(ref context);
						memory.compositeMagnitudes[compositeOrCompositeBindingIndex2] = trigger.magnitude;
						int interactionCount = ptr2->interactionCount;
						if (interactionCount > 0)
						{
							flag = true;
							ProcessInteractions(ref trigger, ptr2->interactionStartIndex, interactionCount);
						}
					}
					m_Suppressed = eventPtr != null && eventPtr.handled && global::UnityEngine.InputSystem.InputSystem.s_Manager.inputEventHandledPolicy == global::UnityEngine.InputSystem.LowLevel.InputEventHandledPolicy.SuppressActionEventNotifications;
					bool flag2 = IsConflictingInput(ref trigger, actionIndex);
					ptr = bindingStates + trigger.bindingIndex;
					if (!flag2)
					{
						ProcessButtonState(ref trigger, actionIndex, ptr);
					}
					int interactionCount2 = ptr->interactionCount;
					if (interactionCount2 > 0 && !ptr->isPartOfComposite)
					{
						ProcessInteractions(ref trigger, ptr->interactionStartIndex, interactionCount2);
					}
					else if (!flag && !flag2)
					{
						ProcessDefaultInteraction(ref trigger, actionIndex);
					}
				}
				finally
				{
					m_InProcessControlStateChange = false;
					m_CurrentlyProcessingThisEvent = default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr);
				}
			}
		}

		private unsafe void ProcessButtonState(ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger, int actionIndex, global::UnityEngine.InputSystem.InputActionState.BindingState* bindingStatePtr)
		{
			global::UnityEngine.InputSystem.InputControl inputControl = controls[trigger.controlIndex];
			float num = (inputControl.isButton ? ((global::UnityEngine.InputSystem.Controls.ButtonControl)inputControl).pressPointOrDefault : global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint);
			if (controlMagnitudes[trigger.controlIndex] <= num * global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonReleaseThreshold)
			{
				bindingStatePtr->pressTime = 0.0;
			}
			float magnitude = trigger.magnitude;
			global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = actionStates + actionIndex;
			if (!ptr->isPressed && magnitude >= num)
			{
				ptr->framePressed = global::UnityEngine.Time.frameCount;
				ptr->pressedInUpdate = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
				ptr->isPressed = true;
			}
			else if (ptr->isPressed)
			{
				float num2 = num * global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
				if (magnitude <= num2)
				{
					ptr->frameReleased = global::UnityEngine.Time.frameCount;
					ptr->releasedInUpdate = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
					ptr->isPressed = false;
				}
			}
		}

		private unsafe static bool ShouldIgnoreInputOnCompositeBinding(global::UnityEngine.InputSystem.InputActionState.BindingState* binding, global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
		{
			if (eventPtr == null)
			{
				return false;
			}
			int eventId = eventPtr->eventId;
			if (eventId != 0 && binding->triggerEventIdForComposite == eventId)
			{
				return true;
			}
			binding->triggerEventIdForComposite = eventId;
			return false;
		}

		private unsafe bool IsConflictingInput(ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger, int actionIndex)
		{
			global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = actionStates + actionIndex;
			if (!ptr->mayNeedConflictResolution)
			{
				return false;
			}
			int num = trigger.controlIndex;
			if (bindingStates[trigger.bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = bindingStates[trigger.bindingIndex].compositeOrCompositeBindingIndex;
				num = bindingStates[compositeOrCompositeBindingIndex].controlStartIndex;
			}
			int num2 = ptr->controlIndex;
			if (bindingStates[ptr->bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex2 = bindingStates[ptr->bindingIndex].compositeOrCompositeBindingIndex;
				num2 = bindingStates[compositeOrCompositeBindingIndex2].controlStartIndex;
			}
			if (num2 == -1)
			{
				ptr->magnitude = trigger.magnitude;
				return false;
			}
			bool flag = num == num2 || controls[num] == controls[num2];
			if (trigger.magnitude > ptr->magnitude)
			{
				if (trigger.magnitude > 0f && !flag && ptr->magnitude > 0f)
				{
					ptr->hasMultipleConcurrentActuations = true;
				}
				ptr->magnitude = trigger.magnitude;
				return false;
			}
			if (trigger.magnitude < ptr->magnitude)
			{
				if (!flag)
				{
					if (trigger.magnitude > 0f)
					{
						ptr->hasMultipleConcurrentActuations = true;
					}
					return true;
				}
				if (!ptr->hasMultipleConcurrentActuations)
				{
					ptr->magnitude = trigger.magnitude;
					return false;
				}
				ushort bindingCount;
				ushort actionBindingStartIndexAndCount = GetActionBindingStartIndexAndCount(actionIndex, out bindingCount);
				float num3 = trigger.magnitude;
				int num4 = -1;
				int num5 = -1;
				int num6 = 0;
				for (int i = 0; i < bindingCount; i++)
				{
					ushort num7 = memory.actionBindingIndices[actionBindingStartIndexAndCount + i];
					global::UnityEngine.InputSystem.InputActionState.BindingState* ptr2 = memory.bindingStates + (int)num7;
					if (ptr2->isComposite)
					{
						int controlStartIndex = ptr2->controlStartIndex;
						int compositeOrCompositeBindingIndex3 = ptr2->compositeOrCompositeBindingIndex;
						float num8 = memory.compositeMagnitudes[compositeOrCompositeBindingIndex3];
						if (num8 > 0f)
						{
							num6++;
						}
						if (num8 > num3)
						{
							num4 = controlStartIndex;
							num5 = controlIndexToBindingIndex[controlStartIndex];
							num3 = num8;
						}
					}
					else
					{
						if (ptr2->isPartOfComposite)
						{
							continue;
						}
						for (int j = 0; j < ptr2->controlCount; j++)
						{
							int num9 = ptr2->controlStartIndex + j;
							float num10 = memory.controlMagnitudes[num9];
							if (num10 > 0f)
							{
								num6++;
							}
							if (num10 > num3)
							{
								num4 = num9;
								num5 = num7;
								num3 = num10;
							}
						}
					}
				}
				if (num6 <= 1)
				{
					ptr->hasMultipleConcurrentActuations = false;
				}
				if (num4 != -1)
				{
					trigger.controlIndex = num4;
					trigger.bindingIndex = num5;
					trigger.magnitude = num3;
					if (ptr->bindingIndex != num5)
					{
						if (ptr->interactionIndex != -1)
						{
							ResetInteractionState(ptr->interactionIndex);
						}
						global::UnityEngine.InputSystem.InputActionState.BindingState* num11 = bindingStates + num5;
						int interactionCount = num11->interactionCount;
						int interactionStartIndex = num11->interactionStartIndex;
						for (int k = 0; k < interactionCount; k++)
						{
							if (interactionStates[interactionStartIndex + k].phase.IsInProgress())
							{
								ptr->interactionIndex = interactionStartIndex + k;
								trigger.interactionIndex = interactionStartIndex + k;
								break;
							}
						}
					}
					ptr->controlIndex = num4;
					ptr->bindingIndex = num5;
					ptr->magnitude = num3;
					return false;
				}
			}
			if (!flag && global::UnityEngine.Mathf.Approximately(trigger.magnitude, ptr->magnitude))
			{
				if (trigger.magnitude > 0f)
				{
					ptr->hasMultipleConcurrentActuations = true;
				}
				return true;
			}
			return false;
		}

		private unsafe ushort GetActionBindingStartIndexAndCount(int actionIndex, out ushort bindingCount)
		{
			bindingCount = memory.actionBindingIndicesAndCounts[actionIndex * 2 + 1];
			return memory.actionBindingIndicesAndCounts[actionIndex * 2];
		}

		private unsafe void ProcessDefaultInteraction(ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger, int actionIndex)
		{
			global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = actionStates + actionIndex;
			switch (ptr->phase)
			{
			case global::UnityEngine.InputSystem.InputActionPhase.Waiting:
				if (trigger.isPassThrough)
				{
					ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref trigger);
				}
				else if (trigger.isButton)
				{
					float magnitude3 = trigger.magnitude;
					if (magnitude3 > 0f)
					{
						ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Started, ref trigger);
					}
					float num4 = ((controls[trigger.controlIndex] is global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl3) ? buttonControl3.pressPointOrDefault : global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint);
					if (magnitude3 >= num4)
					{
						ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref trigger, global::UnityEngine.InputSystem.InputActionPhase.Performed);
					}
				}
				else if (IsActuated(ref trigger))
				{
					ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Started, ref trigger);
					ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref trigger, global::UnityEngine.InputSystem.InputActionPhase.Started);
				}
				break;
			case global::UnityEngine.InputSystem.InputActionPhase.Started:
				if (ptr->isButton)
				{
					float magnitude2 = trigger.magnitude;
					float num3 = ((controls[trigger.controlIndex] is global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl2) ? buttonControl2.pressPointOrDefault : global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint);
					if (magnitude2 >= num3)
					{
						ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref trigger, global::UnityEngine.InputSystem.InputActionPhase.Performed);
					}
					else if (global::UnityEngine.Mathf.Approximately(magnitude2, 0f))
					{
						ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Canceled, ref trigger);
					}
				}
				else if (!IsActuated(ref trigger))
				{
					ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Canceled, ref trigger);
				}
				else
				{
					ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref trigger, global::UnityEngine.InputSystem.InputActionPhase.Started);
				}
				break;
			case global::UnityEngine.InputSystem.InputActionPhase.Performed:
				if (ptr->isButton)
				{
					float magnitude = trigger.magnitude;
					float num = ((controls[trigger.controlIndex] is global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl) ? buttonControl.pressPointOrDefault : global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint);
					if (global::UnityEngine.Mathf.Approximately(0f, magnitude))
					{
						ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Canceled, ref trigger);
						break;
					}
					float num2 = num * global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
					if (magnitude <= num2)
					{
						ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Started, ref trigger);
					}
				}
				else if (ptr->isPassThrough)
				{
					ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref trigger, global::UnityEngine.InputSystem.InputActionPhase.Performed);
				}
				break;
			}
		}

		private unsafe void ProcessInteractions(ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger, int interactionStartIndex, int interactionCount)
		{
			global::UnityEngine.InputSystem.InputInteractionContext context = new global::UnityEngine.InputSystem.InputInteractionContext
			{
				m_State = this,
				m_TriggerState = trigger
			};
			for (int i = 0; i < interactionCount; i++)
			{
				int num = interactionStartIndex + i;
				global::UnityEngine.InputSystem.InputActionState.InteractionState interactionState = interactionStates[num];
				global::UnityEngine.InputSystem.IInputInteraction obj = interactions[num];
				context.m_TriggerState.phase = interactionState.phase;
				context.m_TriggerState.startTime = interactionState.startTime;
				context.m_TriggerState.interactionIndex = num;
				obj.Process(ref context);
			}
		}

		private unsafe void ProcessTimeout(double time, int mapIndex, int controlIndex, int bindingIndex, int interactionIndex)
		{
			ref global::UnityEngine.InputSystem.InputActionState.InteractionState reference = ref interactionStates[interactionIndex];
			global::UnityEngine.InputSystem.InputInteractionContext context = new global::UnityEngine.InputSystem.InputInteractionContext
			{
				m_State = this,
				m_TriggerState = new global::UnityEngine.InputSystem.InputActionState.TriggerState
				{
					phase = reference.phase,
					time = time,
					mapIndex = mapIndex,
					controlIndex = controlIndex,
					bindingIndex = bindingIndex,
					interactionIndex = interactionIndex,
					startTime = reference.startTime
				},
				timerHasExpired = true
			};
			reference.isTimerRunning = false;
			reference.totalTimeoutCompletionTimeRemaining = global::UnityEngine.Mathf.Max(reference.totalTimeoutCompletionTimeRemaining - reference.timerDuration, 0f);
			reference.timerDuration = 0f;
			interactions[interactionIndex].Process(ref context);
		}

		internal unsafe void SetTotalTimeoutCompletionTime(float seconds, ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger)
		{
			global::UnityEngine.InputSystem.InputActionState.InteractionState* num = interactionStates + trigger.interactionIndex;
			num->totalTimeoutCompletionDone = 0f;
			num->totalTimeoutCompletionTimeRemaining = seconds;
		}

		internal unsafe void StartTimeout(float seconds, ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger)
		{
			global::UnityEngine.InputSystem.InputManager s_Manager = global::UnityEngine.InputSystem.InputSystem.s_Manager;
			double time = trigger.time;
			global::UnityEngine.InputSystem.InputControl control = controls[trigger.controlIndex];
			int interactionIndex = trigger.interactionIndex;
			long num = ToCombinedMapAndControlAndBindingIndex(trigger.mapIndex, trigger.controlIndex, trigger.bindingIndex);
			global::UnityEngine.InputSystem.InputActionState.InteractionState* num2 = interactionStates + interactionIndex;
			if (num2->isTimerRunning)
			{
				StopTimeout(interactionIndex);
			}
			s_Manager.AddStateChangeMonitorTimeout(control, this, time + (double)seconds, num, interactionIndex);
			num2->isTimerRunning = true;
			num2->timerStartTime = time;
			num2->timerDuration = seconds;
			num2->timerMonitorIndex = num;
		}

		private unsafe void StopTimeout(int interactionIndex)
		{
			ref global::UnityEngine.InputSystem.InputActionState.InteractionState reference = ref interactionStates[interactionIndex];
			global::UnityEngine.InputSystem.InputSystem.s_Manager.RemoveStateChangeMonitorTimeout(this, reference.timerMonitorIndex, interactionIndex);
			reference.isTimerRunning = false;
			reference.totalTimeoutCompletionDone += reference.timerDuration;
			reference.totalTimeoutCompletionTimeRemaining = global::UnityEngine.Mathf.Max(reference.totalTimeoutCompletionTimeRemaining - reference.timerDuration, 0f);
			reference.timerDuration = 0f;
			reference.timerStartTime = 0.0;
			reference.timerMonitorIndex = 0L;
		}

		internal unsafe void ChangePhaseOfInteraction(global::UnityEngine.InputSystem.InputActionPhase newPhase, ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger, global::UnityEngine.InputSystem.InputActionPhase phaseAfterPerformed = global::UnityEngine.InputSystem.InputActionPhase.Waiting, global::UnityEngine.InputSystem.InputActionPhase phaseAfterCanceled = global::UnityEngine.InputSystem.InputActionPhase.Waiting, bool processNextInteractionOnCancel = true)
		{
			int interactionIndex = trigger.interactionIndex;
			int bindingIndex = trigger.bindingIndex;
			global::UnityEngine.InputSystem.InputActionPhase phaseAfterPerformedOrCanceled = global::UnityEngine.InputSystem.InputActionPhase.Waiting;
			switch (newPhase)
			{
			case global::UnityEngine.InputSystem.InputActionPhase.Performed:
				phaseAfterPerformedOrCanceled = phaseAfterPerformed;
				break;
			case global::UnityEngine.InputSystem.InputActionPhase.Canceled:
				phaseAfterPerformedOrCanceled = phaseAfterCanceled;
				break;
			}
			ref global::UnityEngine.InputSystem.InputActionState.InteractionState reference = ref interactionStates[interactionIndex];
			if (reference.isTimerRunning)
			{
				StopTimeout(trigger.interactionIndex);
			}
			reference.phase = newPhase;
			reference.triggerControlIndex = trigger.controlIndex;
			reference.startTime = trigger.startTime;
			if (newPhase == global::UnityEngine.InputSystem.InputActionPhase.Performed)
			{
				reference.performedTime = trigger.time;
			}
			int actionIndex = bindingStates[bindingIndex].actionIndex;
			if (actionIndex != -1)
			{
				if (actionStates[actionIndex].phase == global::UnityEngine.InputSystem.InputActionPhase.Waiting)
				{
					if (!ChangePhaseOfAction(newPhase, ref trigger, phaseAfterPerformedOrCanceled))
					{
						return;
					}
				}
				else if (newPhase == global::UnityEngine.InputSystem.InputActionPhase.Canceled && actionStates[actionIndex].interactionIndex == trigger.interactionIndex)
				{
					if (!ChangePhaseOfAction(newPhase, ref trigger, phaseAfterPerformedOrCanceled) || !processNextInteractionOnCancel)
					{
						return;
					}
					int interactionStartIndex = bindingStates[bindingIndex].interactionStartIndex;
					int interactionCount = bindingStates[bindingIndex].interactionCount;
					for (int i = 0; i < interactionCount; i++)
					{
						int num = interactionStartIndex + i;
						if (num == trigger.interactionIndex || (interactionStates[num].phase != global::UnityEngine.InputSystem.InputActionPhase.Started && interactionStates[num].phase != global::UnityEngine.InputSystem.InputActionPhase.Performed))
						{
							continue;
						}
						double startTime = interactionStates[num].startTime;
						global::UnityEngine.InputSystem.InputActionState.TriggerState trigger2 = new global::UnityEngine.InputSystem.InputActionState.TriggerState
						{
							phase = global::UnityEngine.InputSystem.InputActionPhase.Started,
							controlIndex = interactionStates[num].triggerControlIndex,
							bindingIndex = trigger.bindingIndex,
							interactionIndex = num,
							mapIndex = trigger.mapIndex,
							time = startTime,
							startTime = startTime
						};
						if (!ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Started, ref trigger2, phaseAfterPerformedOrCanceled))
						{
							return;
						}
						if (interactionStates[num].phase != global::UnityEngine.InputSystem.InputActionPhase.Performed)
						{
							break;
						}
						trigger2 = new global::UnityEngine.InputSystem.InputActionState.TriggerState
						{
							phase = global::UnityEngine.InputSystem.InputActionPhase.Performed,
							controlIndex = interactionStates[num].triggerControlIndex,
							bindingIndex = trigger.bindingIndex,
							interactionIndex = num,
							mapIndex = trigger.mapIndex,
							time = interactionStates[num].performedTime,
							startTime = startTime
						};
						if (!ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref trigger2, phaseAfterPerformedOrCanceled))
						{
							return;
						}
						for (; i < interactionCount; i++)
						{
							num = interactionStartIndex + i;
							ResetInteractionState(num);
						}
						break;
					}
				}
				else if (actionStates[actionIndex].interactionIndex == trigger.interactionIndex)
				{
					if (!ChangePhaseOfAction(newPhase, ref trigger, phaseAfterPerformedOrCanceled))
					{
						return;
					}
					if (newPhase == global::UnityEngine.InputSystem.InputActionPhase.Performed)
					{
						int interactionStartIndex2 = bindingStates[bindingIndex].interactionStartIndex;
						int interactionCount2 = bindingStates[bindingIndex].interactionCount;
						for (int j = 0; j < interactionCount2; j++)
						{
							int num2 = interactionStartIndex2 + j;
							if (num2 != trigger.interactionIndex)
							{
								ResetInteractionState(num2);
							}
						}
					}
				}
			}
			if (newPhase != global::UnityEngine.InputSystem.InputActionPhase.Performed || actionIndex == -1 || actionStates[actionIndex].isPerformed || actionStates[actionIndex].interactionIndex == trigger.interactionIndex)
			{
				if (newPhase == global::UnityEngine.InputSystem.InputActionPhase.Performed && phaseAfterPerformed != global::UnityEngine.InputSystem.InputActionPhase.Waiting)
				{
					reference.phase = phaseAfterPerformed;
				}
				else if (newPhase == global::UnityEngine.InputSystem.InputActionPhase.Performed || newPhase == global::UnityEngine.InputSystem.InputActionPhase.Canceled)
				{
					ResetInteractionState(trigger.interactionIndex);
				}
			}
		}

		private unsafe bool ChangePhaseOfAction(global::UnityEngine.InputSystem.InputActionPhase newPhase, ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger, global::UnityEngine.InputSystem.InputActionPhase phaseAfterPerformedOrCanceled = global::UnityEngine.InputSystem.InputActionPhase.Waiting)
		{
			int actionIndex = bindingStates[trigger.bindingIndex].actionIndex;
			if (actionIndex == -1)
			{
				return true;
			}
			global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = actionStates + actionIndex;
			if (ptr->isDisabled)
			{
				return true;
			}
			ptr->inProcessing = true;
			try
			{
				if (ptr->isPassThrough && trigger.interactionIndex == -1)
				{
					ChangePhaseOfActionInternal(actionIndex, ptr, newPhase, ref trigger, newPhase == global::UnityEngine.InputSystem.InputActionPhase.Canceled && phaseAfterPerformedOrCanceled == global::UnityEngine.InputSystem.InputActionPhase.Disabled);
					if (!ptr->inProcessing)
					{
						return false;
					}
				}
				else if (newPhase == global::UnityEngine.InputSystem.InputActionPhase.Performed && ptr->phase == global::UnityEngine.InputSystem.InputActionPhase.Waiting)
				{
					ChangePhaseOfActionInternal(actionIndex, ptr, global::UnityEngine.InputSystem.InputActionPhase.Started, ref trigger);
					if (!ptr->inProcessing)
					{
						return false;
					}
					ChangePhaseOfActionInternal(actionIndex, ptr, newPhase, ref trigger);
					if (!ptr->inProcessing)
					{
						return false;
					}
					if (phaseAfterPerformedOrCanceled == global::UnityEngine.InputSystem.InputActionPhase.Waiting)
					{
						ChangePhaseOfActionInternal(actionIndex, ptr, global::UnityEngine.InputSystem.InputActionPhase.Canceled, ref trigger);
					}
					if (!ptr->inProcessing)
					{
						return false;
					}
					ptr->phase = phaseAfterPerformedOrCanceled;
				}
				else if (ptr->phase != newPhase || newPhase == global::UnityEngine.InputSystem.InputActionPhase.Performed)
				{
					ChangePhaseOfActionInternal(actionIndex, ptr, newPhase, ref trigger, newPhase == global::UnityEngine.InputSystem.InputActionPhase.Canceled && phaseAfterPerformedOrCanceled == global::UnityEngine.InputSystem.InputActionPhase.Disabled);
					if (!ptr->inProcessing)
					{
						return false;
					}
					if (newPhase == global::UnityEngine.InputSystem.InputActionPhase.Performed || newPhase == global::UnityEngine.InputSystem.InputActionPhase.Canceled)
					{
						ptr->phase = phaseAfterPerformedOrCanceled;
					}
				}
			}
			finally
			{
				ptr->inProcessing = false;
			}
			if (ptr->phase == global::UnityEngine.InputSystem.InputActionPhase.Waiting)
			{
				ptr->controlIndex = -1;
				ptr->flags &= ~global::UnityEngine.InputSystem.InputActionState.TriggerState.Flags.HaveMagnitude;
			}
			return true;
		}

		private unsafe void ChangePhaseOfActionInternal(int actionIndex, global::UnityEngine.InputSystem.InputActionState.TriggerState* actionState, global::UnityEngine.InputSystem.InputActionPhase newPhase, ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger, bool isDisablingAction = false)
		{
			global::UnityEngine.InputSystem.InputActionState.TriggerState triggerState = trigger;
			triggerState.flags = actionState->flags;
			if (newPhase != global::UnityEngine.InputSystem.InputActionPhase.Canceled)
			{
				triggerState.magnitude = trigger.magnitude;
			}
			else
			{
				triggerState.magnitude = 0f;
			}
			triggerState.phase = newPhase;
			switch (newPhase)
			{
			case global::UnityEngine.InputSystem.InputActionPhase.Performed:
				triggerState.framePerformed = global::UnityEngine.Time.frameCount;
				triggerState.lastPerformedInUpdate = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
				triggerState.lastCanceledInUpdate = actionState->lastCanceledInUpdate;
				if (controlGroupingAndComplexity[trigger.controlIndex * 2 + 1] > 1 && m_CurrentlyProcessingThisEvent.valid)
				{
					m_CurrentlyProcessingThisEvent.handled = true;
				}
				break;
			case global::UnityEngine.InputSystem.InputActionPhase.Canceled:
				triggerState.lastCanceledInUpdate = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
				triggerState.lastPerformedInUpdate = actionState->lastPerformedInUpdate;
				triggerState.framePerformed = actionState->framePerformed;
				break;
			default:
				triggerState.lastPerformedInUpdate = actionState->lastPerformedInUpdate;
				triggerState.framePerformed = actionState->framePerformed;
				triggerState.lastCanceledInUpdate = actionState->lastCanceledInUpdate;
				break;
			}
			if (actionState->phase == global::UnityEngine.InputSystem.InputActionPhase.Performed && newPhase != global::UnityEngine.InputSystem.InputActionPhase.Performed && !isDisablingAction)
			{
				triggerState.frameCompleted = global::UnityEngine.Time.frameCount;
				triggerState.lastCompletedInUpdate = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
			}
			else
			{
				triggerState.lastCompletedInUpdate = actionState->lastCompletedInUpdate;
				triggerState.frameCompleted = actionState->frameCompleted;
			}
			triggerState.pressedInUpdate = actionState->pressedInUpdate;
			triggerState.framePressed = actionState->framePressed;
			triggerState.releasedInUpdate = actionState->releasedInUpdate;
			triggerState.frameReleased = actionState->frameReleased;
			if (newPhase == global::UnityEngine.InputSystem.InputActionPhase.Started)
			{
				triggerState.startTime = triggerState.time;
			}
			*actionState = triggerState;
			global::UnityEngine.InputSystem.InputActionMap inputActionMap = maps[trigger.mapIndex];
			global::UnityEngine.InputSystem.InputAction inputAction = inputActionMap.m_Actions[actionIndex - mapIndices[trigger.mapIndex].actionStartIndex];
			trigger.phase = newPhase;
			if (!m_Suppressed)
			{
				switch (newPhase)
				{
				case global::UnityEngine.InputSystem.InputActionPhase.Started:
					CallActionListeners(actionIndex, inputActionMap, newPhase, ref inputAction.m_OnStarted, "started");
					break;
				case global::UnityEngine.InputSystem.InputActionPhase.Performed:
					CallActionListeners(actionIndex, inputActionMap, newPhase, ref inputAction.m_OnPerformed, "performed");
					break;
				case global::UnityEngine.InputSystem.InputActionPhase.Canceled:
					CallActionListeners(actionIndex, inputActionMap, newPhase, ref inputAction.m_OnCanceled, "canceled");
					break;
				}
			}
		}

		private void CallActionListeners(int actionIndex, global::UnityEngine.InputSystem.InputActionMap actionMap, global::UnityEngine.InputSystem.InputActionPhase phase, ref global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>> listeners, string callbackName)
		{
			global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>> callbacks = actionMap.m_ActionCallbacks;
			if (listeners.length == 0 && callbacks.length == 0 && s_GlobalState.onActionChange.length == 0)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction.CallbackContext argument = new global::UnityEngine.InputSystem.InputAction.CallbackContext
			{
				m_State = this,
				m_ActionIndex = actionIndex
			};
			global::UnityEngine.InputSystem.InputAction action = argument.action;
			if (s_GlobalState.onActionChange.length > 0)
			{
				global::UnityEngine.InputSystem.InputActionChange argument2;
				switch (phase)
				{
				default:
					return;
				case global::UnityEngine.InputSystem.InputActionPhase.Started:
					argument2 = global::UnityEngine.InputSystem.InputActionChange.ActionStarted;
					break;
				case global::UnityEngine.InputSystem.InputActionPhase.Performed:
					argument2 = global::UnityEngine.InputSystem.InputActionChange.ActionPerformed;
					break;
				case global::UnityEngine.InputSystem.InputActionPhase.Canceled:
					argument2 = global::UnityEngine.InputSystem.InputActionChange.ActionCanceled;
					break;
				}
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref s_GlobalState.onActionChange, action, argument2, k_InputOnActionChangeMarker, "InputSystem.onActionChange");
			}
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref listeners, argument, callbackName, action);
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref callbacks, argument, callbackName, actionMap);
		}

		private object GetActionOrNoneString(ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger)
		{
			global::UnityEngine.InputSystem.InputAction actionOrNull = GetActionOrNull(ref trigger);
			if (actionOrNull == null)
			{
				return "<none>";
			}
			return actionOrNull;
		}

		internal unsafe global::UnityEngine.InputSystem.InputAction GetActionOrNull(int bindingIndex)
		{
			int actionIndex = bindingStates[bindingIndex].actionIndex;
			if (actionIndex == -1)
			{
				return null;
			}
			int mapIndex = bindingStates[bindingIndex].mapIndex;
			int actionStartIndex = mapIndices[mapIndex].actionStartIndex;
			return maps[mapIndex].m_Actions[actionIndex - actionStartIndex];
		}

		internal unsafe global::UnityEngine.InputSystem.InputAction GetActionOrNull(ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger)
		{
			int actionIndex = bindingStates[trigger.bindingIndex].actionIndex;
			if (actionIndex == -1)
			{
				return null;
			}
			int actionStartIndex = mapIndices[trigger.mapIndex].actionStartIndex;
			return maps[trigger.mapIndex].m_Actions[actionIndex - actionStartIndex];
		}

		internal global::UnityEngine.InputSystem.InputControl GetControl(ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger)
		{
			return controls[trigger.controlIndex];
		}

		private global::UnityEngine.InputSystem.IInputInteraction GetInteractionOrNull(ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger)
		{
			if (trigger.interactionIndex == -1)
			{
				return null;
			}
			return interactions[trigger.interactionIndex];
		}

		internal unsafe int GetBindingIndexInMap(int bindingIndex)
		{
			int mapIndex = bindingStates[bindingIndex].mapIndex;
			int bindingStartIndex = mapIndices[mapIndex].bindingStartIndex;
			return bindingIndex - bindingStartIndex;
		}

		internal unsafe int GetBindingIndexInState(int mapIndex, int bindingIndexInMap)
		{
			return mapIndices[mapIndex].bindingStartIndex + bindingIndexInMap;
		}

		internal unsafe ref global::UnityEngine.InputSystem.InputActionState.BindingState GetBindingState(int bindingIndex)
		{
			return ref bindingStates[bindingIndex];
		}

		internal unsafe ref global::UnityEngine.InputSystem.InputBinding GetBinding(int bindingIndex)
		{
			int mapIndex = bindingStates[bindingIndex].mapIndex;
			int bindingStartIndex = mapIndices[mapIndex].bindingStartIndex;
			return ref maps[mapIndex].m_Bindings[bindingIndex - bindingStartIndex];
		}

		internal unsafe global::UnityEngine.InputSystem.InputActionMap GetActionMap(int bindingIndex)
		{
			int mapIndex = bindingStates[bindingIndex].mapIndex;
			return maps[mapIndex];
		}

		private unsafe void ResetInteractionStateAndCancelIfNecessary(int mapIndex, int bindingIndex, int interactionIndex, global::UnityEngine.InputSystem.InputActionPhase phaseAfterCanceled)
		{
			int actionIndex = bindingStates[bindingIndex].actionIndex;
			if (actionStates[actionIndex].interactionIndex == interactionIndex)
			{
				global::UnityEngine.InputSystem.InputActionPhase phase = interactionStates[interactionIndex].phase;
				if ((uint)(phase - 2) <= 1u)
				{
					ChangePhaseOfInteraction(global::UnityEngine.InputSystem.InputActionPhase.Canceled, ref actionStates[actionIndex], global::UnityEngine.InputSystem.InputActionPhase.Waiting, phaseAfterCanceled, processNextInteractionOnCancel: false);
				}
				actionStates[actionIndex].interactionIndex = -1;
			}
			ResetInteractionState(interactionIndex);
		}

		private unsafe void ResetInteractionState(int interactionIndex)
		{
			interactions[interactionIndex].Reset();
			if (interactionStates[interactionIndex].isTimerRunning)
			{
				StopTimeout(interactionIndex);
			}
			interactionStates[interactionIndex] = new global::UnityEngine.InputSystem.InputActionState.InteractionState
			{
				phase = global::UnityEngine.InputSystem.InputActionPhase.Waiting,
				triggerControlIndex = -1
			};
		}

		internal unsafe int GetValueSizeInBytes(int bindingIndex, int controlIndex)
		{
			if (bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				return composites[compositeOrCompositeBindingIndex2].valueSizeInBytes;
			}
			return controls[controlIndex].valueSizeInBytes;
		}

		internal unsafe global::System.Type GetValueType(int bindingIndex, int controlIndex)
		{
			if (bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				return composites[compositeOrCompositeBindingIndex2].valueType;
			}
			return controls[controlIndex].valueType;
		}

		internal static bool IsActuated(ref global::UnityEngine.InputSystem.InputActionState.TriggerState trigger, float threshold = 0f)
		{
			float magnitude = trigger.magnitude;
			if (magnitude < 0f)
			{
				return true;
			}
			if (global::UnityEngine.Mathf.Approximately(threshold, 0f))
			{
				return magnitude > 0f;
			}
			return magnitude >= threshold;
		}

		internal unsafe void ReadValue(int bindingIndex, int controlIndex, void* buffer, int bufferSize, bool ignoreComposites = false)
		{
			global::UnityEngine.InputSystem.InputControl control = null;
			if (!ignoreComposites && bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				global::UnityEngine.InputSystem.InputBindingComposite obj = composites[compositeOrCompositeBindingIndex2];
				global::UnityEngine.InputSystem.InputBindingCompositeContext context = new global::UnityEngine.InputSystem.InputBindingCompositeContext
				{
					m_State = this,
					m_BindingIndex = compositeOrCompositeBindingIndex
				};
				obj.ReadValue(ref context, buffer, bufferSize);
				bindingIndex = compositeOrCompositeBindingIndex;
			}
			else
			{
				control = controls[controlIndex];
				control.ReadValueIntoBuffer(buffer, bufferSize);
			}
			int processorCount = bindingStates[bindingIndex].processorCount;
			if (processorCount > 0)
			{
				int processorStartIndex = bindingStates[bindingIndex].processorStartIndex;
				for (int i = 0; i < processorCount; i++)
				{
					processors[processorStartIndex + i].Process(buffer, bufferSize, control);
				}
			}
		}

		internal unsafe TValue ReadValue<TValue>(int bindingIndex, int controlIndex, bool ignoreComposites = false) where TValue : struct
		{
			TValue output = default(TValue);
			global::UnityEngine.InputSystem.InputControl<TValue> inputControl = null;
			if (!ignoreComposites && bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				global::UnityEngine.InputSystem.InputBindingComposite inputBindingComposite = composites[compositeOrCompositeBindingIndex2];
				global::UnityEngine.InputSystem.InputBindingCompositeContext context = new global::UnityEngine.InputSystem.InputBindingCompositeContext
				{
					m_State = this,
					m_BindingIndex = compositeOrCompositeBindingIndex
				};
				if (!(inputBindingComposite is global::UnityEngine.InputSystem.InputBindingComposite<TValue> inputBindingComposite2))
				{
					global::System.Type valueType = inputBindingComposite.valueType;
					if (!valueType.IsAssignableFrom(typeof(TValue)))
					{
						throw new global::System.InvalidOperationException($"Cannot read value of type '{typeof(TValue).Name}' from composite '{inputBindingComposite}' bound to action '{GetActionOrNull(bindingIndex)}' (composite is a '{compositeOrCompositeBindingIndex2.GetType().Name}' with value type '{(global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetNiceTypeName(valueType))}')");
					}
					inputBindingComposite.ReadValue(ref context, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>());
				}
				else
				{
					output = inputBindingComposite2.ReadValue(ref context);
				}
				bindingIndex = compositeOrCompositeBindingIndex;
			}
			else if (controlIndex != -1)
			{
				global::UnityEngine.InputSystem.InputControl inputControl2 = controls[controlIndex];
				inputControl = inputControl2 as global::UnityEngine.InputSystem.InputControl<TValue>;
				if (inputControl == null)
				{
					throw new global::System.InvalidOperationException($"Cannot read value of type '{(global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetNiceTypeName(typeof(TValue)))}' from control '{inputControl2.path}' bound to action '{GetActionOrNull(bindingIndex)}' (control is a '{inputControl2.GetType().Name}' with value type '{(global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetNiceTypeName(inputControl2.valueType))}')");
				}
				output = inputControl.value;
			}
			return ApplyProcessors(bindingIndex, output, inputControl);
		}

		internal unsafe TValue ApplyProcessors<TValue>(int bindingIndex, TValue value, global::UnityEngine.InputSystem.InputControl<TValue> controlOfType = null) where TValue : struct
		{
			if (totalBindingCount == 0)
			{
				return value;
			}
			int processorCount = bindingStates[bindingIndex].processorCount;
			if (processorCount > 0)
			{
				int processorStartIndex = bindingStates[bindingIndex].processorStartIndex;
				for (int i = 0; i < processorCount; i++)
				{
					if (processors[processorStartIndex + i] is global::UnityEngine.InputSystem.InputProcessor<TValue> inputProcessor)
					{
						value = inputProcessor.Process(value, controlOfType);
					}
				}
			}
			return value;
		}

		public unsafe float EvaluateCompositePartMagnitude(int bindingIndex, int partNumber)
		{
			int num = bindingIndex + 1;
			float num2 = float.MinValue;
			for (int i = num; i < totalBindingCount && bindingStates[i].isPartOfComposite; i++)
			{
				if (bindingStates[i].partIndex == partNumber)
				{
					int controlCount = bindingStates[i].controlCount;
					int controlStartIndex = bindingStates[i].controlStartIndex;
					for (int j = 0; j < controlCount; j++)
					{
						num2 = global::UnityEngine.Mathf.Max(controls[controlStartIndex + j].magnitude, num2);
					}
				}
			}
			return num2;
		}

		internal unsafe double GetCompositePartPressTime(int bindingIndex, int partNumber)
		{
			int num = bindingIndex + 1;
			double num2 = double.MaxValue;
			for (int i = num; i < totalBindingCount && bindingStates[i].isPartOfComposite; i++)
			{
				ref global::UnityEngine.InputSystem.InputActionState.BindingState reference = ref bindingStates[i];
				if (reference.partIndex == partNumber && reference.pressTime != 0.0 && reference.pressTime < num2)
				{
					num2 = reference.pressTime;
				}
			}
			if (num2 == double.MaxValue)
			{
				return -1.0;
			}
			return num2;
		}

		internal unsafe TValue ReadCompositePartValue<TValue, TComparer>(int bindingIndex, int partNumber, bool* buttonValuePtr, out int controlIndex, TComparer comparer = default(TComparer)) where TValue : struct where TComparer : global::System.Collections.Generic.IComparer<TValue>
		{
			TValue val = default(TValue);
			int num = bindingIndex + 1;
			bool flag = true;
			controlIndex = -1;
			for (int i = num; i < totalBindingCount && bindingStates[i].isPartOfComposite; i++)
			{
				if (bindingStates[i].partIndex != partNumber)
				{
					continue;
				}
				int controlCount = bindingStates[i].controlCount;
				int controlStartIndex = bindingStates[i].controlStartIndex;
				for (int j = 0; j < controlCount; j++)
				{
					int num2 = controlStartIndex + j;
					TValue output = ReadValue<TValue>(i, num2, ignoreComposites: true);
					if (flag)
					{
						val = output;
						controlIndex = num2;
						flag = false;
					}
					else if (comparer.Compare(output, val) > 0)
					{
						val = output;
						controlIndex = num2;
					}
					if (buttonValuePtr != null && controlIndex == num2)
					{
						global::UnityEngine.InputSystem.InputControl inputControl = controls[num2];
						if (inputControl is global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl)
						{
							*buttonValuePtr = buttonControl.isPressed;
						}
						else if (inputControl is global::UnityEngine.InputSystem.InputControl<float>)
						{
							void* ptr = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output);
							*buttonValuePtr = *(float*)ptr >= global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint;
						}
					}
				}
			}
			return val;
		}

		internal unsafe bool ReadCompositePartValue(int bindingIndex, int partNumber, void* buffer, int bufferSize)
		{
			int num = bindingIndex + 1;
			float num2 = float.MinValue;
			for (int i = num; i < totalBindingCount && bindingStates[i].isPartOfComposite; i++)
			{
				if (bindingStates[i].partIndex != partNumber)
				{
					continue;
				}
				int controlCount = bindingStates[i].controlCount;
				int controlStartIndex = bindingStates[i].controlStartIndex;
				for (int j = 0; j < controlCount; j++)
				{
					int num3 = controlStartIndex + j;
					float magnitude = controls[num3].magnitude;
					if (!(magnitude < num2))
					{
						ReadValue(i, num3, buffer, bufferSize, ignoreComposites: true);
						num2 = magnitude;
					}
				}
			}
			return num2 > float.MinValue;
		}

		internal unsafe object ReadCompositePartValueAsObject(int bindingIndex, int partNumber)
		{
			int num = bindingIndex + 1;
			float num2 = float.MinValue;
			object result = null;
			for (int i = num; i < totalBindingCount && bindingStates[i].isPartOfComposite; i++)
			{
				if (bindingStates[i].partIndex != partNumber)
				{
					continue;
				}
				int controlCount = bindingStates[i].controlCount;
				int controlStartIndex = bindingStates[i].controlStartIndex;
				for (int j = 0; j < controlCount; j++)
				{
					int num3 = controlStartIndex + j;
					float magnitude = controls[num3].magnitude;
					if (!(magnitude < num2))
					{
						result = ReadValueAsObject(i, num3, ignoreComposites: true);
						num2 = magnitude;
					}
				}
			}
			return result;
		}

		internal unsafe object ReadValueAsObject(int bindingIndex, int controlIndex, bool ignoreComposites = false)
		{
			global::UnityEngine.InputSystem.InputControl control = null;
			object obj = null;
			if (!ignoreComposites && bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				global::UnityEngine.InputSystem.InputBindingComposite obj2 = composites[compositeOrCompositeBindingIndex2];
				global::UnityEngine.InputSystem.InputBindingCompositeContext context = new global::UnityEngine.InputSystem.InputBindingCompositeContext
				{
					m_State = this,
					m_BindingIndex = compositeOrCompositeBindingIndex
				};
				obj = obj2.ReadValueAsObject(ref context);
				bindingIndex = compositeOrCompositeBindingIndex;
			}
			else if (controlIndex != -1)
			{
				control = controls[controlIndex];
				obj = control.ReadValueAsObject();
			}
			if (obj != null)
			{
				int processorCount = bindingStates[bindingIndex].processorCount;
				if (processorCount > 0)
				{
					int processorStartIndex = bindingStates[bindingIndex].processorStartIndex;
					for (int i = 0; i < processorCount; i++)
					{
						obj = processors[processorStartIndex + i].ProcessAsObject(obj, control);
					}
				}
			}
			return obj;
		}

		internal unsafe bool ReadValueAsButton(int bindingIndex, int controlIndex)
		{
			global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = null;
			if (!bindingStates[bindingIndex].isPartOfComposite)
			{
				buttonControl = controls[controlIndex] as global::UnityEngine.InputSystem.Controls.ButtonControl;
			}
			float num = ReadValue<float>(bindingIndex, controlIndex);
			if (buttonControl != null)
			{
				return num >= buttonControl.pressPointOrDefault;
			}
			return num >= global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint;
		}

		internal static global::UnityEngine.InputSystem.Utilities.ISavedState SaveAndResetState()
		{
			global::UnityEngine.InputSystem.Utilities.SavedStructState<global::UnityEngine.InputSystem.InputActionState.GlobalState> result = new global::UnityEngine.InputSystem.Utilities.SavedStructState<global::UnityEngine.InputSystem.InputActionState.GlobalState>(ref s_GlobalState, delegate(ref global::UnityEngine.InputSystem.InputActionState.GlobalState state)
			{
				s_GlobalState = state;
			}, delegate
			{
				ResetGlobals();
			});
			s_GlobalState = default(global::UnityEngine.InputSystem.InputActionState.GlobalState);
			return result;
		}

		private void AddToGlobalList()
		{
			CompactGlobalList();
			global::System.Runtime.InteropServices.GCHandle value = global::System.Runtime.InteropServices.GCHandle.Alloc(this, global::System.Runtime.InteropServices.GCHandleType.Weak);
			s_GlobalState.globalList.AppendWithCapacity(value);
		}

		private void RemoveMapFromGlobalList()
		{
			int length = s_GlobalState.globalList.length;
			for (int i = 0; i < length; i++)
			{
				if (s_GlobalState.globalList[i].Target == this)
				{
					s_GlobalState.globalList[i].Free();
					s_GlobalState.globalList.RemoveAtByMovingTailWithCapacity(i);
					break;
				}
			}
		}

		private static void CompactGlobalList()
		{
			int length = s_GlobalState.globalList.length;
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				global::System.Runtime.InteropServices.GCHandle value = s_GlobalState.globalList[i];
				if (value.IsAllocated && value.Target != null)
				{
					if (num != i)
					{
						s_GlobalState.globalList[num] = value;
					}
					num++;
				}
				else
				{
					if (value.IsAllocated)
					{
						s_GlobalState.globalList[i].Free();
					}
					s_GlobalState.globalList[i] = default(global::System.Runtime.InteropServices.GCHandle);
				}
			}
			s_GlobalState.globalList.length = num;
		}

		internal void NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange change)
		{
			for (int i = 0; i < totalMapCount; i++)
			{
				global::UnityEngine.InputSystem.InputActionMap inputActionMap = maps[i];
				if (inputActionMap.m_SingletonAction != null)
				{
					NotifyListenersOfActionChange(change, inputActionMap.m_SingletonAction);
					continue;
				}
				if (inputActionMap.m_Asset == null)
				{
					NotifyListenersOfActionChange(change, inputActionMap);
					continue;
				}
				NotifyListenersOfActionChange(change, inputActionMap.m_Asset);
				break;
			}
		}

		internal static void NotifyListenersOfActionChange(global::UnityEngine.InputSystem.InputActionChange change, object actionOrMapOrAsset)
		{
			global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref s_GlobalState.onActionChange, actionOrMapOrAsset, change, k_InputOnActionChangeMarker, "InputSystem.onActionChange");
			if (change == global::UnityEngine.InputSystem.InputActionChange.BoundControlsChanged)
			{
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref s_GlobalState.onActionControlsChanged, actionOrMapOrAsset, "onActionControlsChange");
			}
		}

		private static void ResetGlobals()
		{
			DestroyAllActionMapStates();
			for (int i = 0; i < s_GlobalState.globalList.length; i++)
			{
				if (s_GlobalState.globalList[i].IsAllocated)
				{
					s_GlobalState.globalList[i].Free();
				}
			}
			s_GlobalState.globalList.length = 0;
			s_GlobalState.onActionChange.Clear();
			s_GlobalState.onActionControlsChanged.Clear();
		}

		internal unsafe static int FindAllEnabledActions(global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputAction> result)
		{
			int num = 0;
			int length = s_GlobalState.globalList.length;
			for (int i = 0; i < length; i++)
			{
				global::System.Runtime.InteropServices.GCHandle gCHandle = s_GlobalState.globalList[i];
				if (!gCHandle.IsAllocated)
				{
					continue;
				}
				global::UnityEngine.InputSystem.InputActionState inputActionState = (global::UnityEngine.InputSystem.InputActionState)gCHandle.Target;
				if (inputActionState == null)
				{
					continue;
				}
				int num2 = inputActionState.totalMapCount;
				global::UnityEngine.InputSystem.InputActionMap[] array = inputActionState.maps;
				for (int j = 0; j < num2; j++)
				{
					global::UnityEngine.InputSystem.InputActionMap inputActionMap = array[j];
					if (!inputActionMap.enabled)
					{
						continue;
					}
					global::UnityEngine.InputSystem.InputAction[] actions = inputActionMap.m_Actions;
					int num3 = actions.Length;
					if (inputActionMap.m_EnabledActionsCount == num3)
					{
						result.AddRange(actions);
						num += num3;
						continue;
					}
					int actionStartIndex = inputActionState.mapIndices[inputActionMap.m_MapIndexInState].actionStartIndex;
					for (int k = 0; k < num3; k++)
					{
						if (inputActionState.actionStates[actionStartIndex + k].phase != global::UnityEngine.InputSystem.InputActionPhase.Disabled)
						{
							result.Add(actions[k]);
							num++;
						}
					}
				}
			}
			return num;
		}

		internal static void OnDeviceChange(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputDeviceChange change)
		{
			for (int i = 0; i < s_GlobalState.globalList.length; i++)
			{
				global::System.Runtime.InteropServices.GCHandle gCHandle = s_GlobalState.globalList[i];
				if (!gCHandle.IsAllocated || gCHandle.Target == null)
				{
					if (gCHandle.IsAllocated)
					{
						s_GlobalState.globalList[i].Free();
					}
					s_GlobalState.globalList.RemoveAtWithCapacity(i);
					i--;
					continue;
				}
				global::UnityEngine.InputSystem.InputActionState inputActionState = (global::UnityEngine.InputSystem.InputActionState)gCHandle.Target;
				bool fullResolve = true;
				switch (change)
				{
				case global::UnityEngine.InputSystem.InputDeviceChange.Added:
					if (!inputActionState.CanUseDevice(device))
					{
						continue;
					}
					fullResolve = false;
					break;
				case global::UnityEngine.InputSystem.InputDeviceChange.Removed:
				{
					if (!inputActionState.IsUsingDevice(device))
					{
						continue;
					}
					for (int j = 0; j < inputActionState.totalMapCount; j++)
					{
						global::UnityEngine.InputSystem.InputActionMap obj = inputActionState.maps[j];
						obj.m_Devices.Remove(device);
						obj.asset?.m_Devices.Remove(device);
					}
					fullResolve = false;
					break;
				}
				case global::UnityEngine.InputSystem.InputDeviceChange.UsageChanged:
				case global::UnityEngine.InputSystem.InputDeviceChange.ConfigurationChanged:
					if (!inputActionState.IsUsingDevice(device) && !inputActionState.CanUseDevice(device))
					{
						continue;
					}
					break;
				case global::UnityEngine.InputSystem.InputDeviceChange.SoftReset:
				case global::UnityEngine.InputSystem.InputDeviceChange.HardReset:
					if (inputActionState.IsUsingDevice(device))
					{
						inputActionState.ResetActionStatesDrivenBy(device);
					}
					continue;
				}
				for (int k = 0; k < inputActionState.totalMapCount && !inputActionState.maps[k].LazyResolveBindings(fullResolve); k++)
				{
				}
			}
		}

		internal static void DeferredResolutionOfBindings()
		{
			global::UnityEngine.InputSystem.InputActionMap.s_DeferBindingResolution++;
			try
			{
				if (!global::UnityEngine.InputSystem.InputActionMap.s_NeedToResolveBindings)
				{
					return;
				}
				for (int i = 0; i < s_GlobalState.globalList.length; i++)
				{
					global::System.Runtime.InteropServices.GCHandle gCHandle = s_GlobalState.globalList[i];
					global::UnityEngine.InputSystem.InputActionState inputActionState = (gCHandle.IsAllocated ? ((global::UnityEngine.InputSystem.InputActionState)gCHandle.Target) : null);
					if (inputActionState == null)
					{
						if (gCHandle.IsAllocated)
						{
							s_GlobalState.globalList[i].Free();
						}
						s_GlobalState.globalList.RemoveAtWithCapacity(i);
						i--;
					}
					else
					{
						for (int j = 0; j < inputActionState.totalMapCount; j++)
						{
							inputActionState.maps[j].ResolveBindingsIfNecessary();
						}
					}
				}
				global::UnityEngine.InputSystem.InputActionMap.s_NeedToResolveBindings = false;
			}
			finally
			{
				global::UnityEngine.InputSystem.InputActionMap.s_DeferBindingResolution--;
			}
		}

		internal static void DisableAllActions()
		{
			for (int i = 0; i < s_GlobalState.globalList.length; i++)
			{
				global::System.Runtime.InteropServices.GCHandle gCHandle = s_GlobalState.globalList[i];
				if (gCHandle.IsAllocated && gCHandle.Target != null)
				{
					global::UnityEngine.InputSystem.InputActionState obj = (global::UnityEngine.InputSystem.InputActionState)gCHandle.Target;
					int num = obj.totalMapCount;
					global::UnityEngine.InputSystem.InputActionMap[] array = obj.maps;
					for (int j = 0; j < num; j++)
					{
						array[j].Disable();
					}
				}
			}
		}

		internal static void DestroyAllActionMapStates()
		{
			while (s_GlobalState.globalList.length > 0)
			{
				int index = s_GlobalState.globalList.length - 1;
				global::System.Runtime.InteropServices.GCHandle gCHandle = s_GlobalState.globalList[index];
				if (!gCHandle.IsAllocated || gCHandle.Target == null)
				{
					if (gCHandle.IsAllocated)
					{
						s_GlobalState.globalList[index].Free();
					}
					s_GlobalState.globalList.RemoveAtWithCapacity(index);
				}
				else
				{
					((global::UnityEngine.InputSystem.InputActionState)gCHandle.Target).Destroy();
				}
			}
		}
	}
}
