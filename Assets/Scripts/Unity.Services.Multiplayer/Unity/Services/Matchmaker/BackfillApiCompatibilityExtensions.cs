namespace Unity.Services.Matchmaker
{
	internal static class BackfillApiCompatibilityExtensions
	{
		internal static global::Unity.Services.Matchmaker.Models.CreateBackfillTicketRequest GetLegacyModel(this global::Unity.Services.Matchmaker.CreateBackfillTicketOptions options)
		{
			return new global::Unity.Services.Matchmaker.Models.CreateBackfillTicketRequest(options.Properties.GetLegacyModel(), options.QueueName, options.Connection, options.ConnectionDetails, options.Attributes, options.PoolId, options.MatchId);
		}

		internal static global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket GetLegacyModel(this global::Unity.Services.Matchmaker.Models.BackfillTicket options)
		{
			global::System.Collections.Generic.Dictionary<string, byte[]> legacyModel = options.Properties.GetLegacyModel();
			return new global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket(options.Id, options.Connection, options.Attributes, legacyModel);
		}

		internal static global::System.Collections.Generic.Dictionary<string, byte[]> GetLegacyModel(this global::Unity.Services.Matchmaker.Models.BackfillTicketProperties properties)
		{
			global::System.Collections.Generic.Dictionary<string, byte[]> dictionary = new global::System.Collections.Generic.Dictionary<string, byte[]>();
			string s = global::Newtonsoft.Json.JsonConvert.SerializeObject(properties);
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(s);
			dictionary.Add("Data", bytes);
			return dictionary;
		}

		internal static global::Unity.Services.Matchmaker.Models.BackfillTicket GetCompatibilityModel(this global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket legacyTicket)
		{
			global::Unity.Services.Matchmaker.Models.BackfillTicketProperties properties = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Unity.Services.Matchmaker.Models.BackfillTicketProperties>(global::System.Text.Encoding.UTF8.GetString(legacyTicket.Properties["Data"]));
			return new global::Unity.Services.Matchmaker.Models.BackfillTicket(legacyTicket.Id, legacyTicket.Connection, legacyTicket.Attributes, properties);
		}
	}
}
