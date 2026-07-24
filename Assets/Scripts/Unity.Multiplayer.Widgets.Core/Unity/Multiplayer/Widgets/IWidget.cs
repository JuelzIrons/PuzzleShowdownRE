namespace Unity.Multiplayer.Widgets
{
	internal interface IWidget
	{
		bool IsInitialized { get; set; }

		void OnServicesInitialized();
	}
}
