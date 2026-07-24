namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.Singleton(Name = "VisualScripting SceneVariables", Automatic = true, Persistent = false)]
	[global::UnityEngine.RequireComponent(typeof(global::Unity.VisualScripting.Variables))]
	[global::Unity.VisualScripting.DisableAnnotation]
	[global::UnityEngine.AddComponentMenu("")]
	[global::Unity.VisualScripting.IncludeInSettings(false)]
	public sealed class SceneVariables : global::UnityEngine.MonoBehaviour, global::Unity.VisualScripting.ISingleton
	{
		private global::Unity.VisualScripting.Variables _variables;

		public global::Unity.VisualScripting.Variables variables
		{
			get
			{
				if (_variables == null)
				{
					_variables = base.gameObject.GetOrAddComponent<global::Unity.VisualScripting.Variables>();
				}
				return _variables;
			}
		}

		public static global::Unity.VisualScripting.SceneVariables Instance(global::UnityEngine.SceneManagement.Scene scene)
		{
			return global::Unity.VisualScripting.SceneSingleton<global::Unity.VisualScripting.SceneVariables>.InstanceIn(scene);
		}

		public static bool InstantiatedIn(global::UnityEngine.SceneManagement.Scene scene)
		{
			return global::Unity.VisualScripting.SceneSingleton<global::Unity.VisualScripting.SceneVariables>.InstantiatedIn(scene);
		}

		public static global::Unity.VisualScripting.VariableDeclarations For(global::UnityEngine.SceneManagement.Scene? scene)
		{
			global::Unity.VisualScripting.Ensure.That("scene").IsNotNull(scene);
			return Instance(scene.Value).variables.declarations;
		}

		private void Awake()
		{
			global::Unity.VisualScripting.SceneSingleton<global::Unity.VisualScripting.SceneVariables>.Awake(this);
		}

		private void OnDestroy()
		{
			global::Unity.VisualScripting.SceneSingleton<global::Unity.VisualScripting.SceneVariables>.OnDestroy(this);
		}
	}
}
