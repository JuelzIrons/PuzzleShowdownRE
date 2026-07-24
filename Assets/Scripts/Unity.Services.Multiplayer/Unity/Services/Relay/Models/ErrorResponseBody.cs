namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "ErrorResponseBody")]
	public class ErrorResponseBody
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "status", IsRequired = true, EmitDefaultValue = true)]
		public int Status { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "detail", IsRequired = true, EmitDefaultValue = true)]
		public string Detail { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "title", IsRequired = true, EmitDefaultValue = true)]
		public string Title { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "details", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.KeyValuePair> Details { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
		public string Type { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "code", IsRequired = true, EmitDefaultValue = true)]
		public int Code { get; }

		[global::UnityEngine.Scripting.Preserve]
		public ErrorResponseBody(int status, string detail, string title, string type, int code, global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.KeyValuePair> details = null)
		{
			Status = status;
			Detail = detail;
			Title = title;
			Details = details;
			Type = type;
			Code = code;
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
			return text + "code," + Code;
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
			return dictionary;
		}
	}
}
