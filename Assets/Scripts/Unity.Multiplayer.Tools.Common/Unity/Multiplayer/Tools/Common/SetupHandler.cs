namespace Unity.Multiplayer.Tools.Common
{
	internal abstract class SetupHandler : global::Unity.Multiplayer.Tools.Common.IEditorSetupHandler, global::Unity.Multiplayer.Tools.Common.IContext, global::Unity.Multiplayer.Tools.Common.IRuntimeSetupHandler
	{
		protected enum ContextStatus
		{
			Disabled = 0,
			EnabledInEditor = 1,
			EnabledInRuntime = 2
		}

		protected global::Unity.Multiplayer.Tools.Common.SetupHandler.ContextStatus Status { get; private set; }

		void global::Unity.Multiplayer.Tools.Common.IEditorSetupHandler.EditorSetup()
		{
			Status = global::Unity.Multiplayer.Tools.Common.SetupHandler.ContextStatus.EnabledInEditor;
			Setup();
		}

		void global::Unity.Multiplayer.Tools.Common.IRuntimeSetupHandler.RuntimeSetup()
		{
			if (Status != global::Unity.Multiplayer.Tools.Common.SetupHandler.ContextStatus.EnabledInEditor)
			{
				Status = global::Unity.Multiplayer.Tools.Common.SetupHandler.ContextStatus.EnabledInRuntime;
				Setup();
			}
		}

		void global::Unity.Multiplayer.Tools.Common.IRuntimeSetupHandler.RuntimeTeardown()
		{
			if (Status != global::Unity.Multiplayer.Tools.Common.SetupHandler.ContextStatus.EnabledInEditor)
			{
				EnsureTeardown();
			}
		}

		protected abstract void Setup();

		protected abstract void Teardown();

		private void EnsureTeardown()
		{
			try
			{
				Teardown();
			}
			finally
			{
				Status = global::Unity.Multiplayer.Tools.Common.SetupHandler.ContextStatus.Disabled;
			}
		}
	}
}
