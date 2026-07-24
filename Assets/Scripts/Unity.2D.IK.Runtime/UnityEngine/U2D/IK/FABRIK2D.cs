namespace UnityEngine.U2D.IK
{
	[global::Unity.Burst.BurstCompile]
	public static class FABRIK2D
	{
		private static class Profiling
		{
			internal static readonly global::Unity.Profiling.ProfilerMarker Solve = new global::Unity.Profiling.ProfilerMarker("FABRIK2D.Solve");
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate bool Solve_000000A5_0024PostfixBurstDelegate(in global::Unity.Mathematics.float2 targetPosition, int solverLimit, float tolerance, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions);

		internal static class Solve_000000A5_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.FABRIK2D.Solve_000000A5_0024PostfixBurstDelegate>(Solve).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static bool Invoke(in global::Unity.Mathematics.float2 targetPosition, int solverLimit, float tolerance, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float2, int, float, ref global::Unity.Collections.NativeArray<float>, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>, bool>)functionPointer)(ref targetPosition, solverLimit, tolerance, ref lengths, ref positions);
					}
				}
				return Solve_0024BurstManaged(in targetPosition, solverLimit, tolerance, in lengths, ref positions);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void Forward_000000AB_0024PostfixBurstDelegate(in global::Unity.Mathematics.float2 targetPosition, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions);

		internal static class Forward_000000AB_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.FABRIK2D.Forward_000000AB_0024PostfixBurstDelegate>(Forward).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Mathematics.float2 targetPosition, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float2, ref global::Unity.Collections.NativeArray<float>, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>, void>)functionPointer)(ref targetPosition, ref lengths, ref positions);
						return;
					}
				}
				Forward_0024BurstManaged(in targetPosition, in lengths, ref positions);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void Backward_000000AD_0024PostfixBurstDelegate(in global::Unity.Mathematics.float2 originPosition, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions);

		internal static class Backward_000000AD_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.FABRIK2D.Backward_000000AD_0024PostfixBurstDelegate>(Backward).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Mathematics.float2 originPosition, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float2, ref global::Unity.Collections.NativeArray<float>, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>, void>)functionPointer)(ref originPosition, ref lengths, ref positions);
						return;
					}
				}
				Backward_0024BurstManaged(in originPosition, in lengths, ref positions);
			}
		}

		public static bool Solve(global::UnityEngine.Vector2 targetPosition, int solverLimit, float tolerance, float[] lengths, ref global::UnityEngine.Vector2[] positions)
		{
			global::Unity.Collections.NativeArray<float> lengths2 = new global::Unity.Collections.NativeArray<float>(lengths.Length, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < lengths.Length; i++)
			{
				lengths2[i] = lengths[i];
			}
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions2 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(positions.Length, global::Unity.Collections.Allocator.Temp);
			for (int j = 0; j < positions.Length; j++)
			{
				positions2[j] = new global::Unity.Mathematics.float2(positions[j].x, positions[j].y);
			}
			bool result = Solve((global::Unity.Mathematics.float2)targetPosition, solverLimit, tolerance, in lengths2, ref positions2);
			for (int k = 0; k < positions.Length; k++)
			{
				positions[k] = positions2[k];
			}
			lengths2.Dispose();
			positions2.Dispose();
			return result;
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002ESolve_000000A5_0024PostfixBurstDelegate))]
		internal static bool Solve(in global::Unity.Mathematics.float2 targetPosition, int solverLimit, float tolerance, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			return global::UnityEngine.U2D.IK.FABRIK2D.Solve_000000A5_0024BurstDirectCall.Invoke(in targetPosition, solverLimit, tolerance, in lengths, ref positions);
		}

		public static bool SolveChain(int solverLimit, ref global::UnityEngine.U2D.IK.FABRIKChain2D[] chains)
		{
			if (ValidateChain(chains))
			{
				return false;
			}
			for (int i = 0; i < solverLimit; i++)
			{
				SolveForwardsChain(0, ref chains);
				if (!SolveBackwardsChain(0, ref chains))
				{
					break;
				}
			}
			return true;
		}

		private static bool ValidateChain(global::UnityEngine.U2D.IK.FABRIKChain2D[] chains)
		{
			for (int i = 0; i < chains.Length; i++)
			{
				global::UnityEngine.U2D.IK.FABRIKChain2D fABRIKChain2D = chains[i];
				if (fABRIKChain2D.subChainIndices.Length == 0 && (fABRIKChain2D.target - fABRIKChain2D.last).sqrMagnitude > fABRIKChain2D.sqrTolerance)
				{
					return false;
				}
			}
			return true;
		}

		private static void SolveForwardsChain(int idx, ref global::UnityEngine.U2D.IK.FABRIKChain2D[] chains)
		{
			global::UnityEngine.Vector2 targetPosition = chains[idx].target;
			if (chains[idx].subChainIndices.Length != 0)
			{
				targetPosition = global::UnityEngine.Vector2.zero;
				for (int i = 0; i < chains[idx].subChainIndices.Length; i++)
				{
					int num = chains[idx].subChainIndices[i];
					SolveForwardsChain(num, ref chains);
					targetPosition += chains[num].first;
				}
				targetPosition /= (float)chains[idx].subChainIndices.Length;
			}
			Forward(targetPosition, chains[idx].lengths, ref chains[idx].positions);
		}

		private static bool SolveBackwardsChain(int idx, ref global::UnityEngine.U2D.IK.FABRIKChain2D[] chains)
		{
			bool flag = false;
			Backward(chains[idx].origin, chains[idx].lengths, ref chains[idx].positions);
			for (int i = 0; i < chains[idx].subChainIndices.Length; i++)
			{
				int num = chains[idx].subChainIndices[i];
				chains[num].origin = chains[idx].last;
				flag |= SolveBackwardsChain(num, ref chains);
			}
			if (chains[idx].subChainIndices.Length == 0)
			{
				flag |= (chains[idx].target - chains[idx].last).sqrMagnitude > chains[idx].sqrTolerance;
			}
			return flag;
		}

		private static void Forward(global::UnityEngine.Vector2 targetPosition, float[] lengths, ref global::UnityEngine.Vector2[] positions)
		{
			int num = positions.Length - 1;
			positions[num] = targetPosition;
			for (int num2 = num - 1; num2 >= 0; num2--)
			{
				global::UnityEngine.Vector2 vector = positions[num2 + 1] - positions[num2];
				float num3 = lengths[num2] / vector.magnitude;
				global::UnityEngine.Vector2 vector2 = (1f - num3) * positions[num2 + 1] + num3 * positions[num2];
				positions[num2] = vector2;
			}
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002EForward_000000AB_0024PostfixBurstDelegate))]
		private static void Forward(in global::Unity.Mathematics.float2 targetPosition, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			global::UnityEngine.U2D.IK.FABRIK2D.Forward_000000AB_0024BurstDirectCall.Invoke(in targetPosition, in lengths, ref positions);
		}

		private static void Backward(global::UnityEngine.Vector2 originPosition, float[] lengths, ref global::UnityEngine.Vector2[] positions)
		{
			positions[0] = originPosition;
			int num = positions.Length - 1;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.Vector2 vector = positions[i + 1] - positions[i];
				float num2 = lengths[i] / vector.magnitude;
				global::UnityEngine.Vector2 vector2 = (1f - num2) * positions[i] + num2 * positions[i + 1];
				positions[i + 1] = vector2;
			}
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002EBackward_000000AD_0024PostfixBurstDelegate))]
		private static void Backward(in global::Unity.Mathematics.float2 originPosition, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			global::UnityEngine.U2D.IK.FABRIK2D.Backward_000000AD_0024BurstDirectCall.Invoke(in originPosition, in lengths, ref positions);
		}

		private static global::UnityEngine.Vector2 ValidateJoint(global::UnityEngine.Vector2 endPosition, global::UnityEngine.Vector2 startPosition, global::UnityEngine.Vector2 right, float min, float max)
		{
			global::UnityEngine.Vector2 to = endPosition - startPosition;
			float num = global::UnityEngine.Vector2.SignedAngle(right, to);
			global::UnityEngine.Vector2 result = endPosition;
			if (num < min)
			{
				global::UnityEngine.Quaternion quaternion2 = global::UnityEngine.Quaternion.Euler(0f, 0f, min);
				result = startPosition + (global::UnityEngine.Vector2)(quaternion2 * right * to.magnitude);
			}
			else if (num > max)
			{
				global::UnityEngine.Quaternion quaternion3 = global::UnityEngine.Quaternion.Euler(0f, 0f, max);
				result = startPosition + (global::UnityEngine.Vector2)(quaternion3 * right * to.magnitude);
			}
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static bool Solve_0024BurstManaged(in global::Unity.Mathematics.float2 targetPosition, int solverLimit, float tolerance, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			int index = positions.Length - 1;
			int num = 0;
			float num2 = tolerance * tolerance;
			float num3 = global::Unity.Mathematics.math.lengthsq(targetPosition - positions[index]);
			global::Unity.Mathematics.float2 originPosition = positions[0];
			while (num3 > num2)
			{
				Forward(in targetPosition, in lengths, ref positions);
				Backward(in originPosition, in lengths, ref positions);
				num3 = global::Unity.Mathematics.math.lengthsq(targetPosition - positions[index]);
				if (++num >= solverLimit)
				{
					break;
				}
			}
			return num != 0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void Forward_0024BurstManaged(in global::Unity.Mathematics.float2 targetPosition, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			int num = positions.Length - 1;
			positions[num] = targetPosition;
			for (int num2 = num - 1; num2 >= 0; num2--)
			{
				global::Unity.Mathematics.float2 x = positions[num2 + 1] - positions[num2];
				float num3 = lengths[num2] / global::Unity.Mathematics.math.length(x);
				global::Unity.Mathematics.float2 value = (1f - num3) * positions[num2 + 1] + num3 * positions[num2];
				positions[num2] = value;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void Backward_0024BurstManaged(in global::Unity.Mathematics.float2 originPosition, in global::Unity.Collections.NativeArray<float> lengths, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			positions[0] = originPosition;
			int num = positions.Length - 1;
			for (int i = 0; i < num; i++)
			{
				global::Unity.Mathematics.float2 x = positions[i + 1] - positions[i];
				float num2 = lengths[i] / global::Unity.Mathematics.math.length(x);
				global::Unity.Mathematics.float2 value = (1f - num2) * positions[i] + num2 * positions[i + 1];
				positions[i + 1] = value;
			}
		}
	}
}
