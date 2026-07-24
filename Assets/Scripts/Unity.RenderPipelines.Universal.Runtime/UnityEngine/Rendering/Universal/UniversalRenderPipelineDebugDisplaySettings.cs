namespace UnityEngine.Rendering.Universal
{
	public class UniversalRenderPipelineDebugDisplaySettings : global::UnityEngine.Rendering.DebugDisplaySettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplaySettings>
	{
		private global::UnityEngine.Rendering.Universal.DebugDisplaySettingsCommon commonSettings { get; set; }

		public global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial materialSettings { get; private set; }

		public global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering renderingSettings { get; private set; }

		public global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting lightingSettings { get; private set; }

		public global::UnityEngine.Rendering.DebugDisplaySettingsVolume volumeSettings { get; private set; }

		internal global::UnityEngine.Rendering.DebugDisplaySettingsStats<global::UnityEngine.Rendering.Universal.URPProfileId> displayStats { get; private set; }

		internal global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer gpuResidentDrawerSettings { get; private set; }

		public override bool IsPostProcessingAllowed
		{
			get
			{
				global::UnityEngine.Rendering.Universal.DebugPostProcessingMode postProcessingDebugMode = renderingSettings.postProcessingDebugMode;
				switch (postProcessingDebugMode)
				{
				case global::UnityEngine.Rendering.Universal.DebugPostProcessingMode.Disabled:
					return false;
				case global::UnityEngine.Rendering.Universal.DebugPostProcessingMode.Auto:
				{
					bool flag = true;
					{
						foreach (global::UnityEngine.Rendering.IDebugDisplaySettingsData setting in m_Settings)
						{
							flag &= setting.IsPostProcessingAllowed;
						}
						return flag;
					}
				}
				case global::UnityEngine.Rendering.Universal.DebugPostProcessingMode.Enabled:
					return true;
				default:
					throw new global::System.ArgumentOutOfRangeException("debugPostProcessingMode", $"Invalid post-processing state {postProcessingDebugMode}");
				}
			}
		}

		public override void Reset()
		{
			base.Reset();
			displayStats = Add(new global::UnityEngine.Rendering.DebugDisplaySettingsStats<global::UnityEngine.Rendering.Universal.URPProfileId>(new global::UnityEngine.Rendering.Universal.UniversalRenderPipelineDebugDisplayStats()));
			materialSettings = Add(new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial());
			lightingSettings = Add(new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting());
			renderingSettings = Add(new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering());
			volumeSettings = Add(new global::UnityEngine.Rendering.DebugDisplaySettingsVolume());
			commonSettings = Add(new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsCommon());
			gpuResidentDrawerSettings = Add(new global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer());
			global::UnityEngine.Texture.streamingTextureDiscardUnusedMips = false;
		}

		internal void UpdateDisplayStats()
		{
			if (displayStats != null)
			{
				displayStats.debugDisplayStats.Update();
			}
		}

		internal void UpdateMaterials()
		{
			if (renderingSettings.mipInfoMode != global::UnityEngine.Rendering.Universal.DebugMipInfoMode.None)
			{
				global::UnityEngine.Texture.SetStreamingTextureMaterialDebugProperties((renderingSettings.canAggregateData && renderingSettings.showInfoForAllSlots) ? (-1) : renderingSettings.mipDebugMaterialTextureSlot);
			}
		}
	}
}
