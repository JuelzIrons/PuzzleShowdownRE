namespace UnityEngine.Rendering
{
	internal class DebugRendererBatcherStats : global::System.IDisposable
	{
		public bool enabled;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceCullerViewStats> instanceCullerStats;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceOcclusionEventStats> instanceOcclusionEventStats;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DebugOccluderStats> occluderStats;

		public bool occlusionOverlayEnabled;

		public bool occlusionOverlayCountVisible;

		public bool overrideOcclusionTestToAlwaysPass;

		public DebugRendererBatcherStats()
		{
			instanceCullerStats = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceCullerViewStats>(global::Unity.Collections.Allocator.Persistent);
			instanceOcclusionEventStats = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceOcclusionEventStats>(global::Unity.Collections.Allocator.Persistent);
			occluderStats = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DebugOccluderStats>(global::Unity.Collections.Allocator.Persistent);
		}

		public void FinalizeInstanceCullerViewStats()
		{
			for (int i = 0; i < instanceCullerStats.Length; i++)
			{
				global::UnityEngine.Rendering.InstanceCullerViewStats value = instanceCullerStats[i];
				global::UnityEngine.Rendering.InstanceOcclusionEventStats lastInstanceOcclusionEventStatsForView = GetLastInstanceOcclusionEventStatsForView(i);
				if (lastInstanceOcclusionEventStatsForView.viewInstanceID == value.viewInstanceID)
				{
					value.visibleInstancesOnGPU = global::System.Math.Min(lastInstanceOcclusionEventStatsForView.visibleInstances, value.visibleInstancesOnCPU);
					value.visiblePrimitivesOnGPU = global::System.Math.Min(lastInstanceOcclusionEventStatsForView.visiblePrimitives, value.visiblePrimitivesOnCPU);
				}
				else
				{
					value.visibleInstancesOnGPU = value.visibleInstancesOnCPU;
					value.visiblePrimitivesOnGPU = value.visiblePrimitivesOnCPU;
				}
				instanceCullerStats[i] = value;
			}
		}

		private global::UnityEngine.Rendering.InstanceOcclusionEventStats GetLastInstanceOcclusionEventStatsForView(int viewIndex)
		{
			if (viewIndex < instanceCullerStats.Length)
			{
				int viewInstanceID = instanceCullerStats[viewIndex].viewInstanceID;
				for (int num = instanceOcclusionEventStats.Length - 1; num >= 0; num--)
				{
					if (instanceOcclusionEventStats[num].viewInstanceID == viewInstanceID)
					{
						return instanceOcclusionEventStats[num];
					}
				}
			}
			return default(global::UnityEngine.Rendering.InstanceOcclusionEventStats);
		}

		public void Dispose()
		{
			if (instanceCullerStats.IsCreated)
			{
				instanceCullerStats.Dispose();
			}
			if (instanceOcclusionEventStats.IsCreated)
			{
				instanceOcclusionEventStats.Dispose();
			}
			if (occluderStats.IsCreated)
			{
				occluderStats.Dispose();
			}
		}
	}
}
