namespace Unity.Services.Multiplayer
{
	public class QuerySessionsResults
	{
		private readonly global::Unity.Services.Multiplayer.ISessionQuerier m_SessionQuerier;

		private readonly global::Unity.Services.Multiplayer.QuerySessionsOptions m_QuerySessionsOptions;

		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private long? m_ActionId;

		public string ContinuationToken { get; private set; }

		public global::System.Collections.Generic.IList<global::Unity.Services.Multiplayer.ISessionInfo> Sessions { get; private set; }

		internal QuerySessionsResults(global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.ISessionInfo> sessions, string continuationToken, global::Unity.Services.Multiplayer.QuerySessionsOptions querySessionsOptions, global::Unity.Services.Multiplayer.ISessionQuerier querier, global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler)
		{
			ContinuationToken = continuationToken;
			m_QuerySessionsOptions = querySessionsOptions;
			m_SessionQuerier = querier;
			Sessions = sessions.AsReadOnly();
			m_ActionScheduler = actionScheduler;
		}

		private async void PollOnce(int pollingDelaySeconds)
		{
			try
			{
				Sessions = (await m_SessionQuerier.QueryAsync(m_QuerySessionsOptions)).Sessions;
			}
			catch (global::System.Exception ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Error polling for sessions: " + ex.Message);
			}
			m_ActionId = m_ActionScheduler.ScheduleAction(delegate
			{
				PollOnce(pollingDelaySeconds);
			}, pollingDelaySeconds);
		}

		public void StartPolling(int pollingDelaySeconds = 5)
		{
			if (!m_ActionId.HasValue)
			{
				m_ActionScheduler.ScheduleAction(delegate
				{
					PollOnce(pollingDelaySeconds);
				}, pollingDelaySeconds);
			}
		}

		public void StopPolling()
		{
			if (m_ActionId.HasValue)
			{
				m_ActionScheduler.CancelAction(m_ActionId.Value);
			}
		}
	}
}
