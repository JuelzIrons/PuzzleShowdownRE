namespace UnityEngine.UI
{
	public class VertexHelper : global::System.IDisposable
	{
		private global::System.Collections.Generic.List<global::UnityEngine.Vector3> m_Positions;

		private global::System.Collections.Generic.List<global::UnityEngine.Color32> m_Colors;

		private global::System.Collections.Generic.List<global::UnityEngine.Vector4> m_Uv0S;

		private global::System.Collections.Generic.List<global::UnityEngine.Vector4> m_Uv1S;

		private global::System.Collections.Generic.List<global::UnityEngine.Vector4> m_Uv2S;

		private global::System.Collections.Generic.List<global::UnityEngine.Vector4> m_Uv3S;

		private global::System.Collections.Generic.List<global::UnityEngine.Vector3> m_Normals;

		private global::System.Collections.Generic.List<global::UnityEngine.Vector4> m_Tangents;

		private global::System.Collections.Generic.List<int> m_Indices;

		private static readonly global::UnityEngine.Vector4 s_DefaultTangent = new global::UnityEngine.Vector4(1f, 0f, 0f, -1f);

		private static readonly global::UnityEngine.Vector3 s_DefaultNormal = global::UnityEngine.Vector3.back;

		private bool m_ListsInitalized;

		public int currentVertCount
		{
			get
			{
				if (m_Positions == null)
				{
					return 0;
				}
				return m_Positions.Count;
			}
		}

		public int currentIndexCount
		{
			get
			{
				if (m_Indices == null)
				{
					return 0;
				}
				return m_Indices.Count;
			}
		}

		public VertexHelper()
		{
		}

		public VertexHelper(global::UnityEngine.Mesh m)
		{
			InitializeListIfRequired();
			m_Positions.AddRange(m.vertices);
			m_Colors.AddRange(m.colors32);
			global::System.Collections.Generic.List<global::UnityEngine.Vector4> list = new global::System.Collections.Generic.List<global::UnityEngine.Vector4>();
			m.GetUVs(0, list);
			m_Uv0S.AddRange(list);
			m.GetUVs(1, list);
			m_Uv1S.AddRange(list);
			m.GetUVs(2, list);
			m_Uv2S.AddRange(list);
			m.GetUVs(3, list);
			m_Uv3S.AddRange(list);
			m_Normals.AddRange(m.normals);
			m_Tangents.AddRange(m.tangents);
			m_Indices.AddRange(m.GetIndices(0));
		}

		private void InitializeListIfRequired()
		{
			if (!m_ListsInitalized)
			{
				m_Positions = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector3>, global::UnityEngine.Vector3>.Get();
				m_Colors = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Color32>, global::UnityEngine.Color32>.Get();
				m_Uv0S = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Get();
				m_Uv1S = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Get();
				m_Uv2S = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Get();
				m_Uv3S = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Get();
				m_Normals = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector3>, global::UnityEngine.Vector3>.Get();
				m_Tangents = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Get();
				m_Indices = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<int>, int>.Get();
				m_ListsInitalized = true;
			}
		}

		public void Dispose()
		{
			if (m_ListsInitalized)
			{
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector3>, global::UnityEngine.Vector3>.Release(m_Positions);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Color32>, global::UnityEngine.Color32>.Release(m_Colors);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Release(m_Uv0S);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Release(m_Uv1S);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Release(m_Uv2S);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Release(m_Uv3S);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector3>, global::UnityEngine.Vector3>.Release(m_Normals);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Vector4>, global::UnityEngine.Vector4>.Release(m_Tangents);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<int>, int>.Release(m_Indices);
				m_Positions = null;
				m_Colors = null;
				m_Uv0S = null;
				m_Uv1S = null;
				m_Uv2S = null;
				m_Uv3S = null;
				m_Normals = null;
				m_Tangents = null;
				m_Indices = null;
				m_ListsInitalized = false;
			}
		}

		public void Clear()
		{
			if (m_ListsInitalized)
			{
				m_Positions.Clear();
				m_Colors.Clear();
				m_Uv0S.Clear();
				m_Uv1S.Clear();
				m_Uv2S.Clear();
				m_Uv3S.Clear();
				m_Normals.Clear();
				m_Tangents.Clear();
				m_Indices.Clear();
			}
		}

		public void PopulateUIVertex(ref global::UnityEngine.UIVertex vertex, int i)
		{
			InitializeListIfRequired();
			vertex.position = m_Positions[i];
			vertex.color = m_Colors[i];
			vertex.uv0 = m_Uv0S[i];
			vertex.uv1 = m_Uv1S[i];
			vertex.uv2 = m_Uv2S[i];
			vertex.uv3 = m_Uv3S[i];
			vertex.normal = m_Normals[i];
			vertex.tangent = m_Tangents[i];
		}

		public void SetUIVertex(global::UnityEngine.UIVertex vertex, int i)
		{
			InitializeListIfRequired();
			m_Positions[i] = vertex.position;
			m_Colors[i] = vertex.color;
			m_Uv0S[i] = vertex.uv0;
			m_Uv1S[i] = vertex.uv1;
			m_Uv2S[i] = vertex.uv2;
			m_Uv3S[i] = vertex.uv3;
			m_Normals[i] = vertex.normal;
			m_Tangents[i] = vertex.tangent;
		}

		public void FillMesh(global::UnityEngine.Mesh mesh)
		{
			InitializeListIfRequired();
			mesh.Clear();
			if (m_Positions.Count >= 65000)
			{
				throw new global::System.ArgumentException("Mesh can not have more than 65000 vertices");
			}
			mesh.SetVertices(m_Positions);
			mesh.SetColors(m_Colors);
			mesh.SetUVs(0, m_Uv0S);
			mesh.SetUVs(1, m_Uv1S);
			mesh.SetUVs(2, m_Uv2S);
			mesh.SetUVs(3, m_Uv3S);
			mesh.SetNormals(m_Normals);
			mesh.SetTangents(m_Tangents);
			mesh.SetTriangles(m_Indices, 0);
			mesh.RecalculateBounds();
		}

		public void AddVert(global::UnityEngine.Vector3 position, global::UnityEngine.Color32 color, global::UnityEngine.Vector4 uv0, global::UnityEngine.Vector4 uv1, global::UnityEngine.Vector4 uv2, global::UnityEngine.Vector4 uv3, global::UnityEngine.Vector3 normal, global::UnityEngine.Vector4 tangent)
		{
			InitializeListIfRequired();
			m_Positions.Add(position);
			m_Colors.Add(color);
			m_Uv0S.Add(uv0);
			m_Uv1S.Add(uv1);
			m_Uv2S.Add(uv2);
			m_Uv3S.Add(uv3);
			m_Normals.Add(normal);
			m_Tangents.Add(tangent);
		}

		public void AddVert(global::UnityEngine.Vector3 position, global::UnityEngine.Color32 color, global::UnityEngine.Vector4 uv0, global::UnityEngine.Vector4 uv1, global::UnityEngine.Vector3 normal, global::UnityEngine.Vector4 tangent)
		{
			AddVert(position, color, uv0, uv1, global::UnityEngine.Vector4.zero, global::UnityEngine.Vector4.zero, normal, tangent);
		}

		public void AddVert(global::UnityEngine.Vector3 position, global::UnityEngine.Color32 color, global::UnityEngine.Vector4 uv0)
		{
			AddVert(position, color, uv0, global::UnityEngine.Vector4.zero, s_DefaultNormal, s_DefaultTangent);
		}

		public void AddVert(global::UnityEngine.UIVertex v)
		{
			AddVert(v.position, v.color, v.uv0, v.uv1, v.uv2, v.uv3, v.normal, v.tangent);
		}

		public void AddTriangle(int idx0, int idx1, int idx2)
		{
			InitializeListIfRequired();
			m_Indices.Add(idx0);
			m_Indices.Add(idx1);
			m_Indices.Add(idx2);
		}

		public void AddUIVertexQuad(global::UnityEngine.UIVertex[] verts)
		{
			int num = currentVertCount;
			for (int i = 0; i < 4; i++)
			{
				AddVert(verts[i].position, verts[i].color, verts[i].uv0, verts[i].uv1, verts[i].normal, verts[i].tangent);
			}
			AddTriangle(num, num + 1, num + 2);
			AddTriangle(num + 2, num + 3, num);
		}

		public void AddUIVertexStream(global::System.Collections.Generic.List<global::UnityEngine.UIVertex> verts, global::System.Collections.Generic.List<int> indices)
		{
			InitializeListIfRequired();
			if (verts != null)
			{
				global::UnityEngine.CanvasRenderer.AddUIVertexStream(verts, m_Positions, m_Colors, m_Uv0S, m_Uv1S, m_Uv2S, m_Uv3S, m_Normals, m_Tangents);
			}
			if (indices != null)
			{
				m_Indices.AddRange(indices);
			}
		}

		public void AddUIVertexTriangleStream(global::System.Collections.Generic.List<global::UnityEngine.UIVertex> verts)
		{
			if (verts != null)
			{
				InitializeListIfRequired();
				global::UnityEngine.CanvasRenderer.SplitUIVertexStreams(verts, m_Positions, m_Colors, m_Uv0S, m_Uv1S, m_Uv2S, m_Uv3S, m_Normals, m_Tangents, m_Indices);
			}
		}

		public void GetUIVertexStream(global::System.Collections.Generic.List<global::UnityEngine.UIVertex> stream)
		{
			if (stream != null)
			{
				InitializeListIfRequired();
				global::UnityEngine.CanvasRenderer.CreateUIVertexStream(stream, m_Positions, m_Colors, m_Uv0S, m_Uv1S, m_Uv2S, m_Uv3S, m_Normals, m_Tangents, m_Indices);
			}
		}
	}
}
