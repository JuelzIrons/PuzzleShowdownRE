namespace Unity.Multiplayer.Widgets
{
	internal abstract class WidgetBehaviour : global::UnityEngine.MonoBehaviour, global::Unity.Multiplayer.Widgets.IWidget
	{
		private bool m_IsQuitting;

		public bool IsInitialized { get; set; }

		protected virtual void OnEnable()
		{
			global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.WidgetEventDispatcher>.Instance.RegisterWidget(this);
		}

		protected virtual void OnDisable()
		{
			if (!m_IsQuitting)
			{
				global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.WidgetEventDispatcher>.Instance.UnregisterWidget(this);
			}
		}

		public virtual void OnServicesInitialized()
		{
		}

		private void OnApplicationQuit()
		{
			m_IsQuitting = true;
		}
	}
}
