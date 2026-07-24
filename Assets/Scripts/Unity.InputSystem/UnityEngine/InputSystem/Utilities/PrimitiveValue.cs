namespace UnityEngine.InputSystem.Utilities
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct PrimitiveValue : global::System.IEquatable<global::UnityEngine.InputSystem.Utilities.PrimitiveValue>, global::System.IConvertible
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private global::System.TypeCode m_Type;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private bool m_BoolValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private char m_CharValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private byte m_ByteValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private sbyte m_SByteValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private short m_ShortValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private ushort m_UShortValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private int m_IntValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private uint m_UIntValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private long m_LongValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private ulong m_ULongValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private float m_FloatValue;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private double m_DoubleValue;

		internal unsafe byte* valuePtr => (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref this) + 4;

		public global::System.TypeCode type => m_Type;

		public bool isEmpty => type == global::System.TypeCode.Empty;

		public PrimitiveValue(bool value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.Boolean;
			m_BoolValue = value;
		}

		public PrimitiveValue(char value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.Char;
			m_CharValue = value;
		}

		public PrimitiveValue(byte value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.Byte;
			m_ByteValue = value;
		}

		public PrimitiveValue(sbyte value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.SByte;
			m_SByteValue = value;
		}

		public PrimitiveValue(short value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.Int16;
			m_ShortValue = value;
		}

		public PrimitiveValue(ushort value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.UInt16;
			m_UShortValue = value;
		}

		public PrimitiveValue(int value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.Int32;
			m_IntValue = value;
		}

		public PrimitiveValue(uint value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.UInt32;
			m_UIntValue = value;
		}

		public PrimitiveValue(long value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.Int64;
			m_LongValue = value;
		}

		public PrimitiveValue(ulong value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.UInt64;
			m_ULongValue = value;
		}

		public PrimitiveValue(float value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.Single;
			m_FloatValue = value;
		}

		public PrimitiveValue(double value)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			m_Type = global::System.TypeCode.Double;
			m_DoubleValue = value;
		}

		public global::UnityEngine.InputSystem.Utilities.PrimitiveValue ConvertTo(global::System.TypeCode type)
		{
			return type switch
			{
				global::System.TypeCode.Boolean => ToBoolean(), 
				global::System.TypeCode.Char => ToChar(), 
				global::System.TypeCode.Byte => ToByte(), 
				global::System.TypeCode.SByte => ToSByte(), 
				global::System.TypeCode.Int16 => ToInt16(), 
				global::System.TypeCode.Int32 => ToInt32(), 
				global::System.TypeCode.Int64 => ToInt64(), 
				global::System.TypeCode.UInt16 => ToInt16(), 
				global::System.TypeCode.UInt32 => ToInt32(), 
				global::System.TypeCode.UInt64 => ToUInt64(), 
				global::System.TypeCode.Single => ToSingle(), 
				global::System.TypeCode.Double => ToDouble(), 
				global::System.TypeCode.Empty => default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue), 
				_ => throw new global::System.ArgumentException($"Don't know how to convert PrimitiveValue to '{type}'", "type"), 
			};
		}

		public unsafe bool Equals(global::UnityEngine.InputSystem.Utilities.PrimitiveValue other)
		{
			if (m_Type != other.m_Type)
			{
				return false;
			}
			void* ptr = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref m_DoubleValue);
			void* ptr2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref other.m_DoubleValue);
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr, ptr2, 8L) == 0;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.InputSystem.Utilities.PrimitiveValue other)
			{
				return Equals(other);
			}
			if (obj is bool || obj is char || obj is byte || obj is sbyte || obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is float || obj is double)
			{
				return Equals(FromObject(obj));
			}
			return false;
		}

		public static bool operator ==(global::UnityEngine.InputSystem.Utilities.PrimitiveValue left, global::UnityEngine.InputSystem.Utilities.PrimitiveValue right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.InputSystem.Utilities.PrimitiveValue left, global::UnityEngine.InputSystem.Utilities.PrimitiveValue right)
		{
			return !left.Equals(right);
		}

		public unsafe override int GetHashCode()
		{
			fixed (double* doubleValue = &m_DoubleValue)
			{
				return (m_Type.GetHashCode() * 397) ^ doubleValue->GetHashCode();
			}
		}

		public override string ToString()
		{
			switch (type)
			{
			case global::System.TypeCode.Boolean:
				if (!m_BoolValue)
				{
					return "false";
				}
				return "true";
			case global::System.TypeCode.Char:
				return "'" + m_CharValue + "'";
			case global::System.TypeCode.Byte:
				return m_ByteValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			case global::System.TypeCode.SByte:
				return m_SByteValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			case global::System.TypeCode.Int16:
				return m_ShortValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			case global::System.TypeCode.UInt16:
				return m_UShortValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			case global::System.TypeCode.Int32:
				return m_IntValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			case global::System.TypeCode.UInt32:
				return m_UIntValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			case global::System.TypeCode.Int64:
				return m_LongValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			case global::System.TypeCode.UInt64:
				return m_ULongValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			case global::System.TypeCode.Single:
				return m_FloatValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			case global::System.TypeCode.Double:
				return m_DoubleValue.ToString(global::System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			default:
				return string.Empty;
			}
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			}
			if (value.Equals("true", global::System.StringComparison.InvariantCultureIgnoreCase))
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value: true);
			}
			if (value.Equals("false", global::System.StringComparison.InvariantCultureIgnoreCase))
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value: false);
			}
			if ((value.Contains('.') || value.Contains("e") || value.Contains("E") || value.Contains("infinity", global::System.StringComparison.InvariantCultureIgnoreCase)) && double.TryParse(value, global::System.Globalization.NumberStyles.Float, global::System.Globalization.CultureInfo.InvariantCulture, out var result))
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(result);
			}
			if (long.TryParse(value, global::System.Globalization.NumberStyles.Integer, global::System.Globalization.CultureInfo.InvariantCulture, out var result2))
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(result2);
			}
			if (value.IndexOf("0x", global::System.StringComparison.InvariantCultureIgnoreCase) != -1)
			{
				string text = value.TrimStart();
				if (text.StartsWith("0x"))
				{
					text = text.Substring(2);
				}
				if (long.TryParse(text, global::System.Globalization.NumberStyles.HexNumber, global::System.Globalization.CultureInfo.InvariantCulture, out var result3))
				{
					return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(result3);
				}
			}
			throw new global::System.NotImplementedException();
		}

		public global::System.TypeCode GetTypeCode()
		{
			return type;
		}

		public bool ToBoolean(global::System.IFormatProvider provider = null)
		{
			return type switch
			{
				global::System.TypeCode.Boolean => m_BoolValue, 
				global::System.TypeCode.Char => m_CharValue != '\0', 
				global::System.TypeCode.Byte => m_ByteValue != 0, 
				global::System.TypeCode.SByte => m_SByteValue != 0, 
				global::System.TypeCode.Int16 => m_ShortValue != 0, 
				global::System.TypeCode.UInt16 => m_UShortValue != 0, 
				global::System.TypeCode.Int32 => m_IntValue != 0, 
				global::System.TypeCode.UInt32 => m_UIntValue != 0, 
				global::System.TypeCode.Int64 => m_LongValue != 0, 
				global::System.TypeCode.UInt64 => m_ULongValue != 0, 
				global::System.TypeCode.Single => !global::UnityEngine.Mathf.Approximately(m_FloatValue, 0f), 
				global::System.TypeCode.Double => !global::UnityEngine.InputSystem.Utilities.NumberHelpers.Approximately(m_DoubleValue, 0.0), 
				_ => false, 
			};
		}

		public byte ToByte(global::System.IFormatProvider provider = null)
		{
			return (byte)ToInt64(provider);
		}

		public char ToChar(global::System.IFormatProvider provider = null)
		{
			switch (type)
			{
			case global::System.TypeCode.Char:
				return m_CharValue;
			case global::System.TypeCode.Int16:
			case global::System.TypeCode.UInt16:
			case global::System.TypeCode.Int32:
			case global::System.TypeCode.UInt32:
			case global::System.TypeCode.Int64:
			case global::System.TypeCode.UInt64:
				return (char)ToInt64(provider);
			default:
				return '\0';
			}
		}

		public global::System.DateTime ToDateTime(global::System.IFormatProvider provider = null)
		{
			throw new global::System.NotSupportedException("Converting PrimitiveValue to DateTime");
		}

		public decimal ToDecimal(global::System.IFormatProvider provider = null)
		{
			return new decimal(ToDouble(provider));
		}

		public double ToDouble(global::System.IFormatProvider provider = null)
		{
			switch (type)
			{
			case global::System.TypeCode.Boolean:
				if (m_BoolValue)
				{
					return 1.0;
				}
				return 0.0;
			case global::System.TypeCode.Char:
				return (int)m_CharValue;
			case global::System.TypeCode.Byte:
				return (int)m_ByteValue;
			case global::System.TypeCode.SByte:
				return m_SByteValue;
			case global::System.TypeCode.Int16:
				return m_ShortValue;
			case global::System.TypeCode.UInt16:
				return (int)m_UShortValue;
			case global::System.TypeCode.Int32:
				return m_IntValue;
			case global::System.TypeCode.UInt32:
				return m_UIntValue;
			case global::System.TypeCode.Int64:
				return m_LongValue;
			case global::System.TypeCode.UInt64:
				return m_ULongValue;
			case global::System.TypeCode.Single:
				return m_FloatValue;
			case global::System.TypeCode.Double:
				return m_DoubleValue;
			default:
				return 0.0;
			}
		}

		public short ToInt16(global::System.IFormatProvider provider = null)
		{
			return (short)ToInt64(provider);
		}

		public int ToInt32(global::System.IFormatProvider provider = null)
		{
			return (int)ToInt64(provider);
		}

		public long ToInt64(global::System.IFormatProvider provider = null)
		{
			switch (type)
			{
			case global::System.TypeCode.Boolean:
				if (m_BoolValue)
				{
					return 1L;
				}
				return 0L;
			case global::System.TypeCode.Char:
				return m_CharValue;
			case global::System.TypeCode.Byte:
				return m_ByteValue;
			case global::System.TypeCode.SByte:
				return m_SByteValue;
			case global::System.TypeCode.Int16:
				return m_ShortValue;
			case global::System.TypeCode.UInt16:
				return m_UShortValue;
			case global::System.TypeCode.Int32:
				return m_IntValue;
			case global::System.TypeCode.UInt32:
				return m_UIntValue;
			case global::System.TypeCode.Int64:
				return m_LongValue;
			case global::System.TypeCode.UInt64:
				return (long)m_ULongValue;
			case global::System.TypeCode.Single:
				return (long)m_FloatValue;
			case global::System.TypeCode.Double:
				return (long)m_DoubleValue;
			default:
				return 0L;
			}
		}

		public sbyte ToSByte(global::System.IFormatProvider provider = null)
		{
			return (sbyte)ToInt64(provider);
		}

		public float ToSingle(global::System.IFormatProvider provider = null)
		{
			return (float)ToDouble(provider);
		}

		public string ToString(global::System.IFormatProvider provider)
		{
			return ToString();
		}

		public object ToType(global::System.Type conversionType, global::System.IFormatProvider provider)
		{
			throw new global::System.NotSupportedException();
		}

		public ushort ToUInt16(global::System.IFormatProvider provider = null)
		{
			return (ushort)ToUInt64();
		}

		public uint ToUInt32(global::System.IFormatProvider provider = null)
		{
			return (uint)ToUInt64();
		}

		public ulong ToUInt64(global::System.IFormatProvider provider = null)
		{
			switch (type)
			{
			case global::System.TypeCode.Boolean:
				if (m_BoolValue)
				{
					return 1uL;
				}
				return 0uL;
			case global::System.TypeCode.Char:
				return m_CharValue;
			case global::System.TypeCode.Byte:
				return m_ByteValue;
			case global::System.TypeCode.SByte:
				return (ulong)m_SByteValue;
			case global::System.TypeCode.Int16:
				return (ulong)m_ShortValue;
			case global::System.TypeCode.UInt16:
				return m_UShortValue;
			case global::System.TypeCode.Int32:
				return (ulong)m_IntValue;
			case global::System.TypeCode.UInt32:
				return m_UIntValue;
			case global::System.TypeCode.Int64:
				return (ulong)m_LongValue;
			case global::System.TypeCode.UInt64:
				return m_ULongValue;
			case global::System.TypeCode.Single:
				return (ulong)m_FloatValue;
			case global::System.TypeCode.Double:
				return (ulong)m_DoubleValue;
			default:
				return 0uL;
			}
		}

		public object ToObject()
		{
			return m_Type switch
			{
				global::System.TypeCode.Boolean => m_BoolValue, 
				global::System.TypeCode.Char => m_CharValue, 
				global::System.TypeCode.Byte => m_ByteValue, 
				global::System.TypeCode.SByte => m_SByteValue, 
				global::System.TypeCode.Int16 => m_ShortValue, 
				global::System.TypeCode.UInt16 => m_UShortValue, 
				global::System.TypeCode.Int32 => m_IntValue, 
				global::System.TypeCode.UInt32 => m_UIntValue, 
				global::System.TypeCode.Int64 => m_LongValue, 
				global::System.TypeCode.UInt64 => m_ULongValue, 
				global::System.TypeCode.Single => m_FloatValue, 
				global::System.TypeCode.Double => m_DoubleValue, 
				_ => null, 
			};
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue From<TValue>(TValue value) where TValue : struct
		{
			global::System.Type type = typeof(TValue);
			if (type.IsEnum)
			{
				type = type.GetEnumUnderlyingType();
			}
			return global::System.Type.GetTypeCode(type) switch
			{
				global::System.TypeCode.Boolean => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToBoolean(value)), 
				global::System.TypeCode.Char => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToChar(value)), 
				global::System.TypeCode.Byte => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToByte(value)), 
				global::System.TypeCode.SByte => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToSByte(value)), 
				global::System.TypeCode.Int16 => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToInt16(value)), 
				global::System.TypeCode.Int32 => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToInt32(value)), 
				global::System.TypeCode.Int64 => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToInt64(value)), 
				global::System.TypeCode.UInt16 => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToUInt16(value)), 
				global::System.TypeCode.UInt32 => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToUInt32(value)), 
				global::System.TypeCode.UInt64 => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToUInt64(value)), 
				global::System.TypeCode.Single => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToSingle(value)), 
				global::System.TypeCode.Double => new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(global::System.Convert.ToDouble(value)), 
				_ => throw new global::System.ArgumentException($"Cannot convert value '{value}' of type '{typeof(TValue).Name}' to PrimitiveValue", "value"), 
			};
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromObject(object value)
		{
			if (value == null)
			{
				return default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			}
			if (value is string value2)
			{
				return FromString(value2);
			}
			if (value is bool value3)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value3);
			}
			if (value is char value4)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value4);
			}
			if (value is byte value5)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value5);
			}
			if (value is sbyte value6)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value6);
			}
			if (value is short value7)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value7);
			}
			if (value is ushort value8)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value8);
			}
			if (value is int value9)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value9);
			}
			if (value is uint value10)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value10);
			}
			if (value is long value11)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value11);
			}
			if (value is ulong value12)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value12);
			}
			if (value is float value13)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value13);
			}
			if (value is double value14)
			{
				return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value14);
			}
			if (value is global::System.Enum)
			{
				switch (global::System.Type.GetTypeCode(value.GetType().GetEnumUnderlyingType()))
				{
				case global::System.TypeCode.Byte:
					return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue((byte)value);
				case global::System.TypeCode.SByte:
					return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue((sbyte)value);
				case global::System.TypeCode.Int16:
					return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue((short)value);
				case global::System.TypeCode.Int32:
					return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue((int)value);
				case global::System.TypeCode.Int64:
					return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue((long)value);
				case global::System.TypeCode.UInt16:
					return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue((ushort)value);
				case global::System.TypeCode.UInt32:
					return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue((uint)value);
				case global::System.TypeCode.UInt64:
					return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue((ulong)value);
				}
			}
			throw new global::System.ArgumentException($"Cannot convert '{value}' to primitive value", "value");
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(bool value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(char value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(byte value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(sbyte value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(short value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(ushort value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(int value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(uint value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(long value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(ulong value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(float value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.PrimitiveValue(double value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromBoolean(bool value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromChar(char value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromByte(byte value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromSByte(sbyte value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromInt16(short value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromUInt16(ushort value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromInt32(int value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromUInt32(uint value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromInt64(long value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromUInt64(ulong value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromSingle(float value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue FromDouble(double value)
		{
			return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(value);
		}
	}
}
