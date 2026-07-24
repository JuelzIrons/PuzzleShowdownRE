namespace UnityEngine.Rendering.Universal
{
	internal struct URPLightShadowCullingInfos
	{
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowSliceData> slices;

		public uint slicesValidMask;

		public readonly bool IsSliceValid(int i)
		{
			return (slicesValidMask & (1 << i)) != 0;
		}
	}
}
