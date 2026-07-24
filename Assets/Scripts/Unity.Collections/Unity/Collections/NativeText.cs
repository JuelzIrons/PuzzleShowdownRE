namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}")]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct NativeText : global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IIndexable<byte>, global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::Unity.Collections.IUTF8Bytes, global::System.IComparable<string>, global::System.IEquatable<string>, global::System.IComparable<global::Unity.Collections.NativeText>, global::System.IEquatable<global::Unity.Collections.NativeText>, global::System.IComparable<global::Unity.Collections.FixedString32Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString32Bytes>, global::System.IComparable<global::Unity.Collections.FixedString64Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString64Bytes>, global::System.IComparable<global::Unity.Collections.FixedString128Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString128Bytes>, global::System.IComparable<global::Unity.Collections.FixedString512Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString512Bytes>, global::System.IComparable<global::Unity.Collections.FixedString4096Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString4096Bytes>
	{
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<global::Unity.Collections.Unicode.Rune>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private global::Unity.Collections.NativeText.ReadOnly target;

			private int offset;

			private global::Unity.Collections.Unicode.Rune current;

			object global::System.Collections.IEnumerator.Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return Current;
				}
			}

			public global::Unity.Collections.Unicode.Rune Current => current;

			public Enumerator(global::Unity.Collections.NativeText source)
			{
				target = source.AsReadOnly();
				offset = 0;
				current = default(global::Unity.Collections.Unicode.Rune);
			}

			public Enumerator(global::Unity.Collections.NativeText.ReadOnly source)
			{
				target = source;
				offset = 0;
				current = default(global::Unity.Collections.Unicode.Rune);
			}

			public void Dispose()
			{
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public unsafe bool MoveNext()
			{
				if (offset >= target.Length)
				{
					return false;
				}
				global::Unity.Collections.Unicode.Utf8ToUcs(out current, target.GetUnsafePtr(), ref offset, target.Length);
				return true;
			}

			public void Reset()
			{
				offset = 0;
				current = default(global::Unity.Collections.Unicode.Rune);
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		public struct ReadOnly : global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IIndexable<byte>, global::Unity.Collections.IUTF8Bytes, global::System.IComparable<string>, global::System.IEquatable<string>, global::System.IComparable<global::Unity.Collections.NativeText>, global::System.IEquatable<global::Unity.Collections.NativeText>, global::System.IComparable<global::Unity.Collections.FixedString32Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString32Bytes>, global::System.IComparable<global::Unity.Collections.FixedString64Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString64Bytes>, global::System.IComparable<global::Unity.Collections.FixedString128Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString128Bytes>, global::System.IComparable<global::Unity.Collections.FixedString512Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString512Bytes>, global::System.IComparable<global::Unity.Collections.FixedString4096Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString4096Bytes>
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeText* m_Data;

			public unsafe int Capacity
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return m_Data->Capacity;
				}
				set
				{
				}
			}

			public unsafe bool IsEmpty
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					if (m_Data == null)
					{
						return true;
					}
					return m_Data->IsEmpty;
				}
				set
				{
				}
			}

			public unsafe int Length
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return m_Data->Length;
				}
				set
				{
				}
			}

			public unsafe byte this[int index]
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return m_Data->ElementAt(index);
				}
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				set
				{
				}
			}

			[global::Unity.Properties.CreateProperty]
			[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
			[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed string")]
			public string Value => ToString();

			internal unsafe ReadOnly(global::Unity.Collections.LowLevel.Unsafe.UnsafeText* text)
			{
				m_Data = text;
			}

			public void Clear()
			{
			}

			public ref byte ElementAt(int index)
			{
				throw new global::System.NotSupportedException("Trying to retrieve non-readonly ref to NativeText.ReadOnly data. This is not permitted.");
			}

			public unsafe byte* GetUnsafePtr()
			{
				return m_Data->GetUnsafePtr();
			}

			public bool TryResize(int newLength, global::Unity.Collections.NativeArrayOptions clearOptions = global::Unity.Collections.NativeArrayOptions.ClearMemory)
			{
				return false;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			internal unsafe static void CheckNull(void* dataPtr)
			{
				if (dataPtr == null)
				{
					throw new global::System.InvalidOperationException("NativeText.ReadOnly has yet to be created or has been destroyed!");
				}
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private readonly void CheckRead()
			{
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void ErrorWrite()
			{
				throw new global::System.NotSupportedException("Trying to write to a NativeText.ReadOnly. Write operations are not permitted and are ignored.");
			}

			[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
			public unsafe int CompareTo(string other)
			{
				return m_Data->ToString().CompareTo(other);
			}

			[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
			public unsafe bool Equals(string other)
			{
				return m_Data->ToString().Equals(other);
			}

			public unsafe int CompareTo(global::Unity.Collections.NativeText.ReadOnly other)
			{
				return global::Unity.Collections.FixedStringMethods.CompareTo(ref *m_Data, in *other.m_Data);
			}

			public unsafe bool Equals(global::Unity.Collections.NativeText.ReadOnly other)
			{
				return global::Unity.Collections.FixedStringMethods.Equals(ref *m_Data, in *other.m_Data);
			}

			public unsafe int CompareTo(global::Unity.Collections.NativeText other)
			{
				return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in *other.m_Data);
			}

			public unsafe bool Equals(global::Unity.Collections.NativeText other)
			{
				return global::Unity.Collections.FixedStringMethods.Equals(ref this, in *other.m_Data);
			}

			public int CompareTo(global::Unity.Collections.FixedString32Bytes other)
			{
				return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
			}

			public unsafe static bool operator ==(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString32Bytes b)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeText data = *a.m_Data;
				int length = data.Length;
				int utf8LengthInBytes = b.utf8LengthInBytes;
				byte* unsafePtr = data.GetUnsafePtr();
				byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			public static bool operator !=(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString32Bytes b)
			{
				return !(a == b);
			}

			public bool Equals(global::Unity.Collections.FixedString32Bytes other)
			{
				return this == other;
			}

			public int CompareTo(global::Unity.Collections.FixedString64Bytes other)
			{
				return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
			}

			public unsafe static bool operator ==(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString64Bytes b)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeText data = *a.m_Data;
				int length = data.Length;
				int utf8LengthInBytes = b.utf8LengthInBytes;
				byte* unsafePtr = data.GetUnsafePtr();
				byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			public static bool operator !=(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString64Bytes b)
			{
				return !(a == b);
			}

			public bool Equals(global::Unity.Collections.FixedString64Bytes other)
			{
				return this == other;
			}

			public int CompareTo(global::Unity.Collections.FixedString128Bytes other)
			{
				return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
			}

			public unsafe static bool operator ==(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString128Bytes b)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeText data = *a.m_Data;
				int length = data.Length;
				int utf8LengthInBytes = b.utf8LengthInBytes;
				byte* unsafePtr = data.GetUnsafePtr();
				byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			public static bool operator !=(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString128Bytes b)
			{
				return !(a == b);
			}

			public bool Equals(global::Unity.Collections.FixedString128Bytes other)
			{
				return this == other;
			}

			public int CompareTo(global::Unity.Collections.FixedString512Bytes other)
			{
				return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
			}

			public unsafe static bool operator ==(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString512Bytes b)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeText data = *a.m_Data;
				int length = data.Length;
				int utf8LengthInBytes = b.utf8LengthInBytes;
				byte* unsafePtr = data.GetUnsafePtr();
				byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			public static bool operator !=(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString512Bytes b)
			{
				return !(a == b);
			}

			public bool Equals(global::Unity.Collections.FixedString512Bytes other)
			{
				return this == other;
			}

			public int CompareTo(global::Unity.Collections.FixedString4096Bytes other)
			{
				return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
			}

			public unsafe static bool operator ==(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString4096Bytes b)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeText data = *a.m_Data;
				int length = data.Length;
				int utf8LengthInBytes = b.utf8LengthInBytes;
				byte* unsafePtr = data.GetUnsafePtr();
				byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
			}

			public static bool operator !=(in global::Unity.Collections.NativeText.ReadOnly a, in global::Unity.Collections.FixedString4096Bytes b)
			{
				return !(a == b);
			}

			public bool Equals(global::Unity.Collections.FixedString4096Bytes other)
			{
				return this == other;
			}

			[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed string")]
			public unsafe override string ToString()
			{
				if (m_Data == null)
				{
					return "";
				}
				return global::Unity.Collections.FixedStringMethods.ConvertToString(ref this);
			}

			public override int GetHashCode()
			{
				return global::Unity.Collections.FixedStringMethods.ComputeHashCode(ref this);
			}

			[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed object")]
			public override bool Equals(object other)
			{
				if (other == null)
				{
					return false;
				}
				if (other is string other2)
				{
					return Equals(other2);
				}
				if (other is global::Unity.Collections.NativeText other3)
				{
					return Equals(other3);
				}
				if (other is global::Unity.Collections.NativeText.ReadOnly other4)
				{
					return Equals(other4);
				}
				if (other is global::Unity.Collections.FixedString32Bytes other5)
				{
					return Equals(other5);
				}
				if (other is global::Unity.Collections.FixedString64Bytes other6)
				{
					return Equals(other6);
				}
				if (other is global::Unity.Collections.FixedString128Bytes other7)
				{
					return Equals(other7);
				}
				if (other is global::Unity.Collections.FixedString512Bytes other8)
				{
					return Equals(other8);
				}
				if (other is global::Unity.Collections.FixedString4096Bytes other9)
				{
					return Equals(other9);
				}
				return false;
			}

			public global::Unity.Collections.NativeText.Enumerator GetEnumerator()
			{
				return new global::Unity.Collections.NativeText.Enumerator(this);
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeText* m_Data;

		public unsafe int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return m_Data->Length;
			}
			set
			{
				m_Data->Length = value;
			}
		}

		public unsafe int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return m_Data->Capacity;
			}
			set
			{
				m_Data->Capacity = value;
			}
		}

		public unsafe readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!IsCreated)
				{
					return true;
				}
				return m_Data->IsEmpty;
			}
		}

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Data != null;
			}
		}

		public unsafe byte this[int index]
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Data->ElementAt(index);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				m_Data->ElementAt(index) = value;
			}
		}

		[global::Unity.Properties.CreateProperty]
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed string")]
		public string Value => ToString();

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public NativeText(string source, global::Unity.Collections.Allocator allocator)
			: this(source, (global::Unity.Collections.AllocatorManager.AllocatorHandle)allocator)
		{
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe NativeText(string source, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			: this(source.Length * 2, allocator)
		{
			Length = source.Length * 2;
			fixed (char* src = source)
			{
				if (global::Unity.Collections.UTF8ArrayUnsafeUtility.Copy(GetUnsafePtr(), out var destLength, Capacity, src, source.Length) != global::Unity.Collections.CopyError.None)
				{
					m_Data->Dispose();
					m_Data = global::Unity.Collections.LowLevel.Unsafe.UnsafeText.Alloc(allocator);
					*m_Data = default(global::Unity.Collections.LowLevel.Unsafe.UnsafeText);
				}
				Length = destLength;
			}
		}

		public NativeText(int capacity, global::Unity.Collections.Allocator allocator)
			: this(capacity, (global::Unity.Collections.AllocatorManager.AllocatorHandle)allocator)
		{
		}

		public unsafe NativeText(int capacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_Data = global::Unity.Collections.LowLevel.Unsafe.UnsafeText.Alloc(allocator);
			*m_Data = new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(capacity, allocator);
		}

		public NativeText(global::Unity.Collections.Allocator allocator)
			: this((global::Unity.Collections.AllocatorManager.AllocatorHandle)allocator)
		{
		}

		public NativeText(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			: this(512, allocator)
		{
		}

		public unsafe NativeText(in global::Unity.Collections.FixedString32Bytes source, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			: this(source.utf8LengthInBytes, allocator)
		{
			Length = source.utf8LengthInBytes;
			byte* source2 = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in source.bytes);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(m_Data->GetUnsafePtr(), source2, source.utf8LengthInBytes);
		}

		public NativeText(in global::Unity.Collections.FixedString32Bytes source, global::Unity.Collections.Allocator allocator)
			: this(in source, (global::Unity.Collections.AllocatorManager.AllocatorHandle)allocator)
		{
		}

		public unsafe NativeText(in global::Unity.Collections.FixedString64Bytes source, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			: this(source.utf8LengthInBytes, allocator)
		{
			Length = source.utf8LengthInBytes;
			byte* source2 = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in source.bytes);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(m_Data->GetUnsafePtr(), source2, source.utf8LengthInBytes);
		}

		public NativeText(in global::Unity.Collections.FixedString64Bytes source, global::Unity.Collections.Allocator allocator)
			: this(in source, (global::Unity.Collections.AllocatorManager.AllocatorHandle)allocator)
		{
		}

		public unsafe NativeText(in global::Unity.Collections.FixedString128Bytes source, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			: this(source.utf8LengthInBytes, allocator)
		{
			Length = source.utf8LengthInBytes;
			byte* source2 = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in source.bytes);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(m_Data->GetUnsafePtr(), source2, source.utf8LengthInBytes);
		}

		public NativeText(in global::Unity.Collections.FixedString128Bytes source, global::Unity.Collections.Allocator allocator)
			: this(in source, (global::Unity.Collections.AllocatorManager.AllocatorHandle)allocator)
		{
		}

		public unsafe NativeText(in global::Unity.Collections.FixedString512Bytes source, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			: this(source.utf8LengthInBytes, allocator)
		{
			Length = source.utf8LengthInBytes;
			byte* source2 = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in source.bytes);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(m_Data->GetUnsafePtr(), source2, source.utf8LengthInBytes);
		}

		public NativeText(in global::Unity.Collections.FixedString512Bytes source, global::Unity.Collections.Allocator allocator)
			: this(in source, (global::Unity.Collections.AllocatorManager.AllocatorHandle)allocator)
		{
		}

		public unsafe NativeText(in global::Unity.Collections.FixedString4096Bytes source, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			: this(source.utf8LengthInBytes, allocator)
		{
			Length = source.utf8LengthInBytes;
			byte* source2 = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in source.bytes);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(m_Data->GetUnsafePtr(), source2, source.utf8LengthInBytes);
		}

		public NativeText(in global::Unity.Collections.FixedString4096Bytes source, global::Unity.Collections.Allocator allocator)
			: this(in source, (global::Unity.Collections.AllocatorManager.AllocatorHandle)allocator)
		{
		}

		public bool TryResize(int newLength, global::Unity.Collections.NativeArrayOptions clearOptions = global::Unity.Collections.NativeArrayOptions.ClearMemory)
		{
			Length = newLength;
			return true;
		}

		public unsafe byte* GetUnsafePtr()
		{
			return m_Data->GetUnsafePtr();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe ref byte ElementAt(int index)
		{
			return ref m_Data->ElementAt(index);
		}

		public void Clear()
		{
			Length = 0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void Add(in byte value)
		{
			this[Length++] = value;
		}

		public unsafe int CompareTo(global::Unity.Collections.NativeText other)
		{
			return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in *other.m_Data);
		}

		public unsafe bool Equals(global::Unity.Collections.NativeText other)
		{
			return global::Unity.Collections.FixedStringMethods.Equals(ref this, in *other.m_Data);
		}

		public int CompareTo(global::Unity.Collections.NativeText.ReadOnly other)
		{
			return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
		}

		public unsafe bool Equals(global::Unity.Collections.NativeText.ReadOnly other)
		{
			return global::Unity.Collections.FixedStringMethods.Equals(ref this, in *other.m_Data);
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeText.Free(m_Data);
				m_Data = null;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeTextDisposeJob
			{
				Data = new global::Unity.Collections.NativeTextDispose
				{
					m_TextData = m_Data
				}
			}, inputDeps);
			m_Data = null;
			return result;
		}

		public global::Unity.Collections.NativeText.Enumerator GetEnumerator()
		{
			return new global::Unity.Collections.NativeText.Enumerator(this);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public int CompareTo(string other)
		{
			return ToString().CompareTo(other);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public bool Equals(string other)
		{
			return ToString().Equals(other);
		}

		public int CompareTo(global::Unity.Collections.FixedString32Bytes other)
		{
			return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString32Bytes b)
		{
			global::Unity.Collections.NativeText nativeText = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		public static bool operator !=(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString32Bytes b)
		{
			return !(a == b);
		}

		public bool Equals(global::Unity.Collections.FixedString32Bytes other)
		{
			return this == other;
		}

		public int CompareTo(global::Unity.Collections.FixedString64Bytes other)
		{
			return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString64Bytes b)
		{
			global::Unity.Collections.NativeText nativeText = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		public static bool operator !=(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString64Bytes b)
		{
			return !(a == b);
		}

		public bool Equals(global::Unity.Collections.FixedString64Bytes other)
		{
			return this == other;
		}

		public int CompareTo(global::Unity.Collections.FixedString128Bytes other)
		{
			return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString128Bytes b)
		{
			global::Unity.Collections.NativeText nativeText = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		public static bool operator !=(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString128Bytes b)
		{
			return !(a == b);
		}

		public bool Equals(global::Unity.Collections.FixedString128Bytes other)
		{
			return this == other;
		}

		public int CompareTo(global::Unity.Collections.FixedString512Bytes other)
		{
			return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString512Bytes b)
		{
			global::Unity.Collections.NativeText nativeText = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		public static bool operator !=(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString512Bytes b)
		{
			return !(a == b);
		}

		public bool Equals(global::Unity.Collections.FixedString512Bytes other)
		{
			return this == other;
		}

		public int CompareTo(global::Unity.Collections.FixedString4096Bytes other)
		{
			return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString4096Bytes b)
		{
			global::Unity.Collections.NativeText nativeText = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, bBytes, utf8LengthInBytes);
		}

		public static bool operator !=(in global::Unity.Collections.NativeText a, in global::Unity.Collections.FixedString4096Bytes b)
		{
			return !(a == b);
		}

		public bool Equals(global::Unity.Collections.FixedString4096Bytes other)
		{
			return this == other;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed string")]
		public unsafe override string ToString()
		{
			if (m_Data == null)
			{
				return "";
			}
			return global::Unity.Collections.FixedStringMethods.ConvertToString(ref this);
		}

		public override int GetHashCode()
		{
			return global::Unity.Collections.FixedStringMethods.ComputeHashCode(ref this);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed object")]
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (other is string other2)
			{
				return Equals(other2);
			}
			if (other is global::Unity.Collections.NativeText other3)
			{
				return Equals(other3);
			}
			if (other is global::Unity.Collections.NativeText.ReadOnly other4)
			{
				return Equals(other4);
			}
			if (other is global::Unity.Collections.FixedString32Bytes other5)
			{
				return Equals(other5);
			}
			if (other is global::Unity.Collections.FixedString64Bytes other6)
			{
				return Equals(other6);
			}
			if (other is global::Unity.Collections.FixedString128Bytes other7)
			{
				return Equals(other7);
			}
			if (other is global::Unity.Collections.FixedString512Bytes other8)
			{
				return Equals(other8);
			}
			if (other is global::Unity.Collections.FixedString4096Bytes other9)
			{
				return Equals(other9);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal unsafe static void CheckNull(void* dataPtr)
		{
			if (dataPtr == null)
			{
				throw new global::System.InvalidOperationException("NativeText has yet to be created or has been destroyed!");
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckRead()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckWriteAndBumpSecondaryVersion()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new global::System.IndexOutOfRangeException($"Index {index} must be positive.");
			}
			if (index >= Length)
			{
				throw new global::System.IndexOutOfRangeException($"Index {index} is out of range in NativeText of {Length} length.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowCopyError(global::Unity.Collections.CopyError error, string source)
		{
			throw new global::System.ArgumentException($"NativeText: {error} while copying \"{source}\"");
		}

		public unsafe global::Unity.Collections.NativeText.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.NativeText.ReadOnly(m_Data);
		}
	}
}
