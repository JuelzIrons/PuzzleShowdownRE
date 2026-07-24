namespace Unity.Services.Qos.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QosServer")]
	internal class QosServer
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "endpoints", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Collections.Generic.List<string> Endpoints { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "region", IsRequired = true, EmitDefaultValue = true)]
		public string Region { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "services", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> Services { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QosServer(global::System.Collections.Generic.List<string> endpoints, string region, global::System.Collections.Generic.List<string> services = null)
		{
			Endpoints = endpoints;
			Region = region;
			Services = services;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Endpoints != null)
			{
				text = text + "endpoints," + Endpoints.ToString() + ",";
			}
			if (Region != null)
			{
				text = text + "region," + Region + ",";
			}
			if (Services != null)
			{
				text = text + "services," + Services.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Endpoints != null)
			{
				string value = Endpoints.ToString();
				dictionary.Add("endpoints", value);
			}
			if (Region != null)
			{
				string value2 = Region.ToString();
				dictionary.Add("region", value2);
			}
			if (Services != null)
			{
				string value3 = Services.ToString();
				dictionary.Add("services", value3);
			}
			return dictionary;
		}
	}
}
