namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal interface IRayTracingBackend
	{
		global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader CreateRayTracingShader(global::UnityEngine.Object shader, string kernelName, global::UnityEngine.GraphicsBuffer dispatchBuffer);

		global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct CreateAccelerationStructure(global::UnityEngine.Rendering.UnifiedRayTracing.AccelerationStructureOptions options, global::UnityEngine.Rendering.UnifiedRayTracing.ReferenceCounter counter);

		ulong GetRequiredTraceScratchBufferSizeInBytes(uint width, uint height, uint depth);
	}
}
