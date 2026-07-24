namespace UnityEngine.InputSystem.EnhancedTouch
{
	public struct Touch : global::System.IEquatable<global::UnityEngine.InputSystem.EnhancedTouch.Touch>
	{
		internal struct GlobalState
		{
			internal global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Touchscreen> touchscreens;

			internal int historyLengthPerFinger;

			internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.EnhancedTouch.Finger>> onFingerDown;

			internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.EnhancedTouch.Finger>> onFingerMove;

			internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.EnhancedTouch.Finger>> onFingerUp;

			internal global::UnityEngine.InputSystem.EnhancedTouch.Touch.FingerAndTouchState playerState;
		}

		internal struct FingerAndTouchState
		{
			public global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateMask;

			public global::UnityEngine.InputSystem.EnhancedTouch.Finger[] fingers;

			public global::UnityEngine.InputSystem.EnhancedTouch.Finger[] activeFingers;

			public global::UnityEngine.InputSystem.EnhancedTouch.Touch[] activeTouches;

			public int activeFingerCount;

			public int activeTouchCount;

			public int totalFingerCount;

			public uint lastId;

			public bool haveBuiltActiveTouches;

			public bool haveActiveTouchesNeedingRefreshNextUpdate;

			public global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState> activeTouchState;

			public void AddFingers(global::UnityEngine.InputSystem.Touchscreen screen)
			{
				int count = screen.touches.Count;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EnsureCapacity(ref fingers, totalFingerCount, count);
				for (int i = 0; i < count; i++)
				{
					global::UnityEngine.InputSystem.EnhancedTouch.Finger value = new global::UnityEngine.InputSystem.EnhancedTouch.Finger(screen, i, updateMask);
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref fingers, ref totalFingerCount, value);
				}
			}

			public void RemoveFingers(global::UnityEngine.InputSystem.Touchscreen screen)
			{
				int count = screen.touches.Count;
				for (int i = 0; i < fingers.Length; i++)
				{
					if (fingers[i].screen == screen)
					{
						for (int j = 0; j < count; j++)
						{
							fingers[i + j].m_StateHistory.Dispose();
						}
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseSliceWithCapacity(ref fingers, ref totalFingerCount, i, count);
						break;
					}
				}
				haveBuiltActiveTouches = false;
			}

			public void Destroy()
			{
				for (int i = 0; i < totalFingerCount; i++)
				{
					fingers[i].m_StateHistory.Dispose();
				}
				activeTouchState?.Dispose();
				activeTouchState = null;
			}

			public void UpdateActiveFingers()
			{
				activeFingerCount = 0;
				for (int i = 0; i < totalFingerCount; i++)
				{
					global::UnityEngine.InputSystem.EnhancedTouch.Finger finger = fingers[i];
					if (finger.currentTouch.valid)
					{
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref activeFingers, ref activeFingerCount, finger);
					}
				}
			}

			public unsafe void UpdateActiveTouches()
			{
				if (haveBuiltActiveTouches)
				{
					return;
				}
				if (activeTouchState == null)
				{
					activeTouchState = new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState>
					{
						extraMemoryPerRecord = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState>()
					};
				}
				else
				{
					activeTouchState.Clear();
					activeTouchState.m_ControlCount = 0;
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Clear(activeTouchState.m_Controls);
				}
				activeTouchCount = 0;
				haveActiveTouchesNeedingRefreshNextUpdate = false;
				uint s_UpdateStepCount = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
				for (int i = 0; i < totalFingerCount; i++)
				{
					ref global::UnityEngine.InputSystem.EnhancedTouch.Finger reference = ref fingers[i];
					global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState> stateHistory = reference.m_StateHistory;
					int count = stateHistory.Count;
					if (count == 0)
					{
						continue;
					}
					int index = activeTouchCount;
					int num = 0;
					global::UnityEngine.InputSystem.LowLevel.TouchState* ptr = default(global::UnityEngine.InputSystem.LowLevel.TouchState*);
					int num2 = stateHistory.UserIndexToRecordIndex(count - 1);
					global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* ptr2 = stateHistory.GetRecordUnchecked(num2);
					int bytesPerRecord = stateHistory.bytesPerRecord;
					int num3 = bytesPerRecord - stateHistory.extraMemoryPerRecord;
					for (int j = 0; j < count; j++)
					{
						if (j != 0)
						{
							num2--;
							if (num2 < 0)
							{
								num2 = stateHistory.historyDepth - 1;
								ptr2 = stateHistory.GetRecordUnchecked(num2);
							}
							else
							{
								ptr2 = (global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader*)((byte*)ptr2 - bytesPerRecord);
							}
						}
						global::UnityEngine.InputSystem.LowLevel.TouchState* statePtrWithoutControlIndex = (global::UnityEngine.InputSystem.LowLevel.TouchState*)ptr2->statePtrWithoutControlIndex;
						bool flag = statePtrWithoutControlIndex->updateStepCount == s_UpdateStepCount;
						if (statePtrWithoutControlIndex->touchId == num && !statePtrWithoutControlIndex->phase.IsEndedOrCanceled())
						{
							if (flag && statePtrWithoutControlIndex->phase == global::UnityEngine.InputSystem.TouchPhase.Began)
							{
								ptr->phase = global::UnityEngine.InputSystem.TouchPhase.Began;
								ptr->position = statePtrWithoutControlIndex->position;
								ptr->delta = default(global::UnityEngine.Vector2);
								haveActiveTouchesNeedingRefreshNextUpdate = true;
							}
							continue;
						}
						if (statePtrWithoutControlIndex->phase.IsEndedOrCanceled() && (!statePtrWithoutControlIndex->beganInSameFrame || statePtrWithoutControlIndex->updateStepCount != s_UpdateStepCount - 1) && !flag)
						{
							break;
						}
						global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState* source = (global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState*)((byte*)ptr2 + num3);
						int index2;
						global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* ptr3 = activeTouchState.AllocateRecord(out index2);
						global::UnityEngine.InputSystem.LowLevel.TouchState* statePtrWithControlIndex = (global::UnityEngine.InputSystem.LowLevel.TouchState*)ptr3->statePtrWithControlIndex;
						global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState* ptr4 = (global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState*)((byte*)ptr3 + activeTouchState.bytesPerRecord - global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState>());
						ptr3->time = ptr2->time;
						ptr3->controlIndex = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref activeTouchState.m_Controls, ref activeTouchState.m_ControlCount, reference.m_StateHistory.controls[0]);
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(statePtrWithControlIndex, statePtrWithoutControlIndex, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.LowLevel.TouchState>());
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr4, source, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState>());
						global::UnityEngine.InputSystem.TouchPhase phase = statePtrWithoutControlIndex->phase;
						if ((phase == global::UnityEngine.InputSystem.TouchPhase.Moved || phase == global::UnityEngine.InputSystem.TouchPhase.Began) && !flag && (phase != global::UnityEngine.InputSystem.TouchPhase.Moved || !statePtrWithoutControlIndex->beganInSameFrame || statePtrWithoutControlIndex->updateStepCount != s_UpdateStepCount - 1))
						{
							statePtrWithControlIndex->phase = global::UnityEngine.InputSystem.TouchPhase.Stationary;
							statePtrWithControlIndex->delta = default(global::UnityEngine.Vector2);
						}
						else if (!flag && !statePtrWithoutControlIndex->beganInSameFrame)
						{
							statePtrWithControlIndex->delta = default(global::UnityEngine.Vector2);
						}
						else
						{
							statePtrWithControlIndex->delta = ptr4->accumulatedDelta;
						}
						global::UnityEngine.InputSystem.EnhancedTouch.Touch value = new global::UnityEngine.InputSystem.EnhancedTouch.Touch(touchRecord: new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState>.Record(activeTouchState, index2, ptr3), finger: reference);
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.InsertAtWithCapacity(ref activeTouches, ref activeTouchCount, index, value);
						num = statePtrWithoutControlIndex->touchId;
						ptr = statePtrWithControlIndex;
						if (value.phase != global::UnityEngine.InputSystem.TouchPhase.Stationary)
						{
							haveActiveTouchesNeedingRefreshNextUpdate = true;
						}
					}
				}
				haveBuiltActiveTouches = true;
			}
		}

		internal struct ExtraDataPerTouchState
		{
			public global::UnityEngine.Vector2 accumulatedDelta;

			public uint uniqueId;
		}

		private readonly global::UnityEngine.InputSystem.EnhancedTouch.Finger m_Finger;

		internal global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState>.Record m_TouchRecord;

		internal static global::UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalState s_GlobalState = CreateGlobalState();

		public bool valid => m_TouchRecord.valid;

		public global::UnityEngine.InputSystem.EnhancedTouch.Finger finger => m_Finger;

		public global::UnityEngine.InputSystem.TouchPhase phase => state.phase;

		public bool began => phase == global::UnityEngine.InputSystem.TouchPhase.Began;

		public bool inProgress
		{
			get
			{
				if (phase != global::UnityEngine.InputSystem.TouchPhase.Moved && phase != global::UnityEngine.InputSystem.TouchPhase.Stationary)
				{
					return phase == global::UnityEngine.InputSystem.TouchPhase.Began;
				}
				return true;
			}
		}

		public bool ended
		{
			get
			{
				if (phase != global::UnityEngine.InputSystem.TouchPhase.Ended)
				{
					return phase == global::UnityEngine.InputSystem.TouchPhase.Canceled;
				}
				return true;
			}
		}

		public int touchId => state.touchId;

		public float pressure => state.pressure;

		public global::UnityEngine.Vector2 radius => state.radius;

		public double startTime => state.startTime;

		public double time => m_TouchRecord.time;

		public global::UnityEngine.InputSystem.Touchscreen screen => finger.screen;

		public global::UnityEngine.Vector2 screenPosition => state.position;

		public global::UnityEngine.Vector2 startScreenPosition => state.startPosition;

		public global::UnityEngine.Vector2 delta => state.delta;

		public int tapCount => state.tapCount;

		public bool isTap => state.isTap;

		public int displayIndex => state.displayIndex;

		public bool isInProgress
		{
			get
			{
				global::UnityEngine.InputSystem.TouchPhase touchPhase = phase;
				if ((uint)(touchPhase - 1) <= 1u || touchPhase == global::UnityEngine.InputSystem.TouchPhase.Stationary)
				{
					return true;
				}
				return false;
			}
		}

		internal uint updateStepCount => state.updateStepCount;

		internal uint uniqueId => extraData.uniqueId;

		private unsafe ref global::UnityEngine.InputSystem.LowLevel.TouchState state => ref *(global::UnityEngine.InputSystem.LowLevel.TouchState*)m_TouchRecord.GetUnsafeMemoryPtr();

		private unsafe ref global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState extraData => ref *(global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState*)m_TouchRecord.GetUnsafeExtraMemoryPtr();

		public global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory history
		{
			get
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Touch is invalid");
				}
				return finger.GetTouchHistory(this);
			}
		}

		public static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.EnhancedTouch.Touch> activeTouches
		{
			get
			{
				s_GlobalState.playerState.UpdateActiveTouches();
				return new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.EnhancedTouch.Touch>(s_GlobalState.playerState.activeTouches, 0, s_GlobalState.playerState.activeTouchCount);
			}
		}

		public static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.EnhancedTouch.Finger> fingers => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.EnhancedTouch.Finger>(s_GlobalState.playerState.fingers, 0, s_GlobalState.playerState.totalFingerCount);

		public static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.EnhancedTouch.Finger> activeFingers
		{
			get
			{
				s_GlobalState.playerState.UpdateActiveFingers();
				return new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.EnhancedTouch.Finger>(s_GlobalState.playerState.activeFingers, 0, s_GlobalState.playerState.activeFingerCount);
			}
		}

		public static global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.Touchscreen> screens => s_GlobalState.touchscreens;

		public static int maxHistoryLengthPerFinger => s_GlobalState.historyLengthPerFinger;

		public static event global::System.Action<global::UnityEngine.InputSystem.EnhancedTouch.Finger> onFingerDown
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onFingerDown.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onFingerDown.RemoveCallback(value);
			}
		}

		public static event global::System.Action<global::UnityEngine.InputSystem.EnhancedTouch.Finger> onFingerUp
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onFingerUp.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onFingerUp.RemoveCallback(value);
			}
		}

		public static event global::System.Action<global::UnityEngine.InputSystem.EnhancedTouch.Finger> onFingerMove
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onFingerMove.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				s_GlobalState.onFingerMove.RemoveCallback(value);
			}
		}

		internal Touch(global::UnityEngine.InputSystem.EnhancedTouch.Finger finger, global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState>.Record touchRecord)
		{
			m_Finger = finger;
			m_TouchRecord = touchRecord;
		}

		public override string ToString()
		{
			if (!valid)
			{
				return "<None>";
			}
			return $"{{id={touchId} finger={finger.index} phase={phase} position={screenPosition} delta={delta} time={time}}}";
		}

		public bool Equals(global::UnityEngine.InputSystem.EnhancedTouch.Touch other)
		{
			if (object.Equals(m_Finger, other.m_Finger))
			{
				return m_TouchRecord.Equals(other.m_TouchRecord);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityEngine.InputSystem.EnhancedTouch.Touch other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (((m_Finger != null) ? m_Finger.GetHashCode() : 0) * 397) ^ m_TouchRecord.GetHashCode();
		}

		internal static void AddTouchscreen(global::UnityEngine.InputSystem.Touchscreen screen)
		{
			s_GlobalState.touchscreens.AppendWithCapacity(screen, 5);
			s_GlobalState.playerState.AddFingers(screen);
		}

		internal static void RemoveTouchscreen(global::UnityEngine.InputSystem.Touchscreen screen)
		{
			int index = global::UnityEngine.InputSystem.Utilities.InputArrayExtensions.IndexOfReference(s_GlobalState.touchscreens, screen);
			s_GlobalState.touchscreens.RemoveAtWithCapacity(index);
			s_GlobalState.playerState.RemoveFingers(screen);
		}

		internal static void BeginUpdate()
		{
			if (s_GlobalState.playerState.haveActiveTouchesNeedingRefreshNextUpdate)
			{
				s_GlobalState.playerState.haveBuiltActiveTouches = false;
			}
		}

		private static global::UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalState CreateGlobalState()
		{
			return new global::UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalState
			{
				historyLengthPerFinger = 64
			};
		}

		internal static global::UnityEngine.InputSystem.Utilities.ISavedState SaveAndResetState()
		{
			global::UnityEngine.InputSystem.Utilities.SavedStructState<global::UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalState> result = new global::UnityEngine.InputSystem.Utilities.SavedStructState<global::UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalState>(ref s_GlobalState, delegate(ref global::UnityEngine.InputSystem.EnhancedTouch.Touch.GlobalState state)
			{
				s_GlobalState = state;
			}, delegate
			{
			});
			s_GlobalState = CreateGlobalState();
			return result;
		}
	}
}
