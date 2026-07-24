namespace UnityEngine.U2D
{
	internal class SpriteShapeCopyUtility<T> where T : struct
	{
		internal static void Copy(global::Unity.Collections.NativeSlice<T> dst, T[] src, int length)
		{
			new global::Unity.Collections.NativeSlice<T>(dst, 0, length).CopyFrom(src);
		}

		internal static void Copy(T[] dst, global::Unity.Collections.NativeSlice<T> src, int length)
		{
			new global::Unity.Collections.NativeSlice<T>(src, 0, length).CopyTo(dst);
		}
	}
}
