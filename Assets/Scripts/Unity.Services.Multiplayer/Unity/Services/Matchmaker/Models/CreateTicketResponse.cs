namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "CreateTicketResponse")]
	public class CreateTicketResponse
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "abTestingResult", EmitDefaultValue = false)]
		public global::Unity.Services.Matchmaker.Models.AbTestingResult AbTestingResult { get; }

		[global::UnityEngine.Scripting.Preserve]
		public CreateTicketResponse(string id = null, global::Unity.Services.Matchmaker.Models.AbTestingResult abTestingResult = null)
		{
			Id = id;
			AbTestingResult = abTestingResult;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Id != null)
			{
				text = text + "id," + Id + ",";
			}
			if (AbTestingResult != null)
			{
				text = text + "abTestingResult," + AbTestingResult.ToString();
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
