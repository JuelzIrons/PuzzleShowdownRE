namespace Unity.Services.Qos.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QosServiceServersList")]
	internal class QosServiceServersList
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "servers", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Collections.Generic.List<global::Unity.Services.Qos.Models.QosServiceServer> Servers { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QosServiceServersList(global::System.Collections.Generic.List<global::Unity.Services.Qos.Models.QosServiceServer> servers)
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
