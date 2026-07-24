namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "Player")]
	public class Player
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
		public string Id { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Http.JsonObjectConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "customData", EmitDefaultValue = false)]
		public global::Unity.Services.Matchmaker.Http.IDeserializable CustomData { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "qosResults", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.QosResult> QosResults { get; }

		[global::UnityEngine.Scripting.Preserve]
		public Player(string id, object customData = null, global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.QosResult> qosResults = null)
		{
			Id = id;
			CustomData = global::Unity.Services.Matchmaker.Http.JsonObject.GetNewJsonObjectResponse(customData);
			QosResults = qosResults;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Id != null)
			{
				text = text + "id," + Id + ",";
			}
			if (CustomData != null)
			{
				text = text + "customData," + CustomData.ToString() + ",";
			}
			if (QosResults != null)
			{
				text = text + "qosResults," + QosResults.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Id != null)
			{
				string value = Id.ToString();
				dictionary.Add("id", value);
			}
			return dictionary;
		}
	}
}
