namespace UnityEngine.U2D.Common.UAi
{
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::UnityEngine.U2D.Common.UAi.MatrixMxNDebugView<>))]
	internal struct MatrixMxN<T> : global::System.IDisposable where T : struct
	{
		internal global::Unity.Collections.NativeArray<T> m_Array;

		internal int m_Width;

		internal int m_Height;

		internal global::Unity.Collections.Allocator m_AllocLabel;

		internal global::Unity.Collections.NativeArrayOptions m_Options;

		private T this[int index]
		{
			get
			{
				return m_Array[index];
			}
			set
			{
				m_Array[index] = value;
			}
		}

		public bool IsCreated => m_Array.IsCreated;

		public int Length => m_Width * m_Height;

		public int DimensionX => m_Width;

		public int DimensionY => m_Height;

		public MatrixMxN(int width, int height, global::Unity.Collections.Allocator allocMode, global::Unity.Collections.NativeArrayOptions options)
		{
			m_Width = width;
			m_Height = height;
			m_Array = new global::Unity.Collections.NativeArray<T>(m_Width * m_Height, allocMode, options);
			m_AllocLabel = allocMode;
			m_Options = options;
		}

		public global::Unity.Collections.NativeArray<T> GetArray()
		{
			return m_Array;
		}

		public T Get(int x, int y)
		{
			return m_Array[x * m_Height + y];
		}

		public void Set(int x, int y, T v)
		{
			m_Array[x * m_Height + y] = v;
		}

		public void Dispose()
		{
			m_Array.Dispose();
			m_Width = 0;
			m_Height = 0;
		}

		public void CopyTo(T[] array)
		{
			m_Array.CopyTo(array);
		}
	}
}
