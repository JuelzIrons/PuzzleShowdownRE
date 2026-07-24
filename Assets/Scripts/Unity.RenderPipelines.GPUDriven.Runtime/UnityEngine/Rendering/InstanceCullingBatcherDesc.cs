namespace UnityEngine.Rendering
{
	internal struct InstanceCullingBatcherDesc
	{
		public global::UnityEngine.Rendering.OnCullingCompleteCallback onCompleteCallback;

		public static global::UnityEngine.Rendering.InstanceCullingBatcherDesc NewDefault()
		{
			return new global::UnityEngine.Rendering.InstanceCullingBatcherDesc
			{
				onCompleteCallback = null
			};
		}
	}
}
