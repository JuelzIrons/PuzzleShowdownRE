namespace UnityEngine.Rendering.Universal.UTess
{
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::UnityEngine.Rendering.Universal.UTess.ArraySliceDebugView<>))]
	internal struct ArraySlice<T> : global::System.IEquatable<global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T>> where T : struct
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		internal int m_Stride;

		internal int m_Length;

		public unsafe T this[int index]
		{
			get
			{
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElementWithStride<T>(m_Buffer, index, m_Stride);
			}
			[global::Unity.Collections.LowLevel.Unsafe.WriteAccessRequired]
			set
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElementWithStride(m_Buffer, index, m_Stride, value);
			}
		}

		public int Stride => m_Stride;

		public int Length => m_Length;

		public unsafe ArraySlice(global::Unity.Collections.NativeArray<T> array, int start, int length)
		{
			m_Stride = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			byte* buffer = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(array) + m_Stride * start;
			m_Buffer = buffer;
			m_Length = length;
		}

		public unsafe bool Equals(global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T> other)
		{
			if (m_Buffer == other.m_Buffer && m_Stride == other.m_Stride)
			{
				return m_Length == other.m_Length;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T>)
			{
				return Equals((global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T>)obj);
			}
			return false;
		}

		public unsafe override int GetHashCode()
		{
			return ((((int)m_Buffer * 397) ^ m_Stride) * 397) ^ m_Length;
		}

		public static bool operator ==(global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T> left, global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T> left, global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T> right)
		{
			return !left.Equals(right);
		}

		public unsafe static global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T> ConvertExistingDataToArraySlice(void* dataPointer, int stride, int length)
		{
			if (length < 0)
			{
				throw new global::System.ArgumentException($"Invalid length of '{length}'. It must be greater than 0.", "length");
			}
			if (stride < 0)
			{
				throw new global::System.ArgumentException($"Invalid stride '{stride}'. It must be greater than 0.", "stride");
			}
			return new global::UnityEngine.Rendering.Universal.UTess.ArraySlice<T>
			{
				m_Stride = stride,
				m_Buffer = (byte*)dataPointer,
				m_Length = length
			};
		}

		internal unsafe void* GetUnsafeReadOnlyPtr()
		{
			return m_Buffer;
		}

		internal unsafe void CopyTo(T[] array)
		{
			global::System.Runtime.InteropServices.GCHandle gCHandle = global::System.Runtime.InteropServices.GCHandle.Alloc(array, global::System.Runtime.InteropServices.GCHandleType.Pinned);
			global::System.IntPtr intPtr = gCHandle.AddrOfPinnedObject();
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpyStride((void*)intPtr, num, GetUnsafeReadOnlyPtr(), Stride, num, m_Length);
			gCHandle.Free();
		}

		internal T[] ToArray()
		{
			T[] array = new T[Length];
			CopyTo(array);
			return array;
		}
	}
}
