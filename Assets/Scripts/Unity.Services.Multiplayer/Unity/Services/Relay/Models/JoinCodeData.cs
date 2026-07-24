namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "JoinCodeData")]
	public class JoinCodeData
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "joinCode", IsRequired = true, EmitDefaultValue = true)]
		public string JoinCode { get; }

		[global::UnityEngine.Scripting.Preserve]
		public JoinCodeData(string joinCode)
		{
			JoinCode = joinCode;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (JoinCode != null)
			{
				text = text + "joinCode," + JoinCode;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (JoinCode != null)
			{
				string value = JoinCode.ToString();
				dictionary.Add("joinCode", value);
			}
			return dictionary;
		}
	}
}
