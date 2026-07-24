namespace Unity.Services.Matchmaker.Models
{
	public class BackfillTicket
	{
		public string Id { get; set; }

		public string Connection { get; set; }

		public global::System.Collections.Generic.Dictionary<string, object> Attributes { get; set; }

		public global::Unity.Services.Matchmaker.Models.BackfillTicketProperties Properties { get; set; }

		public BackfillTicket(string id = null, string connection = null, global::System.Collections.Generic.Dictionary<string, object> attributes = null, global::Unity.Services.Matchmaker.Models.BackfillTicketProperties properties = null)
		{
			Id = id;
			Connection = connection;
			Attributes = attributes;
			Properties = properties;
		}
	}
}
