namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime.Analytics
{
	[global::System.Serializable]
	internal class ConnectionStateChangedData : global::UnityEngine.Analytics.IAnalytic.IData
	{
		public bool usedEditorGUI;

		public bool isPartOfLagSpike;
	}
}
