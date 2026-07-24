namespace Unity.Services.Qos.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QosServiceServer")]
	internal class QosServiceServer
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "endpoints", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Collections.Generic.List<string> Endpoints { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "region", IsRequired = true, EmitDefaultValue = true)]
		public string Region { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "annotations", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> Annotations { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QosServiceServer(global::System.Collections.Generic.List<string> endpoints, string region, global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> annotations = null)
		{
			Endpoints = endpoints;
			Region = region;
			Annotations = annotations;
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
			if (Annotations != null)
			{
				text = text + "annotations," + Annotations.ToString();
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
			if (Annotations != null)
			{
				string value3 = Annotations.ToString();
				dictionary.Add("annotations", value3);
			}
			return dictionary;
		}
	}
}
