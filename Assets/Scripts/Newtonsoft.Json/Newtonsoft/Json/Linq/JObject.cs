namespace Newtonsoft.Json.Linq
{
	public class JObject : global::Newtonsoft.Json.Linq.JContainer, global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Linq.JToken?>, global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken?>>, global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken?>>, global::System.Collections.IEnumerable, global::System.ComponentModel.INotifyPropertyChanged, global::System.ComponentModel.ICustomTypeDescriptor, global::System.ComponentModel.INotifyPropertyChanging
	{
		private class JObjectDynamicProxy : global::Newtonsoft.Json.Utilities.DynamicProxy<global::Newtonsoft.Json.Linq.JObject>
		{
			public override bool TryGetMember(global::Newtonsoft.Json.Linq.JObject instance, global::System.Dynamic.GetMemberBinder binder, out object? result)
			{
				result = instance[binder.Name];
				return true;
			}

			public override bool TrySetMember(global::Newtonsoft.Json.Linq.JObject instance, global::System.Dynamic.SetMemberBinder binder, object value)
			{
				global::Newtonsoft.Json.Linq.JToken jToken = value as global::Newtonsoft.Json.Linq.JToken;
				if (jToken == null)
				{
					jToken = new global::Newtonsoft.Json.Linq.JValue(value);
				}
				instance[binder.Name] = jToken;
				return true;
			}

			public override global::System.Collections.Generic.IEnumerable<string> GetDynamicMemberNames(global::Newtonsoft.Json.Linq.JObject instance)
			{
				return global::System.Linq.Enumerable.Select(instance.Properties(), (global::Newtonsoft.Json.Linq.JProperty p) => p.Name);
			}
		}

		private readonly global::Newtonsoft.Json.Linq.JPropertyKeyedCollection _properties = new global::Newtonsoft.Json.Linq.JPropertyKeyedCollection();

		protected override global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> ChildrenTokens => _properties;

		public override global::Newtonsoft.Json.Linq.JTokenType Type => global::Newtonsoft.Json.Linq.JTokenType.Object;

		public override global::Newtonsoft.Json.Linq.JToken? this[object key]
		{
			get
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is string propertyName))
				{
					throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Accessed JObject values with invalid key value: {0}. Object property name expected.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ToString(key)));
				}
				return this[propertyName];
			}
			set
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is string propertyName))
				{
					throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Set JObject values with invalid key value: {0}. Object property name expected.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.Utilities.MiscellaneousUtils.ToString(key)));
				}
				this[propertyName] = value;
			}
		}

		public global::Newtonsoft.Json.Linq.JToken? this[string propertyName]
		{
			get
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
				return Property(propertyName, global::System.StringComparison.Ordinal)?.Value;
			}
			set
			{
				global::Newtonsoft.Json.Linq.JProperty jProperty = Property(propertyName, global::System.StringComparison.Ordinal);
				if (jProperty != null)
				{
					jProperty.Value = value;
					return;
				}
				OnPropertyChanging(propertyName);
				Add(propertyName, value);
				OnPropertyChanged(propertyName);
			}
		}

		global::System.Collections.Generic.ICollection<string> global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Linq.JToken>.Keys => _properties.Keys;

		global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken?> global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Linq.JToken>.Values
		{
			get
			{
				throw new global::System.NotImplementedException();
			}
		}

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>>.IsReadOnly => false;

		public event global::System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

		public event global::System.ComponentModel.PropertyChangingEventHandler? PropertyChanging;

		public override global::System.Threading.Tasks.Task WriteToAsync(global::Newtonsoft.Json.JsonWriter writer, global::System.Threading.CancellationToken cancellationToken, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			global::System.Threading.Tasks.Task task = writer.WriteStartObjectAsync(cancellationToken);
			if (!global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
			{
				return AwaitProperties(task, 0, writer, cancellationToken, converters);
			}
			for (int i = 0; i < _properties.Count; i++)
			{
				task = _properties[i].WriteToAsync(writer, cancellationToken, converters);
				if (!global::Newtonsoft.Json.Utilities.AsyncUtils.IsCompletedSuccessfully(task))
				{
					return AwaitProperties(task, i + 1, writer, cancellationToken, converters);
				}
			}
			return writer.WriteEndObjectAsync(cancellationToken);
			async global::System.Threading.Tasks.Task AwaitProperties(global::System.Threading.Tasks.Task task2, int num, global::Newtonsoft.Json.JsonWriter Writer, global::System.Threading.CancellationToken CancellationToken, global::Newtonsoft.Json.JsonConverter[] Converters)
			{
				await task2.ConfigureAwait(continueOnCapturedContext: false);
				while (num < _properties.Count)
				{
					await _properties[num].WriteToAsync(Writer, CancellationToken, Converters).ConfigureAwait(continueOnCapturedContext: false);
					num++;
				}
				await Writer.WriteEndObjectAsync(CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		public new static global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JObject> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			return LoadAsync(reader, null, cancellationToken);
		}

		public new static async global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JObject> LoadAsync(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !(await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JObject from JsonReader.");
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartObject)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			global::Newtonsoft.Json.Linq.JObject o = new global::Newtonsoft.Json.Linq.JObject();
			o.SetLineInfo(reader as global::Newtonsoft.Json.IJsonLineInfo, settings);
			await o.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return o;
		}

		public JObject()
		{
		}

		public JObject(global::Newtonsoft.Json.Linq.JObject other)
			: base(other, null)
		{
		}

		internal JObject(global::Newtonsoft.Json.Linq.JObject other, global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
			: base(other, settings)
		{
		}

		public JObject(params object[] content)
			: this((object)content)
		{
		}

		public JObject(object content)
		{
			Add(content);
		}

		internal override bool DeepEquals(global::Newtonsoft.Json.Linq.JToken node)
		{
			if (!(node is global::Newtonsoft.Json.Linq.JObject jObject))
			{
				return false;
			}
			return _properties.Compare(jObject._properties);
		}

		internal override int IndexOfItem(global::Newtonsoft.Json.Linq.JToken? item)
		{
			if (item == null)
			{
				return -1;
			}
			return _properties.IndexOfReference(item);
		}

		internal override bool InsertItem(int index, global::Newtonsoft.Json.Linq.JToken? item, bool skipParentCheck, bool copyAnnotations)
		{
			if (item != null && item.Type == global::Newtonsoft.Json.Linq.JTokenType.Comment)
			{
				return false;
			}
			return base.InsertItem(index, item, skipParentCheck, copyAnnotations);
		}

		internal override void ValidateToken(global::Newtonsoft.Json.Linq.JToken o, global::Newtonsoft.Json.Linq.JToken? existing)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type != global::Newtonsoft.Json.Linq.JTokenType.Property)
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not add {0} to {1}.", global::System.Globalization.CultureInfo.InvariantCulture, o.GetType(), GetType()));
			}
			global::Newtonsoft.Json.Linq.JProperty jProperty = (global::Newtonsoft.Json.Linq.JProperty)o;
			if (existing != null)
			{
				global::Newtonsoft.Json.Linq.JProperty jProperty2 = (global::Newtonsoft.Json.Linq.JProperty)existing;
				if (jProperty.Name == jProperty2.Name)
				{
					return;
				}
			}
			if (_properties.TryGetValue(jProperty.Name, out existing))
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not add property {0} to {1}. Property with the same name already exists on object.", global::System.Globalization.CultureInfo.InvariantCulture, jProperty.Name, GetType()));
			}
		}

		internal override void MergeItem(object content, global::Newtonsoft.Json.Linq.JsonMergeSettings? settings)
		{
			if (!(content is global::Newtonsoft.Json.Linq.JObject jObject))
			{
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken> item in jObject)
			{
				global::Newtonsoft.Json.Linq.JProperty jProperty = Property(item.Key, settings?.PropertyNameComparison ?? global::System.StringComparison.Ordinal);
				if (jProperty == null)
				{
					Add(item.Key, item.Value);
				}
				else
				{
					if (item.Value == null)
					{
						continue;
					}
					if (!(jProperty.Value is global::Newtonsoft.Json.Linq.JContainer jContainer) || jContainer.Type != item.Value.Type)
					{
						if (!IsNull(item.Value) || (settings != null && settings.MergeNullValueHandling == global::Newtonsoft.Json.Linq.MergeNullValueHandling.Merge))
						{
							jProperty.Value = item.Value;
						}
					}
					else
					{
						jContainer.Merge(item.Value, settings);
					}
				}
			}
		}

		private static bool IsNull(global::Newtonsoft.Json.Linq.JToken token)
		{
			if (token.Type == global::Newtonsoft.Json.Linq.JTokenType.Null)
			{
				return true;
			}
			if (token is global::Newtonsoft.Json.Linq.JValue { Value: null })
			{
				return true;
			}
			return false;
		}

		internal void InternalPropertyChanged(global::Newtonsoft.Json.Linq.JProperty childProperty)
		{
			OnPropertyChanged(childProperty.Name);
			if (_listChanged != null)
			{
				OnListChanged(new global::System.ComponentModel.ListChangedEventArgs(global::System.ComponentModel.ListChangedType.ItemChanged, IndexOfItem(childProperty)));
			}
			if (_collectionChanged != null)
			{
				OnCollectionChanged(new global::System.Collections.Specialized.NotifyCollectionChangedEventArgs(global::System.Collections.Specialized.NotifyCollectionChangedAction.Replace, childProperty, childProperty, IndexOfItem(childProperty)));
			}
		}

		internal void InternalPropertyChanging(global::Newtonsoft.Json.Linq.JProperty childProperty)
		{
			OnPropertyChanging(childProperty.Name);
		}

		internal override global::Newtonsoft.Json.Linq.JToken CloneToken(global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
		{
			return new global::Newtonsoft.Json.Linq.JObject(this, settings);
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JProperty> Properties()
		{
			return global::System.Linq.Enumerable.Cast<global::Newtonsoft.Json.Linq.JProperty>(_properties);
		}

		public global::Newtonsoft.Json.Linq.JProperty? Property(string name)
		{
			return Property(name, global::System.StringComparison.Ordinal);
		}

		public global::Newtonsoft.Json.Linq.JProperty? Property(string name, global::System.StringComparison comparison)
		{
			if (name == null)
			{
				return null;
			}
			if (_properties.TryGetValue(name, out global::Newtonsoft.Json.Linq.JToken value))
			{
				return (global::Newtonsoft.Json.Linq.JProperty)value;
			}
			if (comparison != global::System.StringComparison.Ordinal)
			{
				for (int i = 0; i < _properties.Count; i++)
				{
					global::Newtonsoft.Json.Linq.JProperty jProperty = (global::Newtonsoft.Json.Linq.JProperty)_properties[i];
					if (string.Equals(jProperty.Name, name, comparison))
					{
						return jProperty;
					}
				}
			}
			return null;
		}

		public global::Newtonsoft.Json.Linq.JEnumerable<global::Newtonsoft.Json.Linq.JToken> PropertyValues()
		{
			return new global::Newtonsoft.Json.Linq.JEnumerable<global::Newtonsoft.Json.Linq.JToken>(global::System.Linq.Enumerable.Select(Properties(), (global::Newtonsoft.Json.Linq.JProperty p) => p.Value));
		}

		public new static global::Newtonsoft.Json.Linq.JObject Load(global::Newtonsoft.Json.JsonReader reader)
		{
			return Load(reader, null);
		}

		public new static global::Newtonsoft.Json.Linq.JObject Load(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None && !reader.Read())
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, "Error reading JObject from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartObject)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
			}
			global::Newtonsoft.Json.Linq.JObject jObject = new global::Newtonsoft.Json.Linq.JObject();
			jObject.SetLineInfo(reader as global::Newtonsoft.Json.IJsonLineInfo, settings);
			jObject.ReadTokenFrom(reader, settings);
			return jObject;
		}

		public new static global::Newtonsoft.Json.Linq.JObject Parse(string json)
		{
			return Parse(json, null);
		}

		public new static global::Newtonsoft.Json.Linq.JObject Parse(string json, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			using global::Newtonsoft.Json.JsonReader jsonReader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(json));
			global::Newtonsoft.Json.Linq.JObject result = Load(jsonReader, settings);
			while (jsonReader.Read())
			{
			}
			return result;
		}

		public new static global::Newtonsoft.Json.Linq.JObject FromObject(object o)
		{
			return FromObject(o, global::Newtonsoft.Json.JsonSerializer.CreateDefault());
		}

		public new static global::Newtonsoft.Json.Linq.JObject FromObject(object o, global::Newtonsoft.Json.JsonSerializer jsonSerializer)
		{
			global::Newtonsoft.Json.Linq.JToken jToken = global::Newtonsoft.Json.Linq.JToken.FromObjectInternal(o, jsonSerializer);
			if (jToken.Type != global::Newtonsoft.Json.Linq.JTokenType.Object)
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Object serialized to {0}. JObject instance expected.", global::System.Globalization.CultureInfo.InvariantCulture, jToken.Type));
			}
			return (global::Newtonsoft.Json.Linq.JObject)jToken;
		}

		public override void WriteTo(global::Newtonsoft.Json.JsonWriter writer, params global::Newtonsoft.Json.JsonConverter[] converters)
		{
			writer.WriteStartObject();
			for (int i = 0; i < _properties.Count; i++)
			{
				_properties[i].WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		public global::Newtonsoft.Json.Linq.JToken? GetValue(string? propertyName)
		{
			return GetValue(propertyName, global::System.StringComparison.Ordinal);
		}

		public global::Newtonsoft.Json.Linq.JToken? GetValue(string? propertyName, global::System.StringComparison comparison)
		{
			if (propertyName == null)
			{
				return null;
			}
			return Property(propertyName, comparison)?.Value;
		}

		public bool TryGetValue(string propertyName, global::System.StringComparison comparison, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::Newtonsoft.Json.Linq.JToken? value)
		{
			value = GetValue(propertyName, comparison);
			return value != null;
		}

		public void Add(string propertyName, global::Newtonsoft.Json.Linq.JToken? value)
		{
			Add(new global::Newtonsoft.Json.Linq.JProperty(propertyName, value));
		}

		public bool ContainsKey(string propertyName)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
			return _properties.Contains(propertyName);
		}

		public bool Remove(string propertyName)
		{
			global::Newtonsoft.Json.Linq.JProperty jProperty = Property(propertyName, global::System.StringComparison.Ordinal);
			if (jProperty == null)
			{
				return false;
			}
			jProperty.Remove();
			return true;
		}

		public bool TryGetValue(string propertyName, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::Newtonsoft.Json.Linq.JToken? value)
		{
			global::Newtonsoft.Json.Linq.JProperty jProperty = Property(propertyName, global::System.StringComparison.Ordinal);
			if (jProperty == null)
			{
				value = null;
				return false;
			}
			value = jProperty.Value;
			return true;
		}

		void global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>>.Add(global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken?> item)
		{
			Add(new global::Newtonsoft.Json.Linq.JProperty(item.Key, item.Value));
		}

		void global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>>.Clear()
		{
			RemoveAll();
		}

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>>.Contains(global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken?> item)
		{
			global::Newtonsoft.Json.Linq.JProperty jProperty = Property(item.Key, global::System.StringComparison.Ordinal);
			if (jProperty == null)
			{
				return false;
			}
			return jProperty.Value == item.Value;
		}

		void global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>>.CopyTo(global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken?>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new global::System.ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new global::System.ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (base.Count > array.Length - arrayIndex)
			{
				throw new global::System.ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (global::Newtonsoft.Json.Linq.JProperty property in _properties)
			{
				array[arrayIndex + num] = new global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>(property.Name, property.Value);
				num++;
			}
		}

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>>.Remove(global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken?> item)
		{
			if (!((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>>)this).Contains(item))
			{
				return false;
			}
			((global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Linq.JToken>)this).Remove(item.Key);
			return true;
		}

		internal override int GetDeepHashCode()
		{
			return ContentsHashCode();
		}

		public global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken?>> GetEnumerator()
		{
			foreach (global::Newtonsoft.Json.Linq.JProperty property in _properties)
			{
				yield return new global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>(property.Name, property.Value);
			}
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new global::System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanging(string propertyName)
		{
			this.PropertyChanging?.Invoke(this, new global::System.ComponentModel.PropertyChangingEventArgs(propertyName));
		}

		global::System.ComponentModel.PropertyDescriptorCollection global::System.ComponentModel.ICustomTypeDescriptor.GetProperties()
		{
			return ((global::System.ComponentModel.ICustomTypeDescriptor)this).GetProperties((global::System.Attribute[])null);
		}

		global::System.ComponentModel.PropertyDescriptorCollection global::System.ComponentModel.ICustomTypeDescriptor.GetProperties(global::System.Attribute[]? attributes)
		{
			global::System.ComponentModel.PropertyDescriptor[] array = new global::System.ComponentModel.PropertyDescriptor[base.Count];
			int num = 0;
			using (global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken>> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					array[num] = new global::Newtonsoft.Json.Linq.JPropertyDescriptor(enumerator.Current.Key);
					num++;
				}
			}
			return new global::System.ComponentModel.PropertyDescriptorCollection(array);
		}

		global::System.ComponentModel.AttributeCollection global::System.ComponentModel.ICustomTypeDescriptor.GetAttributes()
		{
			return global::System.ComponentModel.AttributeCollection.Empty;
		}

		string? global::System.ComponentModel.ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		string? global::System.ComponentModel.ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		global::System.ComponentModel.TypeConverter global::System.ComponentModel.ICustomTypeDescriptor.GetConverter()
		{
			return new global::System.ComponentModel.TypeConverter();
		}

		global::System.ComponentModel.EventDescriptor? global::System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		global::System.ComponentModel.PropertyDescriptor? global::System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		object? global::System.ComponentModel.ICustomTypeDescriptor.GetEditor(global::System.Type editorBaseType)
		{
			return null;
		}

		global::System.ComponentModel.EventDescriptorCollection global::System.ComponentModel.ICustomTypeDescriptor.GetEvents(global::System.Attribute[]? attributes)
		{
			return global::System.ComponentModel.EventDescriptorCollection.Empty;
		}

		global::System.ComponentModel.EventDescriptorCollection global::System.ComponentModel.ICustomTypeDescriptor.GetEvents()
		{
			return global::System.ComponentModel.EventDescriptorCollection.Empty;
		}

		object? global::System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner(global::System.ComponentModel.PropertyDescriptor? pd)
		{
			if (pd is global::Newtonsoft.Json.Linq.JPropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		protected override global::System.Dynamic.DynamicMetaObject GetMetaObject(global::System.Linq.Expressions.Expression parameter)
		{
			return new global::Newtonsoft.Json.Utilities.DynamicProxyMetaObject<global::Newtonsoft.Json.Linq.JObject>(parameter, this, new global::Newtonsoft.Json.Linq.JObject.JObjectDynamicProxy());
		}
	}
}
