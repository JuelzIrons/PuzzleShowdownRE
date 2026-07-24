namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", "Unity.RenderPipelines.Universal.Runtime", null)]
	[global::UnityEngine.AddComponentMenu("Rendering/2D/Light 2D")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/2DLightProperties.html")]
	public sealed class Light2D : global::UnityEngine.U2D.Light2DBase, global::UnityEngine.ISerializationCallbackReceiver
	{
		public enum DeprecatedLightType
		{
			Parametric = 0
		}

		public enum LightType
		{
			Parametric = 0,
			Freeform = 1,
			Sprite = 2,
			Point = 3,
			Global = 4
		}

		public enum NormalMapQuality
		{
			Disabled = 2,
			Fast = 0,
			Accurate = 1
		}

		public enum OverlapOperation
		{
			Additive = 0,
			AlphaBlend = 1
		}

		private enum ComponentVersions
		{
			Version_Unserialized = 0,
			Version_1 = 1,
			Version_2 = 2
		}

		private const global::UnityEngine.Rendering.Universal.Light2D.ComponentVersions k_CurrentComponentVersion = global::UnityEngine.Rendering.Universal.Light2D.ComponentVersions.Version_2;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.Light2D.ComponentVersions m_ComponentVersion;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.Light2D.LightType m_LightType = global::UnityEngine.Rendering.Universal.Light2D.LightType.Point;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_LightOperationIndex")]
		private int m_BlendStyleIndex;

		[global::UnityEngine.SerializeField]
		private float m_FalloffIntensity = 0.5f;

		[global::UnityEngine.ColorUsage(true)]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_Color = global::UnityEngine.Color.white;

		[global::UnityEngine.SerializeField]
		private float m_Intensity = 1f;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_LightVolumeOpacity")]
		[global::UnityEngine.SerializeField]
		private float m_LightVolumeIntensity = 1f;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_LightVolumeIntensityEnabled")]
		[global::UnityEngine.SerializeField]
		private bool m_LightVolumeEnabled;

		[global::UnityEngine.SerializeField]
		private int[] m_ApplyToSortingLayers;

		[global::UnityEngine.Rendering.Reload("Textures/2D/Sparkle.png", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_LightCookieSprite;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_LightCookieSprite")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_DeprecatedPointLightCookieSprite;

		[global::UnityEngine.SerializeField]
		private int m_LightOrder;

		[global::UnityEngine.SerializeField]
		private bool m_AlphaBlendOnOverlap;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.Light2D.OverlapOperation m_OverlapOperation;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_PointLightDistance")]
		[global::UnityEngine.SerializeField]
		private float m_NormalMapDistance = 3f;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_PointLightQuality")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality m_NormalMapQuality = global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality.Disabled;

		[global::UnityEngine.SerializeField]
		private bool m_UseNormalMap;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_ShadowIntensityEnabled")]
		[global::UnityEngine.SerializeField]
		private bool m_ShadowsEnabled = true;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_ShadowIntensity = 0.75f;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_ShadowSoftness = 0.3f;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_ShadowSoftnessFalloffIntensity = 0.5f;

		[global::UnityEngine.SerializeField]
		private bool m_ShadowVolumeIntensityEnabled;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_ShadowVolumeIntensity = 0.75f;

		private global::UnityEngine.Mesh m_Mesh;

		[global::System.NonSerialized]
		private global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex[] m_Vertices = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex[1];

		[global::System.NonSerialized]
		private ushort[] m_Triangles = new ushort[1];

		private int m_PreviousLightCookieSprite;

		internal global::UnityEngine.Vector3 m_CachedPosition;

		private int m_BatchSlotIndex;

		internal global::UnityEngine.Rendering.RTHandle m_CookieSpriteTexture;

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle m_CookieSpriteTextureHandle;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Bounds m_LocalBounds;

		internal bool forceUpdate;

		[global::UnityEngine.SerializeField]
		private float m_PointLightInnerAngle = 360f;

		[global::UnityEngine.SerializeField]
		private float m_PointLightOuterAngle = 360f;

		[global::UnityEngine.SerializeField]
		private float m_PointLightInnerRadius;

		[global::UnityEngine.SerializeField]
		private float m_PointLightOuterRadius = 1f;

		[global::UnityEngine.SerializeField]
		private int m_ShapeLightParametricSides = 5;

		[global::UnityEngine.SerializeField]
		private float m_ShapeLightParametricAngleOffset;

		[global::UnityEngine.SerializeField]
		private float m_ShapeLightParametricRadius = 1f;

		[global::UnityEngine.SerializeField]
		private float m_ShapeLightFalloffSize = 0.5f;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_ShapeLightFalloffOffset = global::UnityEngine.Vector2.zero;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector3[] m_ShapePath;

		private float m_PreviousShapeLightFalloffSize = -1f;

		private int m_PreviousShapeLightParametricSides = -1;

		private float m_PreviousShapeLightParametricAngleOffset = -1f;

		private float m_PreviousShapeLightParametricRadius = -1f;

		private int m_PreviousShapePathHash = -1;

		private global::UnityEngine.Rendering.Universal.Light2D.LightType m_PreviousLightType;

		internal global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex[] vertices
		{
			get
			{
				return m_Vertices;
			}
			set
			{
				m_Vertices = value;
			}
		}

		internal ushort[] indices
		{
			get
			{
				return m_Triangles;
			}
			set
			{
				m_Triangles = value;
			}
		}

		internal int batchSlotIndex
		{
			get
			{
				return m_BatchSlotIndex;
			}
			set
			{
				m_BatchSlotIndex = value;
			}
		}

		private int lightCookieSpriteInstanceID => lightCookieSprite?.GetInstanceID() ?? 0;

		internal bool useCookieSprite
		{
			get
			{
				if (lightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Point || lightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Sprite)
				{
					if (lightCookieSprite != null)
					{
						return lightCookieSprite.texture != null;
					}
					return false;
				}
				return false;
			}
		}

		internal global::UnityEngine.BoundingSphere boundingSphere { get; private set; }

		internal global::UnityEngine.Mesh lightMesh
		{
			get
			{
				if (null == m_Mesh)
				{
					m_Mesh = new global::UnityEngine.Mesh();
				}
				return m_Mesh;
			}
		}

		internal bool hasCachedMesh
		{
			get
			{
				if (vertices.Length > 1)
				{
					return indices.Length > 1;
				}
				return false;
			}
		}

		public global::UnityEngine.Rendering.Universal.Light2D.LightType lightType
		{
			get
			{
				return m_LightType;
			}
			set
			{
				if (m_LightType != value)
				{
					UpdateMesh();
				}
				m_LightType = value;
				global::UnityEngine.Rendering.Universal.Light2DManager.ErrorIfDuplicateGlobalLight(this);
			}
		}

		public int blendStyleIndex
		{
			get
			{
				return m_BlendStyleIndex;
			}
			set
			{
				m_BlendStyleIndex = value;
			}
		}

		public float shadowIntensity
		{
			get
			{
				return m_ShadowIntensity;
			}
			set
			{
				m_ShadowIntensity = global::UnityEngine.Mathf.Clamp01(value);
			}
		}

		public float shadowSoftness
		{
			get
			{
				return m_ShadowSoftness;
			}
			set
			{
				m_ShadowSoftness = value;
			}
		}

		public bool shadowsEnabled
		{
			get
			{
				return m_ShadowsEnabled;
			}
			set
			{
				m_ShadowsEnabled = value;
			}
		}

		public float shadowVolumeIntensity
		{
			get
			{
				return m_ShadowVolumeIntensity;
			}
			set
			{
				m_ShadowVolumeIntensity = global::UnityEngine.Mathf.Clamp01(value);
			}
		}

		public bool volumetricShadowsEnabled
		{
			get
			{
				return m_ShadowVolumeIntensityEnabled;
			}
			set
			{
				m_ShadowVolumeIntensityEnabled = value;
			}
		}

		public global::UnityEngine.Color color
		{
			get
			{
				return m_Color;
			}
			set
			{
				m_Color = value;
			}
		}

		public float intensity
		{
			get
			{
				return m_Intensity;
			}
			set
			{
				m_Intensity = value;
			}
		}

		[global::System.Obsolete("#from(2021.1)")]
		public float volumeOpacity => m_LightVolumeIntensity;

		public float volumeIntensity
		{
			get
			{
				return m_LightVolumeIntensity;
			}
			set
			{
				m_LightVolumeIntensity = value;
			}
		}

		[global::System.Obsolete("#from(2023.1)")]
		public bool volumeIntensityEnabled
		{
			get
			{
				return m_LightVolumeEnabled;
			}
			set
			{
				m_LightVolumeEnabled = value;
			}
		}

		public bool volumetricEnabled
		{
			get
			{
				return m_LightVolumeEnabled;
			}
			set
			{
				m_LightVolumeEnabled = value;
			}
		}

		public global::UnityEngine.Sprite lightCookieSprite
		{
			get
			{
				if (m_LightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Point)
				{
					return m_DeprecatedPointLightCookieSprite;
				}
				return m_LightCookieSprite;
			}
			set
			{
				m_LightCookieSprite = value;
			}
		}

		public float falloffIntensity
		{
			get
			{
				return m_FalloffIntensity;
			}
			set
			{
				m_FalloffIntensity = global::UnityEngine.Mathf.Clamp(value, 0f, 1f);
			}
		}

		public float shadowSoftnessFalloffIntensity
		{
			get
			{
				return m_ShadowSoftnessFalloffIntensity;
			}
			set
			{
				m_ShadowSoftnessFalloffIntensity = global::UnityEngine.Mathf.Clamp(value, 0f, 1f);
			}
		}

		[global::System.Obsolete("#from(2021.1)")]
		public bool alphaBlendOnOverlap => m_OverlapOperation == global::UnityEngine.Rendering.Universal.Light2D.OverlapOperation.AlphaBlend;

		public global::UnityEngine.Rendering.Universal.Light2D.OverlapOperation overlapOperation
		{
			get
			{
				return m_OverlapOperation;
			}
			set
			{
				m_OverlapOperation = value;
			}
		}

		public int lightOrder
		{
			get
			{
				return m_LightOrder;
			}
			set
			{
				m_LightOrder = value;
			}
		}

		public float normalMapDistance => m_NormalMapDistance;

		public global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality normalMapQuality => m_NormalMapQuality;

		public bool renderVolumetricShadows
		{
			get
			{
				if (volumetricShadowsEnabled)
				{
					return shadowVolumeIntensity > 0f;
				}
				return false;
			}
		}

		public int[] targetSortingLayers
		{
			get
			{
				return m_ApplyToSortingLayers;
			}
			set
			{
				global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
				foreach (int num in value)
				{
					if (global::UnityEngine.SortingLayer.IsValid(num))
					{
						list.Add(num);
					}
				}
				m_ApplyToSortingLayers = list.ToArray();
			}
		}

		public float pointLightInnerAngle
		{
			get
			{
				return m_PointLightInnerAngle;
			}
			set
			{
				m_PointLightInnerAngle = value;
			}
		}

		public float pointLightOuterAngle
		{
			get
			{
				return m_PointLightOuterAngle;
			}
			set
			{
				m_PointLightOuterAngle = value;
			}
		}

		public float pointLightInnerRadius
		{
			get
			{
				return m_PointLightInnerRadius;
			}
			set
			{
				m_PointLightInnerRadius = value;
			}
		}

		public float pointLightOuterRadius
		{
			get
			{
				return m_PointLightOuterRadius;
			}
			set
			{
				m_PointLightOuterRadius = value;
			}
		}

		[global::System.Obsolete("pointLightDistance has been changed to normalMapDistance #from(2021.1) #breakingFrom(2021.1)", true)]
		public float pointLightDistance => m_NormalMapDistance;

		[global::System.Obsolete("pointLightQuality has been changed to normalMapQuality #from(2021.1) #breakingFrom(2021.1)", true)]
		public global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality pointLightQuality => m_NormalMapQuality;

		internal bool isPointLight => m_LightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Point;

		public int shapeLightParametricSides => m_ShapeLightParametricSides;

		public float shapeLightParametricAngleOffset => m_ShapeLightParametricAngleOffset;

		public float shapeLightParametricRadius
		{
			get
			{
				return m_ShapeLightParametricRadius;
			}
			internal set
			{
				m_ShapeLightParametricRadius = value;
			}
		}

		public float shapeLightFalloffSize
		{
			get
			{
				return m_ShapeLightFalloffSize;
			}
			set
			{
				m_ShapeLightFalloffSize = global::UnityEngine.Mathf.Max(0f, value);
			}
		}

		public global::UnityEngine.Vector3[] shapePath
		{
			get
			{
				return m_ShapePath;
			}
			internal set
			{
				m_ShapePath = value;
			}
		}

		private bool IsValidLayer(string name)
		{
			global::UnityEngine.SortingLayer[] cachedSortingLayer = global::UnityEngine.Rendering.Universal.Light2DManager.GetCachedSortingLayer();
			foreach (global::UnityEngine.SortingLayer sortingLayer in cachedSortingLayer)
			{
				if (sortingLayer.name == name)
				{
					return true;
				}
			}
			return false;
		}

		public bool AddTargetSortingLayer(string layerName)
		{
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>(m_ApplyToSortingLayers);
			int item = global::UnityEngine.SortingLayer.NameToID(layerName);
			if (!IsValidLayer(layerName) || list.Contains(item))
			{
				return false;
			}
			list.Add(item);
			m_ApplyToSortingLayers = list.ToArray();
			return true;
		}

		public bool AddTargetSortingLayer(int layerID)
		{
			return AddTargetSortingLayer(global::UnityEngine.SortingLayer.IDToName(layerID));
		}

		public bool RemoveTargetSortingLayer(string layerName)
		{
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>(m_ApplyToSortingLayers);
			int item = global::UnityEngine.SortingLayer.NameToID(layerName);
			if (!IsValidLayer(layerName) || !list.Contains(item))
			{
				return false;
			}
			list.Remove(item);
			m_ApplyToSortingLayers = list.ToArray();
			return true;
		}

		public bool RemoveTargetSortingLayer(int layerID)
		{
			return RemoveTargetSortingLayer(global::UnityEngine.SortingLayer.IDToName(layerID));
		}

		internal void MarkForUpdate()
		{
			forceUpdate = true;
		}

		internal void CacheValues()
		{
			m_CachedPosition = base.transform.position;
		}

		internal int GetTopMostLitLayer()
		{
			int result = int.MinValue;
			int num = 0;
			global::UnityEngine.SortingLayer[] cachedSortingLayer = global::UnityEngine.Rendering.Universal.Light2DManager.GetCachedSortingLayer();
			for (int i = 0; i < m_ApplyToSortingLayers.Length; i++)
			{
				for (int num2 = cachedSortingLayer.Length - 1; num2 >= num; num2--)
				{
					if (cachedSortingLayer[num2].id == m_ApplyToSortingLayers[i])
					{
						result = cachedSortingLayer[num2].value;
						num = num2;
					}
				}
			}
			return result;
		}

		internal global::UnityEngine.Bounds UpdateSpriteMesh()
		{
			if (m_LightCookieSprite == null && (m_Vertices.Length != 1 || m_Triangles.Length != 1))
			{
				m_Vertices = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex[1];
				m_Triangles = new ushort[1];
			}
			return global::UnityEngine.Rendering.Universal.LightUtility.GenerateSpriteMesh(this, m_LightCookieSprite, global::UnityEngine.Rendering.Universal.LightBatch.GetBatchColor());
		}

		internal void UpdateBatchSlotIndex()
		{
			if ((bool)lightMesh && lightMesh.colors != null && lightMesh.colors.Length != 0)
			{
				m_BatchSlotIndex = global::UnityEngine.Rendering.Universal.LightBatch.GetBatchSlotIndex(lightMesh.colors[0].b);
			}
		}

		internal bool NeedsColorIndexBaking()
		{
			if ((bool)lightMesh && global::UnityEngine.Rendering.Universal.LightBatch.isBatchingSupported && lightMesh.colors.Length != 0)
			{
				return lightMesh.colors[0].b == 0f;
			}
			return false;
		}

		internal void UpdateCookieSpriteTexture()
		{
			m_CookieSpriteTexture?.Release();
			if (useCookieSprite)
			{
				m_CookieSpriteTexture = global::UnityEngine.Rendering.RTHandles.Alloc(lightCookieSprite.texture);
			}
		}

		internal void UpdateMesh(bool forceUpdate = false)
		{
			int shapePathHash = global::UnityEngine.Rendering.Universal.LightUtility.GetShapePathHash(shapePath);
			bool flag = global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(m_ShapeLightFalloffSize, ref m_PreviousShapeLightFalloffSize);
			bool flag2 = global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(m_ShapeLightParametricRadius, ref m_PreviousShapeLightParametricRadius);
			bool flag3 = global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(m_ShapeLightParametricSides, ref m_PreviousShapeLightParametricSides);
			bool flag4 = global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(m_ShapeLightParametricAngleOffset, ref m_PreviousShapeLightParametricAngleOffset);
			bool flag5 = global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(lightCookieSpriteInstanceID, ref m_PreviousLightCookieSprite);
			bool flag6 = global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(shapePathHash, ref m_PreviousShapePathHash);
			bool flag7 = global::UnityEngine.Rendering.Universal.LightUtility.CheckForChange(m_LightType, ref m_PreviousLightType);
			if (flag || flag2 || flag3 || flag4 || flag5 || flag6 || flag7 || NeedsColorIndexBaking() || forceUpdate)
			{
				float batchColor = global::UnityEngine.Rendering.Universal.LightBatch.GetBatchColor();
				switch (m_LightType)
				{
				case global::UnityEngine.Rendering.Universal.Light2D.LightType.Freeform:
					m_LocalBounds = global::UnityEngine.Rendering.Universal.LightUtility.GenerateShapeMesh(this, m_ShapePath, m_ShapeLightFalloffSize, batchColor);
					break;
				case global::UnityEngine.Rendering.Universal.Light2D.LightType.Parametric:
					m_LocalBounds = global::UnityEngine.Rendering.Universal.LightUtility.GenerateParametricMesh(this, m_ShapeLightParametricRadius, m_ShapeLightFalloffSize, m_ShapeLightParametricAngleOffset, m_ShapeLightParametricSides, batchColor);
					break;
				case global::UnityEngine.Rendering.Universal.Light2D.LightType.Sprite:
					m_LocalBounds = UpdateSpriteMesh();
					break;
				case global::UnityEngine.Rendering.Universal.Light2D.LightType.Point:
					m_LocalBounds = global::UnityEngine.Rendering.Universal.LightUtility.GenerateParametricMesh(this, 1.412135f, 0f, 0f, 4, batchColor);
					break;
				}
				UpdateCookieSpriteTexture();
				UpdateBatchSlotIndex();
			}
		}

		internal void UpdateBoundingSphere()
		{
			if (isPointLight)
			{
				boundingSphere = new global::UnityEngine.BoundingSphere(base.transform.position, m_PointLightOuterRadius);
				return;
			}
			global::UnityEngine.Vector3 vector = base.transform.TransformPoint(global::UnityEngine.Vector3.Max(m_LocalBounds.max, m_LocalBounds.max + (global::UnityEngine.Vector3)m_ShapeLightFalloffOffset));
			global::UnityEngine.Vector3 vector2 = base.transform.TransformPoint(global::UnityEngine.Vector3.Min(m_LocalBounds.min, m_LocalBounds.min + (global::UnityEngine.Vector3)m_ShapeLightFalloffOffset));
			global::UnityEngine.Vector3 vector3 = 0.5f * (vector + vector2);
			float rad = global::UnityEngine.Vector3.Magnitude(vector - vector3);
			boundingSphere = new global::UnityEngine.BoundingSphere(vector3, rad);
		}

		internal bool IsLitLayer(int layer)
		{
			if (m_ApplyToSortingLayers == null)
			{
				return false;
			}
			for (int i = 0; i < m_ApplyToSortingLayers.Length; i++)
			{
				if (m_ApplyToSortingLayers[i] == layer)
				{
					return true;
				}
			}
			return false;
		}

		internal global::UnityEngine.Matrix4x4 GetMatrix()
		{
			global::UnityEngine.Matrix4x4 result = base.transform.localToWorldMatrix;
			if (lightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Point)
			{
				result = global::UnityEngine.Matrix4x4.TRS(s: new global::UnityEngine.Vector3(pointLightOuterRadius, pointLightOuterRadius, pointLightOuterRadius), pos: base.transform.position, q: base.transform.rotation);
			}
			return result;
		}

		private void Awake()
		{
			if (m_ApplyToSortingLayers == null)
			{
				m_ApplyToSortingLayers = new int[global::UnityEngine.SortingLayer.layers.Length];
				for (int i = 0; i < m_ApplyToSortingLayers.Length; i++)
				{
					m_ApplyToSortingLayers[i] = global::UnityEngine.SortingLayer.layers[i].id;
				}
			}
		}

		private void OnEnable()
		{
			m_PreviousLightCookieSprite = lightCookieSpriteInstanceID;
			global::UnityEngine.Rendering.Universal.Light2DManager.RegisterLight(this);
			UpdateCookieSpriteTexture();
		}

		private void OnDisable()
		{
			global::UnityEngine.Rendering.Universal.Light2DManager.DeregisterLight(this);
			m_CookieSpriteTexture?.Release();
		}

		private void LateUpdate()
		{
			if (m_LightType != global::UnityEngine.Rendering.Universal.Light2D.LightType.Global)
			{
				UpdateMesh(forceUpdate);
				UpdateBoundingSphere();
				forceUpdate = false;
			}
		}

		public void OnBeforeSerialize()
		{
			m_ComponentVersion = global::UnityEngine.Rendering.Universal.Light2D.ComponentVersions.Version_2;
		}

		public void OnAfterDeserialize()
		{
			if (m_ComponentVersion == global::UnityEngine.Rendering.Universal.Light2D.ComponentVersions.Version_Unserialized)
			{
				m_ShadowVolumeIntensityEnabled = m_ShadowVolumeIntensity > 0f;
				m_ShadowsEnabled = m_ShadowIntensity > 0f;
				m_LightVolumeEnabled = m_LightVolumeIntensity > 0f;
				m_NormalMapQuality = ((!m_UseNormalMap) ? global::UnityEngine.Rendering.Universal.Light2D.NormalMapQuality.Disabled : m_NormalMapQuality);
				m_OverlapOperation = (m_AlphaBlendOnOverlap ? global::UnityEngine.Rendering.Universal.Light2D.OverlapOperation.AlphaBlend : m_OverlapOperation);
				m_ComponentVersion = global::UnityEngine.Rendering.Universal.Light2D.ComponentVersions.Version_1;
			}
			if (m_ComponentVersion < global::UnityEngine.Rendering.Universal.Light2D.ComponentVersions.Version_2)
			{
				m_ShadowSoftness = 0f;
			}
		}

		public void SetShapePath(global::UnityEngine.Vector3[] path)
		{
			m_ShapePath = path;
		}
	}
}
