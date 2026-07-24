namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "ResponseMeta")]
	public class ResponseMeta
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "requestId", IsRequired = true, EmitDefaultValue = true)]
		public string RequestId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "status", IsRequired = true, EmitDefaultValue = true)]
		public int Status { get; }

		[global::UnityEngine.Scripting.Preserve]
		public ResponseMeta(string requestId, int status)
		{
			RequestId = requestId;
			Status = status;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (RequestId != null)
			{
				text = text + "requestId," + RequestId + ",";
			}
			return text + "status," + Status;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (RequestId != null)
			{
				string value = RequestId.ToString();
				dictionary.Add("requestId", value);
			}
			string value2 = Status.ToString();
			dictionary.Add("status", value2);
			return dictionary;
		}
	}
}
