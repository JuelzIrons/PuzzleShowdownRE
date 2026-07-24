namespace UnityEngine.Rendering.RenderGraphModule
{
	internal abstract class IRenderGraphResourcePool
	{
		public abstract void PurgeUnusedResources(int currentFrameIndex);

		public abstract void Cleanup();

		public abstract void CheckFrameAllocation(bool onException, int frameIndex);

		public abstract void LogResources(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger logger);
	}
}
