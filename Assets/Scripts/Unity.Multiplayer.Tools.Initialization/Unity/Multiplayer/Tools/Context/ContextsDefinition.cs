namespace Unity.Multiplayer.Tools.Context
{
	internal static class ContextsDefinition
	{
		internal static global::Unity.Multiplayer.Tools.Common.IContext[] Contexts { get; }

		static ContextsDefinition()
		{
			global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Common.IContext> list = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Common.IContext>();
			InitializeNetVisContexts(new global::Unity.Multiplayer.Tools.Common.RuntimeUpdater(), list);
			Contexts = list.ToArray();
		}

		private static void InitializeNetVisContexts(global::Unity.Multiplayer.Tools.Common.IRuntimeUpdater runtimeUpdater, global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.Common.IContext> contexts)
		{
		}
	}
}
