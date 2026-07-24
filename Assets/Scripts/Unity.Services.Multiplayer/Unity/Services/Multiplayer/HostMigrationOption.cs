namespace Unity.Services.Multiplayer
{
	internal class HostMigrationOption : global::Unity.Services.Multiplayer.IModuleOption
	{
		public global::System.Type Type => typeof(global::Unity.Services.Multiplayer.NetworkModule);

		public global::Unity.Services.Multiplayer.IMigrationDataHandler DataHandler { get; set; }

		public global::System.TimeSpan DataUploadInterval { get; set; }

		public global::System.TimeSpan DataHandlingTimeout { get; set; }

		public void Process(global::Unity.Services.Multiplayer.SessionHandler session)
		{
			session.GetModule<global::Unity.Services.Multiplayer.NetworkModule>().HostMigrationHandler = new global::Unity.Services.Multiplayer.HostMigrationHandler(session, DataHandler, DataUploadInterval, DataHandlingTimeout);
		}
	}
}
