namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "CreateBackfillTicketResponse")]
	public class CreateBackfillTicketResponse
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; }

		[global::UnityEngine.Scripting.Preserve]
		public CreateBackfillTicketResponse(string id = null)
		{
			Id = id;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Id != null)
			{
				text = text + "id," + Id;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Id != null)
			{
				string value = Id.ToString();
				dictionary.Add("id", value);
			}
			return dictionary;
		}
	}
}
