namespace Unity.Collections.LowLevel.Unsafe.NotBurstCompatible
{
	public static class Extensions
	{
		public static T[] ToArray<T>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T> set) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.NativeArray<T> nativeArray = set.ToNativeArray(global::Unity.Collections.Allocator.TempJob);
			T[] result = nativeArray.ToArray();
			nativeArray.Dispose();
			return result;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe static void AddNBC(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer buffer, string value)
		{
			if (value != null)
			{
				buffer.Add(value.Length);
				fixed (char* ptr = value)
				{
					buffer.Add(ptr, 2 * value.Length);
				}
			}
			else
			{
				buffer.Add(-1);
			}
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed array")]
		public unsafe static byte[] ToBytesNBC(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer buffer)
		{
			byte[] array = new byte[buffer.Length];
			fixed (byte* destination = array)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, buffer.Ptr, buffer.Length);
			}
			return array;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Managed string out argument")]
		public unsafe static void ReadNextNBC(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer.Reader reader, out string value)
		{
			reader.ReadNext(out int value2);
			if (value2 != -1)
			{
				value = new string('0', value2);
				fixed (char* destination = value)
				{
					int num = value2 * 2;
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, reader.ReadNext(num), num);
				}
			}
			else
			{
				value = null;
			}
		}
	}
}
