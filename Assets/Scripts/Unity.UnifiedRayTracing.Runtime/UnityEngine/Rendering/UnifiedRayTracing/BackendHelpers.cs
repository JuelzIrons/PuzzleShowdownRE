namespace UnityEngine.Rendering.UnifiedRayTracing
{
	public static class BackendHelpers
	{
		public static string GetFileNameOfShader(global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend backend, string fileName)
		{
			return fileName + "." + backend switch
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Hardware => "raytrace", 
				global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Compute => "compute", 
				_ => throw new global::System.ArgumentOutOfRangeException("backend", backend, null), 
			};
		}

		public static global::System.Type GetTypeOfShader(global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend backend)
		{
			return backend switch
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Hardware => typeof(global::UnityEngine.Rendering.RayTracingShader), 
				global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Compute => typeof(global::UnityEngine.ComputeShader), 
				_ => throw new global::System.ArgumentOutOfRangeException("backend", backend, null), 
			};
		}
	}
}
