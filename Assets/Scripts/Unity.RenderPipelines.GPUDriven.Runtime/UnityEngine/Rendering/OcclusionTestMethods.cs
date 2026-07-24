namespace UnityEngine.Rendering
{
	public static class OcclusionTestMethods
	{
		public static uint GetBatchLayerMask(this global::UnityEngine.Rendering.OcclusionTest occlusionTest)
		{
			if (occlusionTest != global::UnityEngine.Rendering.OcclusionTest.TestCulled)
			{
				return uint.MaxValue;
			}
			return 268435456u;
		}
	}
}
