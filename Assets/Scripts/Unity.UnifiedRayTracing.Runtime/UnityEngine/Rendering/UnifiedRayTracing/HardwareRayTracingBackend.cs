namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal class HardwareRayTracingBackend : global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingBackend
	{
		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingResources m_Resources;

		public HardwareRayTracingBackend(global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingResources resources)
		{
			m_Resources = resources;
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader CreateRayTracingShader(global::UnityEngine.Object shader, string kernelName, global::UnityEngine.GraphicsBuffer dispatchBuffer)
		{
			return new global::UnityEngine.Rendering.UnifiedRayTracing.HardwareRayTracingShader((global::UnityEngine.Rendering.RayTracingShader)shader, kernelName, dispatchBuffer);
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct CreateAccelerationStructure(global::UnityEngine.Rendering.UnifiedRayTracing.AccelerationStructureOptions options, global::UnityEngine.Rendering.UnifiedRayTracing.ReferenceCounter counter)
		{
			return new global::UnityEngine.Rendering.UnifiedRayTracing.HardwareRayTracingAccelStruct(options, counter);
		}

		public ulong GetRequiredTraceScratchBufferSizeInBytes(uint width, uint height, uint depth)
		{
			return 0uL;
		}
	}
}
