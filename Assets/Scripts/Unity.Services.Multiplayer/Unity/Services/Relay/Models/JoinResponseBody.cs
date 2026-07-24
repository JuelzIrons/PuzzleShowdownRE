namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "JoinResponseBody")]
	public class JoinResponseBody
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "meta", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Relay.Models.ResponseMeta Meta { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "data", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Relay.Models.JoinData Data { get; }

		[global::UnityEngine.Scripting.Preserve]
		public JoinResponseBody(global::Unity.Services.Relay.Models.ResponseMeta meta, global::Unity.Services.Relay.Models.JoinData data)
		{
			Meta = meta;
			Data = data;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Meta != null)
			{
				text = text + "meta," + Meta.ToString() + ",";
			}
			if (Data != null)
			{
				text = text + "data," + Data.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			return new global::System.Collections.Generic.Dictionary<string, string>();
		}
	}
}
