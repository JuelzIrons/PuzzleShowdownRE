namespace System.Runtime.CompilerServices
{
	public static class Unsafe
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static T Read<T>(void* source)
		{
			return global::System.Runtime.CompilerServices.Unsafe.Read<T>(source);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static T ReadUnaligned<T>(void* source)
		{
			return global::System.Runtime.CompilerServices.Unsafe.ReadUnaligned<T>(source);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static T ReadUnaligned<T>(ref byte source)
		{
			return global::System.Runtime.CompilerServices.Unsafe.ReadUnaligned<T>(ref source);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void Write<T>(void* destination, T value)
		{
			global::System.Runtime.CompilerServices.Unsafe.Write(destination, value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void WriteUnaligned<T>(void* destination, T value)
		{
			global::System.Runtime.CompilerServices.Unsafe.WriteUnaligned(destination, value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static void WriteUnaligned<T>(ref byte destination, T value)
		{
			global::System.Runtime.CompilerServices.Unsafe.WriteUnaligned(ref destination, value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void Copy<T>(void* destination, ref T source)
		{
			global::System.Runtime.CompilerServices.Unsafe.Write(destination, source);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void Copy<T>(ref T destination, void* source)
		{
			destination = global::System.Runtime.CompilerServices.Unsafe.Read<T>(source);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void* AsPointer<T>(ref T value)
		{
			return global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static void SkipInit<T>(out T value)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static int SizeOf<T>()
		{
			return global::System.Runtime.CompilerServices.Unsafe.SizeOf<T>();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void CopyBlock(void* destination, void* source, uint byteCount)
		{
			// IL cpblk instruction
			global::System.Runtime.CompilerServices.Unsafe.CopyBlock(destination, source, byteCount);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static void CopyBlock(ref byte destination, ref byte source, uint byteCount)
		{
			// IL cpblk instruction
			global::System.Runtime.CompilerServices.Unsafe.CopyBlock(ref destination, ref source, byteCount);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void CopyBlockUnaligned(void* destination, void* source, uint byteCount)
		{
			// IL cpblk instruction
			global::System.Runtime.CompilerServices.Unsafe.CopyBlockUnaligned(destination, source, byteCount);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static void CopyBlockUnaligned(ref byte destination, ref byte source, uint byteCount)
		{
			// IL cpblk instruction
			global::System.Runtime.CompilerServices.Unsafe.CopyBlockUnaligned(ref destination, ref source, byteCount);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void InitBlock(void* startAddress, byte value, uint byteCount)
		{
			// IL initblk instruction
			global::System.Runtime.CompilerServices.Unsafe.InitBlock(startAddress, value, byteCount);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static void InitBlock(ref byte startAddress, byte value, uint byteCount)
		{
			// IL initblk instruction
			global::System.Runtime.CompilerServices.Unsafe.InitBlock(ref startAddress, value, byteCount);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
		{
			// IL initblk instruction
			global::System.Runtime.CompilerServices.Unsafe.InitBlockUnaligned(startAddress, value, byteCount);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
		{
			// IL initblk instruction
			global::System.Runtime.CompilerServices.Unsafe.InitBlockUnaligned(ref startAddress, value, byteCount);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static T As<T>(object o) where T : class
		{
			return (T)o;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static ref T AsRef<T>(void* source)
		{
			return ref *(T*)source;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static ref T AsRef<T>(in T source)
		{
			return ref source;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static ref TTo As<TFrom, TTo>(ref TFrom source)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.As<TFrom, TTo>(ref source);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static ref T Unbox<T>(object box) where T : struct
		{
			return ref (T)box;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static ref T Add<T>(ref T source, int elementOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.Add(ref source, elementOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void* Add<T>(void* source, int elementOffset)
		{
			return (byte*)source + (nint)elementOffset * (nint)global::System.Runtime.CompilerServices.Unsafe.SizeOf<T>();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static ref T Add<T>(ref T source, global::System.IntPtr elementOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.Add(ref source, elementOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ref T Add<T>(ref T source, [global::System.Runtime.Versioning.NonVersionable] nuint elementOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.Add(ref source, elementOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static ref T AddByteOffset<T>(ref T source, global::System.IntPtr byteOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref source, byteOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ref T AddByteOffset<T>(ref T source, [global::System.Runtime.Versioning.NonVersionable] nuint byteOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref source, byteOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static ref T Subtract<T>(ref T source, int elementOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.Subtract(ref source, elementOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static void* Subtract<T>(void* source, int elementOffset)
		{
			return (byte*)source - (nint)elementOffset * (nint)global::System.Runtime.CompilerServices.Unsafe.SizeOf<T>();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static ref T Subtract<T>(ref T source, global::System.IntPtr elementOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.Subtract(ref source, elementOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ref T Subtract<T>(ref T source, [global::System.Runtime.Versioning.NonVersionable] nuint elementOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.Subtract(ref source, elementOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static ref T SubtractByteOffset<T>(ref T source, global::System.IntPtr byteOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref source, byteOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ref T SubtractByteOffset<T>(ref T source, [global::System.Runtime.Versioning.NonVersionable] nuint byteOffset)
		{
			return ref global::System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref source, byteOffset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static global::System.IntPtr ByteOffset<T>(ref T origin, ref T target)
		{
			return global::System.Runtime.CompilerServices.Unsafe.ByteOffset(target: ref target, origin: ref origin);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static bool AreSame<T>(ref T left, ref T right)
		{
			return global::System.Runtime.CompilerServices.Unsafe.AreSame(ref left, ref right);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static bool IsAddressGreaterThan<T>(ref T left, ref T right)
		{
			return global::System.Runtime.CompilerServices.Unsafe.IsAddressGreaterThan(ref left, ref right);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public static bool IsAddressLessThan<T>(ref T left, ref T right)
		{
			return global::System.Runtime.CompilerServices.Unsafe.IsAddressLessThan(ref left, ref right);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static bool IsNullRef<T>(ref T source)
		{
			return global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref source) == null;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Runtime.Versioning.NonVersionable]
		public unsafe static ref T NullRef<T>()
		{
			return ref *(T*)null;
		}
	}
}
