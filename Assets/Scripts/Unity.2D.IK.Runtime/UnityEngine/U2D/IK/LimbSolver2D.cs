namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[global::UnityEngine.U2D.IK.Solver2DMenu("Limb")]
	public sealed class LimbSolver2D : global::UnityEngine.U2D.IK.Solver2D
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.IK.IKChain2D m_Chain = new global::UnityEngine.U2D.IK.IKChain2D();

		[global::UnityEngine.SerializeField]
		private bool m_Flip;

		private global::UnityEngine.Vector3[] m_Positions = new global::UnityEngine.Vector3[3];

		private float[] m_Lengths = new float[2];

		private float[] m_Angles = new float[2];

		public bool flip
		{
			get
			{
				return m_Flip;
			}
			set
			{
				m_Flip = value;
			}
		}

		protected override void DoInitialize()
		{
			m_Chain.transformCount = ((!(m_Chain.effector == null) && global::UnityEngine.U2D.IK.IKUtility.GetAncestorCount(m_Chain.effector) >= 2) ? 3 : 0);
			base.DoInitialize();
		}

		protected override int GetChainCount()
		{
			return 1;
		}

		public override global::UnityEngine.U2D.IK.IKChain2D GetChain(int index)
		{
			return m_Chain;
		}

		protected override void DoPrepare()
		{
			float[] lengths = m_Chain.lengths;
			m_Positions[0] = m_Chain.transforms[0].position;
			m_Positions[1] = m_Chain.transforms[1].position;
			m_Positions[2] = m_Chain.transforms[2].position;
			m_Lengths[0] = lengths[0];
			m_Lengths[1] = lengths[1];
		}

		protected override void DoUpdateIK(global::System.Collections.Generic.List<global::UnityEngine.Vector3> targetPositions)
		{
			global::UnityEngine.Vector3 position = targetPositions[0];
			global::UnityEngine.Transform transform = m_Chain.transforms[0];
			global::UnityEngine.Transform transform2 = m_Chain.transforms[1];
			global::UnityEngine.Transform effector = m_Chain.effector;
			global::UnityEngine.Vector2 vector = transform.InverseTransformPoint(position);
			position = transform.TransformPoint(vector);
			if (vector.sqrMagnitude > 0f && global::UnityEngine.U2D.IK.Limb.Solve(position, m_Lengths, m_Positions, ref m_Angles))
			{
				float angle = global::UnityEngine.Vector2.SignedAngle(global::UnityEngine.Vector2.right, vector) + global::UnityEngine.Vector2.SignedAngle(transform2.localPosition, global::UnityEngine.Vector2.right) + (flip ? (-1f) : 1f) * m_Angles[0];
				transform.localRotation *= global::UnityEngine.Quaternion.AngleAxis(angle, global::UnityEngine.Vector3.forward);
				float angle2 = global::UnityEngine.Vector2.SignedAngle(global::UnityEngine.Vector2.right, transform2.InverseTransformPoint(position)) + global::UnityEngine.Vector2.SignedAngle(effector.localPosition, global::UnityEngine.Vector2.right);
				m_Chain.transforms[1].localRotation *= global::UnityEngine.Quaternion.AngleAxis(angle2, global::UnityEngine.Vector3.forward);
			}
		}
	}
}
