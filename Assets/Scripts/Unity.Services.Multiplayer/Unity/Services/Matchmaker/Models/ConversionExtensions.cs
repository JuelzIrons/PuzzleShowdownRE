namespace Unity.Services.Matchmaker.Models
{
	internal static class ConversionExtensions
	{
		public static global::Unity.Services.Matchmaker.Models.MatchProperties ToMatchProperties(this global::Unity.Services.Matchmaker.Models.StoredMatchProperties matchProperties)
		{
			return new global::Unity.Services.Matchmaker.Models.MatchProperties(matchProperties.Teams, matchProperties.Players, matchProperties.Region, matchProperties.BackfillTicketId);
		}
	}
}
