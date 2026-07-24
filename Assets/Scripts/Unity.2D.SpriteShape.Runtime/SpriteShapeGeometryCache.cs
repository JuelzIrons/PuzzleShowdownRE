[global::UnityEngine.AddComponentMenu("")]
internal class SpriteShapeGeometryCache : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	[global::UnityEngine.HideInInspector]
	private int m_MaxArrayCount;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.HideInInspector]
	private global::UnityEngine.Vector3[] m_PosArray;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.HideInInspector]
	private global::UnityEngine.Vector2[] m_Uv0Array;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.HideInInspector]
	private global::UnityEngine.Vector4[] m_TanArray;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.HideInInspector]
	private ushort[] m_IndexArray;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.HideInInspector]
	private SpriteShapeGeometryInfo[] m_GeomArray;

	private bool m_RequiresUpdate;

	private bool m_RequiresUpload;

	private global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> m_PosArrayCache;

	private global::Unity.Collections.NativeSlice<global::UnityEngine.Vector2> m_Uv0ArrayCache;

	private global::Unity.Collections.NativeSlice<global::UnityEngine.Vector4> m_TanArrayCache;

	private global::Unity.Collections.NativeArray<ushort> m_IndexArrayCache;

	private global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SpriteShapeSegment> m_GeomArrayCache;

	internal ushort[] indexArray => m_IndexArray;

	internal global::UnityEngine.Vector3[] posArray => m_PosArray;

	public global::UnityEngine.Vector4[] tanArray => m_TanArray;

	internal int maxArrayCount => m_MaxArrayCount;

	internal bool requiresUpdate => m_RequiresUpdate;

	internal bool requiresUpload => m_RequiresUpload;

	private void OnEnable()
	{
		m_RequiresUpload = true;
		m_RequiresUpdate = false;
	}

	internal void SetGeometryCache(int _maxArrayCount, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> _posArray, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector2> _uv0Array, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector4> _tanArray, global::Unity.Collections.NativeArray<ushort> _indexArray, global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SpriteShapeSegment> _geomArray)
	{
		m_RequiresUpdate = true;
		m_PosArrayCache = _posArray;
		m_Uv0ArrayCache = _uv0Array;
		m_TanArrayCache = _tanArray;
		m_GeomArrayCache = _geomArray;
		m_IndexArrayCache = _indexArray;
		m_MaxArrayCount = _maxArrayCount;
	}

	internal void UpdateGeometryCache()
	{
		if (!m_RequiresUpdate || !m_GeomArrayCache.IsCreated || !m_IndexArrayCache.IsCreated)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < m_GeomArrayCache.Length; i++)
		{
			global::UnityEngine.U2D.SpriteShapeSegment spriteShapeSegment = m_GeomArrayCache[i];
			num2 += spriteShapeSegment.indexCount;
			num3 += spriteShapeSegment.vertexCount;
			if (spriteShapeSegment.vertexCount > 0)
			{
				num = i + 1;
			}
		}
		m_GeomArray = new SpriteShapeGeometryInfo[num];
		global::Unity.Collections.NativeArray<SpriteShapeGeometryInfo> nativeArray = m_GeomArrayCache.Reinterpret<SpriteShapeGeometryInfo>();
		global::UnityEngine.U2D.SpriteShapeCopyUtility<SpriteShapeGeometryInfo>.Copy(m_GeomArray, nativeArray, num);
		m_PosArray = new global::UnityEngine.Vector3[num3];
		m_Uv0Array = new global::UnityEngine.Vector2[num3];
		m_IndexArray = new ushort[num2];
		global::UnityEngine.U2D.SpriteShapeCopyUtility<ushort>.Copy(m_IndexArray, m_IndexArrayCache, num2);
		global::UnityEngine.U2D.SpriteShapeCopyUtility<global::UnityEngine.Vector3>.Copy(m_PosArray, m_PosArrayCache, num3);
		global::UnityEngine.U2D.SpriteShapeCopyUtility<global::UnityEngine.Vector2>.Copy(m_Uv0Array, m_Uv0ArrayCache, num3);
		m_TanArray = new global::UnityEngine.Vector4[(m_TanArrayCache.Length < num3) ? 1 : num3];
		if (m_TanArrayCache.Length >= num3)
		{
			global::UnityEngine.U2D.SpriteShapeCopyUtility<global::UnityEngine.Vector4>.Copy(m_TanArray, m_TanArrayCache, num3);
		}
		m_MaxArrayCount = ((num3 > num2) ? num3 : num2);
		m_RequiresUpdate = false;
	}

	internal global::Unity.Jobs.JobHandle Upload(global::UnityEngine.U2D.SpriteShapeRenderer sr, global::UnityEngine.U2D.SpriteShapeController sc)
	{
		global::Unity.Jobs.JobHandle jobHandle = default(global::Unity.Jobs.JobHandle);
		if (m_RequiresUpload)
		{
			sr.GetSegments(m_GeomArray.Length).Reinterpret<SpriteShapeGeometryInfo>().CopyFrom(m_GeomArray);
			global::Unity.Collections.NativeArray<ushort> indices;
			global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> vertices;
			global::Unity.Collections.NativeSlice<global::UnityEngine.Vector2> texcoords;
			if (sc.enableTangents && m_TanArray.Length > 1)
			{
				sr.GetChannels(m_MaxArrayCount, out indices, out vertices, out texcoords, out global::Unity.Collections.NativeSlice<global::UnityEngine.Vector4> tangents);
				global::UnityEngine.U2D.SpriteShapeCopyUtility<global::UnityEngine.Vector4>.Copy(tangents, m_TanArray, m_TanArray.Length);
			}
			else
			{
				sr.GetChannels(m_MaxArrayCount, out indices, out vertices, out texcoords);
			}
			global::UnityEngine.U2D.SpriteShapeCopyUtility<global::UnityEngine.Vector3>.Copy(vertices, m_PosArray, m_PosArray.Length);
			global::UnityEngine.U2D.SpriteShapeCopyUtility<global::UnityEngine.Vector2>.Copy(texcoords, m_Uv0Array, m_Uv0Array.Length);
			global::UnityEngine.U2D.SpriteShapeCopyUtility<ushort>.Copy(indices, m_IndexArray, m_IndexArray.Length);
			sr.Prepare(jobHandle, sc.spriteShapeParameters, sc.spriteArray);
			m_RequiresUpload = false;
		}
		return jobHandle;
	}
}
