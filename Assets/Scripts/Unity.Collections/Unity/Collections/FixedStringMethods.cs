namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class FixedStringMethods
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static global::Unity.Collections.FormatError Append<T>(this ref T fs, global::Unity.Collections.Unicode.Rune rune) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int index = fs.Length;
			int num = rune.LengthInUtf8Bytes();
			if (!fs.TryResize(index + num, global::Unity.Collections.NativeArrayOptions.UninitializedMemory))
			{
				return global::Unity.Collections.FormatError.Overflow;
			}
			return Write(ref fs, ref index, rune);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static global::Unity.Collections.FormatError Append<T>(this ref T fs, char ch) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			return Append(ref fs, (global::Unity.Collections.Unicode.Rune)ch);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static global::Unity.Collections.FormatError AppendRawByte<T>(this ref T fs, byte a) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			if (!fs.TryResize(length + 1, global::Unity.Collections.NativeArrayOptions.UninitializedMemory))
			{
				return global::Unity.Collections.FormatError.Overflow;
			}
			fs.GetUnsafePtr()[length] = a;
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static global::Unity.Collections.FormatError Append<T>(this ref T fs, global::Unity.Collections.Unicode.Rune rune, int count) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			if (!fs.TryResize(length + rune.LengthInUtf8Bytes() * count, global::Unity.Collections.NativeArrayOptions.UninitializedMemory))
			{
				return global::Unity.Collections.FormatError.Overflow;
			}
			int capacity = fs.Capacity;
			byte* unsafePtr = fs.GetUnsafePtr();
			int index = length;
			for (int i = 0; i < count; i++)
			{
				if (global::Unity.Collections.Unicode.UcsToUtf8(unsafePtr, ref index, capacity, rune) != global::Unity.Collections.ConversionError.None)
				{
					return global::Unity.Collections.FormatError.Overflow;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static global::Unity.Collections.FormatError Append<T>(this ref T fs, long input) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			byte* ptr = stackalloc byte[20];
			int num = 20;
			if (input >= 0)
			{
				do
				{
					byte b = (byte)(input % 10);
					ptr[--num] = (byte)(48 + b);
					input /= 10;
				}
				while (input != 0L);
			}
			else
			{
				do
				{
					byte b2 = (byte)(input % 10);
					ptr[--num] = (byte)(48 - b2);
					input /= 10;
				}
				while (input != 0L);
				ptr[--num] = 45;
			}
			return Append(ref fs, ptr + num, 20 - num);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static global::Unity.Collections.FormatError Append<T>(this ref T fs, int input) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			return Append(ref fs, (long)input);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static global::Unity.Collections.FormatError Append<T>(this ref T fs, ulong input) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			byte* ptr = stackalloc byte[20];
			int num = 20;
			do
			{
				byte b = (byte)(input % 10);
				ptr[--num] = (byte)(48 + b);
				input /= 10;
			}
			while (input != 0L);
			return Append(ref fs, ptr + num, 20 - num);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static global::Unity.Collections.FormatError Append<T>(this ref T fs, uint input) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			return Append(ref fs, (ulong)input);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static global::Unity.Collections.FormatError Append<T>(this ref T fs, float input, char decimalSeparator = '.') where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedStringUtils.UintFloatUnion uintFloatUnion = new global::Unity.Collections.FixedStringUtils.UintFloatUnion
			{
				floatValue = input
			};
			uint num = uintFloatUnion.uintValue >> 31;
			uintFloatUnion.uintValue &= 2147483647u;
			global::Unity.Collections.FormatError result;
			if ((uintFloatUnion.uintValue & 0x7F800000) == 2139095040)
			{
				if (uintFloatUnion.uintValue == 2139095040)
				{
					if (num != 0 && (result = Append(ref fs, '-')) != global::Unity.Collections.FormatError.None)
					{
						return result;
					}
					return Append(ref fs, 'I', 'n', 'f', 'i', 'n', 'i', 't', 'y');
				}
				return Append(ref fs, 'N', 'a', 'N');
			}
			if (num != 0 && uintFloatUnion.uintValue != 0 && (result = Append(ref fs, '-')) != global::Unity.Collections.FormatError.None)
			{
				return result;
			}
			ulong mantissa = 0uL;
			int exponent = 0;
			global::Unity.Collections.FixedStringUtils.Base2ToBase10(ref mantissa, ref exponent, uintFloatUnion.floatValue);
			char* ptr = stackalloc char[9];
			int num2 = 0;
			do
			{
				if (num2 >= 9)
				{
					return global::Unity.Collections.FormatError.Overflow;
				}
				ulong num3 = mantissa % 10;
				ptr[8 - num2++] = (char)(48 + num3);
				mantissa /= 10;
			}
			while (mantissa != 0);
			char* ptr2 = ptr + 9 - num2;
			int num4 = -exponent - num2 + 1;
			if (num4 > 0)
			{
				if (num4 > 4)
				{
					return AppendScientific(ref fs, ptr2, num2, exponent, decimalSeparator);
				}
				if ((result = Append(ref fs, '0', decimalSeparator)) != global::Unity.Collections.FormatError.None)
				{
					return result;
				}
				for (num4--; num4 > 0; num4--)
				{
					if ((result = Append(ref fs, '0')) != global::Unity.Collections.FormatError.None)
					{
						return result;
					}
				}
				for (int i = 0; i < num2; i++)
				{
					if ((result = Append(ref fs, ptr2[i])) != global::Unity.Collections.FormatError.None)
					{
						return result;
					}
				}
				return global::Unity.Collections.FormatError.None;
			}
			int num5 = exponent;
			if (num5 > 0)
			{
				if (num5 > 4)
				{
					return AppendScientific(ref fs, ptr2, num2, exponent, decimalSeparator);
				}
				for (int j = 0; j < num2; j++)
				{
					if ((result = Append(ref fs, ptr2[j])) != global::Unity.Collections.FormatError.None)
					{
						return result;
					}
				}
				while (num5 > 0)
				{
					if ((result = Append(ref fs, '0')) != global::Unity.Collections.FormatError.None)
					{
						return result;
					}
					num5--;
				}
				return global::Unity.Collections.FormatError.None;
			}
			int num6 = num2 + exponent;
			for (int k = 0; k < num2; k++)
			{
				if (k == num6 && (result = Append(ref fs, decimalSeparator)) != global::Unity.Collections.FormatError.None)
				{
					return result;
				}
				if ((result = Append(ref fs, ptr2[k])) != global::Unity.Collections.FormatError.None)
				{
					return result;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError Append<T, T2>(this ref T fs, in T2 input) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref T2 reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in input);
			return Append(ref fs, reference.GetUnsafePtr(), reference.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public static global::Unity.Collections.CopyError CopyFrom<T, T2>(this ref T fs, in T2 input) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			fs.Length = 0;
			if (Append(ref fs, in input) != global::Unity.Collections.FormatError.None)
			{
				return global::Unity.Collections.CopyError.Truncation;
			}
			return global::Unity.Collections.CopyError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static global::Unity.Collections.FormatError Append<T>(this ref T fs, byte* utf8Bytes, int utf8BytesLength) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			if (!fs.TryResize(length + utf8BytesLength, global::Unity.Collections.NativeArrayOptions.UninitializedMemory))
			{
				return global::Unity.Collections.FormatError.Overflow;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(fs.GetUnsafePtr() + length, utf8Bytes, utf8BytesLength);
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe static global::Unity.Collections.FormatError Append<T>(this ref T fs, string s) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = s.Length * 4;
			byte* ptr = stackalloc byte[(int)(uint)num];
			int destLength;
			fixed (char* src = s)
			{
				if (global::Unity.Collections.UTF8ArrayUnsafeUtility.Copy(ptr, out destLength, num, src, s.Length) != global::Unity.Collections.CopyError.None)
				{
					return global::Unity.Collections.FormatError.Overflow;
				}
			}
			return Append(ref fs, ptr, destLength);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.CopyError CopyFrom<T>(this ref T fs, string s) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			fs.Length = 0;
			if (Append(ref fs, s) != global::Unity.Collections.FormatError.None)
			{
				return global::Unity.Collections.CopyError.Truncation;
			}
			return global::Unity.Collections.CopyError.None;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public unsafe static global::Unity.Collections.CopyError CopyFromTruncated<T>(this ref T fs, string s) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			fixed (char* src = s)
			{
				int destLength;
				global::Unity.Collections.CopyError result = global::Unity.Collections.UTF8ArrayUnsafeUtility.Copy(fs.GetUnsafePtr(), out destLength, fs.Capacity, src, s.Length);
				fs.Length = destLength;
				return result;
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.CopyError CopyFromTruncated<T, T2>(this ref T fs, in T2 input) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int destLength;
			global::Unity.Collections.CopyError result = global::Unity.Collections.UTF8ArrayUnsafeUtility.Copy(fs.GetUnsafePtr(), out destLength, fs.Capacity, input.GetUnsafePtr(), input.Length);
			fs.Length = destLength;
			return result;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0>(this ref T dest, in U format, in T0 arg0) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((b - 48 != 0) ? global::Unity.Collections.FormatError.BadFormatSpecifier : Append(ref dest, in arg0)) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0, T1>(this ref T dest, in U format, in T0 arg0, in T1 arg1) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((b - 48) switch
						{
							0 => Append(ref dest, in arg0), 
							1 => Append(ref dest, in arg1), 
							_ => global::Unity.Collections.FormatError.BadFormatSpecifier, 
						}) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0, T1, T2>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((int)b switch
						{
							48 => Append(ref dest, in arg0), 
							49 => Append(ref dest, in arg1), 
							50 => Append(ref dest, in arg2), 
							_ => global::Unity.Collections.FormatError.BadFormatSpecifier, 
						}) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0, T1, T2, T3>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((int)b switch
						{
							48 => Append(ref dest, in arg0), 
							49 => Append(ref dest, in arg1), 
							50 => Append(ref dest, in arg2), 
							51 => Append(ref dest, in arg3), 
							_ => global::Unity.Collections.FormatError.BadFormatSpecifier, 
						}) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0, T1, T2, T3, T4>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T4 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((int)b switch
						{
							48 => Append(ref dest, in arg0), 
							49 => Append(ref dest, in arg1), 
							50 => Append(ref dest, in arg2), 
							51 => Append(ref dest, in arg3), 
							52 => Append(ref dest, in arg4), 
							_ => global::Unity.Collections.FormatError.BadFormatSpecifier, 
						}) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0, T1, T2, T3, T4, T5>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T4 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T5 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((int)b switch
						{
							48 => Append(ref dest, in arg0), 
							49 => Append(ref dest, in arg1), 
							50 => Append(ref dest, in arg2), 
							51 => Append(ref dest, in arg3), 
							52 => Append(ref dest, in arg4), 
							53 => Append(ref dest, in arg5), 
							_ => global::Unity.Collections.FormatError.BadFormatSpecifier, 
						}) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0, T1, T2, T3, T4, T5, T6>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T4 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T5 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T6 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((int)b switch
						{
							48 => Append(ref dest, in arg0), 
							49 => Append(ref dest, in arg1), 
							50 => Append(ref dest, in arg2), 
							51 => Append(ref dest, in arg3), 
							52 => Append(ref dest, in arg4), 
							53 => Append(ref dest, in arg5), 
							54 => Append(ref dest, in arg6), 
							_ => global::Unity.Collections.FormatError.BadFormatSpecifier, 
						}) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0, T1, T2, T3, T4, T5, T6, T7>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T4 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T5 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T6 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T7 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((int)b switch
						{
							48 => Append(ref dest, in arg0), 
							49 => Append(ref dest, in arg1), 
							50 => Append(ref dest, in arg2), 
							51 => Append(ref dest, in arg3), 
							52 => Append(ref dest, in arg4), 
							53 => Append(ref dest, in arg5), 
							54 => Append(ref dest, in arg6), 
							55 => Append(ref dest, in arg7), 
							_ => global::Unity.Collections.FormatError.BadFormatSpecifier, 
						}) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0, T1, T2, T3, T4, T5, T6, T7, T8>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T4 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T5 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T6 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T7 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T8 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((int)b switch
						{
							48 => Append(ref dest, in arg0), 
							49 => Append(ref dest, in arg1), 
							50 => Append(ref dest, in arg2), 
							51 => Append(ref dest, in arg3), 
							52 => Append(ref dest, in arg4), 
							53 => Append(ref dest, in arg5), 
							54 => Append(ref dest, in arg6), 
							55 => Append(ref dest, in arg7), 
							56 => Append(ref dest, in arg8), 
							_ => global::Unity.Collections.FormatError.BadFormatSpecifier, 
						}) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static global::Unity.Collections.FormatError AppendFormat<T, U, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this ref T dest, in U format, in T0 arg0, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5, in T6 arg6, in T7 arg7, in T8 arg8, in T9 arg9) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T0 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T4 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T5 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T6 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T7 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T8 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T9 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref U reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in format);
			int length = reference.Length;
			byte* unsafePtr = reference.GetUnsafePtr();
			int num = 0;
			global::Unity.Collections.FormatError formatError = global::Unity.Collections.FormatError.None;
			while (num < length)
			{
				byte b = unsafePtr[num++];
				switch (b)
				{
				case 123:
					if (num < length)
					{
						b = unsafePtr[num++];
						formatError = ((b >= 48 && b <= 57 && num < length && unsafePtr[num++] == 125) ? ((int)b switch
						{
							48 => Append(ref dest, in arg0), 
							49 => Append(ref dest, in arg1), 
							50 => Append(ref dest, in arg2), 
							51 => Append(ref dest, in arg3), 
							52 => Append(ref dest, in arg4), 
							53 => Append(ref dest, in arg5), 
							54 => Append(ref dest, in arg6), 
							55 => Append(ref dest, in arg7), 
							56 => Append(ref dest, in arg8), 
							57 => Append(ref dest, in arg9), 
							_ => global::Unity.Collections.FormatError.BadFormatSpecifier, 
						}) : ((b != 123) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b)));
						break;
					}
					return global::Unity.Collections.FormatError.BadFormatSpecifier;
				case 125:
					if (num < length)
					{
						b = unsafePtr[num++];
					}
					else
					{
						formatError = global::Unity.Collections.FormatError.BadFormatSpecifier;
					}
					formatError = ((b != 125) ? global::Unity.Collections.FormatError.BadFormatSpecifier : AppendRawByte(ref dest, b));
					break;
				default:
					formatError = AppendRawByte(ref dest, b);
					break;
				}
				if (formatError != global::Unity.Collections.FormatError.None)
				{
					return formatError;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal static global::Unity.Collections.FormatError Append<T>(this ref T fs, char a, char b) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if ((global::Unity.Collections.FormatError.None | Append(ref fs, (global::Unity.Collections.Unicode.Rune)a) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)b)) != global::Unity.Collections.FormatError.None)
			{
				return global::Unity.Collections.FormatError.Overflow;
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal static global::Unity.Collections.FormatError Append<T>(this ref T fs, char a, char b, char c) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if ((global::Unity.Collections.FormatError.None | Append(ref fs, (global::Unity.Collections.Unicode.Rune)a) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)b) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)c)) != global::Unity.Collections.FormatError.None)
			{
				return global::Unity.Collections.FormatError.Overflow;
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal static global::Unity.Collections.FormatError Append<T>(this ref T fs, char a, char b, char c, char d, char e, char f, char g, char h) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if ((global::Unity.Collections.FormatError.None | Append(ref fs, (global::Unity.Collections.Unicode.Rune)a) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)b) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)c) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)d) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)e) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)f) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)g) | Append(ref fs, (global::Unity.Collections.Unicode.Rune)h)) != global::Unity.Collections.FormatError.None)
			{
				return global::Unity.Collections.FormatError.Overflow;
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal unsafe static global::Unity.Collections.FormatError AppendScientific<T>(this ref T fs, char* source, int sourceLength, int decimalExponent, char decimalSeparator = '.') where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FormatError result;
			if ((result = Append(ref fs, *source)) != global::Unity.Collections.FormatError.None)
			{
				return result;
			}
			if (sourceLength > 1)
			{
				if ((result = Append(ref fs, decimalSeparator)) != global::Unity.Collections.FormatError.None)
				{
					return result;
				}
				for (int i = 1; i < sourceLength; i++)
				{
					if ((result = Append(ref fs, source[i])) != global::Unity.Collections.FormatError.None)
					{
						return result;
					}
				}
			}
			if ((result = Append(ref fs, 'E')) != global::Unity.Collections.FormatError.None)
			{
				return result;
			}
			if (decimalExponent < 0)
			{
				if ((result = Append(ref fs, '-')) != global::Unity.Collections.FormatError.None)
				{
					return result;
				}
				decimalExponent *= -1;
				decimalExponent -= sourceLength - 1;
			}
			else
			{
				if ((result = Append(ref fs, '+')) != global::Unity.Collections.FormatError.None)
				{
					return result;
				}
				decimalExponent += sourceLength - 1;
			}
			char* ptr = stackalloc char[2];
			for (int j = 0; j < 2; j++)
			{
				int num = decimalExponent % 10;
				ptr[1 - j] = (char)(48 + num);
				decimalExponent /= 10;
			}
			for (int k = 0; k < 2; k++)
			{
				if ((result = Append(ref fs, ptr[k])) != global::Unity.Collections.FormatError.None)
				{
					return result;
				}
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal static bool Found<T>(this ref T fs, ref int offset, char a, char b, char c) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = offset;
			if ((Read(ref fs, ref offset).value | 0x20) == a && (Read(ref fs, ref offset).value | 0x20) == b && (Read(ref fs, ref offset).value | 0x20) == c)
			{
				return true;
			}
			offset = num;
			return false;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal static bool Found<T>(this ref T fs, ref int offset, char a, char b, char c, char d, char e, char f, char g, char h) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = offset;
			if ((Read(ref fs, ref offset).value | 0x20) == a && (Read(ref fs, ref offset).value | 0x20) == b && (Read(ref fs, ref offset).value | 0x20) == c && (Read(ref fs, ref offset).value | 0x20) == d && (Read(ref fs, ref offset).value | 0x20) == e && (Read(ref fs, ref offset).value | 0x20) == f && (Read(ref fs, ref offset).value | 0x20) == g && (Read(ref fs, ref offset).value | 0x20) == h)
			{
				return true;
			}
			offset = num;
			return false;
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckSubstringInRange(int strLength, int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new global::System.ArgumentOutOfRangeException($"startIndex {startIndex} must be positive.");
			}
			if (length < 0)
			{
				throw new global::System.ArgumentOutOfRangeException($"length {length} cannot be negative.");
			}
			if (startIndex > strLength)
			{
				throw new global::System.ArgumentOutOfRangeException($"startIndex {startIndex} cannot be larger than string length {strLength}.");
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static T Substring<T>(this ref T str, int startIndex, int length) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			length = global::Unity.Mathematics.math.min(length, str.Length - startIndex);
			T fs = new T();
			Append(ref fs, str.GetUnsafePtr() + startIndex, length);
			return fs;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static T Substring<T>(this ref T str, int startIndex) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			return Substring(ref str, startIndex, str.Length - startIndex);
		}

		public unsafe static global::Unity.Collections.NativeText Substring(this ref global::Unity.Collections.NativeText str, int startIndex, int length, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			length = global::Unity.Mathematics.math.min(length, str.Length - startIndex);
			global::Unity.Collections.NativeText fs = new global::Unity.Collections.NativeText(length, allocator);
			Append(ref fs, str.GetUnsafePtr() + startIndex, length);
			return fs;
		}

		public static global::Unity.Collections.NativeText Substring(this ref global::Unity.Collections.NativeText str, int startIndex, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return str.Substring(startIndex, str.Length - startIndex);
		}

		public unsafe static global::Unity.Collections.NativeText Substring(this ref global::Unity.Collections.NativeText str, int startIndex, int length)
		{
			return str.Substring(startIndex, length, str.m_Data->m_UntypedListData.Allocator);
		}

		public static global::Unity.Collections.NativeText Substring(this ref global::Unity.Collections.NativeText str, int startIndex)
		{
			return str.Substring(startIndex, str.Length - startIndex);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static int IndexOf<T>(this ref T fs, global::Unity.Collections.Unicode.Rune rune) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			int num = 0;
			while (num < length)
			{
				int index = num;
				if (Read(ref fs, ref index).value == rune.value)
				{
					return num;
				}
				num = index;
			}
			return -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static int IndexOf<T>(this ref T fs, byte* bytes, int bytesLen) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			int length = fs.Length;
			for (int i = 0; i <= length - bytesLen; i++)
			{
				int num = 0;
				while (true)
				{
					if (num < bytesLen)
					{
						if (unsafePtr[i + num] != bytes[num])
						{
							break;
						}
						num++;
						continue;
					}
					return i;
				}
			}
			return -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static int IndexOf<T>(this ref T fs, byte* bytes, int bytesLen, int startIndex, int distance = int.MaxValue) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			int length = fs.Length;
			int num = global::System.Math.Min(distance - 1, length - bytesLen);
			for (int i = startIndex; i <= num; i++)
			{
				int num2 = 0;
				while (true)
				{
					if (num2 < bytesLen)
					{
						if (unsafePtr[i + num2] != bytes[num2])
						{
							break;
						}
						num2++;
						continue;
					}
					return i;
				}
			}
			return -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static int IndexOf<T, T2>(this ref T fs, in T2 other) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref T2 reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in other);
			return IndexOf(ref fs, reference.GetUnsafePtr(), reference.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static int IndexOf<T, T2>(this ref T fs, in T2 other, int startIndex, int distance = int.MaxValue) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref T2 reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in other);
			return IndexOf(ref fs, reference.GetUnsafePtr(), reference.Length, startIndex, distance);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public static bool Contains<T, T2>(this ref T fs, in T2 other) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			return IndexOf(ref fs, in other) != -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static int LastIndexOf<T>(this ref T fs, global::Unity.Collections.Unicode.Rune rune) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if (global::Unity.Collections.Unicode.IsValidCodePoint(rune.value))
			{
				for (int num = fs.Length - 1; num >= 0; num--)
				{
					global::Unity.Collections.Unicode.Rune rune2 = Peek(ref fs, num);
					if (global::Unity.Collections.Unicode.IsValidCodePoint(rune2.value) && rune2.value == rune.value)
					{
						return num;
					}
				}
			}
			return -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static int LastIndexOf<T>(this ref T fs, byte* bytes, int bytesLen) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			for (int num = fs.Length - bytesLen; num >= 0; num--)
			{
				int num2 = 0;
				while (true)
				{
					if (num2 < bytesLen)
					{
						if (unsafePtr[num + num2] != bytes[num2])
						{
							break;
						}
						num2++;
						continue;
					}
					return num;
				}
			}
			return -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static int LastIndexOf<T>(this ref T fs, byte* bytes, int bytesLen, int startIndex, int distance = int.MaxValue) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			startIndex = global::System.Math.Min(fs.Length - bytesLen, startIndex);
			int num = global::System.Math.Max(0, startIndex - distance);
			for (int num2 = startIndex; num2 >= num; num2--)
			{
				int num3 = 0;
				while (true)
				{
					if (num3 < bytesLen)
					{
						if (unsafePtr[num2 + num3] != bytes[num3])
						{
							break;
						}
						num3++;
						continue;
					}
					return num2;
				}
			}
			return -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static int LastIndexOf<T, T2>(this ref T fs, in T2 other) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref T2 reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in other);
			return LastIndexOf(ref fs, reference.GetUnsafePtr(), reference.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static int LastIndexOf<T, T2>(this ref T fs, in T2 other, int startIndex, int distance = int.MaxValue) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref T2 reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in other);
			return LastIndexOf(ref fs, reference.GetUnsafePtr(), reference.Length, startIndex, distance);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static int CompareTo<T>(this ref T fs, byte* bytes, int bytesLen) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			int length = fs.Length;
			int num = ((length < bytesLen) ? length : bytesLen);
			for (int i = 0; i < num; i++)
			{
				if (unsafePtr[i] < bytes[i])
				{
					return -1;
				}
				if (unsafePtr[i] > bytes[i])
				{
					return 1;
				}
			}
			if (length < bytesLen)
			{
				return -1;
			}
			if (length > bytesLen)
			{
				return 1;
			}
			return 0;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static int CompareTo<T, T2>(this ref T fs, in T2 other) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref T2 reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in other);
			return CompareTo(ref fs, reference.GetUnsafePtr(), reference.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static bool Equals<T>(this ref T fs, byte* bytes, int bytesLen) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			byte* unsafePtr = fs.GetUnsafePtr();
			if (fs.Length != bytesLen)
			{
				return false;
			}
			if (unsafePtr == bytes)
			{
				return true;
			}
			return CompareTo(ref fs, bytes, bytesLen) == 0;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static bool Equals<T, T2>(this ref T fs, in T2 other) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ref T2 reference = ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions.AsRef(in other);
			return Equals(ref fs, reference.GetUnsafePtr(), reference.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static global::Unity.Collections.Unicode.Rune Peek<T>(this ref T fs, int index) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if (index >= fs.Length)
			{
				return global::Unity.Collections.Unicode.BadRune;
			}
			global::Unity.Collections.Unicode.Utf8ToUcs(out var rune, fs.GetUnsafePtr(), ref index, fs.Capacity);
			return rune;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static global::Unity.Collections.Unicode.Rune Read<T>(this ref T fs, ref int index) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if (index >= fs.Length)
			{
				return global::Unity.Collections.Unicode.BadRune;
			}
			global::Unity.Collections.Unicode.Utf8ToUcs(out var rune, fs.GetUnsafePtr(), ref index, fs.Capacity);
			return rune;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static global::Unity.Collections.FormatError Write<T>(this ref T fs, ref int index, global::Unity.Collections.Unicode.Rune rune) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if (global::Unity.Collections.Unicode.UcsToUtf8(fs.GetUnsafePtr(), ref index, fs.Capacity, rune) != global::Unity.Collections.ConversionError.None)
			{
				return global::Unity.Collections.FormatError.Overflow;
			}
			return global::Unity.Collections.FormatError.None;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed string")]
		public unsafe static string ConvertToString<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			char* ptr = stackalloc char[fs.Length * 2];
			int utf16Length = 0;
			global::Unity.Collections.Unicode.Utf8ToUtf16(fs.GetUnsafePtr(), fs.Length, ptr, out utf16Length, fs.Length * 2);
			return new string(ptr, 0, utf16Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static int ComputeHashCode<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			return (int)global::Unity.Collections.CollectionHelper.Hash(fs.GetUnsafePtr(), fs.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static int EffectiveSizeOf<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			return 2 + fs.Length + 1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static bool StartsWith<T>(this ref T fs, global::Unity.Collections.Unicode.Rune rune) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = rune.LengthInUtf8Bytes();
			if (fs.Length >= num)
			{
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.StrCmp(fs.GetUnsafePtr(), num, &rune, 1) == 0;
			}
			return false;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static bool StartsWith<T, U>(this ref T fs, in U other) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = other.Length;
			if (fs.Length >= length)
			{
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.StrCmp(fs.GetUnsafePtr(), length, other.GetUnsafePtr(), length) == 0;
			}
			return false;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static bool EndsWith<T>(this ref T fs, global::Unity.Collections.Unicode.Rune rune) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = rune.LengthInUtf8Bytes();
			if (fs.Length >= num)
			{
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.StrCmp(fs.GetUnsafePtr() + fs.Length - num, num, &rune, 1) == 0;
			}
			return false;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString128Bytes),
			typeof(global::Unity.Collections.FixedString128Bytes)
		})]
		public unsafe static bool EndsWith<T, U>(this ref T fs, in U other) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where U : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = other.Length;
			if (fs.Length >= length)
			{
				return global::Unity.Collections.UTF8ArrayUnsafeUtility.StrCmp(fs.GetUnsafePtr() + fs.Length - length, length, other.GetUnsafePtr(), length) == 0;
			}
			return false;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal unsafe static int TrimStartIndex<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			int index = 0;
			int num;
			global::Unity.Collections.Unicode.Rune rune;
			do
			{
				num = index;
			}
			while (global::Unity.Collections.Unicode.Utf8ToUcs(out rune, unsafePtr, ref index, length) == global::Unity.Collections.ConversionError.None && rune.IsWhiteSpace());
			return index - (index - num);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal unsafe static int TrimStartIndex<T>(this ref T fs, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			int index = 0;
			int num;
			global::Unity.Collections.ConversionError conversionError;
			bool flag;
			do
			{
				num = index;
				conversionError = global::Unity.Collections.Unicode.Utf8ToUcs(out var rune, unsafePtr, ref index, length);
				flag = false;
				int i = 0;
				for (int length2 = trimRunes.Length; i < length2; i++)
				{
					if (flag)
					{
						break;
					}
					flag |= trimRunes[i] == rune;
				}
			}
			while (conversionError == global::Unity.Collections.ConversionError.None && flag);
			return index - (index - num);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal unsafe static int TrimEndIndex<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			int index = length;
			int num;
			global::Unity.Collections.Unicode.Rune rune;
			do
			{
				num = index;
			}
			while (global::Unity.Collections.Unicode.Utf8ToUcsReverse(out rune, unsafePtr, ref index, length) == global::Unity.Collections.ConversionError.None && rune.IsWhiteSpace());
			return index + (num - index);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal unsafe static int TrimEndIndex<T>(this ref T fs, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			int index = length;
			int num;
			global::Unity.Collections.ConversionError conversionError;
			bool flag;
			do
			{
				num = index;
				conversionError = global::Unity.Collections.Unicode.Utf8ToUcsReverse(out var rune, unsafePtr, ref index, length);
				flag = false;
				int i = 0;
				for (int length2 = trimRunes.Length; i < length2; i++)
				{
					if (flag)
					{
						break;
					}
					flag |= trimRunes[i] == rune;
				}
			}
			while (conversionError == global::Unity.Collections.ConversionError.None && flag);
			return index + (num - index);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static T TrimStart<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = TrimStartIndex(ref fs);
			T fs2 = new T();
			Append(ref fs2, fs.GetUnsafePtr() + num, fs.Length - num);
			return fs2;
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeText TrimStart(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int num = TrimStartIndex(ref fs);
			int num2 = fs.Length - num;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(num2, allocator);
			Append(ref fs2, fs.GetUnsafePtr() + num, num2);
			return fs2;
		}

		public unsafe static global::Unity.Collections.NativeText TrimStart(this ref global::Unity.Collections.NativeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int num = TrimStartIndex(ref fs);
			int num2 = fs.Length - num;
			global::Unity.Collections.NativeText fs2 = new global::Unity.Collections.NativeText(num2, allocator);
			Append(ref fs2, fs.GetUnsafePtr() + num, num2);
			return fs2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static T TrimStart<T>(this ref T fs, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = TrimStartIndex(ref fs, trimRunes);
			T fs2 = new T();
			Append(ref fs2, fs.GetUnsafePtr() + num, fs.Length - num);
			return fs2;
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeText TrimStart(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes)
		{
			int num = TrimStartIndex(ref fs, trimRunes);
			int num2 = fs.Length - num;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(num2, allocator);
			Append(ref fs2, fs.GetUnsafePtr() + num, num2);
			return fs2;
		}

		public unsafe static global::Unity.Collections.NativeText TrimStart(this ref global::Unity.Collections.NativeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes)
		{
			int num = TrimStartIndex(ref fs, trimRunes);
			int num2 = fs.Length - num;
			global::Unity.Collections.NativeText fs2 = new global::Unity.Collections.NativeText(num2, allocator);
			Append(ref fs2, fs.GetUnsafePtr() + num, num2);
			return fs2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static T TrimEnd<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int utf8BytesLength = TrimEndIndex(ref fs);
			T fs2 = new T();
			Append(ref fs2, fs.GetUnsafePtr(), utf8BytesLength);
			return fs2;
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeText TrimEnd(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int num = TrimEndIndex(ref fs);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(num, allocator);
			Append(ref fs2, fs.GetUnsafePtr(), num);
			return fs2;
		}

		public unsafe static global::Unity.Collections.NativeText TrimEnd(this ref global::Unity.Collections.NativeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int num = TrimEndIndex(ref fs);
			global::Unity.Collections.NativeText fs2 = new global::Unity.Collections.NativeText(num, allocator);
			Append(ref fs2, fs.GetUnsafePtr(), num);
			return fs2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static T TrimEnd<T>(this ref T fs, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int utf8BytesLength = TrimEndIndex(ref fs, trimRunes);
			T fs2 = new T();
			Append(ref fs2, fs.GetUnsafePtr(), utf8BytesLength);
			return fs2;
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeText TrimEnd(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes)
		{
			int num = TrimEndIndex(ref fs, trimRunes);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(num, allocator);
			Append(ref fs2, fs.GetUnsafePtr(), num);
			return fs2;
		}

		public unsafe static global::Unity.Collections.NativeText TrimEnd(this ref global::Unity.Collections.NativeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes)
		{
			int num = TrimEndIndex(ref fs, trimRunes);
			global::Unity.Collections.NativeText fs2 = new global::Unity.Collections.NativeText(num, allocator);
			Append(ref fs2, fs.GetUnsafePtr(), num);
			return fs2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static T Trim<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = TrimStartIndex(ref fs);
			if (num == fs.Length)
			{
				return new T();
			}
			int num2 = TrimEndIndex(ref fs);
			T fs2 = new T();
			Append(ref fs2, fs.GetUnsafePtr() + num, num2 - num);
			return fs2;
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeText Trim(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int num = TrimStartIndex(ref fs);
			if (num == fs.Length)
			{
				return new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(0, allocator);
			}
			int num2 = TrimEndIndex(ref fs) - num;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(num2, allocator);
			Append(ref fs2, fs.GetUnsafePtr() + num, num2);
			return fs2;
		}

		public unsafe static global::Unity.Collections.NativeText Trim(this ref global::Unity.Collections.NativeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int num = TrimStartIndex(ref fs);
			if (num == fs.Length)
			{
				return new global::Unity.Collections.NativeText(0, allocator);
			}
			int num2 = TrimEndIndex(ref fs) - num;
			global::Unity.Collections.NativeText fs2 = new global::Unity.Collections.NativeText(num2, allocator);
			Append(ref fs2, fs.GetUnsafePtr() + num, num2);
			return fs2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static T Trim<T>(this ref T fs, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = TrimStartIndex(ref fs, trimRunes);
			if (num == fs.Length)
			{
				return new T();
			}
			int num2 = TrimEndIndex(ref fs, trimRunes);
			T fs2 = new T();
			Append(ref fs2, fs.GetUnsafePtr() + num, num2 - num);
			return fs2;
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeText Trim(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes)
		{
			int num = TrimStartIndex(ref fs, trimRunes);
			if (num == fs.Length)
			{
				return new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(0, allocator);
			}
			int num2 = TrimEndIndex(ref fs) - num;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(num2, allocator);
			Append(ref fs2, fs.GetUnsafePtr() + num, num2);
			return fs2;
		}

		public unsafe static global::Unity.Collections.NativeText Trim(this ref global::Unity.Collections.NativeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::System.ReadOnlySpan<global::Unity.Collections.Unicode.Rune> trimRunes)
		{
			int num = TrimStartIndex(ref fs, trimRunes);
			if (num == fs.Length)
			{
				return new global::Unity.Collections.NativeText(0, allocator);
			}
			int num2 = TrimEndIndex(ref fs) - num;
			global::Unity.Collections.NativeText fs2 = new global::Unity.Collections.NativeText(num2, allocator);
			Append(ref fs2, fs.GetUnsafePtr() + num, num2);
			return fs2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static T ToLowerAscii<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			T fs2 = new T();
			global::Unity.Collections.ConversionError conversionError = global::Unity.Collections.ConversionError.None;
			int index = 0;
			while (index < length && conversionError == global::Unity.Collections.ConversionError.None)
			{
				conversionError = global::Unity.Collections.Unicode.Utf8ToUcs(out var rune, unsafePtr, ref index, length);
				Append(ref fs2, rune.ToLowerAscii());
			}
			return fs2;
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeText ToLowerAscii(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(length, allocator);
			global::Unity.Collections.ConversionError conversionError = global::Unity.Collections.ConversionError.None;
			int index = 0;
			while (index < length && conversionError == global::Unity.Collections.ConversionError.None)
			{
				conversionError = global::Unity.Collections.Unicode.Utf8ToUcs(out var rune, unsafePtr, ref index, length);
				Append(ref fs2, rune.ToLowerAscii());
			}
			return fs2;
		}

		public unsafe static global::Unity.Collections.NativeText ToLowerAscii(this ref global::Unity.Collections.NativeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			global::Unity.Collections.NativeText fs2 = new global::Unity.Collections.NativeText(length, allocator);
			global::Unity.Collections.ConversionError conversionError = global::Unity.Collections.ConversionError.None;
			int index = 0;
			while (index < length && conversionError == global::Unity.Collections.ConversionError.None)
			{
				conversionError = global::Unity.Collections.Unicode.Utf8ToUcs(out var rune, unsafePtr, ref index, length);
				Append(ref fs2, rune.ToLowerAscii());
			}
			return fs2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public unsafe static T ToUpperAscii<T>(this ref T fs) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			T fs2 = new T();
			global::Unity.Collections.ConversionError conversionError = global::Unity.Collections.ConversionError.None;
			int index = 0;
			while (index < length && conversionError == global::Unity.Collections.ConversionError.None)
			{
				conversionError = global::Unity.Collections.Unicode.Utf8ToUcs(out var rune, unsafePtr, ref index, length);
				Append(ref fs2, rune.ToUpperAscii());
			}
			return fs2;
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeText ToUpperAscii(this ref global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			global::Unity.Collections.LowLevel.Unsafe.UnsafeText fs2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(length, allocator);
			global::Unity.Collections.ConversionError conversionError = global::Unity.Collections.ConversionError.None;
			int index = 0;
			while (index < length && conversionError == global::Unity.Collections.ConversionError.None)
			{
				conversionError = global::Unity.Collections.Unicode.Utf8ToUcs(out var rune, unsafePtr, ref index, length);
				Append(ref fs2, rune.ToUpperAscii());
			}
			return fs2;
		}

		public unsafe static global::Unity.Collections.NativeText ToUpperAscii(this ref global::Unity.Collections.NativeText fs, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			int length = fs.Length;
			byte* unsafePtr = fs.GetUnsafePtr();
			global::Unity.Collections.NativeText fs2 = new global::Unity.Collections.NativeText(length, allocator);
			global::Unity.Collections.ConversionError conversionError = global::Unity.Collections.ConversionError.None;
			int index = 0;
			while (index < length && conversionError == global::Unity.Collections.ConversionError.None)
			{
				conversionError = global::Unity.Collections.Unicode.Utf8ToUcs(out var rune, unsafePtr, ref index, length);
				Append(ref fs2, rune.ToUpperAscii());
			}
			return fs2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		internal static bool ParseLongInternal<T>(ref T fs, ref int offset, out long value) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = offset;
			int num2 = 1;
			if (offset < fs.Length)
			{
				if (Peek(ref fs, offset).value == 43)
				{
					Read(ref fs, ref offset);
				}
				else if (Peek(ref fs, offset).value == 45)
				{
					num2 = -1;
					Read(ref fs, ref offset);
				}
			}
			int num3 = offset;
			value = 0L;
			while (offset < fs.Length && global::Unity.Collections.Unicode.Rune.IsDigit(Peek(ref fs, offset)))
			{
				value *= 10L;
				value += Read(ref fs, ref offset).value - 48;
			}
			value = num2 * value;
			if (offset == num3)
			{
				offset = num;
				return false;
			}
			return true;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static global::Unity.Collections.ParseError Parse<T>(this ref T fs, ref int offset, ref int output) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if (!ParseLongInternal(ref fs, ref offset, out var value))
			{
				return global::Unity.Collections.ParseError.Syntax;
			}
			if (value > int.MaxValue)
			{
				return global::Unity.Collections.ParseError.Overflow;
			}
			if (value < int.MinValue)
			{
				return global::Unity.Collections.ParseError.Overflow;
			}
			output = (int)value;
			return global::Unity.Collections.ParseError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static global::Unity.Collections.ParseError Parse<T>(this ref T fs, ref int offset, ref uint output) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if (!ParseLongInternal(ref fs, ref offset, out var value))
			{
				return global::Unity.Collections.ParseError.Syntax;
			}
			if (value > uint.MaxValue)
			{
				return global::Unity.Collections.ParseError.Overflow;
			}
			if (value < 0)
			{
				return global::Unity.Collections.ParseError.Overflow;
			}
			output = (uint)value;
			return global::Unity.Collections.ParseError.None;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString128Bytes) })]
		public static global::Unity.Collections.ParseError Parse<T>(this ref T fs, ref int offset, ref float output, char decimalSeparator = '.') where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = offset;
			int num2 = 1;
			if (offset < fs.Length)
			{
				if (Peek(ref fs, offset).value == 43)
				{
					Read(ref fs, ref offset);
				}
				else if (Peek(ref fs, offset).value == 45)
				{
					num2 = -1;
					Read(ref fs, ref offset);
				}
			}
			if (Found(ref fs, ref offset, 'n', 'a', 'n'))
			{
				global::Unity.Collections.FixedStringUtils.UintFloatUnion uintFloatUnion = new global::Unity.Collections.FixedStringUtils.UintFloatUnion
				{
					uintValue = 4290772992u
				};
				output = uintFloatUnion.floatValue;
				return global::Unity.Collections.ParseError.None;
			}
			if (Found(ref fs, ref offset, 'i', 'n', 'f', 'i', 'n', 'i', 't', 'y'))
			{
				output = ((num2 == 1) ? float.PositiveInfinity : float.NegativeInfinity);
				return global::Unity.Collections.ParseError.None;
			}
			ulong num3 = 0uL;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			while (offset < fs.Length && global::Unity.Collections.Unicode.Rune.IsDigit(Peek(ref fs, offset)))
			{
				num6++;
				if (num4 < 9)
				{
					long num7 = (long)(num3 * 10) + (long)(Peek(ref fs, offset).value - 48);
					if ((ulong)num7 > num3)
					{
						num4++;
					}
					num3 = (ulong)num7;
				}
				else
				{
					num5--;
				}
				Read(ref fs, ref offset);
			}
			if (offset < fs.Length && Peek(ref fs, offset).value == decimalSeparator)
			{
				Read(ref fs, ref offset);
				while (offset < fs.Length && global::Unity.Collections.Unicode.Rune.IsDigit(Peek(ref fs, offset)))
				{
					num6++;
					if (num4 < 9)
					{
						long num8 = (long)(num3 * 10) + (long)(Peek(ref fs, offset).value - 48);
						if ((ulong)num8 > num3)
						{
							num4++;
						}
						num3 = (ulong)num8;
						num5++;
					}
					Read(ref fs, ref offset);
				}
			}
			if (num6 == 0)
			{
				offset = num;
				return global::Unity.Collections.ParseError.Syntax;
			}
			int num9 = 0;
			int num10 = 1;
			if (offset < fs.Length && (Peek(ref fs, offset).value | 0x20) == 101)
			{
				Read(ref fs, ref offset);
				if (offset < fs.Length)
				{
					if (Peek(ref fs, offset).value == 43)
					{
						Read(ref fs, ref offset);
					}
					else if (Peek(ref fs, offset).value == 45)
					{
						num10 = -1;
						Read(ref fs, ref offset);
					}
				}
				int num11 = offset;
				while (offset < fs.Length && global::Unity.Collections.Unicode.Rune.IsDigit(Peek(ref fs, offset)))
				{
					num9 = num9 * 10 + (Peek(ref fs, offset).value - 48);
					Read(ref fs, ref offset);
				}
				if (offset == num11)
				{
					offset = num;
					return global::Unity.Collections.ParseError.Syntax;
				}
				if (num9 > 38)
				{
					if (num10 == 1)
					{
						return global::Unity.Collections.ParseError.Overflow;
					}
					return global::Unity.Collections.ParseError.Underflow;
				}
			}
			num9 = num9 * num10 - num5;
			global::Unity.Collections.ParseError parseError = global::Unity.Collections.FixedStringUtils.Base10ToBase2(ref output, num3, num9);
			if (parseError != global::Unity.Collections.ParseError.None)
			{
				return parseError;
			}
			output *= num2;
			return global::Unity.Collections.ParseError.None;
		}
	}
}
