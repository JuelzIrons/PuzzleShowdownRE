namespace Unity.Multiplayer.Tools.Context
{
	internal static class ContextsInitializer
	{
		private static readonly global::Unity.Multiplayer.Tools.Common.IContext[] s_Contexts;

		static ContextsInitializer()
		{
			global::UnityEngine.Application.quitting += DisableRuntimeContexts;
			s_Contexts = global::Unity.Multiplayer.Tools.Context.ContextsDefinition.Contexts;
		}

		private static void EnableEditorContexts()
		{
			global::Unity.Multiplayer.Tools.Common.IContext[] array = s_Contexts;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is global::Unity.Multiplayer.Tools.Common.IEditorSetupHandler editorSetupHandler)
				{
					editorSetupHandler.EditorSetup();
				}
			}
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void EnableRuntimeContexts()
		{
			global::Unity.Multiplayer.Tools.Common.IContext[] array = s_Contexts;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is global::Unity.Multiplayer.Tools.Common.IRuntimeSetupHandler runtimeSetupHandler)
				{
					runtimeSetupHandler.RuntimeSetup();
				}
			}
		}

		private static void DisableRuntimeContexts()
		{
			global::Unity.Multiplayer.Tools.Common.IContext[] array = s_Contexts;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is global::Unity.Multiplayer.Tools.Common.IRuntimeSetupHandler runtimeSetupHandler)
				{
					runtimeSetupHandler.RuntimeTeardown();
				}
			}
		}
	}
}
