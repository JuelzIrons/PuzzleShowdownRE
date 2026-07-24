namespace Newtonsoft.Json.Bson
{
	internal class BsonBinaryWriter
	{
		private static readonly global::System.Text.Encoding Encoding = new global::System.Text.UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

		private readonly global::System.IO.BinaryWriter _writer;

		private byte[] _largeByteBuffer;

		public global::System.DateTimeKind DateTimeKindHandling { get; set; }

		public BsonBinaryWriter(global::System.IO.BinaryWriter writer)
		{
			DateTimeKindHandling = global::System.DateTimeKind.Utc;
			_writer = writer;
		}

		public void Flush()
		{
			_writer.Flush();
		}

		public void Close()
		{
			_writer.Close();
		}

		public void WriteToken(global::Newtonsoft.Json.Bson.BsonToken t)
		{
			CalculateSize(t);
			WriteTokenInternal(t);
		}

		private void WriteTokenInternal(global::Newtonsoft.Json.Bson.BsonToken t)
		{
			switch (t.Type)
			{
			case global::Newtonsoft.Json.Bson.BsonType.Object:
			{
				global::Newtonsoft.Json.Bson.BsonObject bsonObject = (global::Newtonsoft.Json.Bson.BsonObject)t;
				_writer.Write(bsonObject.CalculatedSize);
				foreach (global::Newtonsoft.Json.Bson.BsonProperty item in bsonObject)
				{
					_writer.Write((sbyte)item.Value.Type);
					WriteString((string)item.Name.Value, item.Name.ByteCount, null);
					WriteTokenInternal(item.Value);
				}
				_writer.Write((byte)0);
				break;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Array:
			{
				global::Newtonsoft.Json.Bson.BsonArray bsonArray = (global::Newtonsoft.Json.Bson.BsonArray)t;
				_writer.Write(bsonArray.CalculatedSize);
				ulong num2 = 0uL;
				foreach (global::Newtonsoft.Json.Bson.BsonToken item2 in bsonArray)
				{
					_writer.Write((sbyte)item2.Type);
					WriteString(num2.ToString(global::System.Globalization.CultureInfo.InvariantCulture), global::Newtonsoft.Json.Utilities.MathUtils.IntLength(num2), null);
					WriteTokenInternal(item2);
					num2++;
				}
				_writer.Write((byte)0);
				break;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Integer:
			{
				global::Newtonsoft.Json.Bson.BsonValue bsonValue3 = (global::Newtonsoft.Json.Bson.BsonValue)t;
				_writer.Write(global::System.Convert.ToInt32(bsonValue3.Value, global::System.Globalization.CultureInfo.InvariantCulture));
				break;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Long:
			{
				global::Newtonsoft.Json.Bson.BsonValue bsonValue4 = (global::Newtonsoft.Json.Bson.BsonValue)t;
				_writer.Write(global::System.Convert.ToInt64(bsonValue4.Value, global::System.Globalization.CultureInfo.InvariantCulture));
				break;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Number:
			{
				global::Newtonsoft.Json.Bson.BsonValue bsonValue2 = (global::Newtonsoft.Json.Bson.BsonValue)t;
				_writer.Write(global::System.Convert.ToDouble(bsonValue2.Value, global::System.Globalization.CultureInfo.InvariantCulture));
				break;
			}
			case global::Newtonsoft.Json.Bson.BsonType.String:
			{
				global::Newtonsoft.Json.Bson.BsonString bsonString = (global::Newtonsoft.Json.Bson.BsonString)t;
				WriteString((string)bsonString.Value, bsonString.ByteCount, bsonString.CalculatedSize - 4);
				break;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Boolean:
				_writer.Write(t == global::Newtonsoft.Json.Bson.BsonBoolean.True);
				break;
			case global::Newtonsoft.Json.Bson.BsonType.Date:
			{
				global::Newtonsoft.Json.Bson.BsonValue bsonValue = (global::Newtonsoft.Json.Bson.BsonValue)t;
				long num = 0L;
				if (bsonValue.Value is global::System.DateTime dateTime)
				{
					if (DateTimeKindHandling == global::System.DateTimeKind.Utc)
					{
						dateTime = dateTime.ToUniversalTime();
					}
					else if (DateTimeKindHandling == global::System.DateTimeKind.Local)
					{
						dateTime = dateTime.ToLocalTime();
					}
					num = global::Newtonsoft.Json.Utilities.DateTimeUtils.ConvertDateTimeToJavaScriptTicks(dateTime, convertToUtc: false);
				}
				else
				{
					global::System.DateTimeOffset dateTimeOffset = (global::System.DateTimeOffset)bsonValue.Value;
					num = global::Newtonsoft.Json.Utilities.DateTimeUtils.ConvertDateTimeToJavaScriptTicks(dateTimeOffset.UtcDateTime, dateTimeOffset.Offset);
				}
				_writer.Write(num);
				break;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Binary:
			{
				global::Newtonsoft.Json.Bson.BsonBinary bsonBinary = (global::Newtonsoft.Json.Bson.BsonBinary)t;
				byte[] array = (byte[])bsonBinary.Value;
				_writer.Write(array.Length);
				_writer.Write((byte)bsonBinary.BinaryType);
				_writer.Write(array);
				break;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Oid:
			{
				byte[] buffer = (byte[])((global::Newtonsoft.Json.Bson.BsonValue)t).Value;
				_writer.Write(buffer);
				break;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Regex:
			{
				global::Newtonsoft.Json.Bson.BsonRegex bsonRegex = (global::Newtonsoft.Json.Bson.BsonRegex)t;
				WriteString((string)bsonRegex.Pattern.Value, bsonRegex.Pattern.ByteCount, null);
				WriteString((string)bsonRegex.Options.Value, bsonRegex.Options.ByteCount, null);
				break;
			}
			default:
				throw new global::System.ArgumentOutOfRangeException("t", global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token when writing BSON: {0}", global::System.Globalization.CultureInfo.InvariantCulture, t.Type));
			case global::Newtonsoft.Json.Bson.BsonType.Undefined:
			case global::Newtonsoft.Json.Bson.BsonType.Null:
				break;
			}
		}

		private void WriteString(string s, int byteCount, int? calculatedlengthPrefix)
		{
			if (calculatedlengthPrefix.HasValue)
			{
				_writer.Write(calculatedlengthPrefix.GetValueOrDefault());
			}
			WriteUtf8Bytes(s, byteCount);
			_writer.Write((byte)0);
		}

		public void WriteUtf8Bytes(string s, int byteCount)
		{
			if (s == null)
			{
				return;
			}
			if (byteCount <= 256)
			{
				if (_largeByteBuffer == null)
				{
					_largeByteBuffer = new byte[256];
				}
				Encoding.GetBytes(s, 0, s.Length, _largeByteBuffer, 0);
				_writer.Write(_largeByteBuffer, 0, byteCount);
			}
			else
			{
				byte[] bytes = Encoding.GetBytes(s);
				_writer.Write(bytes);
			}
		}

		private int CalculateSize(int stringByteCount)
		{
			return stringByteCount + 1;
		}

		private int CalculateSizeWithLength(int stringByteCount, bool includeSize)
		{
			return ((!includeSize) ? 1 : 5) + stringByteCount;
		}

		private int CalculateSize(global::Newtonsoft.Json.Bson.BsonToken t)
		{
			switch (t.Type)
			{
			case global::Newtonsoft.Json.Bson.BsonType.Object:
			{
				global::Newtonsoft.Json.Bson.BsonObject bsonObject = (global::Newtonsoft.Json.Bson.BsonObject)t;
				int num4 = 4;
				foreach (global::Newtonsoft.Json.Bson.BsonProperty item in bsonObject)
				{
					int num5 = 1;
					num5 += CalculateSize(item.Name);
					num5 += CalculateSize(item.Value);
					num4 += num5;
				}
				return bsonObject.CalculatedSize = num4 + 1;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Array:
			{
				global::Newtonsoft.Json.Bson.BsonArray bsonArray = (global::Newtonsoft.Json.Bson.BsonArray)t;
				int num2 = 4;
				ulong num3 = 0uL;
				foreach (global::Newtonsoft.Json.Bson.BsonToken item2 in bsonArray)
				{
					num2++;
					num2 += CalculateSize(global::Newtonsoft.Json.Utilities.MathUtils.IntLength(num3));
					num2 += CalculateSize(item2);
					num3++;
				}
				num2++;
				bsonArray.CalculatedSize = num2;
				return bsonArray.CalculatedSize;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Integer:
				return 4;
			case global::Newtonsoft.Json.Bson.BsonType.Long:
				return 8;
			case global::Newtonsoft.Json.Bson.BsonType.Number:
				return 8;
			case global::Newtonsoft.Json.Bson.BsonType.String:
			{
				global::Newtonsoft.Json.Bson.BsonString bsonString = (global::Newtonsoft.Json.Bson.BsonString)t;
				string text = (string)bsonString.Value;
				bsonString.ByteCount = ((text != null) ? Encoding.GetByteCount(text) : 0);
				bsonString.CalculatedSize = CalculateSizeWithLength(bsonString.ByteCount, bsonString.IncludeLength);
				return bsonString.CalculatedSize;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Boolean:
				return 1;
			case global::Newtonsoft.Json.Bson.BsonType.Undefined:
			case global::Newtonsoft.Json.Bson.BsonType.Null:
				return 0;
			case global::Newtonsoft.Json.Bson.BsonType.Date:
				return 8;
			case global::Newtonsoft.Json.Bson.BsonType.Binary:
			{
				global::Newtonsoft.Json.Bson.BsonBinary obj = (global::Newtonsoft.Json.Bson.BsonBinary)t;
				byte[] array = (byte[])obj.Value;
				obj.CalculatedSize = 5 + array.Length;
				return obj.CalculatedSize;
			}
			case global::Newtonsoft.Json.Bson.BsonType.Oid:
				return 12;
			case global::Newtonsoft.Json.Bson.BsonType.Regex:
			{
				global::Newtonsoft.Json.Bson.BsonRegex bsonRegex = (global::Newtonsoft.Json.Bson.BsonRegex)t;
				int num = 0;
				num += CalculateSize(bsonRegex.Pattern);
				num += CalculateSize(bsonRegex.Options);
				bsonRegex.CalculatedSize = num;
				return bsonRegex.CalculatedSize;
			}
			default:
				throw new global::System.ArgumentOutOfRangeException("t", global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token when writing BSON: {0}", global::System.Globalization.CultureInfo.InvariantCulture, t.Type));
			}
		}
	}
}
