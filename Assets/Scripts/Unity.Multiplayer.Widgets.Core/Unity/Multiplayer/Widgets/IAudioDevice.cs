namespace Unity.Multiplayer.Widgets
{
	internal interface IAudioDevice
	{
		string DeviceName { get; }

		string DeviceID { get; }

		global::System.Threading.Tasks.Task SetActiveDeviceAsync();
	}
}
