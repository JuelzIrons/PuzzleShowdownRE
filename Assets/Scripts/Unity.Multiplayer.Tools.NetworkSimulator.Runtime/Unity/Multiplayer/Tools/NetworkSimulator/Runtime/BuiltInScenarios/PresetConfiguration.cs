namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios
{
	[global::System.Serializable]
	public class PresetConfiguration
	{
		[global::UnityEngine.SerializeReference]
		internal global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset m_ClassPreset;

		[global::UnityEngine.SerializeField]
		internal global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresetAsset m_ScriptableObjectPreset;

		internal bool IsClassPreset
		{
			get
			{
				if (m_ClassPreset != null)
				{
					return !string.IsNullOrEmpty(m_ClassPreset.Name);
				}
				return false;
			}
		}

		public global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset ConnectionPreset
		{
			get
			{
				global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset scriptableObjectPreset = m_ScriptableObjectPreset;
				return scriptableObjectPreset ?? m_ClassPreset;
			}
			set
			{
				if (value is global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresetAsset scriptableObjectPreset)
				{
					m_ScriptableObjectPreset = scriptableObjectPreset;
				}
				else if (value is global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset classPreset)
				{
					m_ClassPreset = classPreset;
				}
			}
		}
	}
}
