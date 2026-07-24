namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime.Analytics
{
	[global::System.Serializable]
	internal class ConnectionPresetChangedData : global::UnityEngine.Analytics.IAnalytic.IData
	{
		public bool usedEditorGUI;

		public string presetName;

		public bool isPartOfScenario;
	}
}
