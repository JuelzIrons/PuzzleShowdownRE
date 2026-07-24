namespace UnityEngine.Rendering
{
	internal struct InstanceOcclusionTestSubviewSettings
	{
		public int testCount;

		public int occluderSubviewIndices;

		public int occluderSubviewMask;

		public int cullingSplitIndices;

		public int cullingSplitMask;

		public static global::UnityEngine.Rendering.InstanceOcclusionTestSubviewSettings FromSpan(global::System.ReadOnlySpan<global::UnityEngine.Rendering.SubviewOcclusionTest> subviewOcclusionTests)
		{
			global::UnityEngine.Rendering.InstanceOcclusionTestSubviewSettings result = default(global::UnityEngine.Rendering.InstanceOcclusionTestSubviewSettings);
			for (int i = 0; i < subviewOcclusionTests.Length; i++)
			{
				global::UnityEngine.Rendering.SubviewOcclusionTest subviewOcclusionTest = subviewOcclusionTests[i];
				result.occluderSubviewIndices |= subviewOcclusionTest.occluderSubviewIndex << 4 * i;
				result.occluderSubviewMask |= 1 << subviewOcclusionTest.occluderSubviewIndex;
				result.cullingSplitIndices |= subviewOcclusionTest.cullingSplitIndex << 4 * i;
				result.cullingSplitMask |= 1 << subviewOcclusionTest.cullingSplitIndex;
			}
			result.testCount = subviewOcclusionTests.Length;
			return result;
		}
	}
}
