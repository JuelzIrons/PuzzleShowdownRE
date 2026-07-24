namespace UnityEngine.Rendering
{
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.AddComponentMenu("Miscellaneous/Volume")]
	public class Volume : global::UnityEngine.MonoBehaviour, global::UnityEngine.Rendering.IVolume
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("isGlobal")]
		private bool m_IsGlobal = true;

		[global::UnityEngine.Delayed]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_Priority")]
		public float priority;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_BlendDistance")]
		public float blendDistance;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_Weight")]
		public float weight = 1f;

		public global::UnityEngine.Rendering.VolumeProfile sharedProfile;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Collider> m_Colliders = new global::System.Collections.Generic.List<global::UnityEngine.Collider>();

		private global::UnityEngine.GameObject m_CachedGameObject;

		private int m_PreviousLayer;

		private float m_PreviousPriority;

		private global::UnityEngine.Rendering.VolumeProfile m_InternalProfile;

		public bool isGlobal
		{
			get
			{
				return m_IsGlobal;
			}
			set
			{
				m_IsGlobal = value;
				if (!m_IsGlobal)
				{
					UpdateColliders();
				}
			}
		}

		public global::UnityEngine.Rendering.VolumeProfile profile
		{
			get
			{
				if (m_InternalProfile == null)
				{
					m_InternalProfile = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.Rendering.VolumeProfile>();
					if (sharedProfile != null)
					{
						m_InternalProfile.name = sharedProfile.name;
						foreach (global::UnityEngine.Rendering.VolumeComponent component in sharedProfile.components)
						{
							global::UnityEngine.Rendering.VolumeComponent item = global::UnityEngine.Object.Instantiate(component);
							m_InternalProfile.components.Add(item);
						}
					}
				}
				return m_InternalProfile;
			}
			set
			{
				m_InternalProfile = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Collider> colliders => m_Colliders;

		internal global::UnityEngine.GameObject cachedGameObject => m_CachedGameObject;

		internal global::UnityEngine.Rendering.VolumeProfile profileRef
		{
			get
			{
				if (!(m_InternalProfile == null))
				{
					return m_InternalProfile;
				}
				return sharedProfile;
			}
		}

		public bool HasInstantiatedProfile()
		{
			return m_InternalProfile != null;
		}

		private void OnEnable()
		{
			m_CachedGameObject = base.gameObject;
			m_PreviousLayer = cachedGameObject.layer;
			global::UnityEngine.Rendering.VolumeManager.instance.Register(this);
			UpdateColliders();
		}

		private void OnDisable()
		{
			global::UnityEngine.Rendering.VolumeManager.instance.Unregister(this);
		}

		private void Update()
		{
			UpdateLayer();
			UpdatePriority();
		}

		public void UpdateColliders()
		{
			GetComponents(m_Colliders);
		}

		internal void UpdateLayer()
		{
			int layer = cachedGameObject.layer;
			if (layer != m_PreviousLayer)
			{
				global::UnityEngine.Rendering.VolumeManager.instance.UpdateVolumeLayer(this, m_PreviousLayer, layer);
				m_PreviousLayer = layer;
			}
		}

		internal void UpdatePriority()
		{
			if (global::UnityEngine.Mathf.Abs(priority - m_PreviousPriority) > global::UnityEngine.Mathf.Epsilon)
			{
				global::UnityEngine.Rendering.VolumeManager.instance.SetLayerDirty(cachedGameObject.layer);
				m_PreviousPriority = priority;
			}
		}

		private void OnValidate()
		{
			blendDistance = global::UnityEngine.Mathf.Max(blendDistance, 0f);
		}
	}
}
