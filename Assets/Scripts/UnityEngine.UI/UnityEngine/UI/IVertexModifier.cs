namespace UnityEngine.UI
{
	[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
	[global::System.Obsolete("Use IMeshModifier instead", true)]
	public interface IVertexModifier
	{
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts)  instead", true)]
		void ModifyVertices(global::System.Collections.Generic.List<global::UnityEngine.UIVertex> verts);
	}
}
