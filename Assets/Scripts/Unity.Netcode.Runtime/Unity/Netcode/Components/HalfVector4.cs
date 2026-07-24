namespace Unity.Netcode.Components
{
	public struct HalfVector4 : global::Unity.Netcode.INetworkSerializable
	{
		internal const int Length = 4;

		public global::Unity.Mathematics.half4 Axis;

		public global::Unity.Mathematics.half X => Axis.x;

		public global::Unity.Mathematics.half Y => Axis.y;

		public global::Unity.Mathematics.half Z => Axis.z;

		public global::Unity.Mathematics.half W => Axis.w;

		private void SerializeWrite(global::Unity.Netcode.FastBufferWriter writer)
		{
			for (int i = 0; i < 4; i++)
			{
				writer.WriteUnmanagedSafe<global::Unity.Mathematics.half>(Axis[i]);
			}
		}

		private void SerializeRead(global::Unity.Netcode.FastBufferReader reader)
		{
			for (int i = 0; i < 4; i++)
			{
				global::Unity.Mathematics.half value = Axis[i];
				reader.ReadUnmanagedSafe(out value);
				Axis[i] = value;
			}
		}

		public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
			if (serializer.IsReader)
			{
				SerializeRead(serializer.GetFastBufferReader());
			}
			else
			{
				SerializeWrite(serializer.GetFastBufferWriter());
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector4 ToVector4()
		{
			return global::Unity.Mathematics.math.float4(Axis);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Quaternion ToQuaternion()
		{
			return global::Unity.Mathematics.math.quaternion(Axis);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void UpdateFrom(ref global::UnityEngine.Vector4 vector4)
		{
			Axis = global::Unity.Mathematics.math.half4(vector4);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void UpdateFrom(ref global::UnityEngine.Quaternion quaternion)
		{
			Axis = global::Unity.Mathematics.math.half4(global::Unity.Mathematics.math.half(quaternion.x), global::Unity.Mathematics.math.half(quaternion.y), global::Unity.Mathematics.math.half(quaternion.z), global::Unity.Mathematics.math.half(quaternion.w));
		}

		public HalfVector4(global::UnityEngine.Vector4 vector4)
		{
			Axis = default(global::Unity.Mathematics.half4);
			UpdateFrom(ref vector4);
		}

		public HalfVector4(float x, float y, float z, float w)
			: this(new global::UnityEngine.Vector4(x, y, z, w))
		{
		}
	}
}
