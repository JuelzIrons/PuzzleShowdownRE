namespace UnityEngine.InputSystem.LowLevel
{
	public struct InputStateBlock
	{
		public const uint InvalidOffset = uint.MaxValue;

		public const uint AutomaticOffset = 4294967294u;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatInvalid = new global::UnityEngine.InputSystem.Utilities.FourCC(0);

		internal const int kFormatInvalid = 0;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatBit = new global::UnityEngine.InputSystem.Utilities.FourCC('B', 'I', 'T');

		internal const int kFormatBit = 1112101920;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatSBit = new global::UnityEngine.InputSystem.Utilities.FourCC('S', 'B', 'I', 'T');

		internal const int kFormatSBit = 1396853076;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatInt = new global::UnityEngine.InputSystem.Utilities.FourCC('I', 'N', 'T');

		internal const int kFormatInt = 1229870112;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatUInt = new global::UnityEngine.InputSystem.Utilities.FourCC('U', 'I', 'N', 'T');

		internal const int kFormatUInt = 1430867540;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatShort = new global::UnityEngine.InputSystem.Utilities.FourCC('S', 'H', 'R', 'T');

		internal const int kFormatShort = 1397248596;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatUShort = new global::UnityEngine.InputSystem.Utilities.FourCC('U', 'S', 'H', 'T');

		internal const int kFormatUShort = 1431521364;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatByte = new global::UnityEngine.InputSystem.Utilities.FourCC('B', 'Y', 'T', 'E');

		internal const int kFormatByte = 1113150533;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatSByte = new global::UnityEngine.InputSystem.Utilities.FourCC('S', 'B', 'Y', 'T');

		internal const int kFormatSByte = 1396857172;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatLong = new global::UnityEngine.InputSystem.Utilities.FourCC('L', 'N', 'G');

		internal const int kFormatLong = 1280198432;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatULong = new global::UnityEngine.InputSystem.Utilities.FourCC('U', 'L', 'N', 'G');

		internal const int kFormatULong = 1431064135;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatFloat = new global::UnityEngine.InputSystem.Utilities.FourCC('F', 'L', 'T');

		internal const int kFormatFloat = 1179407392;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatDouble = new global::UnityEngine.InputSystem.Utilities.FourCC('D', 'B', 'L');

		internal const int kFormatDouble = 1145195552;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatVector2 = new global::UnityEngine.InputSystem.Utilities.FourCC('V', 'E', 'C', '2');

		internal const int kFormatVector2 = 1447379762;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatVector3 = new global::UnityEngine.InputSystem.Utilities.FourCC('V', 'E', 'C', '3');

		internal const int kFormatVector3 = 1447379763;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatQuaternion = new global::UnityEngine.InputSystem.Utilities.FourCC('Q', 'U', 'A', 'T');

		internal const int kFormatQuaternion = 1364541780;

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatVector2Short = new global::UnityEngine.InputSystem.Utilities.FourCC('V', 'C', '2', 'S');

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatVector3Short = new global::UnityEngine.InputSystem.Utilities.FourCC('V', 'C', '3', 'S');

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatVector2Byte = new global::UnityEngine.InputSystem.Utilities.FourCC('V', 'C', '2', 'B');

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatVector3Byte = new global::UnityEngine.InputSystem.Utilities.FourCC('V', 'C', '3', 'B');

		public static readonly global::UnityEngine.InputSystem.Utilities.FourCC FormatPose = new global::UnityEngine.InputSystem.Utilities.FourCC('P', 'o', 's', 'e');

		internal const int kFormatPose = 1349481317;

		internal uint m_ByteOffset;

		public global::UnityEngine.InputSystem.Utilities.FourCC format { get; set; }

		public uint byteOffset
		{
			get
			{
				return m_ByteOffset;
			}
			set
			{
				m_ByteOffset = value;
			}
		}

		public uint bitOffset { get; set; }

		public uint sizeInBits { get; set; }

		internal uint alignedSizeInBytes => sizeInBits + 7 >> 3;

		internal uint effectiveByteOffset => byteOffset + (bitOffset >> 3);

		internal uint effectiveBitOffset => byteOffset * 8 + bitOffset;

		public static int GetSizeOfPrimitiveFormatInBits(global::UnityEngine.InputSystem.Utilities.FourCC type)
		{
			if (type == FormatBit || type == FormatSBit)
			{
				return 1;
			}
			if (type == FormatInt || type == FormatUInt)
			{
				return 32;
			}
			if (type == FormatShort || type == FormatUShort)
			{
				return 16;
			}
			if (type == FormatByte || type == FormatSByte)
			{
				return 8;
			}
			if (type == FormatLong || type == FormatULong)
			{
				return 64;
			}
			if (type == FormatFloat)
			{
				return 32;
			}
			if (type == FormatDouble)
			{
				return 64;
			}
			if (type == FormatVector2)
			{
				return 64;
			}
			if (type == FormatVector3)
			{
				return 96;
			}
			if (type == FormatQuaternion)
			{
				return 128;
			}
			if (type == FormatVector2Short)
			{
				return 32;
			}
			if (type == FormatVector3Short)
			{
				return 48;
			}
			if (type == FormatVector2Byte)
			{
				return 16;
			}
			if (type == FormatVector3Byte)
			{
				return 24;
			}
			return -1;
		}

		public static global::UnityEngine.InputSystem.Utilities.FourCC GetPrimitiveFormatFromType(global::System.Type type)
		{
			if ((object)type == typeof(int))
			{
				return FormatInt;
			}
			if ((object)type == typeof(uint))
			{
				return FormatUInt;
			}
			if ((object)type == typeof(short))
			{
				return FormatShort;
			}
			if ((object)type == typeof(ushort))
			{
				return FormatUShort;
			}
			if ((object)type == typeof(byte))
			{
				return FormatByte;
			}
			if ((object)type == typeof(sbyte))
			{
				return FormatSByte;
			}
			if ((object)type == typeof(long))
			{
				return FormatLong;
			}
			if ((object)type == typeof(ulong))
			{
				return FormatULong;
			}
			if ((object)type == typeof(float))
			{
				return FormatFloat;
			}
			if ((object)type == typeof(double))
			{
				return FormatDouble;
			}
			if ((object)type == typeof(global::UnityEngine.Vector2))
			{
				return FormatVector2;
			}
			if ((object)type == typeof(global::UnityEngine.Vector3))
			{
				return FormatVector3;
			}
			if ((object)type == typeof(global::UnityEngine.Quaternion))
			{
				return FormatQuaternion;
			}
			return default(global::UnityEngine.InputSystem.Utilities.FourCC);
		}

		public unsafe int ReadInt(void* statePtr)
		{
			byte* ptr = (byte*)statePtr + (int)byteOffset;
			switch (format)
			{
			case 1112101920:
				if (sizeInBits == 1)
				{
					if (!global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr, bitOffset))
					{
						return 0;
					}
					return 1;
				}
				return (int)global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadMultipleBitsAsUInt(ptr, bitOffset, sizeInBits);
			case 1396853076:
				if (sizeInBits == 1)
				{
					if (!global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr, bitOffset))
					{
						return -1;
					}
					return 1;
				}
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadExcessKMultipleBitsAsInt(ptr, bitOffset, sizeInBits);
			case 1229870112:
			case 1430867540:
				_ = 1430867540;
				return *(int*)ptr;
			case 1397248596:
				return *(short*)ptr;
			case 1431521364:
				return *(ushort*)ptr;
			case 1113150533:
				return *ptr;
			case 1396857172:
				return *ptr;
			default:
				throw new global::System.InvalidOperationException($"State format '{format}' is not supported as integer format");
			}
		}

		public unsafe void WriteInt(void* statePtr, int value)
		{
			byte* ptr = (byte*)statePtr + (int)byteOffset;
			switch (format)
			{
			case 1112101920:
				if (sizeInBits == 1)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, bitOffset, value != 0);
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, sizeInBits, (uint)value);
				}
				break;
			case 1396853076:
				if (sizeInBits == 1)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, bitOffset, value > 0);
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteIntAsExcessKMultipleBits(ptr, bitOffset, sizeInBits, value);
				}
				break;
			case 1229870112:
			case 1430867540:
				*(int*)ptr = value;
				break;
			case 1397248596:
				*(short*)ptr = (short)value;
				break;
			case 1431521364:
				*(ushort*)ptr = (ushort)value;
				break;
			case 1113150533:
				*ptr = (byte)value;
				break;
			case 1396857172:
				*ptr = (byte)(sbyte)value;
				break;
			default:
				throw new global::System.Exception($"State format '{format}' is not supported as integer format");
			}
		}

		public unsafe float ReadFloat(void* statePtr)
		{
			byte* ptr = (byte*)statePtr + (int)byteOffset;
			switch (format)
			{
			case 1112101920:
				if (sizeInBits == 1)
				{
					if (!global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr, bitOffset))
					{
						return 0f;
					}
					return 1f;
				}
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadMultipleBitsAsNormalizedUInt(ptr, bitOffset, sizeInBits);
			case 1396853076:
				if (sizeInBits == 1)
				{
					if (!global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr, bitOffset))
					{
						return -1f;
					}
					return 1f;
				}
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadMultipleBitsAsNormalizedUInt(ptr, bitOffset, sizeInBits) * 2f - 1f;
			case 1229870112:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.IntToNormalizedFloat(*(int*)ptr, int.MinValue, int.MaxValue) * 2f - 1f;
			case 1430867540:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.UIntToNormalizedFloat(*(uint*)ptr, 0u, uint.MaxValue);
			case 1397248596:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.IntToNormalizedFloat(*(short*)ptr, -32768, 32767) * 2f - 1f;
			case 1431521364:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.UIntToNormalizedFloat(*(ushort*)ptr, 0u, 65535u);
			case 1113150533:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.UIntToNormalizedFloat(*ptr, 0u, 255u);
			case 1396857172:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.IntToNormalizedFloat(*ptr, -128, 127) * 2f - 1f;
			case 1179407392:
				return *(float*)ptr;
			case 1145195552:
				return (float)(*(double*)ptr);
			default:
				throw new global::System.InvalidOperationException($"State format '{format}' is not supported as floating-point format");
			}
		}

		public unsafe void WriteFloat(void* statePtr, float value)
		{
			byte* ptr = (byte*)statePtr + (int)byteOffset;
			switch (format)
			{
			case 1112101920:
				if (sizeInBits == 1)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, bitOffset, value >= 0.5f);
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteNormalizedUIntAsMultipleBits(ptr, bitOffset, sizeInBits, value);
				}
				break;
			case 1396853076:
				if (sizeInBits == 1)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, bitOffset, value >= 0f);
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteNormalizedUIntAsMultipleBits(ptr, bitOffset, sizeInBits, value * 0.5f + 0.5f);
				}
				break;
			case 1229870112:
				*(int*)ptr = global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
				break;
			case 1430867540:
				*(uint*)ptr = global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(value, 0u, uint.MaxValue);
				break;
			case 1397248596:
				*(short*)ptr = (short)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -32768, 32767);
				break;
			case 1431521364:
				*(ushort*)ptr = (ushort)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(value, 0u, 65535u);
				break;
			case 1113150533:
				*ptr = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(value, 0u, 255u);
				break;
			case 1396857172:
				*ptr = (byte)(sbyte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -128, 127);
				break;
			case 1179407392:
				*(float*)ptr = value;
				break;
			case 1145195552:
				*(double*)ptr = value;
				break;
			default:
				throw new global::System.Exception($"State format '{format}' is not supported as floating-point format");
			}
		}

		internal global::UnityEngine.InputSystem.Utilities.PrimitiveValue FloatToPrimitiveValue(float value)
		{
			switch (format)
			{
			case 1112101920:
				if (sizeInBits == 1)
				{
					return value >= 0.5f;
				}
				return (int)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(value, 0u, (uint)((1L << (int)sizeInBits) - 1));
			case 1396853076:
			{
				if (sizeInBits == 1)
				{
					return value >= 0f;
				}
				int intMinValue = (int)(-(1L << (int)(sizeInBits - 1)));
				int intMaxValue = (int)((1L << (int)(sizeInBits - 1)) - 1);
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt(value, intMinValue, intMaxValue);
			}
			case 1229870112:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
			case 1430867540:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(value, 0u, uint.MaxValue);
			case 1397248596:
				return (short)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -32768, 32767);
			case 1431521364:
				return (ushort)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(value, 0u, 65535u);
			case 1113150533:
				return (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(value, 0u, 255u);
			case 1396857172:
				return (sbyte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -128, 127);
			case 1179407392:
				return value;
			case 1145195552:
				return value;
			default:
				throw new global::System.Exception($"State format '{format}' is not supported as floating-point format");
			}
		}

		public unsafe double ReadDouble(void* statePtr)
		{
			byte* ptr = (byte*)statePtr + (int)byteOffset;
			switch (format)
			{
			case 1112101920:
				if (sizeInBits == 1)
				{
					return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr, bitOffset) ? 1f : 0f;
				}
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadMultipleBitsAsNormalizedUInt(ptr, bitOffset, sizeInBits);
			case 1396853076:
				if (sizeInBits == 1)
				{
					return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr, bitOffset) ? 1f : (-1f);
				}
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadMultipleBitsAsNormalizedUInt(ptr, bitOffset, sizeInBits) * 2f - 1f;
			case 1229870112:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.IntToNormalizedFloat(*(int*)ptr, int.MinValue, int.MaxValue) * 2f - 1f;
			case 1430867540:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.UIntToNormalizedFloat(*(uint*)ptr, 0u, uint.MaxValue);
			case 1397248596:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.IntToNormalizedFloat(*(short*)ptr, -32768, 32767) * 2f - 1f;
			case 1431521364:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.UIntToNormalizedFloat(*(ushort*)ptr, 0u, 65535u);
			case 1113150533:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.UIntToNormalizedFloat(*ptr, 0u, 255u);
			case 1396857172:
				return global::UnityEngine.InputSystem.Utilities.NumberHelpers.IntToNormalizedFloat(*ptr, -128, 127) * 2f - 1f;
			case 1179407392:
				return *(float*)ptr;
			case 1145195552:
				return *(double*)ptr;
			default:
				throw new global::System.Exception($"State format '{format}' is not supported as floating-point format");
			}
		}

		public unsafe void WriteDouble(void* statePtr, double value)
		{
			byte* ptr = (byte*)statePtr + (int)byteOffset;
			switch (format)
			{
			case 1112101920:
				if (sizeInBits == 1)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, bitOffset, value >= 0.5);
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteNormalizedUIntAsMultipleBits(ptr, bitOffset, sizeInBits, (float)value);
				}
				break;
			case 1396853076:
				if (sizeInBits == 1)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, bitOffset, value >= 0.0);
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteNormalizedUIntAsMultipleBits(ptr, bitOffset, sizeInBits, (float)value * 0.5f + 0.5f);
				}
				break;
			case 1229870112:
				*(int*)ptr = global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
				break;
			case 1430867540:
				*(uint*)ptr = global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt((float)value, 0u, uint.MaxValue);
				break;
			case 1397248596:
				*(short*)ptr = (short)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, -32768, 32767);
				break;
			case 1431521364:
				*(ushort*)ptr = (ushort)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt((float)value, 0u, 65535u);
				break;
			case 1113150533:
				*ptr = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt((float)value, 0u, 255u);
				break;
			case 1396857172:
				*ptr = (byte)(sbyte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, -128, 127);
				break;
			case 1179407392:
				*(float*)ptr = (float)value;
				break;
			case 1145195552:
				*(double*)ptr = value;
				break;
			default:
				throw new global::System.InvalidOperationException($"State format '{format}' is not supported as floating-point format");
			}
		}

		public unsafe void Write(void* statePtr, global::UnityEngine.InputSystem.Utilities.PrimitiveValue value)
		{
			byte* ptr = (byte*)statePtr + (int)byteOffset;
			switch (format)
			{
			case 1112101920:
				if (sizeInBits == 1)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, bitOffset, value.ToBoolean());
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteUIntAsMultipleBits(ptr, bitOffset, sizeInBits, value.ToUInt32());
				}
				break;
			case 1396853076:
				if (sizeInBits == 1)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, bitOffset, value.ToBoolean());
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteIntAsExcessKMultipleBits(ptr, bitOffset, sizeInBits, value.ToInt32());
				}
				break;
			case 1229870112:
				*(int*)ptr = value.ToInt32();
				break;
			case 1430867540:
				*(uint*)ptr = value.ToUInt32();
				break;
			case 1397248596:
				*(short*)ptr = value.ToInt16();
				break;
			case 1431521364:
				*(ushort*)ptr = value.ToUInt16();
				break;
			case 1113150533:
				*ptr = value.ToByte();
				break;
			case 1396857172:
				*ptr = (byte)value.ToSByte();
				break;
			case 1179407392:
				*(float*)ptr = value.ToSingle();
				break;
			default:
				throw new global::System.NotImplementedException($"Writing primitive value of type '{value.type}' into state block with format '{format}'");
			}
		}

		public unsafe void CopyToFrom(void* toStatePtr, void* fromStatePtr)
		{
			if (bitOffset != 0 || sizeInBits % 8 != 0)
			{
				throw new global::System.NotImplementedException("Copying bitfields");
			}
			byte* source = (byte*)fromStatePtr + byteOffset;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy((byte*)toStatePtr + byteOffset, source, alignedSizeInBytes);
		}
	}
}
