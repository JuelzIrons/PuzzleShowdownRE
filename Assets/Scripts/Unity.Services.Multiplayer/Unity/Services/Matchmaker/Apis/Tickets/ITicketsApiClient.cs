namespace Unity.Services.Matchmaker.Apis.Tickets
{
	internal interface ITicketsApiClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.CreateTicketResponse>> CreateTicketAsync(global::Unity.Services.Matchmaker.Tickets.CreateTicketRequest request, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response> DeleteTicketAsync(global::Unity.Services.Matchmaker.Tickets.DeleteTicketRequest request, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.TicketStatusResponse>> GetTicketStatusAsync(global::Unity.Services.Matchmaker.Tickets.GetTicketStatusRequest request, global::Unity.Services.Matchmaker.Configuration operationConfiguration = null);
	}
}
