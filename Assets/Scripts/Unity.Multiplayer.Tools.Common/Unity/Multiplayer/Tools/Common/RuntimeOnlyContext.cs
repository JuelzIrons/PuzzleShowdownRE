namespace Unity.Multiplayer.Tools.Common
{
	internal abstract class RuntimeOnlyContext : global::Unity.Multiplayer.Tools.Common.IRuntimeSetupHandler, global::Unity.Multiplayer.Tools.Common.IContext
	{
		void global::Unity.Multiplayer.Tools.Common.IRuntimeSetupHandler.RuntimeSetup()
		{
			Setup();
		}

		public void RuntimeTeardown()
		{
			Teardown();
		}

		protected abstract void Setup();

		protected abstract void Teardown();
	}
}
