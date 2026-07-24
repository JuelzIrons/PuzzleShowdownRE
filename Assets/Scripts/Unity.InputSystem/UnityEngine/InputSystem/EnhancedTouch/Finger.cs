namespace UnityEngine.InputSystem.EnhancedTouch
{
	public class Finger
	{
		internal readonly global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState> m_StateHistory;

		public global::UnityEngine.InputSystem.Touchscreen screen { get; }

		public int index { get; }

		public bool isActive => currentTouch.valid;

		public global::UnityEngine.Vector2 screenPosition
		{
			get
			{
				global::UnityEngine.InputSystem.EnhancedTouch.Touch touch = lastTouch;
				if (!touch.valid)
				{
					return default(global::UnityEngine.Vector2);
				}
				return touch.screenPosition;
			}
		}

		public global::UnityEngine.InputSystem.EnhancedTouch.Touch lastTouch
		{
			get
			{
				int count = m_StateHistory.Count;
				if (count == 0)
				{
					return default(global::UnityEngine.InputSystem.EnhancedTouch.Touch);
				}
				return new global::UnityEngine.InputSystem.EnhancedTouch.Touch(this, m_StateHistory[count - 1]);
			}
		}

		public global::UnityEngine.InputSystem.EnhancedTouch.Touch currentTouch
		{
			get
			{
				global::UnityEngine.InputSystem.EnhancedTouch.Touch result = lastTouch;
				if (!result.valid)
				{
					return default(global::UnityEngine.InputSystem.EnhancedTouch.Touch);
				}
				if (result.isInProgress)
				{
					return result;
				}
				if (result.updateStepCount == global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount)
				{
					return result;
				}
				return default(global::UnityEngine.InputSystem.EnhancedTouch.Touch);
			}
		}

		public global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory touchHistory => new global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory(this, m_StateHistory);

		internal Finger(global::UnityEngine.InputSystem.Touchscreen screen, int index, global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateMask)
		{
			this.screen = screen;
			this.index = index;
			m_StateHistory = new global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState>(screen.touches[index])
			{
				historyDepth = global::UnityEngine.InputSystem.EnhancedTouch.Touch.maxHistoryLengthPerFinger,
				extraMemoryPerRecord = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState>(),
				onRecordAdded = OnTouchRecorded,
				onShouldRecordStateChange = ShouldRecordTouch,
				updateMask = updateMask
			};
			m_StateHistory.StartRecording();
			if (screen.touches[index].isInProgress)
			{
				m_StateHistory.RecordStateChange(screen.touches[index], screen.touches[index].value);
			}
		}

		private unsafe static bool ShouldRecordTouch(global::UnityEngine.InputSystem.InputControl control, double time, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				return false;
			}
			global::UnityEngine.InputSystem.Utilities.FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				return false;
			}
			global::UnityEngine.InputSystem.LowLevel.TouchState* ptr = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)control.currentStatePtr + control.stateBlock.byteOffset);
			if (ptr->isTapRelease)
			{
				return false;
			}
			return true;
		}

		private unsafe void OnTouchRecorded(global::UnityEngine.InputSystem.LowLevel.InputStateHistory.Record record)
		{
			int recordIndex = record.recordIndex;
			global::UnityEngine.InputSystem.LowLevel.InputStateHistory.RecordHeader* recordUnchecked = m_StateHistory.GetRecordUnchecked(recordIndex);
			global::UnityEngine.InputSystem.LowLevel.TouchState* statePtrWithoutControlIndex = (global::UnityEngine.InputSystem.LowLevel.TouchState*)recordUnchecked->statePtrWithoutControlIndex;
			statePtrWithoutControlIndex->updateStepCount = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
			global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.playerState.haveBuiltActiveTouches = false;
			global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState* ptr = (global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState*)((byte*)recordUnchecked + m_StateHistory.bytesPerRecord - global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState>());
			ptr->uniqueId = ++global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.playerState.lastId;
			ptr->accumulatedDelta = statePtrWithoutControlIndex->delta;
			if (statePtrWithoutControlIndex->phase != global::UnityEngine.InputSystem.TouchPhase.Began)
			{
				if (recordIndex != m_StateHistory.m_HeadIndex)
				{
					int num = ((recordIndex == 0) ? (m_StateHistory.historyDepth - 1) : (recordIndex - 1));
					global::UnityEngine.InputSystem.LowLevel.TouchState* statePtrWithoutControlIndex2 = (global::UnityEngine.InputSystem.LowLevel.TouchState*)m_StateHistory.GetRecordUnchecked(num)->statePtrWithoutControlIndex;
					statePtrWithoutControlIndex->delta -= statePtrWithoutControlIndex2->delta;
					statePtrWithoutControlIndex->beganInSameFrame = statePtrWithoutControlIndex2->beganInSameFrame && statePtrWithoutControlIndex2->updateStepCount == statePtrWithoutControlIndex->updateStepCount;
				}
			}
			else
			{
				statePtrWithoutControlIndex->beganInSameFrame = true;
			}
			switch (statePtrWithoutControlIndex->phase)
			{
			case global::UnityEngine.InputSystem.TouchPhase.Began:
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.onFingerDown, this, "Touch.onFingerDown");
				break;
			case global::UnityEngine.InputSystem.TouchPhase.Moved:
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.onFingerMove, this, "Touch.onFingerMove");
				break;
			case global::UnityEngine.InputSystem.TouchPhase.Ended:
			case global::UnityEngine.InputSystem.TouchPhase.Canceled:
				global::UnityEngine.InputSystem.Utilities.DelegateHelpers.InvokeCallbacksSafe(ref global::UnityEngine.InputSystem.EnhancedTouch.Touch.s_GlobalState.onFingerUp, this, "Touch.onFingerUp");
				break;
			}
		}

		private unsafe global::UnityEngine.InputSystem.EnhancedTouch.Touch FindTouch(uint uniqueId)
		{
			foreach (global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState>.Record item in m_StateHistory)
			{
				if (((global::UnityEngine.InputSystem.EnhancedTouch.Touch.ExtraDataPerTouchState*)item.GetUnsafeExtraMemoryPtrUnchecked())->uniqueId == uniqueId)
				{
					return new global::UnityEngine.InputSystem.EnhancedTouch.Touch(this, item);
				}
			}
			return default(global::UnityEngine.InputSystem.EnhancedTouch.Touch);
		}

		internal unsafe global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory GetTouchHistory(global::UnityEngine.InputSystem.EnhancedTouch.Touch touch)
		{
			global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState>.Record touchRecord = touch.m_TouchRecord;
			if (touchRecord.owner != m_StateHistory)
			{
				touch = FindTouch(touch.uniqueId);
				if (!touch.valid)
				{
					return default(global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory);
				}
			}
			int touchId = touch.touchId;
			int num = touch.m_TouchRecord.index;
			int num2 = 0;
			if (touch.phase != global::UnityEngine.InputSystem.TouchPhase.Began)
			{
				global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState>.Record previous = touch.m_TouchRecord.previous;
				while (previous.valid)
				{
					global::UnityEngine.InputSystem.LowLevel.TouchState* unsafeMemoryPtr = (global::UnityEngine.InputSystem.LowLevel.TouchState*)previous.GetUnsafeMemoryPtr();
					if (unsafeMemoryPtr->touchId != touchId)
					{
						break;
					}
					num2++;
					if (unsafeMemoryPtr->phase == global::UnityEngine.InputSystem.TouchPhase.Began)
					{
						break;
					}
					previous = previous.previous;
				}
			}
			if (num2 == 0)
			{
				return default(global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory);
			}
			num--;
			return new global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory(this, m_StateHistory, num, num2);
		}
	}
}
