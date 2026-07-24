namespace UnityEngine.Rendering
{
	public struct ProbeVolumeSystemParameters
	{
		public global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget;

		public global::UnityEngine.Rendering.ProbeVolumeBlendingTextureMemoryBudget blendingMemoryBudget;

		public global::UnityEngine.Rendering.ProbeVolumeSHBands shBands;

		public bool supportScenarios;

		public bool supportScenarioBlending;

		public bool supportGPUStreaming;

		public bool supportDiskStreaming;

		[global::System.Obsolete("This field is not used anymore. #from(2023.3)")]
		public global::UnityEngine.Shader probeDebugShader;

		[global::System.Obsolete("This field is not used anymore. #from(2023.3)")]
		public global::UnityEngine.Shader probeSamplingDebugShader;

		[global::System.Obsolete("This field is not used anymore. #from(2023.3)")]
		public global::UnityEngine.Texture probeSamplingDebugTexture;

		[global::System.Obsolete("This field is not used anymore. #from(2023.3)")]
		public global::UnityEngine.Mesh probeSamplingDebugMesh;

		[global::System.Obsolete("This field is not used anymore. #from(2023.3)")]
		public global::UnityEngine.Shader offsetDebugShader;

		[global::System.Obsolete("This field is not used anymore. #from(2023.3)")]
		public global::UnityEngine.Shader fragmentationDebugShader;

		[global::System.Obsolete("This field is not used anymore. #from(2023.3)")]
		public global::UnityEngine.ComputeShader scenarioBlendingShader;

		[global::System.Obsolete("This field is not used anymore. #from(2023.3)")]
		public global::UnityEngine.ComputeShader streamingUploadShader;

		[global::System.Obsolete("This field is not used anymore. #from(2023.3)")]
		public global::UnityEngine.Rendering.ProbeVolumeSceneData sceneData;

		[global::System.Obsolete("This field is not used anymore. Used with the current Shader Stripping Settings. #from(2023.3)")]
		public bool supportsRuntimeDebug;
	}
}
