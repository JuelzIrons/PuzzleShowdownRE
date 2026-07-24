namespace UnityEngine.U2D
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.U2D.SpriteShapeRenderer))]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.spriteshape@latest/index.html?subfolder=/manual/SSController.html")]
	public class SpriteShapeController : global::UnityEngine.MonoBehaviour
	{
		private const float s_DistanceTolerance = 0.001f;

		private global::UnityEngine.U2D.SpriteShape m_ActiveSpriteShape;

		private global::UnityEngine.EdgeCollider2D m_EdgeCollider2D;

		private global::UnityEngine.PolygonCollider2D m_PolygonCollider2D;

		private global::UnityEngine.U2D.SpriteShapeRenderer m_SpriteShapeRenderer;

		private SpriteShapeGeometryCache m_SpriteShapeGeometryCache;

		private global::UnityEngine.Sprite[] m_SpriteArray = new global::UnityEngine.Sprite[0];

		private global::UnityEngine.Sprite[] m_EdgeSpriteArray = new global::UnityEngine.Sprite[0];

		private global::UnityEngine.Sprite[] m_CornerSpriteArray = new global::UnityEngine.Sprite[0];

		private global::UnityEngine.U2D.AngleRangeInfo[] m_AngleRangeInfoArray = new global::UnityEngine.U2D.AngleRangeInfo[0];

		private global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> m_ColliderData;

		private global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> m_ShadowData;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Vector4> m_TangentData;

		private global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SpriteShapeGeneratorStats> m_Statistics;

		private bool m_DynamicOcclusionLocal;

		private bool m_DynamicOcclusionOverriden;

		private bool m_TessellationNeedsFallback;

		private bool m_WaitForBake;

		private int m_ActiveSplineHash;

		private int m_ActiveSpriteShapeHash;

		private int m_MaxArrayCount;

		private global::Unity.Jobs.JobHandle m_JobHandle;

		private global::UnityEngine.U2D.SpriteShapeParameters m_ActiveShapeParameters;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.Spline m_Spline = new global::UnityEngine.U2D.Spline();

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.SpriteShape m_SpriteShape;

		[global::UnityEngine.SerializeField]
		private float m_FillPixelPerUnit = 100f;

		[global::UnityEngine.SerializeField]
		private float m_StretchTiling = 1f;

		[global::UnityEngine.SerializeField]
		private int m_SplineDetail;

		[global::UnityEngine.SerializeField]
		private bool m_AdaptiveUV;

		[global::UnityEngine.SerializeField]
		private bool m_StretchUV;

		[global::UnityEngine.SerializeField]
		private bool m_WorldSpaceUV;

		[global::UnityEngine.SerializeField]
		private float m_CornerAngleThreshold = 30f;

		[global::UnityEngine.SerializeField]
		private int m_ColliderDetail;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(-0.5f, 0.5f)]
		private float m_ColliderOffset;

		[global::UnityEngine.SerializeField]
		private bool m_UpdateCollider = true;

		[global::UnityEngine.SerializeField]
		private bool m_EnableTangents;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private bool m_GeometryCached;

		[global::UnityEngine.SerializeField]
		private bool m_UTess2D = true;

		[global::UnityEngine.SerializeField]
		private bool m_UpdateShadow;

		[global::UnityEngine.SerializeField]
		private int m_ShadowDetail = 16;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(-0.5f, 0.5f)]
		private float m_ShadowOffset = 0.5f;

		[global::UnityEngine.SerializeField]
		private float m_BoundsScale = 2f;

		[global::UnityEngine.SerializeField]
		private bool m_UpdateGeometry = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.SpriteShapeGeometryCreator m_Creator;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.SpriteShapeGeometryModifier> m_Modifiers = new global::System.Collections.Generic.List<global::UnityEngine.U2D.SpriteShapeGeometryModifier>();

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.Vector2> m_ColliderSegment = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.Vector2> m_ShadowSegment = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();

		internal static readonly global::Unity.Profiling.ProfilerMarker generateGeometry = new global::Unity.Profiling.ProfilerMarker("SpriteShape.GenerateGeometry");

		internal static readonly global::Unity.Profiling.ProfilerMarker generateCollider = new global::Unity.Profiling.ProfilerMarker("SpriteShape.GenerateCollider");

		internal int maxArrayCount
		{
			get
			{
				return m_MaxArrayCount;
			}
			set
			{
				m_MaxArrayCount = value;
			}
		}

		internal bool geometryCached
		{
			get
			{
				return m_GeometryCached;
			}
			set
			{
				m_GeometryCached = value;
			}
		}

		internal int splineHashCode => m_ActiveSplineHash;

		internal global::UnityEngine.Sprite[] spriteArray => m_SpriteArray;

		internal global::UnityEngine.U2D.SpriteShapeParameters spriteShapeParameters => m_ActiveShapeParameters;

		internal SpriteShapeGeometryCache spriteShapeGeometryCache
		{
			get
			{
				if (!m_SpriteShapeGeometryCache)
				{
					global::UnityEngine.Component component;
					bool flag = TryGetComponent(typeof(SpriteShapeGeometryCache), out component);
					m_SpriteShapeGeometryCache = (flag ? (component as SpriteShapeGeometryCache) : null);
				}
				return m_SpriteShapeGeometryCache;
			}
		}

		internal global::UnityEngine.Sprite[] cornerSpriteArray => m_CornerSpriteArray;

		internal global::UnityEngine.Sprite[] edgeSpriteArray => m_EdgeSpriteArray;

		internal global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> shadowData => m_ShadowData;

		public global::UnityEngine.U2D.AngleRangeInfo[] angleRangeInfoArray => m_AngleRangeInfoArray;

		public global::UnityEngine.U2D.SpriteShapeGeometryCreator spriteShapeCreator
		{
			get
			{
				if (m_Creator == null)
				{
					m_Creator = global::UnityEngine.U2D.SpriteShapeDefaultCreator.defaultInstance;
				}
				return m_Creator;
			}
			set
			{
				if (value != null)
				{
					m_Creator = value;
				}
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.U2D.SpriteShapeGeometryModifier> modifiers => m_Modifiers;

		public int spriteShapeHashCode => m_ActiveSpriteShapeHash;

		public bool worldSpaceUVs
		{
			get
			{
				return m_WorldSpaceUV;
			}
			set
			{
				m_WorldSpaceUV = value;
			}
		}

		public float fillPixelsPerUnit
		{
			get
			{
				return m_FillPixelPerUnit;
			}
			set
			{
				m_FillPixelPerUnit = value;
			}
		}

		public bool enableTangents
		{
			get
			{
				return m_EnableTangents;
			}
			set
			{
				m_EnableTangents = value;
			}
		}

		public float stretchTiling
		{
			get
			{
				return m_StretchTiling;
			}
			set
			{
				m_StretchTiling = value;
			}
		}

		public int splineDetail
		{
			get
			{
				return m_SplineDetail;
			}
			set
			{
				m_SplineDetail = global::UnityEngine.Mathf.Max(0, value);
			}
		}

		public int colliderDetail
		{
			get
			{
				return m_ColliderDetail;
			}
			set
			{
				m_ColliderDetail = global::UnityEngine.Mathf.Max(0, value);
			}
		}

		public float colliderOffset
		{
			get
			{
				return m_ColliderOffset;
			}
			set
			{
				m_ColliderOffset = value;
			}
		}

		public float cornerAngleThreshold
		{
			get
			{
				return m_CornerAngleThreshold;
			}
			set
			{
				m_CornerAngleThreshold = value;
			}
		}

		public bool autoUpdateCollider
		{
			get
			{
				return m_UpdateCollider;
			}
			set
			{
				m_UpdateCollider = value;
			}
		}

		public bool optimizeCollider => true;

		public bool optimizeGeometry => true;

		public bool hasCollider
		{
			get
			{
				if (!(edgeCollider != null))
				{
					return polygonCollider != null;
				}
				return true;
			}
		}

		public global::UnityEngine.U2D.Spline spline => m_Spline;

		public float boundsScale
		{
			get
			{
				return m_BoundsScale;
			}
			set
			{
				m_BoundsScale = value;
				InitBounds();
			}
		}

		public global::UnityEngine.U2D.SpriteShape spriteShape
		{
			get
			{
				return m_SpriteShape;
			}
			set
			{
				m_SpriteShape = value;
			}
		}

		public global::UnityEngine.EdgeCollider2D edgeCollider
		{
			get
			{
				if (!m_EdgeCollider2D)
				{
					global::UnityEngine.Component component;
					bool flag = TryGetComponent(typeof(global::UnityEngine.EdgeCollider2D), out component);
					m_EdgeCollider2D = (flag ? (component as global::UnityEngine.EdgeCollider2D) : null);
				}
				return m_EdgeCollider2D;
			}
		}

		public global::UnityEngine.PolygonCollider2D polygonCollider
		{
			get
			{
				if (!m_PolygonCollider2D)
				{
					global::UnityEngine.Component component;
					bool flag = TryGetComponent(typeof(global::UnityEngine.PolygonCollider2D), out component);
					m_PolygonCollider2D = (flag ? (component as global::UnityEngine.PolygonCollider2D) : null);
				}
				return m_PolygonCollider2D;
			}
		}

		public global::UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer
		{
			get
			{
				if (!m_SpriteShapeRenderer)
				{
					m_SpriteShapeRenderer = GetComponent<global::UnityEngine.U2D.SpriteShapeRenderer>();
				}
				return m_SpriteShapeRenderer;
			}
		}

		internal bool updateShadow
		{
			get
			{
				return m_UpdateShadow;
			}
			set
			{
				m_UpdateShadow = value;
			}
		}

		internal int shadowDetail
		{
			get
			{
				return m_ShadowDetail;
			}
			set
			{
				m_ShadowDetail = value;
			}
		}

		internal float shadowOffset
		{
			get
			{
				return m_ShadowOffset;
			}
			set
			{
				m_ShadowOffset = value;
			}
		}

		internal global::System.Collections.Generic.List<global::UnityEngine.Vector2> shadowSegment => m_ShadowSegment;

		internal global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SpriteShapeGeneratorStats> stats
		{
			get
			{
				if (!m_Statistics.IsCreated)
				{
					m_Statistics = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SpriteShapeGeneratorStats>(1, global::Unity.Collections.Allocator.Persistent);
				}
				return m_Statistics;
			}
		}

		public bool WaitForBake
		{
			get
			{
				return m_WaitForBake;
			}
			set
			{
				m_WaitForBake = value;
			}
		}

		internal bool autoUpdateGeometry
		{
			get
			{
				return m_UpdateGeometry;
			}
			set
			{
				if (value != m_UpdateGeometry)
				{
					if (!value)
					{
						BakeMesh();
					}
					m_UpdateGeometry = value;
				}
			}
		}

		private void DisposeInternal()
		{
			m_JobHandle.Complete();
			if (m_ColliderData.IsCreated)
			{
				m_ColliderData.Dispose();
			}
			if (m_ShadowData.IsCreated)
			{
				m_ShadowData.Dispose();
			}
			if (m_TangentData.IsCreated)
			{
				m_TangentData.Dispose();
			}
			if (m_Statistics.IsCreated)
			{
				m_Statistics.Dispose();
			}
		}

		private void OnApplicationQuit()
		{
			DisposeInternal();
		}

		private void OnEnable()
		{
			m_DynamicOcclusionOverriden = true;
			m_DynamicOcclusionLocal = spriteShapeRenderer.allowOcclusionWhenDynamic;
			spriteShapeRenderer.allowOcclusionWhenDynamic = false;
			InitBounds();
			UpdateSpriteData();
		}

		private void OnDisable()
		{
			UpdateGeometryCache();
			DisposeInternal();
		}

		private void OnDestroy()
		{
		}

		private void Reset()
		{
			m_SplineDetail = 16;
			m_AdaptiveUV = true;
			m_StretchUV = false;
			m_FillPixelPerUnit = 100f;
			m_ColliderDetail = 16;
			m_ShadowDetail = 16;
			m_StretchTiling = 1f;
			m_WorldSpaceUV = false;
			m_CornerAngleThreshold = 30f;
			m_ColliderOffset = 0f;
			m_ShadowOffset = 0.5f;
			m_UpdateCollider = true;
			m_EnableTangents = false;
			spline.Clear();
			spline.InsertPointAt(0, global::UnityEngine.Vector2.left + global::UnityEngine.Vector2.down);
			spline.InsertPointAt(1, global::UnityEngine.Vector2.left + global::UnityEngine.Vector2.up);
			spline.InsertPointAt(2, global::UnityEngine.Vector2.right + global::UnityEngine.Vector2.up);
			spline.InsertPointAt(3, global::UnityEngine.Vector2.right + global::UnityEngine.Vector2.down);
		}

		private static void SmartDestroy(global::UnityEngine.Object o)
		{
			if (!(o == null))
			{
				global::UnityEngine.Object.Destroy(o);
			}
		}

		internal global::UnityEngine.Bounds InitBounds()
		{
			int pointCount = spline.GetPointCount();
			if (pointCount > 1)
			{
				global::UnityEngine.Bounds bounds = new global::UnityEngine.Bounds(spline.GetPosition(0), global::UnityEngine.Vector3.zero);
				for (int i = 1; i < pointCount; i++)
				{
					bounds.Encapsulate(spline.GetPosition(i));
				}
				bounds.size *= m_BoundsScale;
				bounds.Encapsulate(spriteShapeRenderer.localBounds);
				spriteShapeRenderer.SetLocalAABB(bounds);
				return bounds;
			}
			global::UnityEngine.Bounds bounds2 = new global::UnityEngine.Bounds(base.gameObject.transform.position, global::UnityEngine.Vector3.one);
			spriteShapeRenderer.bounds = bounds2;
			return bounds2;
		}

		public void RefreshSpriteShape()
		{
			m_ActiveSplineHash = 0;
		}

		private bool ValidateSpline()
		{
			int pointCount = spline.GetPointCount();
			if (pointCount < 2)
			{
				return false;
			}
			for (int i = 0; i < pointCount - 1; i++)
			{
				if ((spline.GetPosition(i) - spline.GetPosition(i + 1)).sqrMagnitude < 0.001f)
				{
					global::UnityEngine.Debug.LogWarningFormat(base.gameObject, "[SpriteShape] Control points {0} & {1} are too close. SpriteShape will not be generated for < {2} >.", i, i + 1, base.gameObject.name);
					return false;
				}
			}
			return true;
		}

		private bool ValidateSpriteShapeTexture()
		{
			bool result = false;
			if (spriteShape != null)
			{
				if (!spline.isOpenEnded)
				{
					result = spriteShape.fillTexture != null;
				}
			}
			else
			{
				global::UnityEngine.Debug.LogWarningFormat(base.gameObject, "[SpriteShape] A valid SpriteShape profile has not been set for gameObject < {0} >.", base.gameObject.name);
			}
			return result;
		}

		internal bool ValidateUTess2D()
		{
			bool flag = m_UTess2D;
			if (m_UTess2D && null != spriteShape)
			{
				flag = spriteShape.fillOffset == 0f;
			}
			if (flag)
			{
				return !m_TessellationNeedsFallback;
			}
			return false;
		}

		private bool HasSpriteShapeChanged()
		{
			bool num = m_ActiveSpriteShape != spriteShape;
			if (num)
			{
				m_ActiveSpriteShape = spriteShape;
			}
			return num;
		}

		private bool HasSpriteShapeDataChanged()
		{
			bool result = HasSpriteShapeChanged();
			if ((bool)spriteShape)
			{
				int num = global::UnityEngine.U2D.SpriteShape.GetSpriteShapeHashCode(spriteShape);
				if (spriteShapeHashCode != num)
				{
					m_ActiveSpriteShapeHash = num;
					result = true;
				}
			}
			return result;
		}

		private int GetCustomScriptHashCode()
		{
			int num = 0;
			num = -2128831035 ^ spriteShapeCreator.GetVersion();
			foreach (global::UnityEngine.U2D.SpriteShapeGeometryModifier modifier in m_Modifiers)
			{
				if (null != modifier)
				{
					num = (num * 16777619) ^ modifier.GetVersion();
				}
			}
			return num;
		}

		private bool HasSplineDataChanged()
		{
			int num = -2128831035;
			num = (num * 16777619) ^ spline.GetChangeIndex();
			num = (num * 16777619) ^ (m_UTess2D ? 1 : 0);
			num = (num * 16777619) ^ (m_WorldSpaceUV ? 1 : 0);
			num = (num * 16777619) ^ (m_EnableTangents ? 1 : 0);
			num = (num * 16777619) ^ (m_GeometryCached ? 1 : 0);
			num = (num * 16777619) ^ (m_UpdateShadow ? 1 : 0);
			num = (num * 16777619) ^ (m_UpdateCollider ? 1 : 0);
			num = (num * 16777619) ^ m_StretchTiling.GetHashCode();
			num = (num * 16777619) ^ m_ColliderOffset.GetHashCode();
			num = (num * 16777619) ^ m_ColliderDetail.GetHashCode();
			num = (num * 16777619) ^ m_ShadowOffset.GetHashCode();
			num = (num * 16777619) ^ m_ShadowDetail.GetHashCode();
			num = (num * 16777619) ^ GetCustomScriptHashCode();
			num = (num * 16777619) ^ ((!(edgeCollider == null)) ? 1 : 0);
			num = (num * 16777619) ^ ((!(polygonCollider == null)) ? 1 : 0);
			if (splineHashCode != num)
			{
				m_ActiveSplineHash = num;
				return true;
			}
			return false;
		}

		private void OnBecameInvisible()
		{
			InitBounds();
		}

		private void LateUpdate()
		{
			BakeCollider();
		}

		private void OnWillRenderObject()
		{
			if (autoUpdateGeometry || m_ActiveSplineHash == 0)
			{
				global::Unity.Jobs.JobHandle jobHandle = BakeMesh();
				if (m_WaitForBake)
				{
					jobHandle.Complete();
				}
			}
		}

		public global::Unity.Jobs.JobHandle BakeMesh()
		{
			global::Unity.Jobs.JobHandle result = default(global::Unity.Jobs.JobHandle);
			if ((bool)spriteShapeGeometryCache && m_ActiveSplineHash != 0 && spriteShapeGeometryCache.maxArrayCount != 0)
			{
				return result;
			}
			if (ValidateSpline())
			{
				bool num = HasSplineDataChanged();
				bool flag = HasSpriteShapeDataChanged();
				bool flag2 = UpdateSpriteShapeParameters();
				if (num || flag || flag2 || m_TessellationNeedsFallback)
				{
					if (flag)
					{
						UpdateSpriteData();
					}
					return ScheduleBake();
				}
			}
			return result;
		}

		internal void UpdateGeometryCache()
		{
			if ((bool)spriteShapeGeometryCache && geometryCached)
			{
				m_JobHandle.Complete();
				spriteShapeGeometryCache.UpdateGeometryCache();
			}
		}

		public bool UpdateSpriteShapeParameters()
		{
			bool flag = !spline.isOpenEnded;
			bool flag2 = true;
			bool adaptiveUV = m_AdaptiveUV;
			bool stretchUV = m_StretchUV;
			bool flag3 = false;
			uint num = 0u;
			uint num2 = (uint)m_SplineDetail;
			float num3 = 0f;
			float num4 = ((m_CornerAngleThreshold >= 0f && m_CornerAngleThreshold < 90f) ? m_CornerAngleThreshold : 89.9999f);
			global::UnityEngine.Texture2D texture2D = null;
			global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.identity;
			if ((bool)spriteShape)
			{
				if (worldSpaceUVs)
				{
					matrix4x = base.transform.localToWorldMatrix;
				}
				texture2D = spriteShape.fillTexture;
				num = (stretchUV ? ((uint)stretchTiling) : ((uint)fillPixelsPerUnit));
				num3 = spriteShape.fillOffset;
				flag3 = spriteShape.useSpriteBorders;
				if (spriteShape.cornerSprites.Count > 0)
				{
					flag2 = false;
				}
			}
			bool result = m_ActiveShapeParameters.adaptiveUV != adaptiveUV || m_ActiveShapeParameters.angleThreshold != num4 || m_ActiveShapeParameters.borderPivot != num3 || m_ActiveShapeParameters.carpet != flag || m_ActiveShapeParameters.fillScale != num || m_ActiveShapeParameters.fillTexture != texture2D || m_ActiveShapeParameters.smartSprite != flag2 || m_ActiveShapeParameters.splineDetail != num2 || m_ActiveShapeParameters.spriteBorders != flag3 || m_ActiveShapeParameters.transform != matrix4x || m_ActiveShapeParameters.stretchUV != stretchUV;
			m_ActiveShapeParameters.adaptiveUV = adaptiveUV;
			m_ActiveShapeParameters.stretchUV = stretchUV;
			m_ActiveShapeParameters.angleThreshold = num4;
			m_ActiveShapeParameters.borderPivot = num3;
			m_ActiveShapeParameters.carpet = flag;
			m_ActiveShapeParameters.fillScale = num;
			m_ActiveShapeParameters.fillTexture = texture2D;
			m_ActiveShapeParameters.smartSprite = flag2;
			m_ActiveShapeParameters.splineDetail = num2;
			m_ActiveShapeParameters.spriteBorders = flag3;
			m_ActiveShapeParameters.transform = matrix4x;
			return result;
		}

		private void UpdateSpriteData()
		{
			if ((bool)spriteShape)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Sprite> list = new global::System.Collections.Generic.List<global::UnityEngine.Sprite>();
				global::System.Collections.Generic.List<global::UnityEngine.Sprite> list2 = new global::System.Collections.Generic.List<global::UnityEngine.Sprite>();
				global::System.Collections.Generic.List<global::UnityEngine.U2D.AngleRangeInfo> list3 = new global::System.Collections.Generic.List<global::UnityEngine.U2D.AngleRangeInfo>();
				global::System.Collections.Generic.List<global::UnityEngine.U2D.AngleRange> list4 = new global::System.Collections.Generic.List<global::UnityEngine.U2D.AngleRange>(spriteShape.angleRanges);
				list4.Sort((global::UnityEngine.U2D.AngleRange a, global::UnityEngine.U2D.AngleRange b) => a.order.CompareTo(b.order));
				for (int num = 0; num < list4.Count; num++)
				{
					bool flag = false;
					global::UnityEngine.U2D.AngleRange angleRange = list4[num];
					foreach (global::UnityEngine.Sprite sprite in angleRange.sprites)
					{
						if (sprite != null)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						continue;
					}
					global::UnityEngine.U2D.AngleRangeInfo item = new global::UnityEngine.U2D.AngleRangeInfo
					{
						start = angleRange.start,
						end = angleRange.end,
						order = (uint)angleRange.order
					};
					global::System.Collections.Generic.List<int> list5 = new global::System.Collections.Generic.List<int>();
					foreach (global::UnityEngine.Sprite sprite2 in angleRange.sprites)
					{
						list.Add(sprite2);
						list5.Add(list.Count - 1);
					}
					item.sprites = list5.ToArray();
					list3.Add(item);
				}
				bool flag2 = false;
				foreach (global::UnityEngine.U2D.CornerSprite cornerSprite2 in spriteShape.cornerSprites)
				{
					if (cornerSprite2.sprites[0] != null)
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					for (int num2 = 0; num2 < spriteShape.cornerSprites.Count; num2++)
					{
						global::UnityEngine.U2D.CornerSprite cornerSprite = spriteShape.cornerSprites[num2];
						list2.Add(cornerSprite.sprites[0]);
					}
				}
				m_EdgeSpriteArray = list.ToArray();
				m_CornerSpriteArray = list2.ToArray();
				m_AngleRangeInfoArray = list3.ToArray();
				global::System.Collections.Generic.List<global::UnityEngine.Sprite> list6 = new global::System.Collections.Generic.List<global::UnityEngine.Sprite>();
				list6.AddRange(m_EdgeSpriteArray);
				list6.AddRange(m_CornerSpriteArray);
				m_SpriteArray = list6.ToArray();
			}
			else
			{
				m_SpriteArray = new global::UnityEngine.Sprite[0];
				m_EdgeSpriteArray = new global::UnityEngine.Sprite[0];
				m_CornerSpriteArray = new global::UnityEngine.Sprite[0];
				m_AngleRangeInfoArray = new global::UnityEngine.U2D.AngleRangeInfo[0];
			}
		}

		internal global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint> GetShapeControlPoints()
		{
			int pointCount = spline.GetPointCount();
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint> result = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint>(pointCount, global::Unity.Collections.Allocator.Temp);
			global::UnityEngine.U2D.ShapeControlPoint value = default(global::UnityEngine.U2D.ShapeControlPoint);
			for (int i = 0; i < pointCount; i++)
			{
				value.position = spline.GetPosition(i);
				value.leftTangent = spline.GetLeftTangent(i);
				value.rightTangent = spline.GetRightTangent(i);
				value.mode = (int)spline.GetTangentMode(i);
				result[i] = value;
			}
			return result;
		}

		internal global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SplinePointMetaData> GetSplinePointMetaData()
		{
			int pointCount = spline.GetPointCount();
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SplinePointMetaData> result = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SplinePointMetaData>(pointCount, global::Unity.Collections.Allocator.Temp);
			global::UnityEngine.U2D.SplinePointMetaData value = default(global::UnityEngine.U2D.SplinePointMetaData);
			for (int i = 0; i < pointCount; i++)
			{
				value.height = m_Spline.GetHeight(i);
				value.spriteIndex = (uint)m_Spline.GetSpriteIndex(i);
				value.cornerMode = (int)m_Spline.GetCornerMode(i);
				result[i] = value;
			}
			return result;
		}

		internal int CalculateMaxArrayCount(global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint> shapePoints)
		{
			int y = 65536;
			bool flag = false;
			float num = 99999f;
			if (spriteArray != null)
			{
				global::UnityEngine.Sprite[] array = m_SpriteArray;
				foreach (global::UnityEngine.Sprite sprite in array)
				{
					if (sprite != null)
					{
						flag = true;
						float spritePixelWidth = global::UnityEngine.U2D.BezierUtility.GetSpritePixelWidth(sprite);
						num = ((num > spritePixelWidth) ? spritePixelWidth : num);
					}
				}
			}
			float smallestSegment = num;
			float num2 = global::UnityEngine.U2D.BezierUtility.BezierLength(shapePoints, splineDetail, ref smallestSegment) * 4f;
			int num3 = shapePoints.Length * 5 * splineDetail;
			int num4 = (flag ? ((int)(num2 / smallestSegment) * splineDetail + num3) : 0);
			num3 = (optimizeGeometry ? num3 : (num3 * 2));
			num3 = (ValidateSpriteShapeTexture() ? num3 : 0);
			maxArrayCount = num3 + num4;
			maxArrayCount = global::Unity.Mathematics.math.min(maxArrayCount, y);
			return maxArrayCount;
		}

		private global::Unity.Jobs.JobHandle ScheduleBake()
		{
			global::Unity.Jobs.JobHandle result = default(global::Unity.Jobs.JobHandle);
			_ = global::UnityEngine.Application.isPlaying;
			if (true && geometryCached && (bool)spriteShapeGeometryCache && spriteShapeGeometryCache.maxArrayCount != 0)
			{
				return spriteShapeGeometryCache.Upload(spriteShapeRenderer, this);
			}
			maxArrayCount = spriteShapeCreator.GetVertexArrayCount(this);
			if (maxArrayCount > 0 && base.enabled)
			{
				m_JobHandle.Complete();
				if (m_ColliderData.IsCreated)
				{
					m_ColliderData.Dispose();
				}
				m_ColliderData = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(maxArrayCount, global::Unity.Collections.Allocator.Persistent);
				if (m_ShadowData.IsCreated)
				{
					m_ShadowData.Dispose();
				}
				m_ShadowData = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(maxArrayCount, global::Unity.Collections.Allocator.Persistent);
				if (!m_TangentData.IsCreated)
				{
					m_TangentData = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector4>(1, global::Unity.Collections.Allocator.Persistent);
				}
				global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SpriteShapeSegment> segments = spriteShapeRenderer.GetSegments(spline.GetPointCount() * 8);
				global::Unity.Collections.NativeSlice<global::UnityEngine.Vector4> tangents = new global::Unity.Collections.NativeSlice<global::UnityEngine.Vector4>(m_TangentData);
				global::Unity.Collections.NativeArray<ushort> indices;
				global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> vertices;
				global::Unity.Collections.NativeSlice<global::UnityEngine.Vector2> texcoords;
				if (m_EnableTangents)
				{
					spriteShapeRenderer.GetChannels(maxArrayCount, out indices, out vertices, out texcoords, out tangents);
				}
				else
				{
					spriteShapeRenderer.GetChannels(maxArrayCount, out indices, out vertices, out texcoords);
				}
				result = (m_JobHandle = spriteShapeCreator.MakeCreatorJob(this, indices, vertices, texcoords, tangents, segments, m_ColliderData));
				foreach (global::UnityEngine.U2D.SpriteShapeGeometryModifier modifier in m_Modifiers)
				{
					if (null != modifier)
					{
						m_JobHandle = modifier.MakeModifierJob(m_JobHandle, this, indices, vertices, texcoords, tangents, segments, m_ColliderData);
					}
				}
				spriteShapeRenderer.Prepare(m_JobHandle, m_ActiveShapeParameters, m_SpriteArray);
				result = m_JobHandle;
				m_TessellationNeedsFallback = false;
				global::Unity.Jobs.JobHandle.ScheduleBatchedJobs();
			}
			if (m_DynamicOcclusionOverriden)
			{
				spriteShapeRenderer.allowOcclusionWhenDynamic = m_DynamicOcclusionLocal;
				m_DynamicOcclusionOverriden = false;
			}
			return result;
		}

		internal void BakeShadow()
		{
			if (!m_ShadowData.IsCreated)
			{
				return;
			}
			if (updateShadow)
			{
				int num = 32766;
				global::Unity.Mathematics.float2 x = 0;
				m_ShadowSegment.Clear();
				for (int i = 0; i < num; i++)
				{
					global::Unity.Mathematics.float2 x2 = m_ShadowData[i];
					if (!global::Unity.Mathematics.math.any(x) && !global::Unity.Mathematics.math.any(x2))
					{
						if (i + 1 >= num)
						{
							break;
						}
						global::Unity.Mathematics.float2 x3 = m_ShadowData[i + 1];
						if (!global::Unity.Mathematics.math.any(x3) && !global::Unity.Mathematics.math.any(x3))
						{
							break;
						}
					}
					m_ShadowSegment.Add(new global::UnityEngine.Vector2(x2.x, x2.y));
				}
			}
			m_ShadowData.Dispose();
		}

		public void BakeCollider()
		{
			m_JobHandle.Complete();
			BakeShadow();
			if (!m_ColliderData.IsCreated)
			{
				return;
			}
			if (autoUpdateCollider && hasCollider)
			{
				int num = 32766;
				global::Unity.Mathematics.float2 x = 0;
				m_ColliderSegment.Clear();
				for (int i = 0; i < num; i++)
				{
					global::Unity.Mathematics.float2 x2 = m_ColliderData[i];
					if (!global::Unity.Mathematics.math.any(x) && !global::Unity.Mathematics.math.any(x2))
					{
						if (i + 1 >= num)
						{
							break;
						}
						global::Unity.Mathematics.float2 x3 = m_ColliderData[i + 1];
						if (!global::Unity.Mathematics.math.any(x3) && !global::Unity.Mathematics.math.any(x3))
						{
							break;
						}
					}
					m_ColliderSegment.Add(new global::UnityEngine.Vector2(x2.x, x2.y));
				}
				if (autoUpdateCollider)
				{
					if (edgeCollider != null)
					{
						edgeCollider.points = m_ColliderSegment.ToArray();
					}
					if (polygonCollider != null)
					{
						polygonCollider.points = m_ColliderSegment.ToArray();
					}
				}
			}
			m_ColliderData.Dispose();
			if (!m_Statistics.IsCreated)
			{
				return;
			}
			switch (m_Statistics[0].status)
			{
			case global::UnityEngine.U2D.SpriteShapeGeneratorResult.ErrorNativeDataOverflow:
				global::UnityEngine.Debug.LogWarningFormat(base.gameObject, "NativeArray access not within range. Please submit a bug report.");
				break;
			case global::UnityEngine.U2D.SpriteShapeGeneratorResult.ErrorSpritesTightPacked:
				global::UnityEngine.Debug.LogWarningFormat(base.gameObject, "Sprites used in SpriteShape profile must use FullRect.");
				break;
			case global::UnityEngine.U2D.SpriteShapeGeneratorResult.ErrorSpritesWrongBorder:
				global::UnityEngine.Debug.LogWarningFormat(base.gameObject, "Sprites used in SpriteShape profile have invalid borders. Please check SpriteShape profile.");
				break;
			case global::UnityEngine.U2D.SpriteShapeGeneratorResult.ErrorVertexLimitReached:
				global::UnityEngine.Debug.LogWarningFormat(base.gameObject, "Mesh data has reached Limits. Please try dividing shape into smaller blocks.");
				break;
			case global::UnityEngine.U2D.SpriteShapeGeneratorResult.ErrorDefaultQuadCreated:
				if (m_UTess2D)
				{
					m_TessellationNeedsFallback = true;
					global::UnityEngine.Debug.LogWarningFormat(base.gameObject, "Fill tessellation (C# Job) encountered errors. Please avoid overlaps or close points in the input Spline. Falling back to default generator.");
				}
				else
				{
					global::UnityEngine.Debug.LogWarningFormat(base.gameObject, "Fill tessellation encountered errors. Please avoid overlaps or close points in the input spline.");
				}
				break;
			}
		}

		internal void BakeMeshForced()
		{
			if (spriteShapeRenderer != null && HasSplineDataChanged())
			{
				BakeMesh();
				global::UnityEngine.Rendering.CommandBuffer commandBuffer = new global::UnityEngine.Rendering.CommandBuffer();
				commandBuffer.GetTemporaryRT(0, 256, 256, 0);
				commandBuffer.SetRenderTarget(0);
				commandBuffer.DrawRenderer(spriteShapeRenderer, spriteShapeRenderer.sharedMaterial);
				commandBuffer.ReleaseTemporaryRT(0);
				global::UnityEngine.Graphics.ExecuteCommandBuffer(commandBuffer);
			}
		}

		internal void ForceShadowShapeUpdate(bool forceUpdate)
		{
			m_UpdateShadow = forceUpdate;
		}

		internal global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> GetShadowShapeData()
		{
			if (m_ShadowData.IsCreated)
			{
				BakeMesh().Complete();
				BakeCollider();
			}
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> result = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(m_ShadowSegment.Count, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < result.Length; i++)
			{
				result[i] = m_ShadowSegment[i];
			}
			return result;
		}
	}
}
