namespace Unity.Multiplayer.Tools.Common
{
	internal interface IRuntimeSetupHandler : global::Unity.Multiplayer.Tools.Common.IContext
	{
		void RuntimeSetup();

		void RuntimeTeardown();
	}
}
