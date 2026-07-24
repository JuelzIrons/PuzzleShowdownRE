namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Effects/Position As UV1", 82)]
	public class PositionAsUV1 : global::UnityEngine.UI.BaseMeshEffect
	{
		protected PositionAsUV1()
		{
		}

		public override void ModifyMesh(global::UnityEngine.UI.VertexHelper vh)
		{
			global::UnityEngine.UIVertex vertex = default(global::UnityEngine.UIVertex);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				vertex.uv1 = new global::UnityEngine.Vector2(vertex.position.x, vertex.position.y);
				vh.SetUIVertex(vertex, i);
			}
		}
	}
}
