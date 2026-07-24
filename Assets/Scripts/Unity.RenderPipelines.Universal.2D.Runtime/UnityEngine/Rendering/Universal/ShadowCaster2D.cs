namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.AddComponentMenu("Rendering/2D/Shadow Caster 2D")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(false, "UnityEngine.Experimental.Rendering.Universal", "com.unity.render-pipelines.universal", null)]
	public class ShadowCaster2D : global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D, global::UnityEngine.ISerializationCallbackReceiver
	{
		internal enum ComponentVersions
		{
			Version_Unserialized = 0,
			Version_1 = 1,
			Version_2 = 2,
			Version_3 = 3,
			Version_4 = 4,
			Version_5 = 5
		}

		internal enum ShadowCastingSources
		{
			None = 0,
			ShapeEditor = 1,
			ShapeProvider = 2
		}

		public enum ShadowCastingOptions
		{
			SelfShadow = 0,
			CastShadow = 1,
			CastAndSelfShadow = 2,
			NoShadow = 3
		}

		internal enum EdgeProcessing
		{
			None = 0,
			Clipping = 1
		}

		private const global::UnityEngine.Rendering.Universal.ShadowCaster2D.ComponentVersions k_CurrentComponentVersion = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ComponentVersions.Version_5;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShadowCaster2D.ComponentVersions m_ComponentVersion;

		[global::UnityEngine.SerializeField]
		private bool m_HasRenderer;

		[global::UnityEngine.SerializeField]
		private bool m_UseRendererSilhouette = true;

		[global::UnityEngine.SerializeField]
		private bool m_CastsShadows = true;

		[global::UnityEngine.SerializeField]
		private bool m_SelfShadows;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_AlphaCutoff = 0.1f;

		[global::UnityEngine.SerializeField]
		private int[] m_ApplyToSortingLayers;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector3[] m_ShapePath;

		[global::UnityEngine.SerializeField]
		private int m_ShapePathHash;

		[global::UnityEngine.SerializeField]
		private int m_InstanceId;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Component m_ShadowShape2DComponent;

		[global::UnityEngine.SerializeReference]
		private global::UnityEngine.Rendering.Universal.ShadowShape2DProvider m_ShadowShape2DProvider;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources m_ShadowCastingSource = (global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources)(-1);

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.ShadowMesh2D m_ShadowMesh;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions m_CastingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastShadow;

		[global::UnityEngine.SerializeField]
		internal float m_PreviousTrimEdge;

		[global::UnityEngine.SerializeField]
		internal int m_PreviousEdgeProcessing;

		[global::UnityEngine.SerializeField]
		internal int m_PreviousShadowCastingSource;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Component m_PreviousShadowShape2DSource;

		internal global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D m_ShadowCasterGroup;

		internal global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D m_PreviousShadowCasterGroup;

		internal bool m_ForceShadowMeshRebuild;

		private int m_PreviousShadowGroup;

		private bool m_PreviousCastsShadows = true;

		private int m_PreviousPathHash;

		private int m_SpriteMaterialCount;

		internal global::UnityEngine.Vector3 m_CachedPosition;

		internal global::UnityEngine.Vector3 m_CachedLossyScale;

		internal global::UnityEngine.Quaternion m_CachedRotation;

		internal global::UnityEngine.Matrix4x4 m_CachedShadowMatrix;

		internal global::UnityEngine.Matrix4x4 m_CachedInverseShadowMatrix;

		internal global::UnityEngine.Matrix4x4 m_CachedLocalToWorldMatrix;

		internal global::UnityEngine.Rendering.Universal.ShadowCaster2D.EdgeProcessing edgeProcessing
		{
			get
			{
				return (global::UnityEngine.Rendering.Universal.ShadowCaster2D.EdgeProcessing)m_ShadowMesh.edgeProcessing;
			}
			set
			{
				m_ShadowMesh.edgeProcessing = (global::UnityEngine.Rendering.Universal.ShadowMesh2D.EdgeProcessing)value;
			}
		}

		public global::UnityEngine.Mesh mesh => m_ShadowMesh.mesh;

		public global::UnityEngine.BoundingSphere boundingSphere => m_ShadowMesh.boundingSphere;

		public float trimEdge
		{
			get
			{
				return m_ShadowMesh.trimEdge;
			}
			set
			{
				m_ShadowMesh.trimEdge = value;
			}
		}

		public float alphaCutoff
		{
			get
			{
				return m_AlphaCutoff;
			}
			set
			{
				m_AlphaCutoff = value;
			}
		}

		public global::UnityEngine.Vector3[] shapePath => m_ShapePath;

		internal int shapePathHash
		{
			get
			{
				return m_ShapePathHash;
			}
			set
			{
				m_ShapePathHash = value;
			}
		}

		internal global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources shadowCastingSource
		{
			get
			{
				return m_ShadowCastingSource;
			}
			set
			{
				m_ShadowCastingSource = value;
			}
		}

		internal global::UnityEngine.Component shadowShape2DComponent
		{
			get
			{
				return m_ShadowShape2DComponent;
			}
			set
			{
				m_ShadowShape2DComponent = value;
			}
		}

		internal global::UnityEngine.Rendering.Universal.ShadowShape2DProvider shadowShape2DProvider
		{
			get
			{
				return m_ShadowShape2DProvider;
			}
			set
			{
				m_ShadowShape2DProvider = value;
			}
		}

		internal int spriteMaterialCount => m_SpriteMaterialCount;

		public global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions castingOption
		{
			get
			{
				return m_CastingOption;
			}
			set
			{
				m_CastingOption = value;
			}
		}

		[global::System.Obsolete("useRendererSilhoutte is deprecated. Use selfShadows instead. #from(2023.1)")]
		public bool useRendererSilhouette
		{
			get
			{
				if (m_UseRendererSilhouette)
				{
					return m_HasRenderer;
				}
				return false;
			}
			set
			{
				m_UseRendererSilhouette = value;
			}
		}

		public bool selfShadows
		{
			get
			{
				if (castingOption != global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow)
				{
					return castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.SelfShadow;
				}
				return true;
			}
			set
			{
				if (value)
				{
					if (castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastShadow)
					{
						castingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow;
					}
					else if (castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.NoShadow)
					{
						castingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.SelfShadow;
					}
				}
				else if (castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow)
				{
					castingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastShadow;
				}
				else if (castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.SelfShadow)
				{
					castingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.NoShadow;
				}
			}
		}

		public bool castsShadows
		{
			get
			{
				if (castingOption != global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastShadow)
				{
					return castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow;
				}
				return true;
			}
			set
			{
				if (value)
				{
					if (castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.SelfShadow)
					{
						castingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow;
					}
					else if (castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.NoShadow)
					{
						castingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastShadow;
					}
				}
				else if (castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow)
				{
					castingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.SelfShadow;
				}
				else if (castingOption == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastShadow)
				{
					castingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.NoShadow;
				}
			}
		}

		internal override void CacheValues()
		{
			m_CachedPosition = base.transform.position;
			m_CachedLossyScale = base.transform.lossyScale;
			m_CachedRotation = base.transform.rotation;
			m_ShadowMesh.GetFlip(out var flipX, out var flipY);
			global::UnityEngine.Vector3 s = new global::UnityEngine.Vector3((!flipX) ? 1 : (-1), (!flipY) ? 1 : (-1), 1f);
			m_CachedShadowMatrix = global::UnityEngine.Matrix4x4.TRS(m_CachedPosition, m_CachedRotation, s);
			m_CachedInverseShadowMatrix = m_CachedShadowMatrix.inverse;
			m_CachedLocalToWorldMatrix = base.transform.localToWorldMatrix;
		}

		private static int[] SetDefaultSortingLayers()
		{
			int num = global::UnityEngine.SortingLayer.layers.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = global::UnityEngine.SortingLayer.layers[i].id;
			}
			return array;
		}

		internal bool IsLit(global::UnityEngine.Rendering.Universal.Light2D light)
		{
			global::UnityEngine.Vector3 vector = default(global::UnityEngine.Vector3);
			vector.x = light.m_CachedPosition.x - boundingSphere.position.x;
			vector.y = light.m_CachedPosition.y - boundingSphere.position.y;
			vector.z = light.m_CachedPosition.z - boundingSphere.position.z;
			float num = global::UnityEngine.Vector3.SqrMagnitude(vector);
			float num2 = light.boundingSphere.radius + boundingSphere.radius;
			return num <= num2 * num2;
		}

		internal bool IsShadowedLayer(int layer)
		{
			if (m_ApplyToSortingLayers == null)
			{
				return false;
			}
			return global::System.Array.IndexOf(m_ApplyToSortingLayers, layer) >= 0;
		}

		private void SetShadowShape(global::UnityEngine.Rendering.Universal.ShadowMesh2D shadowMesh)
		{
			m_ForceShadowMeshRebuild = false;
			if (m_ShadowCastingSource == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources.ShapeEditor)
			{
				global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(m_ShapePath, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.NativeArray<int> indices = new global::Unity.Collections.NativeArray<int>(2 * m_ShapePath.Length, global::Unity.Collections.Allocator.Temp);
				int value = m_ShapePath.Length - 1;
				for (int i = 0; i < m_ShapePath.Length; i++)
				{
					int num = i << 1;
					indices[num] = value;
					indices[num + 1] = i;
					value = i;
				}
				shadowMesh.SetShapeWithLines(vertices, indices, allowTrimming: false);
				vertices.Dispose();
				indices.Dispose();
			}
			if (m_ShadowCastingSource == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources.ShapeProvider)
			{
				global::UnityEngine.Rendering.Universal.ShapeProviderUtility.PersistantDataCreated(m_ShadowShape2DProvider, m_ShadowShape2DComponent, shadowMesh);
			}
		}

		private void Awake()
		{
			if (m_ShadowCastingSource < global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources.None)
			{
				m_ShadowCastingSource = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources.ShapeEditor;
			}
			global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.zero;
			global::UnityEngine.Vector3 vector2 = base.transform.position;
			if (base.transform.lossyScale.x != 0f && base.transform.lossyScale.y != 0f)
			{
				vector = new global::UnityEngine.Vector3(1f / base.transform.lossyScale.x, 1f / base.transform.lossyScale.y);
				vector2 = new global::UnityEngine.Vector3(vector.x * (0f - base.transform.position.x), vector.y * (0f - base.transform.position.y));
			}
			if (m_ApplyToSortingLayers == null)
			{
				m_ApplyToSortingLayers = SetDefaultSortingLayers();
			}
			global::UnityEngine.Bounds bounds = new global::UnityEngine.Bounds(base.transform.position, global::UnityEngine.Vector3.one);
			global::UnityEngine.Renderer component = GetComponent<global::UnityEngine.Renderer>();
			if (component != null)
			{
				bounds = component.bounds;
				m_SpriteMaterialCount = component.sharedMaterials.Length;
			}
			if (m_ShapePath == null || m_ShapePath.Length == 0)
			{
				m_ShapePath = new global::UnityEngine.Vector3[4]
				{
					vector2 + new global::UnityEngine.Vector3(vector.x * bounds.min.x, vector.y * bounds.min.y),
					vector2 + new global::UnityEngine.Vector3(vector.x * bounds.min.x, vector.y * bounds.max.y),
					vector2 + new global::UnityEngine.Vector3(vector.x * bounds.max.x, vector.y * bounds.max.y),
					vector2 + new global::UnityEngine.Vector3(vector.x * bounds.max.x, vector.y * bounds.min.y)
				};
			}
			if (m_ShadowMesh == null)
			{
				global::UnityEngine.Rendering.Universal.ShadowMesh2D shadowMesh2D = new global::UnityEngine.Rendering.Universal.ShadowMesh2D();
				SetShadowShape(shadowMesh2D);
				m_ShadowMesh = shadowMesh2D;
			}
		}

		protected void OnEnable()
		{
			if (m_ShadowShape2DProvider != null)
			{
				m_ShadowShape2DProvider.Enabled(m_ShadowShape2DComponent, m_ShadowMesh);
			}
			m_ShadowCasterGroup = null;
		}

		protected void OnDisable()
		{
			global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.RemoveFromShadowCasterGroup(this, m_ShadowCasterGroup);
			if (m_ShadowShape2DProvider != null)
			{
				m_ShadowShape2DProvider.Disabled(m_ShadowShape2DComponent, m_ShadowMesh);
			}
		}

		public void Update()
		{
			m_HasRenderer = TryGetComponent<global::UnityEngine.Renderer>(out var _);
			bool flag = global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange((int)m_ShadowCastingSource, ref m_PreviousShadowCastingSource);
			flag |= global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange((int)edgeProcessing, ref m_PreviousEdgeProcessing);
			flag |= edgeProcessing != global::UnityEngine.Rendering.Universal.ShadowCaster2D.EdgeProcessing.None && global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(trimEdge, ref m_PreviousTrimEdge);
			flag |= m_ForceShadowMeshRebuild;
			if (m_ShadowCastingSource == global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingSources.ShapeEditor)
			{
				if (flag | global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(m_ShapePathHash, ref m_PreviousPathHash))
				{
					SetShadowShape(m_ShadowMesh);
				}
			}
			else if ((flag || global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(m_ShadowShape2DComponent, ref m_PreviousShadowShape2DSource)) && m_ShadowShape2DComponent != null)
			{
				SetShadowShape(m_ShadowMesh);
			}
			m_PreviousShadowCasterGroup = m_ShadowCasterGroup;
			if (global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.AddToShadowCasterGroup(this, ref m_ShadowCasterGroup, ref m_Priority) && m_ShadowCasterGroup != null)
			{
				if (m_PreviousShadowCasterGroup == this)
				{
					global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.RemoveGroup(this);
				}
				global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.RemoveFromShadowCasterGroup(this, m_PreviousShadowCasterGroup);
				if (m_ShadowCasterGroup == this)
				{
					global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.AddGroup(this);
				}
			}
			if (global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(m_ShadowGroup, ref m_PreviousShadowGroup))
			{
				global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.RemoveGroup(this);
				global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.AddGroup(this);
			}
			if (global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(m_CastsShadows, ref m_PreviousCastsShadows))
			{
				global::UnityEngine.Rendering.Universal.ShadowCasterGroup2DManager.AddGroup(this);
			}
			if (m_ShadowMesh != null)
			{
				m_ShadowMesh.UpdateBoundingSphere(base.transform);
			}
		}

		public void OnBeforeSerialize()
		{
			m_ComponentVersion = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ComponentVersions.Version_5;
		}

		public void OnAfterDeserialize()
		{
			if (m_ComponentVersion < global::UnityEngine.Rendering.Universal.ShadowCaster2D.ComponentVersions.Version_2)
			{
				if (m_SelfShadows && m_CastsShadows)
				{
					m_CastingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastAndSelfShadow;
				}
				else if (m_SelfShadows)
				{
					m_CastingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.SelfShadow;
				}
				else if (m_CastsShadows)
				{
					m_CastingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.CastShadow;
				}
				else
				{
					m_CastingOption = global::UnityEngine.Rendering.Universal.ShadowCaster2D.ShadowCastingOptions.NoShadow;
				}
			}
			if (m_ComponentVersion < global::UnityEngine.Rendering.Universal.ShadowCaster2D.ComponentVersions.Version_3)
			{
				m_ShadowMesh = null;
				m_ForceShadowMeshRebuild = true;
			}
		}
	}
}
