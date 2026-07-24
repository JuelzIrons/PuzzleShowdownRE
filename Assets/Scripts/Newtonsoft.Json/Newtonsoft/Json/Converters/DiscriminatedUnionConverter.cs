namespace Newtonsoft.Json.Converters
{
	public class DiscriminatedUnionConverter : global::Newtonsoft.Json.JsonConverter
	{
		internal class Union
		{
			public readonly global::Newtonsoft.Json.Utilities.FSharpFunction TagReader;

			public readonly global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase> Cases;

			public Union(global::Newtonsoft.Json.Utilities.FSharpFunction tagReader, global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase> cases)
			{
				TagReader = tagReader;
				Cases = cases;
			}
		}

		internal class UnionCase
		{
			public readonly int Tag;

			public readonly string Name;

			public readonly global::System.Reflection.PropertyInfo[] Fields;

			public readonly global::Newtonsoft.Json.Utilities.FSharpFunction FieldReader;

			public readonly global::Newtonsoft.Json.Utilities.FSharpFunction Constructor;

			public UnionCase(int tag, string name, global::System.Reflection.PropertyInfo[] fields, global::Newtonsoft.Json.Utilities.FSharpFunction fieldReader, global::Newtonsoft.Json.Utilities.FSharpFunction constructor)
			{
				Tag = tag;
				Name = name;
				Fields = fields;
				FieldReader = fieldReader;
				Constructor = constructor;
			}
		}

		private const string CasePropertyName = "Case";

		private const string FieldsPropertyName = "Fields";

		private static readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union> UnionCache = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union>(CreateUnion);

		private static readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::System.Type> UnionTypeLookupCache = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::System.Type>(CreateUnionTypeLookup);

		private static global::System.Type CreateUnionTypeLookup(global::System.Type t)
		{
			object arg = global::System.Linq.Enumerable.First((object[])global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.GetUnionCases(null, t, null));
			return (global::System.Type)global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.GetUnionCaseInfoDeclaringType(arg);
		}

		private static global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union CreateUnion(global::System.Type t)
		{
			global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union union = new global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union((global::Newtonsoft.Json.Utilities.FSharpFunction)global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.PreComputeUnionTagReader(null, t, null), new global::System.Collections.Generic.List<global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase>());
			object[] array = (object[])global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.GetUnionCases(null, t, null);
			foreach (object obj in array)
			{
				global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase item = new global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase((int)global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.GetUnionCaseInfoTag(obj), (string)global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.GetUnionCaseInfoName(obj), (global::System.Reflection.PropertyInfo[])global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.GetUnionCaseInfoFields(obj), (global::Newtonsoft.Json.Utilities.FSharpFunction)global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.PreComputeUnionReader(null, obj, null), (global::Newtonsoft.Json.Utilities.FSharpFunction)global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.PreComputeUnionConstructor(null, obj, null));
				union.Cases.Add(item);
			}
			return union;
		}

		public override void WriteJson(global::Newtonsoft.Json.JsonWriter writer, object? value, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			global::Newtonsoft.Json.Serialization.DefaultContractResolver defaultContractResolver = serializer.ContractResolver as global::Newtonsoft.Json.Serialization.DefaultContractResolver;
			global::System.Type key = UnionTypeLookupCache.Get(value.GetType());
			global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union union = UnionCache.Get(key);
			int tag = (int)union.TagReader.Invoke(value);
			global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase unionCase = global::System.Linq.Enumerable.Single(union.Cases, (global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase c) => c.Tag == tag);
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Case") : "Case");
			writer.WriteValue(unionCase.Name);
			if (unionCase.Fields != null && unionCase.Fields.Length != 0)
			{
				object[] obj = (object[])unionCase.FieldReader.Invoke(value);
				writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Fields") : "Fields");
				writer.WriteStartArray();
				object[] array = obj;
				foreach (object value2 in array)
				{
					serializer.Serialize(writer, value2);
				}
				writer.WriteEndArray();
			}
			writer.WriteEndObject();
		}

		public override object? ReadJson(global::Newtonsoft.Json.JsonReader reader, global::System.Type objectType, object? existingValue, global::Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase unionCase = null;
			string caseName = null;
			global::Newtonsoft.Json.Linq.JArray jArray = null;
			reader.ReadAndAssert();
			while (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
			{
				string text = reader.Value.ToString();
				if (string.Equals(text, "Case", global::System.StringComparison.OrdinalIgnoreCase))
				{
					reader.ReadAndAssert();
					global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union union = UnionCache.Get(objectType);
					caseName = reader.Value.ToString();
					unionCase = global::System.Linq.Enumerable.SingleOrDefault(union.Cases, (global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase c) => c.Name == caseName);
					if (unionCase == null)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("No union type found with the name '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, caseName));
					}
				}
				else
				{
					if (!string.Equals(text, "Fields", global::System.StringComparison.OrdinalIgnoreCase))
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected property '{0}' found when reading union.", global::System.Globalization.CultureInfo.InvariantCulture, text));
					}
					reader.ReadAndAssert();
					if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartArray)
					{
						throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, "Union fields must been an array.");
					}
					jArray = (global::Newtonsoft.Json.Linq.JArray)global::Newtonsoft.Json.Linq.JToken.ReadFrom(reader);
				}
				reader.ReadAndAssert();
			}
			if (unionCase == null)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("No '{0}' property with union name found.", global::System.Globalization.CultureInfo.InvariantCulture, "Case"));
			}
			object[] array = new object[unionCase.Fields.Length];
			if (unionCase.Fields.Length != 0 && jArray == null)
			{
				throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("No '{0}' property with union fields found.", global::System.Globalization.CultureInfo.InvariantCulture, "Fields"));
			}
			if (jArray != null)
			{
				if (unionCase.Fields.Length != jArray.Count)
				{
					throw global::Newtonsoft.Json.JsonSerializationException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("The number of field values does not match the number of properties defined by union '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, caseName));
				}
				for (int num = 0; num < jArray.Count; num++)
				{
					global::Newtonsoft.Json.Linq.JToken jToken = jArray[num];
					global::System.Reflection.PropertyInfo propertyInfo = unionCase.Fields[num];
					array[num] = jToken.ToObject(propertyInfo.PropertyType, serializer);
				}
			}
			object[] args = new object[1] { array };
			return unionCase.Constructor.Invoke(args);
		}

		public override bool CanConvert(global::System.Type objectType)
		{
			if (typeof(global::System.Collections.IEnumerable).IsAssignableFrom(objectType))
			{
				return false;
			}
			object[] customAttributes = objectType.GetCustomAttributes(inherit: true);
			bool flag = false;
			object[] array = customAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				global::System.Type type = array[i].GetType();
				if (type.FullName == "Microsoft.FSharp.Core.CompilationMappingAttribute")
				{
					global::Newtonsoft.Json.Utilities.FSharpUtils.EnsureInitialized(global::Newtonsoft.Json.Utilities.TypeExtensions.Assembly(type));
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
			return (bool)global::Newtonsoft.Json.Utilities.FSharpUtils.Instance.IsUnion(null, objectType, null);
		}
	}
}
