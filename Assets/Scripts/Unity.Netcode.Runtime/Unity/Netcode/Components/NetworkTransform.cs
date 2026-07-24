namespace Unity.Netcode.Components
{
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.AddComponentMenu("Netcode/Network Transform")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/manual/components/helper/networktransform.html")]
	public class NetworkTransform : global::Unity.Netcode.NetworkBehaviour
	{
		internal enum Axis
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		internal enum AxialType
		{
			Position = 0,
			Rotation = 1,
			Scale = 2
		}

		internal struct FlagStates
		{
			private const int k_InLocalSpaceBit = 1;

			private const int k_PositionXBit = 2;

			private const int k_PositionYBit = 4;

			private const int k_PositionZBit = 8;

			private const int k_RotAngleXBit = 16;

			private const int k_RotAngleYBit = 32;

			private const int k_RotAngleZBit = 64;

			private const int k_ScaleXBit = 128;

			private const int k_ScaleYBit = 256;

			private const int k_ScaleZBit = 512;

			private const int k_TeleportingBit = 1024;

			private const int k_Interpolate = 2048;

			private const int k_QuaternionSync = 4096;

			private const int k_QuaternionCompress = 8192;

			private const int k_UseHalfFloats = 16384;

			private const int k_Synchronization = 32768;

			private const int k_PositionSlerp = 65536;

			private const int k_IsParented = 131072;

			private const int k_SynchBaseHalfFloat = 262144;

			private const int k_ReliableSequenced = 524288;

			private const int k_UseUnreliableDeltas = 1048576;

			private const int k_UnreliableFrameSync = 2097152;

			private const int k_SwitchTransformSpaceWhenParented = 4194304;

			private const int k_TrackStateId = 268435456;

			internal bool InLocalSpace;

			internal bool HasPositionX;

			internal bool HasPositionY;

			internal bool HasPositionZ;

			internal bool HasPositionChange;

			internal bool HasRotAngleX;

			internal bool HasRotAngleY;

			internal bool HasRotAngleZ;

			internal bool HasRotAngleChange;

			internal bool HasScaleX;

			internal bool HasScaleY;

			internal bool HasScaleZ;

			internal bool HasScaleChange;

			internal bool IsTeleportingNextFrame;

			internal bool WasTeleported;

			internal bool UseInterpolation;

			internal bool QuaternionSync;

			internal bool QuaternionCompression;

			internal bool UseHalfFloatPrecision;

			internal bool IsSynchronizing;

			internal bool UsePositionSlerp;

			internal bool IsParented;

			internal bool SynchronizeBaseHalfFloat;

			internal bool ReliableSequenced;

			internal bool UseUnreliableDeltas;

			internal bool UnreliableFrameSync;

			internal bool SwitchTransformSpaceWhenParented;

			internal bool TrackByStateId;

			internal bool IsDirty;

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal void MarkChanged(global::Unity.Netcode.Components.NetworkTransform.AxialType axialType, bool changed)
			{
				switch (axialType)
				{
				case global::Unity.Netcode.Components.NetworkTransform.AxialType.Position:
					HasPositionX = changed;
					HasPositionY = changed;
					HasPositionZ = changed;
					HasPositionChange = changed;
					break;
				case global::Unity.Netcode.Components.NetworkTransform.AxialType.Rotation:
					HasRotAngleX = changed;
					HasRotAngleY = changed;
					HasRotAngleZ = changed;
					HasRotAngleChange = changed;
					break;
				case global::Unity.Netcode.Components.NetworkTransform.AxialType.Scale:
					HasScaleX = changed;
					HasScaleY = changed;
					HasScaleZ = changed;
					HasScaleChange = changed;
					break;
				}
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal void SetHasPosition(global::Unity.Netcode.Components.NetworkTransform.Axis axis, bool changed)
			{
				switch (axis)
				{
				case global::Unity.Netcode.Components.NetworkTransform.Axis.X:
					HasPositionX = changed;
					break;
				case global::Unity.Netcode.Components.NetworkTransform.Axis.Y:
					HasPositionY = changed;
					break;
				case global::Unity.Netcode.Components.NetworkTransform.Axis.Z:
					HasPositionZ = changed;
					break;
				}
				HasPositionChange = HasPositionX || HasPositionY || HasPositionZ;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal void SetHasRotation(global::Unity.Netcode.Components.NetworkTransform.Axis axis, bool changed)
			{
				switch (axis)
				{
				case global::Unity.Netcode.Components.NetworkTransform.Axis.X:
					HasRotAngleX = changed;
					break;
				case global::Unity.Netcode.Components.NetworkTransform.Axis.Y:
					HasRotAngleY = changed;
					break;
				case global::Unity.Netcode.Components.NetworkTransform.Axis.Z:
					HasRotAngleZ = changed;
					break;
				}
				HasRotAngleChange = HasRotAngleX || HasRotAngleY || HasRotAngleZ;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal void SetHasScale(global::Unity.Netcode.Components.NetworkTransform.Axis axis, bool changed)
			{
				switch (axis)
				{
				case global::Unity.Netcode.Components.NetworkTransform.Axis.X:
					HasScaleX = changed;
					break;
				case global::Unity.Netcode.Components.NetworkTransform.Axis.Y:
					HasScaleY = changed;
					break;
				case global::Unity.Netcode.Components.NetworkTransform.Axis.Z:
					HasScaleZ = changed;
					break;
				}
				HasScaleChange = HasScaleX || HasScaleY || HasScaleZ;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal bool HasScale(global::Unity.Netcode.Components.NetworkTransform.Axis axis)
			{
				return axis switch
				{
					global::Unity.Netcode.Components.NetworkTransform.Axis.Y => HasScaleY, 
					global::Unity.Netcode.Components.NetworkTransform.Axis.X => HasScaleX, 
					_ => HasScaleZ, 
				};
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal uint GetBitsetRepresentation()
			{
				uint num = 0u;
				if (InLocalSpace)
				{
					num |= 1;
				}
				if (HasPositionX)
				{
					num |= 2;
				}
				if (HasPositionY)
				{
					num |= 4;
				}
				if (HasPositionZ)
				{
					num |= 8;
				}
				if (HasRotAngleX)
				{
					num |= 0x10;
				}
				if (HasRotAngleY)
				{
					num |= 0x20;
				}
				if (HasRotAngleZ)
				{
					num |= 0x40;
				}
				if (HasScaleX)
				{
					num |= 0x80;
				}
				if (HasScaleY)
				{
					num |= 0x100;
				}
				if (HasScaleZ)
				{
					num |= 0x200;
				}
				if (IsTeleportingNextFrame)
				{
					num |= 0x400;
				}
				if (UseInterpolation)
				{
					num |= 0x800;
				}
				if (QuaternionSync)
				{
					num |= 0x1000;
				}
				if (QuaternionCompression)
				{
					num |= 0x2000;
				}
				if (UseHalfFloatPrecision)
				{
					num |= 0x4000;
				}
				if (IsSynchronizing)
				{
					num |= 0x8000;
				}
				if (UsePositionSlerp)
				{
					num |= 0x10000;
				}
				if (IsParented)
				{
					num |= 0x20000;
				}
				if (SynchronizeBaseHalfFloat)
				{
					num |= 0x40000;
				}
				if (ReliableSequenced)
				{
					num |= 0x80000;
				}
				if (UseUnreliableDeltas)
				{
					num |= 0x100000;
				}
				if (UnreliableFrameSync)
				{
					num |= 0x200000;
				}
				if (SwitchTransformSpaceWhenParented)
				{
					num |= 0x400000;
				}
				if (TrackByStateId)
				{
					num |= 0x10000000;
				}
				return num;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			internal void SetStateFromBitset(uint bitset)
			{
				InLocalSpace = (bitset & 1) != 0;
				SetHasPosition(global::Unity.Netcode.Components.NetworkTransform.Axis.X, (bitset & 2) != 0);
				SetHasPosition(global::Unity.Netcode.Components.NetworkTransform.Axis.Y, (bitset & 4) != 0);
				SetHasPosition(global::Unity.Netcode.Components.NetworkTransform.Axis.Z, (bitset & 8) != 0);
				SetHasRotation(global::Unity.Netcode.Components.NetworkTransform.Axis.X, (bitset & 0x10) != 0);
				SetHasRotation(global::Unity.Netcode.Components.NetworkTransform.Axis.Y, (bitset & 0x20) != 0);
				SetHasRotation(global::Unity.Netcode.Components.NetworkTransform.Axis.Z, (bitset & 0x40) != 0);
				SetHasScale(global::Unity.Netcode.Components.NetworkTransform.Axis.X, (bitset & 0x80) != 0);
				SetHasScale(global::Unity.Netcode.Components.NetworkTransform.Axis.Y, (bitset & 0x100) != 0);
				SetHasScale(global::Unity.Netcode.Components.NetworkTransform.Axis.Z, (bitset & 0x200) != 0);
				IsTeleportingNextFrame = (bitset & 0x400) != 0;
				UseInterpolation = (bitset & 0x800) != 0;
				QuaternionSync = (bitset & 0x1000) != 0;
				QuaternionCompression = (bitset & 0x2000) != 0;
				UseHalfFloatPrecision = (bitset & 0x4000) != 0;
				IsSynchronizing = (bitset & 0x8000) != 0;
				UsePositionSlerp = (bitset & 0x10000) != 0;
				IsParented = (bitset & 0x20000) != 0;
				SynchronizeBaseHalfFloat = (bitset & 0x40000) != 0;
				ReliableSequenced = (bitset & 0x80000) != 0;
				UseUnreliableDeltas = (bitset & 0x100000) != 0;
				UnreliableFrameSync = (bitset & 0x200000) != 0;
				SwitchTransformSpaceWhenParented = (bitset & 0x400000) != 0;
				TrackByStateId = (bitset & 0x10000000) != 0;
			}

			internal void ClearForNextTick()
			{
				HasPositionX = false;
				HasPositionY = false;
				HasPositionZ = false;
				HasPositionChange = false;
				HasRotAngleX = false;
				HasRotAngleY = false;
				HasRotAngleZ = false;
				HasRotAngleChange = false;
				HasScaleX = false;
				HasScaleY = false;
				HasScaleZ = false;
				HasScaleChange = false;
				IsTeleportingNextFrame = false;
				IsSynchronizing = false;
				IsParented = false;
				SynchronizeBaseHalfFloat = false;
				ReliableSequenced = false;
				UnreliableFrameSync = false;
				TrackByStateId = false;
				IsDirty = false;
			}
		}

		public struct NetworkTransformState : global::Unity.Netcode.INetworkSerializable
		{
			internal double SentTime;

			internal float PositionX;

			internal float PositionY;

			internal float PositionZ;

			internal float RotAngleX;

			internal float RotAngleY;

			internal float RotAngleZ;

			internal global::UnityEngine.Quaternion Rotation;

			internal float ScaleX;

			internal float ScaleY;

			internal float ScaleZ;

			internal global::UnityEngine.Vector3 CurrentPosition;

			internal global::UnityEngine.Vector3 DeltaPosition;

			internal global::Unity.Netcode.Components.NetworkDeltaPosition NetworkDeltaPosition;

			internal global::Unity.Netcode.Components.HalfVector3 HalfVectorScale;

			internal global::UnityEngine.Vector3 Scale;

			internal global::UnityEngine.Vector3 LossyScale;

			internal global::Unity.Netcode.Components.HalfVector4 HalfVectorRotation;

			internal uint QuaternionCompressed;

			internal int NetworkTick;

			internal int StateId;

			internal bool ExplicitSet;

			private global::Unity.Netcode.FastBufferReader m_Reader;

			private global::Unity.Netcode.FastBufferWriter m_Writer;

			internal global::Unity.Netcode.Components.NetworkTransform.FlagStates FlagStates;

			internal global::Unity.Netcode.Components.HalfVector3 HalfEulerRotation;

			public int LastSerializedSize { get; internal set; }

			public bool InLocalSpace
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.InLocalSpace;
				}
			}

			public bool HasPositionX
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasPositionX;
				}
			}

			public bool HasPositionY
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasPositionY;
				}
			}

			public bool HasPositionZ
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasPositionZ;
				}
			}

			public bool HasPositionChange
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasPositionChange;
				}
			}

			public bool HasRotAngleX
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasRotAngleX;
				}
			}

			public bool HasRotAngleY
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasRotAngleY;
				}
			}

			public bool HasRotAngleZ
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasRotAngleZ;
				}
			}

			public bool HasRotAngleChange
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasRotAngleChange;
				}
			}

			public bool HasScaleX
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasScaleX;
				}
			}

			public bool HasScaleY
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasScaleY;
				}
			}

			public bool HasScaleZ
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasScaleZ;
				}
			}

			public bool HasScaleChange
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.HasScaleChange;
				}
			}

			public bool IsTeleportingNextFrame
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.IsTeleportingNextFrame;
				}
			}

			public bool WasTeleported
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.WasTeleported;
				}
			}

			public bool UseInterpolation
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.UseInterpolation;
				}
			}

			public bool QuaternionSync
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.QuaternionSync;
				}
			}

			public bool QuaternionCompression
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.QuaternionCompression;
				}
			}

			public bool UseHalfFloatPrecision
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.UseHalfFloatPrecision;
				}
			}

			public bool IsSynchronizing
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.IsSynchronizing;
				}
			}

			public bool UsePositionSlerp
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return FlagStates.UsePositionSlerp;
				}
			}

			public bool IsUnreliableFrameSync()
			{
				return FlagStates.UnreliableFrameSync;
			}

			public bool IsReliableStateUpdate()
			{
				return FlagStates.ReliableSequenced;
			}

			public global::UnityEngine.Quaternion GetRotation()
			{
				if (HasRotAngleChange)
				{
					if (QuaternionSync)
					{
						return Rotation;
					}
					return global::UnityEngine.Quaternion.Euler(RotAngleX, RotAngleY, RotAngleZ);
				}
				return global::UnityEngine.Quaternion.identity;
			}

			public global::UnityEngine.Vector3 GetPosition()
			{
				if (HasPositionChange)
				{
					if (UseHalfFloatPrecision)
					{
						if (IsTeleportingNextFrame)
						{
							return CurrentPosition;
						}
						return NetworkDeltaPosition.GetFullPosition();
					}
					return new global::UnityEngine.Vector3(PositionX, PositionY, PositionZ);
				}
				return global::UnityEngine.Vector3.zero;
			}

			public global::UnityEngine.Vector3 GetScale()
			{
				if (HasScaleChange)
				{
					if (UseHalfFloatPrecision)
					{
						if (IsTeleportingNextFrame)
						{
							return Scale;
						}
						return HalfVectorScale.ToVector3();
					}
					return new global::UnityEngine.Vector3(ScaleX, ScaleY, ScaleZ);
				}
				return global::UnityEngine.Vector3.zero;
			}

			public int GetNetworkTick()
			{
				return NetworkTick;
			}

			public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
			{
				int num = 0;
				bool isWriter = serializer.IsWriter;
				if (isWriter)
				{
					m_Writer = serializer.GetFastBufferWriter();
					num = m_Writer.Position;
				}
				else
				{
					m_Reader = serializer.GetFastBufferReader();
					num = m_Reader.Position;
				}
				if (isWriter)
				{
					if (FlagStates.UseUnreliableDeltas)
					{
						if (FlagStates.IsTeleportingNextFrame || FlagStates.IsSynchronizing || FlagStates.UnreliableFrameSync || (FlagStates.UseHalfFloatPrecision && NetworkDeltaPosition.CollapsedDeltaIntoBase))
						{
							FlagStates.ReliableSequenced = true;
						}
						else
						{
							FlagStates.ReliableSequenced = false;
						}
					}
					else
					{
						FlagStates.ReliableSequenced = true;
					}
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(m_Writer, FlagStates.GetBitsetRepresentation());
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(m_Writer, NetworkTick);
				}
				else
				{
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(m_Reader, out uint value);
					FlagStates.SetStateFromBitset(value);
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(m_Reader, out NetworkTick);
				}
				if (FlagStates.TrackByStateId)
				{
					serializer.SerializeValue(ref StateId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				if (HasPositionChange)
				{
					if (UseHalfFloatPrecision)
					{
						NetworkDeltaPosition.SynchronizeBase = FlagStates.SynchronizeBaseHalfFloat;
						NetworkDeltaPosition.HalfVector3.AxisToSynchronize[0] = HasPositionX;
						NetworkDeltaPosition.HalfVector3.AxisToSynchronize[1] = HasPositionY;
						NetworkDeltaPosition.HalfVector3.AxisToSynchronize[2] = HasPositionZ;
						if (IsTeleportingNextFrame)
						{
							serializer.SerializeValue(ref CurrentPosition);
							if (IsSynchronizing)
							{
								serializer.SerializeValue(ref DeltaPosition);
								if (!isWriter)
								{
									NetworkDeltaPosition.NetworkTick = NetworkTick;
									NetworkDeltaPosition.NetworkSerialize(serializer);
								}
								else
								{
									serializer.SerializeNetworkSerializable(ref NetworkDeltaPosition);
								}
							}
						}
						else if (!isWriter)
						{
							NetworkDeltaPosition.NetworkTick = NetworkTick;
							NetworkDeltaPosition.NetworkSerialize(serializer);
						}
						else
						{
							serializer.SerializeNetworkSerializable(ref NetworkDeltaPosition);
						}
					}
					else
					{
						if (HasPositionX)
						{
							serializer.SerializeValue(ref PositionX, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
						if (HasPositionY)
						{
							serializer.SerializeValue(ref PositionY, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
						if (HasPositionZ)
						{
							serializer.SerializeValue(ref PositionZ, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
					}
				}
				if (HasRotAngleChange)
				{
					if (QuaternionSync)
					{
						if (IsTeleportingNextFrame)
						{
							serializer.SerializeValue(ref Rotation);
						}
						else if (QuaternionCompression)
						{
							if (isWriter)
							{
								QuaternionCompressed = global::Unity.Netcode.QuaternionCompressor.CompressQuaternion(ref Rotation);
							}
							serializer.SerializeValue(ref QuaternionCompressed, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
							if (!isWriter)
							{
								global::Unity.Netcode.QuaternionCompressor.DecompressQuaternion(ref Rotation, QuaternionCompressed);
							}
						}
						else if (UseHalfFloatPrecision)
						{
							if (isWriter)
							{
								HalfVectorRotation.UpdateFrom(ref Rotation);
							}
							serializer.SerializeNetworkSerializable(ref HalfVectorRotation);
							if (!isWriter)
							{
								Rotation = HalfVectorRotation.ToQuaternion();
							}
						}
						else
						{
							serializer.SerializeValue(ref Rotation);
						}
					}
					else if (UseHalfFloatPrecision && !IsTeleportingNextFrame)
					{
						if (HasRotAngleChange)
						{
							HalfEulerRotation.AxisToSynchronize[0] = HasRotAngleX;
							HalfEulerRotation.AxisToSynchronize[1] = HasRotAngleY;
							HalfEulerRotation.AxisToSynchronize[2] = HasRotAngleZ;
							if (isWriter)
							{
								HalfEulerRotation.Set(RotAngleX, RotAngleY, RotAngleZ);
							}
							serializer.SerializeValue(ref HalfEulerRotation, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
							if (!isWriter)
							{
								global::UnityEngine.Vector3 vector = HalfEulerRotation.ToVector3();
								if (HasRotAngleX)
								{
									RotAngleX = vector.x;
								}
								if (HasRotAngleY)
								{
									RotAngleY = vector.y;
								}
								if (HasRotAngleZ)
								{
									RotAngleZ = vector.z;
								}
							}
						}
					}
					else
					{
						if (HasRotAngleX)
						{
							serializer.SerializeValue(ref RotAngleX, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
						if (HasRotAngleY)
						{
							serializer.SerializeValue(ref RotAngleY, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
						if (HasRotAngleZ)
						{
							serializer.SerializeValue(ref RotAngleZ, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
					}
				}
				if (HasScaleChange)
				{
					if (IsTeleportingNextFrame && FlagStates.IsParented)
					{
						serializer.SerializeValue(ref LossyScale);
					}
					if (UseHalfFloatPrecision)
					{
						if (IsTeleportingNextFrame)
						{
							serializer.SerializeValue(ref Scale);
						}
						else
						{
							HalfVectorScale.AxisToSynchronize[0] = HasScaleX;
							HalfVectorScale.AxisToSynchronize[1] = HasScaleY;
							HalfVectorScale.AxisToSynchronize[2] = HasScaleZ;
							if (isWriter)
							{
								HalfVectorScale.Set(Scale[0], Scale[1], Scale[2]);
							}
							serializer.SerializeValue(ref HalfVectorScale, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
							if (!isWriter)
							{
								Scale = HalfVectorScale.ToVector3();
								if (HasScaleX)
								{
									ScaleX = Scale.x;
								}
								if (HasScaleY)
								{
									ScaleY = Scale.y;
								}
								if (HasScaleZ)
								{
									ScaleZ = Scale.x;
								}
							}
						}
					}
					else
					{
						if (HasScaleX)
						{
							serializer.SerializeValue(ref ScaleX, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
						if (HasScaleY)
						{
							serializer.SerializeValue(ref ScaleY, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
						if (HasScaleZ)
						{
							serializer.SerializeValue(ref ScaleZ, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
					}
				}
				if (!isWriter)
				{
					FlagStates.IsDirty = HasPositionChange || HasRotAngleChange || HasScaleChange;
					LastSerializedSize = m_Reader.Position - num;
				}
				else
				{
					LastSerializedSize = m_Writer.Position - num;
				}
			}
		}

		public enum InterpolationTypes
		{
			LegacyLerp = 0,
			Lerp = 1,
			SmoothDampening = 2
		}

		public enum AuthorityModes
		{
			Server = 0,
			Owner = 1
		}

		public delegate(global::UnityEngine.Vector3 pos, global::UnityEngine.Quaternion rotOut, global::UnityEngine.Vector3 scale) OnClientRequestChangeDelegate(global::UnityEngine.Vector3 pos, global::UnityEngine.Quaternion rot, global::UnityEngine.Vector3 scale);

		internal class NetworkTransformTickRegistration
		{
			private global::Unity.Netcode.NetworkManager m_NetworkManager;

			public global::System.Collections.Generic.HashSet<global::Unity.Netcode.Components.NetworkTransform> NetworkTransforms = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.Components.NetworkTransform>();

			private int m_LastTick;

			private void OnNetworkManagerStopped(bool value)
			{
				Remove();
			}

			public void Remove()
			{
				NetworkTransforms.Clear();
				RemoveTickUpdate(m_NetworkManager);
			}

			internal void TickUpdate()
			{
				if (CurrentTick <= m_LastTick)
				{
					return;
				}
				if (m_NetworkManager == null || m_NetworkManager.ShutdownInProgress || !m_NetworkManager.IsListening)
				{
					Remove();
					return;
				}
				foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform in NetworkTransforms)
				{
					if (networkTransform.IsSpawned)
					{
						networkTransform.OnNetworkTick();
					}
				}
				m_LastTick = CurrentTick;
			}

			public NetworkTransformTickRegistration(global::Unity.Netcode.NetworkManager networkManager)
			{
				m_NetworkManager = networkManager;
				if (networkManager.IsServer)
				{
					networkManager.OnServerStopped += OnNetworkManagerStopped;
				}
				else
				{
					networkManager.OnClientStopped += OnNetworkManagerStopped;
				}
			}
		}

		internal static int CurrentTick;

		public bool AutoOwnerAuthorityTickOffset = true;

		[global::UnityEngine.Tooltip("Lerping yields a traditional linear result where smooth dampening will adjust based on the rate of change. You can mix interpolation types for position, rotation, and scale.")]
		public global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes PositionInterpolationType;

		private global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes m_PreviousPositionInterpolationType;

		[global::UnityEngine.Tooltip("Lerping yields a traditional linear result where smooth dampening will adjust based on the rate of change. You can mix interpolation types for position, rotation, and scale.")]
		public global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes RotationInterpolationType;

		private global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes m_PreviousRotationInterpolationType;

		[global::UnityEngine.Tooltip("Lerping yields a traditional linear result where smooth dampening will adjust based on the rate of change. You can mix interpolation types for position, rotation, and scale.")]
		public global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes ScaleInterpolationType;

		private global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes m_PreviousScaleInterpolationType;

		public bool PositionLerpSmoothing = true;

		private bool m_PreviousPositionLerpSmoothing;

		[global::UnityEngine.Tooltip("The higher the value the smoother, but can result in lost data points (i.e. quick changes in direct). The lower the value the more accurate/precise, but can result in slight stutter (i.e. due to jitter, latency, or a high threshold value).")]
		[global::UnityEngine.Range(0.01f, 1f)]
		public float PositionMaxInterpolationTime = 0.1f;

		public bool RotationLerpSmoothing = true;

		private bool m_PreviousRotationLerpSmoothing;

		[global::UnityEngine.Tooltip("The higher the value the smoother, but can result in lost data points (i.e. quick changes in direct). The lower the value the more accurate/precise, but can result in slight stutter (i.e. due to jitter, latency, or a high threshold value).")]
		[global::UnityEngine.Range(0.01f, 1f)]
		public float RotationMaxInterpolationTime = 0.1f;

		public bool ScaleLerpSmoothing = true;

		private bool m_PreviousScaleLerpSmoothing;

		[global::UnityEngine.Tooltip("The higher the value the smoother, but can result in lost data points (i.e. quick changes in direct). The lower the value the more accurate/precise, but can result in slight stutter (i.e. due to jitter, latency, or a high threshold value).")]
		[global::UnityEngine.Range(0.01f, 1f)]
		public float ScaleMaxInterpolationTime = 0.1f;

		[global::UnityEngine.Tooltip("Selects who has authority (sends state updates) over the transform. When the network topology is set to distributed authority, this always defaults to owner authority. If server (the default), then only server-side adjustments to the transform will be synchronized with clients. If owner (or client), then only the owner-side adjustments to the transform will be synchronized with both the server and other clients.")]
		public global::Unity.Netcode.Components.NetworkTransform.AuthorityModes AuthorityMode;

		[global::UnityEngine.Tooltip("When enabled, any parented children of this instance will send a state update when this instance sends a state update. If this instance doesn't send a state update, the children will still send state updates when reaching their axis specified threshold delta. Children do not have to have this setting enabled.")]
		public bool TickSyncChildren;

		public const float PositionThresholdDefault = 0.001f;

		public const float RotAngleThresholdDefault = 0.01f;

		public const float ScaleThresholdDefault = 0.01f;

		public global::Unity.Netcode.Components.NetworkTransform.OnClientRequestChangeDelegate OnClientRequestChange;

		internal static bool TrackStateUpdateId = false;

		[global::UnityEngine.Tooltip("When set, NetworkTransform will send common state updates using unreliable network delivery to provide a higher tolerance to poor network conditions (especially packet loss). When disabled, all state updates are sent using reliable fragmented sequenced network delivery. Note: This will change the order of operations between transform state updates and other messages sent reliably.")]
		public bool UseUnreliableDeltas;

		public bool SyncPositionX = true;

		public bool SyncPositionY = true;

		public bool SyncPositionZ = true;

		public bool SyncRotAngleX = true;

		public bool SyncRotAngleY = true;

		public bool SyncRotAngleZ = true;

		public bool SyncScaleX = true;

		public bool SyncScaleY = true;

		public bool SyncScaleZ = true;

		public float PositionThreshold = 0.001f;

		[global::UnityEngine.Range(1E-05f, 360f)]
		public float RotAngleThreshold = 0.01f;

		public float ScaleThreshold = 0.01f;

		[global::UnityEngine.Tooltip("When enabled, this will synchronize the full Quaternion (i.e. all Euler rotation axis are updated if one axis has a delta)")]
		public bool UseQuaternionSynchronization;

		[global::UnityEngine.Tooltip("When enabled, this uses a smallest three implementation that reduces full Quaternion updates down to the size of an unsigned integer (ignores half float precision settings).")]
		public bool UseQuaternionCompression;

		[global::UnityEngine.Tooltip("When enabled, this will use half float precision values for position (uses delta position updating), rotation (except when Quaternion compression is enabled), and scale.")]
		public bool UseHalfFloatPrecision;

		[global::UnityEngine.Tooltip("Sets whether this transform should sync in local space or in world space")]
		public bool InLocalSpace;

		[global::UnityEngine.Tooltip("When enabled, NetworkTransform controls world or local space settings while also providing smooth parenting transitions.When disabled, world or local space settings have to be adjusted by script or in the inspector view.")]
		public bool SwitchTransformSpaceWhenParented;

		public bool Interpolate = true;

		[global::UnityEngine.Tooltip("When enabled the position interpolator will Slerp towards its current target position.")]
		public bool SlerpPosition;

		protected global::Unity.Netcode.NetworkManager m_CachedNetworkManager;

		private global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState m_LocalAuthoritativeNetworkState;

		private global::Unity.Netcode.BufferedLinearInterpolatorVector3 m_PositionInterpolator;

		private global::Unity.Netcode.BufferedLinearInterpolatorVector3 m_ScaleInterpolator;

		private global::Unity.Netcode.BufferedLinearInterpolatorQuaternion m_RotationInterpolator;

		private global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState m_OldState;

		private global::UnityEngine.Vector3 m_InternalCurrentPosition;

		private global::UnityEngine.Vector3 m_LastStateTargetPosition;

		private global::UnityEngine.Vector3 m_InternalCurrentScale;

		private global::UnityEngine.Vector3 m_TargetScale;

		private global::UnityEngine.Quaternion m_InternalCurrentRotation;

		private global::UnityEngine.Vector3 m_TargetRotation;

		private bool m_UseRigidbodyForMotion;

		private global::Unity.Netcode.Components.NetworkRigidbodyBase m_NetworkRigidbodyInternal;

		private global::Unity.Netcode.Components.NetworkDeltaPosition m_HalfPositionState = new global::Unity.Netcode.Components.NetworkDeltaPosition(global::UnityEngine.Vector3.zero, 0);

		internal global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState SynchronizeState;

		private bool m_DeltaSynch;

		internal bool LogMotion;

		internal int LastTickSync;

		internal bool LogStateUpdate;

		internal static bool AssignDefaultInterpolationType;

		internal static global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes DefaultInterpolationType;

		internal global::UnityEngine.Transform CachedTransform;

		private int m_HalfFloatTargetTickOwnership;

		private global::Unity.Netcode.NetworkObject m_CachedNetworkObject;

		internal bool IsNested;

		private global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject> m_ParentedChildren = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>();

		private bool m_IsFirstNetworkTransform;

		private float m_FixedTimeFrameDelta;

		private float m_DeltaFixedUpdateCached;

		internal global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState InboundState;

		private global::Unity.Netcode.NetworkTransformMessage m_OutboundMessage;

		private static global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkManager, global::Unity.Netcode.Components.NetworkTransform.NetworkTransformTickRegistration> s_NetworkTickRegistration = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkManager, global::Unity.Netcode.Components.NetworkTransform.NetworkTransformTickRegistration>();

		public static int InterpolationBufferTickOffset = 0;

		private static int s_TickSynchPosition;

		private int m_NextTickSync;

		internal bool SynchronizePosition
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!SyncPositionX && !SyncPositionY)
				{
					return SyncPositionZ;
				}
				return true;
			}
		}

		internal bool SynchronizeRotation
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!SyncRotAngleX && !SyncRotAngleY)
				{
					return SyncRotAngleZ;
				}
				return true;
			}
		}

		internal bool SynchronizeScale
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!SyncScaleX && !SyncScaleY)
				{
					return SyncScaleZ;
				}
				return true;
			}
		}

		protected bool PositionInLocalSpace => InLocalSpace;

		protected bool RotationInLocalSpace => InLocalSpace;

		public bool CanCommitToTransform { get; protected set; }

		internal global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState LocalAuthoritativeNetworkState
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_LocalAuthoritativeNetworkState;
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				m_LocalAuthoritativeNetworkState = value;
			}
		}

		internal global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState OutboundState => m_LocalAuthoritativeNetworkState;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 GetSpaceRelativePosition(bool getCurrentState = false)
		{
			if (!getCurrentState || CanCommitToTransform)
			{
				if (!InLocalSpace)
				{
					return CachedTransform.position;
				}
				return CachedTransform.localPosition;
			}
			if (UseHalfFloatPrecision)
			{
				return m_HalfPositionState.GetFullPosition();
			}
			return m_InternalCurrentPosition;
		}

		public global::UnityEngine.Quaternion GetSpaceRelativeRotation(bool getCurrentState = false)
		{
			if (!getCurrentState || CanCommitToTransform)
			{
				if (!InLocalSpace)
				{
					return CachedTransform.rotation;
				}
				return CachedTransform.localRotation;
			}
			return m_InternalCurrentRotation;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Vector3 GetScale(bool getCurrentState = false)
		{
			if (!getCurrentState || CanCommitToTransform)
			{
				return CachedTransform.localScale;
			}
			return m_InternalCurrentScale;
		}

		internal void RegisterRigidbody(global::Unity.Netcode.Components.NetworkRigidbodyBase networkRigidbody)
		{
			if (networkRigidbody != null)
			{
				m_NetworkRigidbodyInternal = networkRigidbody;
				m_UseRigidbodyForMotion = m_NetworkRigidbodyInternal.UseRigidBodyForMotion;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void AddLogEntry(ref global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState networkTransformState, ulong targetClient, bool preUpdate = false)
		{
		}

		internal void UpdatePositionSlerp()
		{
			if (m_PositionInterpolator != null)
			{
				m_PositionInterpolator.IsSlerp = SlerpPosition;
			}
		}

		private bool ShouldSynchronizeHalfFloat(ulong targetClientId)
		{
			if (!IsServerAuthoritative() && base.NetworkObject.OwnerClientId == targetClientId)
			{
				if (base.NetworkManager.DistributedAuthorityMode || !base.NetworkObject.IsOwnedByServer)
				{
					return base.NetworkManager.DistributedAuthorityMode;
				}
				return true;
			}
			return true;
		}

		protected override void OnSynchronize<T>(ref global::Unity.Netcode.BufferSerializer<T> serializer)
		{
			ulong targetIdBeingSynchronized = base.m_TargetIdBeingSynchronized;
			SynchronizeState = new global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState
			{
				HalfEulerRotation = default(global::Unity.Netcode.Components.HalfVector3),
				HalfVectorRotation = default(global::Unity.Netcode.Components.HalfVector4),
				HalfVectorScale = default(global::Unity.Netcode.Components.HalfVector3),
				NetworkDeltaPosition = default(global::Unity.Netcode.Components.NetworkDeltaPosition)
			};
			if (serializer.IsWriter)
			{
				SynchronizeState.FlagStates.IsTeleportingNextFrame = true;
				CheckForStateChange(ref SynchronizeState, isSynchronization: true, targetIdBeingSynchronized);
				SynchronizeState.NetworkSerialize(serializer);
				LastTickSync = SynchronizeState.GetNetworkTick();
				OnAuthorityPushTransformState(ref SynchronizeState);
			}
			else
			{
				SynchronizeState.NetworkSerialize(serializer);
				LastTickSync = SynchronizeState.GetNetworkTick();
				OnNetworkTransformStateUpdated(ref SynchronizeState, ref SynchronizeState);
			}
		}

		private void ApplySynchronization()
		{
			InLocalSpace = SynchronizeState.InLocalSpace;
			Interpolate = SynchronizeState.UseInterpolation;
			UseQuaternionSynchronization = SynchronizeState.QuaternionSync;
			UseHalfFloatPrecision = SynchronizeState.UseHalfFloatPrecision;
			UseQuaternionCompression = SynchronizeState.QuaternionCompression;
			SlerpPosition = SynchronizeState.UsePositionSlerp;
			UpdatePositionSlerp();
			ApplyTeleportingState(SynchronizeState);
			m_LocalAuthoritativeNetworkState = SynchronizeState;
			m_LocalAuthoritativeNetworkState.FlagStates.IsTeleportingNextFrame = false;
			m_LocalAuthoritativeNetworkState.FlagStates.IsSynchronizing = false;
			SynchronizeState.FlagStates.IsSynchronizing = false;
		}

		protected virtual void OnAuthorityPushTransformState(ref global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState networkTransformState)
		{
		}

		private void TryCommitTransform(bool synchronize = false, bool settingState = false)
		{
			if (!base.IsServer && !base.IsOwner)
			{
				global::Unity.Netcode.NetworkLog.LogError("[" + base.name + "] is trying to commit the transform without authority!");
				return;
			}
			if (m_NetworkRigidbodyInternal != null)
			{
				m_UseRigidbodyForMotion = m_NetworkRigidbodyInternal.UseRigidBodyForMotion;
			}
			if (!m_LocalAuthoritativeNetworkState.ExplicitSet && !CheckForStateChange(ref m_LocalAuthoritativeNetworkState, synchronize, 0uL, settingState))
			{
				return;
			}
			if (m_LocalAuthoritativeNetworkState.ExplicitSet)
			{
				m_LocalAuthoritativeNetworkState.NetworkTick = m_CachedNetworkManager.NetworkTickSystem.ServerTime.Tick;
				if (SwitchTransformSpaceWhenParented && m_LocalAuthoritativeNetworkState.ExplicitSet && m_LocalAuthoritativeNetworkState.FlagStates.IsDirty && base.transform.parent != null && !m_LocalAuthoritativeNetworkState.InLocalSpace)
				{
					InLocalSpace = true;
					CheckForStateChange(ref m_LocalAuthoritativeNetworkState, synchronize, 0uL, forceState: true);
				}
			}
			UpdateTransformState();
			m_OldState = m_LocalAuthoritativeNetworkState;
			m_LocalAuthoritativeNetworkState.FlagStates.WasTeleported = m_LocalAuthoritativeNetworkState.IsTeleportingNextFrame;
			m_LocalAuthoritativeNetworkState.FlagStates.IsTeleportingNextFrame = false;
			m_LocalAuthoritativeNetworkState.ExplicitSet = false;
			try
			{
				OnAuthorityPushTransformState(ref m_LocalAuthoritativeNetworkState);
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
			if (UseUnreliableDeltas && !m_LocalAuthoritativeNetworkState.FlagStates.UnreliableFrameSync && !synchronize)
			{
				m_DeltaSynch = true;
			}
			if (m_UseRigidbodyForMotion && m_NetworkRigidbodyInternal.NetworkRigidbodyConnections.Count > 0)
			{
				foreach (global::Unity.Netcode.Components.NetworkRigidbodyBase networkRigidbodyConnection in m_NetworkRigidbodyInternal.NetworkRigidbodyConnections)
				{
					networkRigidbodyConnection.NetworkTransform.OnNetworkTick(isCalledFromParent: true);
				}
			}
			if (!TickSyncChildren)
			{
				return;
			}
			foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform in base.NetworkObject.NetworkTransforms)
			{
				if (!(networkTransform == this) && networkTransform.AuthorityMode == AuthorityMode && networkTransform.CanCommitToTransform)
				{
					networkTransform.OnNetworkTick(isCalledFromParent: true);
				}
			}
			foreach (global::Unity.Netcode.NetworkObject parentedChild in m_ParentedChildren)
			{
				foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform2 in parentedChild.NetworkTransforms)
				{
					if (networkTransform2.CanCommitToTransform)
					{
						networkTransform2.OnNetworkTick(isCalledFromParent: true);
					}
				}
			}
		}

		internal global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState ApplyLocalNetworkState()
		{
			m_LocalAuthoritativeNetworkState.FlagStates.ClearForNextTick();
			CheckForStateChange(ref m_LocalAuthoritativeNetworkState, isSynchronization: false, 0uL);
			return m_LocalAuthoritativeNetworkState;
		}

		internal bool ApplyTransformToNetworkState(ref global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState networkState, double dirtyTime, global::UnityEngine.Transform transformToUse)
		{
			CachedTransform = transformToUse;
			m_CachedNetworkManager = base.NetworkManager;
			networkState.FlagStates.UseInterpolation = Interpolate;
			networkState.FlagStates.QuaternionSync = UseQuaternionSynchronization;
			networkState.FlagStates.UseHalfFloatPrecision = UseHalfFloatPrecision;
			networkState.FlagStates.QuaternionCompression = UseQuaternionCompression;
			networkState.FlagStates.UseUnreliableDeltas = UseUnreliableDeltas;
			m_HalfPositionState = new global::Unity.Netcode.Components.NetworkDeltaPosition(global::UnityEngine.Vector3.zero, 0, global::Unity.Mathematics.math.bool3(SyncPositionX, SyncPositionY, SyncPositionZ));
			return CheckForStateChange(ref networkState, isSynchronization: false, 0uL);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private bool CheckForStateChange(ref global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState networkState, bool isSynchronization = false, ulong targetClientId = 0uL, bool forceState = false)
		{
			global::Unity.Netcode.Components.NetworkTransform.FlagStates flagStates = networkState.FlagStates;
			bool flag = false;
			if (UseUnreliableDeltas && !isSynchronization && m_DeltaSynch && m_NextTickSync <= CurrentTick)
			{
				m_NextTickSync += (int)m_CachedNetworkManager.NetworkConfig.TickRate;
				flag = !flagStates.IsTeleportingNextFrame;
				m_DeltaSynch = false;
			}
			flagStates.UnreliableFrameSync = flag;
			bool num = flagStates.IsTeleportingNextFrame && !isSynchronization;
			bool flag2 = false;
			bool flag3 = num && flagStates.HasPositionChange;
			bool flag4 = num && flagStates.HasRotAngleChange;
			bool flag5 = num && flagStates.HasScaleChange;
			flagStates.SwitchTransformSpaceWhenParented = SwitchTransformSpaceWhenParented;
			if ((InLocalSpace != flagStates.InLocalSpace || isSynchronization) && !m_UseRigidbodyForMotion)
			{
				flagStates.InLocalSpace = (SwitchTransformSpaceWhenParented ? (base.transform.parent != null) : InLocalSpace);
				if (SwitchTransformSpaceWhenParented)
				{
					InLocalSpace = flagStates.InLocalSpace;
				}
				flag2 = true;
				flagStates.IsTeleportingNextFrame |= !SwitchTransformSpaceWhenParented || isSynchronization;
				forceState = SwitchTransformSpaceWhenParented;
			}
			global::UnityEngine.Vector3 vector = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetPosition() : (InLocalSpace ? CachedTransform.localPosition : CachedTransform.position));
			global::UnityEngine.Quaternion rotation = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetRotation() : (InLocalSpace ? CachedTransform.localRotation : CachedTransform.rotation));
			global::UnityEngine.Vector3 vector2 = global::UnityEngine.Vector3.one * PositionThreshold;
			global::UnityEngine.Vector3 vector3 = global::UnityEngine.Vector3.one * RotAngleThreshold;
			global::UnityEngine.Vector3 eulerAngles = rotation.eulerAngles;
			global::UnityEngine.Vector3 localScale = CachedTransform.localScale;
			flagStates.IsSynchronizing = isSynchronization;
			if (isSynchronization || flagStates.IsTeleportingNextFrame || forceState)
			{
				bool isParented = false;
				global::Unity.Netcode.NetworkObject networkObject = null;
				if (base.NetworkObject.transform.parent != null)
				{
					networkObject = base.NetworkObject.transform.parent.GetComponent<global::Unity.Netcode.NetworkObject>();
					isParented = (networkObject == null && base.NetworkObject.IsSceneObject != false) || networkObject != null;
				}
				flagStates.IsParented = isParented;
			}
			if (Interpolate != flagStates.UseInterpolation)
			{
				flagStates.UseInterpolation = Interpolate;
				flag2 = true;
				flagStates.IsTeleportingNextFrame = true;
			}
			if (UseQuaternionSynchronization != flagStates.QuaternionSync)
			{
				flagStates.QuaternionSync = UseQuaternionSynchronization;
				flag2 = true;
				flagStates.IsTeleportingNextFrame = true;
			}
			if (UseQuaternionCompression != flagStates.QuaternionCompression)
			{
				flagStates.QuaternionCompression = UseQuaternionCompression;
				flag2 = true;
				flagStates.IsTeleportingNextFrame = true;
			}
			if (UseHalfFloatPrecision != flagStates.UseHalfFloatPrecision)
			{
				flagStates.UseHalfFloatPrecision = UseHalfFloatPrecision;
				flag2 = true;
				flagStates.IsTeleportingNextFrame = true;
			}
			if (SlerpPosition != flagStates.UsePositionSlerp)
			{
				flagStates.UsePositionSlerp = SlerpPosition;
				flag2 = true;
				flagStates.IsTeleportingNextFrame = true;
			}
			if (UseUnreliableDeltas != flagStates.UseUnreliableDeltas)
			{
				flagStates.UseUnreliableDeltas = UseUnreliableDeltas;
				flag2 = true;
				flagStates.IsTeleportingNextFrame = true;
			}
			if (!UseHalfFloatPrecision)
			{
				if (SyncPositionX && (global::UnityEngine.Mathf.Abs(networkState.PositionX - vector.x) >= vector2.x || flagStates.IsTeleportingNextFrame || flag || forceState))
				{
					networkState.PositionX = vector.x;
					flagStates.SetHasPosition(global::Unity.Netcode.Components.NetworkTransform.Axis.X, changed: true);
					flag3 = true;
				}
				if (SyncPositionY && (global::UnityEngine.Mathf.Abs(networkState.PositionY - vector.y) >= vector2.y || flagStates.IsTeleportingNextFrame || flag || forceState))
				{
					networkState.PositionY = vector.y;
					flagStates.SetHasPosition(global::Unity.Netcode.Components.NetworkTransform.Axis.Y, changed: true);
					flag3 = true;
				}
				if (SyncPositionZ && (global::UnityEngine.Mathf.Abs(networkState.PositionZ - vector.z) >= vector2.z || flagStates.IsTeleportingNextFrame || flag || forceState))
				{
					networkState.PositionZ = vector.z;
					flagStates.SetHasPosition(global::Unity.Netcode.Components.NetworkTransform.Axis.Z, changed: true);
					flag3 = true;
				}
			}
			else if (SynchronizePosition)
			{
				flag3 = flagStates.IsTeleportingNextFrame || flag || forceState;
				if (m_HalfFloatTargetTickOwnership > CurrentTick)
				{
					flag3 = true;
				}
				if (!flag3)
				{
					for (int i = 0; i < 3; i++)
					{
						if (global::System.Math.Abs(vector[i] - m_HalfPositionState.PreviousPosition[i]) >= vector2[i])
						{
							flag3 = i switch
							{
								1 => SyncPositionY, 
								0 => SyncPositionX, 
								_ => SyncPositionZ, 
							};
							if (flag3)
							{
								break;
							}
						}
					}
				}
				if (flag3)
				{
					if (!isSynchronization)
					{
						if (flagStates.IsTeleportingNextFrame)
						{
							m_HalfPositionState = new global::Unity.Netcode.Components.NetworkDeltaPosition(vector, networkState.NetworkTick, global::Unity.Mathematics.math.bool3(SyncPositionX, SyncPositionY, SyncPositionZ));
							networkState.CurrentPosition = vector;
						}
						else
						{
							m_HalfPositionState.HalfVector3.AxisToSynchronize = global::Unity.Mathematics.math.bool3(SyncPositionX, SyncPositionY, SyncPositionZ);
							m_HalfPositionState.UpdateFrom(ref vector, networkState.NetworkTick);
						}
						networkState.NetworkDeltaPosition = m_HalfPositionState;
						if ((m_HalfFloatTargetTickOwnership > CurrentTick || flag) && !flagStates.IsTeleportingNextFrame)
						{
							flagStates.SynchronizeBaseHalfFloat = true;
						}
						else
						{
							flagStates.SynchronizeBaseHalfFloat = UseUnreliableDeltas && m_HalfPositionState.CollapsedDeltaIntoBase;
						}
					}
					else
					{
						if (ShouldSynchronizeHalfFloat(targetClientId))
						{
							if (m_HalfPositionState.NetworkTick > 0)
							{
								networkState.CurrentPosition = m_HalfPositionState.CurrentBasePosition;
								networkState.NetworkDeltaPosition = m_HalfPositionState;
								if (base.NetworkObject.IsOwnedByServer || IsServerAuthoritative())
								{
									networkState.DeltaPosition = m_HalfPositionState.HalfDeltaConvertedBack;
								}
								else
								{
									networkState.DeltaPosition = m_HalfPositionState.DeltaPosition;
								}
							}
							else
							{
								networkState.NetworkDeltaPosition = new global::Unity.Netcode.Components.NetworkDeltaPosition(global::UnityEngine.Vector3.zero, 0, global::Unity.Mathematics.math.bool3(SyncPositionX, SyncPositionY, SyncPositionZ));
								networkState.DeltaPosition = global::UnityEngine.Vector3.zero;
								networkState.CurrentPosition = vector;
							}
						}
						else
						{
							networkState.NetworkDeltaPosition = new global::Unity.Netcode.Components.NetworkDeltaPosition(global::UnityEngine.Vector3.zero, 0, global::Unity.Mathematics.math.bool3(SyncPositionX, SyncPositionY, SyncPositionZ));
							networkState.CurrentPosition = vector;
						}
						AddLogEntry(ref networkState, targetClientId, preUpdate: true);
					}
					flagStates.HasPositionX = SyncPositionX;
					flagStates.HasPositionY = SyncPositionY;
					flagStates.HasPositionZ = SyncPositionZ;
					flagStates.HasPositionChange = SyncPositionX || SyncPositionY || SyncPositionZ;
				}
			}
			if (!UseQuaternionSynchronization)
			{
				if (SyncRotAngleX && (global::UnityEngine.Mathf.Abs(global::UnityEngine.Mathf.DeltaAngle(networkState.RotAngleX, eulerAngles.x)) >= vector3.x || flagStates.IsTeleportingNextFrame || flag || forceState))
				{
					networkState.RotAngleX = eulerAngles.x;
					flagStates.SetHasRotation(global::Unity.Netcode.Components.NetworkTransform.Axis.X, changed: true);
					flag4 = true;
				}
				if (SyncRotAngleY && (global::UnityEngine.Mathf.Abs(global::UnityEngine.Mathf.DeltaAngle(networkState.RotAngleY, eulerAngles.y)) >= vector3.y || flagStates.IsTeleportingNextFrame || flag || forceState))
				{
					networkState.RotAngleY = eulerAngles.y;
					flagStates.SetHasRotation(global::Unity.Netcode.Components.NetworkTransform.Axis.Y, changed: true);
					flag4 = true;
				}
				if (SyncRotAngleZ && (global::UnityEngine.Mathf.Abs(global::UnityEngine.Mathf.DeltaAngle(networkState.RotAngleZ, eulerAngles.z)) >= vector3.z || flagStates.IsTeleportingNextFrame || flag || forceState))
				{
					networkState.RotAngleZ = eulerAngles.z;
					flagStates.SetHasRotation(global::Unity.Netcode.Components.NetworkTransform.Axis.Z, changed: true);
					flag4 = true;
				}
			}
			else if (SynchronizeRotation)
			{
				flag4 = flagStates.IsTeleportingNextFrame || flag || forceState;
				if (!flag4)
				{
					global::UnityEngine.Vector3 eulerAngles2 = networkState.Rotation.eulerAngles;
					for (int j = 0; j < 3; j++)
					{
						if (global::UnityEngine.Mathf.Abs(global::UnityEngine.Mathf.DeltaAngle(eulerAngles2[j], eulerAngles[j])) >= vector3[j])
						{
							flag4 = true;
							break;
						}
					}
				}
				if (flag4)
				{
					networkState.Rotation = rotation;
					flagStates.MarkChanged(global::Unity.Netcode.Components.NetworkTransform.AxialType.Rotation, changed: true);
				}
			}
			if (flagStates.IsTeleportingNextFrame && flagStates.IsParented)
			{
				networkState.LossyScale = CachedTransform.lossyScale;
			}
			if (!isSynchronization)
			{
				if (!UseHalfFloatPrecision)
				{
					if (SyncScaleX && (global::UnityEngine.Mathf.Abs(networkState.ScaleX - localScale.x) >= ScaleThreshold || flagStates.IsTeleportingNextFrame || flag || forceState))
					{
						networkState.ScaleX = localScale.x;
						flagStates.SetHasScale(global::Unity.Netcode.Components.NetworkTransform.Axis.X, changed: true);
						flag5 = true;
					}
					if (SyncScaleY && (global::UnityEngine.Mathf.Abs(networkState.ScaleY - localScale.y) >= ScaleThreshold || flagStates.IsTeleportingNextFrame || flag || forceState))
					{
						networkState.ScaleY = localScale.y;
						flagStates.SetHasScale(global::Unity.Netcode.Components.NetworkTransform.Axis.Y, changed: true);
						flag5 = true;
					}
					if (SyncScaleZ && (global::UnityEngine.Mathf.Abs(networkState.ScaleZ - localScale.z) >= ScaleThreshold || flagStates.IsTeleportingNextFrame || flag || forceState))
					{
						networkState.ScaleZ = localScale.z;
						flagStates.SetHasScale(global::Unity.Netcode.Components.NetworkTransform.Axis.Z, changed: true);
						flag5 = true;
					}
				}
				else if (SynchronizeScale)
				{
					global::UnityEngine.Vector3 scale = networkState.Scale;
					for (int k = 0; k < 3; k++)
					{
						if (global::UnityEngine.Mathf.Abs(localScale[k] - scale[k]) >= ScaleThreshold || flagStates.IsTeleportingNextFrame || flag || forceState)
						{
							flag5 = true;
							networkState.Scale[k] = localScale[k];
							flagStates.SetHasScale((global::Unity.Netcode.Components.NetworkTransform.Axis)k, k switch
							{
								1 => SyncScaleY, 
								0 => SyncScaleX, 
								_ => SyncScaleZ, 
							});
						}
					}
				}
			}
			else if (SynchronizeScale)
			{
				global::UnityEngine.Vector3 localScale2 = CachedTransform.localScale;
				if (!UseHalfFloatPrecision)
				{
					networkState.ScaleX = localScale2.x;
					networkState.ScaleY = localScale2.y;
					networkState.ScaleZ = localScale2.z;
				}
				else
				{
					networkState.Scale = localScale2;
				}
				flagStates.MarkChanged(global::Unity.Netcode.Components.NetworkTransform.AxialType.Scale, changed: true);
				flag5 = true;
			}
			flag2 = flag2 || flag3 || flag4 || flag5;
			if (flag2 && base.enabled)
			{
				networkState.NetworkTick = CurrentTick;
			}
			flagStates.IsDirty |= flag2;
			networkState.FlagStates = flagStates;
			return flag2;
		}

		private void OnNetworkTick(bool isCalledFromParent = false)
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (CanCommitToTransform)
			{
				if (m_CachedNetworkManager.DistributedAuthorityMode && !base.IsOwner)
				{
					global::UnityEngine.Debug.LogError($"Non-owner Client-{m_CachedNetworkManager.LocalClientId} is being updated by network tick still!!!!");
				}
				else if ((!IsNested || m_LocalAuthoritativeNetworkState.NetworkTick != CurrentTick) && (isCalledFromParent || !m_UseRigidbodyForMotion || !(m_NetworkRigidbodyInternal.ParentBody != null) || m_LocalAuthoritativeNetworkState.IsTeleportingNextFrame))
				{
					OnUpdateAuthoritativeState(isCalledFromParent);
					m_InternalCurrentPosition = (m_LastStateTargetPosition = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetPosition() : GetSpaceRelativePosition()));
					m_InternalCurrentRotation = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetRotation() : GetSpaceRelativeRotation());
					m_TargetRotation = m_InternalCurrentRotation.eulerAngles;
				}
			}
			else
			{
				DeregisterForTickUpdate(this);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void UpdatePositionInterpolator(global::UnityEngine.Vector3 position, double time, bool resetInterpolator = false)
		{
			if (!CanCommitToTransform)
			{
				if (resetInterpolator)
				{
					m_PositionInterpolator.AutoConvertTransformSpace = SwitchTransformSpaceWhenParented;
					m_PositionInterpolator.InLocalSpace = InLocalSpace;
					m_PositionInterpolator.ResetTo(base.transform.parent, position, time);
				}
				else
				{
					m_PositionInterpolator.AddMeasurement(base.transform.parent, position, time);
				}
			}
		}

		protected virtual void OnTransformUpdated()
		{
		}

		protected internal void ApplyAuthoritativeState()
		{
			if (m_NetworkRigidbodyInternal != null)
			{
				m_UseRigidbodyForMotion = m_NetworkRigidbodyInternal.UseRigidBodyForMotion;
			}
			global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState localAuthoritativeNetworkState = m_LocalAuthoritativeNetworkState;
			global::Unity.Netcode.Components.NetworkTransform.FlagStates flagStates = m_LocalAuthoritativeNetworkState.FlagStates;
			global::UnityEngine.Vector3 internalCurrentPosition = m_InternalCurrentPosition;
			global::UnityEngine.Vector3 spaceRelativePosition = GetSpaceRelativePosition();
			internalCurrentPosition.x = (SyncPositionX ? m_InternalCurrentPosition.x : spaceRelativePosition.x);
			internalCurrentPosition.y = (SyncPositionY ? m_InternalCurrentPosition.y : spaceRelativePosition.y);
			internalCurrentPosition.z = (SyncPositionZ ? m_InternalCurrentPosition.z : spaceRelativePosition.z);
			global::UnityEngine.Quaternion internalCurrentRotation = m_InternalCurrentRotation;
			global::UnityEngine.Vector3 eulerAngles = internalCurrentRotation.eulerAngles;
			global::UnityEngine.Vector3 eulerAngles2 = GetSpaceRelativeRotation().eulerAngles;
			eulerAngles.x = (SyncRotAngleX ? eulerAngles.x : eulerAngles2.x);
			eulerAngles.y = (SyncRotAngleY ? eulerAngles.y : eulerAngles2.y);
			eulerAngles.z = (SyncRotAngleZ ? eulerAngles.z : eulerAngles2.z);
			internalCurrentRotation.eulerAngles = eulerAngles;
			global::UnityEngine.Vector3 internalCurrentScale = m_InternalCurrentScale;
			global::UnityEngine.Vector3 scale = GetScale();
			internalCurrentScale.x = (SyncScaleX ? internalCurrentScale.x : scale.x);
			internalCurrentScale.y = (SyncScaleY ? internalCurrentScale.y : scale.y);
			internalCurrentScale.z = (SyncScaleZ ? internalCurrentScale.z : scale.z);
			if (!SwitchTransformSpaceWhenParented)
			{
				InLocalSpace = flagStates.InLocalSpace;
			}
			Interpolate = flagStates.UseInterpolation;
			UseHalfFloatPrecision = flagStates.UseHalfFloatPrecision;
			UseQuaternionSynchronization = flagStates.QuaternionSync;
			UseQuaternionCompression = flagStates.QuaternionCompression;
			UseUnreliableDeltas = flagStates.UseUnreliableDeltas;
			if (SlerpPosition != flagStates.UsePositionSlerp)
			{
				SlerpPosition = flagStates.UsePositionSlerp;
				UpdatePositionSlerp();
			}
			if (Interpolate)
			{
				if (SynchronizePosition)
				{
					global::UnityEngine.Vector3 interpolatedValue = m_PositionInterpolator.GetInterpolatedValue();
					if (UseHalfFloatPrecision)
					{
						internalCurrentPosition = interpolatedValue;
					}
					else
					{
						if (SyncPositionX)
						{
							internalCurrentPosition.x = interpolatedValue.x;
						}
						if (SyncPositionY)
						{
							internalCurrentPosition.y = interpolatedValue.y;
						}
						if (SyncPositionZ)
						{
							internalCurrentPosition.z = interpolatedValue.z;
						}
					}
				}
				if (SynchronizeScale)
				{
					if (UseHalfFloatPrecision)
					{
						internalCurrentScale = m_ScaleInterpolator.GetInterpolatedValue();
					}
					else
					{
						global::UnityEngine.Vector3 interpolatedValue2 = m_ScaleInterpolator.GetInterpolatedValue();
						if (SyncScaleX)
						{
							internalCurrentScale.x = interpolatedValue2.x;
						}
						if (SyncScaleY)
						{
							internalCurrentScale.y = interpolatedValue2.y;
						}
						if (SyncScaleZ)
						{
							internalCurrentScale.z = interpolatedValue2.z;
						}
					}
				}
				if (SynchronizeRotation)
				{
					global::UnityEngine.Quaternion interpolatedValue3 = m_RotationInterpolator.GetInterpolatedValue();
					if (UseQuaternionSynchronization)
					{
						internalCurrentRotation = interpolatedValue3;
					}
					else
					{
						global::UnityEngine.Vector3 eulerAngles3 = interpolatedValue3.eulerAngles;
						if (SyncRotAngleX)
						{
							eulerAngles.x = eulerAngles3.x;
						}
						if (SyncRotAngleY)
						{
							eulerAngles.y = eulerAngles3.y;
						}
						if (SyncRotAngleZ)
						{
							eulerAngles.z = eulerAngles3.z;
						}
						internalCurrentRotation.eulerAngles = eulerAngles;
					}
				}
			}
			else
			{
				if (UseHalfFloatPrecision)
				{
					if (flagStates.HasPositionChange && SynchronizePosition)
					{
						internalCurrentPosition = m_LastStateTargetPosition;
					}
					if (flagStates.HasScaleChange && SynchronizeScale)
					{
						for (int i = 0; i < 3; i++)
						{
							if (m_LocalAuthoritativeNetworkState.FlagStates.HasScale((global::Unity.Netcode.Components.NetworkTransform.Axis)i))
							{
								internalCurrentScale[i] = m_LocalAuthoritativeNetworkState.Scale[i];
							}
						}
					}
				}
				else
				{
					if (flagStates.HasPositionX)
					{
						internalCurrentPosition.x = localAuthoritativeNetworkState.PositionX;
					}
					if (flagStates.HasPositionY)
					{
						internalCurrentPosition.y = localAuthoritativeNetworkState.PositionY;
					}
					if (flagStates.HasPositionZ)
					{
						internalCurrentPosition.z = localAuthoritativeNetworkState.PositionZ;
					}
					if (flagStates.HasScaleX)
					{
						internalCurrentScale.x = localAuthoritativeNetworkState.ScaleX;
					}
					if (flagStates.HasScaleY)
					{
						internalCurrentScale.y = localAuthoritativeNetworkState.ScaleY;
					}
					if (flagStates.HasScaleZ)
					{
						internalCurrentScale.z = localAuthoritativeNetworkState.ScaleZ;
					}
				}
				if (SynchronizeRotation)
				{
					if (flagStates.QuaternionSync && flagStates.HasRotAngleChange)
					{
						internalCurrentRotation = localAuthoritativeNetworkState.Rotation;
					}
					else
					{
						if (flagStates.HasRotAngleX)
						{
							eulerAngles.x = localAuthoritativeNetworkState.RotAngleX;
						}
						if (flagStates.HasRotAngleY)
						{
							eulerAngles.y = localAuthoritativeNetworkState.RotAngleY;
						}
						if (flagStates.HasRotAngleZ)
						{
							eulerAngles.z = localAuthoritativeNetworkState.RotAngleZ;
						}
						internalCurrentRotation.eulerAngles = eulerAngles;
					}
				}
			}
			if (SynchronizePosition)
			{
				if (flagStates.HasPositionChange || Interpolate)
				{
					if (SyncPositionX && SyncPositionY && SyncPositionZ)
					{
						m_InternalCurrentPosition = internalCurrentPosition;
					}
					else
					{
						global::UnityEngine.Vector3 vector = (InLocalSpace ? CachedTransform.localPosition : CachedTransform.position);
						m_InternalCurrentPosition.x = (SyncPositionX ? internalCurrentPosition.x : vector.x);
						m_InternalCurrentPosition.y = (SyncPositionY ? internalCurrentPosition.y : vector.y);
						m_InternalCurrentPosition.z = (SyncPositionZ ? internalCurrentPosition.z : vector.z);
					}
				}
				if (m_UseRigidbodyForMotion)
				{
					m_NetworkRigidbodyInternal.MovePosition(m_InternalCurrentPosition);
					if (LogMotion)
					{
						global::UnityEngine.Debug.Log($"[Client-{m_CachedNetworkManager.LocalClientId}][Interpolate: {localAuthoritativeNetworkState.UseInterpolation}][TransPos: {base.transform.position}][RBPos: {m_NetworkRigidbodyInternal.GetPosition()}][CurrentPos: {m_InternalCurrentPosition}");
					}
				}
				else if (PositionInLocalSpace)
				{
					CachedTransform.localPosition = m_InternalCurrentPosition;
				}
				else
				{
					CachedTransform.position = m_InternalCurrentPosition;
				}
			}
			if (SynchronizeRotation)
			{
				if (localAuthoritativeNetworkState.HasRotAngleChange || Interpolate)
				{
					if ((SyncRotAngleX && SyncRotAngleY && SyncRotAngleZ) || UseQuaternionSynchronization)
					{
						m_InternalCurrentRotation = internalCurrentRotation;
					}
					else
					{
						global::UnityEngine.Vector3 vector2 = (InLocalSpace ? CachedTransform.localRotation.eulerAngles : CachedTransform.rotation.eulerAngles);
						global::UnityEngine.Vector3 eulerAngles4 = m_InternalCurrentRotation.eulerAngles;
						global::UnityEngine.Vector3 eulerAngles5 = internalCurrentRotation.eulerAngles;
						eulerAngles4.x = (SyncRotAngleX ? eulerAngles5.x : vector2.x);
						eulerAngles4.y = (SyncRotAngleY ? eulerAngles5.y : vector2.y);
						eulerAngles4.z = (SyncRotAngleZ ? eulerAngles5.z : vector2.z);
						m_InternalCurrentRotation.eulerAngles = eulerAngles4;
					}
				}
				if (m_UseRigidbodyForMotion)
				{
					m_NetworkRigidbodyInternal.MoveRotation(m_InternalCurrentRotation);
				}
				else if (RotationInLocalSpace)
				{
					CachedTransform.localRotation = m_InternalCurrentRotation;
				}
				else
				{
					CachedTransform.rotation = m_InternalCurrentRotation;
				}
			}
			if (SynchronizeScale)
			{
				if (flagStates.HasScaleChange || Interpolate)
				{
					if (SyncScaleX && SyncScaleY && SyncScaleZ)
					{
						m_InternalCurrentScale = internalCurrentScale;
					}
					else
					{
						global::UnityEngine.Vector3 localScale = CachedTransform.localScale;
						m_InternalCurrentScale.x = (SyncScaleX ? internalCurrentScale.x : localScale.x);
						m_InternalCurrentScale.y = (SyncScaleY ? internalCurrentScale.y : localScale.y);
						m_InternalCurrentScale.z = (SyncScaleZ ? internalCurrentScale.z : localScale.z);
					}
				}
				CachedTransform.localScale = m_InternalCurrentScale;
			}
			OnTransformUpdated();
		}

		private void ApplyTeleportingState(global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState newState)
		{
			if (!newState.IsTeleportingNextFrame)
			{
				return;
			}
			double sentTime = newState.SentTime;
			global::UnityEngine.Vector3 vector = GetSpaceRelativePosition();
			global::UnityEngine.Quaternion quaternion2 = GetSpaceRelativeRotation();
			global::UnityEngine.Vector3 eulerAngles = quaternion2.eulerAngles;
			global::UnityEngine.Vector3 vector2 = CachedTransform.localScale;
			bool isSynchronizing = newState.IsSynchronizing;
			global::Unity.Netcode.Components.NetworkTransform.FlagStates flagStates = newState.FlagStates;
			m_ScaleInterpolator.Clear();
			m_PositionInterpolator.Clear();
			m_RotationInterpolator.Clear();
			if (flagStates.HasPositionChange)
			{
				if (!UseHalfFloatPrecision)
				{
					if (flagStates.HasPositionX)
					{
						vector.x = newState.PositionX;
					}
					if (flagStates.HasPositionY)
					{
						vector.y = newState.PositionY;
					}
					if (flagStates.HasPositionZ)
					{
						vector.z = newState.PositionZ;
					}
				}
				else
				{
					m_HalfPositionState = new global::Unity.Netcode.Components.NetworkDeltaPosition(newState.CurrentPosition, newState.NetworkTick, global::Unity.Mathematics.math.bool3(SyncPositionX, SyncPositionY, SyncPositionZ));
					if (isSynchronizing)
					{
						if (ShouldSynchronizeHalfFloat(base.NetworkManager.LocalClientId))
						{
							m_HalfPositionState.HalfVector3.Axis = newState.NetworkDeltaPosition.HalfVector3.Axis;
							m_HalfPositionState.DeltaPosition = newState.DeltaPosition;
							vector = m_HalfPositionState.ToVector3(newState.NetworkTick);
						}
						else
						{
							vector = newState.CurrentPosition;
						}
						AddLogEntry(ref newState, base.NetworkObject.OwnerClientId, preUpdate: true);
					}
					else
					{
						vector = newState.CurrentPosition;
					}
				}
				m_InternalCurrentPosition = vector;
				m_LastStateTargetPosition = vector;
				if (flagStates.InLocalSpace)
				{
					CachedTransform.localPosition = vector;
				}
				else
				{
					CachedTransform.position = vector;
				}
				if (m_UseRigidbodyForMotion)
				{
					m_NetworkRigidbodyInternal.SetPosition(base.transform.position);
				}
				if (Interpolate)
				{
					UpdatePositionInterpolator(vector, sentTime, resetInterpolator: true);
				}
			}
			if (flagStates.HasScaleChange)
			{
				bool flag = false;
				if (UseHalfFloatPrecision)
				{
					vector2 = (flag ? newState.LossyScale : newState.Scale);
				}
				else
				{
					if (flagStates.HasScaleX)
					{
						vector2.x = (flag ? newState.LossyScale.x : newState.ScaleX);
					}
					if (flagStates.HasScaleY)
					{
						vector2.y = (flag ? newState.LossyScale.y : newState.ScaleY);
					}
					if (flagStates.HasScaleZ)
					{
						vector2.z = (flag ? newState.LossyScale.z : newState.ScaleZ);
					}
				}
				m_InternalCurrentScale = vector2;
				m_TargetScale = vector2;
				CachedTransform.localScale = vector2;
				if (Interpolate)
				{
					m_ScaleInterpolator.ResetTo(vector2, sentTime);
				}
			}
			if (flagStates.HasRotAngleChange)
			{
				if (flagStates.QuaternionSync)
				{
					quaternion2 = newState.Rotation;
				}
				else
				{
					if (flagStates.HasRotAngleX)
					{
						eulerAngles.x = newState.RotAngleX;
					}
					if (flagStates.HasRotAngleY)
					{
						eulerAngles.y = newState.RotAngleY;
					}
					if (flagStates.HasRotAngleZ)
					{
						eulerAngles.z = newState.RotAngleZ;
					}
					quaternion2.eulerAngles = eulerAngles;
				}
				m_InternalCurrentRotation = quaternion2;
				m_TargetRotation = quaternion2.eulerAngles;
				if (InLocalSpace)
				{
					CachedTransform.localRotation = quaternion2;
				}
				else
				{
					CachedTransform.rotation = quaternion2;
				}
				if (m_UseRigidbodyForMotion)
				{
					m_NetworkRigidbodyInternal.SetRotation(CachedTransform.rotation);
				}
				if (Interpolate)
				{
					m_RotationInterpolator.AutoConvertTransformSpace = SwitchTransformSpaceWhenParented;
					m_RotationInterpolator.InLocalSpace = newState.InLocalSpace;
					m_RotationInterpolator.ResetTo(CachedTransform.parent, quaternion2, sentTime);
				}
			}
			if (isSynchronizing)
			{
				AddLogEntry(ref newState, base.NetworkObject.OwnerClientId);
			}
			OnTransformUpdated();
		}

		internal void ApplyUpdatedState(global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState newState)
		{
			global::Unity.Netcode.Components.NetworkTransform.FlagStates flagStates = newState.FlagStates;
			InLocalSpace = flagStates.InLocalSpace;
			Interpolate = flagStates.UseInterpolation;
			UseQuaternionSynchronization = flagStates.QuaternionSync;
			UseQuaternionCompression = flagStates.QuaternionCompression;
			UseHalfFloatPrecision = flagStates.UseHalfFloatPrecision;
			UseUnreliableDeltas = flagStates.UseUnreliableDeltas;
			SwitchTransformSpaceWhenParented = flagStates.SwitchTransformSpaceWhenParented;
			if (SlerpPosition != flagStates.UsePositionSlerp)
			{
				SlerpPosition = flagStates.UsePositionSlerp;
				UpdatePositionSlerp();
			}
			m_LocalAuthoritativeNetworkState = newState;
			if (flagStates.IsTeleportingNextFrame)
			{
				LastTickSync = m_LocalAuthoritativeNetworkState.GetNetworkTick();
				ApplyTeleportingState(m_LocalAuthoritativeNetworkState);
				return;
			}
			if (flagStates.IsSynchronizing)
			{
				LastTickSync = m_LocalAuthoritativeNetworkState.GetNetworkTick();
			}
			double sentTime = newState.SentTime;
			global::UnityEngine.Quaternion newMeasurement = GetSpaceRelativeRotation();
			if (UseHalfFloatPrecision && flagStates.HasPositionChange)
			{
				if (flagStates.SynchronizeBaseHalfFloat)
				{
					m_HalfPositionState = m_LocalAuthoritativeNetworkState.NetworkDeltaPosition;
				}
				else
				{
					m_HalfPositionState.HalfVector3.Axis = m_LocalAuthoritativeNetworkState.NetworkDeltaPosition.HalfVector3.Axis;
					m_LocalAuthoritativeNetworkState.NetworkDeltaPosition.CurrentBasePosition = m_HalfPositionState.CurrentBasePosition;
					m_LocalAuthoritativeNetworkState.NetworkDeltaPosition.ToVector3(0);
				}
				m_LastStateTargetPosition = m_HalfPositionState.ToVector3(newState.NetworkTick);
				m_LocalAuthoritativeNetworkState.CurrentPosition = m_LastStateTargetPosition;
			}
			if (!Interpolate)
			{
				ApplyAuthoritativeState();
				return;
			}
			if (flagStates.HasPositionChange)
			{
				if (!flagStates.UseHalfFloatPrecision)
				{
					global::UnityEngine.Vector3 lastStateTargetPosition = ((Interpolate && SwitchTransformSpaceWhenParented) ? m_PositionInterpolator.GetInterpolatedValue() : m_LastStateTargetPosition);
					global::UnityEngine.Vector3 position = m_LocalAuthoritativeNetworkState.GetPosition();
					if (flagStates.HasPositionX)
					{
						lastStateTargetPosition.x = position.x;
					}
					if (flagStates.HasPositionY)
					{
						lastStateTargetPosition.y = position.y;
					}
					if (flagStates.HasPositionZ)
					{
						lastStateTargetPosition.z = position.z;
					}
					m_LastStateTargetPosition = lastStateTargetPosition;
				}
				UpdatePositionInterpolator(m_LastStateTargetPosition, sentTime);
			}
			if (flagStates.HasScaleChange)
			{
				global::UnityEngine.Vector3 targetScale = m_TargetScale;
				if (UseHalfFloatPrecision)
				{
					for (int i = 0; i < 3; i++)
					{
						if (m_LocalAuthoritativeNetworkState.FlagStates.HasScale((global::Unity.Netcode.Components.NetworkTransform.Axis)i))
						{
							targetScale[i] = m_LocalAuthoritativeNetworkState.Scale[i];
						}
					}
				}
				else
				{
					if (flagStates.HasScaleX)
					{
						targetScale.x = m_LocalAuthoritativeNetworkState.ScaleX;
					}
					if (flagStates.HasScaleY)
					{
						targetScale.y = m_LocalAuthoritativeNetworkState.ScaleY;
					}
					if (flagStates.HasScaleZ)
					{
						targetScale.z = m_LocalAuthoritativeNetworkState.ScaleZ;
					}
				}
				m_TargetScale = targetScale;
				m_ScaleInterpolator.AddMeasurement(base.transform.parent, targetScale, sentTime);
			}
			if (!flagStates.HasRotAngleChange)
			{
				return;
			}
			if (flagStates.QuaternionSync)
			{
				newMeasurement = m_LocalAuthoritativeNetworkState.Rotation;
			}
			else
			{
				global::UnityEngine.Vector3 targetRotation = m_TargetRotation;
				if (flagStates.HasRotAngleX)
				{
					targetRotation.x = m_LocalAuthoritativeNetworkState.RotAngleX;
				}
				if (flagStates.HasRotAngleY)
				{
					targetRotation.y = m_LocalAuthoritativeNetworkState.RotAngleY;
				}
				if (flagStates.HasRotAngleZ)
				{
					targetRotation.z = m_LocalAuthoritativeNetworkState.RotAngleZ;
				}
				m_TargetRotation = targetRotation;
				newMeasurement.eulerAngles = targetRotation;
			}
			m_RotationInterpolator.AddMeasurement(base.transform.parent, newMeasurement, sentTime);
		}

		protected virtual void OnNetworkTransformStateUpdated(ref global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState oldState, ref global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState newState)
		{
		}

		protected virtual void OnBeforeUpdateTransformState()
		{
		}

		private void OnNetworkStateChanged(global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState oldState, global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState newState)
		{
			if (UseUnreliableDeltas && oldState.NetworkTick > newState.NetworkTick && !newState.FlagStates.IsTeleportingNextFrame && !newState.FlagStates.UnreliableFrameSync)
			{
				return;
			}
			newState.SentTime = new global::Unity.Netcode.NetworkTime(m_CachedNetworkManager.NetworkTickSystem.TickRate, newState.NetworkTick).Time;
			if (LogStateUpdate)
			{
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				stringBuilder.AppendLine($"[Client-{m_CachedNetworkManager.LocalClientId}][State Update: {newState.GetNetworkTick()}][HasPos: {newState.FlagStates.HasPositionChange}][Has Rot: {newState.FlagStates.HasRotAngleChange}][Has Scale: {newState.FlagStates.HasScaleChange}]");
				if (newState.FlagStates.HasPositionChange)
				{
					stringBuilder.AppendLine($"Position = {newState.GetPosition()}");
				}
				if (newState.FlagStates.HasRotAngleChange)
				{
					stringBuilder.AppendLine($"Rotation = {newState.GetRotation()}");
				}
				if (newState.FlagStates.HasScaleChange)
				{
					stringBuilder.AppendLine($"Scale = {newState.GetScale()}");
				}
				global::UnityEngine.Debug.Log(stringBuilder);
			}
			OnBeforeUpdateTransformState();
			ApplyUpdatedState(newState);
			if (TickSyncChildren && m_IsFirstNetworkTransform)
			{
				foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform in base.NetworkObject.NetworkTransforms)
				{
					if (!(networkTransform == this) && networkTransform.AuthorityMode == AuthorityMode && networkTransform.CanCommitToTransform)
					{
						networkTransform.OnNetworkTick(isCalledFromParent: true);
					}
				}
				foreach (global::Unity.Netcode.NetworkObject parentedChild in m_ParentedChildren)
				{
					foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform2 in parentedChild.NetworkTransforms)
					{
						if (networkTransform2.CanCommitToTransform)
						{
							networkTransform2.OnNetworkTick(isCalledFromParent: true);
						}
					}
				}
			}
			OnNetworkTransformStateUpdated(ref oldState, ref m_LocalAuthoritativeNetworkState);
		}

		public void SetMaxInterpolationBound(float maxInterpolationBound)
		{
			PositionMaxInterpolationTime = maxInterpolationBound;
			RotationMaxInterpolationTime = maxInterpolationBound;
			ScaleMaxInterpolationTime = maxInterpolationBound;
			m_RotationInterpolator.MaxInterpolationBound = maxInterpolationBound;
			m_PositionInterpolator.MaxInterpolationBound = maxInterpolationBound;
			m_ScaleInterpolator.MaxInterpolationBound = maxInterpolationBound;
		}

		private void AxisChangedDeltaPositionCheck()
		{
			if (!UseHalfFloatPrecision || !SynchronizePosition)
			{
				return;
			}
			global::Unity.Mathematics.bool3 axisToSynchronize = m_HalfPositionState.HalfVector3.AxisToSynchronize;
			if (SyncPositionX != axisToSynchronize.x || SyncPositionY != axisToSynchronize.y || SyncPositionZ != axisToSynchronize.z)
			{
				global::UnityEngine.Vector3 fullPosition = m_HalfPositionState.GetFullPosition();
				global::UnityEngine.Vector3 spaceRelativePosition = GetSpaceRelativePosition();
				bool isTeleportingNextFrame = false;
				if (SyncPositionX && SyncPositionX != axisToSynchronize.x)
				{
					isTeleportingNextFrame = global::UnityEngine.Mathf.Abs(spaceRelativePosition.x - fullPosition.x) >= 64f;
				}
				if (SyncPositionY && SyncPositionY != axisToSynchronize.y)
				{
					isTeleportingNextFrame = global::UnityEngine.Mathf.Abs(spaceRelativePosition.y - fullPosition.y) >= 64f;
				}
				if (SyncPositionZ && SyncPositionZ != axisToSynchronize.z)
				{
					isTeleportingNextFrame = global::UnityEngine.Mathf.Abs(spaceRelativePosition.z - fullPosition.z) >= 64f;
				}
				m_LocalAuthoritativeNetworkState.FlagStates.IsTeleportingNextFrame = isTeleportingNextFrame;
			}
		}

		internal void OnUpdateAuthoritativeState(bool settingState = false)
		{
			if (!m_LocalAuthoritativeNetworkState.ExplicitSet && m_LocalAuthoritativeNetworkState.FlagStates.IsDirty && !m_LocalAuthoritativeNetworkState.IsTeleportingNextFrame)
			{
				m_LocalAuthoritativeNetworkState.FlagStates.ClearForNextTick();
				if (TrackStateUpdateId)
				{
					m_LocalAuthoritativeNetworkState.FlagStates.TrackByStateId = true;
					m_LocalAuthoritativeNetworkState.StateId++;
				}
				else
				{
					m_LocalAuthoritativeNetworkState.FlagStates.TrackByStateId = false;
				}
			}
			AxisChangedDeltaPositionCheck();
			TryCommitTransform(synchronize: false, settingState);
		}

		private void NonAuthorityFinalizeSynchronization()
		{
			if (!SynchronizeState.IsSynchronizing)
			{
				return;
			}
			if (m_IsFirstNetworkTransform)
			{
				foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform in base.NetworkObject.NetworkTransforms)
				{
					if (!networkTransform.CanCommitToTransform)
					{
						networkTransform.ApplySynchronization();
						networkTransform.InternalInitialization();
					}
				}
				return;
			}
			if (!CanCommitToTransform)
			{
				ApplySynchronization();
				InternalInitialization();
			}
		}

		protected internal override void InternalOnNetworkSessionSynchronized()
		{
			NonAuthorityFinalizeSynchronization();
			base.InternalOnNetworkSessionSynchronized();
		}

		private void ApplyPlayerTransformState()
		{
			SynchronizeState.FlagStates.InLocalSpace = InLocalSpace;
			SynchronizeState.FlagStates.UseInterpolation = Interpolate;
			SynchronizeState.FlagStates.QuaternionSync = UseQuaternionSynchronization;
			SynchronizeState.FlagStates.UseHalfFloatPrecision = UseHalfFloatPrecision;
			SynchronizeState.FlagStates.QuaternionCompression = UseQuaternionCompression;
			SynchronizeState.FlagStates.UsePositionSlerp = SlerpPosition;
		}

		protected internal override void InternalOnNetworkPostSpawn()
		{
			if (base.NetworkManager.IsServer && !base.NetworkManager.DistributedAuthorityMode && !base.IsOwner && !OnIsServerAuthoritative() && !SynchronizeState.IsSynchronizing)
			{
				if (m_IsFirstNetworkTransform)
				{
					SynchronizeState.FlagStates.IsSynchronizing = true;
					foreach (global::Unity.Netcode.Components.NetworkTransform networkTransform in base.NetworkObject.NetworkTransforms)
					{
						if (!(networkTransform != this) || networkTransform.AuthorityMode == AuthorityMode)
						{
							networkTransform.ApplyPlayerTransformState();
						}
					}
				}
				else
				{
					ApplyPlayerTransformState();
				}
			}
			if (!CanCommitToTransform && base.NetworkManager.IsConnectedClient && SynchronizeState.IsSynchronizing)
			{
				NonAuthorityFinalizeSynchronization();
			}
			base.InternalOnNetworkPostSpawn();
		}

		protected virtual void Awake()
		{
			if (AssignDefaultInterpolationType)
			{
				PositionInterpolationType = DefaultInterpolationType;
				RotationInterpolationType = DefaultInterpolationType;
				ScaleInterpolationType = DefaultInterpolationType;
			}
			m_RotationInterpolator = new global::Unity.Netcode.BufferedLinearInterpolatorQuaternion();
			m_PositionInterpolator = new global::Unity.Netcode.BufferedLinearInterpolatorVector3();
			m_ScaleInterpolator = new global::Unity.Netcode.BufferedLinearInterpolatorVector3();
			if (SwitchTransformSpaceWhenParented)
			{
				InLocalSpace = false;
			}
			CachedTransform = base.transform;
		}

		internal override void InternalOnNetworkPreSpawn(ref global::Unity.Netcode.NetworkManager networkManager)
		{
			m_CachedNetworkManager = networkManager;
			CachedTransform = base.transform;
			base.InternalOnNetworkPreSpawn(ref networkManager);
		}

		public override void OnNetworkSpawn()
		{
			m_ParentedChildren.Clear();
			m_CachedNetworkManager = base.NetworkManager;
			Initialize();
			if (CanCommitToTransform && !SwitchTransformSpaceWhenParented)
			{
				SetState(GetSpaceRelativePosition(), GetSpaceRelativeRotation(), GetScale(), teleportDisabled: false);
			}
		}

		private void CleanUpOnDestroyOrDespawn()
		{
			m_ParentedChildren.Clear();
			bool onUpdate = !m_UseRigidbodyForMotion;
			if (m_CachedNetworkObject != null)
			{
				base.NetworkManager?.NetworkTransformRegistration(m_CachedNetworkObject, onUpdate, register: false);
			}
			DeregisterForTickUpdate(this);
			CanCommitToTransform = false;
		}

		public override void OnNetworkDespawn()
		{
			CleanUpOnDestroyOrDespawn();
			base.OnNetworkDespawn();
		}

		public override void OnDestroy()
		{
			CleanUpOnDestroyOrDespawn();
			base.OnDestroy();
		}

		protected virtual void OnInitialize(ref global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState replicatedState)
		{
		}

		protected virtual void OnInitialize(ref global::Unity.Netcode.NetworkVariable<global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState> replicatedState)
		{
		}

		private void ResetInterpolatedStateToCurrentAuthoritativeState()
		{
			double time = base.NetworkManager.ServerTime.Time;
			global::UnityEngine.Vector3 position = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetPosition() : GetSpaceRelativePosition());
			global::UnityEngine.Quaternion targetValue = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetRotation() : GetSpaceRelativeRotation());
			m_PositionInterpolator.AutoConvertTransformSpace = SwitchTransformSpaceWhenParented;
			m_PositionInterpolator.InLocalSpace = InLocalSpace;
			UpdatePositionInterpolator(position, time, resetInterpolator: true);
			UpdatePositionSlerp();
			m_RotationInterpolator.AutoConvertTransformSpace = SwitchTransformSpaceWhenParented;
			m_RotationInterpolator.InLocalSpace = InLocalSpace;
			m_RotationInterpolator.ResetTo(base.transform.parent, targetValue, time);
			m_ScaleInterpolator.ResetTo(base.transform.parent, base.transform.localScale, time);
		}

		private void InternalInitialization(bool isOwnershipChange = false)
		{
			if (!base.IsSpawned)
			{
				return;
			}
			m_CachedNetworkObject = base.NetworkObject;
			m_IsFirstNetworkTransform = base.NetworkObject.NetworkTransforms[0] == this;
			if ((bool)m_CachedNetworkManager && m_CachedNetworkManager.DistributedAuthorityMode)
			{
				AuthorityMode = global::Unity.Netcode.Components.NetworkTransform.AuthorityModes.Owner;
			}
			CanCommitToTransform = (IsServerAuthoritative() ? base.IsServer : base.IsOwner);
			if (SwitchTransformSpaceWhenParented)
			{
				if (CanCommitToTransform)
				{
					InLocalSpace = m_CachedNetworkObject.transform.GetComponentsInParent<global::Unity.Netcode.NetworkObject>().Length > 1;
				}
				TickSyncChildren = true;
			}
			global::UnityEngine.Vector3 spaceRelativePosition = GetSpaceRelativePosition();
			global::UnityEngine.Quaternion spaceRelativeRotation = GetSpaceRelativeRotation();
			if (base.NetworkManager.DistributedAuthorityMode)
			{
				RegisterNetworkManagerForTickUpdate(base.NetworkManager);
			}
			if ((bool)m_NetworkRigidbodyInternal)
			{
				m_NetworkRigidbodyInternal.UpdateOwnershipAuthority();
			}
			if (m_UseRigidbodyForMotion)
			{
				m_NetworkRigidbodyInternal.SetPosition(spaceRelativePosition);
				m_NetworkRigidbodyInternal.SetRotation(spaceRelativeRotation);
			}
			bool onUpdate = !m_UseRigidbodyForMotion;
			m_LocalAuthoritativeNetworkState.FlagStates.SynchronizeBaseHalfFloat = false;
			if (CanCommitToTransform)
			{
				m_CachedNetworkManager.NetworkTransformRegistration(base.NetworkObject, onUpdate, register: false);
				if (UseHalfFloatPrecision)
				{
					m_HalfPositionState = new global::Unity.Netcode.Components.NetworkDeltaPosition(spaceRelativePosition, m_CachedNetworkManager.ServerTime.Tick, global::Unity.Mathematics.math.bool3(SyncPositionX, SyncPositionY, SyncPositionZ));
					m_LocalAuthoritativeNetworkState.FlagStates.SynchronizeBaseHalfFloat = isOwnershipChange;
					SetState(null, null, null, teleportDisabled: false);
				}
				m_InternalCurrentPosition = spaceRelativePosition;
				m_LastStateTargetPosition = spaceRelativePosition;
				RegisterForTickUpdate(this);
				if (UseHalfFloatPrecision && isOwnershipChange && !IsServerAuthoritative() && Interpolate)
				{
					m_HalfFloatTargetTickOwnership = m_CachedNetworkManager.ServerTime.Tick;
				}
			}
			else
			{
				m_PreviousPositionInterpolationType = PositionInterpolationType;
				m_PreviousRotationInterpolationType = RotationInterpolationType;
				m_PreviousScaleInterpolationType = ScaleInterpolationType;
				m_PreviousPositionLerpSmoothing = PositionLerpSmoothing;
				m_PreviousRotationLerpSmoothing = RotationLerpSmoothing;
				m_PreviousScaleLerpSmoothing = ScaleLerpSmoothing;
				m_CachedNetworkManager.NetworkTransformRegistration(base.NetworkObject, onUpdate);
				DeregisterForTickUpdate(this);
				ResetInterpolatedStateToCurrentAuthoritativeState();
				m_InternalCurrentPosition = spaceRelativePosition;
				m_LastStateTargetPosition = spaceRelativePosition;
				m_InternalCurrentScale = base.transform.localScale;
				m_TargetScale = base.transform.localScale;
				m_InternalCurrentRotation = spaceRelativeRotation;
				m_TargetRotation = spaceRelativeRotation.eulerAngles;
			}
			OnInitialize(ref m_LocalAuthoritativeNetworkState);
		}

		protected void Initialize()
		{
			InternalInitialization();
		}

		public override void OnLostOwnership()
		{
			base.OnLostOwnership();
		}

		public override void OnGainedOwnership()
		{
			base.OnGainedOwnership();
		}

		protected override void OnOwnershipChanged(ulong previous, ulong current)
		{
			if (current == m_CachedNetworkManager.LocalClientId || previous == m_CachedNetworkManager.LocalClientId)
			{
				InternalInitialization(isOwnershipChange: true);
			}
			base.OnOwnershipChanged(previous, current);
		}

		public override void OnNetworkObjectParentChanged(global::Unity.Netcode.NetworkObject parentNetworkObject)
		{
			base.OnNetworkObjectParentChanged(parentNetworkObject);
		}

		private void DefaultParentChanged()
		{
			global::UnityEngine.Vector3 internalCurrentPosition = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetPosition() : GetSpaceRelativePosition());
			global::UnityEngine.Quaternion internalCurrentRotation = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetRotation() : GetSpaceRelativeRotation());
			m_LastStateTargetPosition = (m_InternalCurrentPosition = internalCurrentPosition);
			m_InternalCurrentRotation = internalCurrentRotation;
			m_TargetRotation = m_InternalCurrentRotation.eulerAngles;
			m_TargetScale = (m_InternalCurrentScale = GetScale());
			if (Interpolate)
			{
				m_ScaleInterpolator.Clear();
				m_PositionInterpolator.Clear();
				m_RotationInterpolator.Clear();
				double time = new global::Unity.Netcode.NetworkTime(base.NetworkManager.NetworkConfig.TickRate, base.NetworkManager.ServerTime.Tick).Time;
				UpdatePositionInterpolator(m_InternalCurrentPosition, time, resetInterpolator: true);
				m_ScaleInterpolator.ResetTo(m_InternalCurrentScale, time);
				m_RotationInterpolator.ResetTo(m_InternalCurrentRotation, time);
			}
		}

		internal override void InternalOnNetworkObjectParentChanged(global::Unity.Netcode.NetworkObject parentNetworkObject)
		{
			if (!SwitchTransformSpaceWhenParented)
			{
				if (!CanCommitToTransform)
				{
					DefaultParentChanged();
				}
				return;
			}
			InLocalSpace = parentNetworkObject != null;
			if (SynchronizePosition)
			{
				m_PositionInterpolator.AutoConvertTransformSpace = SwitchTransformSpaceWhenParented;
				m_PositionInterpolator.InLocalSpace = InLocalSpace;
				m_PositionInterpolator.Parent = (InLocalSpace ? parentNetworkObject.transform : null);
				if (LastTickSync == m_LocalAuthoritativeNetworkState.GetNetworkTick())
				{
					m_InternalCurrentPosition = (m_LastStateTargetPosition = GetSpaceRelativePosition());
					m_PositionInterpolator.ResetTo(m_PositionInterpolator.Parent, m_InternalCurrentPosition, base.NetworkManager.ServerTime.Time);
					if (InLocalSpace)
					{
						base.transform.localPosition = m_InternalCurrentPosition;
					}
					else
					{
						base.transform.position = m_InternalCurrentPosition;
					}
				}
				else if (CanCommitToTransform)
				{
					m_InternalCurrentPosition = GetSpaceRelativePosition();
				}
				else
				{
					m_InternalCurrentPosition = (m_LastStateTargetPosition = (Interpolate ? m_PositionInterpolator.GetInterpolatedValue() : GetSpaceRelativePosition()));
				}
			}
			if (SynchronizeRotation)
			{
				m_RotationInterpolator.AutoConvertTransformSpace = SwitchTransformSpaceWhenParented;
				m_RotationInterpolator.InLocalSpace = InLocalSpace;
				m_RotationInterpolator.Parent = (InLocalSpace ? parentNetworkObject.transform : null);
				if (LastTickSync == m_LocalAuthoritativeNetworkState.GetNetworkTick())
				{
					m_InternalCurrentRotation = GetSpaceRelativeRotation();
					m_TargetRotation = m_InternalCurrentRotation.eulerAngles;
					m_RotationInterpolator.ResetTo(m_RotationInterpolator.Parent, m_InternalCurrentRotation, base.NetworkManager.ServerTime.Time);
					if (InLocalSpace)
					{
						base.transform.localRotation = m_InternalCurrentRotation;
					}
					else
					{
						base.transform.rotation = m_InternalCurrentRotation;
					}
				}
				else
				{
					if (CanCommitToTransform)
					{
						m_InternalCurrentRotation = GetSpaceRelativeRotation();
					}
					else
					{
						m_InternalCurrentRotation = (Interpolate ? m_RotationInterpolator.GetInterpolatedValue() : GetSpaceRelativeRotation());
					}
					m_TargetRotation = m_InternalCurrentRotation.eulerAngles;
				}
			}
			base.InternalOnNetworkObjectParentChanged(parentNetworkObject);
		}

		public void SetState(global::UnityEngine.Vector3? posIn = null, global::UnityEngine.Quaternion? rotIn = null, global::UnityEngine.Vector3? scaleIn = null, bool teleportDisabled = true)
		{
			if (!base.IsSpawned)
			{
				global::Unity.Netcode.NetworkLog.LogError("Cannot commit transform when not spawned!");
				return;
			}
			if (!base.IsServer && !base.IsOwner)
			{
				global::Unity.Netcode.NetworkLog.LogError((base.gameObject != base.NetworkObject.gameObject) ? ("Non-authority instance of " + base.NetworkObject.gameObject.name + " is trying to commit a transform on " + base.gameObject.name + "!") : ("Non-authority instance of " + base.NetworkObject.gameObject.name + " is trying to commit a transform!"));
				return;
			}
			global::UnityEngine.Vector3 vector = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetPosition() : GetSpaceRelativePosition());
			global::UnityEngine.Quaternion quaternion2 = (m_UseRigidbodyForMotion ? m_NetworkRigidbodyInternal.GetRotation() : GetSpaceRelativeRotation());
			global::UnityEngine.Vector3 pos = posIn ?? vector;
			global::UnityEngine.Quaternion rot = rotIn ?? quaternion2;
			global::UnityEngine.Vector3 scale = scaleIn ?? CachedTransform.localScale;
			if (!CanCommitToTransform)
			{
				if (base.IsServer)
				{
					SetStateClientRpc(pos, rot, scale, !teleportDisabled);
				}
				else
				{
					SetStateServerRpc(pos, rot, scale, !teleportDisabled);
				}
			}
			else
			{
				SetStateInternal(pos, rot, scale, !teleportDisabled);
			}
		}

		private void SetStateInternal(global::UnityEngine.Vector3 pos, global::UnityEngine.Quaternion rot, global::UnityEngine.Vector3 scale, bool shouldTeleport)
		{
			if (m_UseRigidbodyForMotion)
			{
				m_NetworkRigidbodyInternal.SetPosition(pos);
				m_NetworkRigidbodyInternal.SetRotation(rot);
			}
			else if (InLocalSpace)
			{
				base.transform.SetLocalPositionAndRotation(pos, rot);
			}
			else
			{
				base.transform.SetPositionAndRotation(pos, rot);
			}
			base.transform.localScale = scale;
			m_LocalAuthoritativeNetworkState.FlagStates.IsTeleportingNextFrame = shouldTeleport;
			bool isDirty = m_LocalAuthoritativeNetworkState.FlagStates.IsDirty;
			bool explicitSet = m_LocalAuthoritativeNetworkState.ExplicitSet;
			bool flag = CheckForStateChange(ref m_LocalAuthoritativeNetworkState, isSynchronization: false, 0uL);
			m_LocalAuthoritativeNetworkState.ExplicitSet = (isDirty && explicitSet) || flag;
			m_LocalAuthoritativeNetworkState.FlagStates.IsDirty = m_LocalAuthoritativeNetworkState.ExplicitSet;
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Owner)]
		private void SetStateClientRpc(global::UnityEngine.Vector3 pos, global::UnityEngine.Quaternion rot, global::UnityEngine.Vector3 scale, bool shouldTeleport)
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = default(global::Unity.Netcode.RpcAttribute.RpcAttributeParams);
				global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams);
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(1045589154u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Owner, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in pos);
				bufferWriter.WriteValueSafe(in rot);
				bufferWriter.WriteValueSafe(in scale);
				bufferWriter.WriteValueSafe(in shouldTeleport, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				__endSendRpc(ref bufferWriter, 1045589154u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Owner, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				SetStateInternal(pos, rot, scale, shouldTeleport);
			}
		}

		[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner)]
		private void SetStateServerRpc(global::UnityEngine.Vector3 pos, global::UnityEngine.Quaternion rot, global::UnityEngine.Vector3 scale, bool shouldTeleport)
		{
			global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
			if ((object)networkManager == null || !networkManager.IsListening)
			{
				global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
				return;
			}
			if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
				{
					InvokePermission = global::Unity.Netcode.RpcInvokePermission.Owner
				};
				global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams);
				global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(2044052851u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
				bufferWriter.WriteValueSafe(in pos);
				bufferWriter.WriteValueSafe(in rot);
				bufferWriter.WriteValueSafe(in scale);
				bufferWriter.WriteValueSafe(in shouldTeleport, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				__endSendRpc(ref bufferWriter, 2044052851u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
			}
			if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
			{
				__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
				if (OnClientRequestChange != null)
				{
					(pos, rot, scale) = OnClientRequestChange(pos, rot, scale);
				}
				SetStateInternal(pos, rot, scale, shouldTeleport);
			}
		}

		public void Teleport(global::UnityEngine.Vector3 newPosition, global::UnityEngine.Quaternion newRotation, global::UnityEngine.Vector3 newScale)
		{
			if (!CanCommitToTransform)
			{
				throw new global::System.Exception("Teleporting on non-authoritative side is not allowed!");
			}
			SetStateInternal(newPosition, newRotation, newScale, shouldTeleport: true);
		}

		internal global::Unity.Netcode.BufferedLinearInterpolatorVector3 GetPositionInterpolator()
		{
			return m_PositionInterpolator;
		}

		internal global::Unity.Netcode.BufferedLinearInterpolatorQuaternion GetRotationInterpolator()
		{
			return m_RotationInterpolator;
		}

		private void UpdateInterpolation()
		{
			global::Unity.Netcode.NetworkTime localTime = m_CachedNetworkManager.LocalTime;
			double num = localTime.Time;
			float deltaTime = (m_UseRigidbodyForMotion ? m_CachedNetworkManager.RealTimeProvider.FixedDeltaTime : m_CachedNetworkManager.RealTimeProvider.DeltaTime);
			int num2 = global::UnityEngine.Mathf.Max(1, m_CachedNetworkManager.NetworkTimeSystem.TickLatency + InterpolationBufferTickOffset);
			if (!IsServerAuthoritative() && (!m_CachedNetworkManager.DistributedAuthorityMode || (m_CachedNetworkManager.DistributedAuthorityMode && !m_CachedNetworkManager.CMBServiceConnection)) && !m_CachedNetworkManager.IsServer && !base.NetworkObject.IsOwnedByServer)
			{
				num2++;
			}
			double renderTime = 0.0;
			if (PositionInterpolationType == global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes.LegacyLerp || RotationInterpolationType == global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes.LegacyLerp || ScaleInterpolationType == global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes.LegacyLerp)
			{
				int ticks = ((IsServerAuthoritative() || base.IsServer) ? 1 : 2) + InterpolationBufferTickOffset;
				renderTime = localTime.TimeTicksAgo(ticks).Time;
			}
			double num3 = localTime.TimeTicksAgo(num2).Time;
			if (m_UseRigidbodyForMotion)
			{
				num3 += (double)m_FixedTimeFrameDelta;
				num += (double)m_FixedTimeFrameDelta;
			}
			double fixedDeltaTimeAsDouble = localTime.FixedDeltaTimeAsDouble;
			double maxDeltaTime = (double)num2 * fixedDeltaTimeAsDouble;
			if (SynchronizePosition)
			{
				if (PositionLerpSmoothing)
				{
					m_PositionInterpolator.MaximumInterpolationTime = PositionMaxInterpolationTime;
				}
				m_PositionInterpolator.LerpSmoothEnabled = PositionLerpSmoothing;
				if (m_PreviousPositionInterpolationType != PositionInterpolationType || m_PreviousPositionLerpSmoothing != PositionLerpSmoothing)
				{
					m_PreviousPositionInterpolationType = PositionInterpolationType;
					m_PreviousPositionLerpSmoothing = PositionLerpSmoothing;
					m_PositionInterpolator.ResetCurrentState();
				}
				if (PositionInterpolationType == global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes.LegacyLerp)
				{
					m_PositionInterpolator.Update(deltaTime, renderTime, num);
				}
				else
				{
					m_PositionInterpolator.Update(deltaTime, num3, fixedDeltaTimeAsDouble, maxDeltaTime, PositionInterpolationType == global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes.Lerp);
				}
			}
			if (SynchronizeRotation)
			{
				if (RotationLerpSmoothing)
				{
					m_RotationInterpolator.MaximumInterpolationTime = RotationMaxInterpolationTime;
				}
				m_RotationInterpolator.LerpSmoothEnabled = RotationLerpSmoothing;
				if (m_PreviousRotationInterpolationType != RotationInterpolationType || m_PreviousRotationLerpSmoothing != RotationLerpSmoothing)
				{
					m_PreviousRotationInterpolationType = RotationInterpolationType;
					m_PreviousRotationLerpSmoothing = RotationLerpSmoothing;
					m_RotationInterpolator.ResetCurrentState();
				}
				m_RotationInterpolator.IsSlerp = !UseHalfFloatPrecision;
				if (RotationInterpolationType == global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes.LegacyLerp)
				{
					m_RotationInterpolator.Update(deltaTime, renderTime, num);
				}
				else
				{
					m_RotationInterpolator.Update(deltaTime, num3, fixedDeltaTimeAsDouble, maxDeltaTime, RotationInterpolationType == global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes.Lerp);
				}
			}
			if (SynchronizeScale)
			{
				if (ScaleLerpSmoothing)
				{
					m_ScaleInterpolator.MaximumInterpolationTime = ScaleMaxInterpolationTime;
				}
				m_ScaleInterpolator.LerpSmoothEnabled = ScaleLerpSmoothing;
				if (m_PreviousScaleInterpolationType != ScaleInterpolationType || m_PreviousScaleLerpSmoothing != ScaleLerpSmoothing)
				{
					m_PreviousScaleInterpolationType = ScaleInterpolationType;
					m_PreviousScaleLerpSmoothing = ScaleLerpSmoothing;
					m_ScaleInterpolator.ResetCurrentState();
				}
				if (ScaleInterpolationType == global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes.LegacyLerp)
				{
					m_ScaleInterpolator.Update(deltaTime, renderTime, num);
				}
				else
				{
					m_ScaleInterpolator.Update(deltaTime, num3, fixedDeltaTimeAsDouble, maxDeltaTime, ScaleInterpolationType == global::Unity.Netcode.Components.NetworkTransform.InterpolationTypes.Lerp);
				}
			}
		}

		public virtual void OnUpdate()
		{
			if (base.IsSpawned && !CanCommitToTransform && !m_UseRigidbodyForMotion)
			{
				if (Interpolate)
				{
					UpdateInterpolation();
				}
				ApplyAuthoritativeState();
			}
		}

		internal void ResetFixedTimeDelta()
		{
			if (m_UseRigidbodyForMotion && base.IsSpawned && !CanCommitToTransform)
			{
				m_DeltaFixedUpdateCached = m_CachedNetworkManager.RealTimeProvider.FixedDeltaTime;
				m_FixedTimeFrameDelta = 0f;
			}
		}

		public virtual void OnFixedUpdate()
		{
			if (m_UseRigidbodyForMotion && base.IsSpawned && !CanCommitToTransform)
			{
				m_NetworkRigidbodyInternal.WakeIfSleeping();
				if (Interpolate)
				{
					UpdateInterpolation();
				}
				ApplyAuthoritativeState();
				m_FixedTimeFrameDelta += m_DeltaFixedUpdateCached;
			}
		}

		protected virtual bool OnIsServerAuthoritative()
		{
			return AuthorityMode == global::Unity.Netcode.Components.NetworkTransform.AuthorityModes.Server;
		}

		public bool IsServerAuthoritative()
		{
			if ((bool)m_CachedNetworkManager && m_CachedNetworkManager.DistributedAuthorityMode)
			{
				return false;
			}
			return OnIsServerAuthoritative();
		}

		internal void TransformStateUpdate()
		{
			if (base.IsSpawned && !CanCommitToTransform)
			{
				m_OldState = m_LocalAuthoritativeNetworkState;
				m_LocalAuthoritativeNetworkState = InboundState;
				OnNetworkStateChanged(m_OldState, m_LocalAuthoritativeNetworkState);
			}
		}

		private void UpdateTransformState()
		{
			if (m_CachedNetworkManager.ShutdownInProgress || (m_CachedNetworkManager.DistributedAuthorityMode && !m_CachedNetworkManager.CMBServiceConnection && m_CachedNetworkObject.Observers.Count - 1 == 0))
			{
				return;
			}
			bool flag = IsServerAuthoritative();
			if (flag && !base.IsServer)
			{
				global::UnityEngine.Debug.LogError("Server authoritative NetworkTransform can only be updated by the server!");
			}
			else if (!flag && !base.IsServer && !base.IsOwner)
			{
				global::UnityEngine.Debug.LogError("Owner authoritative NetworkTransform can only be updated by the owner!");
			}
			m_OutboundMessage.NetworkTransform = this;
			global::Unity.Netcode.NetworkDelivery delivery = ((!(!UseUnreliableDeltas | m_LocalAuthoritativeNetworkState.FlagStates.IsTeleportingNextFrame | m_LocalAuthoritativeNetworkState.FlagStates.IsSynchronizing | m_LocalAuthoritativeNetworkState.FlagStates.UnreliableFrameSync | m_LocalAuthoritativeNetworkState.FlagStates.SynchronizeBaseHalfFloat)) ? global::Unity.Netcode.NetworkDelivery.UnreliableSequenced : global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.NetworkTransformMessage>.DefaultDelivery);
			if (base.IsServer)
			{
				int count = m_CachedNetworkManager.ConnectionManager.ConnectedClientsList.Count;
				for (int i = 0; i < count; i++)
				{
					ulong clientId = m_CachedNetworkManager.ConnectionManager.ConnectedClientsList[i].ClientId;
					if (clientId != 0L && base.NetworkObject.Observers.Contains(clientId))
					{
						base.NetworkManager.MessageManager.SendMessage(ref m_OutboundMessage, delivery, clientId);
					}
				}
			}
			else
			{
				base.NetworkManager.MessageManager.SendMessage(ref m_OutboundMessage, delivery, 0uL);
			}
			m_LocalAuthoritativeNetworkState.LastSerializedSize = m_OutboundMessage.BytesWritten;
		}

		internal static void UpdateNetworkTick(global::Unity.Netcode.NetworkManager networkManager)
		{
			if (s_NetworkTickRegistration.ContainsKey(networkManager))
			{
				s_NetworkTickRegistration[networkManager].TickUpdate();
			}
		}

		internal static float GetTickLatency(global::Unity.Netcode.NetworkManager networkManager)
		{
			if (networkManager.IsListening)
			{
				return (float)((double)(networkManager.NetworkTimeSystem.TickLatency + InterpolationBufferTickOffset) + networkManager.LocalTime.TickOffset);
			}
			return 0f;
		}

		public static float GetTickLatency()
		{
			return GetTickLatency(global::Unity.Netcode.NetworkManager.Singleton);
		}

		internal static float GetTickLatencyInSeconds(global::Unity.Netcode.NetworkManager networkManager)
		{
			if (networkManager.IsListening)
			{
				return (float)networkManager.LocalTime.TimeTicksAgo(networkManager.NetworkTimeSystem.TickLatency + InterpolationBufferTickOffset).Time;
			}
			return 0f;
		}

		public static float GetTickLatencyInSeconds()
		{
			return GetTickLatencyInSeconds(global::Unity.Netcode.NetworkManager.Singleton);
		}

		private static void RemoveTickUpdate(global::Unity.Netcode.NetworkManager networkManager)
		{
			s_NetworkTickRegistration.Remove(networkManager);
		}

		internal void RegisterForTickSynchronization()
		{
			s_TickSynchPosition++;
			m_NextTickSync = base.NetworkManager.ServerTime.Tick + s_TickSynchPosition % (int)base.NetworkManager.NetworkConfig.TickRate;
		}

		private static void RegisterNetworkManagerForTickUpdate(global::Unity.Netcode.NetworkManager networkManager)
		{
			if (!s_NetworkTickRegistration.ContainsKey(networkManager))
			{
				s_NetworkTickRegistration.Add(networkManager, new global::Unity.Netcode.Components.NetworkTransform.NetworkTransformTickRegistration(networkManager));
			}
		}

		private static void RegisterForTickUpdate(global::Unity.Netcode.Components.NetworkTransform networkTransform)
		{
			if (!networkTransform.NetworkManager.DistributedAuthorityMode && !s_NetworkTickRegistration.ContainsKey(networkTransform.NetworkManager))
			{
				s_NetworkTickRegistration.Add(networkTransform.NetworkManager, new global::Unity.Netcode.Components.NetworkTransform.NetworkTransformTickRegistration(networkTransform.NetworkManager));
			}
			networkTransform.RegisterForTickSynchronization();
			s_NetworkTickRegistration[networkTransform.NetworkManager].NetworkTransforms.Add(networkTransform);
		}

		private static void DeregisterForTickUpdate(global::Unity.Netcode.Components.NetworkTransform networkTransform)
		{
			if (!(networkTransform.NetworkManager == null) && s_NetworkTickRegistration.ContainsKey(networkTransform.NetworkManager))
			{
				s_NetworkTickRegistration[networkTransform.NetworkManager].NetworkTransforms.Remove(networkTransform);
				if (!networkTransform.NetworkManager.DistributedAuthorityMode && s_NetworkTickRegistration[networkTransform.NetworkManager].NetworkTransforms.Count == 0)
				{
					s_NetworkTickRegistration[networkTransform.NetworkManager].Remove();
				}
			}
		}

		protected override void __initializeVariables()
		{
			base.__initializeVariables();
		}

		protected override void __initializeRpcs()
		{
			__registerRpc(1045589154u, __rpc_handler_1045589154, "SetStateClientRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
			__registerRpc(2044052851u, __rpc_handler_2044052851, "SetStateServerRpc", global::Unity.Netcode.RpcInvokePermission.Owner);
			base.__initializeRpcs();
		}

		private static void __rpc_handler_1045589154(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::UnityEngine.Vector3 value);
				reader.ReadValueSafe(out global::UnityEngine.Quaternion value2);
				reader.ReadValueSafe(out global::UnityEngine.Vector3 value3);
				reader.ReadValueSafe(out bool value4, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkTransform)target).SetStateClientRpc(value, value2, value3, value4);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		private static void __rpc_handler_2044052851(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
			if ((object)networkManager != null && networkManager.IsListening)
			{
				reader.ReadValueSafe(out global::UnityEngine.Vector3 value);
				reader.ReadValueSafe(out global::UnityEngine.Quaternion value2);
				reader.ReadValueSafe(out global::UnityEngine.Vector3 value3);
				reader.ReadValueSafe(out bool value4, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
				((global::Unity.Netcode.Components.NetworkTransform)target).SetStateServerRpc(value, value2, value3, value4);
				target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			}
		}

		protected internal override string __getTypeName()
		{
			return "NetworkTransform";
		}
	}
}
