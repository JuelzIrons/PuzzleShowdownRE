namespace Newtonsoft.Json
{
	public abstract class JsonWriter : global::System.IDisposable
	{
		internal enum State
		{
			Start = 0,
			Property = 1,
			ObjectStart = 2,
			Object = 3,
			ArrayStart = 4,
			Array = 5,
			ConstructorStart = 6,
			Constructor = 7,
			Closed = 8,
			Error = 9
		}

		private static readonly global::Newtonsoft.Json.JsonWriter.State[][] StateArray;

		internal static readonly global::Newtonsoft.Json.JsonWriter.State[][] StateArrayTemplate;

		private global::System.Collections.Generic.List<global::Newtonsoft.Json.JsonPosition>? _stack;

		private global::Newtonsoft.Json.JsonPosition _currentPosition;

		private global::Newtonsoft.Json.JsonWriter.State _currentState;

		private global::Newtonsoft.Json.Formatting _formatting;

		private global::Newtonsoft.Json.DateFormatHandling _dateFormatHandling;

		private global::Newtonsoft.Json.DateTimeZoneHandling _dateTimeZoneHandling;

		private global::Newtonsoft.Json.StringEscapeHandling _stringEscapeHandling;

		private global::Newtonsoft.Json.FloatFormatHandling _floatFormatHandling;

		private string? _dateFormatString;

		private global::System.Globalization.CultureInfo? _culture;

		public bool CloseOutput { get; set; }

		public bool AutoCompleteOnClose { get; set; }

		protected internal int Top
		{
			get
			{
				int num = _stack?.Count ?? 0;
				if (Peek() != global::Newtonsoft.Json.JsonContainerType.None)
				{
					num++;
				}
				return num;
			}
		}

		public global::Newtonsoft.Json.WriteState WriteState
		{
			get
			{
				switch (_currentState)
				{
				case global::Newtonsoft.Json.JsonWriter.State.Error:
					return global::Newtonsoft.Json.WriteState.Error;
				case global::Newtonsoft.Json.JsonWriter.State.Closed:
					return global::Newtonsoft.Json.WriteState.Closed;
				case global::Newtonsoft.Json.JsonWriter.State.ObjectStart:
				case global::Newtonsoft.Json.JsonWriter.State.Object:
					return global::Newtonsoft.Json.WriteState.Object;
				case global::Newtonsoft.Json.JsonWriter.State.ArrayStart:
				case global::Newtonsoft.Json.JsonWriter.State.Array:
					return global::Newtonsoft.Json.WriteState.Array;
				case global::Newtonsoft.Json.JsonWriter.State.ConstructorStart:
				case global::Newtonsoft.Json.JsonWriter.State.Constructor:
					return global::Newtonsoft.Json.WriteState.Constructor;
				case global::Newtonsoft.Json.JsonWriter.State.Property:
					return global::Newtonsoft.Json.WriteState.Property;
				case global::Newtonsoft.Json.JsonWriter.State.Start:
					return global::Newtonsoft.Json.WriteState.Start;
				default:
					throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Invalid state: " + _currentState, null);
				}
			}
		}

		internal string ContainerPath
		{
			get
			{
				if (_currentPosition.Type == global::Newtonsoft.Json.JsonContainerType.None || _stack == null)
				{
					return string.Empty;
				}
				return global::Newtonsoft.Json.JsonPosition.BuildPath(_stack, null);
			}
		}

		public string Path
		{
			get
			{
				if (_currentPosition.Type == global::Newtonsoft.Json.JsonContainerType.None)
				{
					return string.Empty;
				}
				global::Newtonsoft.Json.JsonPosition? currentPosition = ((_currentState != global::Newtonsoft.Json.JsonWriter.State.ArrayStart && _currentState != global::Newtonsoft.Json.JsonWriter.State.ConstructorStart && _currentState != global::Newtonsoft.Json.JsonWriter.State.ObjectStart) ? new global::Newtonsoft.Json.JsonPosition?(_currentPosition) : ((global::Newtonsoft.Json.JsonPosition?)null));
				return global::Newtonsoft.Json.JsonPosition.BuildPath(_stack, currentPosition);
			}
		}

		public global::Newtonsoft.Json.Formatting Formatting
		{
			get
			{
				return _formatting;
			}
			set
			{
				if (value < global::Newtonsoft.Json.Formatting.None || value > global::Newtonsoft.Json.Formatting.Indented)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_formatting = value;
			}
		}

		public global::Newtonsoft.Json.DateFormatHandling DateFormatHandling
		{
			get
			{
				return _dateFormatHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.DateFormatHandling.IsoDateFormat || value > global::Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_dateFormatHandling = value;
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

		public global::Newtonsoft.Json.StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return _stringEscapeHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.StringEscapeHandling.Default || value > global::Newtonsoft.Json.StringEscapeHandling.EscapeHtml)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_stringEscapeHandling = value;
				OnStringEscapeHandlingChanged();
			}
		}

		public global::Newtonsoft.Json.FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return _floatFormatHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.FloatFormatHandling.String || value > global::Newtonsoft.Json.FloatFormatHandling.DefaultValue)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_floatFormatHandling = value;
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

		internal global::System.Threading.Tasks.Task AutoCompleteAsync(global::Newtonsoft.Json.JsonToken tokenBeingWritten, global::System.Threading.CancellationToken cancellationToken)
		{
			global::Newtonsoft.Json.JsonWriter.State currentState = _currentState;
			global::Newtonsoft.Json.JsonWriter.State state = StateArray[(int)tokenBeingWritten][(int)currentState];
			if (state == global::Newtonsoft.Json.JsonWriter.State.Error)
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Token {0} in state {1} would result in an invalid JSON object.", global::System.Globalization.CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), currentState.ToString()), null);
			}
			_currentState = state;
			if (_formatting == global::Newtonsoft.Json.Formatting.Indented)
			{
				switch (currentState)
				{
				case global::Newtonsoft.Json.JsonWriter.State.Property:
					return WriteIndentSpaceAsync(cancellationToken);
				case global::Newtonsoft.Json.JsonWriter.State.ArrayStart:
				case global::Newtonsoft.Json.JsonWriter.State.ConstructorStart:
					return WriteIndentAsync(cancellationToken);
				case global::Newtonsoft.Json.JsonWriter.State.Array:
				case global::Newtonsoft.Json.JsonWriter.State.Constructor:
					if (tokenBeingWritten != global::Newtonsoft.Json.JsonToken.Comment)
					{
						return AutoCompleteAsync(cancellationToken);
					}
					return WriteIndentAsync(cancellationToken);
				case global::Newtonsoft.Json.JsonWriter.State.Object:
					switch (tokenBeingWritten)
					{
					case global::Newtonsoft.Json.JsonToken.PropertyName:
						return AutoCompleteAsync(cancellationToken);
					default:
						return WriteValueDelimiterAsync(cancellationToken);
					case global::Newtonsoft.Json.JsonToken.Comment:
						break;
					}
					break;
				default:
					if (tokenBeingWritten == global::Newtonsoft.Json.JsonToken.PropertyName)
					{
						return WriteIndentAsync(cancellationToken);
					}
					break;
				case global::Newtonsoft.Json.JsonWriter.State.Start:
					break;
				}
			}
			else if (tokenBeingWritten != global::Newtonsoft.Json.JsonToken.Comment)
			{
				switch (currentState)
				{
				case global::Newtonsoft.Json.JsonWriter.State.Object:
				case global::Newtonsoft.Json.JsonWriter.State.Array:
				case global::Newtonsoft.Json.JsonWriter.State.Constructor:
					return WriteValueDelimiterAsync(cancellationToken);
				}
			}
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		private async global::System.Threading.Tasks.Task AutoCompleteAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			await WriteValueDelimiterAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			await WriteIndentAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual global::System.Threading.Tasks.Task CloseAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			Close();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task FlushAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			Flush();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		protected virtual global::System.Threading.Tasks.Task WriteEndAsync(global::Newtonsoft.Json.JsonToken token, global::System.Threading.CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteEnd(token);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		protected virtual global::System.Threading.Tasks.Task WriteIndentAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteIndent();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		protected virtual global::System.Threading.Tasks.Task WriteValueDelimiterAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValueDelimiter();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		protected virtual global::System.Threading.Tasks.Task WriteIndentSpaceAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteIndentSpace();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteRawAsync(string? json, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteRaw(json);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteEndAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteEnd();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		internal global::System.Threading.Tasks.Task WriteEndInternalAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			global::Newtonsoft.Json.JsonContainerType jsonContainerType = Peek();
			switch (jsonContainerType)
			{
			case global::Newtonsoft.Json.JsonContainerType.Object:
				return WriteEndObjectAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonContainerType.Array:
				return WriteEndArrayAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonContainerType.Constructor:
				return WriteEndConstructorAsync(cancellationToken);
			default:
				if (cancellationToken.IsCancellationRequested)
				{
					return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
				}
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unexpected type when writing end: " + jsonContainerType, null);
			}
		}

		internal global::System.Threading.Tasks.Task InternalWriteEndAsync(global::Newtonsoft.Json.JsonContainerType type, global::System.Threading.CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			int levelsToComplete = CalculateLevelsToComplete(type);
			while (levelsToComplete-- > 0)
			{
				global::Newtonsoft.Json.JsonToken closeTokenForType = GetCloseTokenForType(Pop());
				global::System.Threading.Tasks.Task task;
				if (_currentState == global::Newtonsoft.Json.JsonWriter.State.Property)
				{
					task = WriteNullAsync(cancellationToken);
					if (!global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
					{
						return AwaitProperty(task, levelsToComplete, closeTokenForType, cancellationToken);
					}
				}
				if (_formatting == global::Newtonsoft.Json.Formatting.Indented && _currentState != global::Newtonsoft.Json.JsonWriter.State.ObjectStart && _currentState != global::Newtonsoft.Json.JsonWriter.State.ArrayStart)
				{
					task = WriteIndentAsync(cancellationToken);
					if (!global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
					{
						return AwaitIndent(task, levelsToComplete, closeTokenForType, cancellationToken);
					}
				}
				task = WriteEndAsync(closeTokenForType, cancellationToken);
				if (!global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
				{
					return AwaitEnd(task, levelsToComplete, cancellationToken);
				}
				UpdateCurrentState();
			}
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
			async global::System.Threading.Tasks.Task AwaitEnd(global::System.Threading.Tasks.Task task2, int LevelsToComplete, global::System.Threading.CancellationToken CancellationToken)
			{
				await task2.ConfigureAwait(continueOnCapturedContext: false);
				UpdateCurrentState();
				await AwaitRemaining(LevelsToComplete, CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			async global::System.Threading.Tasks.Task AwaitIndent(global::System.Threading.Tasks.Task task2, int LevelsToComplete, global::Newtonsoft.Json.JsonToken token, global::System.Threading.CancellationToken CancellationToken)
			{
				await task2.ConfigureAwait(continueOnCapturedContext: false);
				await WriteEndAsync(token, CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				UpdateCurrentState();
				await AwaitRemaining(LevelsToComplete, CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			async global::System.Threading.Tasks.Task AwaitProperty(global::System.Threading.Tasks.Task task2, int LevelsToComplete, global::Newtonsoft.Json.JsonToken token, global::System.Threading.CancellationToken CancellationToken)
			{
				await task2.ConfigureAwait(continueOnCapturedContext: false);
				if (_formatting == global::Newtonsoft.Json.Formatting.Indented && _currentState != global::Newtonsoft.Json.JsonWriter.State.ObjectStart && _currentState != global::Newtonsoft.Json.JsonWriter.State.ArrayStart)
				{
					await WriteIndentAsync(CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				await WriteEndAsync(token, CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				UpdateCurrentState();
				await AwaitRemaining(LevelsToComplete, CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			async global::System.Threading.Tasks.Task AwaitRemaining(int LevelsToComplete, global::System.Threading.CancellationToken CancellationToken)
			{
				while (LevelsToComplete-- > 0)
				{
					global::Newtonsoft.Json.JsonToken token = GetCloseTokenForType(Pop());
					if (_currentState == global::Newtonsoft.Json.JsonWriter.State.Property)
					{
						await WriteNullAsync(CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					if (_formatting == global::Newtonsoft.Json.Formatting.Indented && _currentState != global::Newtonsoft.Json.JsonWriter.State.ObjectStart && _currentState != global::Newtonsoft.Json.JsonWriter.State.ArrayStart)
					{
						await WriteIndentAsync(CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					await WriteEndAsync(token, CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					UpdateCurrentState();
				}
			}
		}

		public virtual global::System.Threading.Tasks.Task WriteEndArrayAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteEndArray();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteEndConstructorAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteEndConstructor();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteEndObjectAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteEndObject();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteNullAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteNull();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WritePropertyNameAsync(string name, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WritePropertyName(name);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WritePropertyNameAsync(string name, bool escape, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WritePropertyName(name, escape);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		internal global::System.Threading.Tasks.Task InternalWritePropertyNameAsync(string name, global::System.Threading.CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			_currentPosition.PropertyName = name;
			return AutoCompleteAsync(global::Newtonsoft.Json.JsonToken.PropertyName, cancellationToken);
		}

		public virtual global::System.Threading.Tasks.Task WriteStartArrayAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteStartArray();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		internal async global::System.Threading.Tasks.Task InternalWriteStartAsync(global::Newtonsoft.Json.JsonToken token, global::Newtonsoft.Json.JsonContainerType container, global::System.Threading.CancellationToken cancellationToken)
		{
			UpdateScopeWithFinishedValue();
			await AutoCompleteAsync(token, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			Push(container);
		}

		public virtual global::System.Threading.Tasks.Task WriteCommentAsync(string? text, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteComment(text);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		internal global::System.Threading.Tasks.Task InternalWriteCommentAsync(global::System.Threading.CancellationToken cancellationToken)
		{
			return AutoCompleteAsync(global::Newtonsoft.Json.JsonToken.Comment, cancellationToken);
		}

		public virtual global::System.Threading.Tasks.Task WriteRawValueAsync(string? json, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteRawValue(json);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteStartConstructorAsync(string name, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteStartConstructor(name);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteStartObjectAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteStartObject();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public global::System.Threading.Tasks.Task WriteTokenAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return WriteTokenAsync(reader, writeChildren: true, cancellationToken);
		}

		public global::System.Threading.Tasks.Task WriteTokenAsync(global::Newtonsoft.Json.JsonReader reader, bool writeChildren, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			return WriteTokenAsync(reader, writeChildren, writeDateConstructorAsDate: true, writeComments: true, cancellationToken);
		}

		public global::System.Threading.Tasks.Task WriteTokenAsync(global::Newtonsoft.Json.JsonToken token, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return WriteTokenAsync(token, null, cancellationToken);
		}

		public global::System.Threading.Tasks.Task WriteTokenAsync(global::Newtonsoft.Json.JsonToken token, object? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			switch (token)
			{
			case global::Newtonsoft.Json.JsonToken.None:
				return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
			case global::Newtonsoft.Json.JsonToken.StartObject:
				return WriteStartObjectAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonToken.StartArray:
				return WriteStartArrayAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonToken.StartConstructor:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				return WriteStartConstructorAsync(value.ToString(), cancellationToken);
			case global::Newtonsoft.Json.JsonToken.PropertyName:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				return WritePropertyNameAsync(value.ToString(), cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Comment:
				return WriteCommentAsync(value?.ToString(), cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Integer:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				if (!(value is global::System.Numerics.BigInteger bigInteger))
				{
					return WriteValueAsync(global::System.Convert.ToInt64(value, global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken);
				}
				return WriteValueAsync(bigInteger, cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Float:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				if (value is decimal value4)
				{
					return WriteValueAsync(value4, cancellationToken);
				}
				if (value is double value5)
				{
					return WriteValueAsync(value5, cancellationToken);
				}
				if (value is float value6)
				{
					return WriteValueAsync(value6, cancellationToken);
				}
				return WriteValueAsync(global::System.Convert.ToDouble(value, global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken);
			case global::Newtonsoft.Json.JsonToken.String:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				return WriteValueAsync(value.ToString(), cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Boolean:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				return WriteValueAsync(global::System.Convert.ToBoolean(value, global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Null:
				return WriteNullAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Undefined:
				return WriteUndefinedAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonToken.EndObject:
				return WriteEndObjectAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return WriteEndArrayAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonToken.EndConstructor:
				return WriteEndConstructorAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Date:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				if (value is global::System.DateTimeOffset value3)
				{
					return WriteValueAsync(value3, cancellationToken);
				}
				return WriteValueAsync(global::System.Convert.ToDateTime(value, global::System.Globalization.CultureInfo.InvariantCulture), cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Raw:
				return WriteRawValueAsync(value?.ToString(), cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Bytes:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				if (value is global::System.Guid value2)
				{
					return WriteValueAsync(value2, cancellationToken);
				}
				return WriteValueAsync((byte[])value, cancellationToken);
			default:
				throw global::Newtonsoft.Json.Utilities.MiscellaneousUtils.CreateArgumentOutOfRangeException("token", token, "Unexpected token type.");
			}
		}

		internal virtual async global::System.Threading.Tasks.Task WriteTokenAsync(global::Newtonsoft.Json.JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, global::System.Threading.CancellationToken cancellationToken)
		{
			int initialDepth = CalculateWriteTokenInitialDepth(reader);
			bool flag;
			do
			{
				if (writeDateConstructorAsDate && reader.TokenType == global::Newtonsoft.Json.JsonToken.StartConstructor && string.Equals(reader.Value?.ToString(), "Date", global::System.StringComparison.Ordinal))
				{
					await WriteConstructorDateAsync(reader, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				else if (writeComments || reader.TokenType != global::Newtonsoft.Json.JsonToken.Comment)
				{
					await WriteTokenAsync(reader.TokenType, reader.Value, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				flag = initialDepth - 1 < reader.Depth - (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0) && writeChildren;
				if (flag)
				{
					flag = await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			while (flag);
			if (IsWriteTokenIncomplete(reader, writeChildren, initialDepth))
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unexpected end when reading token.", null);
			}
		}

		internal async global::System.Threading.Tasks.Task WriteTokenSyncReadingAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken)
		{
			int initialDepth = CalculateWriteTokenInitialDepth(reader);
			bool flag;
			do
			{
				if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartConstructor && string.Equals(reader.Value?.ToString(), "Date", global::System.StringComparison.Ordinal))
				{
					WriteConstructorDate(reader);
				}
				else
				{
					WriteToken(reader.TokenType, reader.Value);
				}
				flag = initialDepth - 1 < reader.Depth - (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0);
				if (flag)
				{
					flag = await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			while (flag);
			if (initialDepth < CalculateWriteTokenFinalDepth(reader))
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unexpected end when reading token.", null);
			}
		}

		private async global::System.Threading.Tasks.Task WriteConstructorDateAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken)
		{
			if (!(await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unexpected end when reading date constructor.", null);
			}
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.Integer)
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unexpected token when reading date constructor. Expected Integer, got " + reader.TokenType, null);
			}
			global::System.DateTime date = global::Newtonsoft.Json.Utilities.DateTimeUtils.ConvertJavaScriptTicksToDateTime((long)reader.Value);
			if (!(await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unexpected end when reading date constructor.", null);
			}
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.EndConstructor)
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unexpected token when reading date constructor. Expected EndConstructor, got " + reader.TokenType, null);
			}
			await WriteValueAsync(date, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(bool value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(bool? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(byte value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(byte? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(byte[]? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(char value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(char? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(global::System.DateTime value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(global::System.DateTime? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(global::System.DateTimeOffset value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(global::System.DateTimeOffset? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(decimal value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(decimal? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(double value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(double? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(float value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(float? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(global::System.Guid value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(global::System.Guid? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(int value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(int? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(long value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(long? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(object? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		[global::System.CLSCompliant(false)]
		public virtual global::System.Threading.Tasks.Task WriteValueAsync(sbyte value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		[global::System.CLSCompliant(false)]
		public virtual global::System.Threading.Tasks.Task WriteValueAsync(sbyte? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(short value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(short? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(string? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(global::System.TimeSpan value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(global::System.TimeSpan? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		[global::System.CLSCompliant(false)]
		public virtual global::System.Threading.Tasks.Task WriteValueAsync(uint value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		[global::System.CLSCompliant(false)]
		public virtual global::System.Threading.Tasks.Task WriteValueAsync(uint? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		[global::System.CLSCompliant(false)]
		public virtual global::System.Threading.Tasks.Task WriteValueAsync(ulong value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		[global::System.CLSCompliant(false)]
		public virtual global::System.Threading.Tasks.Task WriteValueAsync(ulong? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteValueAsync(global::System.Uri? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		[global::System.CLSCompliant(false)]
		public virtual global::System.Threading.Tasks.Task WriteValueAsync(ushort value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		[global::System.CLSCompliant(false)]
		public virtual global::System.Threading.Tasks.Task WriteValueAsync(ushort? value, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteValue(value);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteUndefinedAsync(global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteUndefined();
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		public virtual global::System.Threading.Tasks.Task WriteWhitespaceAsync(string ws, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			WriteWhitespace(ws);
			return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
		}

		internal global::System.Threading.Tasks.Task InternalWriteValueAsync(global::Newtonsoft.Json.JsonToken token, global::System.Threading.CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			UpdateScopeWithFinishedValue();
			return AutoCompleteAsync(token, cancellationToken);
		}

		protected global::System.Threading.Tasks.Task SetWriteStateAsync(global::Newtonsoft.Json.JsonToken token, object value, global::System.Threading.CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return global::Newtonsoft.Json.Utilities.AsyncUtils.FromCanceled(cancellationToken);
			}
			switch (token)
			{
			case global::Newtonsoft.Json.JsonToken.StartObject:
				return InternalWriteStartAsync(token, global::Newtonsoft.Json.JsonContainerType.Object, cancellationToken);
			case global::Newtonsoft.Json.JsonToken.StartArray:
				return InternalWriteStartAsync(token, global::Newtonsoft.Json.JsonContainerType.Array, cancellationToken);
			case global::Newtonsoft.Json.JsonToken.StartConstructor:
				return InternalWriteStartAsync(token, global::Newtonsoft.Json.JsonContainerType.Constructor, cancellationToken);
			case global::Newtonsoft.Json.JsonToken.PropertyName:
				if (!(value is string name))
				{
					throw new global::System.ArgumentException("A name is required when setting property name state.", "value");
				}
				return InternalWritePropertyNameAsync(name, cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Comment:
				return InternalWriteCommentAsync(cancellationToken);
			case global::Newtonsoft.Json.JsonToken.Raw:
				return global::Newtonsoft.Json.Utilities.AsyncUtils.CompletedTask;
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			case global::Newtonsoft.Json.JsonToken.String:
			case global::Newtonsoft.Json.JsonToken.Boolean:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.Undefined:
			case global::Newtonsoft.Json.JsonToken.Date:
			case global::Newtonsoft.Json.JsonToken.Bytes:
				return InternalWriteValueAsync(token, cancellationToken);
			case global::Newtonsoft.Json.JsonToken.EndObject:
				return InternalWriteEndAsync(global::Newtonsoft.Json.JsonContainerType.Object, cancellationToken);
			case global::Newtonsoft.Json.JsonToken.EndArray:
				return InternalWriteEndAsync(global::Newtonsoft.Json.JsonContainerType.Array, cancellationToken);
			case global::Newtonsoft.Json.JsonToken.EndConstructor:
				return InternalWriteEndAsync(global::Newtonsoft.Json.JsonContainerType.Constructor, cancellationToken);
			default:
				throw new global::System.ArgumentOutOfRangeException("token");
			}
		}

		internal static global::System.Threading.Tasks.Task WriteValueAsync(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode, object value, global::System.Threading.CancellationToken cancellationToken)
		{
			while (true)
			{
				switch (typeCode)
				{
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Char:
					return writer.WriteValueAsync((char)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.CharNullable:
					return writer.WriteValueAsync((value == null) ? ((char?)null) : new char?((char)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Boolean:
					return writer.WriteValueAsync((bool)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BooleanNullable:
					return writer.WriteValueAsync((value == null) ? ((bool?)null) : new bool?((bool)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte:
					return writer.WriteValueAsync((sbyte)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByteNullable:
					return writer.WriteValueAsync((value == null) ? ((sbyte?)null) : new sbyte?((sbyte)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16:
					return writer.WriteValueAsync((short)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16Nullable:
					return writer.WriteValueAsync((value == null) ? ((short?)null) : new short?((short)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16:
					return writer.WriteValueAsync((ushort)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16Nullable:
					return writer.WriteValueAsync((value == null) ? ((ushort?)null) : new ushort?((ushort)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32:
					return writer.WriteValueAsync((int)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32Nullable:
					return writer.WriteValueAsync((value == null) ? ((int?)null) : new int?((int)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte:
					return writer.WriteValueAsync((byte)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.ByteNullable:
					return writer.WriteValueAsync((value == null) ? ((byte?)null) : new byte?((byte)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32:
					return writer.WriteValueAsync((uint)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32Nullable:
					return writer.WriteValueAsync((value == null) ? ((uint?)null) : new uint?((uint)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64:
					return writer.WriteValueAsync((long)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64Nullable:
					return writer.WriteValueAsync((value == null) ? ((long?)null) : new long?((long)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64:
					return writer.WriteValueAsync((ulong)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64Nullable:
					return writer.WriteValueAsync((value == null) ? ((ulong?)null) : new ulong?((ulong)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Single:
					return writer.WriteValueAsync((float)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SingleNullable:
					return writer.WriteValueAsync((value == null) ? ((float?)null) : new float?((float)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Double:
					return writer.WriteValueAsync((double)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DoubleNullable:
					return writer.WriteValueAsync((value == null) ? ((double?)null) : new double?((double)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime:
					return writer.WriteValueAsync((global::System.DateTime)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeNullable:
					return writer.WriteValueAsync((value == null) ? ((global::System.DateTime?)null) : new global::System.DateTime?((global::System.DateTime)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffset:
					return writer.WriteValueAsync((global::System.DateTimeOffset)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffsetNullable:
					return writer.WriteValueAsync((value == null) ? ((global::System.DateTimeOffset?)null) : new global::System.DateTimeOffset?((global::System.DateTimeOffset)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Decimal:
					return writer.WriteValueAsync((decimal)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DecimalNullable:
					return writer.WriteValueAsync((value == null) ? ((decimal?)null) : new decimal?((decimal)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Guid:
					return writer.WriteValueAsync((global::System.Guid)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.GuidNullable:
					return writer.WriteValueAsync((value == null) ? ((global::System.Guid?)null) : new global::System.Guid?((global::System.Guid)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpan:
					return writer.WriteValueAsync((global::System.TimeSpan)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpanNullable:
					return writer.WriteValueAsync((value == null) ? ((global::System.TimeSpan?)null) : new global::System.TimeSpan?((global::System.TimeSpan)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigInteger:
					return writer.WriteValueAsync((global::System.Numerics.BigInteger)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigIntegerNullable:
					return writer.WriteValueAsync((value == null) ? ((global::System.Numerics.BigInteger?)null) : new global::System.Numerics.BigInteger?((global::System.Numerics.BigInteger)value), cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Uri:
					return writer.WriteValueAsync((global::System.Uri)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.String:
					return writer.WriteValueAsync((string)value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Bytes:
					return writer.WriteValueAsync((byte[])value, cancellationToken);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DBNull:
					return writer.WriteNullAsync(cancellationToken);
				}
				if (value is global::System.IConvertible convertible)
				{
					ResolveConvertibleValue(convertible, out typeCode, out value);
					continue;
				}
				if (value == null)
				{
					return writer.WriteNullAsync(cancellationToken);
				}
				throw CreateUnsupportedTypeException(writer, value);
			}
		}

		internal static global::Newtonsoft.Json.JsonWriter.State[][] BuildStateArray()
		{
			global::System.Collections.Generic.List<global::Newtonsoft.Json.JsonWriter.State[]> list = global::System.Linq.Enumerable.ToList(StateArrayTemplate);
			global::Newtonsoft.Json.JsonWriter.State[] item = StateArrayTemplate[0];
			global::Newtonsoft.Json.JsonWriter.State[] item2 = StateArrayTemplate[7];
			ulong[] values = global::Newtonsoft.Json.Utilities.EnumUtils.GetEnumValuesAndNames(typeof(global::Newtonsoft.Json.JsonToken)).Values;
			foreach (ulong num in values)
			{
				if (list.Count <= (int)num)
				{
					global::Newtonsoft.Json.JsonToken jsonToken = (global::Newtonsoft.Json.JsonToken)num;
					if ((uint)(jsonToken - 7) <= 5u || (uint)(jsonToken - 16) <= 1u)
					{
						list.Add(item2);
					}
					else
					{
						list.Add(item);
					}
				}
			}
			return list.ToArray();
		}

		static JsonWriter()
		{
			StateArrayTemplate = new global::Newtonsoft.Json.JsonWriter.State[8][]
			{
				new global::Newtonsoft.Json.JsonWriter.State[10]
				{
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error
				},
				new global::Newtonsoft.Json.JsonWriter.State[10]
				{
					global::Newtonsoft.Json.JsonWriter.State.ObjectStart,
					global::Newtonsoft.Json.JsonWriter.State.ObjectStart,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.ObjectStart,
					global::Newtonsoft.Json.JsonWriter.State.ObjectStart,
					global::Newtonsoft.Json.JsonWriter.State.ObjectStart,
					global::Newtonsoft.Json.JsonWriter.State.ObjectStart,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error
				},
				new global::Newtonsoft.Json.JsonWriter.State[10]
				{
					global::Newtonsoft.Json.JsonWriter.State.ArrayStart,
					global::Newtonsoft.Json.JsonWriter.State.ArrayStart,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.ArrayStart,
					global::Newtonsoft.Json.JsonWriter.State.ArrayStart,
					global::Newtonsoft.Json.JsonWriter.State.ArrayStart,
					global::Newtonsoft.Json.JsonWriter.State.ArrayStart,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error
				},
				new global::Newtonsoft.Json.JsonWriter.State[10]
				{
					global::Newtonsoft.Json.JsonWriter.State.ConstructorStart,
					global::Newtonsoft.Json.JsonWriter.State.ConstructorStart,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.ConstructorStart,
					global::Newtonsoft.Json.JsonWriter.State.ConstructorStart,
					global::Newtonsoft.Json.JsonWriter.State.ConstructorStart,
					global::Newtonsoft.Json.JsonWriter.State.ConstructorStart,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error
				},
				new global::Newtonsoft.Json.JsonWriter.State[10]
				{
					global::Newtonsoft.Json.JsonWriter.State.Property,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Property,
					global::Newtonsoft.Json.JsonWriter.State.Property,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error
				},
				new global::Newtonsoft.Json.JsonWriter.State[10]
				{
					global::Newtonsoft.Json.JsonWriter.State.Start,
					global::Newtonsoft.Json.JsonWriter.State.Property,
					global::Newtonsoft.Json.JsonWriter.State.ObjectStart,
					global::Newtonsoft.Json.JsonWriter.State.Object,
					global::Newtonsoft.Json.JsonWriter.State.ArrayStart,
					global::Newtonsoft.Json.JsonWriter.State.Array,
					global::Newtonsoft.Json.JsonWriter.State.Constructor,
					global::Newtonsoft.Json.JsonWriter.State.Constructor,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error
				},
				new global::Newtonsoft.Json.JsonWriter.State[10]
				{
					global::Newtonsoft.Json.JsonWriter.State.Start,
					global::Newtonsoft.Json.JsonWriter.State.Property,
					global::Newtonsoft.Json.JsonWriter.State.ObjectStart,
					global::Newtonsoft.Json.JsonWriter.State.Object,
					global::Newtonsoft.Json.JsonWriter.State.ArrayStart,
					global::Newtonsoft.Json.JsonWriter.State.Array,
					global::Newtonsoft.Json.JsonWriter.State.Constructor,
					global::Newtonsoft.Json.JsonWriter.State.Constructor,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error
				},
				new global::Newtonsoft.Json.JsonWriter.State[10]
				{
					global::Newtonsoft.Json.JsonWriter.State.Start,
					global::Newtonsoft.Json.JsonWriter.State.Object,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Array,
					global::Newtonsoft.Json.JsonWriter.State.Array,
					global::Newtonsoft.Json.JsonWriter.State.Constructor,
					global::Newtonsoft.Json.JsonWriter.State.Constructor,
					global::Newtonsoft.Json.JsonWriter.State.Error,
					global::Newtonsoft.Json.JsonWriter.State.Error
				}
			};
			StateArray = BuildStateArray();
		}

		internal virtual void OnStringEscapeHandlingChanged()
		{
		}

		protected JsonWriter()
		{
			_currentState = global::Newtonsoft.Json.JsonWriter.State.Start;
			_formatting = global::Newtonsoft.Json.Formatting.None;
			_dateTimeZoneHandling = global::Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind;
			CloseOutput = true;
			AutoCompleteOnClose = true;
		}

		internal void UpdateScopeWithFinishedValue()
		{
			if (_currentPosition.HasIndex)
			{
				_currentPosition.Position++;
			}
		}

		private void Push(global::Newtonsoft.Json.JsonContainerType value)
		{
			if (_currentPosition.Type != global::Newtonsoft.Json.JsonContainerType.None)
			{
				if (_stack == null)
				{
					_stack = new global::System.Collections.Generic.List<global::Newtonsoft.Json.JsonPosition>();
				}
				_stack.Add(_currentPosition);
			}
			_currentPosition = new global::Newtonsoft.Json.JsonPosition(value);
		}

		private global::Newtonsoft.Json.JsonContainerType Pop()
		{
			global::Newtonsoft.Json.JsonPosition currentPosition = _currentPosition;
			if (_stack != null && _stack.Count > 0)
			{
				_currentPosition = _stack[_stack.Count - 1];
				_stack.RemoveAt(_stack.Count - 1);
			}
			else
			{
				_currentPosition = default(global::Newtonsoft.Json.JsonPosition);
			}
			return currentPosition.Type;
		}

		private global::Newtonsoft.Json.JsonContainerType Peek()
		{
			return _currentPosition.Type;
		}

		public abstract void Flush();

		public virtual void Close()
		{
			if (AutoCompleteOnClose)
			{
				AutoCompleteAll();
			}
		}

		public virtual void WriteStartObject()
		{
			InternalWriteStart(global::Newtonsoft.Json.JsonToken.StartObject, global::Newtonsoft.Json.JsonContainerType.Object);
		}

		public virtual void WriteEndObject()
		{
			InternalWriteEnd(global::Newtonsoft.Json.JsonContainerType.Object);
		}

		public virtual void WriteStartArray()
		{
			InternalWriteStart(global::Newtonsoft.Json.JsonToken.StartArray, global::Newtonsoft.Json.JsonContainerType.Array);
		}

		public virtual void WriteEndArray()
		{
			InternalWriteEnd(global::Newtonsoft.Json.JsonContainerType.Array);
		}

		public virtual void WriteStartConstructor(string name)
		{
			InternalWriteStart(global::Newtonsoft.Json.JsonToken.StartConstructor, global::Newtonsoft.Json.JsonContainerType.Constructor);
		}

		public virtual void WriteEndConstructor()
		{
			InternalWriteEnd(global::Newtonsoft.Json.JsonContainerType.Constructor);
		}

		public virtual void WritePropertyName(string name)
		{
			InternalWritePropertyName(name);
		}

		public virtual void WritePropertyName(string name, bool escape)
		{
			WritePropertyName(name);
		}

		public virtual void WriteEnd()
		{
			WriteEnd(Peek());
		}

		public void WriteToken(global::Newtonsoft.Json.JsonReader reader)
		{
			WriteToken(reader, writeChildren: true);
		}

		public void WriteToken(global::Newtonsoft.Json.JsonReader reader, bool writeChildren)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			WriteToken(reader, writeChildren, writeDateConstructorAsDate: true, writeComments: true);
		}

		public void WriteToken(global::Newtonsoft.Json.JsonToken token, object? value)
		{
			switch (token)
			{
			case global::Newtonsoft.Json.JsonToken.StartObject:
				WriteStartObject();
				break;
			case global::Newtonsoft.Json.JsonToken.StartArray:
				WriteStartArray();
				break;
			case global::Newtonsoft.Json.JsonToken.StartConstructor:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				WriteStartConstructor(value.ToString());
				break;
			case global::Newtonsoft.Json.JsonToken.PropertyName:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				WritePropertyName(value.ToString());
				break;
			case global::Newtonsoft.Json.JsonToken.Comment:
				WriteComment(value?.ToString());
				break;
			case global::Newtonsoft.Json.JsonToken.Integer:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				if (value is global::System.Numerics.BigInteger bigInteger)
				{
					WriteValue(bigInteger);
				}
				else
				{
					WriteValue(global::System.Convert.ToInt64(value, global::System.Globalization.CultureInfo.InvariantCulture));
				}
				break;
			case global::Newtonsoft.Json.JsonToken.Float:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				if (value is decimal value3)
				{
					WriteValue(value3);
				}
				else if (value is double value4)
				{
					WriteValue(value4);
				}
				else if (value is float value5)
				{
					WriteValue(value5);
				}
				else
				{
					WriteValue(global::System.Convert.ToDouble(value, global::System.Globalization.CultureInfo.InvariantCulture));
				}
				break;
			case global::Newtonsoft.Json.JsonToken.String:
				WriteValue(value?.ToString());
				break;
			case global::Newtonsoft.Json.JsonToken.Boolean:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				WriteValue(global::System.Convert.ToBoolean(value, global::System.Globalization.CultureInfo.InvariantCulture));
				break;
			case global::Newtonsoft.Json.JsonToken.Null:
				WriteNull();
				break;
			case global::Newtonsoft.Json.JsonToken.Undefined:
				WriteUndefined();
				break;
			case global::Newtonsoft.Json.JsonToken.EndObject:
				WriteEndObject();
				break;
			case global::Newtonsoft.Json.JsonToken.EndArray:
				WriteEndArray();
				break;
			case global::Newtonsoft.Json.JsonToken.EndConstructor:
				WriteEndConstructor();
				break;
			case global::Newtonsoft.Json.JsonToken.Date:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				if (value is global::System.DateTimeOffset value6)
				{
					WriteValue(value6);
				}
				else
				{
					WriteValue(global::System.Convert.ToDateTime(value, global::System.Globalization.CultureInfo.InvariantCulture));
				}
				break;
			case global::Newtonsoft.Json.JsonToken.Raw:
				WriteRawValue(value?.ToString());
				break;
			case global::Newtonsoft.Json.JsonToken.Bytes:
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(value, "value");
				if (value is global::System.Guid value2)
				{
					WriteValue(value2);
				}
				else
				{
					WriteValue((byte[])value);
				}
				break;
			default:
				throw global::Newtonsoft.Json.Utilities.MiscellaneousUtils.CreateArgumentOutOfRangeException("token", token, "Unexpected token type.");
			case global::Newtonsoft.Json.JsonToken.None:
				break;
			}
		}

		public void WriteToken(global::Newtonsoft.Json.JsonToken token)
		{
			WriteToken(token, null);
		}

		internal virtual void WriteToken(global::Newtonsoft.Json.JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			int num = CalculateWriteTokenInitialDepth(reader);
			do
			{
				if (writeDateConstructorAsDate && reader.TokenType == global::Newtonsoft.Json.JsonToken.StartConstructor && string.Equals(reader.Value?.ToString(), "Date", global::System.StringComparison.Ordinal))
				{
					WriteConstructorDate(reader);
				}
				else if (writeComments || reader.TokenType != global::Newtonsoft.Json.JsonToken.Comment)
				{
					WriteToken(reader.TokenType, reader.Value);
				}
			}
			while (num - 1 < reader.Depth - (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0) && writeChildren && reader.Read());
			if (IsWriteTokenIncomplete(reader, writeChildren, num))
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unexpected end when reading token.", null);
			}
		}

		private bool IsWriteTokenIncomplete(global::Newtonsoft.Json.JsonReader reader, bool writeChildren, int initialDepth)
		{
			int num = CalculateWriteTokenFinalDepth(reader);
			if (initialDepth >= num)
			{
				if (writeChildren && initialDepth == num)
				{
					return global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsStartToken(reader.TokenType);
				}
				return false;
			}
			return true;
		}

		private int CalculateWriteTokenInitialDepth(global::Newtonsoft.Json.JsonReader reader)
		{
			global::Newtonsoft.Json.JsonToken tokenType = reader.TokenType;
			if (tokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				return -1;
			}
			if (!global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsStartToken(tokenType))
			{
				return reader.Depth + 1;
			}
			return reader.Depth;
		}

		private int CalculateWriteTokenFinalDepth(global::Newtonsoft.Json.JsonReader reader)
		{
			global::Newtonsoft.Json.JsonToken tokenType = reader.TokenType;
			if (tokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				return -1;
			}
			if (!global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsEndToken(tokenType))
			{
				return reader.Depth;
			}
			return reader.Depth - 1;
		}

		private void WriteConstructorDate(global::Newtonsoft.Json.JsonReader reader)
		{
			if (!global::Newtonsoft.Json.Utilities.JavaScriptUtils.TryGetDateFromConstructorJson(reader, out global::System.DateTime dateTime, out string errorMessage))
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, errorMessage, null);
			}
			WriteValue(dateTime);
		}

		private void WriteEnd(global::Newtonsoft.Json.JsonContainerType type)
		{
			switch (type)
			{
			case global::Newtonsoft.Json.JsonContainerType.Object:
				WriteEndObject();
				break;
			case global::Newtonsoft.Json.JsonContainerType.Array:
				WriteEndArray();
				break;
			case global::Newtonsoft.Json.JsonContainerType.Constructor:
				WriteEndConstructor();
				break;
			default:
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unexpected type when writing end: " + type, null);
			}
		}

		private void AutoCompleteAll()
		{
			while (Top > 0)
			{
				WriteEnd();
			}
		}

		private global::Newtonsoft.Json.JsonToken GetCloseTokenForType(global::Newtonsoft.Json.JsonContainerType type)
		{
			return type switch
			{
				global::Newtonsoft.Json.JsonContainerType.Object => global::Newtonsoft.Json.JsonToken.EndObject, 
				global::Newtonsoft.Json.JsonContainerType.Array => global::Newtonsoft.Json.JsonToken.EndArray, 
				global::Newtonsoft.Json.JsonContainerType.Constructor => global::Newtonsoft.Json.JsonToken.EndConstructor, 
				_ => throw global::Newtonsoft.Json.JsonWriterException.Create(this, "No close token for type: " + type, null), 
			};
		}

		private void AutoCompleteClose(global::Newtonsoft.Json.JsonContainerType type)
		{
			int num = CalculateLevelsToComplete(type);
			for (int i = 0; i < num; i++)
			{
				global::Newtonsoft.Json.JsonToken closeTokenForType = GetCloseTokenForType(Pop());
				if (_currentState == global::Newtonsoft.Json.JsonWriter.State.Property)
				{
					WriteNull();
				}
				if (_formatting == global::Newtonsoft.Json.Formatting.Indented && _currentState != global::Newtonsoft.Json.JsonWriter.State.ObjectStart && _currentState != global::Newtonsoft.Json.JsonWriter.State.ArrayStart)
				{
					WriteIndent();
				}
				WriteEnd(closeTokenForType);
				UpdateCurrentState();
			}
		}

		private int CalculateLevelsToComplete(global::Newtonsoft.Json.JsonContainerType type)
		{
			int num = 0;
			if (_currentPosition.Type == type)
			{
				num = 1;
			}
			else
			{
				int num2 = Top - 2;
				for (int num3 = num2; num3 >= 0; num3--)
				{
					int index = num2 - num3;
					if (_stack[index].Type == type)
					{
						num = num3 + 2;
						break;
					}
				}
			}
			if (num == 0)
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "No token to close.", null);
			}
			return num;
		}

		private void UpdateCurrentState()
		{
			global::Newtonsoft.Json.JsonContainerType jsonContainerType = Peek();
			switch (jsonContainerType)
			{
			case global::Newtonsoft.Json.JsonContainerType.Object:
				_currentState = global::Newtonsoft.Json.JsonWriter.State.Object;
				break;
			case global::Newtonsoft.Json.JsonContainerType.Array:
				_currentState = global::Newtonsoft.Json.JsonWriter.State.Array;
				break;
			case global::Newtonsoft.Json.JsonContainerType.Constructor:
				_currentState = global::Newtonsoft.Json.JsonWriter.State.Array;
				break;
			case global::Newtonsoft.Json.JsonContainerType.None:
				_currentState = global::Newtonsoft.Json.JsonWriter.State.Start;
				break;
			default:
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Unknown JsonType: " + jsonContainerType, null);
			}
		}

		protected virtual void WriteEnd(global::Newtonsoft.Json.JsonToken token)
		{
		}

		protected virtual void WriteIndent()
		{
		}

		protected virtual void WriteValueDelimiter()
		{
		}

		protected virtual void WriteIndentSpace()
		{
		}

		internal void AutoComplete(global::Newtonsoft.Json.JsonToken tokenBeingWritten)
		{
			global::Newtonsoft.Json.JsonWriter.State state = StateArray[(int)tokenBeingWritten][(int)_currentState];
			if (state == global::Newtonsoft.Json.JsonWriter.State.Error)
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Token {0} in state {1} would result in an invalid JSON object.", global::System.Globalization.CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), _currentState.ToString()), null);
			}
			if ((_currentState == global::Newtonsoft.Json.JsonWriter.State.Object || _currentState == global::Newtonsoft.Json.JsonWriter.State.Array || _currentState == global::Newtonsoft.Json.JsonWriter.State.Constructor) && tokenBeingWritten != global::Newtonsoft.Json.JsonToken.Comment)
			{
				WriteValueDelimiter();
			}
			if (_formatting == global::Newtonsoft.Json.Formatting.Indented)
			{
				if (_currentState == global::Newtonsoft.Json.JsonWriter.State.Property)
				{
					WriteIndentSpace();
				}
				if (_currentState == global::Newtonsoft.Json.JsonWriter.State.Array || _currentState == global::Newtonsoft.Json.JsonWriter.State.ArrayStart || _currentState == global::Newtonsoft.Json.JsonWriter.State.Constructor || _currentState == global::Newtonsoft.Json.JsonWriter.State.ConstructorStart || (tokenBeingWritten == global::Newtonsoft.Json.JsonToken.PropertyName && _currentState != global::Newtonsoft.Json.JsonWriter.State.Start))
				{
					WriteIndent();
				}
			}
			_currentState = state;
		}

		public virtual void WriteNull()
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Null);
		}

		public virtual void WriteUndefined()
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Undefined);
		}

		public virtual void WriteRaw(string? json)
		{
			InternalWriteRaw();
		}

		public virtual void WriteRawValue(string? json)
		{
			UpdateScopeWithFinishedValue();
			AutoComplete(global::Newtonsoft.Json.JsonToken.Undefined);
			WriteRaw(json);
		}

		public virtual void WriteValue(string? value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
		}

		public virtual void WriteValue(int value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
		}

		[global::System.CLSCompliant(false)]
		public virtual void WriteValue(uint value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
		}

		public virtual void WriteValue(long value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
		}

		[global::System.CLSCompliant(false)]
		public virtual void WriteValue(ulong value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
		}

		public virtual void WriteValue(float value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Float);
		}

		public virtual void WriteValue(double value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Float);
		}

		public virtual void WriteValue(bool value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Boolean);
		}

		public virtual void WriteValue(short value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
		}

		[global::System.CLSCompliant(false)]
		public virtual void WriteValue(ushort value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
		}

		public virtual void WriteValue(char value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
		}

		public virtual void WriteValue(byte value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
		}

		[global::System.CLSCompliant(false)]
		public virtual void WriteValue(sbyte value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Integer);
		}

		public virtual void WriteValue(decimal value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Float);
		}

		public virtual void WriteValue(global::System.DateTime value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Date);
		}

		public virtual void WriteValue(global::System.DateTimeOffset value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.Date);
		}

		public virtual void WriteValue(global::System.Guid value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
		}

		public virtual void WriteValue(global::System.TimeSpan value)
		{
			InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
		}

		public virtual void WriteValue(int? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		[global::System.CLSCompliant(false)]
		public virtual void WriteValue(uint? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(long? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		[global::System.CLSCompliant(false)]
		public virtual void WriteValue(ulong? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(float? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(double? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(bool? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value == true);
			}
		}

		public virtual void WriteValue(short? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		[global::System.CLSCompliant(false)]
		public virtual void WriteValue(ushort? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(char? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(byte? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		[global::System.CLSCompliant(false)]
		public virtual void WriteValue(sbyte? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(decimal? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(global::System.DateTime? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(global::System.DateTimeOffset? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(global::System.Guid? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(global::System.TimeSpan? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
			}
			else
			{
				WriteValue(value.GetValueOrDefault());
			}
		}

		public virtual void WriteValue(byte[]? value)
		{
			if (value == null)
			{
				WriteNull();
			}
			else
			{
				InternalWriteValue(global::Newtonsoft.Json.JsonToken.Bytes);
			}
		}

		public virtual void WriteValue(global::System.Uri? value)
		{
			if (value == null)
			{
				WriteNull();
			}
			else
			{
				InternalWriteValue(global::Newtonsoft.Json.JsonToken.String);
			}
		}

		public virtual void WriteValue(object? value)
		{
			if (value == null)
			{
				WriteNull();
				return;
			}
			if (value is global::System.Numerics.BigInteger)
			{
				throw CreateUnsupportedTypeException(this, value);
			}
			WriteValue(this, global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeCode(value.GetType()), value);
		}

		public virtual void WriteComment(string? text)
		{
			InternalWriteComment();
		}

		public virtual void WriteWhitespace(string ws)
		{
			InternalWriteWhitespace(ws);
		}

		void global::System.IDisposable.Dispose()
		{
			Dispose(disposing: true);
			global::System.GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_currentState != global::Newtonsoft.Json.JsonWriter.State.Closed && disposing)
			{
				Close();
			}
		}

		internal static void WriteValue(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode, object value)
		{
			while (true)
			{
				switch (typeCode)
				{
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Char:
					writer.WriteValue((char)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.CharNullable:
					writer.WriteValue((value == null) ? ((char?)null) : new char?((char)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Boolean:
					writer.WriteValue((bool)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BooleanNullable:
					writer.WriteValue((value == null) ? ((bool?)null) : new bool?((bool)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte:
					writer.WriteValue((sbyte)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByteNullable:
					writer.WriteValue((value == null) ? ((sbyte?)null) : new sbyte?((sbyte)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16:
					writer.WriteValue((short)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16Nullable:
					writer.WriteValue((value == null) ? ((short?)null) : new short?((short)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16:
					writer.WriteValue((ushort)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16Nullable:
					writer.WriteValue((value == null) ? ((ushort?)null) : new ushort?((ushort)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32:
					writer.WriteValue((int)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32Nullable:
					writer.WriteValue((value == null) ? ((int?)null) : new int?((int)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte:
					writer.WriteValue((byte)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.ByteNullable:
					writer.WriteValue((value == null) ? ((byte?)null) : new byte?((byte)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32:
					writer.WriteValue((uint)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32Nullable:
					writer.WriteValue((value == null) ? ((uint?)null) : new uint?((uint)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64:
					writer.WriteValue((long)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64Nullable:
					writer.WriteValue((value == null) ? ((long?)null) : new long?((long)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64:
					writer.WriteValue((ulong)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64Nullable:
					writer.WriteValue((value == null) ? ((ulong?)null) : new ulong?((ulong)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Single:
					writer.WriteValue((float)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SingleNullable:
					writer.WriteValue((value == null) ? ((float?)null) : new float?((float)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Double:
					writer.WriteValue((double)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DoubleNullable:
					writer.WriteValue((value == null) ? ((double?)null) : new double?((double)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime:
					writer.WriteValue((global::System.DateTime)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeNullable:
					writer.WriteValue((value == null) ? ((global::System.DateTime?)null) : new global::System.DateTime?((global::System.DateTime)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffset:
					writer.WriteValue((global::System.DateTimeOffset)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffsetNullable:
					writer.WriteValue((value == null) ? ((global::System.DateTimeOffset?)null) : new global::System.DateTimeOffset?((global::System.DateTimeOffset)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Decimal:
					writer.WriteValue((decimal)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DecimalNullable:
					writer.WriteValue((value == null) ? ((decimal?)null) : new decimal?((decimal)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Guid:
					writer.WriteValue((global::System.Guid)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.GuidNullable:
					writer.WriteValue((value == null) ? ((global::System.Guid?)null) : new global::System.Guid?((global::System.Guid)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpan:
					writer.WriteValue((global::System.TimeSpan)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpanNullable:
					writer.WriteValue((value == null) ? ((global::System.TimeSpan?)null) : new global::System.TimeSpan?((global::System.TimeSpan)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigInteger:
					writer.WriteValue((global::System.Numerics.BigInteger)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigIntegerNullable:
					writer.WriteValue((value == null) ? ((global::System.Numerics.BigInteger?)null) : new global::System.Numerics.BigInteger?((global::System.Numerics.BigInteger)value));
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Uri:
					writer.WriteValue((global::System.Uri)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.String:
					writer.WriteValue((string)value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Bytes:
					writer.WriteValue((byte[])value);
					return;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DBNull:
					writer.WriteNull();
					return;
				}
				if (value is global::System.IConvertible convertible)
				{
					ResolveConvertibleValue(convertible, out typeCode, out value);
					continue;
				}
				if (value == null)
				{
					writer.WriteNull();
					return;
				}
				throw CreateUnsupportedTypeException(writer, value);
			}
		}

		private static void ResolveConvertibleValue(global::System.IConvertible convertible, out global::Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode, out object value)
		{
			global::Newtonsoft.Json.Utilities.TypeInformation typeInformation = global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeInformation(convertible);
			typeCode = ((typeInformation.TypeCode == global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Object) ? global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.String : typeInformation.TypeCode);
			global::System.Type conversionType = ((typeInformation.TypeCode == global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Object) ? typeof(string) : typeInformation.Type);
			value = convertible.ToType(conversionType, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		private static global::Newtonsoft.Json.JsonWriterException CreateUnsupportedTypeException(global::Newtonsoft.Json.JsonWriter writer, object value)
		{
			return global::Newtonsoft.Json.JsonWriterException.Create(writer, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.", global::System.Globalization.CultureInfo.InvariantCulture, value.GetType()), null);
		}

		protected void SetWriteState(global::Newtonsoft.Json.JsonToken token, object value)
		{
			switch (token)
			{
			case global::Newtonsoft.Json.JsonToken.StartObject:
				InternalWriteStart(token, global::Newtonsoft.Json.JsonContainerType.Object);
				break;
			case global::Newtonsoft.Json.JsonToken.StartArray:
				InternalWriteStart(token, global::Newtonsoft.Json.JsonContainerType.Array);
				break;
			case global::Newtonsoft.Json.JsonToken.StartConstructor:
				InternalWriteStart(token, global::Newtonsoft.Json.JsonContainerType.Constructor);
				break;
			case global::Newtonsoft.Json.JsonToken.PropertyName:
				if (!(value is string name))
				{
					throw new global::System.ArgumentException("A name is required when setting property name state.", "value");
				}
				InternalWritePropertyName(name);
				break;
			case global::Newtonsoft.Json.JsonToken.Comment:
				InternalWriteComment();
				break;
			case global::Newtonsoft.Json.JsonToken.Raw:
				InternalWriteRaw();
				break;
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			case global::Newtonsoft.Json.JsonToken.String:
			case global::Newtonsoft.Json.JsonToken.Boolean:
			case global::Newtonsoft.Json.JsonToken.Null:
			case global::Newtonsoft.Json.JsonToken.Undefined:
			case global::Newtonsoft.Json.JsonToken.Date:
			case global::Newtonsoft.Json.JsonToken.Bytes:
				InternalWriteValue(token);
				break;
			case global::Newtonsoft.Json.JsonToken.EndObject:
				InternalWriteEnd(global::Newtonsoft.Json.JsonContainerType.Object);
				break;
			case global::Newtonsoft.Json.JsonToken.EndArray:
				InternalWriteEnd(global::Newtonsoft.Json.JsonContainerType.Array);
				break;
			case global::Newtonsoft.Json.JsonToken.EndConstructor:
				InternalWriteEnd(global::Newtonsoft.Json.JsonContainerType.Constructor);
				break;
			default:
				throw new global::System.ArgumentOutOfRangeException("token");
			}
		}

		internal void InternalWriteEnd(global::Newtonsoft.Json.JsonContainerType container)
		{
			AutoCompleteClose(container);
		}

		internal void InternalWritePropertyName(string name)
		{
			_currentPosition.PropertyName = name;
			AutoComplete(global::Newtonsoft.Json.JsonToken.PropertyName);
		}

		internal void InternalWriteRaw()
		{
		}

		internal void InternalWriteStart(global::Newtonsoft.Json.JsonToken token, global::Newtonsoft.Json.JsonContainerType container)
		{
			UpdateScopeWithFinishedValue();
			AutoComplete(token);
			Push(container);
		}

		internal void InternalWriteValue(global::Newtonsoft.Json.JsonToken token)
		{
			UpdateScopeWithFinishedValue();
			AutoComplete(token);
		}

		internal void InternalWriteWhitespace(string ws)
		{
			if (ws != null && !global::Newtonsoft.Json.Utilities.StringUtils.IsWhiteSpace(ws))
			{
				throw global::Newtonsoft.Json.JsonWriterException.Create(this, "Only white space characters should be used.", null);
			}
		}

		internal void InternalWriteComment()
		{
			AutoComplete(global::Newtonsoft.Json.JsonToken.Comment);
		}
	}
}
