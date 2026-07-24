namespace UnityEngine.U2D.Animation
{
	internal struct NativeCustomSlice<T> where T : struct
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public global::System.IntPtr data;

		public int length;

		public int stride;

		public unsafe T this[int index] => global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElementWithStride<T>(data.ToPointer(), index, stride);

		public int Length => length;

		public static global::UnityEngine.U2D.Animation.NativeCustomSlice<T> Default()
		{
			return new global::UnityEngine.U2D.Animation.NativeCustomSlice<T>
			{
				data = global::System.IntPtr.Zero,
				length = 0,
				stride = 0
			};
		}

		public unsafe NativeCustomSlice(global::Unity.Collections.NativeSlice<T> nativeSlice)
		{
			data = new global::System.IntPtr(global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafeReadOnlyPtr(nativeSlice));
			length = nativeSlice.Length;
			stride = nativeSlice.Stride;
		}

		public unsafe NativeCustomSlice(global::Unity.Collections.NativeSlice<byte> slice, int length, int stride)
		{
			data = new global::System.IntPtr(global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafeReadOnlyPtr(slice));
			this.length = length;
			this.stride = stride;
		}
	}
}
