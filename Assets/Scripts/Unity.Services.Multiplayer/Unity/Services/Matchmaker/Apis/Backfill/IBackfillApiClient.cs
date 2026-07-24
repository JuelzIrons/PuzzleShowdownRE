namespace Unity.Services.Matchmaker.Apis.Backfill
{
	internal interface IBackfillApiClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket>> ApproveBackfillTicketAsync(global::Unity.Services.Matchmaker.Backfill.ApproveBackfillTicketRequest request, string payloadProxyToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.CreateBackfillTicketResponse>> CreateBackfillTicketAsync(global::Unity.Services.Matchmaker.Backfill.CreateBackfillTicketRequest request, string payloadProxyToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response> DeleteBackfillTicketAsync(global::Unity.Services.Matchmaker.Backfill.DeleteBackfillTicketRequest request, string payloadProxyToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response> UpdateBackfillTicketAsync(global::Unity.Services.Matchmaker.Backfill.UpdateBackfillTicketRequest request, string payloadProxyToken, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null);
	}
}
