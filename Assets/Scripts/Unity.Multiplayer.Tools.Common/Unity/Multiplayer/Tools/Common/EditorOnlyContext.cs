namespace Unity.Multiplayer.Tools.Common
{
	internal abstract class EditorOnlyContext : global::Unity.Multiplayer.Tools.Common.IEditorSetupHandler, global::Unity.Multiplayer.Tools.Common.IContext
	{
		void global::Unity.Multiplayer.Tools.Common.IEditorSetupHandler.EditorSetup()
		{
			Setup();
		}

		protected abstract void Setup();
	}
}
