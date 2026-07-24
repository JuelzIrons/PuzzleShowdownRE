namespace Newtonsoft.Json.Utilities
{
	internal static class ConvertUtils
	{
		internal enum ConvertResult
		{
			Success = 0,
			CannotConvertNull = 1,
			NotInstantiableType = 2,
			NoValidConversion = 3
		}

		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Newtonsoft.Json.Utilities.PrimitiveTypeCode> TypeCodeMap = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Newtonsoft.Json.Utilities.PrimitiveTypeCode>
		{
			{
				typeof(char),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Char
			},
			{
				typeof(char?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.CharNullable
			},
			{
				typeof(bool),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Boolean
			},
			{
				typeof(bool?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BooleanNullable
			},
			{
				typeof(sbyte),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte
			},
			{
				typeof(sbyte?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByteNullable
			},
			{
				typeof(short),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16
			},
			{
				typeof(short?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16Nullable
			},
			{
				typeof(ushort),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16
			},
			{
				typeof(ushort?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16Nullable
			},
			{
				typeof(int),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32
			},
			{
				typeof(int?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32Nullable
			},
			{
				typeof(byte),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte
			},
			{
				typeof(byte?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.ByteNullable
			},
			{
				typeof(uint),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32
			},
			{
				typeof(uint?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32Nullable
			},
			{
				typeof(long),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64
			},
			{
				typeof(long?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64Nullable
			},
			{
				typeof(ulong),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64
			},
			{
				typeof(ulong?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64Nullable
			},
			{
				typeof(float),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Single
			},
			{
				typeof(float?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SingleNullable
			},
			{
				typeof(double),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Double
			},
			{
				typeof(double?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DoubleNullable
			},
			{
				typeof(global::System.DateTime),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime
			},
			{
				typeof(global::System.DateTime?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeNullable
			},
			{
				typeof(global::System.DateTimeOffset),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffset
			},
			{
				typeof(global::System.DateTimeOffset?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffsetNullable
			},
			{
				typeof(decimal),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Decimal
			},
			{
				typeof(decimal?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DecimalNullable
			},
			{
				typeof(global::System.Guid),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Guid
			},
			{
				typeof(global::System.Guid?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.GuidNullable
			},
			{
				typeof(global::System.TimeSpan),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpan
			},
			{
				typeof(global::System.TimeSpan?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpanNullable
			},
			{
				typeof(global::System.Numerics.BigInteger),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigInteger
			},
			{
				typeof(global::System.Numerics.BigInteger?),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigIntegerNullable
			},
			{
				typeof(global::System.Uri),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Uri
			},
			{
				typeof(string),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.String
			},
			{
				typeof(byte[]),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Bytes
			},
			{
				typeof(global::System.DBNull),
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DBNull
			}
		};

		private static readonly global::Newtonsoft.Json.Utilities.TypeInformation[] PrimitiveTypeCodes = new global::Newtonsoft.Json.Utilities.TypeInformation[19]
		{
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(object), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Empty),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(object), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Object),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(object), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DBNull),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(bool), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Boolean),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(char), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Char),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(sbyte), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(byte), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(short), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(ushort), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(int), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(uint), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(long), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(ulong), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(float), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Single),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(double), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Double),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(decimal), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Decimal),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(global::System.DateTime), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(object), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Empty),
			new global::Newtonsoft.Json.Utilities.TypeInformation(typeof(string), global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.String)
		};

		private static readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type>, global::System.Func<object?, object?>?> CastConverters = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type>, global::System.Func<object, object>>(CreateCastConverter);

		public static global::Newtonsoft.Json.Utilities.PrimitiveTypeCode GetTypeCode(global::System.Type t)
		{
			bool isEnum;
			return GetTypeCode(t, out isEnum);
		}

		public static global::Newtonsoft.Json.Utilities.PrimitiveTypeCode GetTypeCode(global::System.Type t, out bool isEnum)
		{
			if (TypeCodeMap.TryGetValue(t, out var value))
			{
				isEnum = false;
				return value;
			}
			if (t.IsEnum())
			{
				isEnum = true;
				return GetTypeCode(global::System.Enum.GetUnderlyingType(t));
			}
			if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(t))
			{
				global::System.Type underlyingType = global::System.Nullable.GetUnderlyingType(t);
				if (underlyingType.IsEnum())
				{
					global::System.Type t2 = typeof(global::System.Nullable<>).MakeGenericType(global::System.Enum.GetUnderlyingType(underlyingType));
					isEnum = true;
					return GetTypeCode(t2);
				}
			}
			isEnum = false;
			return global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Object;
		}

		public static global::Newtonsoft.Json.Utilities.TypeInformation GetTypeInformation(global::System.IConvertible convertable)
		{
			return PrimitiveTypeCodes[(int)convertable.GetTypeCode()];
		}

		public static bool IsConvertible(global::System.Type t)
		{
			return typeof(global::System.IConvertible).IsAssignableFrom(t);
		}

		public static global::System.TimeSpan ParseTimeSpan(string input)
		{
			return global::System.TimeSpan.Parse(input, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		private static global::System.Func<object?, object?>? CreateCastConverter(global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type> t)
		{
			global::System.Type value = t.Value1;
			global::System.Type value2 = t.Value2;
			global::System.Reflection.MethodInfo methodInfo = value2.GetMethod("op_Implicit", new global::System.Type[1] { value }) ?? value2.GetMethod("op_Explicit", new global::System.Type[1] { value });
			if (methodInfo == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Utilities.MethodCall<object?, object?> call = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
			return (object? o) => call(null, o);
		}

		internal static global::System.Numerics.BigInteger ToBigInteger(object value)
		{
			if (value is global::System.Numerics.BigInteger)
			{
				return (global::System.Numerics.BigInteger)value;
			}
			if (value is string value2)
			{
				return global::System.Numerics.BigInteger.Parse(value2, global::System.Globalization.CultureInfo.InvariantCulture);
			}
			if (value is float value3)
			{
				return new global::System.Numerics.BigInteger(value3);
			}
			if (value is double value4)
			{
				return new global::System.Numerics.BigInteger(value4);
			}
			if (value is decimal value5)
			{
				return new global::System.Numerics.BigInteger(value5);
			}
			if (value is int value6)
			{
				return new global::System.Numerics.BigInteger(value6);
			}
			if (value is long value7)
			{
				return new global::System.Numerics.BigInteger(value7);
			}
			if (value is uint value8)
			{
				return new global::System.Numerics.BigInteger(value8);
			}
			if (value is ulong value9)
			{
				return new global::System.Numerics.BigInteger(value9);
			}
			if (value is byte[] value10)
			{
				return new global::System.Numerics.BigInteger(value10);
			}
			throw new global::System.InvalidCastException("Cannot convert {0} to BigInteger.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, value.GetType()));
		}

		public static object FromBigInteger(global::System.Numerics.BigInteger i, global::System.Type targetType)
		{
			if (targetType == typeof(decimal))
			{
				return (decimal)i;
			}
			if (targetType == typeof(double))
			{
				return (double)i;
			}
			if (targetType == typeof(float))
			{
				return (float)i;
			}
			if (targetType == typeof(ulong))
			{
				return (ulong)i;
			}
			if (targetType == typeof(bool))
			{
				return i != 0L;
			}
			try
			{
				return global::System.Convert.ChangeType((long)i, targetType, global::System.Globalization.CultureInfo.InvariantCulture);
			}
			catch (global::System.Exception innerException)
			{
				throw new global::System.InvalidOperationException("Can not convert from BigInteger to {0}.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, targetType), innerException);
			}
		}

		public static object Convert(object initialValue, global::System.Globalization.CultureInfo culture, global::System.Type targetType)
		{
			object value;
			return TryConvertInternal(initialValue, culture, targetType, out value) switch
			{
				global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success => value, 
				global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.CannotConvertNull => throw new global::System.Exception("Can not convert null {0} into non-nullable {1}.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, initialValue.GetType(), targetType)), 
				global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.NotInstantiableType => throw new global::System.ArgumentException("Target type {0} is not a value type or a non-abstract class.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, targetType), "targetType"), 
				global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.NoValidConversion => throw new global::System.InvalidOperationException("Can not convert from {0} to {1}.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, initialValue.GetType(), targetType)), 
				_ => throw new global::System.InvalidOperationException("Unexpected conversion result."), 
			};
		}

		private static bool TryConvert(object? initialValue, global::System.Globalization.CultureInfo culture, global::System.Type targetType, out object? value)
		{
			try
			{
				if (TryConvertInternal(initialValue, culture, targetType, out value) == global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success)
				{
					return true;
				}
				value = null;
				return false;
			}
			catch
			{
				value = null;
				return false;
			}
		}

		private static global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult TryConvertInternal(object? initialValue, global::System.Globalization.CultureInfo culture, global::System.Type targetType, out object? value)
		{
			if (initialValue == null)
			{
				throw new global::System.ArgumentNullException("initialValue");
			}
			if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(targetType))
			{
				targetType = global::System.Nullable.GetUnderlyingType(targetType);
			}
			global::System.Type type = initialValue.GetType();
			if (targetType == type)
			{
				value = initialValue;
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
			}
			if (IsConvertible(initialValue.GetType()) && IsConvertible(targetType))
			{
				if (targetType.IsEnum())
				{
					if (initialValue is string)
					{
						value = global::System.Enum.Parse(targetType, initialValue.ToString(), ignoreCase: true);
						return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
					}
					if (IsInteger(initialValue))
					{
						value = global::System.Enum.ToObject(targetType, initialValue);
						return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
					}
				}
				value = global::System.Convert.ChangeType(initialValue, targetType, culture);
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is global::System.DateTime dateTime && targetType == typeof(global::System.DateTimeOffset))
			{
				value = new global::System.DateTimeOffset(dateTime);
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is byte[] b && targetType == typeof(global::System.Guid))
			{
				value = new global::System.Guid(b);
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is global::System.Guid guid && targetType == typeof(byte[]))
			{
				value = guid.ToByteArray();
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is string text)
			{
				if (targetType == typeof(global::System.Guid))
				{
					value = new global::System.Guid(text);
					return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(global::System.Uri))
				{
					value = new global::System.Uri(text, global::System.UriKind.RelativeOrAbsolute);
					return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(global::System.TimeSpan))
				{
					value = ParseTimeSpan(text);
					return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(byte[]))
				{
					value = global::System.Convert.FromBase64String(text);
					return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(global::System.Version))
				{
					if (VersionTryParse(text, out global::System.Version result))
					{
						value = result;
						return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
					}
					value = null;
					return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.NoValidConversion;
				}
				if (typeof(global::System.Type).IsAssignableFrom(targetType))
				{
					value = global::System.Type.GetType(text, throwOnError: true);
					return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
				}
			}
			if (targetType == typeof(global::System.Numerics.BigInteger))
			{
				value = ToBigInteger(initialValue);
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is global::System.Numerics.BigInteger i)
			{
				value = FromBigInteger(i, targetType);
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
			}
			global::System.ComponentModel.TypeConverter converter = global::System.ComponentModel.TypeDescriptor.GetConverter(type);
			if (converter != null && converter.CanConvertTo(targetType))
			{
				value = converter.ConvertTo(null, culture, initialValue, targetType);
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
			}
			global::System.ComponentModel.TypeConverter converter2 = global::System.ComponentModel.TypeDescriptor.GetConverter(targetType);
			if (converter2 != null && converter2.CanConvertFrom(type))
			{
				value = converter2.ConvertFrom(null, culture, initialValue);
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
			}
			if (initialValue == global::System.DBNull.Value)
			{
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(targetType))
				{
					value = EnsureTypeAssignable(null, type, targetType);
					return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.Success;
				}
				value = null;
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.CannotConvertNull;
			}
			if (targetType.IsInterface() || targetType.IsGenericTypeDefinition() || targetType.IsAbstract())
			{
				value = null;
				return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.NotInstantiableType;
			}
			value = null;
			return global::Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult.NoValidConversion;
		}

		public static object? ConvertOrCast(object? initialValue, global::System.Globalization.CultureInfo culture, global::System.Type targetType)
		{
			if (targetType == typeof(object))
			{
				return initialValue;
			}
			if (initialValue == null && global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			if (TryConvert(initialValue, culture, targetType, out object value))
			{
				return value;
			}
			return EnsureTypeAssignable(initialValue, global::Newtonsoft.Json.Utilities.ReflectionUtils.GetObjectType(initialValue), targetType);
		}

		private static object? EnsureTypeAssignable(object? value, global::System.Type initialType, global::System.Type targetType)
		{
			if (value != null)
			{
				global::System.Type type = value.GetType();
				if (targetType.IsAssignableFrom(type))
				{
					return value;
				}
				global::System.Func<object, object> func = CastConverters.Get(new global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type>(type, targetType));
				if (func != null)
				{
					return func(value);
				}
			}
			else if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			throw new global::System.ArgumentException("Could not cast or convert from {0} to {1}.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, initialType?.ToString() ?? "{null}", targetType));
		}

		public static bool VersionTryParse(string input, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::System.Version? result)
		{
			return global::System.Version.TryParse(input, out result);
		}

		public static bool IsInteger(object value)
		{
			switch (GetTypeCode(value.GetType()))
			{
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64:
				return true;
			default:
				return false;
			}
		}

		public static global::Newtonsoft.Json.Utilities.ParseResult Int32TryParse(char[] chars, int start, int length, out int value)
		{
			value = 0;
			if (length == 0)
			{
				return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int num = start + length;
			if (length > 10 || (length == 10 && chars[start] - 48 > 2))
			{
				for (int i = start; i < num; i++)
				{
					int num2 = chars[i] - 48;
					if (num2 < 0 || num2 > 9)
					{
						return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
					}
				}
				return global::Newtonsoft.Json.Utilities.ParseResult.Overflow;
			}
			for (int j = start; j < num; j++)
			{
				int num3 = chars[j] - 48;
				if (num3 < 0 || num3 > 9)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				int num4 = 10 * value - num3;
				if (num4 > value)
				{
					for (j++; j < num; j++)
					{
						num3 = chars[j] - 48;
						if (num3 < 0 || num3 > 9)
						{
							return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
						}
					}
					return global::Newtonsoft.Json.Utilities.ParseResult.Overflow;
				}
				value = num4;
			}
			if (!flag)
			{
				if (value == int.MinValue)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Overflow;
				}
				value = -value;
			}
			return global::Newtonsoft.Json.Utilities.ParseResult.Success;
		}

		public static global::Newtonsoft.Json.Utilities.ParseResult Int64TryParse(char[] chars, int start, int length, out long value)
		{
			value = 0L;
			if (length == 0)
			{
				return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int num = start + length;
			if (length > 19)
			{
				for (int i = start; i < num; i++)
				{
					int num2 = chars[i] - 48;
					if (num2 < 0 || num2 > 9)
					{
						return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
					}
				}
				return global::Newtonsoft.Json.Utilities.ParseResult.Overflow;
			}
			for (int j = start; j < num; j++)
			{
				int num3 = chars[j] - 48;
				if (num3 < 0 || num3 > 9)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				long num4 = 10 * value - num3;
				if (num4 > value)
				{
					for (j++; j < num; j++)
					{
						num3 = chars[j] - 48;
						if (num3 < 0 || num3 > 9)
						{
							return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
						}
					}
					return global::Newtonsoft.Json.Utilities.ParseResult.Overflow;
				}
				value = num4;
			}
			if (!flag)
			{
				if (value == long.MinValue)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Overflow;
				}
				value = -value;
			}
			return global::Newtonsoft.Json.Utilities.ParseResult.Success;
		}

		public static global::Newtonsoft.Json.Utilities.ParseResult DecimalTryParse(char[] chars, int start, int length, out decimal value)
		{
			value = default(decimal);
			if (length == 0)
			{
				return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int i = start;
			int num = start + length;
			int num2 = num;
			int num3 = num;
			int num4 = 0;
			ulong num5 = 0uL;
			ulong num6 = 0uL;
			int num7 = 0;
			int num8 = 0;
			char? c = null;
			bool? flag2 = null;
			for (; i < num; i++)
			{
				char c2 = chars[i];
				if (c2 == '.')
				{
					goto IL_0074;
				}
				if (c2 == 'E' || c2 == 'e')
				{
					goto IL_0091;
				}
				if (c2 < '0' || c2 > '9')
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				if (i == start && c2 == '0')
				{
					i++;
					if (i != num)
					{
						c2 = chars[i];
						if (c2 == '.')
						{
							goto IL_0074;
						}
						if (c2 != 'e' && c2 != 'E')
						{
							return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
						}
						goto IL_0091;
					}
				}
				if (num7 < 29)
				{
					if (num7 == 28)
					{
						bool? flag3 = flag2;
						bool valueOrDefault;
						if (!flag3.HasValue)
						{
							flag2 = num5 > 7922816251426433759L || (num5 == 7922816251426433759L && (num6 > 354395033 || (num6 == 354395033 && c2 > '5')));
							bool? flag4 = flag2;
							valueOrDefault = flag4 == true;
						}
						else
						{
							valueOrDefault = flag3 == true;
						}
						if (valueOrDefault)
						{
							goto IL_01ff;
						}
					}
					if (num7 < 19)
					{
						num5 = num5 * 10 + (ulong)(c2 - 48);
					}
					else
					{
						num6 = num6 * 10 + (ulong)(c2 - 48);
					}
					num7++;
					continue;
				}
				goto IL_01ff;
				IL_0074:
				if (i == start)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				if (i + 1 == num)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				if (num2 != num)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				num2 = i + 1;
				continue;
				IL_01ff:
				if (!c.HasValue)
				{
					c = c2;
				}
				num8++;
				continue;
				IL_0091:
				if (i == start)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				if (i == num2)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				i++;
				if (i == num)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
				}
				if (num2 < num)
				{
					num3 = i - 1;
				}
				c2 = chars[i];
				bool flag5 = false;
				switch (c2)
				{
				case '-':
					flag5 = true;
					i++;
					break;
				case '+':
					i++;
					break;
				}
				for (; i < num; i++)
				{
					c2 = chars[i];
					if (c2 < '0' || c2 > '9')
					{
						return global::Newtonsoft.Json.Utilities.ParseResult.Invalid;
					}
					int num9 = 10 * num4 + (c2 - 48);
					if (num4 < num9)
					{
						num4 = num9;
					}
				}
				if (flag5)
				{
					num4 = -num4;
				}
			}
			num4 += num8;
			num4 -= num3 - num2;
			if (num7 <= 19)
			{
				value = num5;
			}
			else
			{
				value = (decimal)num5 / new decimal(1, 0, 0, isNegative: false, (byte)(num7 - 19)) + (decimal)num6;
			}
			if (num4 > 0)
			{
				num7 += num4;
				if (num7 > 29)
				{
					return global::Newtonsoft.Json.Utilities.ParseResult.Overflow;
				}
				if (num7 == 29)
				{
					if (num4 > 1)
					{
						value /= new decimal(1, 0, 0, isNegative: false, (byte)(num4 - 1));
						if (value > 7922816251426433759354395033m)
						{
							return global::Newtonsoft.Json.Utilities.ParseResult.Overflow;
						}
					}
					else if (value == 7922816251426433759354395033m && c > '5')
					{
						return global::Newtonsoft.Json.Utilities.ParseResult.Overflow;
					}
					value *= 10m;
				}
				else
				{
					value /= new decimal(1, 0, 0, isNegative: false, (byte)num4);
				}
			}
			else
			{
				if (c >= '5' && num4 >= -28)
				{
					++value;
				}
				if (num4 < 0)
				{
					if (num7 + num4 + 28 <= 0)
					{
						value = (flag ? 0m : 0m);
						return global::Newtonsoft.Json.Utilities.ParseResult.Success;
					}
					if (num4 >= -28)
					{
						value *= new decimal(1, 0, 0, isNegative: false, (byte)(-num4));
					}
					else
					{
						value /= 10000000000000000000000000000m;
						value *= new decimal(1, 0, 0, isNegative: false, (byte)(-num4 - 28));
					}
				}
			}
			if (flag)
			{
				value = -value;
			}
			return global::Newtonsoft.Json.Utilities.ParseResult.Success;
		}

		public static bool TryConvertGuid(string s, out global::System.Guid g)
		{
			return global::System.Guid.TryParseExact(s, "D", out g);
		}

		public static bool TryHexTextToInt(char[] text, int start, int end, out int value)
		{
			value = 0;
			for (int i = start; i < end; i++)
			{
				char c = text[i];
				int num;
				if (c <= '9' && c >= '0')
				{
					num = c - 48;
				}
				else if (c <= 'F' && c >= 'A')
				{
					num = c - 55;
				}
				else
				{
					if (c > 'f' || c < 'a')
					{
						value = 0;
						return false;
					}
					num = c - 87;
				}
				value += num << (end - 1 - i) * 4;
			}
			return true;
		}
	}
}
