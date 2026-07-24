namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[global::UnityEngine.U2D.IK.Solver2DMenu("Chain (CCD)")]
	[global::Unity.Burst.BurstCompile]
	public sealed class CCDSolver2D : global::UnityEngine.U2D.IK.Solver2D, global::UnityEngine.U2D.IK.ISolverCleanup
	{
		private const int k_MinIterations = 1;

		private const float k_MinTolerance = 0.001f;

		private const float k_MinVelocity = 0.01f;

		private const float k_MaxVelocity = 1f;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.IK.IKChain2D m_Chain = new global::UnityEngine.U2D.IK.IKChain2D();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(1f, 50f)]
		private int m_Iterations = 10;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0.001f, 0.1f)]
		private float m_Tolerance = 0.01f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0f, 1f)]
		private float m_Velocity = 0.5f;

		private float m_InterpolatedVelocity = global::UnityEngine.Mathf.Lerp(0.01f, 1f, 0.5f);

		private global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> m_Positions;

		public int iterations
		{
			get
			{
				return m_Iterations;
			}
			set
			{
				m_Iterations = global::UnityEngine.Mathf.Max(value, 1);
			}
		}

		public float tolerance
		{
			get
			{
				return m_Tolerance;
			}
			set
			{
				m_Tolerance = global::UnityEngine.Mathf.Max(value, 0.001f);
			}
		}

		public float velocity
		{
			get
			{
				return m_Velocity;
			}
			set
			{
				m_Velocity = global::UnityEngine.Mathf.Clamp01(value);
				m_InterpolatedVelocity = global::UnityEngine.Mathf.Lerp(0.01f, 1f, m_Velocity);
			}
		}

		protected override int GetChainCount()
		{
			return 1;
		}

		public override global::UnityEngine.U2D.IK.IKChain2D GetChain(int index)
		{
			return m_Chain;
		}

		protected override bool DoValidate()
		{
			int transformCount = m_Chain.transformCount;
			if (!m_Positions.IsCreated)
			{
				m_Positions = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(transformCount, global::Unity.Collections.Allocator.Persistent);
			}
			else if (m_Positions.Length != transformCount)
			{
				global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeIfNeeded(ref m_Positions, transformCount);
			}
			return true;
		}

		protected override void DoPrepare()
		{
			global::UnityEngine.Transform rootTransform = m_Chain.rootTransform;
			int transformCount = m_Chain.transformCount;
			global::System.Span<global::UnityEngine.Vector3> positions = stackalloc global::UnityEngine.Vector3[transformCount];
			for (int i = 0; i < transformCount; i++)
			{
				positions[i] = m_Chain.transforms[i].position;
			}
			rootTransform.InverseTransformPoints(positions);
			for (int j = 0; j < transformCount; j++)
			{
				m_Positions[j] = (global::UnityEngine.Vector2)positions[j];
			}
		}

		protected override void DoUpdateIK(global::System.Collections.Generic.List<global::UnityEngine.Vector3> targetPositions)
		{
			global::UnityEngine.Transform rootTransform = m_Chain.rootTransform;
			int transformCount = m_Chain.transformCount;
			if (global::UnityEngine.U2D.IK.CCD2D.Solve(((global::Unity.Mathematics.float3)rootTransform.InverseTransformPoint(targetPositions[0])).xy, iterations, tolerance, m_InterpolatedVelocity, ref m_Positions))
			{
				global::System.Span<global::UnityEngine.Vector3> positions = stackalloc global::UnityEngine.Vector3[transformCount];
				for (int i = 0; i < transformCount; i++)
				{
					positions[i] = new global::Unity.Mathematics.float3(m_Positions[i], 0f);
				}
				rootTransform.TransformPoints(positions);
				for (int j = 0; j < transformCount - 1; j++)
				{
					global::UnityEngine.Vector2 vector = m_Chain.transforms[j + 1].localPosition;
					global::UnityEngine.Vector2 to = m_Chain.transforms[j].InverseTransformPoint(positions[j + 1]);
					m_Chain.transforms[j].localRotation *= global::UnityEngine.Quaternion.AngleAxis(global::UnityEngine.Vector2.SignedAngle(vector, to), global::UnityEngine.Vector3.forward);
				}
			}
		}

		void global::UnityEngine.U2D.IK.ISolverCleanup.DoCleanUp()
		{
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.DisposeIfCreated(m_Positions);
			m_Positions = default(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>);
		}
	}
}
