namespace Unity.Netcode.Components
{
	public struct HalfVector3 : global::Unity.Netcode.INetworkSerializable
	{
		internal const int Length = 3;

		public global::Unity.Mathematics.half3 Axis;

		public global::Unity.Mathematics.bool3 AxisToSynchronize;

		public global::Unity.Mathematics.half X => Axis.x;

		public global::Unity.Mathematics.half Y => Axis.y;

		public global::Unity.Mathematics.half Z => Axis.z;

		internal void Set(float x, float y, float z)
		{
			Axis.x = global::Unity.Mathematics.math.half(x);
			Axis.y = global::Unity.Mathematics.math.half(y);
			Axis.z = global::Unity.Mathematics.math.half(z);
		}

		private void SerializeWrite(global::Unity.Netcode.FastBufferWriter writer)
		{
			for (int i = 0; i < 3; i++)
			{
				if (AxisToSynchronize[i])
				{
					writer.WriteUnmanagedSafe<global::Unity.Mathematics.half>(Axis[i]);
				}
			}
		}

		private void SerializeRead(global::Unity.Netcode.FastBufferReader reader)
		{
			for (int i = 0; i < 3; i++)
			{
				if (AxisToSynchronize[i])
				{
					global::Unity.Mathematics.half value = Axis[i];
					reader.ReadUnmanagedSafe(out value);
					Axis[i] = value;
				}
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
		public global::UnityEngine.Vector3 ToVector3()
		{
			global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
			global::UnityEngine.Vector3 vector = global::Unity.Mathematics.math.float3(Axis);
			for (int i = 0; i < 3; i++)
			{
				if (AxisToSynchronize[i])
				{
					zero[i] = vector[i];
				}
			}
			return zero;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void UpdateFrom(ref global::UnityEngine.Vector3 vector3)
		{
			global::Unity.Mathematics.half3 half5 = global::Unity.Mathematics.math.half3(vector3);
			for (int i = 0; i < 3; i++)
			{
				if (AxisToSynchronize[i])
				{
					Axis[i] = half5[i];
				}
			}
		}

		public HalfVector3(global::UnityEngine.Vector3 vector3, global::Unity.Mathematics.bool3 axisToSynchronize)
		{
			Axis = global::Unity.Mathematics.half3.zero;
			AxisToSynchronize = axisToSynchronize;
			UpdateFrom(ref vector3);
		}

		public HalfVector3(global::UnityEngine.Vector3 vector3)
			: this(vector3, global::Unity.Mathematics.math.bool3(v: true))
		{
		}

		public HalfVector3(float x, float y, float z, global::Unity.Mathematics.bool3 axisToSynchronize)
			: this(new global::UnityEngine.Vector3(x, y, z), axisToSynchronize)
		{
		}

		public HalfVector3(float x, float y, float z)
			: this(new global::UnityEngine.Vector3(x, y, z), global::Unity.Mathematics.math.bool3(v: true))
		{
		}
	}
}
