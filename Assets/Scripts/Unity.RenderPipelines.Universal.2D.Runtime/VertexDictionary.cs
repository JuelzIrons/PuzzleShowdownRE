[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
internal struct VertexDictionary
{
	private static global::System.Collections.Generic.Dictionary<global::UnityEngine.Vector3, int> m_VertexDictionary = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Vector3, int>();

	public global::Unity.Collections.NativeArray<int> GetIndexRemap(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, global::Unity.Collections.NativeArray<int> indices)
	{
		global::Unity.Collections.NativeArray<int> nativeArray = new global::Unity.Collections.NativeArray<int>(vertices.Length, global::Unity.Collections.Allocator.Temp);
		m_VertexDictionary.Clear();
		m_VertexDictionary.EnsureCapacity(vertices.Length);
		for (int i = 0; i < vertices.Length; i++)
		{
			global::UnityEngine.Vector3 key = vertices[i];
			if (!m_VertexDictionary.ContainsKey(key))
			{
				nativeArray[i] = i;
				m_VertexDictionary.Add(key, i);
			}
			else
			{
				nativeArray[i] = m_VertexDictionary[key];
			}
		}
		global::Unity.Collections.NativeArray<int> result = new global::Unity.Collections.NativeArray<int>(indices.Length, global::Unity.Collections.Allocator.Temp);
		for (int j = 0; j < indices.Length; j++)
		{
			result[j] = nativeArray[indices[j]];
		}
		return result;
	}
}
