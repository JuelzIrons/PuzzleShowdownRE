namespace TMPro
{
	internal struct FloatTween : global::TMPro.ITweenValue
	{
		public class FloatTweenCallback : global::UnityEngine.Events.UnityEvent<float>
		{
		}

		private global::TMPro.FloatTween.FloatTweenCallback m_Target;

		private float m_StartValue;

		private float m_TargetValue;

		private float m_Duration;

		private bool m_IgnoreTimeScale;

		public float startValue
		{
			get
			{
				return m_StartValue;
			}
			set
			{
				m_StartValue = value;
			}
		}

		public float targetValue
		{
			get
			{
				return m_TargetValue;
			}
			set
			{
				m_TargetValue = value;
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
				float arg = global::UnityEngine.Mathf.Lerp(m_StartValue, m_TargetValue, floatPercentage);
				m_Target.Invoke(arg);
			}
		}

		public void AddOnChangedCallback(global::UnityEngine.Events.UnityAction<float> callback)
		{
			if (m_Target == null)
			{
				m_Target = new global::TMPro.FloatTween.FloatTweenCallback();
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
