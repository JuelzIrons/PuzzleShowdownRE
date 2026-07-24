namespace Unity.Collections
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct Unicode
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility]
		public struct Rune
		{
			public int value;

			public Rune(int codepoint)
			{
				value = codepoint;
			}

			public static implicit operator global::Unity.Collections.Unicode.Rune(char codepoint)
			{
				return new global::Unity.Collections.Unicode.Rune
				{
					value = codepoint
				};
			}

			public static bool operator ==(global::Unity.Collections.Unicode.Rune lhs, global::Unity.Collections.Unicode.Rune rhs)
			{
				return lhs.value == rhs.value;
			}

			[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed object")]
			public override bool Equals(object obj)
			{
				if (obj is global::Unity.Collections.Unicode.Rune)
				{
					return value == ((global::Unity.Collections.Unicode.Rune)obj).value;
				}
				return false;
			}

			public override int GetHashCode()
			{
				return value;
			}

			public static bool operator !=(global::Unity.Collections.Unicode.Rune lhs, global::Unity.Collections.Unicode.Rune rhs)
			{
				return lhs.value != rhs.value;
			}

			public static bool IsDigit(global::Unity.Collections.Unicode.Rune r)
			{
				return r.IsDigit();
			}

			internal bool IsAscii()
			{
				return value < 128;
			}

			internal bool IsLatin1()
			{
				return value < 256;
			}

			internal bool IsDigit()
			{
				if (value >= 48)
				{
					return value <= 57;
				}
				return false;
			}

			internal bool IsWhiteSpace()
			{
				if (IsLatin1())
				{
					if (value != 32 && (value < 9 || value > 13) && value != 160)
					{
						return value == 133;
					}
					return true;
				}
				if (value != 5760 && (value < 8192 || value > 8202) && value != 8232 && value != 8233 && value != 8239 && value != 8287)
				{
					return value == 12288;
				}
				return true;
			}

			internal global::Unity.Collections.Unicode.Rune ToLowerAscii()
			{
				return new global::Unity.Collections.Unicode.Rune(value + (((uint)(value - 65) <= 25u) ? 32 : 0));
			}

			internal global::Unity.Collections.Unicode.Rune ToUpperAscii()
			{
				return new global::Unity.Collections.Unicode.Rune(value - (((uint)(value - 97) <= 25u) ? 32 : 0));
			}

			public int LengthInUtf8Bytes()
			{
				if (value < 0)
				{
					return 4;
				}
				if (value <= 127)
				{
					return 1;
				}
				if (value <= 2047)
				{
					return 2;
				}
				if (value <= 65535)
				{
					return 3;
				}
				_ = value;
				_ = 2097151;
				return 4;
			}
		}

		public const int kMaximumValidCodePoint = 1114111;

		public static global::Unity.Collections.Unicode.Rune ReplacementCharacter => new global::Unity.Collections.Unicode.Rune
		{
			value = 65533
		};

		public static global::Unity.Collections.Unicode.Rune BadRune => new global::Unity.Collections.Unicode.Rune
		{
			value = 0
		};

		public static bool IsValidCodePoint(int codepoint)
		{
			if (codepoint > 1114111)
			{
				return false;
			}
			if (codepoint < 0)
			{
				return false;
			}
			return true;
		}

		public static bool NotTrailer(byte b)
		{
			return (b & 0xC0) != 128;
		}

		public unsafe static global::Unity.Collections.ConversionError Utf8ToUcs(out global::Unity.Collections.Unicode.Rune rune, byte* buffer, ref int index, int capacity)
		{
			int num = 0;
			rune = ReplacementCharacter;
			if (index + 1 > capacity)
			{
				return global::Unity.Collections.ConversionError.Overflow;
			}
			if ((buffer[index] & 0x80) == 0)
			{
				rune.value = buffer[index];
				index++;
				return global::Unity.Collections.ConversionError.None;
			}
			if ((buffer[index] & 0xE0) == 192)
			{
				if (index + 2 > capacity)
				{
					index++;
					return global::Unity.Collections.ConversionError.Overflow;
				}
				num = buffer[index] & 0x1F;
				num = (num << 6) | (buffer[index + 1] & 0x3F);
				if (num < 128 || NotTrailer(buffer[index + 1]))
				{
					index++;
					return global::Unity.Collections.ConversionError.Encoding;
				}
				rune.value = num;
				index += 2;
				return global::Unity.Collections.ConversionError.None;
			}
			if ((buffer[index] & 0xF0) == 224)
			{
				if (index + 3 > capacity)
				{
					index++;
					return global::Unity.Collections.ConversionError.Overflow;
				}
				num = buffer[index] & 0xF;
				num = (num << 6) | (buffer[index + 1] & 0x3F);
				num = (num << 6) | (buffer[index + 2] & 0x3F);
				if (num < 2048 || !IsValidCodePoint(num) || NotTrailer(buffer[index + 1]) || NotTrailer(buffer[index + 2]))
				{
					index++;
					return global::Unity.Collections.ConversionError.Encoding;
				}
				rune.value = num;
				index += 3;
				return global::Unity.Collections.ConversionError.None;
			}
			if ((buffer[index] & 0xF8) == 240)
			{
				if (index + 4 > capacity)
				{
					index++;
					return global::Unity.Collections.ConversionError.Overflow;
				}
				num = buffer[index] & 7;
				num = (num << 6) | (buffer[index + 1] & 0x3F);
				num = (num << 6) | (buffer[index + 2] & 0x3F);
				num = (num << 6) | (buffer[index + 3] & 0x3F);
				if (num < 65536 || !IsValidCodePoint(num) || NotTrailer(buffer[index + 1]) || NotTrailer(buffer[index + 2]) || NotTrailer(buffer[index + 3]))
				{
					index++;
					return global::Unity.Collections.ConversionError.Encoding;
				}
				rune.value = num;
				index += 4;
				return global::Unity.Collections.ConversionError.None;
			}
			index++;
			return global::Unity.Collections.ConversionError.Encoding;
		}

		private unsafe static int FindUtf8CharStartInReverse(byte* ptr, ref int index)
		{
			do
			{
				if (index <= 0)
				{
					return 0;
				}
				index--;
			}
			while ((ptr[index] & 0xC0) == 128);
			return index;
		}

		internal unsafe static global::Unity.Collections.ConversionError Utf8ToUcsReverse(out global::Unity.Collections.Unicode.Rune rune, byte* buffer, ref int index, int capacity)
		{
			int num = index;
			index--;
			index = FindUtf8CharStartInReverse(buffer, ref index);
			if (index == num)
			{
				rune = ReplacementCharacter;
				return global::Unity.Collections.ConversionError.Overflow;
			}
			int index2 = index;
			return Utf8ToUcs(out rune, buffer, ref index2, capacity);
		}

		private static bool IsLeadingSurrogate(char c)
		{
			if (c >= '\ud800')
			{
				return c <= '\udbff';
			}
			return false;
		}

		private static bool IsTrailingSurrogate(char c)
		{
			if (c >= '\udc00')
			{
				return c <= '\udfff';
			}
			return false;
		}

		public unsafe static global::Unity.Collections.ConversionError Utf16ToUcs(out global::Unity.Collections.Unicode.Rune rune, char* buffer, ref int index, int capacity)
		{
			int num = 0;
			rune = ReplacementCharacter;
			if (index + 1 > capacity)
			{
				return global::Unity.Collections.ConversionError.Overflow;
			}
			if (!IsLeadingSurrogate(buffer[index]) || index + 2 > capacity)
			{
				rune.value = buffer[index];
				index++;
				return global::Unity.Collections.ConversionError.None;
			}
			num = buffer[index] & 0x3FF;
			if (!IsTrailingSurrogate(buffer[index + 1]))
			{
				rune.value = buffer[index];
				index++;
				return global::Unity.Collections.ConversionError.None;
			}
			num = (num << 10) | (buffer[index + 1] & 0x3FF);
			num += 65536;
			rune.value = num;
			index += 2;
			return global::Unity.Collections.ConversionError.None;
		}

		internal unsafe static global::Unity.Collections.ConversionError UcsToUcs(out global::Unity.Collections.Unicode.Rune rune, global::Unity.Collections.Unicode.Rune* buffer, ref int index, int capacity)
		{
			rune = ReplacementCharacter;
			if (index + 1 > capacity)
			{
				return global::Unity.Collections.ConversionError.Overflow;
			}
			rune = buffer[index];
			index++;
			return global::Unity.Collections.ConversionError.None;
		}

		public unsafe static global::Unity.Collections.ConversionError UcsToUtf8(byte* buffer, ref int index, int capacity, global::Unity.Collections.Unicode.Rune rune)
		{
			if (!IsValidCodePoint(rune.value))
			{
				return global::Unity.Collections.ConversionError.CodePoint;
			}
			if (index + 1 > capacity)
			{
				return global::Unity.Collections.ConversionError.Overflow;
			}
			if (rune.value <= 127)
			{
				buffer[index++] = (byte)rune.value;
				return global::Unity.Collections.ConversionError.None;
			}
			if (rune.value <= 2047)
			{
				if (index + 2 > capacity)
				{
					return global::Unity.Collections.ConversionError.Overflow;
				}
				buffer[index++] = (byte)(0xC0 | (rune.value >> 6));
				buffer[index++] = (byte)(0x80 | (rune.value & 0x3F));
				return global::Unity.Collections.ConversionError.None;
			}
			if (rune.value <= 65535)
			{
				if (index + 3 > capacity)
				{
					return global::Unity.Collections.ConversionError.Overflow;
				}
				buffer[index++] = (byte)(0xE0 | (rune.value >> 12));
				buffer[index++] = (byte)(0x80 | ((rune.value >> 6) & 0x3F));
				buffer[index++] = (byte)(0x80 | (rune.value & 0x3F));
				return global::Unity.Collections.ConversionError.None;
			}
			if (rune.value <= 2097151)
			{
				if (index + 4 > capacity)
				{
					return global::Unity.Collections.ConversionError.Overflow;
				}
				buffer[index++] = (byte)(0xF0 | (rune.value >> 18));
				buffer[index++] = (byte)(0x80 | ((rune.value >> 12) & 0x3F));
				buffer[index++] = (byte)(0x80 | ((rune.value >> 6) & 0x3F));
				buffer[index++] = (byte)(0x80 | (rune.value & 0x3F));
				return global::Unity.Collections.ConversionError.None;
			}
			return global::Unity.Collections.ConversionError.Encoding;
		}

		public unsafe static global::Unity.Collections.ConversionError UcsToUtf16(char* buffer, ref int index, int capacity, global::Unity.Collections.Unicode.Rune rune)
		{
			if (!IsValidCodePoint(rune.value))
			{
				return global::Unity.Collections.ConversionError.CodePoint;
			}
			if (index + 1 > capacity)
			{
				return global::Unity.Collections.ConversionError.Overflow;
			}
			if (rune.value >= 65536)
			{
				if (index + 2 > capacity)
				{
					return global::Unity.Collections.ConversionError.Overflow;
				}
				int num = rune.value - 65536;
				if (num >= 1048576)
				{
					return global::Unity.Collections.ConversionError.Encoding;
				}
				buffer[index++] = (char)(0xD800 | (num >> 10));
				buffer[index++] = (char)(0xDC00 | (num & 0x3FF));
				return global::Unity.Collections.ConversionError.None;
			}
			buffer[index++] = (char)rune.value;
			return global::Unity.Collections.ConversionError.None;
		}

		public unsafe static global::Unity.Collections.ConversionError Utf16ToUtf8(char* utf16Buffer, int utf16Length, byte* utf8Buffer, out int utf8Length, int utf8Capacity)
		{
			utf8Length = 0;
			int index = 0;
			while (index < utf16Length)
			{
				Utf16ToUcs(out var rune, utf16Buffer, ref index, utf16Length);
				if (UcsToUtf8(utf8Buffer, ref utf8Length, utf8Capacity, rune) == global::Unity.Collections.ConversionError.Overflow)
				{
					return global::Unity.Collections.ConversionError.Overflow;
				}
			}
			return global::Unity.Collections.ConversionError.None;
		}

		public unsafe static global::Unity.Collections.ConversionError Utf8ToUtf8(byte* srcBuffer, int srcLength, byte* destBuffer, out int destLength, int destCapacity)
		{
			if (destCapacity >= srcLength)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destBuffer, srcBuffer, srcLength);
				destLength = srcLength;
				return global::Unity.Collections.ConversionError.None;
			}
			destLength = 0;
			int index = 0;
			while (index < srcLength)
			{
				Utf8ToUcs(out var rune, srcBuffer, ref index, srcLength);
				if (UcsToUtf8(destBuffer, ref destLength, destCapacity, rune) == global::Unity.Collections.ConversionError.Overflow)
				{
					return global::Unity.Collections.ConversionError.Overflow;
				}
			}
			return global::Unity.Collections.ConversionError.None;
		}

		public unsafe static global::Unity.Collections.ConversionError Utf8ToUtf16(byte* utf8Buffer, int utf8Length, char* utf16Buffer, out int utf16Length, int utf16Capacity)
		{
			utf16Length = 0;
			int index = 0;
			while (index < utf8Length)
			{
				Utf8ToUcs(out var rune, utf8Buffer, ref index, utf8Length);
				if (UcsToUtf16(utf16Buffer, ref utf16Length, utf16Capacity, rune) == global::Unity.Collections.ConversionError.Overflow)
				{
					return global::Unity.Collections.ConversionError.Overflow;
				}
			}
			return global::Unity.Collections.ConversionError.None;
		}

		private unsafe static int CountRunes(byte* utf8Buffer, int utf8Length, int maxRunes = int.MaxValue)
		{
			int num = 0;
			int num2 = 0;
			while (num < maxRunes && num2 < utf8Length)
			{
				if ((utf8Buffer[num2] & 0xC0) != 128)
				{
					num++;
				}
				num2++;
			}
			return num;
		}
	}
}
