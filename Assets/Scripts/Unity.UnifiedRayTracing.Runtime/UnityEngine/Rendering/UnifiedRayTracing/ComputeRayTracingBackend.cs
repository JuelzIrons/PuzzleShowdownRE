namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal class ComputeRayTracingBackend : global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingBackend
	{
		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingResources m_Resources;

		public ComputeRayTracingBackend(global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingResources resources)
		{
			m_Resources = resources;
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader CreateRayTracingShader(global::UnityEngine.Object shader, string kernelName, global::UnityEngine.GraphicsBuffer dispatchBuffer)
		{
			return new global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingShader((global::UnityEngine.ComputeShader)shader, kernelName, dispatchBuffer);
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct CreateAccelerationStructure(global::UnityEngine.Rendering.UnifiedRayTracing.AccelerationStructureOptions options, global::UnityEngine.Rendering.UnifiedRayTracing.ReferenceCounter counter)
		{
			return new global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct(options, m_Resources, counter);
		}

		public ulong GetRequiredTraceScratchBufferSizeInBytes(uint width, uint height, uint depth)
		{
			return global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.GetTraceMemoryRequirements(width * height * depth) * global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingContext.GetScratchBufferStrideInBytes();
		}
	}
}
