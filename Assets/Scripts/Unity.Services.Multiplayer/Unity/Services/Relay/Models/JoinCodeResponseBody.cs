namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "JoinCodeResponseBody")]
	public class JoinCodeResponseBody
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "meta", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Relay.Models.ResponseMeta Meta { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "links", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Relay.Models.ResponseLinks Links { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "data", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Relay.Models.JoinCodeData Data { get; }

		[global::UnityEngine.Scripting.Preserve]
		public JoinCodeResponseBody(global::Unity.Services.Relay.Models.ResponseMeta meta, global::Unity.Services.Relay.Models.ResponseLinks links, global::Unity.Services.Relay.Models.JoinCodeData data)
		{
			Meta = meta;
			Links = links;
			Data = data;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Meta != null)
			{
				text = text + "meta," + Meta.ToString() + ",";
			}
			if (Links != null)
			{
				text = text + "links," + Links.ToString() + ",";
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
