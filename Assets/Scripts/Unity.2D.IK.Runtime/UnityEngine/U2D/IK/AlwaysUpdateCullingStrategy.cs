namespace UnityEngine.U2D.IK
{
	internal class AlwaysUpdateCullingStrategy : global::UnityEngine.U2D.IK.BaseCullingStrategy
	{
		public override bool AreBonesVisible(global::System.Collections.Generic.IList<int> transformIds)
		{
			return true;
		}
	}
}
