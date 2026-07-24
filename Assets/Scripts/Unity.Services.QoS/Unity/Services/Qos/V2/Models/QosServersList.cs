namespace Unity.Services.Qos.V2.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QosServersList")]
	internal class QosServersList
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "servers", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Collections.Generic.List<global::Unity.Services.Qos.V2.Models.QosServer> Servers { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QosServersList(global::System.Collections.Generic.List<global::Unity.Services.Qos.V2.Models.QosServer> servers)
		{
			Servers = servers;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Servers != null)
			{
				text = text + "servers," + Servers.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			return new global::System.Collections.Generic.Dictionary<string, string>();
		}
	}
}
