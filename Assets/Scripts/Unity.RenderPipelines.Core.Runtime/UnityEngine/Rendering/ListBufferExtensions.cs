namespace UnityEngine.Rendering
{
	public static class ListBufferExtensions
	{
		public unsafe static void QuickSort<T>(this global::UnityEngine.Rendering.ListBuffer<T> self) where T : unmanaged, global::System.IComparable<T>
		{
			global::UnityEngine.Rendering.CoreUnsafeUtils.QuickSort<int>(self.Count, self.BufferPtr);
		}
	}
}
