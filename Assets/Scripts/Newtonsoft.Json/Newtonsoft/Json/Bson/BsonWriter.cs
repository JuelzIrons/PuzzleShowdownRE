namespace Newtonsoft.Json.Bson
{
	[global::System.Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonWriter : global::Newtonsoft.Json.JsonWriter
	{
		private readonly global::Newtonsoft.Json.Bson.BsonBinaryWriter _writer;

		private global::Newtonsoft.Json.Bson.BsonToken _root;

		private global::Newtonsoft.Json.Bson.BsonToken _parent;

		private string _propertyName;

		public global::System.DateTimeKind DateTimeKindHandling
		{
			get
			{
				return _writer.DateTimeKindHandling;
			}
			set
			{
				_writer.DateTimeKindHandling = value;
			}
		}

		public BsonWriter(global::System.IO.Stream stream)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(stream, "stream");
			_writer = new global::Newtonsoft.Json.Bson.BsonBinaryWriter(new global::System.IO.BinaryWriter(stream));
		}

		public BsonWriter(global::System.IO.BinaryWriter writer)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(writer, "writer");
			_writer = new global::Newtonsoft.Json.Bson.BsonBinaryWriter(writer);
		}

		public override void Flush()
		{
			_writer.Flush();
		}

		protected override void WriteEnd(global::Newtonsoft.Json.JsonToken token)
		{
			base.WriteEnd(token);
			RemoveParent();
			if (base.Top == 0)
			{
				_writer.WriteToken(_root);
			}
		}

		public override void WriteComment(string text)
		{
			throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Cannot write JSON comment as BSON.", null);
		}

		public override void WriteStartConstructor(string name)
		{
			throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Cannot write JSON constructor as BSON.", null);
		}

		public override void WriteRaw(string json)
		{
			throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		public override void WriteRawValue(string json)
		{
			throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		public override void WriteStartArray()
		{
			base.WriteStartArray();
			AddParent(new global::Newtonsoft.Json.Bson.BsonArray());
		}

		public override void WriteStartObject()
		{
			base.WriteStartObject();
			AddParent(new global::Newtonsoft.Json.Bson.BsonObject());
		}

		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			_propertyName = name;
		}

		public override void Close()
		{
			base.Close();
			if (base.CloseOutput)
			{
				_writer?.Close();
			}
		}

		private void AddParent(global::Newtonsoft.Json.Bson.BsonToken container)
		{
			AddToken(container);
			_parent = container;
		}

		private void RemoveParent()
		{
			_parent = _parent.Parent;
		}

		private void AddValue(object value, global::Newtonsoft.Json.Bson.BsonType type)
		{
			AddToken(new global::Newtonsoft.Json.Bson.BsonValue(value, type));
		}

		internal void AddToken(global::Newtonsoft.Json.Bson.BsonToken token)
		{
			if (_parent != null)
			{
				if (_parent is global::Newtonsoft.Json.Bson.BsonObject bsonObject)
				{
					bsonObject.Add(_propertyName, token);
					_propertyName = null;
				}
				else
				{
					((global::Newtonsoft.Json.Bson.BsonArray)_parent).Add(token);
				}
				return;
			}
			if (token.Type != global::Newtonsoft.Json.Bson.BsonType.Object && token.Type != global::Newtonsoft.Json.Bson.BsonType.Array)
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error writing {0} value. BSON must start with an Object or Array.", global::System.Globalization.CultureInfo.InvariantCulture, token.Type), null);
			}
			_parent = token;
			_root = token;
		}

		public override void WriteValue(object value)
		{
			if (value is global::System.Numerics.BigInteger bigInteger)
			{
				SetWriteState(global::Newtonsoft.Json.JsonToken.Integer, null);
				AddToken(new global::Newtonsoft.Json.Bson.BsonBinary(bigInteger.ToByteArray(), global::Newtonsoft.Json.Bson.BsonBinaryType.Binary));
			}
			else
			{
				base.WriteValue(value);
			}
		}

		public override void WriteNull()
		{
			base.WriteNull();
			AddToken(global::Newtonsoft.Json.Bson.BsonEmpty.Null);
		}

		public override void WriteUndefined()
		{
			base.WriteUndefined();
			AddToken(global::Newtonsoft.Json.Bson.BsonEmpty.Undefined);
		}

		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			AddToken((value == null) ? global::Newtonsoft.Json.Bson.BsonEmpty.Null : new global::Newtonsoft.Json.Bson.BsonString(value, includeLength: true));
		}

		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Integer);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			if (value > int.MaxValue)
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Value is too large to fit in a signed 32 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Integer);
		}

		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Long);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			if (value > long.MaxValue)
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Value is too large to fit in a signed 64 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Long);
		}

		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Number);
		}

		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Number);
		}

		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			AddToken(value ? global::Newtonsoft.Json.Bson.BsonBoolean.True : global::Newtonsoft.Json.Bson.BsonBoolean.False);
		}

		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Integer);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Integer);
		}

		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = null;
			text = value.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
			AddToken(new global::Newtonsoft.Json.Bson.BsonString(text, includeLength: true));
		}

		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Integer);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Integer);
		}

		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Number);
		}

		public override void WriteValue(global::System.DateTime value)
		{
			base.WriteValue(value);
			value = global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Date);
		}

		public override void WriteValue(global::System.DateTimeOffset value)
		{
			base.WriteValue(value);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Date);
		}

		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				WriteNull();
				return;
			}
			base.WriteValue(value);
			AddToken(new global::Newtonsoft.Json.Bson.BsonBinary(value, global::Newtonsoft.Json.Bson.BsonBinaryType.Binary));
		}

		public override void WriteValue(global::System.Guid value)
		{
			base.WriteValue(value);
			AddToken(new global::Newtonsoft.Json.Bson.BsonBinary(value.ToByteArray(), global::Newtonsoft.Json.Bson.BsonBinaryType.Uuid));
		}

		public override void WriteValue(global::System.TimeSpan value)
		{
			base.WriteValue(value);
			AddToken(new global::Newtonsoft.Json.Bson.BsonString(value.ToString(), includeLength: true));
		}

		public override void WriteValue(global::System.Uri value)
		{
			if (value == null)
			{
				WriteNull();
				return;
			}
			base.WriteValue(value);
			AddToken(new global::Newtonsoft.Json.Bson.BsonString(value.ToString(), includeLength: true));
		}

		public void WriteObjectId(byte[] value)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "An object id must be 12 bytes", null);
			}
			SetWriteState(global::Newtonsoft.Json.JsonToken.Undefined, null);
			AddValue(value, global::Newtonsoft.Json.Bson.BsonType.Oid);
		}

		public void WriteRegex(string pattern, string options)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(pattern, "pattern");
			SetWriteState(global::Newtonsoft.Json.JsonToken.Undefined, null);
			AddToken(new global::Newtonsoft.Json.Bson.BsonRegex(pattern, options));
		}
	}
}
