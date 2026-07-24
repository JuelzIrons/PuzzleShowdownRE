namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[global::UnityEngine.U2D.IK.Solver2DMenu("Chain (FABRIK)")]
	[global::Unity.Burst.BurstCompile]
	public sealed class FabrikSolver2D : global::UnityEngine.U2D.IK.Solver2D, global::UnityEngine.U2D.IK.ISolverCleanup
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate bool Solve_0000003E_0024PostfixBurstDelegate(in global::Unity.Mathematics.float2 targetPosition, in global::Unity.Mathematics.float4x4 rootLocalToWorldMatrix, int iterations, float tolerance, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> worldPositions);

		internal static class Solve_0000003E_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.FabrikSolver2D.Solve_0000003E_0024PostfixBurstDelegate>(Solve).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static bool Invoke(in global::Unity.Mathematics.float2 targetPosition, in global::Unity.Mathematics.float4x4 rootLocalToWorldMatrix, int iterations, float tolerance, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> worldPositions)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float2, ref global::Unity.Mathematics.float4x4, int, float, ref global::Unity.Collections.NativeArray<float>, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3>, bool>)functionPointer)(ref targetPosition, ref rootLocalToWorldMatrix, iterations, tolerance, ref lengths, ref positions, ref worldPositions);
					}
				}
				return Solve_0024BurstManaged(in targetPosition, in rootLocalToWorldMatrix, iterations, tolerance, in lengths, ref positions, ref worldPositions);
			}
		}

		private const float k_MinTolerance = 0.001f;

		private const int k_MinIterations = 1;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.IK.IKChain2D m_Chain = new global::UnityEngine.U2D.IK.IKChain2D();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(1f, 50f)]
		private int m_Iterations = 10;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0.001f, 0.1f)]
		private float m_Tolerance = 0.01f;

		private global::Unity.Collections.NativeArray<float> m_Lengths;

		private global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> m_Positions;

		private global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> m_WorldPositions;

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
			if (!m_Lengths.IsCreated)
			{
				m_Lengths = new global::Unity.Collections.NativeArray<float>(transformCount - 1, global::Unity.Collections.Allocator.Persistent);
			}
			else if (m_Lengths.Length != transformCount - 1)
			{
				global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeIfNeeded(ref m_Lengths, transformCount - 1);
			}
			if (!m_WorldPositions.IsCreated)
			{
				m_WorldPositions = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3>(transformCount, global::Unity.Collections.Allocator.Persistent);
			}
			else if (m_WorldPositions.Length != transformCount)
			{
				global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeIfNeeded(ref m_WorldPositions, transformCount);
			}
			return true;
		}

		protected override void DoPrepare()
		{
			int transformCount = m_Chain.transformCount;
			ref global::UnityEngine.Plane plane = ref GetPlane();
			global::System.Span<global::UnityEngine.Vector3> positions = stackalloc global::UnityEngine.Vector3[transformCount];
			for (int i = 0; i < transformCount; i++)
			{
				positions[i] = plane.ClosestPointOnPlane(m_Chain.transforms[i].position);
			}
			GetPlaneRootTransform().InverseTransformPoints(positions);
			for (int j = 0; j < transformCount; j++)
			{
				m_Positions[j] = (global::UnityEngine.Vector2)positions[j];
			}
			for (int k = 0; k < transformCount - 1; k++)
			{
				m_Lengths[k] = global::Unity.Mathematics.math.length(m_Positions[k + 1] - m_Positions[k]);
			}
		}

		protected override void DoUpdateIK(global::System.Collections.Generic.List<global::UnityEngine.Vector3> targetPositions)
		{
			if (Solve((global::Unity.Mathematics.float2)(global::UnityEngine.Vector2)GetPointOnSolverPlane(targetPositions[0]), (global::Unity.Mathematics.float4x4)m_Chain.rootTransform.localToWorldMatrix, m_Iterations, m_Tolerance, in m_Lengths, ref m_Positions, ref m_WorldPositions))
			{
				for (int i = 0; i < m_Chain.transformCount - 1; i++)
				{
					global::UnityEngine.Vector2 vector = m_Chain.transforms[i + 1].localPosition;
					global::UnityEngine.Vector2 to = m_Chain.transforms[i].InverseTransformPoint(m_WorldPositions[i + 1]);
					m_Chain.transforms[i].localRotation *= global::UnityEngine.Quaternion.AngleAxis(global::UnityEngine.Vector2.SignedAngle(vector, to), global::UnityEngine.Vector3.forward);
				}
			}
		}

		void global::UnityEngine.U2D.IK.ISolverCleanup.DoCleanUp()
		{
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.DisposeIfCreated(m_Positions);
			m_Positions = default(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.DisposeIfCreated(m_Lengths);
			m_Lengths = default(global::Unity.Collections.NativeArray<float>);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.DisposeIfCreated(m_WorldPositions);
			m_WorldPositions = default(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3>);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002ESolve_0000003E_0024PostfixBurstDelegate))]
		private static bool Solve(in global::Unity.Mathematics.float2 targetPosition, in global::Unity.Mathematics.float4x4 rootLocalToWorldMatrix, int iterations, float tolerance, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> worldPositions)
		{
			return global::UnityEngine.U2D.IK.FabrikSolver2D.Solve_0000003E_0024BurstDirectCall.Invoke(in targetPosition, in rootLocalToWorldMatrix, iterations, tolerance, in lengths, ref positions, ref worldPositions);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static bool Solve_0024BurstManaged(in global::Unity.Mathematics.float2 targetPosition, in global::Unity.Mathematics.float4x4 rootLocalToWorldMatrix, int iterations, float tolerance, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> worldPositions)
		{
			bool flag = global::UnityEngine.U2D.IK.FABRIK2D.Solve(in targetPosition, iterations, tolerance, in lengths, ref positions);
			if (flag)
			{
				for (int i = 0; i < positions.Length; i++)
				{
					worldPositions[i] = global::Unity.Mathematics.math.transform(rootLocalToWorldMatrix, new global::Unity.Mathematics.float3(positions[i], 0f));
				}
			}
			return flag;
		}
	}
}
