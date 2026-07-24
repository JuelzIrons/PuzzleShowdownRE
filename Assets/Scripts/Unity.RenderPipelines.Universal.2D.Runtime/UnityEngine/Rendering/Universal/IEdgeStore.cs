namespace UnityEngine.Rendering.Universal
{
	internal interface IEdgeStore
	{
		global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> GetOutsideEdges(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, global::Unity.Collections.NativeArray<int> indices);
	}
}
