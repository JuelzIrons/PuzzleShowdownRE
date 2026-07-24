namespace Unity.Multiplayer.Tools.Common
{
	internal class RuntimeUpdaterBehaviour : global::UnityEngine.MonoBehaviour, global::Unity.Multiplayer.Tools.Common.IRuntimeUpdater
	{
		private static string RuntimeErrorMessage => "RuntimeUpdaterBehaviour can only be called at runtime.";

		public event global::System.Action OnAwake;

		public event global::System.Action OnStart;

		public event global::System.Action OnUpdate;

		public event global::System.Action OnFixedUpdate;

		public event global::System.Action OnLateUpdate;

		public event global::System.Action OnDestroyed;

		internal void ReplaceCallbacks(global::System.Action onAwake, global::System.Action onStart, global::System.Action onUpdate, global::System.Action onFixedUpdate, global::System.Action onLateUpdate, global::System.Action onDestroyed)
		{
			this.OnAwake = onAwake;
			this.OnStart = onStart;
			this.OnUpdate = onUpdate;
			this.OnFixedUpdate = onFixedUpdate;
			this.OnLateUpdate = onLateUpdate;
			this.OnDestroyed = onDestroyed;
		}

		private void Awake()
		{
			this.OnAwake?.Invoke();
		}

		private void Start()
		{
			this.OnStart?.Invoke();
		}

		private void Update()
		{
			this.OnUpdate?.Invoke();
		}

		private void FixedUpdate()
		{
			this.OnFixedUpdate?.Invoke();
		}

		private void LateUpdate()
		{
			this.OnLateUpdate?.Invoke();
		}

		private void OnDestroy()
		{
			this.OnDestroyed?.Invoke();
			this.OnAwake = null;
			this.OnStart = null;
			this.OnUpdate = null;
			this.OnFixedUpdate = null;
			this.OnLateUpdate = null;
		}
	}
}
