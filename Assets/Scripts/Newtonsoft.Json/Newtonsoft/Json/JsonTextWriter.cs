namespace Newtonsoft.Json
{
	public class JsonTextWriter : global::Newtonsoft.Json.JsonWriter
	{
		private readonly bool _safeAsync;

		private const int IndentCharBufferSize = 12;

		private readonly global::System.IO.TextWriter _writer;

		private global::Newtonsoft.Json.Utilities.Base64Encoder? _base64Encoder;

		private char _indentChar;

		private int _indentation;

		private char _quoteChar;

		private bool _quoteName;

		private bool[]? _charEscapeFlags;

		private char[]? _writeBuffer;

		private global::Newtonsoft.Json.IArrayPool<char>? _arrayPool;

		private char[]? _indentChars;

		private global::Newtonsoft.Json.Utilities.Base64Encoder Base64Encoder
		{
			get
			{
				if (_base64Encoder == null)
				{
					_base64Encoder = new global::Newtonsoft.Json.Utilities.Base64Encoder(_writer);
				}
				return _base64Encoder;
			}
		}

		public global::Newtonsoft.Json.IArrayPool<char>? ArrayPool
		{
			get
			{
				return _arrayPool;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				_arrayPool = value;
			}
		}

		public int Indentation
		{
			get
			{
				return _indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new global::System.ArgumentException("Indentation value must be greater than 0.");
				}
				_indentation = value;
			}
		}

		public char QuoteChar
		{
			get
			{
				return _quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new global::System.ArgumentException("Invalid JavaScript string quote character. Valid quote characters are ' and \".");
				}
				_quoteChar = value;
				UpdateCharEscapeFlags();
			}
		}

		public char IndentChar
		{
			get
			{
				return _indentChar;
			}
			set
			{
				if (value != _indentChar)
				{
					_indentChar = value;
					_indentChars = null;
				}
			}
		}

		public bool QuoteName
		{
			get
			{
				return _quoteName;
			}
			set
			{
				_quoteName = value;
			}
		}

		public override global::System.Threading.Tasks.Task FlushAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.FlushAsync(cancellationToken);
			}
			return DoFlushAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoFlushAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CancelIfRequestedAsync(cancellationToken) ?? _writer.FlushAsync();
		}

		protected override global::System.Threading.Tasks.Task WriteValueDelimiterAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (!_safeAsync)
			{
				return base.WriteValueDelimiterAsync(cancellationToken);
			}
			return DoWriteValueDelimiterAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueDelimiterAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, ',', cancellationToken);
		}

		protected override global::System.Threading.Tasks.Task WriteEndAsync(global::Newtonsoft.Json.JsonToken token, global::System.Threading.CancellationToken cancellationToken)
		{
			if (!_safeAsync)
			{
				return base.WriteEndAsync(token, cancellationToken);
			}
			return DoWriteEndAsync(token, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteEndAsync(global::Newtonsoft.Json.JsonToken token, global::System.Threading.CancellationToken cancellationToken)
		{
			return token switch
			{
				global::Newtonsoft.Json.JsonToken.EndObject => global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, '}', cancellationToken), 
				global::Newtonsoft.Json.JsonToken.EndArray => global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, ']', cancellationToken), 
				global::Newtonsoft.Json.JsonToken.EndConstructor => global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, ')', cancellationToken), 
				_ => throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Invalid JsonToken: " + token, null), 
			};
		}

		public override global::System.Threading.Tasks.Task CloseAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.CloseAsync(cancellationToken);
			}
			return DoCloseAsync(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoCloseAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (base.Top == 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
			}
			while (base.Top > 0)
			{
				await WriteEndAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			await CloseBufferAndWriterAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		private async global::System.Threading.Tasks.Task CloseBufferAndWriterAsync()
		{
			if (_writeBuffer != null)
			{
				global::Newtonsoft.Json.Utilities.BufferUtils.ReturnBuffer(_arrayPool, _writeBuffer);
				_writeBuffer = null;
			}
			if (base.CloseOutput && _writer != null)
			{
				await _writer.FlushAsync().ConfigureAwait(continueOnCapturedContext: false);
				_writer.Close();
			}
		}

		public override global::System.Threading.Tasks.Task WriteEndAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteEndAsync(cancellationToken);
			}
			return WriteEndInternalAsync(cancellationToken);
		}

		protected override global::System.Threading.Tasks.Task WriteIndentAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (!_safeAsync)
			{
				return base.WriteIndentAsync(cancellationToken);
			}
			return DoWriteIndentAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteIndentAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			int num = base.Top * _indentation;
			int num2 = SetIndentChars();
			if (num <= 12)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, _indentChars, 0, num2 + num, cancellationToken);
			}
			return WriteIndentAsync(num, num2, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task WriteIndentAsync(int currentIndentCount, int newLineLen, global::System.Threading.CancellationToken cancellationToken)
		{
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, _indentChars, 0, newLineLen + global::System.Math.Min(currentIndentCount, 12), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			while (true)
			{
				int num;
				currentIndentCount = (num = currentIndentCount - 12);
				if (num <= 0)
				{
					break;
				}
				await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, _indentChars, newLineLen, global::System.Math.Min(currentIndentCount, 12), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		private global::System.Threading.Tasks.Task WriteValueInternalAsync(global::Newtonsoft.Json.JsonToken token, string value, global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.Task task = InternalWriteValueAsync(token, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, value, cancellationToken);
			}
			return WriteValueInternalAsync(task, value, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task WriteValueInternalAsync(global::System.Threading.Tasks.Task task, string value, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, value, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		protected override global::System.Threading.Tasks.Task WriteIndentSpaceAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (!_safeAsync)
			{
				return base.WriteIndentSpaceAsync(cancellationToken);
			}
			return DoWriteIndentSpaceAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteIndentSpaceAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, ' ', cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteRawAsync(string? json, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteRawAsync(json, cancellationToken);
			}
			return DoWriteRawAsync(json, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteRawAsync(string? json, global::System.Threading.CancellationToken cancellationToken)
		{
			return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, json, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteNullAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteNullAsync(cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteNullAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(global::Newtonsoft.Json.JsonToken.Null, global::Newtonsoft.Json.JsonConvert.Null, cancellationToken);
		}

		private global::System.Threading.Tasks.Task WriteDigitsAsync(ulong uvalue, bool negative, global::System.Threading.CancellationToken cancellationToken)
		{
			if (uvalue <= 9 && !negative)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, (char)(48 + uvalue), cancellationToken);
			}
			int count = WriteNumberToBuffer(uvalue, negative);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, _writeBuffer, 0, count, cancellationToken);
		}

		private global::System.Threading.Tasks.Task WriteIntegerValueAsync(ulong uvalue, bool negative, global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.Task task = InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken.Integer, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return WriteDigitsAsync(uvalue, negative, cancellationToken);
			}
			return WriteIntegerValueAsync(task, uvalue, negative, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task WriteIntegerValueAsync(global::System.Threading.Tasks.Task task, ulong uvalue, bool negative, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await WriteDigitsAsync(uvalue, negative, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		internal global::System.Threading.Tasks.Task WriteIntegerValueAsync(long value, global::System.Threading.CancellationToken cancellationToken)
		{
			bool flag = value < 0;
			if (flag)
			{
				value = -value;
			}
			return WriteIntegerValueAsync((ulong)value, flag, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task WriteIntegerValueAsync(ulong uvalue, global::System.Threading.CancellationToken cancellationToken)
		{
			return WriteIntegerValueAsync(uvalue, negative: false, cancellationToken);
		}

		private global::System.Threading.Tasks.Task WriteEscapedStringAsync(string value, bool quote, global::System.Threading.CancellationToken cancellationToken)
		{
			return global::Newtonsoft.Json.Utilities.JavaScriptUtils.WriteEscapedJavaScriptStringAsync(_writer, value, _quoteChar, quote, _charEscapeFlags, base.StringEscapeHandling, this, _writeBuffer, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WritePropertyNameAsync(string name, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WritePropertyNameAsync(name, cancellationToken);
			}
			return DoWritePropertyNameAsync(name, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWritePropertyNameAsync(string name, global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.Task task = InternalWritePropertyNameAsync(name, cancellationToken);
			if (!global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return DoWritePropertyNameAsync(task, name, cancellationToken);
			}
			task = WriteEscapedStringAsync(name, _quoteName, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, ':', cancellationToken);
			}
			return global::Newtonsoft.Json.Utilities.JavaScriptUtils.WriteCharAsync(task, _writer, ':', cancellationToken);
		}

		private async global::System.Threading.Tasks.Task DoWritePropertyNameAsync(global::System.Threading.Tasks.Task task, string name, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await WriteEscapedStringAsync(name, _quoteName, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await _writer.WriteAsync(':').ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WritePropertyNameAsync(string name, bool escape, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WritePropertyNameAsync(name, escape, cancellationToken);
			}
			return DoWritePropertyNameAsync(name, escape, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoWritePropertyNameAsync(string name, bool escape, global::System.Threading.CancellationToken cancellationToken)
		{
			await InternalWritePropertyNameAsync(name, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (escape)
			{
				await WriteEscapedStringAsync(name, _quoteName, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				if (_quoteName)
				{
					await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
				}
				await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, name, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (_quoteName)
				{
					await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			await _writer.WriteAsync(':').ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteStartArrayAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteStartArrayAsync(cancellationToken);
			}
			return DoWriteStartArrayAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteStartArrayAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.Task task = InternalWriteStartAsync(global::Newtonsoft.Json.JsonToken.StartArray, global::Newtonsoft.Json.JsonContainerType.Array, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, '[', cancellationToken);
			}
			return DoWriteStartArrayAsync(task, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoWriteStartArrayAsync(global::System.Threading.Tasks.Task task, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, '[', cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteStartObjectAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteStartObjectAsync(cancellationToken);
			}
			return DoWriteStartObjectAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteStartObjectAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.Task task = InternalWriteStartAsync(global::Newtonsoft.Json.JsonToken.StartObject, global::Newtonsoft.Json.JsonContainerType.Object, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, '{', cancellationToken);
			}
			return DoWriteStartObjectAsync(task, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoWriteStartObjectAsync(global::System.Threading.Tasks.Task task, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, '{', cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteStartConstructorAsync(string name, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteStartConstructorAsync(name, cancellationToken);
			}
			return DoWriteStartConstructorAsync(name, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoWriteStartConstructorAsync(string name, global::System.Threading.CancellationToken cancellationToken)
		{
			await InternalWriteStartAsync(global::Newtonsoft.Json.JsonToken.StartConstructor, global::Newtonsoft.Json.JsonContainerType.Constructor, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, "new ", cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, name, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await _writer.WriteAsync('(').ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteUndefinedAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteUndefinedAsync(cancellationToken);
			}
			return DoWriteUndefinedAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteUndefinedAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.Task task = InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken.Undefined, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, global::Newtonsoft.Json.JsonConvert.Undefined, cancellationToken);
			}
			return DoWriteUndefinedAsync(task, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task DoWriteUndefinedAsync(global::System.Threading.Tasks.Task task, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, global::Newtonsoft.Json.JsonConvert.Undefined, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteWhitespaceAsync(string ws, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteWhitespaceAsync(ws, cancellationToken);
			}
			return DoWriteWhitespaceAsync(ws, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteWhitespaceAsync(string ws, global::System.Threading.CancellationToken cancellationToken)
		{
			InternalWriteWhitespace(ws);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, ws, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(bool value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(bool value, global::System.Threading.CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(global::Newtonsoft.Json.JsonToken.Boolean, global::Newtonsoft.Json.JsonConvert.ToString(value), cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(bool? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(bool? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value == true, cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(byte value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(byte? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(byte? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(byte[]? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value != null)
			{
				return WriteValueNonNullAsync(value, cancellationToken);
			}
			return WriteNullAsync(cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task WriteValueNonNullAsync(byte[] value, global::System.Threading.CancellationToken cancellationToken)
		{
			await InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken.Bytes, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
			await Base64Encoder.EncodeAsync(value, 0, value.Length, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await Base64Encoder.FlushAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(char value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(char value, global::System.Threading.CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(global::Newtonsoft.Json.JsonToken.String, global::Newtonsoft.Json.JsonConvert.ToString(value), cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(char? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(char? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(global::System.DateTime value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoWriteValueAsync(global::System.DateTime value, global::System.Threading.CancellationToken cancellationToken)
		{
			await InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken.Date, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			value = global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int count = WriteValueToBuffer(value);
				await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, _writeBuffer, 0, count, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
				await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, value.ToString(base.DateFormatString, base.Culture), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(global::System.DateTime? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(global::System.DateTime? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(global::System.DateTimeOffset value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoWriteValueAsync(global::System.DateTimeOffset value, global::System.Threading.CancellationToken cancellationToken)
		{
			await InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken.Date, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int count = WriteValueToBuffer(value);
				await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, _writeBuffer, 0, count, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
				await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, value.ToString(base.DateFormatString, base.Culture), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(global::System.DateTimeOffset? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(global::System.DateTimeOffset? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(decimal value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(decimal value, global::System.Threading.CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(global::Newtonsoft.Json.JsonToken.Float, global::Newtonsoft.Json.JsonConvert.ToString(value), cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(decimal? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(decimal? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(double value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteValueAsync(value, nullable: false, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task WriteValueAsync(double value, bool nullable, global::System.Threading.CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(global::Newtonsoft.Json.JsonToken.Float, global::Newtonsoft.Json.JsonConvert.ToString(value, base.FloatFormatHandling, QuoteChar, nullable), cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(double? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (!value.HasValue)
			{
				return WriteNullAsync(cancellationToken);
			}
			return WriteValueAsync(value.GetValueOrDefault(), nullable: true, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(float value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteValueAsync(value, nullable: false, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task WriteValueAsync(float value, bool nullable, global::System.Threading.CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(global::Newtonsoft.Json.JsonToken.Float, global::Newtonsoft.Json.JsonConvert.ToString(value, base.FloatFormatHandling, QuoteChar, nullable), cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(float? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (!value.HasValue)
			{
				return WriteNullAsync(cancellationToken);
			}
			return WriteValueAsync(value.GetValueOrDefault(), nullable: true, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(global::System.Guid value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoWriteValueAsync(global::System.Guid value, global::System.Threading.CancellationToken cancellationToken)
		{
			await InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken.String, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, value.ToString("D", global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await _writer.WriteAsync(_quoteChar).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(global::System.Guid? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(global::System.Guid? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(int value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(int? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(int? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(long value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(long? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(long? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task WriteValueAsync(global::System.Numerics.BigInteger value, global::System.Threading.CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(global::Newtonsoft.Json.JsonToken.Integer, value.ToString(global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(object? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (_safeAsync)
			{
				if (value == null)
				{
					return WriteNullAsync(cancellationToken);
				}
				if (value is global::System.Numerics.BigInteger value2)
				{
					return WriteValueAsync(value2, cancellationToken);
				}
				return global::Newtonsoft.Json.JsonWriter.WriteValueAsync(this, global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeCode(value.GetType()), value, cancellationToken);
			}
			return base.WriteValueAsync(value, cancellationToken);
		}

		[global::System.CLSCompliant(false)]
		public override global::System.Threading.Tasks.Task WriteValueAsync(sbyte value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		[global::System.CLSCompliant(false)]
		public override global::System.Threading.Tasks.Task WriteValueAsync(sbyte? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(sbyte? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(short value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(short? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(short? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(string? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(string? value, global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.Task task = InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken.String, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				if (value != null)
				{
					return WriteEscapedStringAsync(value, quote: true, cancellationToken);
				}
				return global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, global::Newtonsoft.Json.JsonConvert.Null, cancellationToken);
			}
			return DoWriteValueAsync(task, value, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task DoWriteValueAsync(global::System.Threading.Tasks.Task task, string? value, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await ((value == null) ? global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, global::Newtonsoft.Json.JsonConvert.Null, cancellationToken) : WriteEscapedStringAsync(value, quote: true, cancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(global::System.TimeSpan value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoWriteValueAsync(global::System.TimeSpan value, global::System.Threading.CancellationToken cancellationToken)
		{
			await InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken.String, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, _quoteChar, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, _quoteChar, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(global::System.TimeSpan? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(global::System.TimeSpan? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		[global::System.CLSCompliant(false)]
		public override global::System.Threading.Tasks.Task WriteValueAsync(uint value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		[global::System.CLSCompliant(false)]
		public override global::System.Threading.Tasks.Task WriteValueAsync(uint? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(uint? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		[global::System.CLSCompliant(false)]
		public override global::System.Threading.Tasks.Task WriteValueAsync(ulong value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		[global::System.CLSCompliant(false)]
		public override global::System.Threading.Tasks.Task WriteValueAsync(ulong? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(ulong? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteValueAsync(global::System.Uri? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (!(value == null))
			{
				return WriteValueNotNullAsync(value, cancellationToken);
			}
			return WriteNullAsync(cancellationToken);
		}

		internal global::System.Threading.Tasks.Task WriteValueNotNullAsync(global::System.Uri value, global::System.Threading.CancellationToken cancellationToken)
		{
			global::System.Threading.Tasks.Task task = InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken.String, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return WriteEscapedStringAsync(value.OriginalString, quote: true, cancellationToken);
			}
			return WriteValueNotNullAsync(task, value, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task WriteValueNotNullAsync(global::System.Threading.Tasks.Task task, global::System.Uri value, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await WriteEscapedStringAsync(value.OriginalString, quote: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		[global::System.CLSCompliant(false)]
		public override global::System.Threading.Tasks.Task WriteValueAsync(ushort value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		[global::System.CLSCompliant(false)]
		public override global::System.Threading.Tasks.Task WriteValueAsync(ushort? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteValueAsync(ushort? value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteCommentAsync(string? text, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteCommentAsync(text, cancellationToken);
			}
			return DoWriteCommentAsync(text, cancellationToken);
		}

		internal async global::System.Threading.Tasks.Task DoWriteCommentAsync(string? text, global::System.Threading.CancellationToken cancellationToken)
		{
			await InternalWriteCommentAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, "/*", cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, text ?? string.Empty, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await global::Newtonsoft.Json.Utilities.AsyncUtils.WriteAsync(_writer, "*/", cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override global::System.Threading.Tasks.Task WriteEndArrayAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteEndArrayAsync(cancellationToken);
			}
			return InternalWriteEndAsync(global::Newtonsoft.Json.JsonContainerType.Array, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteEndConstructorAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteEndConstructorAsync(cancellationToken);
			}
			return InternalWriteEndAsync(global::Newtonsoft.Json.JsonContainerType.Constructor, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteEndObjectAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteEndObjectAsync(cancellationToken);
			}
			return InternalWriteEndAsync(global::Newtonsoft.Json.JsonContainerType.Object, cancellationToken);
		}

		public override global::System.Threading.Tasks.Task WriteRawValueAsync(string? json, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteRawValueAsync(json, cancellationToken);
			}
			return DoWriteRawValueAsync(json, cancellationToken);
		}

		internal global::System.Threading.Tasks.Task DoWriteRawValueAsync(string? json, global::System.Threading.CancellationToken cancellationToken)
		{
			UpdateScopeWithFinishedValue();
			global::System.Threading.Tasks.Task task = AutoCompleteAsync(global::Newtonsoft.Json.JsonToken.Undefined, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return WriteRawAsync(json, cancellationToken);
			}
			return DoWriteRawValueAsync(task, json, cancellationToken);
		}

		private async global::System.Threading.Tasks.Task DoWriteRawValueAsync(global::System.Threading.Tasks.Task task, string? json, global::System.Threading.CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await WriteRawAsync(json, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		internal char[] EnsureWriteBuffer(int length, int copyTo)
		{
			if (length < 35)
			{
				length = 35;
			}
			char[] writeBuffer = _writeBuffer;
			if (writeBuffer == null)
			{
				return _writeBuffer = global::Newtonsoft.Json.Utilities.BufferUtils.RentBuffer(_arrayPool, length);
			}
			if (writeBuffer.Length >= length)
			{
				return writeBuffer;
			}
			char[] array = global::Newtonsoft.Json.Utilities.BufferUtils.RentBuffer(_arrayPool, length);
			if (copyTo != 0)
			{
				global::System.Array.Copy(writeBuffer, array, copyTo);
			}
			global::Newtonsoft.Json.Utilities.BufferUtils.ReturnBuffer(_arrayPool, writeBuffer);
			_writeBuffer = array;
			return array;
		}

		public JsonTextWriter(global::System.IO.TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new global::System.ArgumentNullException("textWriter");
			}
			_writer = textWriter;
			_quoteChar = '"';
			_quoteName = true;
			_indentChar = ' ';
			_indentation = 2;
			UpdateCharEscapeFlags();
			_safeAsync = GetType() == typeof(global::Newtonsoft.Json.JsonTextWriter);
		}

		public override void Flush()
		{
			_writer.Flush();
		}

		public override void Close()
		{
			base.Close();
			CloseBufferAndWriter();
		}

		private void CloseBufferAndWriter()
		{
			if (_writeBuffer != null)
			{
				global::Newtonsoft.Json.Utilities.BufferUtils.ReturnBuffer(_arrayPool, _writeBuffer);
				_writeBuffer = null;
			}
			if (base.CloseOutput)
			{
				_writer?.Close();
			}
		}

		public override void WriteStartObject()
		{
			InternalWriteStart(global::Newtonsoft.Json.JsonToken.StartObject, global::Newtonsoft.Json.JsonContainerType.Object);
			_writer.Write('{');
		}

		public override void WriteStartArray()
		{
			InternalWriteStart(global::Newtonsoft.Json.JsonToken.StartArray, global::Newtonsoft.Json.JsonContainerType.Array);
			_writer.Write('[');
		}

		public override void WriteStartConstructor(string name)
		{
			InternalWriteStart(global::Newtonsoft.Json.JsonToken.StartConstructor, global::Newtonsoft.Json.JsonContainerType.Constructor);
			_writer.Write("new ");
			_writer.Write(name);
			_writer.Write('(');
		}

		protected override void WriteEnd(global::Newtonsoft.Json.JsonToken token)
		{
			switch (token)
			{
			case global::Newtonsoft.Json.JsonToken.EndObject:
				_writer.Write('}');
				break;
			case global::Newtonsoft.Json.JsonToken.EndArray:
				_writer.Write(']');
				break;
			case global::Newtonsoft.Json.JsonToken.EndConstructor:
				_writer.Write(')');
				break;
			default:
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Invalid JsonToken: " + token, null);
			}
		}

		public override void WritePropertyName(string name)
		{
			InternalWritePropertyName(name);
			WriteEscapedString(name, _quoteName);
			_writer.Write(':');
		}

		public override void WritePropertyName(string name, bool escape)
		{
			InternalWritePropertyName(name);
			if (escape)
			{
				WriteEscapedString(name, _quoteName);
			}
			else
			{
				if (_quoteName)
				{
					_writer.Write(_quoteChar);
				}
				_writer.Write(name);
				if (_quoteName)
				{
					_writer.Write(_quoteChar);
				}
			}
			_writer.Write(':');
		}

		internal override void OnStringEscapeHandlingChanged()
		{
			UpdateCharEscapeFlags();
		}

		private void UpdateCharEscapeFlags()
		{
			_charEscapeFlags = global::Newtonsoft.Json.Utilities.JavaScriptUtils.GetCharEscapeFlags(base.StringEscapeHandling, _quoteChar);
		}

		protected override void WriteIndent()
		{
			int num = base.Top * _indentation;
			int num2 = SetIndentChars();
			_writer.Write(_indentChars, 0, num2 + global::System.Math.Min(num, 12));
			while ((num -= 12) > 0)
			{
				_writer.Write(_indentChars, num2, global::System.Math.Min(num, 12));
			}
		}

		private int SetIndentChars()
		{
			string newLine = _writer.NewLine;
			int length = newLine.Length;
			bool flag = _indentChars != null && _indentChars.Length == 12 + length;
			if (flag)
			{
				for (int i = 0; i != length; i++)
				{
					if (newLine[i] != _indentChars[i])
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				_indentChars = (newLine + new string(_indentChar, 12)).ToCharArray();
			}
			return length;
		}

		protected override void WriteValueDelimiter()
		{
			_writer.Write(',');
		}

		protected override void WriteIndentSpace()
		{
			_writer.Write(' ');
		}

		private void WriteValueInternal(string value, global::Newtonsoft.Json.JsonToken token)
		{
			_writer.Write(value);
		}

		public override void WriteValue(object? value)
		{
			if (value is global::System.Numerics.BigInteger bigInteger)
			{
				InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
				WriteValueInternal(bigInteger.ToString(global::System.Globalization.CultureInfo.InvariantCulture), global::Newtonsoft.Json.JsonToken.String);
			}
			else
			{
				base.WriteValue(value);
			}
		}

		public override void WriteNull()
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Null);
			WriteValueInternal(global::Newtonsoft.Json.JsonConvert.Null, global::Newtonsoft.Json.JsonToken.Null);
		}

		public override void WriteUndefined()
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Undefined);
			WriteValueInternal(global::Newtonsoft.Json.JsonConvert.Undefined, global::Newtonsoft.Json.JsonToken.Undefined);
		}

		public override void WriteRaw(string? json)
		{
			InternalWriteRaw();
			_writer.Write(json);
		}

		public override void WriteValue(string? value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
			if (value == null)
			{
				WriteValueInternal(global::Newtonsoft.Json.JsonConvert.Null, global::Newtonsoft.Json.JsonToken.Null);
			}
			else
			{
				WriteEscapedString(value, quote: true);
			}
		}

		private void WriteEscapedString(string value, bool quote)
		{
			EnsureWriteBuffer();
			global::Newtonsoft.Json.Utilities.JavaScriptUtils.WriteEscapedJavaScriptString(_writer, value, _quoteChar, quote, _charEscapeFlags, base.StringEscapeHandling, _arrayPool, ref _writeBuffer);
		}

		public override void WriteValue(int value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
			WriteIntegerValue(value);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
			WriteIntegerValue(value);
		}

		public override void WriteValue(long value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
			WriteIntegerValue(value);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
			WriteIntegerValue(value, negative: false);
		}

		public override void WriteValue(float value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Float);
			WriteValueInternal(global::Newtonsoft.Json.JsonConvert.ToString(value, base.FloatFormatHandling, QuoteChar, nullable: false), global::Newtonsoft.Json.JsonToken.Float);
		}

		public override void WriteValue(float? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
				return;
			}
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Float);
			WriteValueInternal(global::Newtonsoft.Json.JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, QuoteChar, nullable: true), global::Newtonsoft.Json.JsonToken.Float);
		}

		public override void WriteValue(double value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Float);
			WriteValueInternal(global::Newtonsoft.Json.JsonConvert.ToString(value, base.FloatFormatHandling, QuoteChar, nullable: false), global::Newtonsoft.Json.JsonToken.Float);
		}

		public override void WriteValue(double? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
				return;
			}
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Float);
			WriteValueInternal(global::Newtonsoft.Json.JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, QuoteChar, nullable: true), global::Newtonsoft.Json.JsonToken.Float);
		}

		public override void WriteValue(bool value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Boolean);
			WriteValueInternal(global::Newtonsoft.Json.JsonConvert.ToString(value), global::Newtonsoft.Json.JsonToken.Boolean);
		}

		public override void WriteValue(short value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
			WriteIntegerValue(value);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
			WriteIntegerValue(value);
		}

		public override void WriteValue(char value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
			WriteValueInternal(global::Newtonsoft.Json.JsonConvert.ToString(value), global::Newtonsoft.Json.JsonToken.String);
		}

		public override void WriteValue(byte value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
			WriteIntegerValue(value);
		}

		[global::System.CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
			WriteIntegerValue(value);
		}

		public override void WriteValue(decimal value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Float);
			WriteValueInternal(global::Newtonsoft.Json.JsonConvert.ToString(value), global::Newtonsoft.Json.JsonToken.Float);
		}

		public override void WriteValue(global::System.DateTime value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Date);
			value = global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int count = WriteValueToBuffer(value);
				_writer.Write(_writeBuffer, 0, count);
			}
			else
			{
				_writer.Write(_quoteChar);
				_writer.Write(value.ToString(base.DateFormatString, base.Culture));
				_writer.Write(_quoteChar);
			}
		}

		private int WriteValueToBuffer(global::System.DateTime value)
		{
			EnsureWriteBuffer();
			int start = 0;
			_writeBuffer[start++] = _quoteChar;
			start = global::Newtonsoft.Json.Utilities.DateTimeUtils.WriteDateTimeString(_writeBuffer, start, value, null, value.Kind, base.DateFormatHandling);
			_writeBuffer[start++] = _quoteChar;
			return start;
		}

		public override void WriteValue(byte[]? value)
		{
			if (value == null)
			{
				WriteNull();
				return;
			}
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Bytes);
			_writer.Write(_quoteChar);
			Base64Encoder.Encode(value, 0, value.Length);
			Base64Encoder.Flush();
			_writer.Write(_quoteChar);
		}

		public override void WriteValue(global::System.DateTimeOffset value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Date);
			if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int count = WriteValueToBuffer(value);
				_writer.Write(_writeBuffer, 0, count);
			}
			else
			{
				_writer.Write(_quoteChar);
				_writer.Write(value.ToString(base.DateFormatString, base.Culture));
				_writer.Write(_quoteChar);
			}
		}

		private int WriteValueToBuffer(global::System.DateTimeOffset value)
		{
			EnsureWriteBuffer();
			int start = 0;
			_writeBuffer[start++] = _quoteChar;
			start = global::Newtonsoft.Json.Utilities.DateTimeUtils.WriteDateTimeString(_writeBuffer, start, (base.DateFormatHandling == global::Newtonsoft.Json.DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, value.Offset, global::System.DateTimeKind.Local, base.DateFormatHandling);
			_writeBuffer[start++] = _quoteChar;
			return start;
		}

		public override void WriteValue(global::System.Guid value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
			string value2 = value.ToString("D", global::System.Globalization.CultureInfo.InvariantCulture);
			_writer.Write(_quoteChar);
			_writer.Write(value2);
			_writer.Write(_quoteChar);
		}

		public override void WriteValue(global::System.TimeSpan value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
			string value2 = value.ToString(null, global::System.Globalization.CultureInfo.InvariantCulture);
			_writer.Write(_quoteChar);
			_writer.Write(value2);
			_writer.Write(_quoteChar);
		}

		public override void WriteValue(global::System.Uri? value)
		{
			if (value == null)
			{
				WriteNull();
				return;
			}
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
			WriteEscapedString(value.OriginalString, quote: true);
		}

		public override void WriteComment(string? text)
		{
			InternalWriteComment();
			_writer.Write("/*");
			_writer.Write(text);
			_writer.Write("*/");
		}

		public override void WriteWhitespace(string ws)
		{
			InternalWriteWhitespace(ws);
			_writer.Write(ws);
		}

		private void EnsureWriteBuffer()
		{
			if (_writeBuffer == null)
			{
				_writeBuffer = global::Newtonsoft.Json.Utilities.BufferUtils.RentBuffer(_arrayPool, 35);
			}
		}

		private void WriteIntegerValue(long value)
		{
			if (value >= 0 && value <= 9)
			{
				_writer.Write((char)(48 + value));
				return;
			}
			bool flag = value < 0;
			WriteIntegerValue((ulong)(flag ? (-value) : value), flag);
		}

		private void WriteIntegerValue(ulong value, bool negative)
		{
			if (!negative && value <= 9)
			{
				_writer.Write((char)(48 + value));
				return;
			}
			int count = WriteNumberToBuffer(value, negative);
			_writer.Write(_writeBuffer, 0, count);
		}

		private int WriteNumberToBuffer(ulong value, bool negative)
		{
			if (value <= uint.MaxValue)
			{
				return WriteNumberToBuffer((uint)value, negative);
			}
			EnsureWriteBuffer();
			int num = global::Newtonsoft.Json.Utilities.MathUtils.IntLength(value);
			if (negative)
			{
				num++;
				_writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				ulong num3 = value / 10;
				ulong num4 = value - num3 * 10;
				_writeBuffer[--num2] = (char)(48 + num4);
				value = num3;
			}
			while (value != 0L);
			return num;
		}

		private void WriteIntegerValue(int value)
		{
			if (value >= 0 && value <= 9)
			{
				_writer.Write((char)(48 + value));
				return;
			}
			bool flag = value < 0;
			WriteIntegerValue((uint)(flag ? (-value) : value), flag);
		}

		private void WriteIntegerValue(uint value, bool negative)
		{
			if (!negative && value <= 9)
			{
				_writer.Write((char)(48 + value));
				return;
			}
			int count = WriteNumberToBuffer(value, negative);
			_writer.Write(_writeBuffer, 0, count);
		}

		private int WriteNumberToBuffer(uint value, bool negative)
		{
			EnsureWriteBuffer();
			int num = global::Newtonsoft.Json.Utilities.MathUtils.IntLength(value);
			if (negative)
			{
				num++;
				_writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				uint num3 = value / 10;
				uint num4 = value - num3 * 10;
				_writeBuffer[--num2] = (char)(48 + num4);
				value = num3;
			}
			while (value != 0);
			return num;
		}
	}
}
