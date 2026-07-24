namespace UnityEngine.U2D.Common.URaster
{
	internal struct Pixels
	{
		internal global::Unity.Mathematics.int4 rect;

		internal global::Unity.Mathematics.int4 minmax;

		internal global::Unity.Mathematics.int4 texrect;

		internal global::Unity.Mathematics.int2 size;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		internal global::Unity.Collections.NativeArray<byte> data;
	}
}
