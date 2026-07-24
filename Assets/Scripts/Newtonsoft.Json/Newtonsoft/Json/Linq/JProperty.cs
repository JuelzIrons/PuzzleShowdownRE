namespace Newtonsoft.Json.Linq
{
	public class JProperty : global::Newtonsoft.Json.Linq.JContainer
	{
		private class JPropertyList : global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.IEnumerable
		{
			internal global::Newtonsoft.Json.Linq.JToken? _token;

			public int Count
			{
				get
				{
					if (_token == null)
					{
						return 0;
					}
					return 1;
				}
			}

			public bool IsReadOnly => false;

			public global::Newtonsoft.Json.Linq.JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new global::System.IndexOutOfRangeException();
					}
					return _token;
				}
				set
				{
					if (index != 0)
					{
						throw new global::System.IndexOutOfRangeException();
					}
					_token = value;
				}
			}

			public global::System.Collections.Generic.IEnumerator<global::Newtonsoft.Json.Linq.JToken> GetEnumerator()
			{
				if (_token != null)
				{
					yield return _token;
				}
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Add(global::Newtonsoft.Json.Linq.JToken item)
			{
				_token = item;
			}

			public void Clear()
			{
				_token = null;
			}

			public bool Contains(global::Newtonsoft.Json.Linq.JToken item)
			{
				return _token == item;
			}

			public void CopyTo(global::Newtonsoft.Json.Linq.JToken[] array, int arrayIndex)
			{
				if (_token != null)
				{
					array[arrayIndex] = _token;
				}
			}

			public bool Remove(global::Newtonsoft.Json.Linq.JToken item)
			{
				if (_token == item)
				{
					_token = null;
					return true;
				}
				return false;
			}

			public int IndexOf(global::Newtonsoft.Json.Linq.JToken item)
			{
				if (_token != item)
				{
					return -1;
				}
				return 0;
			}

			public void Insert(int index, global::Newtonsoft.Json.Linq.JToken item)
			{
				if (index == 0)
				{
					_token = item;
				}
			}

			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					_token = null;
				}
			}
		}

		private readonly global::Newtonsoft.Json.Linq.JProperty.JPropertyList _content = new global::Newtonsoft.Json.Linq.JProperty.JPropertyList();

		private readonly string _name;

		protected override global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> ChildrenTokens => _content;

		public string Name
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return _name;
			}
		}

		public new global::Newtonsoft.Json.Linq.JToken Value
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return _content._token;
			}
			set
			{
				CheckReentrancy();
				global::Newtonsoft.Json.Linq.JToken item = value ?? global::Newtonsoft.Json.Linq.JValue.CreateNull();
				if (_content._token == null)
				{
					InsertItem(0, item, skipParentCheck: false, copyAnnotations: true);
				}
				else
				{
					SetItem(0, item);
				}
			}
		}

		public override global::Newtonsoft.Json.Linq.JTokenType Type
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return global::Newtonsoft.Json.Linq.JTokenType.Property;
			}
		}

		public override global::System.Threading.Tasks.Task WriteToAsync(global::Newtonsoft.Json.JsonWriter writer, global::System.Threading.CancellationToken cancellationToken, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			global::System.Threading.Tasks.Task task = writer.WritePropertyNameAsync(_name, cancellationToken);
			if (global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return WriteValueAsync(writer, cancellationToken, converters);
			}
			return WriteToAsync(task, writer, cancellationToken, converters);
		}

		private async global::System.Threading.Tasks.Task WriteToAsync(global::System.Threading.Tasks.Task task, global::Newtonsoft.Json.JsonWriter writer, global::System.Threading.CancellationToken cancellationToken, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			await task.ConfigureAwait(continueOnCapturedContext: false);
			await WriteValueAsync(writer, cancellationToken, converters).ConfigureAwait(continueOnCapturedContext: false);
		}

		private global::System.Threading.Tasks.Task WriteValueAsync(global::Newtonsoft.Json.JsonWriter writer, global::System.Threading.CancellationToken cancellationToken, global::Newtonsoft.Json.JsonConverter[] converters)
		{
			global::Newtonsoft.Json.Linq.JToken value = Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		public new static global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JProperty> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return LoadAsync(reader, null, cancellationToken);
		}

		public new static async global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JProperty> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !(await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			global::Newtonsoft.Json.Linq.JProperty p = new global::Newtonsoft.Json.Linq.JProperty((string)reader.Value);
			p.SetLineInfo(reader as global::Newtonsoft.Json.IJsonLineInfo, settings);
			await p.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return p;
		}

		public JProperty(global::Newtonsoft.Json.Linq.JProperty other)
			: base(other, null)
		{
			_name = other.Name;
		}

		internal JProperty(global::Newtonsoft.Json.Linq.JProperty other, global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
			: base(other, settings)
		{
			_name = other.Name;
		}

		internal override global::Newtonsoft.Json.Linq.JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new global::System.ArgumentOutOfRangeException();
			}
			return Value;
		}

		internal override void SetItem(int index, global::Newtonsoft.Json.Linq.JToken? item)
		{
			if (index != 0)
			{
				throw new global::System.ArgumentOutOfRangeException();
			}
			if (!global::Newtonsoft.Json.Linq.JContainer.IsTokenUnchanged(Value, item))
			{
				((global::Newtonsoft.Json.Linq.JObject)base.Parent)?.InternalPropertyChanging(this);
				base.SetItem(0, item);
				((global::Newtonsoft.Json.Linq.JObject)base.Parent)?.InternalPropertyChanged(this);
			}
		}

		internal override bool RemoveItem(global::Newtonsoft.Json.Linq.JToken? item)
		{
			throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot add or remove items from {0}.", global::System.Globalization.CultureInfo.InvariantCulture, typeof(global::Newtonsoft.Json.Linq.JProperty)));
		}

		internal override void RemoveItemAt(int index)
		{
			throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot add or remove items from {0}.", global::System.Globalization.CultureInfo.InvariantCulture, typeof(global::Newtonsoft.Json.Linq.JProperty)));
		}

		internal override int IndexOfItem(global::Newtonsoft.Json.Linq.JToken? item)
		{
			if (item == null)
			{
				return -1;
			}
			return _content.IndexOf(item);
		}

		internal override bool InsertItem(int index, global::Newtonsoft.Json.Linq.JToken? item, bool skipParentCheck, bool copyAnnotations)
		{
			if (item != null && item.Type == global::Newtonsoft.Json.Linq.JTokenType.Comment)
			{
				return false;
			}
			if (Value != null)
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("{0} cannot have multiple values.", global::System.Globalization.CultureInfo.InvariantCulture, typeof(global::Newtonsoft.Json.Linq.JProperty)));
			}
			return base.InsertItem(0, item, skipParentCheck: false, copyAnnotations);
		}

		internal override bool ContainsItem(global::Newtonsoft.Json.Linq.JToken? item)
		{
			return Value == item;
		}

		internal override void MergeItem(object content, global::Newtonsoft.Json.Linq.JsonMergeSettings? settings)
		{
			global::Newtonsoft.Json.Linq.JToken jToken = (content as global::Newtonsoft.Json.Linq.JProperty)?.Value;
			if (jToken != null && jToken.Type != global::Newtonsoft.Json.Linq.JTokenType.Null)
			{
				Value = jToken;
			}
		}

		internal override void ClearItems()
		{
			throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot add or remove items from {0}.", global::System.Globalization.CultureInfo.InvariantCulture, typeof(global::Newtonsoft.Json.Linq.JProperty)));
		}

		internal override bool DeepEquals(global::Newtonsoft.Json.Linq.JToken node)
		{
			if (node is global::Newtonsoft.Json.Linq.JProperty jProperty && _name == jProperty.Name)
			{
				return ContentsEqual(jProperty);
			}
			return false;
		}

		internal override global::Newtonsoft.Json.Linq.JToken CloneToken(global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
		{
			return new global::Newtonsoft.Json.Linq.JProperty(this, settings);
		}

		internal JProperty(string name)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(name, "name");
			_name = name;
		}

		public JProperty(string name, params object[] content)
			: this(name, (object?)content)
		{
		}

		public JProperty(string name, object? content)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(name, "name");
			_name = name;
			Value = (IsMultiContent(content) ? new global::Newtonsoft.Json.Linq.JArray(content) : global::Newtonsoft.Json.Linq.JContainer.CreateFromContent(content));
		}

		public override void WriteTo(global::Newtonsoft.Json.JsonWriter writer, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			writer.WritePropertyName(_name);
			global::Newtonsoft.Json.Linq.JToken value = Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
			}
			else
			{
				writer.WriteNull();
			}
		}

		internal override int GetDeepHashCode()
		{
			return _name.GetHashCode() ^ (Value?.GetDeepHashCode() ?? 0);
		}

		public new static global::Newtonsoft.Json.Linq.JProperty Load(global::Newtonsoft.Json.JsonReader reader)
		{
			return Load(reader, null);
		}

		public new static global::Newtonsoft.Json.Linq.JProperty Load(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !reader.Read())
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			global::Newtonsoft.Json.Linq.JProperty jProperty = new global::Newtonsoft.Json.Linq.JProperty((string)reader.Value);
			jProperty.SetLineInfo(reader as global::Newtonsoft.Json.IJsonLineInfo, settings);
			jProperty.ReadTokenFrom(reader, settings);
			return jProperty;
		}
	}
}
