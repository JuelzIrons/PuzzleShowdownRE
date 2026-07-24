namespace UnityEngine.U2D.Common.UTess
{
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::UnityEngine.U2D.Common.UTess.ArrayDebugView<>))]
	internal struct Array<T> : global::System.IDisposable where T : struct
	{
		internal global::Unity.Collections.NativeArray<T> m_Array;

		internal int m_MaxSize;

		internal global::Unity.Collections.Allocator m_AllocLabel;

		internal global::Unity.Collections.NativeArrayOptions m_Options;

		public T this[int index]
		{
			get
			{
				return m_Array[index];
			}
			set
			{
				ResizeIfRequired(index);
				m_Array[index] = value;
			}
		}

		public bool IsCreated => m_Array.IsCreated;

		public int Length
		{
			get
			{
				if (m_MaxSize == 0)
				{
					return 0;
				}
				return m_Array.Length;
			}
		}

		public int MaxSize => m_MaxSize;

		public unsafe void* UnsafePtr => global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_Array);

		public unsafe void* UnsafeReadOnlyPtr => global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(m_Array);

		public Array(int length, int maxSize, global::Unity.Collections.Allocator allocMode, global::Unity.Collections.NativeArrayOptions options)
		{
			m_Array = new global::Unity.Collections.NativeArray<T>(length, allocMode, options);
			m_AllocLabel = allocMode;
			m_Options = options;
			m_MaxSize = maxSize;
		}

		private void ResizeIfRequired(int index)
		{
			if (index >= m_MaxSize || index < 0)
			{
				throw new global::System.IndexOutOfRangeException($"Trying to access beyond allowed size. {index} is out of range of '{m_MaxSize}' MaxSize.");
			}
			if (index >= m_Array.Length)
			{
				int num;
				for (num = Length; num <= index; num *= 2)
				{
				}
				num = ((num > m_MaxSize) ? m_MaxSize : num);
				global::Unity.Collections.NativeArray<T> nativeArray = new global::Unity.Collections.NativeArray<T>(num, m_AllocLabel, m_Options);
				global::Unity.Collections.NativeArray<T>.Copy(m_Array, nativeArray, Length);
				m_Array.Dispose();
				m_Array = nativeArray;
			}
		}

		public void Dispose()
		{
			m_Array.Dispose();
			m_MaxSize = 0;
		}

		public void CopyTo(T[] array)
		{
			m_Array.CopyTo(array);
		}
	}
}
