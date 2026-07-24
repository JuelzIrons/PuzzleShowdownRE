namespace Unity.Netcode.Components
{
	public struct NetworkDeltaPosition : global::Unity.Netcode.INetworkSerializable
	{
		internal const float MaxDeltaBeforeAdjustment = 64f;

		public global::Unity.Netcode.Components.HalfVector3 HalfVector3;

		internal global::UnityEngine.Vector3 CurrentBasePosition;

		internal global::UnityEngine.Vector3 PrecisionLossDelta;

		internal global::UnityEngine.Vector3 HalfDeltaConvertedBack;

		internal global::UnityEngine.Vector3 PreviousPosition;

		internal global::UnityEngine.Vector3 DeltaPosition;

		internal int NetworkTick;

		internal bool SynchronizeBase;

		internal bool CollapsedDeltaIntoBase;

		public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
			if (!SynchronizeBase)
			{
				HalfVector3.NetworkSerialize(serializer);
				return;
			}
			serializer.SerializeValue(ref DeltaPosition);
			serializer.SerializeValue(ref CurrentBasePosition);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 ToVector3(int networkTick)
		{
			if (networkTick == NetworkTick)
			{
				return CurrentBasePosition + DeltaPosition;
			}
			for (int i = 0; i < 3; i++)
			{
				if (HalfVector3.AxisToSynchronize[i])
				{
					DeltaPosition[i] = global::UnityEngine.Mathf.HalfToFloat(HalfVector3.Axis[i].value);
					if (global::UnityEngine.Mathf.Abs(DeltaPosition[i]) >= 64f)
					{
						CurrentBasePosition[i] += DeltaPosition[i];
						DeltaPosition[i] = 0f;
						HalfVector3.Axis[i] = global::Unity.Mathematics.half.zero;
					}
				}
			}
			return CurrentBasePosition + DeltaPosition;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 GetCurrentBasePosition()
		{
			return CurrentBasePosition;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 GetFullPosition()
		{
			return CurrentBasePosition + DeltaPosition;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 GetConvertedDelta()
		{
			return HalfDeltaConvertedBack;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 GetDeltaPosition()
		{
			return DeltaPosition;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void UpdateFrom(ref global::UnityEngine.Vector3 vector3, int networkTick)
		{
			CollapsedDeltaIntoBase = false;
			NetworkTick = networkTick;
			DeltaPosition = vector3 + PrecisionLossDelta - CurrentBasePosition;
			for (int i = 0; i < 3; i++)
			{
				if (HalfVector3.AxisToSynchronize[i])
				{
					HalfVector3.Axis[i] = global::Unity.Mathematics.math.half(DeltaPosition[i]);
					HalfDeltaConvertedBack[i] = global::UnityEngine.Mathf.HalfToFloat(HalfVector3.Axis[i].value);
					PrecisionLossDelta[i] = DeltaPosition[i] - HalfDeltaConvertedBack[i];
					if (global::UnityEngine.Mathf.Abs(HalfDeltaConvertedBack[i]) >= 64f)
					{
						CurrentBasePosition[i] += HalfDeltaConvertedBack[i];
						HalfDeltaConvertedBack[i] = 0f;
						DeltaPosition[i] = 0f;
						CollapsedDeltaIntoBase = true;
					}
				}
			}
			for (int j = 0; j < 3; j++)
			{
				if (HalfVector3.AxisToSynchronize[j])
				{
					PreviousPosition[j] = vector3[j];
				}
			}
		}

		public NetworkDeltaPosition(global::UnityEngine.Vector3 vector3, int networkTick, global::Unity.Mathematics.bool3 axisToSynchronize)
		{
			NetworkTick = networkTick;
			CurrentBasePosition = vector3;
			PreviousPosition = vector3;
			PrecisionLossDelta = global::UnityEngine.Vector3.zero;
			DeltaPosition = global::UnityEngine.Vector3.zero;
			HalfDeltaConvertedBack = global::UnityEngine.Vector3.zero;
			HalfVector3 = new global::Unity.Netcode.Components.HalfVector3(vector3, axisToSynchronize);
			SynchronizeBase = false;
			CollapsedDeltaIntoBase = false;
			UpdateFrom(ref vector3, networkTick);
		}

		public NetworkDeltaPosition(global::UnityEngine.Vector3 vector3, int networkTick)
			: this(vector3, networkTick, global::Unity.Mathematics.math.bool3(v: true))
		{
		}

		public NetworkDeltaPosition(float x, float y, float z, int networkTick, global::Unity.Mathematics.bool3 axisToSynchronize)
			: this(new global::UnityEngine.Vector3(x, y, z), networkTick, axisToSynchronize)
		{
		}

		public NetworkDeltaPosition(float x, float y, float z, int networkTick)
			: this(new global::UnityEngine.Vector3(x, y, z), networkTick, global::Unity.Mathematics.math.bool3(v: true))
		{
		}
	}
}
