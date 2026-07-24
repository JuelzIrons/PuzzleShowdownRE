namespace UnityEngine.Rendering.Universal
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct EdgeDictionary : global::UnityEngine.Rendering.Universal.IEdgeStore
	{
		private class EdgeComparer : global::System.Collections.Generic.IEqualityComparer<global::UnityEngine.Rendering.Universal.ShadowEdge>
		{
			public bool Equals(global::UnityEngine.Rendering.Universal.ShadowEdge edge0, global::UnityEngine.Rendering.Universal.ShadowEdge edge1)
			{
				if (edge0.v0 != edge1.v0 || edge0.v1 != edge1.v1)
				{
					if (edge0.v1 == edge1.v0)
					{
						return edge0.v0 == edge1.v1;
					}
					return false;
				}
				return true;
			}

			public int GetHashCode(global::UnityEngine.Rendering.Universal.ShadowEdge edge)
			{
				int num = edge.v0;
				int num2 = edge.v1;
				if (edge.v1 < edge.v0)
				{
					num = edge.v1;
					num2 = edge.v0;
				}
				return ((num << 15) | num2).GetHashCode();
			}
		}

		private static global::System.Collections.Generic.Dictionary<global::UnityEngine.Rendering.Universal.ShadowEdge, int> m_EdgeDictionary = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Rendering.Universal.ShadowEdge, int>(new global::UnityEngine.Rendering.Universal.EdgeDictionary.EdgeComparer());

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> GetOutsideEdges(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, global::Unity.Collections.NativeArray<int> indices)
		{
			m_EdgeDictionary.Clear();
			m_EdgeDictionary.EnsureCapacity(indices.Length);
			for (int i = 0; i < indices.Length; i += 3)
			{
				int num = indices[i];
				int num2 = indices[i + 1];
				int num3 = indices[i + 2];
				global::UnityEngine.Rendering.Universal.ShadowEdge key = new global::UnityEngine.Rendering.Universal.ShadowEdge(num, num2);
				global::UnityEngine.Rendering.Universal.ShadowEdge key2 = new global::UnityEngine.Rendering.Universal.ShadowEdge(num2, num3);
				global::UnityEngine.Rendering.Universal.ShadowEdge key3 = new global::UnityEngine.Rendering.Universal.ShadowEdge(num3, num);
				if (m_EdgeDictionary.ContainsKey(key))
				{
					m_EdgeDictionary[key] += 1;
				}
				else
				{
					m_EdgeDictionary.Add(key, 1);
				}
				if (m_EdgeDictionary.ContainsKey(key2))
				{
					m_EdgeDictionary[key2] += 1;
				}
				else
				{
					m_EdgeDictionary.Add(key2, 1);
				}
				if (m_EdgeDictionary.ContainsKey(key3))
				{
					m_EdgeDictionary[key3] += 1;
				}
				else
				{
					m_EdgeDictionary.Add(key3, 1);
				}
			}
			int num4 = 0;
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Rendering.Universal.ShadowEdge, int> item in m_EdgeDictionary)
			{
				if (item.Value == 1)
				{
					num4++;
				}
			}
			int num5 = 0;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> result = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>(num4, global::Unity.Collections.Allocator.Temp);
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Rendering.Universal.ShadowEdge, int> item2 in m_EdgeDictionary)
			{
				if (item2.Value == 1)
				{
					result[num5++] = item2.Key;
				}
			}
			return result;
		}
	}
}
