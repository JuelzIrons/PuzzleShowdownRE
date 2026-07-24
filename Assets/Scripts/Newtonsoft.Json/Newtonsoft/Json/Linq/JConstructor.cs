namespace Newtonsoft.Json.Linq
{
	public class JConstructor : global::Newtonsoft.Json.Linq.JContainer
	{
		private string? _name;

		private readonly global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken> _values = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken>();

		protected override global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> ChildrenTokens => _values;

		public string? Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		public override global::Newtonsoft.Json.Linq.JTokenType Type => global::Newtonsoft.Json.Linq.JTokenType.Constructor;

		public override global::Newtonsoft.Json.Linq.JToken? this[object key]
		{
			get
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int index))
				{
					throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Accessed JConstructor values with invalid key value: {0}. Argument position index expected.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ToString(key)));
				}
				return GetItem(index);
			}
			set
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int index))
				{
					throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Set JConstructor values with invalid key value: {0}. Argument position index expected.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ToString(key)));
				}
				SetItem(index, value);
			}
		}

		public override async global::System.Threading.Tasks.Task WriteToAsync(global::Newtonsoft.Json.JsonWriter writer, global::System.Threading.CancellationToken cancellationToken, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			await writer.WriteStartConstructorAsync(_name ?? string.Empty, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			for (int i = 0; i < _values.Count; i++)
			{
				await _values[i].WriteToAsync(writer, cancellationToken, converters).ConfigureAwait(continueOnCapturedContext: false);
			}
			await writer.WriteEndConstructorAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public new static global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JConstructor> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return LoadAsync(reader, null, cancellationToken);
		}

		public new static async global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JConstructor> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !(await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader.");
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartConstructor)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JConstructor from JsonReader. Current JsonReader item is not a constructor: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			global::Newtonsoft.Json.Linq.JConstructor c = new global::Newtonsoft.Json.Linq.JConstructor((string)reader.Value);
			c.SetLineInfo(reader as global::Newtonsoft.Json.IJsonLineInfo, settings);
			await c.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return c;
		}

		internal override int IndexOfItem(global::Newtonsoft.Json.Linq.JToken? item)
		{
			if (item == null)
			{
				return -1;
			}
			return global::Newtonsoft.Json.Utilities.CollectionUtils.IndexOfReference(_values, item);
		}

		internal override void MergeItem(object content, global::Newtonsoft.Json.Linq.JsonMergeSettings? settings)
		{
			if (content is global::Newtonsoft.Json.Linq.JConstructor jConstructor)
			{
				if (jConstructor.Name != null)
				{
					Name = jConstructor.Name;
				}
				global::Newtonsoft.Json.Linq.JContainer.MergeEnumerableContent(this, jConstructor, settings);
			}
		}

		public JConstructor()
		{
		}

		public JConstructor(global::Newtonsoft.Json.Linq.JConstructor other)
			: base(other, null)
		{
			_name = other.Name;
		}

		internal JConstructor(global::Newtonsoft.Json.Linq.JConstructor other, global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
			: base(other, settings)
		{
			_name = other.Name;
		}

		public JConstructor(string name, params object[] content)
			: this(name, (object)content)
		{
		}

		public JConstructor(string name, object content)
			: this(name)
		{
			Add(content);
		}

		public JConstructor(string name)
		{
			if (name == null)
			{
				throw new global::System.ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new global::System.ArgumentException("Constructor name cannot be empty.", "name");
			}
			_name = name;
		}

		internal override bool DeepEquals(global::Newtonsoft.Json.Linq.JToken node)
		{
			if (node is global::Newtonsoft.Json.Linq.JConstructor jConstructor && _name == jConstructor.Name)
			{
				return ContentsEqual(jConstructor);
			}
			return false;
		}

		internal override global::Newtonsoft.Json.Linq.JToken CloneToken(global::Newtonsoft.Json.Linq.JsonCloneSettings? settings = null)
		{
			return new global::Newtonsoft.Json.Linq.JConstructor(this, settings);
		}

		public override void WriteTo(global::Newtonsoft.Json.JsonWriter writer, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			writer.WriteStartConstructor(_name);
			int count = _values.Count;
			for (int i = 0; i < count; i++)
			{
				_values[i].WriteTo(writer, converters);
			}
			writer.WriteEndConstructor();
		}

		internal override int GetDeepHashCode()
		{
			return (_name?.GetHashCode() ?? 0) ^ ContentsHashCode();
		}

		public new static global::Newtonsoft.Json.Linq.JConstructor Load(global::Newtonsoft.Json.JsonReader reader)
		{
			return Load(reader, null);
		}

		public new static global::Newtonsoft.Json.Linq.JConstructor Load(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !reader.Read())
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartConstructor)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JConstructor from JsonReader. Current JsonReader item is not a constructor: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			global::Newtonsoft.Json.Linq.JConstructor jConstructor = new global::Newtonsoft.Json.Linq.JConstructor((string)reader.Value);
			jConstructor.SetLineInfo(reader as global::Newtonsoft.Json.IJsonLineInfo, settings);
			jConstructor.ReadTokenFrom(reader, settings);
			return jConstructor;
		}
	}
}
