namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "BackfillTicketProperties")]
	public class BackfillTicketProperties
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "matchProperties", EmitDefaultValue = false)]
		public global::Unity.Services.Matchmaker.Models.MatchProperties MatchProperties { get; }

		[global::UnityEngine.Scripting.Preserve]
		public BackfillTicketProperties(global::Unity.Services.Matchmaker.Models.MatchProperties matchProperties = null)
		{
			MatchProperties = matchProperties;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (MatchProperties != null)
			{
				text = text + "matchProperties," + MatchProperties.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			return new global::System.Collections.Generic.Dictionary<string, string>();
		}
	}
}
