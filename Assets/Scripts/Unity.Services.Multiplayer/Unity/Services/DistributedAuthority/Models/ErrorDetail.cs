namespace Unity.Services.DistributedAuthority.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "ErrorDetail")]
	internal class ErrorDetail
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "error", IsRequired = true, EmitDefaultValue = true)]
		public string Error { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "message", IsRequired = true, EmitDefaultValue = true)]
		public string Message { get; }

		[global::UnityEngine.Scripting.Preserve]
		public ErrorDetail(string error, string message)
		{
			Error = error;
			Message = message;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Error != null)
			{
				text = text + "error," + Error + ",";
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
			if (Error != null)
			{
				string value = Error.ToString();
				dictionary.Add("error", value);
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
