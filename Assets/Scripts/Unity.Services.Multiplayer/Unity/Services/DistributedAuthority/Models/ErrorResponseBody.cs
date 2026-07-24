namespace Unity.Services.DistributedAuthority.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "ErrorResponseBody")]
	internal class ErrorResponseBody
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "status", IsRequired = true, EmitDefaultValue = true)]
		public int Status { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "detail", EmitDefaultValue = false)]
		public string Detail { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "title", EmitDefaultValue = false)]
		public string Title { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "details", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.Models.ErrorDetail> Details { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "type", EmitDefaultValue = false)]
		public string Type { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "code", EmitDefaultValue = false)]
		public int Code { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "requestId", IsRequired = true, EmitDefaultValue = true)]
		public string RequestId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public ErrorResponseBody(int status, string requestId, string detail = null, string title = null, global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.Models.ErrorDetail> details = null, string type = null, int code = 0)
		{
			Status = status;
			Detail = detail;
			Title = title;
			Details = details;
			Type = type;
			Code = code;
			RequestId = requestId;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			text = text + "status," + Status + ",";
			if (Detail != null)
			{
				text = text + "detail," + Detail + ",";
			}
			if (Title != null)
			{
				text = text + "title," + Title + ",";
			}
			if (Details != null)
			{
				text = text + "details," + Details.ToString() + ",";
			}
			if (Type != null)
			{
				text = text + "type," + Type + ",";
			}
			text = text + "code," + Code + ",";
			if (RequestId != null)
			{
				text = text + "requestId," + RequestId;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			string value = Status.ToString();
			dictionary.Add("status", value);
			if (Detail != null)
			{
				string value2 = Detail.ToString();
				dictionary.Add("detail", value2);
			}
			if (Title != null)
			{
				string value3 = Title.ToString();
				dictionary.Add("title", value3);
			}
			if (Type != null)
			{
				string value4 = Type.ToString();
				dictionary.Add("type", value4);
			}
			string value5 = Code.ToString();
			dictionary.Add("code", value5);
			if (RequestId != null)
			{
				string value6 = RequestId.ToString();
				dictionary.Add("requestId", value6);
			}
			return dictionary;
		}
	}
}
