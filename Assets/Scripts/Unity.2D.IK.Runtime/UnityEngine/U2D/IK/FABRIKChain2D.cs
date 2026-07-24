namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.IK")]
	public struct FABRIKChain2D
	{
		public global::UnityEngine.Vector2 origin;

		public global::UnityEngine.Vector2 target;

		public float sqrTolerance;

		public global::UnityEngine.Vector2[] positions;

		public float[] lengths;

		public int[] subChainIndices;

		public global::UnityEngine.Vector3[] worldPositions;

		public global::UnityEngine.Vector2 first => positions[0];

		public global::UnityEngine.Vector2 last => positions[^1];
	}
}
