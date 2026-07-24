namespace UnityEngine.InputSystem.Interactions
{
	[global::System.ComponentModel.DisplayName("Press")]
	public class PressInteraction : global::UnityEngine.InputSystem.IInputInteraction
	{
		[global::UnityEngine.Tooltip("The amount of actuation a control requires before being considered pressed. If not set, default to 'Default Press Point' in the global input settings.")]
		public float pressPoint;

		[global::UnityEngine.Tooltip("Determines how button presses trigger the action. By default (PressOnly), the action is performed on press. With ReleaseOnly, the action is performed on release. With PressAndRelease, the action is performed on press and release.")]
		public global::UnityEngine.InputSystem.Interactions.PressBehavior behavior;

		private bool m_WaitingForRelease;

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

		private float releasePointOrDefault => pressPointOrDefault * global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonReleaseThreshold;

		public void Process(ref global::UnityEngine.InputSystem.InputInteractionContext context)
		{
			float num = context.ComputeMagnitude();
			switch (behavior)
			{
			case global::UnityEngine.InputSystem.Interactions.PressBehavior.PressOnly:
				if (m_WaitingForRelease)
				{
					if (num <= releasePointOrDefault)
					{
						m_WaitingForRelease = false;
						if (global::UnityEngine.Mathf.Approximately(0f, num))
						{
							context.Canceled();
						}
						else
						{
							context.Started();
						}
					}
				}
				else if (num >= pressPointOrDefault)
				{
					m_WaitingForRelease = true;
					context.PerformedAndStayPerformed();
				}
				else if (num > 0f && !context.isStarted)
				{
					context.Started();
				}
				else if (global::UnityEngine.Mathf.Approximately(0f, num) && context.isStarted)
				{
					context.Canceled();
				}
				break;
			case global::UnityEngine.InputSystem.Interactions.PressBehavior.ReleaseOnly:
			{
				if (m_WaitingForRelease)
				{
					if (num <= releasePointOrDefault)
					{
						m_WaitingForRelease = false;
						context.Performed();
						context.Canceled();
					}
					break;
				}
				if (num >= pressPointOrDefault)
				{
					m_WaitingForRelease = true;
					if (!context.isStarted)
					{
						context.Started();
					}
					break;
				}
				bool isStarted2 = context.isStarted;
				if (num > 0f && !isStarted2)
				{
					context.Started();
				}
				else if (global::UnityEngine.Mathf.Approximately(0f, num) && isStarted2)
				{
					context.Canceled();
				}
				break;
			}
			case global::UnityEngine.InputSystem.Interactions.PressBehavior.PressAndRelease:
				if (m_WaitingForRelease)
				{
					if (num <= releasePointOrDefault)
					{
						m_WaitingForRelease = false;
						context.Performed();
						if (global::UnityEngine.Mathf.Approximately(0f, num))
						{
							context.Canceled();
						}
					}
				}
				else if (num >= pressPointOrDefault)
				{
					m_WaitingForRelease = true;
					context.PerformedAndStayPerformed();
				}
				else
				{
					bool isStarted = context.isStarted;
					if (num > 0f && !isStarted)
					{
						context.Started();
					}
					else if (global::UnityEngine.Mathf.Approximately(0f, num) && isStarted)
					{
						context.Canceled();
					}
				}
				break;
			}
		}

		public void Reset()
		{
			m_WaitingForRelease = false;
		}
	}
}
