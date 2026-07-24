namespace Unity.Services.Qos.V2.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QosServersResponseBody")]
	internal class QosServersResponseBody
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "data", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Qos.V2.Models.QosServersList Data { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QosServersResponseBody(global::Unity.Services.Qos.V2.Models.QosServersList data)
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
