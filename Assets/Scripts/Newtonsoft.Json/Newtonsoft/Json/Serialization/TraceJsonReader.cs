namespace Newtonsoft.Json.Serialization
{
	internal class TraceJsonReader : global::Newtonsoft.Json.JsonReader, global::Newtonsoft.Json.IJsonLineInfo
	{
		private readonly global::Newtonsoft.Json.JsonReader _innerReader;

		private readonly global::Newtonsoft.Json.JsonTextWriter _textWriter;

		private readonly global::System.IO.StringWriter _sw;

		public override int Depth => _innerReader.Depth;

		public override string Path => _innerReader.Path;

		public override char QuoteChar
		{
			get
			{
				return _innerReader.QuoteChar;
			}
			protected internal set
			{
				_innerReader.QuoteChar = value;
			}
		}

		public override global::Newtonsoft.Json.JsonToken TokenType => _innerReader.TokenType;

		public override object? Value => _innerReader.Value;

		public override global::System.Type? ValueType => _innerReader.ValueType;

		int global::Newtonsoft.Json.IJsonLineInfo.LineNumber
		{
			get
			{
				if (!(_innerReader is global::Newtonsoft.Json.IJsonLineInfo jsonLineInfo))
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		int global::Newtonsoft.Json.IJsonLineInfo.LinePosition
		{
			get
			{
				if (!(_innerReader is global::Newtonsoft.Json.IJsonLineInfo jsonLineInfo))
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		public TraceJsonReader(global::Newtonsoft.Json.JsonReader innerReader)
		{
			_innerReader = innerReader;
			_sw = new global::System.IO.StringWriter(global::System.Globalization.CultureInfo.InvariantCulture);
			_sw.Write("Deserialized JSON: " + global::System.Environment.NewLine);
			_textWriter = new global::Newtonsoft.Json.JsonTextWriter(_sw);
			_textWriter.Formatting = global::Newtonsoft.Json.Formatting.Indented;
		}

		public string GetDeserializedJsonMessage()
		{
			return _sw.ToString();
		}

		public override bool Read()
		{
			bool result = _innerReader.Read();
			WriteCurrentToken();
			return result;
		}

		public override int? ReadAsInt32()
		{
			int? result = _innerReader.ReadAsInt32();
			WriteCurrentToken();
			return result;
		}

		public override string? ReadAsString()
		{
			string? result = _innerReader.ReadAsString();
			WriteCurrentToken();
			return result;
		}

		public override byte[]? ReadAsBytes()
		{
			byte[]? result = _innerReader.ReadAsBytes();
			WriteCurrentToken();
			return result;
		}

		public override decimal? ReadAsDecimal()
		{
			decimal? result = _innerReader.ReadAsDecimal();
			WriteCurrentToken();
			return result;
		}

		public override double? ReadAsDouble()
		{
			double? result = _innerReader.ReadAsDouble();
			WriteCurrentToken();
			return result;
		}

		public override bool? ReadAsBoolean()
		{
			bool? result = _innerReader.ReadAsBoolean();
			WriteCurrentToken();
			return result;
		}

		public override global::System.DateTime? ReadAsDateTime()
		{
			global::System.DateTime? result = _innerReader.ReadAsDateTime();
			WriteCurrentToken();
			return result;
		}

		public override global::System.DateTimeOffset? ReadAsDateTimeOffset()
		{
			global::System.DateTimeOffset? result = _innerReader.ReadAsDateTimeOffset();
			WriteCurrentToken();
			return result;
		}

		public void WriteCurrentToken()
		{
			_textWriter.WriteToken(_innerReader, writeChildren: false, writeDateConstructorAsDate: false, writeComments: true);
		}

		public override void Close()
		{
			_innerReader.Close();
		}

		bool global::Newtonsoft.Json.IJsonLineInfo.HasLineInfo()
		{
			if (_innerReader is global::Newtonsoft.Json.IJsonLineInfo jsonLineInfo)
			{
				return jsonLineInfo.HasLineInfo();
			}
			return false;
		}
	}
}
