namespace UnityEngine.Rendering
{
	[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\Lighting\\ProbeVolume\\ShaderVariablesProbeVolumes.cs", needAccessors = false, generateCBuffer = true, constantRegister = 6)]
	internal struct ShaderVariablesProbeVolumes
	{
		public global::UnityEngine.Vector4 _Offset_LayerCount;

		public global::UnityEngine.Vector4 _MinLoadedCellInEntries_IndirectionEntryDim;

		public global::UnityEngine.Vector4 _MaxLoadedCellInEntries_RcpIndirectionEntryDim;

		public global::UnityEngine.Vector4 _PoolDim_MinBrickSize;

		public global::UnityEngine.Vector4 _RcpPoolDim_XY;

		public global::UnityEngine.Vector4 _MinEntryPos_Noise;

		public global::Unity.Mathematics.uint4 _EntryCount_X_XY_LeakReduction;

		public global::UnityEngine.Vector4 _Biases_NormalizationClamp;

		public global::UnityEngine.Vector4 _FrameIndex_Weights;

		public global::Unity.Mathematics.uint4 _ProbeVolumeLayerMask;
	}
}
