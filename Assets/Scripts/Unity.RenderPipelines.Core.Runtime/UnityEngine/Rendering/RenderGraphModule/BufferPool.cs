namespace UnityEngine.Rendering.RenderGraphModule
{
	internal class BufferPool : global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<global::UnityEngine.GraphicsBuffer>
	{
		protected override void ReleaseInternalResource(global::UnityEngine.GraphicsBuffer res)
		{
			res.Release();
		}

		protected override string GetResourceName(in global::UnityEngine.GraphicsBuffer res)
		{
			return "GraphicsBufferNameNotAvailable";
		}

		protected override long GetResourceSize(in global::UnityEngine.GraphicsBuffer res)
		{
			return res.count * res.stride;
		}

		protected override string GetResourceTypeName()
		{
			return "GraphicsBuffer";
		}

		protected override int GetSortIndex(global::UnityEngine.GraphicsBuffer res)
		{
			return res.GetHashCode();
		}
	}
}
