namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("BufferResource ({desc.name})")]
	internal class BufferResource : global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.BufferDesc, global::UnityEngine.GraphicsBuffer>
	{
		public override string GetName()
		{
			if (imported)
			{
				return "ImportedGraphicsBuffer";
			}
			return desc.name;
		}

		public override int GetDescHashCode()
		{
			return desc.GetHashCode();
		}

		public override void CreateGraphicsResource()
		{
			GetName();
			graphicsResource = new global::UnityEngine.GraphicsBuffer(desc.target, desc.usageFlags, desc.count, desc.stride);
		}

		public override void UpdateGraphicsResource()
		{
			if (graphicsResource != null)
			{
				graphicsResource.name = GetName();
			}
		}

		public override void ReleaseGraphicsResource()
		{
			if (graphicsResource != null)
			{
				graphicsResource.Release();
			}
			base.ReleaseGraphicsResource();
		}

		public override void LogCreation(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger logger)
		{
			logger.LogLine("Created GraphicsBuffer: " + desc.name);
		}

		public override void LogRelease(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger logger)
		{
			logger.LogLine("Released GraphicsBuffer: " + desc.name);
		}
	}
}
