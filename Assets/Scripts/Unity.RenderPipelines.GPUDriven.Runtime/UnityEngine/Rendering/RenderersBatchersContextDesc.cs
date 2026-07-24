namespace UnityEngine.Rendering
{
	internal struct RenderersBatchersContextDesc
	{
		public global::UnityEngine.Rendering.InstanceNumInfo instanceNumInfo;

		public bool supportDitheringCrossFade;

		public bool enableBoundingSpheresInstanceData;

		public float smallMeshScreenPercentage;

		public bool enableCullerDebugStats;

		public static global::UnityEngine.Rendering.RenderersBatchersContextDesc NewDefault()
		{
			return new global::UnityEngine.Rendering.RenderersBatchersContextDesc
			{
				instanceNumInfo = new global::UnityEngine.Rendering.InstanceNumInfo(1024, 32)
			};
		}
	}
}
