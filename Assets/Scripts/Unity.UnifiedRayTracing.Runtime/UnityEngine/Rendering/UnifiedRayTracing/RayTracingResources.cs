namespace UnityEngine.Rendering.UnifiedRayTracing
{
	public class RayTracingResources
	{
		internal global::UnityEngine.ComputeShader geometryPoolKernels { get; set; }

		internal global::UnityEngine.ComputeShader copyBuffer { get; set; }

		internal global::UnityEngine.ComputeShader copyPositions { get; set; }

		internal global::UnityEngine.ComputeShader bitHistogram { get; set; }

		internal global::UnityEngine.ComputeShader blockReducePart { get; set; }

		internal global::UnityEngine.ComputeShader blockScan { get; set; }

		internal global::UnityEngine.ComputeShader buildHlbvh { get; set; }

		internal global::UnityEngine.ComputeShader restructureBvh { get; set; }

		internal global::UnityEngine.ComputeShader scatter { get; set; }

		public void LoadFromAssetBundle(global::UnityEngine.AssetBundle assetBundle)
		{
			geometryPoolKernels = assetBundle.LoadAsset<global::UnityEngine.ComputeShader>("Packages/com.unity.render-pipelines.core/Runtime/UnifiedRayTracing/Common/GeometryPool/GeometryPoolKernels.compute");
			copyBuffer = assetBundle.LoadAsset<global::UnityEngine.ComputeShader>("Packages/com.unity.render-pipelines.core/Runtime/UnifiedRayTracing/Common/Utilities/CopyBuffer.compute");
			copyPositions = assetBundle.LoadAsset<global::UnityEngine.ComputeShader>("Packages/com.unity.render-pipelines.core/Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/copyPositions.compute");
			bitHistogram = assetBundle.LoadAsset<global::UnityEngine.ComputeShader>("Packages/com.unity.render-pipelines.core/Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/bit_histogram.compute");
			blockReducePart = assetBundle.LoadAsset<global::UnityEngine.ComputeShader>("Packages/com.unity.render-pipelines.core/Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/block_reduce_part.compute");
			blockScan = assetBundle.LoadAsset<global::UnityEngine.ComputeShader>("Packages/com.unity.render-pipelines.core/Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/block_scan.compute");
			buildHlbvh = assetBundle.LoadAsset<global::UnityEngine.ComputeShader>("Packages/com.unity.render-pipelines.core/Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/build_hlbvh.compute");
			restructureBvh = assetBundle.LoadAsset<global::UnityEngine.ComputeShader>("Packages/com.unity.render-pipelines.core/Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/restructure_bvh.compute");
			scatter = assetBundle.LoadAsset<global::UnityEngine.ComputeShader>("Packages/com.unity.render-pipelines.core/Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/scatter.compute");
		}

		public bool LoadFromRenderPipelineResources()
		{
			if (!global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingRenderPipelineResources>(out var settings))
			{
				return false;
			}
			geometryPoolKernels = settings.GeometryPoolKernels;
			copyBuffer = settings.CopyBuffer;
			copyPositions = settings.CopyPositions;
			bitHistogram = settings.BitHistogram;
			blockReducePart = settings.BlockReducePart;
			blockScan = settings.BlockScan;
			buildHlbvh = settings.BuildHlbvh;
			restructureBvh = settings.RestructureBvh;
			scatter = settings.Scatter;
			return true;
		}
	}
}
