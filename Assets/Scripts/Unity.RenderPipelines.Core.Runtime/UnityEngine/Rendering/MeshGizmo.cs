namespace UnityEngine.Rendering
{
	internal class MeshGizmo : global::System.IDisposable
	{
		public static readonly int vertexCountPerCube = 24;

		public global::UnityEngine.Mesh mesh;

		private global::System.Collections.Generic.List<global::UnityEngine.Vector3> vertices;

		private global::System.Collections.Generic.List<int> indices;

		private global::System.Collections.Generic.List<global::UnityEngine.Color> colors;

		private global::UnityEngine.Material wireMaterial;

		private global::UnityEngine.Material dottedWireMaterial;

		private global::UnityEngine.Material solidMaterial;

		public MeshGizmo(int capacity = 0)
		{
			vertices = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>(capacity);
			indices = new global::System.Collections.Generic.List<int>(capacity);
			colors = new global::System.Collections.Generic.List<global::UnityEngine.Color>(capacity);
			mesh = new global::UnityEngine.Mesh
			{
				indexFormat = global::UnityEngine.Rendering.IndexFormat.UInt32,
				hideFlags = global::UnityEngine.HideFlags.HideAndDontSave
			};
		}

		public void Clear()
		{
			vertices.Clear();
			indices.Clear();
			colors.Clear();
		}

		public void AddWireCube(global::UnityEngine.Vector3 center, global::UnityEngine.Vector3 size, global::UnityEngine.Color color)
		{
			global::UnityEngine.Vector3 vector = size / 2f;
			global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(vector.x, vector.y, vector.z);
			global::UnityEngine.Vector3 vector3 = new global::UnityEngine.Vector3(0f - vector.x, vector.y, vector.z);
			global::UnityEngine.Vector3 vector4 = new global::UnityEngine.Vector3(0f - vector.x, 0f - vector.y, vector.z);
			global::UnityEngine.Vector3 vector5 = new global::UnityEngine.Vector3(vector.x, 0f - vector.y, vector.z);
			global::UnityEngine.Vector3 vector6 = new global::UnityEngine.Vector3(vector.x, vector.y, 0f - vector.z);
			global::UnityEngine.Vector3 vector7 = new global::UnityEngine.Vector3(0f - vector.x, vector.y, 0f - vector.z);
			global::UnityEngine.Vector3 vector8 = new global::UnityEngine.Vector3(0f - vector.x, 0f - vector.y, 0f - vector.z);
			global::UnityEngine.Vector3 vector9 = new global::UnityEngine.Vector3(vector.x, 0f - vector.y, 0f - vector.z);
			AddEdge(center + vector2, center + vector3);
			AddEdge(center + vector3, center + vector4);
			AddEdge(center + vector4, center + vector5);
			AddEdge(center + vector5, center + vector2);
			AddEdge(center + vector6, center + vector7);
			AddEdge(center + vector7, center + vector8);
			AddEdge(center + vector8, center + vector9);
			AddEdge(center + vector9, center + vector6);
			AddEdge(center + vector2, center + vector6);
			AddEdge(center + vector3, center + vector7);
			AddEdge(center + vector4, center + vector8);
			AddEdge(center + vector5, center + vector9);
			void AddEdge(global::UnityEngine.Vector3 p1, global::UnityEngine.Vector3 p2)
			{
				vertices.Add(p1);
				vertices.Add(p2);
				indices.Add(indices.Count);
				indices.Add(indices.Count);
				colors.Add(color);
				colors.Add(color);
			}
		}

		private void DrawMesh(global::UnityEngine.Matrix4x4 trs, global::UnityEngine.Material mat, global::UnityEngine.MeshTopology topology, global::UnityEngine.Rendering.CompareFunction depthTest, string gizmoName)
		{
			mesh.Clear();
			mesh.SetVertices(vertices);
			mesh.SetColors(colors);
			mesh.SetIndices(indices, topology, 0);
			mat.SetFloat("_HandleZTest", (float)depthTest);
			global::UnityEngine.Rendering.CommandBuffer commandBuffer = global::UnityEngine.Rendering.CommandBufferPool.Get(gizmoName ?? "Mesh Gizmo Rendering");
			commandBuffer.DrawMesh(mesh, trs, mat, 0, 0);
			global::UnityEngine.Graphics.ExecuteCommandBuffer(commandBuffer);
		}

		public void RenderWireframe(global::UnityEngine.Matrix4x4 trs, global::UnityEngine.Rendering.CompareFunction depthTest = global::UnityEngine.Rendering.CompareFunction.LessEqual, string gizmoName = null)
		{
			DrawMesh(trs, wireMaterial, global::UnityEngine.MeshTopology.Lines, depthTest, gizmoName);
		}

		public void Dispose()
		{
			global::UnityEngine.Rendering.CoreUtils.Destroy(mesh);
		}
	}
}
