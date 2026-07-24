namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "MultiplayAssignment")]
	public class MultiplayAssignment
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
		public global::Unity.Services.Matchmaker.Models.MultiplayAssignment.StatusOptions Status { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "ip", EmitDefaultValue = false)]
		public string Ip { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "port", EmitDefaultValue = false)]
		public int? Port { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "matchId", EmitDefaultValue = false)]
		public string MatchId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public MultiplayAssignment(string assignmentType, string message = null, global::Unity.Services.Matchmaker.Models.MultiplayAssignment.StatusOptions status = (global::Unity.Services.Matchmaker.Models.MultiplayAssignment.StatusOptions)0, string ip = null, int? port = null, string matchId = null)
		{
			AssignmentType = assignmentType;
			Message = message;
			Status = status;
			Ip = ip;
			Port = port;
			MatchId = matchId;
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
			if (Ip != null)
			{
				text = text + "ip," + Ip + ",";
			}
			if (Port.HasValue)
			{
				text = text + "port," + Port + ",";
			}
			if (MatchId != null)
			{
				text = text + "matchId," + MatchId;
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
			if (Ip != null)
			{
				string value4 = Ip.ToString();
				dictionary.Add("ip", value4);
			}
			if (Port.HasValue)
			{
				string value5 = Port.ToString();
				dictionary.Add("port", value5);
			}
			if (MatchId != null)
			{
				string value6 = MatchId.ToString();
				dictionary.Add("matchId", value6);
			}
			return dictionary;
		}
	}
}
