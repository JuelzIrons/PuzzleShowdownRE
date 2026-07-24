namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "ProblemDetails")]
	internal class ProblemDetails
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "type", EmitDefaultValue = false)]
		public string Type { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "title", EmitDefaultValue = false)]
		public string Title { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Http.JsonObjectConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "errors", EmitDefaultValue = false)]
		public global::Unity.Services.Matchmaker.Http.IDeserializable Errors { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "status", EmitDefaultValue = false)]
		public int? Status { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "detail", EmitDefaultValue = false)]
		public string Detail { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "instance", EmitDefaultValue = false)]
		public string Instance { get; }

		[global::UnityEngine.Scripting.Preserve]
		public ProblemDetails(string type = null, string title = null, object errors = null, int? status = null, string detail = null, string instance = null)
		{
			Type = type;
			Title = title;
			Errors = global::Unity.Services.Matchmaker.Http.JsonObject.GetNewJsonObjectResponse(errors);
			Status = status;
			Detail = detail;
			Instance = instance;
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
			if (Errors != null)
			{
				text = text + "errors," + Errors.ToString() + ",";
			}
			if (Status.HasValue)
			{
				text = text + "status," + Status + ",";
			}
			if (Detail != null)
			{
				text = text + "detail," + Detail + ",";
			}
			if (Instance != null)
			{
				text = text + "instance," + Instance;
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
			if (Status.HasValue)
			{
				string value3 = Status.ToString();
				dictionary.Add("status", value3);
			}
			if (Detail != null)
			{
				string value4 = Detail.ToString();
				dictionary.Add("detail", value4);
			}
			if (Instance != null)
			{
				string value5 = Instance.ToString();
				dictionary.Add("instance", value5);
			}
			return dictionary;
		}
	}
}
