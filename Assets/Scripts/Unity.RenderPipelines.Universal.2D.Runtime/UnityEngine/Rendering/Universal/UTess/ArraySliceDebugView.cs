namespace UnityEngine.Rendering.Universal.UTess
{
	internal sealed class ArraySliceDebugView<T> where T : struct
	{
		private global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T> m_Slice;

		public T[] Items => m_Slice.ToArray();

		public ArraySliceDebugView(global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T> slice)
		{
			m_Slice = slice;
		}
	}
}
