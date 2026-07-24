namespace Unity.Services.Qos.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QosServiceServersResponseBody")]
	internal class QosServiceServersResponseBody
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "data", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Qos.Models.QosServiceServersList Data { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QosServiceServersResponseBody(global::Unity.Services.Qos.Models.QosServiceServersList data)
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
