namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "RegionsData")]
	public class RegionsData
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "regions", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.Region> Regions { get; }

		[global::UnityEngine.Scripting.Preserve]
		public RegionsData(global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.Region> regions)
		{
			Regions = regions;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Regions != null)
			{
				text = text + "regions," + Regions.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			return new global::System.Collections.Generic.Dictionary<string, string>();
		}
	}
}
