namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveFramerate : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private int m_DefaultFPS;

		private int m_FirstTimeStart = 0;

		protected override void Awake()
		{
			base.Awake();
			m_FirstTimeStart = 0;
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveFramerate);
			}
		}

		protected override void OnDisabled()
		{
			if (m_FirstTimeStart < 2)
			{
				m_FirstTimeStart++;
			}
			else
			{
				global::UnityEngine.Application.targetFrameRate = m_DefaultFPS;
			}
		}

		protected override void OnEnabled()
		{
			if (m_FirstTimeStart >= 2)
			{
				m_DefaultFPS = global::UnityEngine.Application.targetFrameRate;
				global::UnityEngine.Application.targetFrameRate = (int)MaxBound;
			}
		}

		protected override void OnLevelIncrease()
		{
			base.OnLevelIncrease();
			int num = 1;
			if (global::UnityEngine.AdaptivePerformance.Holder.Instance.Indexer.PerformanceAction == global::UnityEngine.AdaptivePerformance.StateAction.FastDecrease)
			{
				num = 5;
			}
			int num2 = global::UnityEngine.Application.targetFrameRate - num;
			if ((float)num2 >= MinBound && (float)num2 <= MaxBound)
			{
				global::UnityEngine.Application.targetFrameRate = num2;
			}
		}

		protected override void OnLevelDecrease()
		{
			base.OnLevelDecrease();
			int num = global::UnityEngine.Application.targetFrameRate + 5;
			if ((float)num >= MinBound && (float)num <= MaxBound)
			{
				global::UnityEngine.Application.targetFrameRate = num;
			}
		}
	}
}
