namespace Unity.Services.Multiplayer
{
	internal class MatchmakerProvider : global::Unity.Services.Multiplayer.IModuleProvider
	{
		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private readonly global::Unity.Services.Matchmaker.IMatchmakerService m_MatchmakerService;

		public global::System.Type Type => typeof(global::Unity.Services.Multiplayer.MatchmakerModule);

		public int Priority => 2000;

		internal MatchmakerProvider(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::Unity.Services.Matchmaker.IMatchmakerService matchmakerService)
		{
			m_ActionScheduler = actionScheduler;
			m_MatchmakerService = matchmakerService;
		}

		public global::Unity.Services.Multiplayer.IModule Build(global::Unity.Services.Multiplayer.ISession session)
		{
			return new global::Unity.Services.Multiplayer.MatchmakerModule(session, m_ActionScheduler, m_MatchmakerService);
		}
	}
}
