namespace Unity.Services.Qos.V2.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "ErrorResponseBody")]
	internal class ErrorResponseBody
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
		public string Type { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "title", IsRequired = true, EmitDefaultValue = true)]
		public string Title { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "status", IsRequired = true, EmitDefaultValue = true)]
		public int Status { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "code", IsRequired = true, EmitDefaultValue = true)]
		public int Code { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "detail", IsRequired = true, EmitDefaultValue = true)]
		public string Detail { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "instance", EmitDefaultValue = false)]
		public string Instance { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "details", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Qos.V2.Http.IDeserializable> Details { get; }

		[global::UnityEngine.Scripting.Preserve]
		public ErrorResponseBody(string type, string title, int status, int code, string detail, string instance = null, global::System.Collections.Generic.List<object> details = null)
		{
			Type = type;
			Title = title;
			Status = status;
			Code = code;
			Detail = detail;
			Instance = instance;
			Details = global::Unity.Services.Qos.V2.Http.JsonObject.GetNewJsonObjectResponse(details);
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Type != null)
			{
				text = text + "type," + Type + ",";
			}
			if (Title != null)
			{
				text = text + "title," + Title + ",";
			}
			text = text + "status," + Status + ",";
			text = text + "code," + Code + ",";
			if (Detail != null)
			{
				text = text + "detail," + Detail + ",";
			}
			if (Instance != null)
			{
				text = text + "instance," + Instance + ",";
			}
			if (Details != null)
			{
				text = text + "details," + Details.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Type != null)
			{
				string value = Type.ToString();
				dictionary.Add("type", value);
			}
			if (Title != null)
			{
				string value2 = Title.ToString();
				dictionary.Add("title", value2);
			}
			string value3 = Status.ToString();
			dictionary.Add("status", value3);
			string value4 = Code.ToString();
			dictionary.Add("code", value4);
			if (Detail != null)
			{
				string value5 = Detail.ToString();
				dictionary.Add("detail", value5);
			}
			if (Instance != null)
			{
				string value6 = Instance.ToString();
				dictionary.Add("instance", value6);
			}
			if (Details != null)
			{
				string value7 = Details.ToString();
				dictionary.Add("details", value7);
			}
			return dictionary;
		}
	}
}
