namespace Unity.Collections
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.FixedList32BytesDebugView<>))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct FixedList32Bytes<T> : global::Unity.Collections.INativeList<T>, global::Unity.Collections.IIndexable<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable, global::System.IEquatable<global::Unity.Collections.FixedList32Bytes<T>>, global::System.IComparable<global::Unity.Collections.FixedList32Bytes<T>>, global::System.IEquatable<global::Unity.Collections.FixedList64Bytes<T>>, global::System.IComparable<global::Unity.Collections.FixedList64Bytes<T>>, global::System.IEquatable<global::Unity.Collections.FixedList128Bytes<T>>, global::System.IComparable<global::Unity.Collections.FixedList128Bytes<T>>, global::System.IEquatable<global::Unity.Collections.FixedList512Bytes<T>>, global::System.IComparable<global::Unity.Collections.FixedList512Bytes<T>>, global::System.IEquatable<global::Unity.Collections.FixedList4096Bytes<T>>, global::System.IComparable<global::Unity.Collections.FixedList4096Bytes<T>> where T : unmanaged
	{
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private global::Unity.Collections.FixedList32Bytes<T> m_List;

			private int m_Index;

			public T Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_List[m_Index];
				}
			}

			object global::System.Collections.IEnumerator.Current => Current;

			public Enumerator(ref global::Unity.Collections.FixedList32Bytes<T> list)
			{
				m_List = list;
				m_Index = -1;
			}

			public void Dispose()
			{
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				m_Index++;
				return m_Index < m_List.Length;
			}

			public void Reset()
			{
				m_Index = -1;
			}
		}

		[global::UnityEngine.SerializeField]
		internal global::Unity.Collections.FixedBytes32Align8 data;

		internal unsafe ushort length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				fixed (global::Unity.Collections.FixedBytes32Align8* ptr = &data)
				{
					void* ptr2 = ptr;
					return *(ushort*)ptr2;
				}
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				fixed (global::Unity.Collections.FixedBytes32Align8* ptr = &data)
				{
					void* ptr2 = ptr;
					*(ushort*)ptr2 = value;
				}
			}
		}

		internal unsafe readonly byte* buffer
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (global::Unity.Collections.FixedBytes32Align8* ptr = &data)
				{
					void* ptr2 = ptr;
					return (byte*)ptr2 + global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<ushort>();
				}
			}
		}

		[global::Unity.Properties.CreateProperty]
		public int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return length;
			}
			set
			{
				length = (ushort)value;
			}
		}

		[global::Unity.Properties.CreateProperty]
		private global::System.Collections.Generic.IEnumerable<T> Elements => ToArray();

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Length == 0;
			}
		}

		internal int LengthInBytes => Length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();

		internal unsafe readonly byte* Buffer
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return buffer + global::Unity.Collections.FixedList.PaddingBytes<T>();
			}
		}

		public int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return global::Unity.Collections.FixedList.Capacity<global::Unity.Collections.FixedBytes32Align8, T>();
			}
			set
			{
			}
		}

		public unsafe T this[int index]
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(Buffer, global::Unity.Collections.CollectionHelper.AssumePositive(index));
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(Buffer, global::Unity.Collections.CollectionHelper.AssumePositive(index), value);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<T>(Buffer, index);
		}

		public unsafe override int GetHashCode()
		{
			return (int)global::Unity.Collections.CollectionHelper.Hash(Buffer, LengthInBytes);
		}

		public void Add(in T item)
		{
			AddNoResize(in item);
		}

		public unsafe void AddRange(void* ptr, int length)
		{
			AddRangeNoResize(ptr, length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void AddNoResize(in T item)
		{
			this[Length++] = item;
		}

		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			int num = Length;
			Length += length;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Buffer + (nint)num * (nint)sizeof(T), ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * length);
		}

		public unsafe void AddReplicate(in T value, int count)
		{
			int num = Length;
			Length += count;
			fixed (T* source = &value)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpyReplicate(Buffer + (nint)num * (nint)sizeof(T), source, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), count);
			}
		}

		public void Clear()
		{
			Length = 0;
		}

		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num >= 1)
			{
				int num2 = length - begin;
				Length += num;
				if (num2 >= 1)
				{
					int num3 = num2 * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
					byte* num4 = Buffer;
					byte* destination = num4 + end * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
					byte* source = num4 + begin * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(destination, source, num3);
				}
			}
		}

		public void InsertRange(int index, int count)
		{
			InsertRangeWithBeginEnd(index, index + count);
		}

		public void Insert(int index, in T item)
		{
			InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		public void RemoveAtSwapBack(int index)
		{
			RemoveRangeSwapBack(index, 1);
		}

		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			if (count > 0)
			{
				int num = global::Unity.Mathematics.math.max(Length - count, index + count);
				int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				void* destination = Buffer + index * num2;
				void* source = Buffer + num * num2;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, (Length - num) * num2);
				Length -= count;
			}
		}

		public void RemoveAt(int index)
		{
			RemoveRange(index, 1);
		}

		public unsafe void RemoveRange(int index, int count)
		{
			if (count > 0)
			{
				int num = global::Unity.Mathematics.math.min(index + count, Length);
				int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				void* destination = Buffer + index * num2;
				void* source = Buffer + num * num2;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, (Length - num) * num2);
				Length -= count;
			}
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed array")]
		public unsafe T[] ToArray()
		{
			T[] array = new T[Length];
			byte* source = Buffer;
			fixed (T* destination = array)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, LengthInBytes);
			}
			return array;
		}

		public unsafe global::Unity.Collections.NativeArray<T> ToNativeArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeArray<T> nativeArray = global::Unity.Collections.CollectionHelper.CreateNativeArray<T>(Length, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), Buffer, LengthInBytes);
			return nativeArray;
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(a.Buffer, b.Buffer, a.LengthInBytes) == 0;
		}

		public static bool operator !=(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList32Bytes<T> b)
		{
			return !(a == b);
		}

		public unsafe int CompareTo(global::Unity.Collections.FixedList32Bytes<T> other)
		{
			byte* num = buffer;
			byte* ptr = other.buffer;
			byte* ptr2 = num + global::Unity.Collections.FixedList.PaddingBytes<T>();
			byte* ptr3 = ptr + global::Unity.Collections.FixedList.PaddingBytes<T>();
			int num2 = global::Unity.Mathematics.math.min(Length, other.Length);
			for (int i = 0; i < num2; i++)
			{
				int num3 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr2 + sizeof(T) * i, ptr3 + sizeof(T) * i, sizeof(T));
				if (num3 != 0)
				{
					return num3;
				}
			}
			return Length.CompareTo(other.Length);
		}

		public bool Equals(global::Unity.Collections.FixedList32Bytes<T> other)
		{
			return CompareTo(other) == 0;
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(a.Buffer, b.Buffer, a.LengthInBytes) == 0;
		}

		public static bool operator !=(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList64Bytes<T> b)
		{
			return !(a == b);
		}

		public unsafe int CompareTo(global::Unity.Collections.FixedList64Bytes<T> other)
		{
			byte* num = buffer;
			byte* ptr = other.buffer;
			byte* ptr2 = num + global::Unity.Collections.FixedList.PaddingBytes<T>();
			byte* ptr3 = ptr + global::Unity.Collections.FixedList.PaddingBytes<T>();
			int num2 = global::Unity.Mathematics.math.min(Length, other.Length);
			for (int i = 0; i < num2; i++)
			{
				int num3 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr2 + sizeof(T) * i, ptr3 + sizeof(T) * i, sizeof(T));
				if (num3 != 0)
				{
					return num3;
				}
			}
			return Length.CompareTo(other.Length);
		}

		public bool Equals(global::Unity.Collections.FixedList64Bytes<T> other)
		{
			return CompareTo(other) == 0;
		}

		public FixedList32Bytes(in global::Unity.Collections.FixedList64Bytes<T> other)
		{
			this = default(global::Unity.Collections.FixedList32Bytes<T>);
			Initialize(in other);
		}

		internal unsafe int Initialize(in global::Unity.Collections.FixedList64Bytes<T> other)
		{
			if (other.Length > Capacity)
			{
				return 1;
			}
			length = other.length;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Buffer, other.Buffer, LengthInBytes);
			return 0;
		}

		public static implicit operator global::Unity.Collections.FixedList32Bytes<T>(in global::Unity.Collections.FixedList64Bytes<T> other)
		{
			return new global::Unity.Collections.FixedList32Bytes<T>(in other);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(a.Buffer, b.Buffer, a.LengthInBytes) == 0;
		}

		public static bool operator !=(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList128Bytes<T> b)
		{
			return !(a == b);
		}

		public unsafe int CompareTo(global::Unity.Collections.FixedList128Bytes<T> other)
		{
			byte* num = buffer;
			byte* ptr = other.buffer;
			byte* ptr2 = num + global::Unity.Collections.FixedList.PaddingBytes<T>();
			byte* ptr3 = ptr + global::Unity.Collections.FixedList.PaddingBytes<T>();
			int num2 = global::Unity.Mathematics.math.min(Length, other.Length);
			for (int i = 0; i < num2; i++)
			{
				int num3 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr2 + sizeof(T) * i, ptr3 + sizeof(T) * i, sizeof(T));
				if (num3 != 0)
				{
					return num3;
				}
			}
			return Length.CompareTo(other.Length);
		}

		public bool Equals(global::Unity.Collections.FixedList128Bytes<T> other)
		{
			return CompareTo(other) == 0;
		}

		public FixedList32Bytes(in global::Unity.Collections.FixedList128Bytes<T> other)
		{
			this = default(global::Unity.Collections.FixedList32Bytes<T>);
			Initialize(in other);
		}

		internal unsafe int Initialize(in global::Unity.Collections.FixedList128Bytes<T> other)
		{
			if (other.Length > Capacity)
			{
				return 1;
			}
			length = other.length;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Buffer, other.Buffer, LengthInBytes);
			return 0;
		}

		public static implicit operator global::Unity.Collections.FixedList32Bytes<T>(in global::Unity.Collections.FixedList128Bytes<T> other)
		{
			return new global::Unity.Collections.FixedList32Bytes<T>(in other);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(a.Buffer, b.Buffer, a.LengthInBytes) == 0;
		}

		public static bool operator !=(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList512Bytes<T> b)
		{
			return !(a == b);
		}

		public unsafe int CompareTo(global::Unity.Collections.FixedList512Bytes<T> other)
		{
			byte* num = buffer;
			byte* ptr = other.buffer;
			byte* ptr2 = num + global::Unity.Collections.FixedList.PaddingBytes<T>();
			byte* ptr3 = ptr + global::Unity.Collections.FixedList.PaddingBytes<T>();
			int num2 = global::Unity.Mathematics.math.min(Length, other.Length);
			for (int i = 0; i < num2; i++)
			{
				int num3 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr2 + sizeof(T) * i, ptr3 + sizeof(T) * i, sizeof(T));
				if (num3 != 0)
				{
					return num3;
				}
			}
			return Length.CompareTo(other.Length);
		}

		public bool Equals(global::Unity.Collections.FixedList512Bytes<T> other)
		{
			return CompareTo(other) == 0;
		}

		public FixedList32Bytes(in global::Unity.Collections.FixedList512Bytes<T> other)
		{
			this = default(global::Unity.Collections.FixedList32Bytes<T>);
			Initialize(in other);
		}

		internal unsafe int Initialize(in global::Unity.Collections.FixedList512Bytes<T> other)
		{
			if (other.Length > Capacity)
			{
				return 1;
			}
			length = other.length;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Buffer, other.Buffer, LengthInBytes);
			return 0;
		}

		public static implicit operator global::Unity.Collections.FixedList32Bytes<T>(in global::Unity.Collections.FixedList512Bytes<T> other)
		{
			return new global::Unity.Collections.FixedList32Bytes<T>(in other);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(a.Buffer, b.Buffer, a.LengthInBytes) == 0;
		}

		public static bool operator !=(in global::Unity.Collections.FixedList32Bytes<T> a, in global::Unity.Collections.FixedList4096Bytes<T> b)
		{
			return !(a == b);
		}

		public unsafe int CompareTo(global::Unity.Collections.FixedList4096Bytes<T> other)
		{
			byte* num = buffer;
			byte* ptr = other.buffer;
			byte* ptr2 = num + global::Unity.Collections.FixedList.PaddingBytes<T>();
			byte* ptr3 = ptr + global::Unity.Collections.FixedList.PaddingBytes<T>();
			int num2 = global::Unity.Mathematics.math.min(Length, other.Length);
			for (int i = 0; i < num2; i++)
			{
				int num3 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr2 + sizeof(T) * i, ptr3 + sizeof(T) * i, sizeof(T));
				if (num3 != 0)
				{
					return num3;
				}
			}
			return Length.CompareTo(other.Length);
		}

		public bool Equals(global::Unity.Collections.FixedList4096Bytes<T> other)
		{
			return CompareTo(other) == 0;
		}

		public FixedList32Bytes(in global::Unity.Collections.FixedList4096Bytes<T> other)
		{
			this = default(global::Unity.Collections.FixedList32Bytes<T>);
			Initialize(in other);
		}

		internal unsafe int Initialize(in global::Unity.Collections.FixedList4096Bytes<T> other)
		{
			if (other.Length > Capacity)
			{
				return 1;
			}
			length = other.length;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Buffer, other.Buffer, LengthInBytes);
			return 0;
		}

		public static implicit operator global::Unity.Collections.FixedList32Bytes<T>(in global::Unity.Collections.FixedList4096Bytes<T> other)
		{
			return new global::Unity.Collections.FixedList32Bytes<T>(in other);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed object")]
		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Collections.FixedList32Bytes<T> other)
			{
				return Equals(other);
			}
			if (obj is global::Unity.Collections.FixedList64Bytes<T> other2)
			{
				return Equals(other2);
			}
			if (obj is global::Unity.Collections.FixedList128Bytes<T> other3)
			{
				return Equals(other3);
			}
			if (obj is global::Unity.Collections.FixedList512Bytes<T> other4)
			{
				return Equals(other4);
			}
			if (obj is global::Unity.Collections.FixedList4096Bytes<T> other5)
			{
				return Equals(other5);
			}
			return false;
		}

		public global::Unity.Collections.FixedList32Bytes<T>.Enumerator GetEnumerator()
		{
			return new global::Unity.Collections.FixedList32Bytes<T>.Enumerator(ref this);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		global::System.Collections.Generic.IEnumerator<T> global::System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}
	}
}
