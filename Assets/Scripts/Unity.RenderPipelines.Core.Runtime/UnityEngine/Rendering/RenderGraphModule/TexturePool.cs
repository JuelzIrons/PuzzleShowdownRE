namespace UnityEngine.Rendering.RenderGraphModule
{
	internal class TexturePool : global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<global::UnityEngine.Rendering.RTHandle>
	{
		protected override void ReleaseInternalResource(global::UnityEngine.Rendering.RTHandle res)
		{
			res.Release();
		}

		protected override string GetResourceName(in global::UnityEngine.Rendering.RTHandle res)
		{
			return res.rt.name;
		}

		protected override long GetResourceSize(in global::UnityEngine.Rendering.RTHandle res)
		{
			return global::UnityEngine.Profiling.Profiler.GetRuntimeMemorySizeLong(res.rt);
		}

		protected override string GetResourceTypeName()
		{
			return "Texture";
		}

		protected override int GetSortIndex(global::UnityEngine.Rendering.RTHandle res)
		{
			return res.GetInstanceID();
		}
	}
}
