namespace Newtonsoft.Json.Linq
{
	public class JTokenWriter : global::Newtonsoft.Json.JsonWriter
	{
		private global::Newtonsoft.Json.Linq.JContainer? _token;

		private global::Newtonsoft.Json.Linq.JContainer? _parent;

		private global::Newtonsoft.Json.Linq.JValue? _value;

		private global::Newtonsoft.Json.Linq.JToken? _current;

		public global::Newtonsoft.Json.Linq.JToken? CurrentToken => _current;

		public global::Newtonsoft.Json.Linq.JToken? Token
		{
			get
			{
				if (_token != null)
				{
					return _token;
				}
				return _value;
			}
		}

		internal override global::System.Threading.Tasks.Task WriteTokenAsync(global::Newtonsoft.Json.JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, global::System.Threading.CancellationToken cancellationToken)
		{
			if (reader is global::Newtonsoft.Json.Linq.JTokenReader)
			{
				WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
			}
			return WriteTokenSyncReadingAsync(reader, cancellationToken);
		}

		public JTokenWriter(global::Newtonsoft.Json.Linq.JContainer container)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(container, "container");
			_token = container;
			_parent = container;
		}

		public JTokenWriter()
		{
		}

		public override void Flush()
		{
		}

		public override void Close()
		{
			base.Close();
		}

		public override void WriteStartObject()
		{
			base.WriteStartObject();
			AddParent(new global::Newtonsoft.Json.Linq.JObject());
		}

		private void AddParent(global::Newtonsoft.Json.Linq.JContainer container)
		{
			if (_parent == null)
			{
				_token = container;
			}
			else
			{
				_parent.AddAndSkipParentCheck(container);
			}
			_parent = container;
			_current = container;
		}

		private void RemoveParent()
		{
			_current = _parent;
			_parent = _parent.Parent;
			if (_parent != null && _parent.Type == global::Newtonsoft.Json.Linq.JTokenType.Property)
			{
				_parent = _parent.Parent;
			}
		}

