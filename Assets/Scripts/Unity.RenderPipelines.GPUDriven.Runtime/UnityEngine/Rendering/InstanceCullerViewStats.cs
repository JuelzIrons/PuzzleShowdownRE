namespace UnityEngine.Rendering
{
	internal struct InstanceCullerViewStats
	{
		public global::UnityEngine.Rendering.BatchCullingViewType viewType;

		public int viewInstanceID;

		public int splitIndex;

		public int visibleInstancesOnCPU;

		public int visibleInstancesOnGPU;

		public int visiblePrimitivesOnCPU;

		public int visiblePrimitivesOnGPU;

		public int drawCommands;
	}
}
