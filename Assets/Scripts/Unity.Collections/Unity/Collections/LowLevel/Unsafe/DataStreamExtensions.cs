namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class DataStreamExtensions
	{
		public unsafe static global::Unity.Collections.DataStreamWriter Create(byte* data, int length)
		{
			return new global::Unity.Collections.DataStreamWriter(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(data, length, global::Unity.Collections.Allocator.None));
		}

		public unsafe static bool WriteBytesUnsafe(this ref global::Unity.Collections.DataStreamWriter writer, byte* data, int bytes)
		{
			global::Unity.Collections.NativeArray<byte> value = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(data, bytes, global::Unity.Collections.Allocator.None);
			return writer.WriteBytes(value);
		}

		public unsafe static void ReadBytesUnsafe(this ref global::Unity.Collections.DataStreamReader reader, byte* data, int length)
		{
			global::Unity.Collections.NativeArray<byte> array = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(data, length, global::Unity.Collections.Allocator.None);
			reader.ReadBytes(array);
		}

		public unsafe static ushort ReadFixedStringUnsafe(this ref global::Unity.Collections.DataStreamReader reader, byte* data, int maxLength)
		{
			global::Unity.Collections.NativeArray<byte> array = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(data, maxLength, global::Unity.Collections.Allocator.Temp);
			return reader.ReadFixedString(array);
		}

		public unsafe static ushort ReadPackedFixedStringDeltaUnsafe(this ref global::Unity.Collections.DataStreamReader reader, byte* data, int maxLength, byte* baseData, ushort baseLength, global::Unity.Collections.StreamCompressionModel model)
		{
			global::Unity.Collections.NativeArray<byte> data2 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(data, maxLength, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<byte> baseData2 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(baseData, baseLength, global::Unity.Collections.Allocator.Temp);
			return reader.ReadPackedFixedStringDelta(data2, baseData2, in model);
		}

		public unsafe static void* GetUnsafeReadOnlyPtr(this ref global::Unity.Collections.DataStreamReader reader)
		{
			return reader.m_BufferPtr;
		}
	}
}
