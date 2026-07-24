namespace Unity.Services.Qos.V2.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QosServer")]
	public class QosServer
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "endpoints", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Collections.Generic.List<string> Endpoints { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "annotations", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Qos.V2.Models.QosServerAnnotations Annotations { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QosServer(global::System.Collections.Generic.List<string> endpoints, global::Unity.Services.Qos.V2.Models.QosServerAnnotations annotations)
		{
			Endpoints = endpoints;
			Annotations = annotations;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Endpoints != null)
			{
				text = text + "endpoints," + Endpoints.ToString() + ",";
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
			return dictionary;
		}
	}
}
