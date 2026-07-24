namespace Newtonsoft.Json.Linq
{
	public class JArray : global::Newtonsoft.Json.Linq.JContainer, global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.IEnumerable
	{
		private readonly global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken> _values = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken>();

		protected override global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> ChildrenTokens => _values;

		public override global::Newtonsoft.Json.Linq.JTokenType Type => global::Newtonsoft.Json.Linq.JTokenType.Array;

		public override global::Newtonsoft.Json.Linq.JToken? this[object key]
		{
			get
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Accessed JArray values with invalid key value: {0}. Int32 array index expected.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ToString(key)));
				}
				return GetItem((int)key);
			}
			set
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Set JArray values with invalid key value: {0}. Int32 array index expected.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ToString(key)));
				}
				SetItem((int)key, value);
			}
		}

		public global::Newtonsoft.Json.Linq.JToken this[int index]
		{
			get
			{
				return GetItem(index);
			}
			set
			{
				SetItem(index, value);
			}
		}

		public bool IsReadOnly => false;

		public override async global::System.Threading.Tasks.Task WriteToAsync(global::Newtonsoft.Json.JsonWriter writer, global::System.Threading.CancellationToken cancellationToken, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			await writer.WriteStartArrayAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			for (int i = 0; i < _values.Count; i++)
			{
				await _values[i].WriteToAsync(writer, cancellationToken, converters).ConfigureAwait(continueOnCapturedContext: false);
			}
			await writer.WriteEndArrayAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public new static global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JArray> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return LoadAsync(reader, null, cancellationToken);
		}

		public new static async global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JArray> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !(await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartArray)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			global::Newtonsoft.Json.Linq.JArray a = new global::Newtonsoft.Json.Linq.JArray();
			a.SetLineInfo(reader as global::Newtonsoft.Json.IJsonLineInfo, settings);
			await a.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return a;
		}

		public JArray()
		{
		}

		public JArray(global::Newtonsoft.Json.Linq.JArray other)
			: base(other, null)
		{
		}

		internal JArray(global::Newtonsoft.Json.Linq.JArray other, global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
			: base(other, settings)
		{
		}

		public JArray(params object[] content)
			: this((object)content)
		{
		}

		public JArray(object content)
		{
			Add(content);
		}

		internal override bool DeepEquals(global::Newtonsoft.Json.Linq.JToken node)
		{
			if (node is global::Newtonsoft.Json.Linq.JArray container)
			{
				return ContentsEqual(container);
			}
			return false;
		}

		internal override global::Newtonsoft.Json.Linq.JToken CloneToken(global::Newtonsoft.Json.Linq.JsonCloneSettings? settings = null)
		{
			return new global::Newtonsoft.Json.Linq.JArray(this, settings);
		}

		public new static global::Newtonsoft.Json.Linq.JArray Load(global::Newtonsoft.Json.JsonReader reader)
		{
			return Load(reader, null);
		}

		public new static global::Newtonsoft.Json.Linq.JArray Load(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !reader.Read())
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartArray)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			global::Newtonsoft.Json.Linq.JArray jArray = new global::Newtonsoft.Json.Linq.JArray();
			jArray.SetLineInfo(reader as global::Newtonsoft.Json.IJsonLineInfo, settings);
			jArray.ReadTokenFrom(reader, settings);
			return jArray;
		}

		public new static global::Newtonsoft.Json.Linq.JArray Parse(string json)
		{
			return Parse(json, null);
		}

		public new static global::Newtonsoft.Json.Linq.JArray Parse(string json, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			using global::Newtonsoft.Json.JsonReader jsonReader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(json));
			global::Newtonsoft.Json.Linq.JArray result = Load(jsonReader, settings);
			while (jsonReader.Read())
			{
			}
			return result;
		}

		public new static global::Newtonsoft.Json.Linq.JArray FromObject(object o)
		{
			return FromObject(o, global::Newtonsoft.Json.JsonSerializer.CreateDefault());
		}

		public new static global::Newtonsoft.Json.Linq.JArray FromObject(object o, global::Newtonsoft.Json.JsonSerializer jsonSerializer)
		{
			global::Newtonsoft.Json.Linq.JToken jToken = global::Newtonsoft.Json.Linq.JToken.FromObjectInternal(o, jsonSerializer);
			if (jToken.Type != global::Newtonsoft.Json.Linq.JTokenType.Array)
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Object serialized to {0}. JArray instance expected.", global::System.Globalization.CultureInfo.InvariantCulture, jToken.Type));
			}
			return (global::Newtonsoft.Json.Linq.JArray)jToken;
		}

		public override void WriteTo(global::Newtonsoft.Json.JsonWriter writer, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < _values.Count; i++)
			{
				_values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
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
			global::System.Collections.IEnumerable enumerable = ((IsMultiContent(content) || content is global::Newtonsoft.Json.Linq.JArray) ? ((global::System.Collections.IEnumerable)content) : null);
			if (enumerable != null)
			{
				global::Newtonsoft.Json.Linq.JContainer.MergeEnumerableContent(this, enumerable, settings);
			}
		}

		public int IndexOf(global::Newtonsoft.Json.Linq.JToken item)
		{
			return IndexOfItem(item);
		}

		public void Insert(int index, global::Newtonsoft.Json.Linq.JToken item)
		{
			InsertItem(index, item, skipParentCheck: false, copyAnnotations: true);
		}

		public void RemoveAt(int index)
		{
			RemoveItemAt(index);
		}

		public global::System.Collections.Generic.IEnumerator<global::Newtonsoft.Json.Linq.JToken> GetEnumerator()
		{
			return Children().GetEnumerator();
		}

		public void Add(global::Newtonsoft.Json.Linq.JToken item)
		{
			Add((object?)item);
		}

		public void Clear()
		{
			ClearItems();
		}

		public bool Contains(global::Newtonsoft.Json.Linq.JToken item)
		{
			return ContainsItem(item);
		}

		public void CopyTo(global::Newtonsoft.Json.Linq.JToken[] array, int arrayIndex)
		{
			CopyItemsTo(array, arrayIndex);
		}

		public bool Remove(global::Newtonsoft.Json.Linq.JToken item)
		{
			return RemoveItem(item);
		}

		internal override int GetDeepHashCode()
		{
			return ContentsHashCode();
		}
	}
}
