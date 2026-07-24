namespace Unity.Collections
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 512)]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct FixedString512Bytes : global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IIndexable<byte>, global::Unity.Collections.IUTF8Bytes, global::System.IComparable<string>, global::System.IEquatable<string>, global::System.IComparable<global::Unity.Collections.FixedString32Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString32Bytes>, global::System.IComparable<global::Unity.Collections.FixedString64Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString64Bytes>, global::System.IComparable<global::Unity.Collections.FixedString128Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString128Bytes>, global::System.IComparable<global::Unity.Collections.FixedString512Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString512Bytes>, global::System.IComparable<global::Unity.Collections.FixedString4096Bytes>, global::System.IEquatable<global::Unity.Collections.FixedString4096Bytes>
	{
		public struct Enumerator : global::System.Collections.IEnumerator
		{
			private global::Unity.Collections.FixedString512Bytes target;

			private int offset;

			private global::Unity.Collections.Unicode.Rune current;

			public global::Unity.Collections.Unicode.Rune Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return current;
				}
			}

			object global::System.Collections.IEnumerator.Current => Current;

			public Enumerator(global::Unity.Collections.FixedString512Bytes other)
			{
				target = other;
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

		internal const ushort utf8MaxLengthInBytes = 509;

		[global::UnityEngine.SerializeField]
		internal ushort utf8LengthInBytes;

		[global::UnityEngine.SerializeField]
		internal global::Unity.Collections.FixedBytes510 bytes;

		public static int UTF8MaxLengthInBytes => 509;

		[global::Unity.Properties.CreateProperty]
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed string")]
		public string Value => ToString();

		public unsafe int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return utf8LengthInBytes;
			}
			set
			{
				utf8LengthInBytes = (ushort)value;
				GetUnsafePtr()[(int)utf8LengthInBytes] = 0;
			}
		}

		public int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return 509;
			}
			set
			{
			}
		}

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return utf8LengthInBytes == 0;
			}
		}

		public unsafe byte this[int index]
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return GetUnsafePtr()[index];
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				GetUnsafePtr()[index] = value;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe readonly byte* GetUnsafePtr()
		{
			fixed (global::Unity.Collections.FixedBytes510* result = &bytes)
			{
				return (byte*)result;
			}
		}

		public unsafe bool TryResize(int newLength, global::Unity.Collections.NativeArrayOptions clearOptions = global::Unity.Collections.NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 509)
			{
				return false;
			}
			if (newLength == utf8LengthInBytes)
			{
				return true;
			}
			if (clearOptions == global::Unity.Collections.NativeArrayOptions.ClearMemory)
			{
				if (newLength > utf8LengthInBytes)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(GetUnsafePtr() + (int)utf8LengthInBytes, newLength - utf8LengthInBytes);
				}
				else
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(GetUnsafePtr() + newLength, utf8LengthInBytes - newLength);
				}
			}
			utf8LengthInBytes = (ushort)newLength;
			GetUnsafePtr()[(int)utf8LengthInBytes] = 0;
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe ref byte ElementAt(int index)
		{
			return ref GetUnsafePtr()[index];
		}

		public void Clear()
		{
			Length = 0;
		}

		public void Add(in byte value)
		{
			this[Length++] = value;
		}

		public global::Unity.Collections.FixedString512Bytes.Enumerator GetEnumerator()
		{
			return new global::Unity.Collections.FixedString512Bytes.Enumerator(this);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public int CompareTo(string other)
		{
			return ToString().CompareTo(other);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe bool Equals(string other)
		{
			int num = utf8LengthInBytes;
			int length = other.Length;
			byte* utf8Buffer = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in bytes);
			fixed (char* utf16Buffer = other)
			{
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.StrCmp(utf8Buffer, num, utf16Buffer, length) == 0;
			}
		}

		public unsafe ref global::Unity.Collections.FixedList512Bytes<byte> AsFixedList()
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<global::Unity.Collections.FixedList512Bytes<byte>>(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref this));
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public FixedString512Bytes(string source)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			Initialize(source);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		internal global::Unity.Collections.CopyError Initialize(string source)
		{
			return global::Unity.Collections.FixedStringMethods.CopyFromTruncated(ref this, source);
		}

		public FixedString512Bytes(global::Unity.Collections.Unicode.Rune rune, int count = 1)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			Initialize(rune, count);
		}

		internal global::Unity.Collections.FormatError Initialize(global::Unity.Collections.Unicode.Rune rune, int count = 1)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			return global::Unity.Collections.FixedStringMethods.Append(ref this, rune, count);
		}

		internal unsafe global::Unity.Collections.FormatError Initialize(byte* srcBytes, int srcLength)
		{
			bytes = default(global::Unity.Collections.FixedBytes510);
			utf8LengthInBytes = 0;
			int destLength = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.UTF8ArrayUnsafeUtility.AppendUTF8Bytes(GetUnsafePtr(), ref destLength, 509, srcBytes, srcLength);
			if (formatError != global::Unity.Collections.FormatError.None)
			{
				return formatError;
			}
			Length = destLength;
			return global::Unity.Collections.FormatError.None;
		}

		public unsafe FixedString512Bytes(global::Unity.Collections.NativeText.ReadOnly other)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			Initialize(other.GetUnsafePtr(), other.Length);
		}

		public unsafe FixedString512Bytes(in global::Unity.Collections.LowLevel.Unsafe.UnsafeText other)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			Initialize(other.GetUnsafePtr(), other.Length);
		}

		public int CompareTo(global::Unity.Collections.FixedString32Bytes other)
		{
			return global::Unity.Collections.FixedStringMethods.CompareTo(ref this, in other);
		}

		public FixedString512Bytes(in global::Unity.Collections.FixedString32Bytes other)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			Initialize(in other);
		}

		internal unsafe global::Unity.Collections.FormatError Initialize(in global::Unity.Collections.FixedString32Bytes other)
		{
			return Initialize((byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in other.bytes), other.utf8LengthInBytes);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString32Bytes b)
		{
			int aLength = a.utf8LengthInBytes;
			int bLength = b.utf8LengthInBytes;
			byte* aBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in a.bytes);
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		public static bool operator !=(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString32Bytes b)
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

		public FixedString512Bytes(in global::Unity.Collections.FixedString64Bytes other)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			Initialize(in other);
		}

		internal unsafe global::Unity.Collections.FormatError Initialize(in global::Unity.Collections.FixedString64Bytes other)
		{
			return Initialize((byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in other.bytes), other.utf8LengthInBytes);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString64Bytes b)
		{
			int aLength = a.utf8LengthInBytes;
			int bLength = b.utf8LengthInBytes;
			byte* aBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in a.bytes);
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		public static bool operator !=(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString64Bytes b)
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

		public FixedString512Bytes(in global::Unity.Collections.FixedString128Bytes other)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			Initialize(in other);
		}

		internal unsafe global::Unity.Collections.FormatError Initialize(in global::Unity.Collections.FixedString128Bytes other)
		{
			return Initialize((byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in other.bytes), other.utf8LengthInBytes);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString128Bytes b)
		{
			int aLength = a.utf8LengthInBytes;
			int bLength = b.utf8LengthInBytes;
			byte* aBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in a.bytes);
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		public static bool operator !=(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString128Bytes b)
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

		public FixedString512Bytes(in global::Unity.Collections.FixedString512Bytes other)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			Initialize(in other);
		}

		internal unsafe global::Unity.Collections.FormatError Initialize(in global::Unity.Collections.FixedString512Bytes other)
		{
			return Initialize((byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in other.bytes), other.utf8LengthInBytes);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString512Bytes b)
		{
			int aLength = a.utf8LengthInBytes;
			int bLength = b.utf8LengthInBytes;
			byte* aBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in a.bytes);
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		public static bool operator !=(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString512Bytes b)
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

		public FixedString512Bytes(in global::Unity.Collections.FixedString4096Bytes other)
		{
			this = default(global::Unity.Collections.FixedString512Bytes);
			Initialize(in other);
		}

		internal unsafe global::Unity.Collections.FormatError Initialize(in global::Unity.Collections.FixedString4096Bytes other)
		{
			return Initialize((byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in other.bytes), other.utf8LengthInBytes);
		}

		public unsafe static bool operator ==(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString4096Bytes b)
		{
			int aLength = a.utf8LengthInBytes;
			int bLength = b.utf8LengthInBytes;
			byte* aBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in a.bytes);
			byte* bBytes = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AddressOf(in b.bytes);
			return global::Unity.Collections.UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(aBytes, aLength, bBytes, bLength);
		}

		public static bool operator !=(in global::Unity.Collections.FixedString512Bytes a, in global::Unity.Collections.FixedString4096Bytes b)
		{
			return !(a == b);
		}

		public bool Equals(global::Unity.Collections.FixedString4096Bytes other)
		{
			return this == other;
		}

		public static implicit operator global::Unity.Collections.FixedString4096Bytes(in global::Unity.Collections.FixedString512Bytes fs)
		{
			return new global::Unity.Collections.FixedString4096Bytes(in fs);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static implicit operator global::Unity.Collections.FixedString512Bytes(string b)
		{
			return new global::Unity.Collections.FixedString512Bytes(b);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed string")]
		public override string ToString()
		{
			return global::Unity.Collections.FixedStringMethods.ConvertToString(ref this);
		}

		public override int GetHashCode()
		{
			return global::Unity.Collections.FixedStringMethods.ComputeHashCode(ref this);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed object")]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is string other)
			{
				return Equals(other);
			}
			if (obj is global::Unity.Collections.FixedString32Bytes other2)
			{
				return Equals(other2);
			}
			if (obj is global::Unity.Collections.FixedString64Bytes other3)
			{
				return Equals(other3);
			}
			if (obj is global::Unity.Collections.FixedString128Bytes other4)
			{
				return Equals(other4);
			}
			if (obj is global::Unity.Collections.FixedString512Bytes other5)
			{
				return Equals(other5);
			}
			if (obj is global::Unity.Collections.FixedString4096Bytes other6)
			{
				return Equals(other6);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private readonly void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new global::System.IndexOutOfRangeException($"Index {index} must be positive.");
			}
			if (index >= utf8LengthInBytes)
			{
				throw new global::System.IndexOutOfRangeException($"Index {index} is out of range in FixedString512Bytes of '{utf8LengthInBytes}' Length.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new global::System.ArgumentOutOfRangeException($"Length {length} must be positive.");
			}
			if (length > 509)
			{
				throw new global::System.ArgumentOutOfRangeException($"Length {length} is out of range in FixedString512Bytes of '{(ushort)509}' Capacity.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 509)
			{
				throw new global::System.ArgumentOutOfRangeException($"Capacity {capacity} must be lower than {(ushort)509}.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckCopyError(global::Unity.Collections.CopyError error, string source)
		{
			if (error != global::Unity.Collections.CopyError.None)
			{
				throw new global::System.ArgumentException($"FixedString512Bytes: {error} while copying \"{source}\"");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckFormatError(global::Unity.Collections.FormatError error)
		{
			if (error != global::Unity.Collections.FormatError.None)
			{
				throw new global::System.ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}
	}
}
