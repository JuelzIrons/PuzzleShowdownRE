namespace Unity.Netcode
{
	internal class FixedStringSerializer<T> : global::Unity.Netcode.INetworkVariableSerializer<T> where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref T value)
		{
			writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			reader.ReadValueSafeInPlace(ref value);
		}

		public unsafe void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref T value, ref T previousValue)
		{
			using global::Unity.Netcode.ResizableBitVector value2 = new global::Unity.Netcode.ResizableBitVector(global::Unity.Collections.Allocator.Temp);
			int num = global::Unity.Mathematics.math.min(value.Length, previousValue.Length);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				byte num3 = value[i];
				byte b = previousValue[i];
				if (num3 != b)
				{
					num2++;
					value2.Set(i);
				}
			}
			for (int j = previousValue.Length; j < value.Length; j++)
			{
				num2++;
				value2.Set(j);
			}
			if (value2.GetSerializedSize() + global::Unity.Netcode.FastBufferWriter.GetWriteSize<byte>() * num2 > global::Unity.Netcode.FastBufferWriter.GetWriteSize<byte>() * value.Length)
			{
				writer.WriteByteSafe(1);
				writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
				return;
			}
			writer.WriteByteSafe(0);
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, value.Length);
			writer.WriteValueSafe(in value2, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			byte* unsafePtr = value.GetUnsafePtr();
			for (int k = 0; k < value.Length; k++)
			{
				if (value2.IsSet(k))
				{
					writer.WriteByteSafe(unsafePtr[k]);
				}
			}
		}

		public unsafe void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			reader.ReadByteSafe(out var value2);
			if (value2 == 1)
			{
				reader.ReadValueSafeInPlace(ref value);
				return;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out int value3);
			global::Unity.Netcode.ResizableBitVector value4 = new global::Unity.Netcode.ResizableBitVector(global::Unity.Collections.Allocator.Temp);
			using (value4)
			{
				reader.ReadNetworkSerializableInPlace(ref value4);
				value.Length = value3;
				byte* unsafePtr = value.GetUnsafePtr();
				for (int i = 0; i < value.Length; i++)
				{
					if (value4.IsSet(i))
					{
						reader.ReadByteSafe(out unsafePtr[i]);
					}
				}
			}
		}

		void global::Unity.Netcode.INetworkVariableSerializer<T>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out T value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in T value, ref T duplicatedValue)
		{
			duplicatedValue = value;
		}

		void global::Unity.Netcode.INetworkVariableSerializer<T>.Duplicate(in T value, ref T duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
