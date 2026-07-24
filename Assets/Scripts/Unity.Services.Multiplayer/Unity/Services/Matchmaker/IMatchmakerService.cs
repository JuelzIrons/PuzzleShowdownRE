namespace Unity.Services.Matchmaker
{
	public interface IMatchmakerService
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.CreateTicketResponse> CreateTicketAsync(global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Player> players, global::Unity.Services.Matchmaker.CreateTicketOptions options);

		global::System.Threading.Tasks.Task DeleteTicketAsync(string ticketId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.TicketStatusResponse> GetTicketAsync(string ticketId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.BackfillTicket> ApproveBackfillTicketAsync(string backfillTicketId);

		global::System.Threading.Tasks.Task<string> CreateBackfillTicketAsync(global::Unity.Services.Matchmaker.CreateBackfillTicketOptions options);

		global::System.Threading.Tasks.Task DeleteBackfillTicketAsync(string backfillTicketId);

		global::System.Threading.Tasks.Task UpdateBackfillTicketAsync(string backfillTicketId, global::Unity.Services.Matchmaker.Models.BackfillTicket ticket);

		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults> GetMatchmakingResultsAsync(string matchId);
	}
}
