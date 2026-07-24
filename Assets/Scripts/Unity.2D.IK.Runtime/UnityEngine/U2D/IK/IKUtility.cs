namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.IK")]
	public class IKUtility
	{
		public static bool IsDescendentOf(global::UnityEngine.Transform transform, global::UnityEngine.Transform ancestor)
		{
			global::UnityEngine.Transform parent = transform.parent;
			while ((bool)parent)
			{
				if (parent == ancestor)
				{
					return true;
				}
				parent = parent.parent;
			}
			return false;
		}

		public static int GetAncestorCount(global::UnityEngine.Transform transform)
		{
			int num = 0;
			global::UnityEngine.Transform parent = transform.parent;
			while ((bool)parent && parent.GetComponent<global::UnityEngine.U2D.IK.IKManager2D>() == null)
			{
				num++;
				parent = parent.parent;
			}
			return num;
		}

		public static int GetMaxChainCount(global::UnityEngine.U2D.IK.IKChain2D chain)
		{
			int result = 0;
			if ((bool)chain.effector)
			{
				result = GetAncestorCount(chain.effector) + 1;
			}
			return result;
		}
	}
}
