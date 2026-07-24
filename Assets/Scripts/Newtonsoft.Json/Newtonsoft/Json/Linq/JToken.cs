namespace Newtonsoft.Json.Linq
{
	public abstract class JToken : global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.IEnumerable, global::Newtonsoft.Json.IJsonLineInfo, global::System.ICloneable, global::System.Dynamic.IDynamicMetaObjectProvider
	{
		private class LineInfoAnnotation
		{
			internal readonly int LineNumber;

			internal readonly int LinePosition;

			public LineInfoAnnotation(int lineNumber, int linePosition)
			{
				LineNumber = lineNumber;
				LinePosition = linePosition;
			}
		}

		private static global::Newtonsoft.Json.Linq.JTokenEqualityComparer? _equalityComparer;

		private global::Newtonsoft.Json.Linq.JContainer? _parent;

		private global::Newtonsoft.Json.Linq.JToken? _previous;

		private global::Newtonsoft.Json.Linq.JToken? _next;

		private object? _annotations;

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] BooleanTypes = new global::Newtonsoft.Json.Linq.JTokenType[6]
		{
			global::Newtonsoft.Json.Linq.JTokenType.Integer,
			global::Newtonsoft.Json.Linq.JTokenType.Float,
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw,
			global::Newtonsoft.Json.Linq.JTokenType.Boolean
		};

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] NumberTypes = new global::Newtonsoft.Json.Linq.JTokenType[6]
		{
			global::Newtonsoft.Json.Linq.JTokenType.Integer,
			global::Newtonsoft.Json.Linq.JTokenType.Float,
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw,
			global::Newtonsoft.Json.Linq.JTokenType.Boolean
		};

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] BigIntegerTypes = new global::Newtonsoft.Json.Linq.JTokenType[7]
		{
			global::Newtonsoft.Json.Linq.JTokenType.Integer,
			global::Newtonsoft.Json.Linq.JTokenType.Float,
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw,
			global::Newtonsoft.Json.Linq.JTokenType.Boolean,
			global::Newtonsoft.Json.Linq.JTokenType.Bytes
		};

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] StringTypes = new global::Newtonsoft.Json.Linq.JTokenType[11]
		{
			global::Newtonsoft.Json.Linq.JTokenType.Date,
			global::Newtonsoft.Json.Linq.JTokenType.Integer,
			global::Newtonsoft.Json.Linq.JTokenType.Float,
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw,
			global::Newtonsoft.Json.Linq.JTokenType.Boolean,
			global::Newtonsoft.Json.Linq.JTokenType.Bytes,
			global::Newtonsoft.Json.Linq.JTokenType.Guid,
			global::Newtonsoft.Json.Linq.JTokenType.TimeSpan,
			global::Newtonsoft.Json.Linq.JTokenType.Uri
		};

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] GuidTypes = new global::Newtonsoft.Json.Linq.JTokenType[5]
		{
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw,
			global::Newtonsoft.Json.Linq.JTokenType.Guid,
			global::Newtonsoft.Json.Linq.JTokenType.Bytes
		};

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] TimeSpanTypes = new global::Newtonsoft.Json.Linq.JTokenType[4]
		{
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw,
			global::Newtonsoft.Json.Linq.JTokenType.TimeSpan
		};

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] UriTypes = new global::Newtonsoft.Json.Linq.JTokenType[4]
		{
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw,
			global::Newtonsoft.Json.Linq.JTokenType.Uri
		};

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] CharTypes = new global::Newtonsoft.Json.Linq.JTokenType[5]
		{
			global::Newtonsoft.Json.Linq.JTokenType.Integer,
			global::Newtonsoft.Json.Linq.JTokenType.Float,
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw
		};

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] DateTimeTypes = new global::Newtonsoft.Json.Linq.JTokenType[4]
		{
			global::Newtonsoft.Json.Linq.JTokenType.Date,
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw
		};

		private static readonly global::Newtonsoft.Json.Linq.JTokenType[] BytesTypes = new global::Newtonsoft.Json.Linq.JTokenType[5]
		{
			global::Newtonsoft.Json.Linq.JTokenType.Bytes,
			global::Newtonsoft.Json.Linq.JTokenType.String,
			global::Newtonsoft.Json.Linq.JTokenType.Comment,
			global::Newtonsoft.Json.Linq.JTokenType.Raw,
			global::Newtonsoft.Json.Linq.JTokenType.Integer
		};

		public static global::Newtonsoft.Json.Linq.JTokenEqualityComparer EqualityComparer
		{
			get
			{
				if (_equalityComparer == null)
				{
					_equalityComparer = new global::Newtonsoft.Json.Linq.JTokenEqualityComparer();
				}
				return _equalityComparer;
			}
		}

		public global::Newtonsoft.Json.Linq.JContainer? Parent
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return _parent;
			}
			internal set
			{
				_parent = value;
			}
		}

		public global::Newtonsoft.Json.Linq.JToken Root
		{
			get
			{
				global::Newtonsoft.Json.Linq.JContainer parent = Parent;
				if (parent == null)
				{
					return this;
				}
				while (parent.Parent != null)
				{
					parent = parent.Parent;
				}
				return parent;
			}
		}

		public abstract global::Newtonsoft.Json.Linq.JTokenType Type { get; }

		public abstract bool HasValues { get; }

		public global::Newtonsoft.Json.Linq.JToken? Next
		{
			get
			{
				return _next;
			}
			internal set
			{
				_next = value;
			}
		}

		public global::Newtonsoft.Json.Linq.JToken? Previous
		{
			get
			{
				return _previous;
			}
			internal set
			{
				_previous = value;
			}
		}

		public string Path
		{
			get
			{
				if (Parent == null)
				{
					return string.Empty;
				}
				global::System.Collections.Generic.List<global::Newtonsoft.Json.JsonPosition> list = new global::System.Collections.Generic.List<global::Newtonsoft.Json.JsonPosition>();
				global::Newtonsoft.Json.Linq.JToken jToken = null;
				for (global::Newtonsoft.Json.Linq.JToken jToken2 = this; jToken2 != null; jToken2 = jToken2.Parent)
				{
					switch (jToken2.Type)
					{
					case global::Newtonsoft.Json.Linq.JTokenType.Property:
					{
						global::Newtonsoft.Json.Linq.JProperty jProperty = (global::Newtonsoft.Json.Linq.JProperty)jToken2;
						list.Add(new global::Newtonsoft.Json.JsonPosition(global::Newtonsoft.Json.JsonContainerType.Object)
						{
							PropertyName = jProperty.Name
						});
						break;
					}
					case global::Newtonsoft.Json.Linq.JTokenType.Array:
					case global::Newtonsoft.Json.Linq.JTokenType.Constructor:
						if (jToken != null)
						{
							int position = ((global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken>)jToken2).IndexOf(jToken);
							list.Add(new global::Newtonsoft.Json.JsonPosition(global::Newtonsoft.Json.JsonContainerType.Array)
							{
								Position = position
							});
						}
						break;
					}
					jToken = jToken2;
				}
				global::Newtonsoft.Json.Utilities.CollectionUtils.FastReverse(list);
				return global::Newtonsoft.Json.JsonPosition.BuildPath(list, null);
			}
		}

		public virtual global::Newtonsoft.Json.Linq.JToken? this[object key]
		{
			get
			{
				throw new global::System.InvalidOperationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot access child value on {0}.", global::System.Globalization.CultureInfo.InvariantCulture, GetType()));
			}
			set
			{
				throw new global::System.InvalidOperationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot set child value on {0}.", global::System.Globalization.CultureInfo.InvariantCulture, GetType()));
			}
		}

		public virtual global::Newtonsoft.Json.Linq.JToken? First
		{
			get
			{
				throw new global::System.InvalidOperationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot access child value on {0}.", global::System.Globalization.CultureInfo.InvariantCulture, GetType()));
			}
		}

		public virtual global::Newtonsoft.Json.Linq.JToken? Last
		{
			get
			{
				throw new global::System.InvalidOperationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot access child value on {0}.", global::System.Globalization.CultureInfo.InvariantCulture, GetType()));
			}
		}

		global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken>.this[object key] => this[key];

		int global::Newtonsoft.Json.IJsonLineInfo.LineNumber => Annotation<global::Newtonsoft.Json.Linq.JToken.LineInfoAnnotation>()?.LineNumber ?? 0;

		int global::Newtonsoft.Json.IJsonLineInfo.LinePosition => Annotation<global::Newtonsoft.Json.Linq.JToken.LineInfoAnnotation>()?.LinePosition ?? 0;

		public virtual global::System.Threading.Tasks.Task WriteToAsync(global::Newtonsoft.Json.JsonWriter writer, global::System.Threading.CancellationToken cancellationToken, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			throw new global::System.NotImplementedException();
		}

		public global::System.Threading.Tasks.Task WriteToAsync(global::Newtonsoft.Json.JsonWriter writer, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			return WriteToAsync(writer, default(global::System.Threading.CancellationToken), converters);
		}

		public static global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JToken> ReadFromAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return ReadFromAsync(reader, null, cancellationToken);
		}

		public static async global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JToken> ReadFromAsync(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !(await ((settings != null && settings.CommentHandling == global::Newtonsoft.Json.Linq.CommentHandling.Ignore) ? reader.ReadAndMoveToContentAsync(cancellationToken) : reader.ReadAsync(cancellationToken)).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JToken from JsonReader.");
			}
			global::Newtonsoft.Json.IJsonLineInfo lineInfo = reader as global::Newtonsoft.Json.IJsonLineInfo;
			switch (reader.TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.StartObject:
				return await global::Newtonsoft.Json.Linq.JObject.LoadAsync(reader, settings, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			case global::Newtonsoft.Json.JsonToken.StartArray:
				return await global::Newtonsoft.Json.Linq.JArray.LoadAsync(reader, settings, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			case global::Newtonsoft.Json.JsonToken.StartConstructor:
				return await global::Newtonsoft.Json.Linq.JConstructor.LoadAsync(reader, settings, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			case global::Newtonsoft.Json.JsonToken.PropertyName:
				return await global::Newtonsoft.Json.Linq.JProperty.LoadAsync(reader, settings, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			case global::Newtonsoft.Json.JsonToken.String:
			case global::Newtonsoft.Json.JsonToken.Boolean:
			case global::Newtonsoft.Json.JsonToken.Date:
			case global::Newtonsoft.Json.JsonToken.Bytes:
			{
				global::Newtonsoft.Json.Linq.JValue jValue4 = new global::Newtonsoft.Json.Linq.JValue(reader.Value);
				jValue4.SetLineInfo(lineInfo, settings);
				return jValue4;
			}
			case global::Newtonsoft.Json.JsonToken.Comment:
			{
				global::Newtonsoft.Json.Linq.JValue jValue3 = global::Newtonsoft.Json.Linq.JValue.CreateComment(reader.Value?.ToString());
				jValue3.SetLineInfo(lineInfo, settings);
				return jValue3;
			}
			case global::Newtonsoft.Json.JsonToken.Null:
			{
				global::Newtonsoft.Json.Linq.JValue jValue2 = global::Newtonsoft.Json.Linq.JValue.CreateNull();
				jValue2.SetLineInfo(lineInfo, settings);
				return jValue2;
			}
			case global::Newtonsoft.Json.JsonToken.Undefined:
			{
				global::Newtonsoft.Json.Linq.JValue jValue = global::Newtonsoft.Json.Linq.JValue.CreateUndefined();
				jValue.SetLineInfo(lineInfo, settings);
				return jValue;
			}
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JToken from JsonReader. Unexpected token: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
		}

		public static global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JToken> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return LoadAsync(reader, null, cancellationToken);
		}

		public static global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JToken> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return ReadFromAsync(reader, settings, cancellationToken);
		}

		internal abstract global::Newtonsoft.Json.Linq.JToken CloneToken(global::Newtonsoft.Json.Linq.JsonCloneSettings? settings);

		internal abstract bool DeepEquals(global::Newtonsoft.Json.Linq.JToken node);

		public static bool DeepEquals(global::Newtonsoft.Json.Linq.JToken? t1, global::Newtonsoft.Json.Linq.JToken? t2)
		{
			if (t1 != t2)
			{
				if (t1 != null && t2 != null)
				{
					return t1.DeepEquals(t2);
				}
				return false;
			}
			return true;
		}

		internal JToken()
		{
		}

		public void AddAfterSelf(object? content)
		{
			if (_parent == null)
			{
				throw new global::System.InvalidOperationException("The parent is missing.");
			}
			int num = _parent.IndexOfItem(this);
			_parent.TryAddInternal(num + 1, content, skipParentCheck: false, copyAnnotations: true);
		}

		public void AddBeforeSelf(object? content)
		{
			if (_parent == null)
			{
				throw new global::System.InvalidOperationException("The parent is missing.");
			}
			int index = _parent.IndexOfItem(this);
			_parent.TryAddInternal(index, content, skipParentCheck: false, copyAnnotations: true);
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> Ancestors()
		{
			return GetAncestors(self: false);
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> AncestorsAndSelf()
		{
			return GetAncestors(self: true);
		}

		internal global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> GetAncestors(bool self)
		{
			for (global::Newtonsoft.Json.Linq.JToken current = (self ? this : Parent); current != null; current = current.Parent)
			{
				yield return current;
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> AfterSelf()
		{
			if (Parent != null)
			{
				for (global::Newtonsoft.Json.Linq.JToken o = Next; o != null; o = o.Next)
				{
					yield return o;
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> BeforeSelf()
		{
			if (Parent != null)
			{
				global::Newtonsoft.Json.Linq.JToken o = Parent.First;
				while (o != this && o != null)
				{
					yield return o;
					o = o.Next;
				}
			}
		}

		public virtual T? Value<T>(object key)
		{
			global::Newtonsoft.Json.Linq.JToken jToken = this[key];
			if (jToken != null)
			{
				return jToken.Convert<global::Newtonsoft.Json.Linq.JToken, T>();
			}
			return default(T);
		}

		public virtual global::Newtonsoft.Json.Linq.JEnumerable<global::Newtonsoft.Json.Linq.JToken> Children()
		{
			return global::Newtonsoft.Json.Linq.JEnumerable<global::Newtonsoft.Json.Linq.JToken>.Empty;
		}

		public global::Newtonsoft.Json.Linq.JEnumerable<T> Children<T>() where T : global::Newtonsoft.Json.Linq.JToken
		{
			return new global::Newtonsoft.Json.Linq.JEnumerable<T>(global::System.Linq.Enumerable.OfType<T>(Children()));
		}

		public virtual global::System.Collections.Generic.IEnumerable<T?> Values<T>()
		{
			throw new global::System.InvalidOperationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot access child value on {0}.", global::System.Globalization.CultureInfo.InvariantCulture, GetType()));
		}

		public void Remove()
		{
			if (_parent == null)
			{
				throw new global::System.InvalidOperationException("The parent is missing.");
			}
			_parent.RemoveItem(this);
		}

		public void Replace(global::Newtonsoft.Json.Linq.JToken value)
		{
			if (_parent == null)
			{
				throw new global::System.InvalidOperationException("The parent is missing.");
			}
			_parent.ReplaceItem(this, value);
		}

		public abstract void WriteTo(global::Newtonsoft.Json.JsonWriter writer, params global::Newtonsoft.Json.JsonConverter[] converters);

		public override string ToString()
		{
			return ToString(global::Newtonsoft.Json.Formatting.Indented);
		}

		public string ToString(global::Newtonsoft.Json.Formatting formatting, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			using global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter(global::System.Globalization.CultureInfo.InvariantCulture);
			global::Newtonsoft.Json.JsonTextWriter jsonTextWriter = new global::Newtonsoft.Json.JsonTextWriter(stringWriter);
			jsonTextWriter.Formatting = formatting;
			WriteTo(jsonTextWriter, converters);
			return stringWriter.ToString();
		}

		private static global::Newtonsoft.Json.Linq.JValue? EnsureValue(global::Newtonsoft.Json.Linq.JToken value)
		{
			if (value == null)
			{
				throw new global::System.ArgumentNullException("value");
			}
			if (value is global::Newtonsoft.Json.Linq.JProperty jProperty)
			{
				value = jProperty.Value;
			}
			return value as global::Newtonsoft.Json.Linq.JValue;
		}

		private static string GetType(global::Newtonsoft.Json.Linq.JToken token)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(token, "token");
			if (token is global::Newtonsoft.Json.Linq.JProperty jProperty)
			{
				token = jProperty.Value;
			}
			return token.Type.ToString();
		}

		private static bool ValidateToken(global::Newtonsoft.Json.Linq.JToken o, global::Newtonsoft.Json.Linq.JTokenType[] validTypes, bool nullable)
		{
			if (global::System.Array.IndexOf(validTypes, o.Type) == -1)
			{
				if (nullable)
				{
					if (o.Type != global::Newtonsoft.Json.Linq.JTokenType.Null)
					{
						return o.Type == global::Newtonsoft.Json.Linq.JTokenType.Undefined;
					}
					return true;
				}
				return false;
			}
			return true;
		}

		public static explicit operator bool(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, BooleanTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Boolean.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return global::System.Convert.ToBoolean((int)bigInteger);
			}
			return global::System.Convert.ToBoolean(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator global::System.DateTimeOffset(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, DateTimeTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to DateTimeOffset.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			object value2 = jValue.Value;
			if (value2 is global::System.DateTimeOffset)
			{
				return (global::System.DateTimeOffset)value2;
			}
			if (jValue.Value is string input)
			{
				return global::System.DateTimeOffset.Parse(input, global::System.Globalization.CultureInfo.InvariantCulture);
			}
			return new global::System.DateTimeOffset(global::System.Convert.ToDateTime(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture));
		}

		public static explicit operator bool?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, BooleanTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Boolean.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return global::System.Convert.ToBoolean((int)bigInteger);
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToBoolean(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator long(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Int64.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (long)bigInteger;
			}
			return global::System.Convert.ToInt64(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator global::System.DateTime?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, DateTimeTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to DateTime.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.DateTimeOffset dateTimeOffset)
			{
				return dateTimeOffset.DateTime;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToDateTime(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator global::System.DateTimeOffset?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, DateTimeTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to DateTimeOffset.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value == null)
			{
				return null;
			}
			object value2 = jValue.Value;
			if (value2 is global::System.DateTimeOffset)
			{
				return (global::System.DateTimeOffset)value2;
			}
			if (jValue.Value is string input)
			{
				return global::System.DateTimeOffset.Parse(input, global::System.Globalization.CultureInfo.InvariantCulture);
			}
			return new global::System.DateTimeOffset(global::System.Convert.ToDateTime(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture));
		}

		public static explicit operator decimal?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Decimal.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (decimal)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToDecimal(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator double?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Double.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (double)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToDouble(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator char?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, CharTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Char.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (char)(ushort)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToChar(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator int(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Int32.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (int)bigInteger;
			}
			return global::System.Convert.ToInt32(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator short(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Int16.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (short)bigInteger;
			}
			return global::System.Convert.ToInt16(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static explicit operator ushort(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to UInt16.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (ushort)bigInteger;
			}
			return global::System.Convert.ToUInt16(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static explicit operator char(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, CharTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Char.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (char)(ushort)bigInteger;
			}
			return global::System.Convert.ToChar(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator byte(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Byte.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (byte)bigInteger;
			}
			return global::System.Convert.ToByte(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static explicit operator sbyte(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to SByte.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (sbyte)bigInteger;
			}
			return global::System.Convert.ToSByte(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator int?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Int32.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (int)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToInt32(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator short?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Int16.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (short)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToInt16(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static explicit operator ushort?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to UInt16.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (ushort)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToUInt16(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator byte?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Byte.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (byte)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToByte(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static explicit operator sbyte?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to SByte.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (sbyte)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToSByte(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator global::System.DateTime(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, DateTimeTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to DateTime.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.DateTimeOffset dateTimeOffset)
			{
				return dateTimeOffset.DateTime;
			}
			return global::System.Convert.ToDateTime(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator long?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Int64.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (long)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToInt64(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator float?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Single.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (float)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToSingle(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator decimal(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Decimal.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (decimal)bigInteger;
			}
			return global::System.Convert.ToDecimal(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static explicit operator uint?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to UInt32.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (uint)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToUInt32(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static explicit operator ulong?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to UInt64.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (ulong)bigInteger;
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::System.Convert.ToUInt64(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator double(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Double.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (double)bigInteger;
			}
			return global::System.Convert.ToDouble(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator float(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Single.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (float)bigInteger;
			}
			return global::System.Convert.ToSingle(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator string?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, StringTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to String.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value == null)
			{
				return null;
			}
			if (jValue.Value is byte[] inArray)
			{
				return global::System.Convert.ToBase64String(inArray);
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return bigInteger.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
			}
			return global::System.Convert.ToString(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static explicit operator uint(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to UInt32.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (uint)bigInteger;
			}
			return global::System.Convert.ToUInt32(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		[global::System.CLSCompliant(false)]
		public static explicit operator ulong(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, NumberTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to UInt64.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return (ulong)bigInteger;
			}
			return global::System.Convert.ToUInt64(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		public static explicit operator byte[]?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, BytesTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to byte array.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is string)
			{
				return global::System.Convert.FromBase64String(global::System.Convert.ToString(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture));
			}
			if (jValue.Value is global::System.Numerics.BigInteger bigInteger)
			{
				return bigInteger.ToByteArray();
			}
			if (jValue.Value is byte[] result)
			{
				return result;
			}
			throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to byte array.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
		}

		public static explicit operator global::System.Guid(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, GuidTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Guid.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value is byte[] b)
			{
				return new global::System.Guid(b);
			}
			object value2 = jValue.Value;
			if (value2 is global::System.Guid)
			{
				return (global::System.Guid)value2;
			}
			return new global::System.Guid(global::System.Convert.ToString(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture));
		}

		public static explicit operator global::System.Guid?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, GuidTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Guid.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value == null)
			{
				return null;
			}
			if (jValue.Value is byte[] b)
			{
				return new global::System.Guid(b);
			}
			return (jValue.Value is global::System.Guid guid) ? guid : new global::System.Guid(global::System.Convert.ToString(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture));
		}

		public static explicit operator global::System.TimeSpan(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, TimeSpanTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to TimeSpan.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			object value2 = jValue.Value;
			if (value2 is global::System.TimeSpan)
			{
				return (global::System.TimeSpan)value2;
			}
			return global::Newtonsoft.Json.Utilities.ConvertUtils.ParseTimeSpan(global::System.Convert.ToString(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture));
		}

		public static explicit operator global::System.TimeSpan?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, TimeSpanTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to TimeSpan.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return (jValue.Value is global::System.TimeSpan timeSpan) ? timeSpan : global::Newtonsoft.Json.Utilities.ConvertUtils.ParseTimeSpan(global::System.Convert.ToString(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture));
		}

		public static explicit operator global::System.Uri?(global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (value == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, UriTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to Uri.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value == null)
			{
				return null;
			}
			if (!(jValue.Value is global::System.Uri result))
			{
				return new global::System.Uri(global::System.Convert.ToString(jValue.Value, global::System.Globalization.CultureInfo.InvariantCulture));
			}
			return result;
		}

		private static global::System.Numerics.BigInteger ToBigInteger(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, BigIntegerTypes, nullable: false))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to BigInteger.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			return global::Newtonsoft.Json.Utilities.ConvertUtils.ToBigInteger(jValue.Value);
		}

		private static global::System.Numerics.BigInteger? ToBigIntegerNullable(global::Newtonsoft.Json.Linq.JToken value)
		{
			global::Newtonsoft.Json.Linq.JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateToken(jValue, BigIntegerTypes, nullable: true))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not convert {0} to BigInteger.", global::System.Globalization.CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return global::Newtonsoft.Json.Utilities.ConvertUtils.ToBigInteger(jValue.Value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(bool value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(global::System.DateTimeOffset value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(byte value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(byte? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(sbyte value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(sbyte? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(bool? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(long value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(global::System.DateTime? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(global::System.DateTimeOffset? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(decimal? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(double? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(short value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(ushort value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(int value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(int? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(global::System.DateTime value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(long? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(float? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(decimal value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(short? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(ushort? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(uint? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(ulong? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(double value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(float value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(string? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(uint value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		[global::System.CLSCompliant(false)]
		public static implicit operator global::Newtonsoft.Json.Linq.JToken(ulong value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(byte[] value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(global::System.Uri? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(global::System.TimeSpan value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(global::System.TimeSpan? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(global::System.Guid value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		public static implicit operator global::Newtonsoft.Json.Linq.JToken(global::System.Guid? value)
		{
			return new global::Newtonsoft.Json.Linq.JValue(value);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return ((global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)this).GetEnumerator();
		}

		global::System.Collections.Generic.IEnumerator<global::Newtonsoft.Json.Linq.JToken> global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>.GetEnumerator()
		{
			return Children().GetEnumerator();
		}

		internal abstract int GetDeepHashCode();

		public global::Newtonsoft.Json.JsonReader CreateReader()
		{
			return new global::Newtonsoft.Json.Linq.JTokenReader(this);
		}

		internal static global::Newtonsoft.Json.Linq.JToken FromObjectInternal(object o, global::Newtonsoft.Json.JsonSerializer jsonSerializer)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(o, "o");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			using global::Newtonsoft.Json.Linq.JTokenWriter jTokenWriter = new global::Newtonsoft.Json.Linq.JTokenWriter();
			jsonSerializer.Serialize(jTokenWriter, o);
			return jTokenWriter.Token;
		}

		public static global::Newtonsoft.Json.Linq.JToken FromObject(object o)
		{
			return FromObjectInternal(o, global::Newtonsoft.Json.JsonSerializer.CreateDefault());
		}

		public static global::Newtonsoft.Json.Linq.JToken FromObject(object o, global::Newtonsoft.Json.JsonSerializer jsonSerializer)
		{
			return FromObjectInternal(o, jsonSerializer);
		}

		public T? ToObject<T>()
		{
			return (T)ToObject(typeof(T));
		}

		public object? ToObject(global::System.Type objectType)
		{
			if (global::Newtonsoft.Json.JsonConvert.DefaultSettings == null)
			{
				bool isEnum;
				global::Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode = global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeCode(objectType, out isEnum);
				if (isEnum)
				{
					if (Type == global::Newtonsoft.Json.Linq.JTokenType.String)
					{
						try
						{
							return ToObject(objectType, global::Newtonsoft.Json.JsonSerializer.CreateDefault());
						}
						catch (global::System.Exception innerException)
						{
							global::System.Type type = (global::Newtonsoft.Json.Utilities.TypeExtensions.IsEnum(objectType) ? objectType : global::System.Nullable.GetUnderlyingType(objectType));
							throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not convert '{0}' to {1}.", global::System.Globalization.CultureInfo.InvariantCulture, (string?)this, type.Name), innerException);
						}
					}
					if (Type == global::Newtonsoft.Json.Linq.JTokenType.Integer)
					{
						return global::System.Enum.ToObject(global::Newtonsoft.Json.Utilities.TypeExtensions.IsEnum(objectType) ? objectType : global::System.Nullable.GetUnderlyingType(objectType), ((global::Newtonsoft.Json.Linq.JValue)this).Value);
					}
				}
				switch (typeCode)
				{
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BooleanNullable:
					return (bool?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Boolean:
					return (bool)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.CharNullable:
					return (char?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Char:
					return (char)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte:
					return (sbyte)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByteNullable:
					return (sbyte?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.ByteNullable:
					return (byte?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte:
					return (byte)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16Nullable:
					return (short?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16:
					return (short)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16Nullable:
					return (ushort?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16:
					return (ushort)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32Nullable:
					return (int?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32:
					return (int)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32Nullable:
					return (uint?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32:
					return (uint)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64Nullable:
					return (long?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64:
					return (long)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64Nullable:
					return (ulong?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64:
					return (ulong)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SingleNullable:
					return (float?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Single:
					return (float)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DoubleNullable:
					return (double?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Double:
					return (double)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DecimalNullable:
					return (decimal?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Decimal:
					return (decimal)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeNullable:
					return (global::System.DateTime?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime:
					return (global::System.DateTime)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffsetNullable:
					return (global::System.DateTimeOffset?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffset:
					return (global::System.DateTimeOffset)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.String:
					return (string?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.GuidNullable:
					return (global::System.Guid?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Guid:
					return (global::System.Guid)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Uri:
					return (global::System.Uri?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpanNullable:
					return (global::System.TimeSpan?)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.TimeSpan:
					return (global::System.TimeSpan)this;
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigIntegerNullable:
					return ToBigIntegerNullable(this);
				case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigInteger:
					return ToBigInteger(this);
				}
			}
			return ToObject(objectType, global::Newtonsoft.Json.JsonSerializer.CreateDefault());
		}

		public T? ToObject<T>(global::Newtonsoft.Json.JsonSerializer jsonSerializer)
		{
			return (T)ToObject(typeof(T), jsonSerializer);
		}

		public object? ToObject(global::System.Type? objectType, global::Newtonsoft.Json.JsonSerializer jsonSerializer)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			using global::Newtonsoft.Json.Linq.JTokenReader reader = new global::Newtonsoft.Json.Linq.JTokenReader(this);
			if (jsonSerializer is global::Newtonsoft.Json.Serialization.JsonSerializerProxy jsonSerializerProxy)
			{
				jsonSerializerProxy._serializer.SetupReader(reader, out global::System.Globalization.CultureInfo _, out global::Newtonsoft.Json.DateTimeZoneHandling? _, out global::Newtonsoft.Json.DateParseHandling? _, out global::Newtonsoft.Json.FloatParseHandling? _, out int? _, out string _);
			}
			return jsonSerializer.Deserialize(reader, objectType);
		}

		public static global::Newtonsoft.Json.Linq.JToken ReadFrom(global::Newtonsoft.Json.JsonReader reader)
		{
			return ReadFrom(reader, null);
		}

		public static global::Newtonsoft.Json.Linq.JToken ReadFrom(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			if (!((reader.TokenType == global::Newtonsoft.Json.JsonToken.None) ? ((settings != null && settings.CommentHandling == global::Newtonsoft.Json.Linq.CommentHandling.Ignore) ? reader.ReadAndMoveToContent() : reader.Read()) : (reader.TokenType != global::Newtonsoft.Json.JsonToken.Comment || settings == null || settings.CommentHandling != global::Newtonsoft.Json.Linq.CommentHandling.Ignore || reader.ReadAndMoveToContent())))
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JToken from JsonReader.");
			}
			global::Newtonsoft.Json.IJsonLineInfo lineInfo = reader as global::Newtonsoft.Json.IJsonLineInfo;
			switch (reader.TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.StartObject:
				return global::Newtonsoft.Json.Linq.JObject.Load(reader, settings);
			case global::Newtonsoft.Json.JsonToken.StartArray:
				return global::Newtonsoft.Json.Linq.JArray.Load(reader, settings);
			case global::Newtonsoft.Json.JsonToken.StartConstructor:
				return global::Newtonsoft.Json.Linq.JConstructor.Load(reader, settings);
			case global::Newtonsoft.Json.JsonToken.PropertyName:
				return global::Newtonsoft.Json.Linq.JProperty.Load(reader, settings);
			case global::Newtonsoft.Json.JsonToken.Integer:
			case global::Newtonsoft.Json.JsonToken.Float:
			case global::Newtonsoft.Json.JsonToken.String:
			case global::Newtonsoft.Json.JsonToken.Boolean:
			case global::Newtonsoft.Json.JsonToken.Date:
			case global::Newtonsoft.Json.JsonToken.Bytes:
			{
				global::Newtonsoft.Json.Linq.JValue jValue4 = new global::Newtonsoft.Json.Linq.JValue(reader.Value);
				jValue4.SetLineInfo(lineInfo, settings);
				return jValue4;
			}
			case global::Newtonsoft.Json.JsonToken.Comment:
			{
				global::Newtonsoft.Json.Linq.JValue jValue3 = global::Newtonsoft.Json.Linq.JValue.CreateComment(reader.Value.ToString());
				jValue3.SetLineInfo(lineInfo, settings);
				return jValue3;
			}
			case global::Newtonsoft.Json.JsonToken.Null:
			{
				global::Newtonsoft.Json.Linq.JValue jValue2 = global::Newtonsoft.Json.Linq.JValue.CreateNull();
				jValue2.SetLineInfo(lineInfo, settings);
				return jValue2;
			}
			case global::Newtonsoft.Json.JsonToken.Undefined:
			{
				global::Newtonsoft.Json.Linq.JValue jValue = global::Newtonsoft.Json.Linq.JValue.CreateUndefined();
				jValue.SetLineInfo(lineInfo, settings);
				return jValue;
			}
			default:
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JToken from JsonReader. Unexpected token: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
		}

		public static global::Newtonsoft.Json.Linq.JToken Parse(string json)
		{
			return Parse(json, null);
		}

		public static global::Newtonsoft.Json.Linq.JToken Parse(string json, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			using global::Newtonsoft.Json.JsonReader jsonReader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(json));
			global::Newtonsoft.Json.Linq.JToken result = Load(jsonReader, settings);
			while (jsonReader.Read())
			{
			}
			return result;
		}

		public static global::Newtonsoft.Json.Linq.JToken Load(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			return ReadFrom(reader, settings);
		}

		public static global::Newtonsoft.Json.Linq.JToken Load(global::Newtonsoft.Json.JsonReader reader)
		{
			return Load(reader, null);
		}

		internal void SetLineInfo(global::Newtonsoft.Json.IJsonLineInfo? lineInfo, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			if ((settings == null || settings.LineInfoHandling == global::Newtonsoft.Json.Linq.LineInfoHandling.Load) && lineInfo != null && lineInfo.HasLineInfo())
			{
				SetLineInfo(lineInfo.LineNumber, lineInfo.LinePosition);
			}
		}

		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			AddAnnotation(new global::Newtonsoft.Json.Linq.JToken.LineInfoAnnotation(lineNumber, linePosition));
		}

		bool global::Newtonsoft.Json.IJsonLineInfo.HasLineInfo()
		{
			return Annotation<global::Newtonsoft.Json.Linq.JToken.LineInfoAnnotation>() != null;
		}

		public global::Newtonsoft.Json.Linq.JToken? SelectToken(string path)
		{
			return SelectToken(path, null);
		}

		public global::Newtonsoft.Json.Linq.JToken? SelectToken(string path, bool errorWhenNoMatch)
		{
			global::Newtonsoft.Json.Linq.JsonSelectSettings settings = (errorWhenNoMatch ? new global::Newtonsoft.Json.Linq.JsonSelectSettings
			{
				ErrorWhenNoMatch = true
			} : null);
			return SelectToken(path, settings);
		}

		public global::Newtonsoft.Json.Linq.JToken? SelectToken(string path, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			global::Newtonsoft.Json.Linq.JsonPath.JPath jPath = new global::Newtonsoft.Json.Linq.JsonPath.JPath(path);
			global::Newtonsoft.Json.Linq.JToken jToken = null;
			foreach (global::Newtonsoft.Json.Linq.JToken item in jPath.Evaluate(this, this, settings))
			{
				if (jToken != null)
				{
					throw new global::Newtonsoft.Json.JsonException("Path returned multiple tokens.");
				}
				jToken = item;
			}
			return jToken;
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> SelectTokens(string path)
		{
			return SelectTokens(path, null);
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> SelectTokens(string path, bool errorWhenNoMatch)
		{
			global::Newtonsoft.Json.Linq.JsonSelectSettings settings = (errorWhenNoMatch ? new global::Newtonsoft.Json.Linq.JsonSelectSettings
			{
				ErrorWhenNoMatch = true
			} : null);
			return SelectTokens(path, settings);
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> SelectTokens(string path, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			return new global::Newtonsoft.Json.Linq.JsonPath.JPath(path).Evaluate(this, this, settings);
		}

		protected virtual global::System.Dynamic.DynamicMetaObject GetMetaObject(global::System.Linq.Expressions.Expression parameter)
		{
			return new global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<global::Newtonsoft.Json.Linq.JToken>(parameter, this, new global::Newtonsoft.Json.Utilities.DynamicProxy<global::Newtonsoft.Json.Linq.JToken>());
		}

		global::System.Dynamic.DynamicMetaObject global::System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(global::System.Linq.Expressions.Expression parameter)
		{
			return GetMetaObject(parameter);
		}

		object global::System.ICloneable.Clone()
		{
			return DeepClone();
		}

		public global::Newtonsoft.Json.Linq.JToken DeepClone()
		{
			return CloneToken(null);
		}

		public global::Newtonsoft.Json.Linq.JToken DeepClone(global::Newtonsoft.Json.Linq.JsonCloneSettings settings)
		{
			return CloneToken(settings);
		}

		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new global::System.ArgumentNullException("annotation");
			}
			if (_annotations == null)
			{
				_annotations = ((!(annotation is object[])) ? annotation : new object[1] { annotation });
				return;
			}
			object[] array = _annotations as object[];
			if (array == null)
			{
				_annotations = new object[2] { _annotations, annotation };
				return;
			}
			int i;
			for (i = 0; i < array.Length && array[i] != null; i++)
			{
			}
			if (i == array.Length)
			{
				global::System.Array.Resize(ref array, i * 2);
				_annotations = array;
			}
			array[i] = annotation;
		}

		public T? Annotation<T>() where T : class
		{
			if (_annotations != null)
			{
				if (!(_annotations is object[] array))
				{
					return _annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					if (obj is T result)
					{
						return result;
					}
				}
			}
			return null;
		}

		public object? Annotation(global::System.Type type)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (_annotations != null)
			{
				if (!(_annotations is object[] array))
				{
					if (type.IsInstanceOfType(_annotations))
					{
						return _annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		public global::System.Collections.Generic.IEnumerable<T> Annotations<T>() where T : class
		{
			if (_annotations == null)
			{
				yield break;
			}
			object annotations = _annotations;
			object[] annotations2 = annotations as object[];
			if (annotations2 != null)
			{
				foreach (object obj in annotations2)
				{
					if (obj != null)
					{
						if (obj is T val)
						{
							yield return val;
						}
						continue;
					}
					break;
				}
			}
			else if (_annotations is T val2)
			{
				yield return val2;
			}
		}

		public global::System.Collections.Generic.IEnumerable<object> Annotations(global::System.Type type)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (_annotations == null)
			{
				yield break;
			}
			object annotations = _annotations;
			object[] annotations2 = annotations as object[];
			if (annotations2 != null)
			{
				foreach (object obj in annotations2)
				{
					if (obj != null)
					{
						if (type.IsInstanceOfType(obj))
						{
							yield return obj;
						}
						continue;
					}
					break;
				}
			}
			else if (type.IsInstanceOfType(_annotations))
			{
				yield return _annotations;
			}
		}

		public void RemoveAnnotations<T>() where T : class
		{
			if (_annotations == null)
			{
				return;
			}
			if (!(_annotations is object[] array))
			{
				if (_annotations is T)
				{
					_annotations = null;
				}
				return;
			}
			int i = 0;
			int num = 0;
			for (; i < array.Length; i++)
			{
				object obj = array[i];
				if (obj == null)
				{
					break;
				}
				if (!(obj is T))
				{
					array[num++] = obj;
				}
			}
			if (num != 0)
			{
				while (num < i)
				{
					array[num++] = null;
				}
			}
			else
			{
				_annotations = null;
			}
		}

		public void RemoveAnnotations(global::System.Type type)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (_annotations == null)
			{
				return;
			}
			if (!(_annotations is object[] array))
			{
				if (type.IsInstanceOfType(_annotations))
				{
					_annotations = null;
				}
				return;
			}
			int i = 0;
			int num = 0;
			for (; i < array.Length; i++)
			{
				object obj = array[i];
				if (obj == null)
				{
					break;
				}
				if (!type.IsInstanceOfType(obj))
				{
					array[num++] = obj;
				}
			}
			if (num != 0)
			{
				while (num < i)
				{
					array[num++] = null;
				}
			}
			else
			{
				_annotations = null;
			}
		}

		internal void CopyAnnotations(global::Newtonsoft.Json.Linq.JToken target, global::Newtonsoft.Json.Linq.JToken source)
		{
			if (source._annotations is object[] source2)
			{
				target._annotations = global::System.Linq.Enumerable.ToArray(source2);
			}
			else
			{
				target._annotations = source._annotations;
			}
		}
	}
}
