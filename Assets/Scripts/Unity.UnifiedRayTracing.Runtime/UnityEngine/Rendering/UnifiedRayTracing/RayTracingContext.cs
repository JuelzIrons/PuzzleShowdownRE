namespace UnityEngine.Rendering.UnifiedRayTracing
{
	public sealed class RayTracingContext : global::System.IDisposable
	{
		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingBackend m_Backend;

		private readonly global::UnityEngine.Rendering.UnifiedRayTracing.ReferenceCounter m_AccelStructCounter = new global::UnityEngine.Rendering.UnifiedRayTracing.ReferenceCounter();

		private readonly global::UnityEngine.GraphicsBuffer m_DispatchBuffer;

		public global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingResources Resources { get; private set; }

		public global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend BackendType { get; private set; }

		public RayTracingContext(global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend backend, global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingResources resources)
		{
			if (!IsBackendSupported(backend))
			{
				throw new global::System.InvalidOperationException("Unsupported backend: " + backend);
			}
			BackendType = backend;
			switch (backend)
			{
			case global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Hardware:
				m_Backend = new global::UnityEngine.Rendering.UnifiedRayTracing.HardwareRayTracingBackend(resources);
				break;
			case global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Compute:
				m_Backend = new global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingBackend(resources);
				break;
			}
			Resources = resources;
			m_DispatchBuffer = global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingHelper.CreateDispatchIndirectBuffer();
		}

		public RayTracingContext(global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingResources resources)
			: this((!IsBackendSupported(global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Hardware)) ? global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Compute : global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Hardware, resources)
		{
		}

		public void Dispose()
		{
			if (m_AccelStructCounter.value != 0L)
			{
				global::UnityEngine.Debug.LogError("Memory Leak. Please call .Dispose() on all the IAccelerationStructure resources that have been created with this context before calling RayTracingContext.Dispose()");
			}
			m_DispatchBuffer?.Release();
		}

		public static bool IsBackendSupported(global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend backend)
		{
			return backend switch
			{
				global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Hardware => global::UnityEngine.SystemInfo.supportsRayTracing, 
				global::UnityEngine.Rendering.UnifiedRayTracing.RayTracingBackend.Compute => global::UnityEngine.SystemInfo.supportsComputeShaders, 
				_ => false, 
			};
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader CreateRayTracingShader(global::UnityEngine.Object shader)
		{
			return m_Backend.CreateRayTracingShader(shader, "MainRayGenShader", m_DispatchBuffer);
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader LoadRayTracingShaderFromAssetBundle(global::UnityEngine.AssetBundle assetBundle, string name)
		{
			global::UnityEngine.Object shader = assetBundle.LoadAsset(name, global::UnityEngine.Rendering.UnifiedRayTracing.BackendHelpers.GetTypeOfShader(BackendType));
			return CreateRayTracingShader(shader);
		}

		public global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct CreateAccelerationStructure(global::UnityEngine.Rendering.UnifiedRayTracing.AccelerationStructureOptions options)
		{
			return m_Backend.CreateAccelerationStructure(options, m_AccelStructCounter);
		}

		public ulong GetRequiredTraceScratchBufferSizeInBytes(uint width, uint height, uint depth)
		{
			return m_Backend.GetRequiredTraceScratchBufferSizeInBytes(width, height, depth);
		}

		public static uint GetScratchBufferStrideInBytes()
		{
			return 4u;
		}
	}
}
