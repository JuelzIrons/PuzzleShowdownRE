namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime.Analytics
{
	[global::UnityEngine.Analytics.AnalyticInfo("mpToolsNetSimConnectionStateChanged", "unity.multiplayer.tools", 1, 1000, 1000)]
	internal class ConnectionStateChangedAnalytic : global::UnityEngine.Analytics.IAnalytic
	{
		private readonly bool m_UsedEditorGUI;

		private readonly bool m_IsPartOfLagSpike;

		public ConnectionStateChangedAnalytic(bool usedEditorGUI, bool isPartOfLagSpike)
		{
			m_UsedEditorGUI = usedEditorGUI;
			m_IsPartOfLagSpike = isPartOfLagSpike;
		}

		public bool TryGatherData(out global::UnityEngine.Analytics.IAnalytic.IData data, out global::System.Exception error)
		{
			error = null;
			data = new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.Analytics.ConnectionStateChangedData
			{
				usedEditorGUI = m_UsedEditorGUI,
				isPartOfLagSpike = m_IsPartOfLagSpike
			};
			return true;
		}
	}
}
