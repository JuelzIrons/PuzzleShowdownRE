namespace Unity.Services.Multiplayer
{
	internal class NetworkBuilder : global::Unity.Services.Multiplayer.INetworkBuilder
	{
		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		public NetworkBuilder(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler)
		{
			m_ActionScheduler = actionScheduler;
		}

		public global::Unity.Services.Multiplayer.INetworkHandler Build()
		{
			return global::Unity.Services.Multiplayer.NetcodeUtils.Current switch
			{
				global::Unity.Services.Multiplayer.NetcodeType.GameObjects => new global::Unity.Services.Multiplayer.GameObjectsNetcodeNetworkHandler(), 
				global::Unity.Services.Multiplayer.NetcodeType.Entities => throw new global::Unity.Services.Multiplayer.SessionException("Netcode for Entities package is not installed", global::Unity.Services.Multiplayer.SessionError.MissingAssembly), 
				_ => throw new global::Unity.Services.Multiplayer.SessionException("Netcode for GameObjects or Netcode for Entities package need to be installed", global::Unity.Services.Multiplayer.SessionError.MissingAssembly), 
			};
		}
	}
}
