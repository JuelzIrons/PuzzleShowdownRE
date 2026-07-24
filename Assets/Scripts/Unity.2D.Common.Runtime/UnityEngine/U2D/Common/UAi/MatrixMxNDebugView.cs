namespace UnityEngine.U2D.Common.UAi
{
	internal sealed class MatrixMxNDebugView<T> where T : struct
	{
		private global::UnityEngine.U2D.Common.UAi.MatrixMxN<T> array;

		public T[] Items
		{
			get
			{
				T[] result = new T[array.Length];
				array.CopyTo(result);
				return result;
			}
		}

		public MatrixMxNDebugView(global::UnityEngine.U2D.Common.UAi.MatrixMxN<T> array)
		{
			this.array = array;
		}
	}
}
