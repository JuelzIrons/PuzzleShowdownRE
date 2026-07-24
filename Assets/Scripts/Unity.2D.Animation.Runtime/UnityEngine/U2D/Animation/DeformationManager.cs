namespace UnityEngine.U2D.Animation
{
	internal class DeformationManager : global::UnityEngine.ScriptableObject
	{
		private static global::UnityEngine.U2D.Animation.DeformationManager s_Instance;

		private global::UnityEngine.U2D.Animation.BaseDeformationSystem[] m_DeformationSystems;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.GameObject m_Helper;

		private bool m_WasUsingGpuDeformationLastFrame;

		public static global::UnityEngine.U2D.Animation.DeformationManager instance
		{
			get
			{
				if (s_Instance == null)
				{
					global::UnityEngine.U2D.Animation.DeformationManager[] array = global::UnityEngine.Resources.FindObjectsOfTypeAll<global::UnityEngine.U2D.Animation.DeformationManager>();
					if (array.Length != 0)
					{
						s_Instance = array[0];
					}
					else
					{
						s_Instance = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.U2D.Animation.DeformationManager>();
					}
					s_Instance.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
					s_Instance.Init();
				}
				return s_Instance;
			}
		}

		internal global::UnityEngine.GameObject helperGameObject => m_Helper;

		private bool canUseGpuDeformation { get; set; }

		private void OnEnable()
		{
			s_Instance = this;
			canUseGpuDeformation = global::UnityEngine.U2D.Animation.SpriteSkinUtility.CanUseGpuDeformation();
			m_WasUsingGpuDeformationLastFrame = global::UnityEngine.U2D.Animation.SpriteSkinUtility.IsUsingGpuDeformation();
			Init();
		}

		private void Init()
		{
			CreateBatchSystems();
			CreateHelper();
		}

		private void CreateBatchSystems()
		{
			if (m_DeformationSystems == null)
			{
				int num = ((!canUseGpuDeformation) ? 1 : 2);
				m_DeformationSystems = new global::UnityEngine.U2D.Animation.BaseDeformationSystem[num];
				m_DeformationSystems[0] = new global::UnityEngine.U2D.Animation.CpuDeformationSystem();
				if (canUseGpuDeformation)
				{
					m_DeformationSystems[1] = new global::UnityEngine.U2D.Animation.GpuDeformationSystem();
				}
				for (int i = 0; i < m_DeformationSystems.Length; i++)
				{
					m_DeformationSystems[i].Initialize(m_DeformationSystems[i].GetHashCode());
				}
			}
		}

		private void CreateHelper()
		{
			if (!(m_Helper != null))
			{
				m_Helper = new global::UnityEngine.GameObject("DeformationManagerUpdater");
				m_Helper.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				global::UnityEngine.U2D.Animation.DeformationManagerUpdater deformationManagerUpdater = m_Helper.AddComponent<global::UnityEngine.U2D.Animation.DeformationManagerUpdater>();
				deformationManagerUpdater.onDestroyingComponent = (global::System.Action<global::UnityEngine.GameObject>)global::System.Delegate.Combine(deformationManagerUpdater.onDestroyingComponent, new global::System.Action<global::UnityEngine.GameObject>(OnHelperDestroyed));
				global::UnityEngine.Object.DontDestroyOnLoad(m_Helper);
			}
		}

		private void OnHelperDestroyed(global::UnityEngine.GameObject helperGo)
		{
			if (!(m_Helper != helperGo))
			{
				m_Helper = null;
				CreateHelper();
			}
		}

		private void OnDisable()
		{
			if (m_Helper != null)
			{
				global::UnityEngine.U2D.Animation.DeformationManagerUpdater component = m_Helper.GetComponent<global::UnityEngine.U2D.Animation.DeformationManagerUpdater>();
				component.onDestroyingComponent = (global::System.Action<global::UnityEngine.GameObject>)global::System.Delegate.Remove(component.onDestroyingComponent, new global::System.Action<global::UnityEngine.GameObject>(OnHelperDestroyed));
				global::UnityEngine.Object.DestroyImmediate(m_Helper);
			}
			for (int i = 0; i < m_DeformationSystems.Length; i++)
			{
				m_DeformationSystems[i].Cleanup();
			}
			s_Instance = null;
		}

		internal void Update()
		{
			if (HasToggledGpuDeformation())
			{
				MoveSpriteSkinsToActiveSystem();
			}
			for (int i = 0; i < m_DeformationSystems.Length; i++)
			{
				m_DeformationSystems[i].Update();
			}
		}

		private bool HasToggledGpuDeformation()
		{
			bool flag = global::UnityEngine.U2D.Animation.SpriteSkinUtility.IsUsingGpuDeformation();
			if (flag != m_WasUsingGpuDeformationLastFrame)
			{
				m_WasUsingGpuDeformationLastFrame = flag;
				return true;
			}
			return false;
		}

		private void MoveSpriteSkinsToActiveSystem()
		{
			global::UnityEngine.U2D.Animation.BaseDeformationSystem baseDeformationSystem = (global::UnityEngine.U2D.Animation.SpriteSkinUtility.IsUsingGpuDeformation() ? m_DeformationSystems[0] : m_DeformationSystems[1]);
			global::System.Collections.Generic.HashSet<global::UnityEngine.U2D.Animation.SpriteSkin> spriteSkins = baseDeformationSystem.GetSpriteSkins();
			foreach (global::UnityEngine.U2D.Animation.SpriteSkin item in spriteSkins)
			{
				baseDeformationSystem.RemoveSpriteSkin(item);
			}
			foreach (global::UnityEngine.U2D.Animation.SpriteSkin item2 in spriteSkins)
			{
				AddSpriteSkin(item2);
			}
		}

		internal void AddSpriteSkin(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (spriteSkin == null)
			{
				return;
			}
			global::UnityEngine.U2D.Animation.DeformationMethods deformationMethods = (global::UnityEngine.U2D.Animation.SpriteSkinUtility.IsUsingGpuDeformation() ? global::UnityEngine.U2D.Animation.DeformationMethods.Gpu : global::UnityEngine.U2D.Animation.DeformationMethods.Cpu);
			if (deformationMethods == global::UnityEngine.U2D.Animation.DeformationMethods.Gpu && null != spriteSkin.sprite)
			{
				if (!canUseGpuDeformation)
				{
					deformationMethods = global::UnityEngine.U2D.Animation.DeformationMethods.Cpu;
					global::UnityEngine.Debug.LogWarning(spriteSkin.name + " is trying to use GPU deformation, but the platform does not support it. Switching the renderer over to CPU deformation.", spriteSkin);
				}
				else if (!global::UnityEngine.U2D.Animation.SpriteSkinUtility.CanSpriteSkinUseGpuDeformation(spriteSkin))
				{
					deformationMethods = global::UnityEngine.U2D.Animation.DeformationMethods.Cpu;
					global::UnityEngine.Debug.LogWarning(spriteSkin.name + " is using a shader without GPU deformation support. Switching the renderer over to CPU deformation.", spriteSkin);
				}
			}
			global::UnityEngine.U2D.Animation.BaseDeformationSystem baseDeformationSystem = m_DeformationSystems[(int)deformationMethods];
			if (baseDeformationSystem.AddSpriteSkin(spriteSkin))
			{
				spriteSkin.SetDeformationSystem(baseDeformationSystem);
			}
		}

		internal void RemoveBoneTransforms(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			for (int i = 0; i < m_DeformationSystems.Length; i++)
			{
				m_DeformationSystems[i].RemoveBoneTransforms(spriteSkin);
			}
		}

		internal void AddSpriteSkinBoneTransform(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (!(spriteSkin == null))
			{
				spriteSkin.DeformationSystem?.AddBoneTransforms(spriteSkin);
			}
		}
	}
}
