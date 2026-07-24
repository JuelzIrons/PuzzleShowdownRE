namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "AllocationRequest")]
	public class AllocationRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "maxConnections", IsRequired = true, EmitDefaultValue = true)]
		public int MaxConnections { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "region", EmitDefaultValue = false)]
		public string Region { get; }

		[global::UnityEngine.Scripting.Preserve]
		public AllocationRequest(int maxConnections, string region = null)
		{
			MaxConnections = maxConnections;
			Region = region;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			text = text + "maxConnections," + MaxConnections + ",";
			if (Region != null)
			{
				text = text + "region," + Region;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			string value = MaxConnections.ToString();
			dictionary.Add("maxConnections", value);
			if (Region != null)
			{
				string value2 = Region.ToString();
				dictionary.Add("region", value2);
			}
			return dictionary;
		}
	}
}
