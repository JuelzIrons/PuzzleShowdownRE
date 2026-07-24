namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("RayTracingAccelerationStructureResource ({desc.name})")]
	internal class RayTracingAccelerationStructureResource : global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureDesc, global::UnityEngine.Rendering.RayTracingAccelerationStructure>
	{
		public override string GetName()
		{
			return desc.name;
		}
	}
}
