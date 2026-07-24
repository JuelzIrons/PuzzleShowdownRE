namespace UnityEngine.InputSystem.UI
{
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/UISupport.html#multiplayer-uis")]
	public class MultiplayerEventSystem : global::UnityEngine.EventSystems.EventSystem
	{
		[global::UnityEngine.Tooltip("If set, only process mouse and navigation events for any game objects which are children of this game object.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.GameObject m_PlayerRoot;

		public global::UnityEngine.GameObject playerRoot
		{
			get
			{
				return m_PlayerRoot;
			}
			set
			{
				m_PlayerRoot = value;
				InitializePlayerRoot();
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			InitializePlayerRoot();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
		}

		private void InitializePlayerRoot()
		{
			global::UnityEngine.InputSystem.UI.InputSystemUIInputModule component = GetComponent<global::UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
			if (component != null)
			{
				component.localMultiPlayerRoot = m_PlayerRoot;
			}
		}

		protected override void Update()
		{
			global::UnityEngine.EventSystems.EventSystem eventSystem = global::UnityEngine.EventSystems.EventSystem.current;
			global::UnityEngine.EventSystems.EventSystem.current = this;
			try
			{
				base.Update();
			}
			finally
			{
				global::UnityEngine.EventSystems.EventSystem.current = eventSystem;
			}
		}
	}
}
