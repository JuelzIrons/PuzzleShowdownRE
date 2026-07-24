namespace Newtonsoft.Json
{
	public abstract class JsonReader : global::System.IDisposable
	{
		protected internal enum State
		{
			Start = 0,
			Complete = 1,
			Property = 2,
			ObjectStart = 3,
			Object = 4,
			ArrayStart = 5,
			Array = 6,
			Closed = 7,
			PostValue = 8,
			ConstructorStart = 9,
			Constructor = 10,
			Error = 11,
			Finished = 12
		}

		private global::Newtonsoft.Json.JsonToken _tokenType;

		private object? _value;

		internal char _quoteChar;

		internal global::Newtonsoft.Json.JsonReader.State _currentState;

		private global::Newtonsoft.Json.JsonPosition _currentPosition;

		private global::System.Globalization.CultureInfo? _culture;

		private global::Newtonsoft.Json.DateTimeZoneHandling _dateTimeZoneHandling;

		private int? _maxDepth;

		private bool _hasExceededMaxDepth;

		internal global::Newtonsoft.Json.DateParseHandling _dateParseHandling;

		internal global::Newtonsoft.Json.FloatParseHandling _floatParseHandling;

		private string? _dateFormatString;

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.JsonPosition>? _stack;

		protected global::Newtonsoft.Json.JsonReader.State CurrentState => _currentState;

		public bool CloseInput { get; set; }

		public bool SupportMultipleContent { get; set; }

		public virtual char QuoteChar
		{
			get
			{
				return _quoteChar;
			}
			protected internal set
			{
				_quoteChar = value;
			}
		}

		public global::Newtonsoft.Json.DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return _dateTimeZoneHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.DateTimeZoneHandling.Local || value > global::Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_dateTimeZoneHandling = value;
			}
		}

		public global::Newtonsoft.Json.DateParseHandling DateParseHandling
		{
			get
			{
				return _dateParseHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.DateParseHandling.None || value > global::Newtonsoft.Json.DateParseHandling.DateTimeOffset)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_dateParseHandling = value;
			}
		}

		public global::Newtonsoft.Json.FloatParseHandling FloatParseHandling
		{
			get
			{
				return _floatParseHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.FloatParseHandling.Double || value > global::Newtonsoft.Json.FloatParseHandling.Decimal)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_floatParseHandling = value;
			}
		}

		public string? DateFormatString
		{
			get
			{
				return _dateFormatString;
			}
			set
			{
				_dateFormatString = value;
			}
		}

		public int? MaxDepth
		{
			get
			{
				return _maxDepth;
			}
			set
			{
				if (value <= 0)
				{
					throw new global::System.ArgumentException("Value must be positive.", "value");
				}
				_maxDepth = value;
			}
		}

		public virtual global::Newtonsoft.Json.JsonToken TokenType => _tokenType;

		public virtual object? Value => _value;

		public virtual global::System.Type? ValueType => _value?.GetType();

		public virtual int Depth
		{
			get
			{
				int num = _stack?.Count ?? 0;
				if (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsStartToken(TokenType) || _currentPosition.Type == global::Newtonsoft.Json.JsonContainerType.None)
				{
					return num;
				}
				return num + 1;
			}
		}

		public virtual string Path
		{
			get
			{
				if (_currentPosition.Type == global::Newtonsoft.Json.JsonContainerType.None)
				{
					return string.Empty;
				}
				global::Newtonsoft.Json.JsonPosition? currentPosition = ((_currentState != global::Newtonsoft.Json.JsonReader.State.ArrayStart && _currentState != global::Newtonsoft.Json.JsonReader.State.ConstructorStart && _currentState != global::Newtonsoft.Json.JsonReader.State.ObjectStart) ? new global::Newtonsoft.Json.JsonPosition?(_currentPosition) : ((global::Newtonsoft.Json.JsonPosition?)null));
				return global::Newtonsoft.Json.JsonPosition.BuildPath(_stack, currentPosition);
			}
		}

		public global::System.Globalization.CultureInfo Culture
		{
			get
			{
				return _culture ?? global::System.Globalization.CultureInfo.InvariantCulture;
			}
			set
			{
				_culture = value;
			}
		}

		public virtual global::System.Threading.Tasks.Task<bool> ReadAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CancelIfRequestedAsync<bool>(cancellationToken) ?? global::Newtonsoft.Json.Utilities.AsyncUtils.ToAsync(Read());
		}

		public async global::System.Threading.Tasks.Task SkipAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				await ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			if (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsStartToken(TokenType))
			{
				int depth = Depth;
				while (await ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false) && depth < Depth)
				{
				}
			}
		}

		internal async global::System.Threading.Tasks.Task ReaderReadAndAssertAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (!(await ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw CreateUnexpectedEndException();
			}
		}

		public virtual global::System.Threading.Tasks.Task<bool?> ReadAsBooleanAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CancelIfRequestedAsync<bool?>(cancellationToken) ?? global::System.Threading.Tasks.Task.FromResult(ReadAsBoolean());
		}

		public virtual global::System.Threading.Tasks.Task<byte[]?> ReadAsBytesAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CancelIfRequestedAsync<byte[]>(cancellationToken) ?? global::System.Threading.Tasks.Task.FromResult(ReadAsBytes());
		}

		internal async global::System.Threading.Tasks.Task<byte[]?> ReadArrayIntoByteArrayAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Collections.Generic.List<byte> buffer = new global::System.Collections.Generic.List<byte>();
			do
			{
				if (!(await ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
				{
					SetToken(global::Newtonsoft.Json.JsonToken.None);
				}
			}
			while (!ReadArrayElementIntoByteArrayReportDone(buffer));
			byte[] array = buffer.ToArray();
			SetToken(global::Newtonsoft.Json.JsonToken.Bytes, array, updateIndex: false);
			return array;
		}

		public virtual global::System.Threading.Tasks.Task<global::System.DateTime?> ReadAsDateTimeAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CancelIfRequestedAsync<global::System.DateTime?>(cancellationToken) ?? global::System.Threading.Tasks.Task.FromResult(ReadAsDateTime());
		}

		public virtual global::System.Threading.Tasks.Task<global::System.DateTimeOffset?> ReadAsDateTimeOffsetAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CancelIfRequestedAsync<global::System.DateTimeOffset?>(cancellationToken) ?? global::System.Threading.Tasks.Task.FromResult(ReadAsDateTimeOffset());
		}

		public virtual global::System.Threading.Tasks.Task<decimal?> ReadAsDecimalAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CancelIfRequestedAsync<decimal?>(cancellationToken) ?? global::System.Threading.Tasks.Task.FromResult(ReadAsDecimal());
		}

		public virtual global::System.Threading.Tasks.Task<double?> ReadAsDoubleAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return global::System.Threading.Tasks.Task.FromResult(ReadAsDouble());
		}

		public virtual global::System.Threading.Tasks.Task<int?> ReadAsInt32Async(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CancelIfRequestedAsync<int?>(cancellationToken) ?? global::System.Threading.Tasks.Task.FromResult(ReadAsInt32());
		}

		public virtual global::System.Threading.Tasks.Task<string?> ReadAsStringAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CancelIfRequestedAsync<string>(cancellationToken) ?? global::System.Threading.Tasks.Task.FromResult(ReadAsString());
		}

		internal async global::System.Threading.Tasks.Task<bool> ReadAndMoveToContentAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			bool flag = await ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (flag)
			{
				flag = await MoveToContentAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			return flag;
		}

		internal global::System.Threading.Tasks.Task<bool> MoveToContentAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			global::Newtonsoft.Json.JsonToken tokenType = TokenType;
			if (tokenType == global::Newtonsoft.Json.JsonToken.None || tokenType == global::Newtonsoft.Json.JsonToken.Comment)
			{
				return MoveToContentFromNonContentAsync(cancellationToken);
			}
			return global::Newtonsoft.Json.Utilities.AsyncUtils.True;
		}

		private async global::System.Threading.Tasks.Task<bool> MoveToContentFromNonContentAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			global::Newtonsoft.Json.JsonToken tokenType;
			do
			{
				if (!(await ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
				{
					return false;
				}
				tokenType = TokenType;
			}
			while (tokenType == global::Newtonsoft.Json.JsonToken.None || tokenType == global::Newtonsoft.Json.JsonToken.Comment);
			return true;
		}

		internal global::Newtonsoft.Json.JsonPosition GetPosition(int depth)
		{
			if (_stack != null && depth < _stack.Count)
			{
				return _stack[depth];
			}
			return _currentPosition;
		}

		protected JsonReader()
		{
			_currentState = global::Newtonsoft.Json.JsonReader.State.Start;
			_dateTimeZoneHandling = global::Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind;
			_dateParseHandling = global::Newtonsoft.Json.DateParseHandling.DateTime;
			_floatParseHandling = global::Newtonsoft.Json.FloatParseHandling.Double;
			_maxDepth = 64;
			CloseInput = true;
		}

		private void Push(global::Newtonsoft.Json.JsonContainerType value)
		{
			UpdateScopeWithFinishedValue();
			if (_currentPosition.Type == global::Newtonsoft.Json.JsonContainerType.None)
			{
				_currentPosition = new global::Newtonsoft.Json.JsonPosition(value);
				return;
			}
			if (_stack == null)
			{
				_stack = new global::System.Collections.Generic.List<global::Newtonsoft.Json.JsonPosition>();
			}
			_stack.Add(_currentPosition);
			_currentPosition = new global::Newtonsoft.Json.JsonPosition(value);
			if (!_maxDepth.HasValue || !(Depth + 1 > _maxDepth) || _hasExceededMaxDepth)
			{
				return;
			}
			_hasExceededMaxDepth = true;
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("The reader's MaxDepth of {0} has been exceeded.", global::System.Globalization.CultureInfo.InvariantCulture, _maxDepth));
		}

		private global::Newtonsoft.Json.JsonContainerType Pop()
		{
			global::Newtonsoft.Json.JsonPosition currentPosition;
			if (_stack != null && _stack.Count > 0)
			{
				currentPosition = _currentPosition;
				_currentPosition = _stack[_stack.Count - 1];
				_stack.RemoveAt(_stack.Count - 1);
			}
			else
			{
				currentPosition = _currentPosition;
				_currentPosition = default(global::Newtonsoft.Json.JsonPosition);
			}
			if (_maxDepth.HasValue && Depth <= _maxDepth)
			{
				_hasExceededMaxDepth = false;
			}
			return currentPosition.Type;
		}

		private global::Newtonsoft.Json.JsonContainerType Peek()
		{
			return _currentPosition.Type;
		}

		public abstract bool Read();

		public virtual int? ReadAsInt32()
		{
			global::Newtonsoft.Json.JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case global::Newtonsoft.Json.JsonToken.None:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return null;
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			{
				object value = Value;
				if (value is int)
				{
					return (int)value;
				}
				int num;
				if (value is global::System.Numerics.BigInteger bigInteger)
				{
					num = (int)bigInteger;
				}
				else
				{
					try
					{
						num = global::System.Convert.ToInt32(value, global::System.Globalization.CultureInfo.InvariantCulture);
					}
					catch (global::System.Exception ex)
					{
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert to integer: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, value), ex);
					}
				}
				SetToken(global::Newtonsoft.Json.JsonToken.Integer, num, updateIndex: false);
				return num;
			}
			case global::Newtonsoft.Json.JsonToken.String:
			{
				string s = (string)Value;
				return ReadInt32String(s);
			}
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading integer. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal int? ReadInt32String(string? s)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Null, null, updateIndex: false);
				return null;
			}
			if (int.TryParse(s, global::System.Globalization.NumberStyles.Integer, Culture, out var result))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Integer, result, updateIndex: false);
				return result;
			}
			SetToken(global::Newtonsoft.Json.JsonToken.String, s, updateIndex: false);
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert string to integer: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, s));
		}

		public virtual string? ReadAsString()
		{
			global::Newtonsoft.Json.JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case global::Newtonsoft.Json.JsonToken.None:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return null;
			case global::Newtonsoft.Json.JsonToken.String:
				return (string)Value;
			default:
				if (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsPrimitiveToken(contentToken))
				{
					object value = Value;
					if (value != null)
					{
						string text = ((!(value is global::System.IFormattable formattable)) ? ((value is global::System.Uri uri) ? uri.OriginalString : value.ToString()) : formattable.ToString(null, Culture));
						SetToken(global::Newtonsoft.Json.JsonToken.String, text, updateIndex: false);
						return text;
					}
				}
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading string. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contentToken));
			}
		}

		public virtual byte[]? ReadAsBytes()
		{
			global::Newtonsoft.Json.JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case global::Newtonsoft.Json.JsonToken.StartObject:
			{
				ReadIntoWrappedTypeObject();
				byte[] array2 = ReadAsBytes();
				ReaderReadAndAssert();
				if (TokenType != global::Newtonsoft.Json.JsonToken.EndObject)
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading bytes. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, TokenType));
				}
				SetToken(global::Newtonsoft.Json.JsonToken.Bytes, array2, updateIndex: false);
				return array2;
			}
			case global::Newtonsoft.Json.JsonToken.String:
			{
				string text = (string)Value;
				global::System.Guid g;
				byte[] array3 = ((text.Length == 0) ? global::Newtonsoft.Json.Utilities.CollectionUtils.ArrayEmpty<byte>() : ((!global::Newtonsoft.Json.Utilities.ConvertUtils.TryConvertGuid(text, out g)) ? global::System.Convert.FromBase64String(text) : g.ToByteArray()));
				SetToken(global::Newtonsoft.Json.JsonToken.Bytes, array3, updateIndex: false);
				return array3;
			}
			case global::Newtonsoft.Json.JsonToken.None:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return null;
			case global::Newtonsoft.Json.JsonToken.Bytes:
				if (Value is global::System.Guid guid)
				{
					byte[] array = guid.ToByteArray();
					SetToken(global::Newtonsoft.Json.JsonToken.Bytes, array, updateIndex: false);
					return array;
				}
				return (byte[])Value;
			case global::Newtonsoft.Json.JsonToken.StartArray:
				return ReadArrayIntoByteArray();
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading bytes. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal byte[] ReadArrayIntoByteArray()
		{
			global::System.Collections.Generic.List<byte> list = new global::System.Collections.Generic.List<byte>();
			do
			{
				if (!Read())
				{
					SetToken(global::Newtonsoft.Json.JsonToken.None);
				}
			}
			while (!ReadArrayElementIntoByteArrayReportDone(list));
			byte[] array = list.ToArray();
			SetToken(global::Newtonsoft.Json.JsonToken.Bytes, array, updateIndex: false);
			return array;
		}

		private bool ReadArrayElementIntoByteArrayReportDone(global::System.Collections.Generic.List<byte> buffer)
		{
			switch (TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.None:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end when reading bytes.");
			case global::Newtonsoft.Json.JsonToken.Integer:
				buffer.Add(global::System.Convert.ToByte(Value, global::System.Globalization.CultureInfo.InvariantCulture));
				return false;
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return true;
			case global::Newtonsoft.Json.JsonToken.Comment:
				return false;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token when reading bytes: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, TokenType));
			}
		}

		public virtual double? ReadAsDouble()
		{
			global::Newtonsoft.Json.JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case global::Newtonsoft.Json.JsonToken.None:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return null;
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			{
				object value = Value;
				if (value is double)
				{
					return (double)value;
				}
				double num = ((!(value is global::System.Numerics.BigInteger bigInteger)) ? global::System.Convert.ToDouble(value, global::System.Globalization.CultureInfo.InvariantCulture) : ((double)bigInteger));
				SetToken(global::Newtonsoft.Json.JsonToken.Float, num, updateIndex: false);
				return num;
			}
			case global::Newtonsoft.Json.JsonToken.String:
				return ReadDoubleString((string)Value);
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading double. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal double? ReadDoubleString(string? s)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Null, null, updateIndex: false);
				return null;
			}
			if (double.TryParse(s, global::System.Globalization.NumberStyles.Float | global::System.Globalization.NumberStyles.AllowThousands, Culture, out var result))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Float, result, updateIndex: false);
				return result;
			}
			SetToken(global::Newtonsoft.Json.JsonToken.String, s, updateIndex: false);
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert string to double: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, s));
		}

		public virtual bool? ReadAsBoolean()
		{
			global::Newtonsoft.Json.JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case global::Newtonsoft.Json.JsonToken.None:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return null;
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			{
				bool flag = ((!(Value is global::System.Numerics.BigInteger bigInteger)) ? global::System.Convert.ToBoolean(Value, global::System.Globalization.CultureInfo.InvariantCulture) : (bigInteger != 0L));
				SetToken(global::Newtonsoft.Json.JsonToken.Boolean, flag, updateIndex: false);
				return flag;
			}
			case global::Newtonsoft.Json.JsonToken.String:
				return ReadBooleanString((string)Value);
			case global::Newtonsoft.Json.JsonToken.Boolean:
				return (bool)Value;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading boolean. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal bool? ReadBooleanString(string? s)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Null, null, updateIndex: false);
				return null;
			}
			if (bool.TryParse(s, out var result))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Boolean, result, updateIndex: false);
				return result;
			}
			SetToken(global::Newtonsoft.Json.JsonToken.String, s, updateIndex: false);
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert string to boolean: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, s));
		}

		public virtual decimal? ReadAsDecimal()
		{
			global::Newtonsoft.Json.JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case global::Newtonsoft.Json.JsonToken.None:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return null;
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			{
				object value = Value;
				if (value is decimal)
				{
					return (decimal)value;
				}
				decimal num;
				if (value is global::System.Numerics.BigInteger bigInteger)
				{
					num = (decimal)bigInteger;
				}
				else
				{
					try
					{
						num = global::System.Convert.ToDecimal(value, global::System.Globalization.CultureInfo.InvariantCulture);
					}
					catch (global::System.Exception ex)
					{
						throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert to decimal: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, value), ex);
					}
				}
				SetToken(global::Newtonsoft.Json.JsonToken.Float, num, updateIndex: false);
				return num;
			}
			case global::Newtonsoft.Json.JsonToken.String:
				return ReadDecimalString((string)Value);
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading decimal. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal decimal? ReadDecimalString(string? s)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Null, null, updateIndex: false);
				return null;
			}
			if (decimal.TryParse(s, global::System.Globalization.NumberStyles.Number, Culture, out var result))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Float, result, updateIndex: false);
				return result;
			}
			if (global::Newtonsoft.Json.Utilities.ConvertUtils.DecimalTryParse(s.ToCharArray(), 0, s.Length, out result) == global::Newtonsoft.Json.Utilities.ParseResult.Success)
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Float, result, updateIndex: false);
				return result;
			}
			SetToken(global::Newtonsoft.Json.JsonToken.String, s, updateIndex: false);
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert string to decimal: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, s));
		}

		public virtual global::System.DateTime? ReadAsDateTime()
		{
			switch (GetContentToken())
			{
			case global::Newtonsoft.Json.JsonToken.None:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return null;
			case global::Newtonsoft.Json.JsonToken.Date:
				if (Value is global::System.DateTimeOffset dateTimeOffset)
				{
					SetToken(global::Newtonsoft.Json.JsonToken.Date, dateTimeOffset.DateTime, updateIndex: false);
				}
				return (global::System.DateTime)Value;
			case global::Newtonsoft.Json.JsonToken.String:
				return ReadDateTimeString((string)Value);
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading date. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, TokenType));
			}
		}

		internal global::System.DateTime? ReadDateTimeString(string? s)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Null, null, updateIndex: false);
				return null;
			}
			if (global::Newtonsoft.Json.Utilities.DateTimeUtils.TryParseDateTime(s, DateTimeZoneHandling, _dateFormatString, Culture, out var dt))
			{
				dt = global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime(dt, DateTimeZoneHandling);
				SetToken(global::Newtonsoft.Json.JsonToken.Date, dt, updateIndex: false);
				return dt;
			}
			if (global::System.DateTime.TryParse(s, Culture, global::System.Globalization.DateTimeStyles.RoundtripKind, out dt))
			{
				dt = global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime(dt, DateTimeZoneHandling);
				SetToken(global::Newtonsoft.Json.JsonToken.Date, dt, updateIndex: false);
				return dt;
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert string to DateTime: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, s));
		}

		public virtual global::System.DateTimeOffset? ReadAsDateTimeOffset()
		{
			global::Newtonsoft.Json.JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case global::Newtonsoft.Json.JsonToken.None:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return null;
			case global::Newtonsoft.Json.JsonToken.Date:
				if (Value is global::System.DateTime dateTime)
				{
					SetToken(global::Newtonsoft.Json.JsonToken.Date, new global::System.DateTimeOffset(dateTime), updateIndex: false);
				}
				return (global::System.DateTimeOffset)Value;
			case global::Newtonsoft.Json.JsonToken.String:
			{
				string s = (string)Value;
				return ReadDateTimeOffsetString(s);
			}
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading date. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal global::System.DateTimeOffset? ReadDateTimeOffsetString(string? s)
		{
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(s))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Null, null, updateIndex: false);
				return null;
			}
			if (global::Newtonsoft.Json.Utilities.DateTimeUtils.TryParseDateTimeOffset(s, _dateFormatString, Culture, out var dt))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Date, dt, updateIndex: false);
				return dt;
			}
			if (global::System.DateTimeOffset.TryParse(s, Culture, global::System.Globalization.DateTimeStyles.RoundtripKind, out dt))
			{
				SetToken(global::Newtonsoft.Json.JsonToken.Date, dt, updateIndex: false);
				return dt;
			}
			SetToken(global::Newtonsoft.Json.JsonToken.String, s, updateIndex: false);
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert string to DateTimeOffset: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, s));
		}

		internal void ReaderReadAndAssert()
		{
			if (!Read())
			{
				throw CreateUnexpectedEndException();
			}
		}

		internal global::Newtonsoft.Json.JsonReaderException CreateUnexpectedEndException()
		{
			return global::Newtonsoft.Json.JsonReaderException.Create(this, "Unexpected end when reading JSON.");
		}

		internal void ReadIntoWrappedTypeObject()
		{
			ReaderReadAndAssert();
			if (Value != null && Value.ToString() == "$type")
			{
				ReaderReadAndAssert();
				if (Value != null && Value.ToString().StartsWith("System.Byte[]", global::System.StringComparison.Ordinal))
				{
					ReaderReadAndAssert();
					if (Value.ToString() == "$value")
					{
						return;
					}
				}
			}
			throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading bytes. Unexpected token: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.JsonToken.StartObject));
		}

		public void Skip()
		{
			if (TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				Read();
			}
			if (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsStartToken(TokenType))
			{
				int depth = Depth;
				while (Read() && depth < Depth)
				{
				}
			}
		}

		protected void SetToken(global::Newtonsoft.Json.JsonToken newToken)
		{
			SetToken(newToken, null, updateIndex: true);
		}

		protected void SetToken(global::Newtonsoft.Json.JsonToken newToken, object? value)
		{
			SetToken(newToken, value, updateIndex: true);
		}

		protected void SetToken(global::Newtonsoft.Json.JsonToken newToken, object? value, bool updateIndex)
		{
			_tokenType = newToken;
			_value = value;
			switch (newToken)
			{
			case global::Newtonsoft.Json.JsonToken.StartObject:
				_currentState = global::Newtonsoft.Json.JsonReader.State.ObjectStart;
				Push(global::Newtonsoft.Json.JsonContainerType.Object);
				break;
			case global::Newtonsoft.Json.JsonToken.StartArray:
				_currentState = global::Newtonsoft.Json.JsonReader.State.ArrayStart;
				Push(global::Newtonsoft.Json.JsonContainerType.Array);
				break;
			case global::Newtonsoft.Json.JsonToken.StartConstructor:
				_currentState = global::Newtonsoft.Json.JsonReader.State.ConstructorStart;
				Push(global::Newtonsoft.Json.JsonContainerType.Constructor);
				break;
			case global::Newtonsoft.Json.JsonToken.EndObject:
				ValidateEnd(global::Newtonsoft.Json.JsonToken.EndObject);
				break;
			case global::Newtonsoft.Json.JsonToken.EndArray:
				ValidateEnd(global::Newtonsoft.Json.JsonToken.EndArray);
				break;
			case global::Newtonsoft.Json.JsonToken.EndConstructor:
				ValidateEnd(global::Newtonsoft.Json.JsonToken.EndConstructor);
				break;
			case global::Newtonsoft.Json.JsonToken.PropertyName:
				_currentState = global::Newtonsoft.Json.JsonReader.State.Property;
				_currentPosition.PropertyName = (string)value;
				break;
			case global::Newtonsoft.Json.JsonToken.Raw:
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			case global::Newtonsoft.Json.JsonToken.String:
			case global::Newtonsoft.Json.JsonToken.Boolean:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.Undefined:
			case global::Newtonsoft.Json.JsonToken.Date:
			case global::Newtonsoft.Json.JsonToken.Bytes:
				SetPostValueState(updateIndex);
				break;
			case global::Newtonsoft.Json.JsonToken.Comment:
				break;
			}
		}

		internal void SetPostValueState(bool updateIndex)
		{
			if (Peek() != global::Newtonsoft.Json.JsonContainerType.None || SupportMultipleContent)
			{
				_currentState = global::Newtonsoft.Json.JsonReader.State.PostValue;
			}
			else
			{
				SetFinished();
			}
			if (updateIndex)
			{
				UpdateScopeWithFinishedValue();
			}
		}

		private void UpdateScopeWithFinishedValue()
		{
			if (_currentPosition.HasIndex)
			{
				_currentPosition.Position++;
			}
		}

		private void ValidateEnd(global::Newtonsoft.Json.JsonToken endToken)
		{
			global::Newtonsoft.Json.JsonContainerType jsonContainerType = Pop();
			if (GetTypeForCloseToken(endToken) != jsonContainerType)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("JsonToken {0} is not valid for closing JsonType {1}.", global::System.Globalization.CultureInfo.InvariantCulture, endToken, jsonContainerType));
			}
			if (Peek() != global::Newtonsoft.Json.JsonContainerType.None || SupportMultipleContent)
			{
				_currentState = global::Newtonsoft.Json.JsonReader.State.PostValue;
			}
			else
			{
				SetFinished();
			}
		}

		protected void SetStateBasedOnCurrent()
		{
			global::Newtonsoft.Json.JsonContainerType jsonContainerType = Peek();
			switch (jsonContainerType)
			{
			case global::Newtonsoft.Json.JsonContainerType.Object:
				_currentState = global::Newtonsoft.Json.JsonReader.State.Object;
				break;
			case global::Newtonsoft.Json.JsonContainerType.Array:
				_currentState = global::Newtonsoft.Json.JsonReader.State.Array;
				break;
			case global::Newtonsoft.Json.JsonContainerType.Constructor:
				_currentState = global::Newtonsoft.Json.JsonReader.State.Constructor;
				break;
			case global::Newtonsoft.Json.JsonContainerType.None:
				SetFinished();
				break;
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("While setting the reader state back to current object an unexpected JsonType was encountered: {0}", global::System.Globalization.CultureInfo.InvariantCulture, jsonContainerType));
			}
		}

		private void SetFinished()
		{
			_currentState = ((!SupportMultipleContent) ? global::Newtonsoft.Json.JsonReader.State.Finished : global::Newtonsoft.Json.JsonReader.State.Start);
		}

		private global::Newtonsoft.Json.JsonContainerType GetTypeForCloseToken(global::Newtonsoft.Json.JsonToken token)
		{
			return token switch
			{
				global::Newtonsoft.Json.JsonToken.EndObject => global::Newtonsoft.Json.JsonContainerType.Object, 
				global::Newtonsoft.Json.JsonToken.EndArray => global::Newtonsoft.Json.JsonContainerType.Array, 
				global::Newtonsoft.Json.JsonToken.EndConstructor => global::Newtonsoft.Json.JsonContainerType.Constructor, 
				_ => throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Not a valid close JsonToken: {0}", global::System.Globalization.CultureInfo.InvariantCulture, token)), 
			};
		}

		void global::System.IDisposable.Dispose()
		{
			Dispose(disposing: true);
			global::System.GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_currentState != global::Newtonsoft.Json.JsonReader.State.Closed && disposing)
			{
				Close();
			}
		}

		public virtual void Close()
		{
			_currentState = global::Newtonsoft.Json.JsonReader.State.Closed;
			_tokenType = global::Newtonsoft.Json.JsonToken.None;
			_value = null;
		}

		internal void ReadAndAssert()
		{
			if (!Read())
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		internal void ReadForTypeAndAssert(global::Newtonsoft.Json.Serialization.JsonContract? contract, bool hasConverter)
		{
			if (!ReadForType(contract, hasConverter))
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		internal bool ReadForType(global::Newtonsoft.Json.Serialization.JsonContract? contract, bool hasConverter)
		{
			if (hasConverter)
			{
				return Read();
			}
			switch (contract?.InternalReadType ?? global::Newtonsoft.Json.ReadType.Read)
			{
			case global::Newtonsoft.Json.ReadType.Read:
				return ReadAndMoveToContent();
			case global::Newtonsoft.Json.ReadType.ReadAsInt32:
				ReadAsInt32();
				break;
			case global::Newtonsoft.Json.ReadType.ReadAsInt64:
			{
				bool result = ReadAndMoveToContent();
				if (TokenType == global::Newtonsoft.Json.JsonToken.Undefined)
				{
					throw global::Newtonsoft.Json.JsonReaderException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("An undefined token is not a valid {0}.", global::System.Globalization.CultureInfo.InvariantCulture, contract?.UnderlyingType ?? typeof(long)));
				}
				return result;
			}
			case global::Newtonsoft.Json.ReadType.ReadAsDecimal:
				ReadAsDecimal();
				break;
			case global::Newtonsoft.Json.ReadType.ReadAsDouble:
				ReadAsDouble();
				break;
			case global::Newtonsoft.Json.ReadType.ReadAsBytes:
				ReadAsBytes();
				break;
			case global::Newtonsoft.Json.ReadType.ReadAsBoolean:
				ReadAsBoolean();
				break;
			case global::Newtonsoft.Json.ReadType.ReadAsString:
				ReadAsString();
				break;
			case global::Newtonsoft.Json.ReadType.ReadAsDateTime:
				ReadAsDateTime();
				break;
			case global::Newtonsoft.Json.ReadType.ReadAsDateTimeOffset:
				ReadAsDateTimeOffset();
				break;
			default:
				throw new global::System.ArgumentOutOfRangeException();
			}
			return TokenType != global::Newtonsoft.Json.JsonToken.None;
		}

		internal bool ReadAndMoveToContent()
		{
			if (Read())
			{
				return MoveToContent();
			}
			return false;
		}

		internal bool MoveToContent()
		{
			global::Newtonsoft.Json.JsonToken tokenType = TokenType;
			while (tokenType == global::Newtonsoft.Json.JsonToken.None || tokenType == global::Newtonsoft.Json.JsonToken.Comment)
			{
				if (!Read())
				{
					return false;
				}
				tokenType = TokenType;
			}
			return true;
		}

		private global::Newtonsoft.Json.JsonToken GetContentToken()
		{
			global::Newtonsoft.Json.JsonToken tokenType;
			do
			{
				if (!Read())
				{
					SetToken(global::Newtonsoft.Json.JsonToken.None);
					return global::Newtonsoft.Json.JsonToken.None;
				}
				tokenType = TokenType;
			}
			while (tokenType == global::Newtonsoft.Json.JsonToken.Comment);
			return tokenType;
		}
	}
}