		public override void WriteStartArray()
		{
			base.WriteStartArray();
			AddParent(new global::Newtonsoft.Json.Linq.JArray());
		}

		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			AddParent(new global::Newtonsoft.Json.Linq.JConstructor(name));
		}

		protected override void WriteEnd(global::Newtonsoft.Json.JsonToken token)
		{
			RemoveParent();
		}

		public override void WritePropertyName(string name)
		{
			(_parent as global::Newtonsoft.Json.Linq.JObject)?.Remove(name);
			AddParent(new global::Newtonsoft.Json.Linq.JProperty(name));
			base.WritePropertyName(name);
		}

		private void AddRawValue(object? value, global::Newtonsoft.Json.Linq.JTokenType type, global::Newtonsoft.Json.JsonToken token)
		{
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value, type), token);
		}

		internal void AddJValue(global::Newtonsoft.Json.Linq.JValue? value, global::Newtonsoft.Json.JsonToken token)
		{
			if (_parent != null)
			{
				if (_parent.TryAdd(value))
				{
					_current = _parent.Last;
					if (_parent.Type == global::Newtonsoft.Json.Linq.JTokenType.Property)
					{
						_parent = _parent.Parent;
					}
				}
			}
			else
			{
				_value = value ?? global::Newtonsoft.Json.Linq.JValue.CreateNull();
				_current = _value;
			}
		}

		public override void WriteValue(object? value)
		{
			if (value is global::System.Numerics.BigInteger)
			{
				InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
				AddRawValue(value, global::Newtonsoft.Json.Linq.JTokenType.Integer, global::Newtonsoft.Json.JsonToken.Integer);
			}
			else
			{
				base.WriteValue(value);
			}
		}

		public override void WriteNull()
		{
			base.WriteNull();
			AddJValue(global::Newtonsoft.Json.Linq.JValue.CreateNull(), global::Newtonsoft.Json.JsonToken.Null);
		}

		public override void WriteUndefined()
		{
			base.WriteUndefined();
			AddJValue(global::Newtonsoft.Json.Linq.JValue.CreateUndefined(), global::Newtonsoft.Json.JsonToken.Undefined);
		}

		public override void WriteRaw(string? json)
		{
			base.WriteRaw(json);
			AddJValue(new global::Newtonsoft.Json.Linq.JRaw(json), global::Newtonsoft.Json.JsonToken.Raw);
		}

		public override void WriteComment(string? text)
		{
			base.WriteComment(text);
			AddJValue(global::Newtonsoft.Json.Linq.JValue.CreateComment(text), global::Newtonsoft.Json.JsonToken.Comment);
		}

		public override void WriteValue(string? value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.String);
		}

		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			AddRawValue(value, global::Newtonsoft.Json.Linq.JTokenType.Integer, global::Newtonsoft.Json.JsonToken.Integer);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			AddRawValue(value, global::Newtonsoft.Json.Linq.JTokenType.Integer, global::Newtonsoft.Json.JsonToken.Integer);
		}

		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.Integer);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.Integer);
		}

		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.Float);
		}

		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.Float);
		}

		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.Boolean);
		}

		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			AddRawValue(value, global::Newtonsoft.Json.Linq.JTokenType.Integer, global::Newtonsoft.Json.JsonToken.Integer);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			AddRawValue(value, global::Newtonsoft.Json.Linq.JTokenType.Integer, global::Newtonsoft.Json.JsonToken.Integer);
		}

		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value2), global::Newtonsoft.Json.JsonToken.String);
		}

		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			AddRawValue(value, global::Newtonsoft.Json.Linq.JTokenType.Integer, global::Newtonsoft.Json.JsonToken.Integer);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			AddRawValue(value, global::Newtonsoft.Json.Linq.JTokenType.Integer, global::Newtonsoft.Json.JsonToken.Integer);
		}

		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.Float);
		}

		public override void WriteValue(global::System.DateTime value)
		{
			base.WriteValue(value);
			value = global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.Date);
		}

		public override void WriteValue(global::System.DateTimeOffset value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.Date);
		}

		public override void WriteValue(byte[]? value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value, global::Newtonsoft.Json.Linq.JTokenType.Bytes), global::Newtonsoft.Json.JsonToken.Bytes);
		}

		public override void WriteValue(global::System.TimeSpan value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.String);
		}

		public override void WriteValue(global::System.Guid value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.String);
		}

		public override void WriteValue(global::System.Uri? value)
		{
			base.WriteValue(value);
			AddJValue(new global::Newtonsoft.Json.Linq.JValue(value), global::Newtonsoft.Json.JsonToken.String);
		}

		internal override void WriteToken(global::Newtonsoft.Json.JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			global::Newtonsoft.Json.Linq.JTokenReader jTokenReader = reader as global::Newtonsoft.Json.Linq.JTokenReader;
			if (jTokenReader != null && writeChildren && writeDateConstructorAsDate && writeComments)
			{
				if (jTokenReader.TokenType == global::Newtonsoft.Json.JsonToken.None && !jTokenReader.Read())
				{
					return;
				}
				global::Newtonsoft.Json.Linq.JToken jToken = jTokenReader.CurrentToken.CloneToken(null);
				if (_parent != null)
				{
					_parent.Add(jToken);
					_current = _parent.Last;
					if (_parent.Type == global::Newtonsoft.Json.Linq.JTokenType.Property)
					{
						_parent = _parent.Parent;
						InternalWriteValue(global::Newtonsoft.Json.JsonToken.Null);
					}
				}
				else
				{
					_current = jToken;
					if (_token == null && _value == null)
					{
						_token = jToken as global::Newtonsoft.Json.Linq.JContainer;
						_value = jToken as global::Newtonsoft.Json.Linq.JValue;
					}
				}
				jTokenReader.Skip();
			}
			else
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
			}
		}
	}
}
