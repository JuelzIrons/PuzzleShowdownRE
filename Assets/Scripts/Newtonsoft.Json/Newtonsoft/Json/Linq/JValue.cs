namespace Newtonsoft.Json.Linq
{
	public class JValue : global::Newtonsoft.Json.Linq.JToken, global::System.IEquatable<global::Newtonsoft.Json.Linq.JValue>, global::System.IFormattable, global::System.IComparable, global::System.IComparable<global::Newtonsoft.Json.Linq.JValue>, global::System.IConvertible
	{
		private class JValueDynamicProxy : global::Newtonsoft.Json.Utilities.DynamicProxy<global::Newtonsoft.Json.Linq.JValue>
		{
			public override bool TryConvert(global::Newtonsoft.Json.Linq.JValue instance, global::System.Dynamic.ConvertBinder binder, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out object? result)
			{
				if (binder.Type == typeof(global::Newtonsoft.Json.Linq.JValue) || binder.Type == typeof(global::Newtonsoft.Json.Linq.JToken))
				{
					result = instance;
					return true;
				}
				object value = instance.Value;
				if (value == null)
				{
					result = null;
					return global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullable(binder.Type);
				}
				result = global::Newtonsoft.Json.Utilities.ConvertUtils.Convert(value, global::System.Globalization.CultureInfo.InvariantCulture, binder.Type);
				return true;
			}

			public override bool TryBinaryOperation(global::Newtonsoft.Json.Linq.JValue instance, global::System.Dynamic.BinaryOperationBinder binder, object arg, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out object? result)
			{
				object objB = ((arg is global::Newtonsoft.Json.Linq.JValue jValue) ? jValue.Value : arg);
				switch (binder.Operation)
				{
				case global::System.Linq.Expressions.ExpressionType.Equal:
					result = Compare(instance.Type, instance.Value, objB) == 0;
					return true;
				case global::System.Linq.Expressions.ExpressionType.NotEqual:
					result = Compare(instance.Type, instance.Value, objB) != 0;
					return true;
				case global::System.Linq.Expressions.ExpressionType.GreaterThan:
					result = Compare(instance.Type, instance.Value, objB) > 0;
					return true;
				case global::System.Linq.Expressions.ExpressionType.GreaterThanOrEqual:
					result = Compare(instance.Type, instance.Value, objB) >= 0;
					return true;
				case global::System.Linq.Expressions.ExpressionType.LessThan:
					result = Compare(instance.Type, instance.Value, objB) < 0;
					return true;
				case global::System.Linq.Expressions.ExpressionType.LessThanOrEqual:
					result = Compare(instance.Type, instance.Value, objB) <= 0;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Add:
				case global::System.Linq.Expressions.ExpressionType.Divide:
				case global::System.Linq.Expressions.ExpressionType.Multiply:
				case global::System.Linq.Expressions.ExpressionType.Subtract:
				case global::System.Linq.Expressions.ExpressionType.AddAssign:
				case global::System.Linq.Expressions.ExpressionType.DivideAssign:
				case global::System.Linq.Expressions.ExpressionType.MultiplyAssign:
				case global::System.Linq.Expressions.ExpressionType.SubtractAssign:
					if (Operation(binder.Operation, instance.Value, objB, out result))
					{
						result = new global::Newtonsoft.Json.Linq.JValue(result);
						return true;
					}
					break;
				}
				result = null;
				return false;
			}
		}

		private global::Newtonsoft.Json.Linq.JTokenType _valueType;

		private object? _value;

		public override bool HasValues => false;

		public override global::Newtonsoft.Json.Linq.JTokenType Type => _valueType;

		public new object? Value
		{
			get
			{
				return _value;
			}
			set
			{
				global::System.Type obj = _value?.GetType();
				global::System.Type type = value?.GetType();
				if (obj != type)
				{
					_valueType = GetValueType(_valueType, value);
				}
				_value = value;
			}
		}

		public override global::System.Threading.Tasks.Task WriteToAsync(global::Newtonsoft.Json.JsonWriter writer, global::System.Threading.CancellationToken cancellationToken, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && _value != null)
			{
				global::Newtonsoft.Json.JsonConverter matchingConverter = global::Newtonsoft.Json.JsonSerializer.GetMatchingConverter(converters, _value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, _value, global::Newtonsoft.Json.JsonSerializer.CreateDefault());
					return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
				}
			}
			switch (_valueType)
			{
			case global::Newtonsoft.Json.Linq.JTokenType.Comment:
				return writer.WriteCommentAsync(_value?.ToString(), cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Raw:
				return writer.WriteRawValueAsync(_value?.ToString(), cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Null:
				return writer.WriteNullAsync(cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Undefined:
				return writer.WriteUndefinedAsync(cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Integer:
				if (_value is int value5)
				{
					return writer.WriteValueAsync(value5, cancellationToken);
				}
				if (_value is long value6)
				{
					return writer.WriteValueAsync(value6, cancellationToken);
				}
				if (_value is ulong value7)
				{
					return writer.WriteValueAsync(value7, cancellationToken);
				}
				if (_value is global::System.Numerics.BigInteger bigInteger)
				{
					return writer.WriteValueAsync(bigInteger, cancellationToken);
				}
				return writer.WriteValueAsync(global::System.Convert.ToInt64(_value, global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Float:
				if (_value is decimal value)
				{
					return writer.WriteValueAsync(value, cancellationToken);
				}
				if (_value is double value2)
				{
					return writer.WriteValueAsync(value2, cancellationToken);
				}
				if (_value is float value3)
				{
					return writer.WriteValueAsync(value3, cancellationToken);
				}
				return writer.WriteValueAsync(global::System.Convert.ToDouble(_value, global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.String:
				return writer.WriteValueAsync(_value?.ToString(), cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Boolean:
				return writer.WriteValueAsync(global::System.Convert.ToBoolean(_value, global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Date:
				if (_value is global::System.DateTimeOffset value4)
				{
					return writer.WriteValueAsync(value4, cancellationToken);
				}
				return writer.WriteValueAsync(global::System.Convert.ToDateTime(_value, global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Bytes:
				return writer.WriteValueAsync((byte[])_value, cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Guid:
				return writer.WriteValueAsync((_value != null) ? ((global::System.Guid?)_value) : ((global::System.Guid?)null), cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.TimeSpan:
				return writer.WriteValueAsync((_value != null) ? ((global::System.TimeSpan?)_value) : ((global::System.TimeSpan?)null), cancellationToken);
			case global::Newtonsoft.Json.Linq.JTokenType.Uri:
				return writer.WriteValueAsync((global::System.Uri)_value, cancellationToken);
			default:
				throw global::Newtonsoft.Json.Utilities.MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", _valueType, "Unexpected token type.");
			}
		}

		internal JValue(object? value, global::Newtonsoft.Json.Linq.JTokenType type)
		{
			_value = value;
			_valueType = type;
		}

		internal JValue(global::Newtonsoft.Json.Linq.JValue other, global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
			: this(other.Value, other.Type)
		{
			if (settings == null || settings.CopyAnnotations)
			{
				CopyAnnotations(this, other);
			}
		}

		public JValue(global::Newtonsoft.Json.Linq.JValue other)
			: this(other.Value, other.Type)
		{
		}

		public JValue(long value)
			: this(global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(value), global::Newtonsoft.Json.Linq.JTokenType.Integer)
		{
		}

		public JValue(decimal value)
			: this(global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(value), global::Newtonsoft.Json.Linq.JTokenType.Float)
		{
		}

		public JValue(char value)
			: this(value, global::Newtonsoft.Json.Linq.JTokenType.String)
		{
		}

		[global::System.CLSCompliant(false)]
		public JValue(ulong value)
			: this(value, global::Newtonsoft.Json.Linq.JTokenType.Integer)
		{
		}

		public JValue(double value)
			: this(global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(value), global::Newtonsoft.Json.Linq.JTokenType.Float)
		{
		}

		public JValue(float value)
			: this(value, global::Newtonsoft.Json.Linq.JTokenType.Float)
		{
		}

		public JValue(global::System.DateTime value)
			: this(value, global::Newtonsoft.Json.Linq.JTokenType.Date)
		{
		}

		public JValue(global::System.DateTimeOffset value)
			: this(value, global::Newtonsoft.Json.Linq.JTokenType.Date)
		{
		}

		public JValue(bool value)
			: this(global::Newtonsoft.Json.Utilities.BoxedPrimitives.Get(value), global::Newtonsoft.Json.Linq.JTokenType.Boolean)
		{
		}

		public JValue(string? value)
			: this(value, global::Newtonsoft.Json.Linq.JTokenType.String)
		{
		}

		public JValue(global::System.Guid value)
			: this(value, global::Newtonsoft.Json.Linq.JTokenType.Guid)
		{
		}

		public JValue(global::System.Uri? value)
			: this(value, (value != null) ? global::Newtonsoft.Json.Linq.JTokenType.Uri : global::Newtonsoft.Json.Linq.JTokenType.Null)
		{
		}

		public JValue(global::System.TimeSpan value)
			: this(value, global::Newtonsoft.Json.Linq.JTokenType.TimeSpan)
		{
		}

		public JValue(object? value)
			: this(value, GetValueType(null, value))
		{
		}

		internal override bool DeepEquals(global::Newtonsoft.Json.Linq.JToken node)
		{
			if (!(node is global::Newtonsoft.Json.Linq.JValue jValue))
			{
				return false;
			}
			if (jValue == this)
			{
				return true;
			}
			return ValuesEquals(this, jValue);
		}

		private static int CompareBigInteger(global::System.Numerics.BigInteger i1, object i2)
		{
			int num = i1.CompareTo(global::Newtonsoft.Json.Utilities.ConvertUtils.ToBigInteger(i2));
			if (num != 0)
			{
				return num;
			}
			if (i2 is decimal num2)
			{
				decimal num3 = 0m;
				return num3.CompareTo(global::System.Math.Abs(num2 - global::System.Math.Truncate(num2)));
			}
			if (i2 is double || i2 is float)
			{
				double num4 = global::System.Convert.ToDouble(i2, global::System.Globalization.CultureInfo.InvariantCulture);
				return 0.0.CompareTo(global::System.Math.Abs(num4 - global::System.Math.Truncate(num4)));
			}
			return num;
		}

		internal static int Compare(global::Newtonsoft.Json.Linq.JTokenType valueType, object? objA, object? objB)
		{
			if (objA == objB)
			{
				return 0;
			}
			if (objB == null)
			{
				return 1;
			}
			if (objA == null)
			{
				return -1;
			}
			switch (valueType)
			{
			case global::Newtonsoft.Json.Linq.JTokenType.Integer:
				if (objA is global::System.Numerics.BigInteger i3)
				{
					return CompareBigInteger(i3, objB);
				}
				if (objB is global::System.Numerics.BigInteger i4)
				{
					return -CompareBigInteger(i4, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return global::System.Convert.ToDecimal(objA, global::System.Globalization.CultureInfo.InvariantCulture).CompareTo(global::System.Convert.ToDecimal(objB, global::System.Globalization.CultureInfo.InvariantCulture));
				}
				if (objA is float || objB is float || objA is double || objB is double)
				{
					return CompareFloat(objA, objB);
				}
				return global::System.Convert.ToInt64(objA, global::System.Globalization.CultureInfo.InvariantCulture).CompareTo(global::System.Convert.ToInt64(objB, global::System.Globalization.CultureInfo.InvariantCulture));
			case global::Newtonsoft.Json.Linq.JTokenType.Float:
				if (objA is global::System.Numerics.BigInteger i)
				{
					return CompareBigInteger(i, objB);
				}
				if (objB is global::System.Numerics.BigInteger i2)
				{
					return -CompareBigInteger(i2, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return global::System.Convert.ToDecimal(objA, global::System.Globalization.CultureInfo.InvariantCulture).CompareTo(global::System.Convert.ToDecimal(objB, global::System.Globalization.CultureInfo.InvariantCulture));
				}
				return CompareFloat(objA, objB);
			case global::Newtonsoft.Json.Linq.JTokenType.Comment:
			case global::Newtonsoft.Json.Linq.JTokenType.String:
			case global::Newtonsoft.Json.Linq.JTokenType.Raw:
			{
				string strA = global::System.Convert.ToString(objA, global::System.Globalization.CultureInfo.InvariantCulture);
				string strB = global::System.Convert.ToString(objB, global::System.Globalization.CultureInfo.InvariantCulture);
				return string.CompareOrdinal(strA, strB);
			}
			case global::Newtonsoft.Json.Linq.JTokenType.Boolean:
			{
				bool flag = global::System.Convert.ToBoolean(objA, global::System.Globalization.CultureInfo.InvariantCulture);
				bool value4 = global::System.Convert.ToBoolean(objB, global::System.Globalization.CultureInfo.InvariantCulture);
				return flag.CompareTo(value4);
			}
			case global::Newtonsoft.Json.Linq.JTokenType.Date:
			{
				if (objA is global::System.DateTime dateTime)
				{
					global::System.DateTime value2 = ((!(objB is global::System.DateTimeOffset dateTimeOffset)) ? global::System.Convert.ToDateTime(objB, global::System.Globalization.CultureInfo.InvariantCulture) : dateTimeOffset.DateTime);
					return dateTime.CompareTo(value2);
				}
				global::System.DateTimeOffset dateTimeOffset2 = (global::System.DateTimeOffset)objA;
				global::System.DateTimeOffset other = ((!(objB is global::System.DateTimeOffset)) ? new global::System.DateTimeOffset(global::System.Convert.ToDateTime(objB, global::System.Globalization.CultureInfo.InvariantCulture)) : ((global::System.DateTimeOffset)objB));
				return dateTimeOffset2.CompareTo(other);
			}
			case global::Newtonsoft.Json.Linq.JTokenType.Bytes:
				if (!(objB is byte[] a))
				{
					throw new global::System.ArgumentException("Object must be of type byte[].");
				}
				return global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ByteArrayCompare(objA as byte[], a);
			case global::Newtonsoft.Json.Linq.JTokenType.Guid:
			{
				if (!(objB is global::System.Guid))
				{
					throw new global::System.ArgumentException("Object must be of type Guid.");
				}
				global::System.Guid guid = (global::System.Guid)objA;
				global::System.Guid value3 = (global::System.Guid)objB;
				return guid.CompareTo(value3);
			}
			case global::Newtonsoft.Json.Linq.JTokenType.Uri:
			{
				global::System.Uri uri = objB as global::System.Uri;
				if (uri == null)
				{
					throw new global::System.ArgumentException("Object must be of type Uri.");
				}
				global::System.Uri uri2 = (global::System.Uri)objA;
				return global::System.Collections.Generic.Comparer<string>.Default.Compare(uri2.ToString(), uri.ToString());
			}
			case global::Newtonsoft.Json.Linq.JTokenType.TimeSpan:
			{
				if (!(objB is global::System.TimeSpan))
				{
					throw new global::System.ArgumentException("Object must be of type TimeSpan.");
				}
				global::System.TimeSpan timeSpan = (global::System.TimeSpan)objA;
				global::System.TimeSpan value = (global::System.TimeSpan)objB;
				return timeSpan.CompareTo(value);
			}
			default:
				throw global::Newtonsoft.Json.Utilities.MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected value type: {0}", global::System.Globalization.CultureInfo.InvariantCulture, valueType));
			}
		}

		private static int CompareFloat(object objA, object objB)
		{
			double d = global::System.Convert.ToDouble(objA, global::System.Globalization.CultureInfo.InvariantCulture);
			double num = global::System.Convert.ToDouble(objB, global::System.Globalization.CultureInfo.InvariantCulture);
			if (global::Newtonsoft.Json.Utilities.MathUtils.ApproxEquals(d, num))
			{
				return 0;
			}
			return d.CompareTo(num);
		}

		private static bool Operation(global::System.Linq.Expressions.ExpressionType operation, object? objA, object? objB, out object? result)
		{
			if ((objA is string || objB is string) && (operation == global::System.Linq.Expressions.ExpressionType.Add || operation == global::System.Linq.Expressions.ExpressionType.AddAssign))
			{
				result = objA?.ToString() + objB;
				return true;
			}
			if (objA is global::System.Numerics.BigInteger || objB is global::System.Numerics.BigInteger)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				global::System.Numerics.BigInteger bigInteger = global::Newtonsoft.Json.Utilities.ConvertUtils.ToBigInteger(objA);
				global::System.Numerics.BigInteger bigInteger2 = global::Newtonsoft.Json.Utilities.ConvertUtils.ToBigInteger(objB);
				switch (operation)
				{
				case global::System.Linq.Expressions.ExpressionType.Add:
				case global::System.Linq.Expressions.ExpressionType.AddAssign:
					result = bigInteger + bigInteger2;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Subtract:
				case global::System.Linq.Expressions.ExpressionType.SubtractAssign:
					result = bigInteger - bigInteger2;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Multiply:
				case global::System.Linq.Expressions.ExpressionType.MultiplyAssign:
					result = bigInteger * bigInteger2;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Divide:
				case global::System.Linq.Expressions.ExpressionType.DivideAssign:
					result = bigInteger / bigInteger2;
					return true;
				}
			}
			else if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				decimal num = global::System.Convert.ToDecimal(objA, global::System.Globalization.CultureInfo.InvariantCulture);
				decimal num2 = global::System.Convert.ToDecimal(objB, global::System.Globalization.CultureInfo.InvariantCulture);
				switch (operation)
				{
				case global::System.Linq.Expressions.ExpressionType.Add:
				case global::System.Linq.Expressions.ExpressionType.AddAssign:
					result = num + num2;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Subtract:
				case global::System.Linq.Expressions.ExpressionType.SubtractAssign:
					result = num - num2;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Multiply:
				case global::System.Linq.Expressions.ExpressionType.MultiplyAssign:
					result = num * num2;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Divide:
				case global::System.Linq.Expressions.ExpressionType.DivideAssign:
					result = num / num2;
					return true;
				}
			}
			else if (objA is float || objB is float || objA is double || objB is double)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				double num3 = global::System.Convert.ToDouble(objA, global::System.Globalization.CultureInfo.InvariantCulture);
				double num4 = global::System.Convert.ToDouble(objB, global::System.Globalization.CultureInfo.InvariantCulture);
				switch (operation)
				{
				case global::System.Linq.Expressions.ExpressionType.Add:
				case global::System.Linq.Expressions.ExpressionType.AddAssign:
					result = num3 + num4;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Subtract:
				case global::System.Linq.Expressions.ExpressionType.SubtractAssign:
					result = num3 - num4;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Multiply:
				case global::System.Linq.Expressions.ExpressionType.MultiplyAssign:
					result = num3 * num4;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Divide:
				case global::System.Linq.Expressions.ExpressionType.DivideAssign:
					result = num3 / num4;
					return true;
				}
			}
			else if (objA is int || objA is uint || objA is long || objA is short || objA is ushort || objA is sbyte || objA is byte || objB is int || objB is uint || objB is long || objB is short || objB is ushort || objB is sbyte || objB is byte)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				long num5 = global::System.Convert.ToInt64(objA, global::System.Globalization.CultureInfo.InvariantCulture);
				long num6 = global::System.Convert.ToInt64(objB, global::System.Globalization.CultureInfo.InvariantCulture);
				switch (operation)
				{
				case global::System.Linq.Expressions.ExpressionType.Add:
				case global::System.Linq.Expressions.ExpressionType.AddAssign:
					result = num5 + num6;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Subtract:
				case global::System.Linq.Expressions.ExpressionType.SubtractAssign:
					result = num5 - num6;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Multiply:
				case global::System.Linq.Expressions.ExpressionType.MultiplyAssign:
					result = num5 * num6;
					return true;
				case global::System.Linq.Expressions.ExpressionType.Divide:
				case global::System.Linq.Expressions.ExpressionType.DivideAssign:
					result = num5 / num6;
					return true;
				}
			}
			result = null;
			return false;
		}

		internal override global::Newtonsoft.Json.Linq.JToken CloneToken(global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
		{
			return new global::Newtonsoft.Json.Linq.JValue(this, settings);
		}

		public static global::Newtonsoft.Json.Linq.JValue CreateComment(string? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value, global::Newtonsoft.Json.Linq.JTokenType.Comment);
		}

		public static global::Newtonsoft.Json.Linq.JValue CreateString(string? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value, global::Newtonsoft.Json.Linq.JTokenType.String);
		}

		public static global::Newtonsoft.Json.Linq.JValue CreateNull()
		{
			return new global::Newtonsoft.Json.Linq.JValue(null, global::Newtonsoft.Json.Linq.JTokenType.Null);
		}

		public static global::Newtonsoft.Json.Linq.JValue CreateUndefined()
		{
			return new global::Newtonsoft.Json.Linq.JValue(null, global::Newtonsoft.Json.Linq.JTokenType.Undefined);
		}

		private static global::Newtonsoft.Json.Linq.JTokenType GetValueType(global::Newtonsoft.Json.Linq.JTokenType? current, object? value)
		{
			if (value == null)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Null;
			}
			if (value == global::System.DBNull.Value)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Null;
			}
			if (value is string)
			{
				return GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Integer;
			}
			if (value is global::System.Enum)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Integer;
			}
			if (value is global::System.Numerics.BigInteger)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Float;
			}
			if (value is global::System.DateTime)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Date;
			}
			if (value is global::System.DateTimeOffset)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Date;
			}
			if (value is byte[])
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Bytes;
			}
			if (value is bool)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Boolean;
			}
			if (value is global::System.Guid)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Guid;
			}
			if (value is global::System.Uri)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Uri;
			}
			if (value is global::System.TimeSpan)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.TimeSpan;
			}
			throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not determine JSON object type for type {0}.", global::System.Globalization.CultureInfo.InvariantCulture, value.GetType()));
		}

		private static global::Newtonsoft.Json.Linq.JTokenType GetStringValueType(global::Newtonsoft.Json.Linq.JTokenType? current)
		{
			if (!current.HasValue)
			{
				return global::Newtonsoft.Json.Linq.JTokenType.String;
			}
			global::Newtonsoft.Json.Linq.JTokenType valueOrDefault = current.GetValueOrDefault();
			if (valueOrDefault == global::Newtonsoft.Json.Linq.JTokenType.Comment || valueOrDefault == global::Newtonsoft.Json.Linq.JTokenType.String || valueOrDefault == global::Newtonsoft.Json.Linq.JTokenType.Raw)
			{
				return current.GetValueOrDefault();
			}
			return global::Newtonsoft.Json.Linq.JTokenType.String;
		}

		public override void WriteTo(global::Newtonsoft.Json.JsonWriter writer, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && _value != null)
			{
				global::Newtonsoft.Json.JsonConverter matchingConverter = global::Newtonsoft.Json.JsonSerializer.GetMatchingConverter(converters, _value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, _value, global::Newtonsoft.Json.JsonSerializer.CreateDefault());
					return;
				}
			}
			switch (_valueType)
			{
			case global::Newtonsoft.Json.Linq.JTokenType.Comment:
				writer.WriteComment(_value?.ToString());
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Raw:
				writer.WriteRawValue(_value?.ToString());
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Null:
				writer.WriteNull();
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Undefined:
				writer.WriteUndefined();
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Integer:
				if (_value is int value5)
				{
					writer.WriteValue(value5);
				}
				else if (_value is long value6)
				{
					writer.WriteValue(value6);
				}
				else if (_value is ulong value7)
				{
					writer.WriteValue(value7);
				}
				else if (_value is global::System.Numerics.BigInteger bigInteger)
				{
					writer.WriteValue(bigInteger);
				}
				else
				{
					writer.WriteValue(global::System.Convert.ToInt64(_value, global::System.Globalization.CultureInfo.InvariantCulture));
				}
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Float:
				if (_value is decimal value)
				{
					writer.WriteValue(value);
				}
				else if (_value is double value2)
				{
					writer.WriteValue(value2);
				}
				else if (_value is float value3)
				{
					writer.WriteValue(value3);
				}
				else
				{
					writer.WriteValue(global::System.Convert.ToDouble(_value, global::System.Globalization.CultureInfo.InvariantCulture));
				}
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.String:
				writer.WriteValue(_value?.ToString());
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Boolean:
				writer.WriteValue(global::System.Convert.ToBoolean(_value, global::System.Globalization.CultureInfo.InvariantCulture));
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Date:
				if (_value is global::System.DateTimeOffset value4)
				{
					writer.WriteValue(value4);
				}
				else
				{
					writer.WriteValue(global::System.Convert.ToDateTime(_value, global::System.Globalization.CultureInfo.InvariantCulture));
				}
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Bytes:
				writer.WriteValue((byte[])_value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Guid:
				writer.WriteValue((_value != null) ? ((global::System.Guid?)_value) : ((global::System.Guid?)null));
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.TimeSpan:
				writer.WriteValue((_value != null) ? ((global::System.TimeSpan?)_value) : ((global::System.TimeSpan?)null));
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Uri:
				writer.WriteValue((global::System.Uri)_value);
				break;
			default:
				throw global::Newtonsoft.Json.Utilities.MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", _valueType, "Unexpected token type.");
			}
		}

		internal override int GetDeepHashCode()
		{
			int num = ((_value != null) ? _value.GetHashCode() : 0);
			int valueType = (int)_valueType;
			return valueType.GetHashCode() ^ num;
		}

		private static bool ValuesEquals(global::Newtonsoft.Json.Linq.JValue v1, global::Newtonsoft.Json.Linq.JValue v2)
		{
			if (v1 != v2)
			{
				if (v1._valueType == v2._valueType)
				{
					return Compare(v1._valueType, v1._value, v2._value) == 0;
				}
				return false;
			}
			return true;
		}

		public bool Equals(global::Newtonsoft.Json.Linq.JValue? other)
		{
			if (other == null)
			{
				return false;
			}
			return ValuesEquals(this, other);
		}

		public override bool Equals(object? obj)
		{
			if (obj is global::Newtonsoft.Json.Linq.JValue other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			if (_value == null)
			{
				return 0;
			}
			return _value.GetHashCode();
		}

		public override string ToString()
		{
			if (_value == null)
			{
				return string.Empty;
			}
			return _value.ToString();
		}

		public string ToString(string format)
		{
			return ToString(format, global::System.Globalization.CultureInfo.CurrentCulture);
		}

		public string ToString(global::System.IFormatProvider? formatProvider)
		{
			return ToString(null, formatProvider);
		}

		public string ToString(string? format, global::System.IFormatProvider? formatProvider)
		{
			if (_value == null)
			{
				return string.Empty;
			}
			if (_value is global::System.IFormattable formattable)
			{
				return formattable.ToString(format, formatProvider);
			}
			return _value.ToString();
		}

		protected override global::System.Dynamic.DynamicMetaObject GetMetaObject(global::System.Linq.Expressions.Expression parameter)
		{
			return new global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<global::Newtonsoft.Json.Linq.JValue>(parameter, this, new global::Newtonsoft.Json.Linq.JValue.JValueDynamicProxy());
		}

		int global::System.IComparable.CompareTo(object? obj)
		{
			if (obj == null)
			{
				return 1;
			}
			object objB;
			global::Newtonsoft.Json.Linq.JTokenType valueType;
			if (obj is global::Newtonsoft.Json.Linq.JValue jValue)
			{
				objB = jValue.Value;
				valueType = ((_valueType == global::Newtonsoft.Json.Linq.JTokenType.String && _valueType != jValue._valueType) ? jValue._valueType : _valueType);
			}
			else
			{
				objB = obj;
				valueType = _valueType;
			}
			return Compare(valueType, _value, objB);
		}

		public int CompareTo(global::Newtonsoft.Json.Linq.JValue? obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return Compare((_valueType == global::Newtonsoft.Json.Linq.JTokenType.String && _valueType != obj._valueType) ? obj._valueType : _valueType, _value, obj._value);
		}

		global::System.TypeCode global::System.IConvertible.GetTypeCode()
		{
			if (_value == null)
			{
				return global::System.TypeCode.Empty;
			}
			if (_value is global::System.IConvertible convertible)
			{
				return convertible.GetTypeCode();
			}
			return global::System.TypeCode.Object;
		}

		bool global::System.IConvertible.ToBoolean(global::System.IFormatProvider? provider)
		{
			return (bool)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		char global::System.IConvertible.ToChar(global::System.IFormatProvider? provider)
		{
			return (char)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		sbyte global::System.IConvertible.ToSByte(global::System.IFormatProvider? provider)
		{
			return (sbyte)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		byte global::System.IConvertible.ToByte(global::System.IFormatProvider? provider)
		{
			return (byte)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		short global::System.IConvertible.ToInt16(global::System.IFormatProvider? provider)
		{
			return (short)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		ushort global::System.IConvertible.ToUInt16(global::System.IFormatProvider? provider)
		{
			return (ushort)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		int global::System.IConvertible.ToInt32(global::System.IFormatProvider? provider)
		{
			return (int)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		uint global::System.IConvertible.ToUInt32(global::System.IFormatProvider? provider)
		{
			return (uint)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		long global::System.IConvertible.ToInt64(global::System.IFormatProvider? provider)
		{
			return (long)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		ulong global::System.IConvertible.ToUInt64(global::System.IFormatProvider? provider)
		{
			return (ulong)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		float global::System.IConvertible.ToSingle(global::System.IFormatProvider? provider)
		{
			return (float)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		double global::System.IConvertible.ToDouble(global::System.IFormatProvider? provider)
		{
			return (double)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		decimal global::System.IConvertible.ToDecimal(global::System.IFormatProvider? provider)
		{
			return (decimal)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		global::System.DateTime global::System.IConvertible.ToDateTime(global::System.IFormatProvider? provider)
		{
			return (global::System.DateTime)(global::Newtonsoft.Json.Linq.JToken)this;
		}

		object global::System.IConvertible.ToType(global::System.Type conversionType, global::System.IFormatProvider? provider)
		{
			return ToObject(conversionType);
		}
	}
}
