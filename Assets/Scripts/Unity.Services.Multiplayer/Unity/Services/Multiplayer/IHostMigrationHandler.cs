namespace Unity.Services.Multiplayer
{
	internal interface IHostMigrationHandler
	{
		global::System.Threading.Tasks.Task ApplyMigrationDataAsync();

		void Start();

		void Stop();
	}
}
