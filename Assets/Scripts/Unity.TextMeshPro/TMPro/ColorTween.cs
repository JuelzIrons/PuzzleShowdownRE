namespace TMPro
{
	internal struct ColorTween : global::TMPro.ITweenValue
	{
		public enum ColorTweenMode
		{
			All = 0,
			RGB = 1,
			Alpha = 2
		}

		public class ColorTweenCallback : global::UnityEngine.Events.UnityEvent<global::UnityEngine.Color>
		{
		}

		private global::TMPro.ColorTween.ColorTweenCallback m_Target;

		private global::UnityEngine.Color m_StartColor;

		private global::UnityEngine.Color m_TargetColor;

		private global::TMPro.ColorTween.ColorTweenMode m_TweenMode;

		private float m_Duration;

		private bool m_IgnoreTimeScale;

		public global::UnityEngine.Color startColor
		{
			get
			{
				return m_StartColor;
			}
			set
			{
				m_StartColor = value;
			}
		}

		public global::UnityEngine.Color targetColor
		{
			get
			{
				return m_TargetColor;
			}
			set
			{
				m_TargetColor = value;
			}
		}

		public global::TMPro.ColorTween.ColorTweenMode tweenMode
		{
			get
			{
				return m_TweenMode;
			}
			set
			{
				m_TweenMode = value;
			}
		}

		public float duration
		{
			get
			{
				return m_Duration;
			}
			set
			{
				m_Duration = value;
			}
		}

		public bool ignoreTimeScale
		{
			get
			{
				return m_IgnoreTimeScale;
			}
			set
			{
				m_IgnoreTimeScale = value;
			}
		}

		public void TweenValue(float floatPercentage)
		{
			if (ValidTarget())
			{
				global::UnityEngine.Color arg = global::UnityEngine.Color.Lerp(m_StartColor, m_TargetColor, floatPercentage);
				if (m_TweenMode == global::TMPro.ColorTween.ColorTweenMode.Alpha)
				{
					arg.r = m_StartColor.r;
					arg.g = m_StartColor.g;
					arg.b = m_StartColor.b;
				}
				else if (m_TweenMode == global::TMPro.ColorTween.ColorTweenMode.RGB)
				{
					arg.a = m_StartColor.a;
				}
				m_Target.Invoke(arg);
			}
		}

		public void AddOnChangedCallback(global::UnityEngine.Events.UnityAction<global::UnityEngine.Color> callback)
		{
			if (m_Target == null)
			{
				m_Target = new global::TMPro.ColorTween.ColorTweenCallback();
			}
			m_Target.AddListener(callback);
		}

		public bool GetIgnoreTimescale()
		{
			return m_IgnoreTimeScale;
		}

		public float GetDuration()
		{
			return m_Duration;
		}

		public bool ValidTarget()
		{
			return m_Target != null;
		}
	}
}
