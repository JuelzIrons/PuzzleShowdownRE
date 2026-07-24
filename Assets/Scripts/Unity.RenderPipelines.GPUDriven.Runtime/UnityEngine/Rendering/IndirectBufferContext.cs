namespace UnityEngine.Rendering
{
	internal struct IndirectBufferContext
	{
		public enum BufferState
		{
			Pending = 0,
			Zeroed = 1,
			NoOcclusionTest = 2,
			AllInstancesOcclusionTested = 3,
			OccludedInstancesReTested = 4
		}

		public global::Unity.Jobs.JobHandle cullingJobHandle;

		public global::UnityEngine.Rendering.IndirectBufferContext.BufferState bufferState;

		public int occluderVersion;

		public int subviewMask;

		public IndirectBufferContext(global::Unity.Jobs.JobHandle cullingJobHandle)
		{
			this.cullingJobHandle = cullingJobHandle;
			bufferState = global::UnityEngine.Rendering.IndirectBufferContext.BufferState.Pending;
			occluderVersion = 0;
			subviewMask = 0;
		}

		public bool Matches(global::UnityEngine.Rendering.IndirectBufferContext.BufferState bufferState, int occluderVersion, int subviewMask)
		{
			if (this.bufferState == bufferState && this.occluderVersion == occluderVersion)
			{
				return this.subviewMask == subviewMask;
			}
			return false;
		}
	}
}
