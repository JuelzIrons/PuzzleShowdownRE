namespace UnityEngine.U2D.Animation
{
	internal struct NativeCustomSliceEnumerator<T> : global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable where T : struct
	{
		private global::UnityEngine.U2D.Animation.NativeCustomSlice<T> nativeCustomSlice;

		private int index;

		public T Current => nativeCustomSlice[index];

		object global::System.Collections.IEnumerator.Current => Current;

		internal NativeCustomSliceEnumerator(global::Unity.Collections.NativeSlice<byte> slice, int length, int stride)
		{
			nativeCustomSlice = new global::UnityEngine.U2D.Animation.NativeCustomSlice<T>(slice, length, stride);
			index = -1;
			Reset();
		}

		public global::System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			return this;
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool MoveNext()
		{
			if (++index < nativeCustomSlice.length)
			{
				return true;
			}
			return false;
		}

		public void Reset()
		{
			index = -1;
		}

		void global::System.IDisposable.Dispose()
		{
		}
	}
}
