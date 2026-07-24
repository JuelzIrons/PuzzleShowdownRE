namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("RayTracingAccelerationStructure ({handle.index})")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public readonly struct RayTracingAccelerationStructureHandle
	{
		private static global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle s_NullHandle;

		internal readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle;

		public static global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle nullHandle => s_NullHandle;

		internal RayTracingAccelerationStructureHandle(int handle)
		{
			this.handle = new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(handle, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.AccelerationStructure, shared: false);
		}

		public static implicit operator global::UnityEngine.Rendering.RayTracingAccelerationStructure(global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle handle)
		{
			if (!handle.IsValid())
			{
				return null;
			}
			return global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.current.GetRayTracingAccelerationStructure(in handle);
		}

		public bool IsValid()
		{
			return handle.IsValid();
		}
	}
}
