namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "RegionsResponseBody")]
	public class RegionsResponseBody
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "data", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Relay.Models.RegionsData Data { get; }

		[global::UnityEngine.Scripting.Preserve]
		public RegionsResponseBody(global::Unity.Services.Relay.Models.RegionsData data)
		{
			Data = data;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
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
