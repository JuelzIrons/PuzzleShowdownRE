namespace UnityEngine.UI
{
	public interface IMeshModifier
	{
		[global::System.Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts) instead", false)]
		void ModifyMesh(global::UnityEngine.Mesh mesh);

		void ModifyMesh(global::UnityEngine.UI.VertexHelper verts);
	}
}
