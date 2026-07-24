namespace UnityEngine.InputSystem.Interactions
{
	[global::System.ComponentModel.DisplayName("Hold")]
	public class HoldInteraction : global::UnityEngine.InputSystem.IInputInteraction
	{
		public float duration;

		public float pressPoint;

		private double m_TimePressed;

		private float durationOrDefault
		{
			get
			{
				if (!((double)duration > 0.0))
				{
					return global::UnityEngine.InputSystem.InputSystem.settings.defaultHoldTime;
				}
				return duration;
			}
		}

		private float pressPointOrDefault
		{
			get
			{
				if (!((double)pressPoint > 0.0))
				{
					return global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return pressPoint;
			}
		}

		public void Process(ref global::UnityEngine.InputSystem.InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.PerformedAndStayPerformed();
				return;
			}
			switch (context.phase)
			{
			case global::UnityEngine.InputSystem.InputActionPhase.Waiting:
				if (context.ControlIsActuated(pressPointOrDefault))
				{
					m_TimePressed = context.time;
					context.Started();
					context.SetTimeout(durationOrDefault);
				}
				break;
			case global::UnityEngine.InputSystem.InputActionPhase.Started:
				if (context.time - m_TimePressed >= (double)durationOrDefault)
				{
					context.PerformedAndStayPerformed();
				}
				if (!context.ControlIsActuated())
				{
					context.Canceled();
				}
				break;
			case global::UnityEngine.InputSystem.InputActionPhase.Performed:
				if (!context.ControlIsActuated(pressPointOrDefault))
				{
					context.Canceled();
				}
				break;
			}
		}

		public void Reset()
		{
			m_TimePressed = 0.0;
		}
	}
}
