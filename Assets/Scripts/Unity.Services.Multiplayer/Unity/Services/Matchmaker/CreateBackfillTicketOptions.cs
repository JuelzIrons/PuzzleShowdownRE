namespace Unity.Services.Matchmaker
{
	public class CreateBackfillTicketOptions
	{
		public string QueueName { get; set; }

		public string Connection { get; set; }

		internal global::Unity.Services.Matchmaker.Models.ConnectionDetails ConnectionDetails { get; private set; }

		public global::System.Collections.Generic.Dictionary<string, object> Attributes { get; set; }

		public global::Unity.Services.Matchmaker.Models.BackfillTicketProperties Properties { get; set; }

		public string PoolId { get; set; }

		public string MatchId { get; set; }

		public CreateBackfillTicketOptions()
		{
		}

		public CreateBackfillTicketOptions(string queueName, string connection = null, global::System.Collections.Generic.Dictionary<string, object> attributes = null, global::Unity.Services.Matchmaker.Models.BackfillTicketProperties properties = null, string poolId = null, string matchId = null)
		{
			QueueName = queueName;
			Connection = connection;
			Attributes = attributes;
			Properties = properties;
			PoolId = poolId;
			MatchId = matchId;
		}

		public global::Unity.Services.Matchmaker.CreateBackfillTicketOptions WithIpPortConnection(string ip, uint port, global::System.Collections.Generic.Dictionary<string, object> data = null)
		{
			global::Unity.Services.Matchmaker.Models.IpPortConnectionDetails value = new global::Unity.Services.Matchmaker.Models.IpPortConnectionDetails("IpPort", ip, (int)port, data);
			ConnectionDetails = new global::Unity.Services.Matchmaker.Models.ConnectionDetails(value, typeof(global::Unity.Services.Matchmaker.Models.IpPortConnectionDetails));
			return this;
		}

		public global::Unity.Services.Matchmaker.CreateBackfillTicketOptions WithCustomConnection(global::System.Collections.Generic.Dictionary<string, object> data = null)
		{
			global::Unity.Services.Matchmaker.Models.CustomConnectionDetails value = new global::Unity.Services.Matchmaker.Models.CustomConnectionDetails("Custom", data);
			ConnectionDetails = new global::Unity.Services.Matchmaker.Models.ConnectionDetails(value, typeof(global::Unity.Services.Matchmaker.Models.CustomConnectionDetails));
			return this;
		}
	}
}
