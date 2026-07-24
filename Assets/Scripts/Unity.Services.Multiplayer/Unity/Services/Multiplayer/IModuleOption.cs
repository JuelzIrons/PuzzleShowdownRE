namespace Unity.Services.Multiplayer
{
	internal interface IModuleOption
	{
		global::System.Type Type { get; }

		void Process(global::Unity.Services.Multiplayer.SessionHandler session);
	}
}
