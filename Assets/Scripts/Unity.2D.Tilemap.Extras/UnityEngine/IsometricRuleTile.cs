namespace UnityEngine
{
	public class IsometricRuleTile<T> : global::UnityEngine.IsometricRuleTile
	{
		public sealed override global::System.Type m_NeighborType => typeof(T);
	}
	[global::System.Serializable]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/RuleTile.html")]
	public class IsometricRuleTile : global::UnityEngine.RuleTile
	{
	}
}
