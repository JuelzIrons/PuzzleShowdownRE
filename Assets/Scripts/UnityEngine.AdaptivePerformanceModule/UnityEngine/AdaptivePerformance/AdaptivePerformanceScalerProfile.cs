namespace UnityEngine.AdaptivePerformance
{
	[global::System.Serializable]
	public class AdaptivePerformanceScalerProfile : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettings
	{
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> m_AddedScalers = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Name of the scaler profile.")]
		private string m_Name = "Default Scaler Profile";

		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler> AddedScalers
		{
			get
			{
				return m_AddedScalers;
			}
			set
			{
				m_AddedScalers = value;
			}
		}

		internal void EnableAddedScalers()
		{
			for (int i = 0; i < m_AddedScalers.Count; i++)
			{
				if ((bool)m_AddedScalers[i])
				{
					m_AddedScalers[i].InitializeScaler();
				}
				else
				{
					global::UnityEngine.AdaptivePerformance.APLog.Debug("Null scaler is added to the scaler list");
				}
			}
		}

		internal void RemoveAllAddedScalersFromIndexer()
		{
			foreach (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler addedScaler in m_AddedScalers)
			{
				if ((bool)addedScaler)
				{
					addedScaler.RemoveScaler();
				}
			}
		}
	}
}
