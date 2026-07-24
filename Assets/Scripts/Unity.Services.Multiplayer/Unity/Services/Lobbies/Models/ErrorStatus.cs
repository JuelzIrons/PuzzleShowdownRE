namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "ErrorStatus")]
	public class ErrorStatus
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "type", EmitDefaultValue = false)]
		public string Type { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "status", EmitDefaultValue = false)]
		public int Status { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "title", EmitDefaultValue = false)]
		public string Title { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "detail", EmitDefaultValue = false)]
		public string Detail { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "code", EmitDefaultValue = false)]
		public int Code { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "details", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Detail> Details { get; }

		[global::UnityEngine.Scripting.Preserve]
		public ErrorStatus(string type = null, int status = 0, string title = null, string detail = null, int code = 0, global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Detail> details = null)
		{
			Type = type;
			Status = status;
			Title = title;
			Detail = detail;
			Code = code;
			Details = details;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Type != null)
			{
				text = text + "type," + Type + ",";
			}
			text = text + "status," + Status + ",";
			if (Title != null)
			{
				text = text + "title," + Title + ",";
			}
			if (Detail != null)
			{
				text = text + "detail," + Detail + ",";
			}
			text = text + "code," + Code + ",";
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
			string value2 = Status.ToString();
			dictionary.Add("status", value2);
			if (Title != null)
			{
				string value3 = Title.ToString();
				dictionary.Add("title", value3);
			}
			if (Detail != null)
			{
				string value4 = Detail.ToString();
				dictionary.Add("detail", value4);
			}
			string value5 = Code.ToString();
			dictionary.Add("code", value5);
			return dictionary;
		}
	}
}
