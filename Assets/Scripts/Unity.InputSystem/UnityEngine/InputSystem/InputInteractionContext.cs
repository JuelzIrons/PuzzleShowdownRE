namespace UnityEngine.InputSystem
{
	public struct InputInteractionContext
	{
		[global::System.Flags]
		internal enum Flags
		{
			TimerHasExpired = 2
		}

		internal global::UnityEngine.InputSystem.InputActionState m_State;

		internal global::UnityEngine.InputSystem.InputInteractionContext.Flags m_Flags;

		internal global::UnityEngine.InputSystem.InputActionState.TriggerState m_TriggerState;

		public global::UnityEngine.InputSystem.InputAction action => m_State.GetActionOrNull(ref m_TriggerState);

		public global::UnityEngine.InputSystem.InputControl control => m_State.GetControl(ref m_TriggerState);

		public global::UnityEngine.InputSystem.InputActionPhase phase => m_TriggerState.phase;

		public double time => m_TriggerState.time;

		public double startTime => m_TriggerState.startTime;

		public bool timerHasExpired
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.InputInteractionContext.Flags.TimerHasExpired) != 0;
			}
			internal set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputInteractionContext.Flags.TimerHasExpired;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputInteractionContext.Flags.TimerHasExpired;
				}
			}
		}

		public bool isWaiting => phase == global::UnityEngine.InputSystem.InputActionPhase.Waiting;

		public bool isStarted => phase == global::UnityEngine.InputSystem.InputActionPhase.Started;

		internal int mapIndex => m_TriggerState.mapIndex;

		internal int controlIndex => m_TriggerState.controlIndex;

		internal int bindingIndex => m_TriggerState.bindingIndex;

		internal int interactionIndex => m_TriggerState.interactionIndex;

		public float ComputeMagnitude()
		{
			return m_TriggerState.magnitude;
		}

		public bool ControlIsActuated(float threshold = 0f)
		{
			return global::UnityEngine.InputSystem.InputActionState.IsActuated(ref m_TriggerState, threshold);
		}

		public void Started()
		{
			m_TriggerState.startTime = time;
			m_State.ChangePhaseOfInteraction(global::UnityEngine.InputSystem.InputActionPhase.Started, ref m_TriggerState);
		}

		public void Performed()
		{
			if (m_TriggerState.phase == global::UnityEngine.InputSystem.InputActionPhase.Waiting)
			{
				m_TriggerState.startTime = time;
			}
			m_State.ChangePhaseOfInteraction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref m_TriggerState);
		}

		public void PerformedAndStayStarted()
		{
			if (m_TriggerState.phase == global::UnityEngine.InputSystem.InputActionPhase.Waiting)
			{
				m_TriggerState.startTime = time;
			}
			m_State.ChangePhaseOfInteraction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref m_TriggerState, global::UnityEngine.InputSystem.InputActionPhase.Started);
		}

		public void PerformedAndStayPerformed()
		{
			if (m_TriggerState.phase == global::UnityEngine.InputSystem.InputActionPhase.Waiting)
			{
				m_TriggerState.startTime = time;
			}
			m_State.ChangePhaseOfInteraction(global::UnityEngine.InputSystem.InputActionPhase.Performed, ref m_TriggerState, global::UnityEngine.InputSystem.InputActionPhase.Performed);
		}

		public void Canceled()
		{
			if (m_TriggerState.phase != global::UnityEngine.InputSystem.InputActionPhase.Canceled)
			{
				m_State.ChangePhaseOfInteraction(global::UnityEngine.InputSystem.InputActionPhase.Canceled, ref m_TriggerState);
			}
		}

		public void Waiting()
		{
			if (m_TriggerState.phase != global::UnityEngine.InputSystem.InputActionPhase.Waiting)
			{
				m_State.ChangePhaseOfInteraction(global::UnityEngine.InputSystem.InputActionPhase.Waiting, ref m_TriggerState);
			}
		}

		public void SetTimeout(float seconds)
		{
			m_State.StartTimeout(seconds, ref m_TriggerState);
		}

		public void SetTotalTimeoutCompletionTime(float seconds)
		{
			if (seconds <= 0f)
			{
				throw new global::System.ArgumentException("Seconds must be a positive value", "seconds");
			}
			m_State.SetTotalTimeoutCompletionTime(seconds, ref m_TriggerState);
		}

		public TValue ReadValue<TValue>() where TValue : struct
		{
			return m_State.ReadValue<TValue>(m_TriggerState.bindingIndex, m_TriggerState.controlIndex);
		}
	}
}
