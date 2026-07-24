namespace Unity.Services.Multiplayer
{
	internal class HostMigrationHandler : global::Unity.Services.Multiplayer.IHostMigrationHandler
	{
		private const string k_EnclosingTypeName = "HostMigrationHandler";

		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private readonly global::Unity.Services.Multiplayer.SessionHandler m_SessionHandler;

		internal readonly global::Unity.Services.Multiplayer.IMigrationDataHandler MigrationDataHandler;

		private readonly global::System.TimeSpan m_DataUploadInterval;

		private readonly global::System.TimeSpan m_DataHandlingTimeout;

		internal long? m_ScheduledMigrationId;

		internal HostMigrationHandler(global::Unity.Services.Multiplayer.SessionHandler sessionHandler, global::Unity.Services.Multiplayer.IMigrationDataHandler dataHandler, global::System.TimeSpan dataUploadInterval, global::System.TimeSpan dataHandlingTimeout)
		{
			m_SessionHandler = sessionHandler;
			m_ActionScheduler = sessionHandler.ActionScheduler;
			MigrationDataHandler = dataHandler;
			m_DataUploadInterval = dataUploadInterval;
			m_DataHandlingTimeout = dataHandlingTimeout;
		}

		public async global::System.Threading.Tasks.Task ApplyMigrationDataAsync()
		{
			if (MigrationDataHandler == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogWarning("No data handler provided, cannot apply migration data.");
				return;
			}
			if (!m_SessionHandler.IsHost)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Cannot apply migration data for non-host player.");
				return;
			}
			try
			{
				global::Unity.Services.Multiplayer.SessionMigrationData sessionMigrationData = await m_SessionHandler.GetHostMigrationDataAsync(m_DataHandlingTimeout);
				if (sessionMigrationData != null && sessionMigrationData.Data != null)
				{
					MigrationDataHandler.Apply(sessionMigrationData.Data);
				}
			}
			catch (global::System.Exception ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogWarning("Error while getting host migration data: " + ex.Message);
			}
			ScheduleHostDataUpload();
		}

		public void Start()
		{
			if (!m_ScheduledMigrationId.HasValue)
			{
				if (MigrationDataHandler != null)
				{
					ScheduleHostDataUpload();
				}
				else
				{
					global::Unity.Services.Multiplayer.Logger.LogWarning("No data handler provided, host migration data will not run.");
				}
			}
		}

		public void Stop()
		{
			if (m_ScheduledMigrationId.HasValue)
			{
				m_ActionScheduler.CancelAction(m_ScheduledMigrationId.Value);
				m_ScheduledMigrationId = null;
			}
		}

		private void ScheduleHostDataUpload()
		{
			if (m_SessionHandler.IsHost && !m_ScheduledMigrationId.HasValue)
			{
				m_ScheduledMigrationId = m_ActionScheduler.ScheduleAction(UploadHostData, m_DataUploadInterval.TotalSeconds);
			}
		}

		private async void UploadHostData()
		{
			m_ScheduledMigrationId = null;
			if (m_SessionHandler.IsHost)
			{
				if (m_SessionHandler.State != global::Unity.Services.Multiplayer.SessionState.Connected)
				{
					ScheduleHostDataUpload();
					return;
				}
				if (m_SessionHandler.PlayerCount < 2)
				{
					ScheduleHostDataUpload();
					return;
				}
				byte[] data = MigrationDataHandler.Generate();
				await m_SessionHandler.SetHostMigrationDataAsync(data, m_DataHandlingTimeout);
				ScheduleHostDataUpload();
			}
		}
	}
}
