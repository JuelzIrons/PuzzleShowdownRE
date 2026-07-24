namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.Singleton(Name = "VisualScripting CoroutineRunner", Automatic = true, Persistent = true)]
	[global::UnityEngine.AddComponentMenu("")]
	[global::Unity.VisualScripting.DisableAnnotation]
	[global::Unity.VisualScripting.IncludeInSettings(false)]
	public sealed class CoroutineRunner : global::UnityEngine.MonoBehaviour, global::Unity.VisualScripting.ISingleton
	{
		public static global::Unity.VisualScripting.CoroutineRunner instance => global::Unity.VisualScripting.Singleton<global::Unity.VisualScripting.CoroutineRunner>.instance;

		private void Awake()
		{
			global::Unity.VisualScripting.Singleton<global::Unity.VisualScripting.CoroutineRunner>.Awake(this);
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
			global::Unity.VisualScripting.Singleton<global::Unity.VisualScripting.CoroutineRunner>.OnDestroy(this);
		}
	}
}
