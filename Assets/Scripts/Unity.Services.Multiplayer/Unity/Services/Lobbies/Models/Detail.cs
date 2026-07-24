namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "Detail")]
	public class Detail
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "errorType", EmitDefaultValue = false)]
		public string ErrorType { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "message", EmitDefaultValue = false)]
		public string Message { get; }

		[global::UnityEngine.Scripting.Preserve]
		public Detail(string errorType = null, string message = null)
		{
			ErrorType = errorType;
			Message = message;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (ErrorType != null)
			{
				text = text + "errorType," + ErrorType + ",";
			}
			if (Message != null)
			{
				text = text + "message," + Message;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (ErrorType != null)
			{
				string value = ErrorType.ToString();
				dictionary.Add("errorType", value);
			}
			if (Message != null)
			{
				string value2 = Message.ToString();
				dictionary.Add("message", value2);
			}
			return dictionary;
		}
	}
}
