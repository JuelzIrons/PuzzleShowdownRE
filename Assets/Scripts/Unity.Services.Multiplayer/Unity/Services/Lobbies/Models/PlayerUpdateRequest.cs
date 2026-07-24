namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "PlayerUpdateRequest")]
	public class PlayerUpdateRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "connectionInfo", EmitDefaultValue = false)]
		public string ConnectionInfo { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Lobbies.Http.JsonObjectCollectionConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "data", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Http.JsonObject Data { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "allocationId", EmitDefaultValue = false)]
		public string AllocationId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public PlayerUpdateRequest(string connectionInfo = null, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject> data = null, string allocationId = null)
		{
			ConnectionInfo = connectionInfo;
			Data = new global::Unity.Services.Lobbies.Http.JsonObject(data);
			AllocationId = allocationId;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (ConnectionInfo != null)
			{
				text = text + "connectionInfo," + ConnectionInfo + ",";
			}
			if (Data != null)
			{
				text = text + "data," + Data.ToString() + ",";
			}
			if (AllocationId != null)
			{
				text = text + "allocationId," + AllocationId;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (ConnectionInfo != null)
			{
				string value = ConnectionInfo.ToString();
				dictionary.Add("connectionInfo", value);
			}
			if (AllocationId != null)
			{
				string value2 = AllocationId.ToString();
				dictionary.Add("allocationId", value2);
			}
			return dictionary;
		}
	}
}
