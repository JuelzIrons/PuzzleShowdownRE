namespace Unity.Multiplayer.Tools.Common
{
	internal class RuntimeUpdater : global::Unity.Multiplayer.Tools.Common.IRuntimeUpdater
	{
		private global::Unity.Multiplayer.Tools.Common.RuntimeUpdaterBehaviour m_Component;

		private event global::System.Action m_OnAwake;

		private event global::System.Action m_OnStart;

		private event global::System.Action m_OnUpdate;

		private event global::System.Action m_OnFixedUpdate;

		private event global::System.Action m_OnLateUpdate;

		private event global::System.Action m_OnDestroyed;

		public event global::System.Action OnAwake
		{
			add
			{
				if (m_Component != null)
				{
					m_Component.OnAwake += value;
				}
				m_OnAwake += value;
			}
			remove
			{
				if (m_Component != null)
				{
					m_Component.OnAwake -= value;
				}
				m_OnAwake -= value;
			}
		}

		public event global::System.Action OnStart
		{
			add
			{
				if (m_Component != null)
				{
					m_Component.OnStart += value;
				}
				m_OnStart += value;
			}
			remove
			{
				if (m_Component != null)
				{
					m_Component.OnStart -= value;
				}
				m_OnStart -= value;
			}
		}

		public event global::System.Action OnUpdate
		{
			add
			{
				if (m_Component != null)
				{
					m_Component.OnUpdate += value;
				}
				m_OnUpdate += value;
			}
			remove
			{
				if (m_Component != null)
				{
					m_Component.OnUpdate -= value;
				}
				m_OnUpdate -= value;
			}
		}

		public event global::System.Action OnFixedUpdate
		{
			add
			{
				if (m_Component != null)
				{
					m_Component.OnFixedUpdate += value;
				}
				m_OnFixedUpdate += value;
			}
			remove
			{
				if (m_Component != null)
				{
					m_Component.OnFixedUpdate -= value;
				}
				m_OnFixedUpdate -= value;
			}
		}

		public event global::System.Action OnLateUpdate
		{
			add
			{
				if (m_Component != null)
				{
					m_Component.OnLateUpdate += value;
				}
				m_OnLateUpdate += value;
			}
			remove
			{
				if (m_Component != null)
				{
					m_Component.OnLateUpdate -= value;
				}
				m_OnLateUpdate -= value;
			}
		}

		public event global::System.Action OnDestroyed
		{
			add
			{
				if (m_Component != null)
				{
					m_Component.OnDestroyed += value;
				}
				m_OnDestroyed += value;
			}
			remove
			{
				if (m_Component != null)
				{
					m_Component.OnDestroyed -= value;
				}
				m_OnDestroyed -= value;
			}
		}

		public RuntimeUpdater()
		{
			CreateInstance();
		}

		private void CreateInstance()
		{
			m_Component = new global::UnityEngine.GameObject("[RuntimeUpdaterBehaviour]").AddComponent<global::Unity.Multiplayer.Tools.Common.RuntimeUpdaterBehaviour>();
			m_Component.gameObject.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			m_Component.ReplaceCallbacks(this.m_OnAwake, this.m_OnStart, this.m_OnUpdate, this.m_OnFixedUpdate, this.m_OnLateUpdate, this.m_OnDestroyed);
			global::UnityEngine.Object.DontDestroyOnLoad(m_Component.gameObject);
		}
	}
}
