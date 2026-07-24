namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "RelayConnectionDetails")]
	public class RelayConnectionDetails
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
		public string Type { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "joinCode", EmitDefaultValue = false)]
		public string JoinCode { get; }

		[global::UnityEngine.Scripting.Preserve]
		public RelayConnectionDetails(string type, string joinCode = null)
		{
			Type = type;
			JoinCode = joinCode;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Type != null)
			{
				text = text + "type," + Type + ",";
			}
			if (JoinCode != null)
			{
				text = text + "joinCode," + JoinCode;
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
			if (JoinCode != null)
			{
				string value2 = JoinCode.ToString();
				dictionary.Add("joinCode", value2);
			}
			return dictionary;
		}
	}
}
