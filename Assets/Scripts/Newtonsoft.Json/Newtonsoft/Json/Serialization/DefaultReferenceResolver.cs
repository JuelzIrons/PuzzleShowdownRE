namespace Newtonsoft.Json.Serialization
{
	internal class DefaultReferenceResolver : global::Newtonsoft.Json.Serialization.IReferenceResolver
	{
		private int _referenceCount;

		private global::Newtonsoft.Json.Utilities.BidirectionalDictionary<string, object> GetMappings(object context)
		{
			global::Newtonsoft.Json.Serialization.JsonSerializerInternalBase jsonSerializerInternalBase = context as global::Newtonsoft.Json.Serialization.JsonSerializerInternalBase;
			if (jsonSerializerInternalBase == null)
			{
				if (!(context is global::Newtonsoft.Json.Serialization.JsonSerializerProxy jsonSerializerProxy))
				{
					throw new global::Newtonsoft.Json.JsonException("The DefaultReferenceResolver can only be used internally.");
				}
				jsonSerializerInternalBase = jsonSerializerProxy.GetInternalSerializer();
			}
			return jsonSerializerInternalBase.DefaultReferenceMappings;
		}

		public object ResolveReference(object context, string reference)
		{
			GetMappings(context).TryGetByFirst(reference, out object second);
			return second;
		}

		public string GetReference(object context, object value)
		{
			global::Newtonsoft.Json.Utilities.BidirectionalDictionary<string, object> mappings = GetMappings(context);
			if (!mappings.TryGetBySecond(value, out string first))
			{
				_referenceCount++;
				first = _referenceCount.ToString(global::System.Globalization.CultureInfo.InvariantCulture);
				mappings.Set(first, value);
			}
			return first;
		}

		public void AddReference(object context, string reference, object value)
		{
			GetMappings(context).Set(reference, value);
		}

		public bool IsReferenced(object context, object value)
		{
			string first;
			return GetMappings(context).TryGetBySecond(value, out first);
		}
	}
}
