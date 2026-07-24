namespace UnityEngine.InputSystem.Interactions
{
	[global::System.ComponentModel.DisplayName("Long Tap")]
	public class SlowTapInteraction : global::UnityEngine.InputSystem.IInputInteraction
	{
		public float duration;

		public float pressPoint;

		private double m_SlowTapStartTime;

		private float durationOrDefault
		{
			get
			{
				if (!(duration > 0f))
				{
					return global::UnityEngine.InputSystem.InputSystem.settings.defaultSlowTapTime;
				}
				return duration;
			}
		}

		private float pressPointOrDefault
		{
			get
			{
				if (!(pressPoint > 0f))
				{
					return global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return pressPoint;
			}
		}

		public void Process(ref global::UnityEngine.InputSystem.InputInteractionContext context)
		{
			if (context.isWaiting && context.ControlIsActuated(pressPointOrDefault))
			{
				m_SlowTapStartTime = context.time;
				context.Started();
			}
			else if (context.isStarted && !context.ControlIsActuated(pressPointOrDefault))
			{
				if (context.time - m_SlowTapStartTime >= (double)durationOrDefault)
				{
					context.Performed();
				}
				else
				{
					context.Canceled();
				}
			}
		}

		public void Reset()
		{
			m_SlowTapStartTime = 0.0;
		}
	}
}
