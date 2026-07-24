namespace UnityEngine.U2D.Animation
{
	public interface ISpriteLibraryCategory
	{
		string name { get; }

		global::System.Collections.Generic.IEnumerable<global::UnityEngine.U2D.Animation.ISpriteLibraryLabel> labels { get; }
	}
}
