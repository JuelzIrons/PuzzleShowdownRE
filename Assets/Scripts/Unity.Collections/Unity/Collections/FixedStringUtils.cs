namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal static class FixedStringUtils
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		internal struct UintFloatUnion
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public uint uintValue;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public float floatValue;
		}

		internal static global::Unity.Collections.ParseError Base10ToBase2(ref float output, ulong mantissa10, int exponent10)
		{
			if (mantissa10 == 0L)
			{
				output = 0f;
				return global::Unity.Collections.ParseError.None;
			}
			if (exponent10 == 0)
			{
				output = mantissa10;
				return global::Unity.Collections.ParseError.None;
			}
			int num = exponent10;
			ulong num2 = mantissa10;
			while (exponent10 > 0)
			{
				while ((num2 & 0xE000000000000000uL) != 0L)
				{
					num2 >>= 1;
					num++;
				}
				num2 *= 5;
				exponent10--;
			}
			while (exponent10 < 0)
			{
				while ((num2 & 0x8000000000000000uL) == 0L)
				{
					num2 <<= 1;
					num--;
				}
				num2 /= 5;
				exponent10++;
			}
			global::Unity.Collections.FixedStringUtils.UintFloatUnion uintFloatUnion = new global::Unity.Collections.FixedStringUtils.UintFloatUnion
			{
				floatValue = num2
			};
			int num3 = (int)(((uintFloatUnion.uintValue >> 23) & 0xFF) - 127);
			num3 += num;
			if (num3 > 128)
			{
				return global::Unity.Collections.ParseError.Overflow;
			}
			if (num3 < -127)
			{
				return global::Unity.Collections.ParseError.Underflow;
			}
			uintFloatUnion.uintValue = (uintFloatUnion.uintValue & 0x807FFFFFu) | (uint)(num3 + 127 << 23);
			output = uintFloatUnion.floatValue;
			return global::Unity.Collections.ParseError.None;
		}

		internal static void Base2ToBase10(ref ulong mantissa10, ref int exponent10, float input)
		{
			global::Unity.Collections.FixedStringUtils.UintFloatUnion uintFloatUnion = new global::Unity.Collections.FixedStringUtils.UintFloatUnion
			{
				floatValue = input
			};
			if (uintFloatUnion.uintValue == 0)
			{
				mantissa10 = 0uL;
				exponent10 = 0;
				return;
			}
			uint num = (uintFloatUnion.uintValue & 0x7FFFFF) | 0x800000;
			int i = (int)((uintFloatUnion.uintValue >> 23) - 127 - 23);
			mantissa10 = num;
			exponent10 = i;
			if (i > 0)
			{
				while (i > 0)
				{
					while (mantissa10 <= 1844674407370955161L)
					{
						mantissa10 *= 10uL;
						exponent10--;
					}
					mantissa10 /= 5uL;
					i--;
				}
			}
			if (i < 0)
			{
				for (; i < 0; i++)
				{
					while (mantissa10 > 3689348814741910323L)
					{
						mantissa10 /= 10uL;
						exponent10++;
					}
					mantissa10 *= 5uL;
				}
			}
			while (mantissa10 > 9999999 || mantissa10 % 10 == 0L)
			{
				mantissa10 = (mantissa10 + (uint)((mantissa10 < 100000000) ? 5 : 0)) / 10;
				exponent10++;
			}
		}
	}
}
