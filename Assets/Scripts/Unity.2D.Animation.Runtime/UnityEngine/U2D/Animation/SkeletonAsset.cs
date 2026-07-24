namespace UnityEngine.U2D.Animation
{
	public class SkeletonAsset : global::UnityEngine.ScriptableObject
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.SpriteBone[] m_SpriteBones;

		public global::UnityEngine.U2D.SpriteBone[] GetSpriteBones()
		{
			return m_SpriteBones;
		}

		public void SetSpriteBones(global::UnityEngine.U2D.SpriteBone[] spriteBones)
		{
			m_SpriteBones = spriteBones;
		}
	}
}
