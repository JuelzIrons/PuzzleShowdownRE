namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Models.ConnectionDetailsOneOfJsonConverter))]
	[global::System.Runtime.Serialization.DataContract(Name = "ConnectionDetailsOneOf")]
	internal class ConnectionDetailsOneOf : global::Unity.Services.Matchmaker.Models.IOneOf
	{
		private const string DiscriminatorKey = "type";

		private static global::System.Collections.Generic.Dictionary<string, global::System.Type> TypeLookup = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
		{
			{
				"Custom",
				typeof(global::Unity.Services.Matchmaker.Models.CustomConnectionDetails)
			},
			{
				"IpPort",
				typeof(global::Unity.Services.Matchmaker.Models.IpPortConnectionDetails)
			},
			{
				"CustomConnectionDetails",
				typeof(global::Unity.Services.Matchmaker.Models.CustomConnectionDetails)
			},
			{
				"IpPortConnectionDetails",
				typeof(global::Unity.Services.Matchmaker.Models.IpPortConnectionDetails)
			}
		};

		private static global::System.Collections.Generic.List<global::System.Type> PossibleTypes = new global::System.Collections.Generic.List<global::System.Type>
		{
			typeof(global::Unity.Services.Matchmaker.Models.CustomConnectionDetails),
			typeof(global::Unity.Services.Matchmaker.Models.IpPortConnectionDetails)
		};

		public object Value { get; }

		public global::System.Type Type { get; }

		public ConnectionDetailsOneOf(object value, global::System.Type type)
		{
			Value = value;
			Type = type;
		}

		private static global::System.Type GetConcreteType(string type)
		{
			if (!TypeLookup.ContainsKey(type))
			{
				string text = string.Join(", ", global::System.Linq.Enumerable.ToList(TypeLookup.Keys));
				throw new global::System.ArgumentException("Failed to lookup discriminator value for " + type + ". Possible values: " + text);
			}
			return TypeLookup[type];
		}

		public static global::Unity.Services.Matchmaker.Models.ConnectionDetailsOneOf FromJson(string jsonString)
		{
			if (jsonString == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty("type"))
			{
				return DeserializeIntoActualObject(jsonString);
			}
			global::Newtonsoft.Json.Linq.JObject jObject = global::Newtonsoft.Json.Linq.JObject.Parse(jsonString);
			if (!jObject.ContainsKey("type"))
			{
				throw new global::System.MissingFieldException("ConnectionDetailsOneOf", "type");
			}
			return DeserializeIntoActualObject(jObject["type"].ToString(), jsonString);
		}

		private static global::Unity.Services.Matchmaker.Models.ConnectionDetailsOneOf DeserializeIntoActualObject(string discriminatorValue, string jsonString)
		{
			global::System.Type concreteType = GetConcreteType(discriminatorValue);
			if (concreteType == null)
			{
				string text = string.Join(", ", global::System.Linq.Enumerable.ToList(TypeLookup.Keys));
				throw new global::System.IO.InvalidDataException("Failed to lookup discriminator value for " + discriminatorValue + ". Possible values: " + text);
			}
			return new global::Unity.Services.Matchmaker.Models.ConnectionDetailsOneOf(global::Unity.Services.Matchmaker.Http.IsolatedJsonConvert.DeserializeObject(jsonString, concreteType), concreteType);
		}

		private static global::Unity.Services.Matchmaker.Models.ConnectionDetailsOneOf DeserializeIntoActualObject(string jsonString)
		{
			global::System.Collections.Generic.List<(object, global::System.Type)> list = new global::System.Collections.Generic.List<(object, global::System.Type)>();
			foreach (global::System.Type possibleType in PossibleTypes)
			{
				try
				{
					object item = global::Unity.Services.Matchmaker.Http.IsolatedJsonConvert.DeserializeObject(jsonString, possibleType);
					list.Add((item, possibleType));
				}
				catch (global::System.Exception)
				{
				}
			}
			if (global::System.Linq.Enumerable.Count(list) == 0)
			{
				throw new global::Unity.Services.Matchmaker.Http.ResponseDeserializationException("Could not deserialize into any of possible types. Possible types are: " + string.Join(", ", PossibleTypes));
			}
			if (global::System.Linq.Enumerable.Count(list) > 1)
			{
				throw new global::Unity.Services.Matchmaker.Http.ResponseDeserializationException("Could not deserialize; type is ambiguous. Possible types are: " + string.Join(", ", global::System.Linq.Enumerable.Select<(object, global::System.Type), global::System.Type>(list, ((object ActualObject, global::System.Type ActualType) p) => p.ActualType)));
			}
			return new global::Unity.Services.Matchmaker.Models.ConnectionDetailsOneOf(global::System.Linq.Enumerable.First(list).Item1, global::System.Linq.Enumerable.First(list).Item2);
		}
	}
}
