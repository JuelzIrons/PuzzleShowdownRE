namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	internal struct VirtualOffsetSettings
	{
		public bool useVirtualOffset;

		[global::UnityEngine.Range(0f, 0.95f)]
		public float validityThreshold;

		[global::UnityEngine.Range(0f, 1f)]
		public float outOfGeoOffset;

		[global::UnityEngine.Range(0f, 2f)]
		public float searchMultiplier;

		[global::UnityEngine.Range(-0.05f, 0f)]
		public float rayOriginBias;

		public global::UnityEngine.LayerMask collisionMask;

		internal void SetDefaults()
		{
			useVirtualOffset = true;
			validityThreshold = 0.25f;
			outOfGeoOffset = 0.01f;
			searchMultiplier = 0.2f;
			UpgradeFromTo(global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.Initial, global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset);
		}

		internal void UpgradeFromTo(global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion from, global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion to)
		{
			if (from < global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset && to >= global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset)
			{
				rayOriginBias = -0.001f;
				collisionMask = -5;
			}
		}
	}
}
