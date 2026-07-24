namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Models.TicketStatusResponseJsonConverter))]
	[global::System.Runtime.Serialization.DataContract(Name = "TicketStatusResponse")]
	public class TicketStatusResponse : global::Unity.Services.Matchmaker.Models.IOneOf
	{
		private const string DiscriminatorKey = "assignmentType";

		private static global::System.Collections.Generic.Dictionary<string, global::System.Type> TypeLookup = new global::System.Collections.Generic.Dictionary<string, global::System.Type>
		{
			{
				"CustomAssignment",
				typeof(global::Unity.Services.Matchmaker.Models.CustomAssignment)
			},
			{
				"IpPortAssignment",
				typeof(global::Unity.Services.Matchmaker.Models.IpPortAssignment)
			},
			{
				"MatchIdAssignment",
				typeof(global::Unity.Services.Matchmaker.Models.MatchIdAssignment)
			},
			{
				"MultiplayAssignment",
				typeof(global::Unity.Services.Matchmaker.Models.MultiplayAssignment)
			},
			{
				"None",
				typeof(global::Unity.Services.Matchmaker.Models.NoneAssignment)
			}
		};

		private static global::System.Collections.Generic.List<global::System.Type> PossibleTypes = new global::System.Collections.Generic.List<global::System.Type>
		{
			typeof(global::Unity.Services.Matchmaker.Models.CustomAssignment),
			typeof(global::Unity.Services.Matchmaker.Models.IpPortAssignment),
			typeof(global::Unity.Services.Matchmaker.Models.MatchIdAssignment),
			typeof(global::Unity.Services.Matchmaker.Models.MultiplayAssignment),
			typeof(global::Unity.Services.Matchmaker.Models.NoneAssignment)
		};

		public object Value { get; }

		public global::System.Type Type { get; }

		public TicketStatusResponse(object value, global::System.Type type)
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

		public static global::Unity.Services.Matchmaker.Models.TicketStatusResponse FromJson(string jsonString)
		{
			if (jsonString == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty("assignmentType"))
			{
				return DeserializeIntoActualObject(jsonString);
			}
			global::Newtonsoft.Json.Linq.JObject jObject = global::Newtonsoft.Json.Linq.JObject.Parse(jsonString);
			if (!jObject.ContainsKey("assignmentType"))
			{
				throw new global::System.MissingFieldException("TicketStatusResponse", "assignmentType");
			}
			return DeserializeIntoActualObject(jObject["assignmentType"].ToString(), jsonString);
		}

		private static global::Unity.Services.Matchmaker.Models.TicketStatusResponse DeserializeIntoActualObject(string discriminatorValue, string jsonString)
		{
			global::System.Type concreteType = GetConcreteType(discriminatorValue);
			if (concreteType == null)
			{
				string text = string.Join(", ", global::System.Linq.Enumerable.ToList(TypeLookup.Keys));
				throw new global::System.IO.InvalidDataException("Failed to lookup discriminator value for " + discriminatorValue + ". Possible values: " + text);
			}
			return new global::Unity.Services.Matchmaker.Models.TicketStatusResponse(global::Unity.Services.Matchmaker.Http.IsolatedJsonConvert.DeserializeObject(jsonString, concreteType), concreteType);
		}

		private static global::Unity.Services.Matchmaker.Models.TicketStatusResponse DeserializeIntoActualObject(string jsonString)
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
			return new global::Unity.Services.Matchmaker.Models.TicketStatusResponse(global::System.Linq.Enumerable.First(list).Item1, global::System.Linq.Enumerable.First(list).Item2);
		}
	}
}
