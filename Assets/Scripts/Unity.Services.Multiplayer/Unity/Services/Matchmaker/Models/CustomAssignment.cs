namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "CustomAssignment")]
	public class CustomAssignment
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		public enum StatusOptions
		{
			[global::System.Runtime.Serialization.EnumMember(Value = "Timeout")]
			Timeout = 1,
			[global::System.Runtime.Serialization.EnumMember(Value = "Failed")]
			Failed = 2,
			[global::System.Runtime.Serialization.EnumMember(Value = "InProgress")]
			InProgress = 3,
			[global::System.Runtime.Serialization.EnumMember(Value = "Found")]
			Found = 4
		}

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "assignmentType", IsRequired = true, EmitDefaultValue = true)]
		public string AssignmentType { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "message", EmitDefaultValue = false)]
		public string Message { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "status", EmitDefaultValue = false)]
		public global::Unity.Services.Matchmaker.Models.CustomAssignment.StatusOptions Status { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "matchId", EmitDefaultValue = false)]
		public string MatchId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Http.JsonObjectCollectionConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "customData", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Matchmaker.Http.IDeserializable> CustomData { get; }

		[global::UnityEngine.Scripting.Preserve]
		public CustomAssignment(string assignmentType, string message = null, global::Unity.Services.Matchmaker.Models.CustomAssignment.StatusOptions status = (global::Unity.Services.Matchmaker.Models.CustomAssignment.StatusOptions)0, string matchId = null, global::System.Collections.Generic.Dictionary<string, object> customData = null)
		{
			AssignmentType = assignmentType;
			Message = message;
			Status = status;
			MatchId = matchId;
			CustomData = global::Unity.Services.Matchmaker.Http.JsonObject.GetNewJsonObjectResponse(customData);
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (AssignmentType != null)
			{
				text = text + "assignmentType," + AssignmentType + ",";
			}
			if (Message != null)
			{
				text = text + "message," + Message + ",";
			}
			text = text + "status," + Status.ToString() + ",";
			if (MatchId != null)
			{
				text = text + "matchId," + MatchId + ",";
			}
			if (CustomData != null)
			{
				text = text + "customData," + CustomData.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (AssignmentType != null)
			{
				string value = AssignmentType.ToString();
				dictionary.Add("assignmentType", value);
			}
			if (Message != null)
			{
				string value2 = Message.ToString();
				dictionary.Add("message", value2);
			}
			string value3 = Status.ToString();
			dictionary.Add("status", value3);
			if (MatchId != null)
			{
				string value4 = MatchId.ToString();
				dictionary.Add("matchId", value4);
			}
			if (CustomData != null)
			{
				string value5 = CustomData.ToString();
				dictionary.Add("customData", value5);
			}
			return dictionary;
		}
	}
}
