namespace UnityEngine.U2D.Common.UTess
{
	internal sealed class ArrayDebugView<T> where T : struct
	{
		private global::UnityEngine.U2D.Common.UTess.Array<T> array;

		public T[] Items
		{
			get
			{
				T[] result = new T[array.Length];
				array.CopyTo(result);
				return result;
			}
		}

		public ArrayDebugView(global::UnityEngine.U2D.Common.UTess.Array<T> array)
		{
			this.array = array;
		}
	}
}
