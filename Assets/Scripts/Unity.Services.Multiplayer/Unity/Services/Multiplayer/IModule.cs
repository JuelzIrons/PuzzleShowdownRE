namespace Unity.Services.Multiplayer
{
	internal interface IModule
	{
		global::System.Threading.Tasks.Task InitializeAsync();

		global::System.Threading.Tasks.Task LeaveAsync();
	}
}
