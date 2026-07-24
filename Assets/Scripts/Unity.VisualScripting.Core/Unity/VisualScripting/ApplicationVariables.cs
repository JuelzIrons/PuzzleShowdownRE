namespace Unity.VisualScripting
{
	public static class ApplicationVariables
	{
		public const string assetPath = "ApplicationVariables";

		private static global::Unity.VisualScripting.VariablesAsset _asset;

		public static global::Unity.VisualScripting.VariablesAsset asset
		{
			get
			{
				if (_asset == null)
				{
					Load();
				}
				return _asset;
			}
		}

		public static global::Unity.VisualScripting.VariableDeclarations runtime { get; private set; }

		public static global::Unity.VisualScripting.VariableDeclarations initial => asset.declarations;

		public static global::Unity.VisualScripting.VariableDeclarations current
		{
			get
			{
				if (!global::UnityEngine.Application.isPlaying)
				{
					return initial;
				}
				return runtime;
			}
		}

		public static void Load()
		{
			_asset = global::UnityEngine.Resources.Load<global::Unity.VisualScripting.VariablesAsset>("ApplicationVariables") ?? global::UnityEngine.ScriptableObject.CreateInstance<global::Unity.VisualScripting.VariablesAsset>();
		}

		public static void OnEnterEditMode()
		{
			DestroyRuntimeDeclarations();
		}

		public static void OnExitEditMode()
		{
		}

		internal static void OnEnterPlayMode()
		{
			CreateRuntimeDeclarations();
		}

		internal static void OnExitPlayMode()
		{
		}

		private static void CreateRuntimeDeclarations()
		{
			runtime = asset.declarations.CloneViaFakeSerialization();
		}

		private static void DestroyRuntimeDeclarations()
		{
			runtime = null;
		}
	}
}
