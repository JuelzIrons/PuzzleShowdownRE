namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime.Analytics
{
	[global::UnityEngine.Analytics.AnalyticInfo("mpToolsNetSimScenarioInitialized", "unity.multiplayer.tools", 1, 100, 1000)]
	internal class ScenarioInitializedAnalytic : global::UnityEngine.Analytics.IAnalytic
	{
		private readonly bool m_AutoRun;

		private readonly string m_ScenarioClassType;

		public ScenarioInitializedAnalytic(bool autoRun, global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.Analytics.ScenarioClassType scenarioClassType)
		{
			m_AutoRun = autoRun;
			m_ScenarioClassType = scenarioClassType.ToString();
		}

		public bool TryGatherData(out global::UnityEngine.Analytics.IAnalytic.IData data, out global::System.Exception error)
		{
			error = null;
			data = new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.Analytics.ScenarioInitializedData
			{
				autoRun = m_AutoRun,
				scenarioClassType = m_ScenarioClassType
			};
			return true;
		}
	}
}
