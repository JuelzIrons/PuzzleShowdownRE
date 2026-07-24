namespace Newtonsoft.Json.Serialization
{
	public class JsonPropertyCollection : global::System.Collections.ObjectModel.KeyedCollection<string, global::Newtonsoft.Json.Serialization.JsonProperty>
	{
		private readonly global::System.Type _type;

		private readonly global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.JsonProperty> _list;

		public JsonPropertyCollection(global::System.Type type)
			: base((global::System.Collections.Generic.IEqualityComparer<string>)global::System.StringComparer.Ordinal)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(type, "type");
			_type = type;
			_list = (global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.JsonProperty>)base.Items;
		}

		protected override string GetKeyForItem(global::Newtonsoft.Json.Serialization.JsonProperty item)
		{
			return item.PropertyName;
		}

		public void AddProperty(global::Newtonsoft.Json.Serialization.JsonProperty property)
		{
			if (Contains(property.PropertyName))
			{
				if (property.Ignored)
				{
					return;
				}
				global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = base[property.PropertyName];
				bool flag = true;
				if (jsonProperty.Ignored)
				{
					Remove(jsonProperty);
					flag = false;
				}
				else if (property.DeclaringType != null && jsonProperty.DeclaringType != null)
				{
					if (property.DeclaringType.IsSubclassOf(jsonProperty.DeclaringType) || (global::Newtonsoft.Json.Utilities.TypeExtensions.IsInterface(jsonProperty.DeclaringType) && global::Newtonsoft.Json.Utilities.TypeExtensions.ImplementInterface(property.DeclaringType, jsonProperty.DeclaringType)))
					{
						Remove(jsonProperty);
						flag = false;
					}
					if (jsonProperty.DeclaringType.IsSubclassOf(property.DeclaringType) || (global::Newtonsoft.Json.Utilities.TypeExtensions.IsInterface(property.DeclaringType) && global::Newtonsoft.Json.Utilities.TypeExtensions.ImplementInterface(jsonProperty.DeclaringType, property.DeclaringType)) || (global::Newtonsoft.Json.Utilities.TypeExtensions.ImplementInterface(_type, jsonProperty.DeclaringType) && global::Newtonsoft.Json.Utilities.TypeExtensions.ImplementInterface(_type, property.DeclaringType)))
					{
						return;
					}
				}
				if (flag)
				{
					throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("A member with the name '{0}' already exists on '{1}'. Use the JsonPropertyAttribute to specify another name.", global::System.Globalization.CultureInfo.InvariantCulture, property.PropertyName, _type));
				}
			}
			Add(property);
		}

		public global::Newtonsoft.Json.Serialization.JsonProperty? GetClosestMatchProperty(string propertyName)
		{
			global::Newtonsoft.Json.Serialization.JsonProperty property = GetProperty(propertyName, global::System.StringComparison.Ordinal);
			if (property == null)
			{
				property = GetProperty(propertyName, global::System.StringComparison.OrdinalIgnoreCase);
			}
			return property;
		}

		private bool TryGetProperty(string key, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::Newtonsoft.Json.Serialization.JsonProperty? item)
		{
			if (base.Dictionary == null)
			{
				item = null;
				return false;
			}
			return base.Dictionary.TryGetValue(key, out item);
		}

		public global::Newtonsoft.Json.Serialization.JsonProperty? GetProperty(string propertyName, global::System.StringComparison comparisonType)
		{
			if (comparisonType == global::System.StringComparison.Ordinal)
			{
				if (TryGetProperty(propertyName, out global::Newtonsoft.Json.Serialization.JsonProperty item))
				{
					return item;
				}
				return null;
			}
			for (int i = 0; i < _list.Count; i++)
			{
				global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = _list[i];
				if (string.Equals(propertyName, jsonProperty.PropertyName, comparisonType))
				{
					return jsonProperty;
				}
			}
			return null;
		}
	}
}
