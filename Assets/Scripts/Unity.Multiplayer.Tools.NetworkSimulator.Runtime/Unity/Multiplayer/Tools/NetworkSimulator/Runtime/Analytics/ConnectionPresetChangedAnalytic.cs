namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime.Analytics
{
	[global::UnityEngine.Analytics.AnalyticInfo("mpToolsNetSimConnectionPresetChanged", "unity.multiplayer.tools", 1, 1000, 1000)]
	internal class ConnectionPresetChangedAnalytic : global::UnityEngine.Analytics.IAnalytic
	{
		private readonly bool m_UsedEditorGUI;

		private readonly string m_PresetName;

		private readonly bool m_IsPartOfScenario;

		public ConnectionPresetChangedAnalytic(bool usedEditorGUI, string presetName, bool isPartOfScenario)
		{
			m_UsedEditorGUI = usedEditorGUI;
			m_PresetName = presetName;
			m_IsPartOfScenario = isPartOfScenario;
		}

		public bool TryGatherData(out global::UnityEngine.Analytics.IAnalytic.IData data, out global::System.Exception error)
		{
			error = null;
			data = new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.Analytics.ConnectionPresetChangedData
			{
				usedEditorGUI = m_UsedEditorGUI,
				presetName = m_PresetName,
				isPartOfScenario = m_IsPartOfScenario
			};
			return true;
		}
	}
}
