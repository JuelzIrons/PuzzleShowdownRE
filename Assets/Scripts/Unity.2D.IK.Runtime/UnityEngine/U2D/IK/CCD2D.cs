namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[global::Unity.Burst.BurstCompile]
	public static class CCD2D
	{
		private static class Profiling
		{
			internal static readonly global::Unity.Profiling.ProfilerMarker Solve = new global::Unity.Profiling.ProfilerMarker("CCD2D.Solve");
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate bool Solve_0000009F_0024PostfixBurstDelegate(in global::Unity.Mathematics.float2 targetPosition, int solverLimit, float tolerance, float velocity, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions);

		internal static class Solve_0000009F_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.CCD2D.Solve_0000009F_0024PostfixBurstDelegate>(Solve).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static bool Invoke(in global::Unity.Mathematics.float2 targetPosition, int solverLimit, float tolerance, float velocity, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float2, int, float, float, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>, bool>)functionPointer)(ref targetPosition, solverLimit, tolerance, velocity, ref positions);
					}
				}
				return Solve_0024BurstManaged(in targetPosition, solverLimit, tolerance, velocity, ref positions);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void DoIteration_000000A0_0024PostfixBurstDelegate([global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 targetPosition, [global::Unity.Burst.CompilerServices.AssumeRange(1L, 2147483647L)] int last, float velocity, [global::Unity.Burst.NoAlias] ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions);

		internal static class DoIteration_000000A0_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.CCD2D.DoIteration_000000A0_0024PostfixBurstDelegate>(DoIteration).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke([global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 targetPosition, [global::Unity.Burst.CompilerServices.AssumeRange(1L, 2147483647L)] int last, float velocity, [global::Unity.Burst.NoAlias] ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float2, int, float, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>, void>)functionPointer)(ref targetPosition, last, velocity, ref positions);
						return;
					}
				}
				DoIteration_0024BurstManaged(in targetPosition, last, velocity, ref positions);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void RotatePositionFrom_000000A1_0024PostfixBurstDelegate([global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 position, [global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 pivot, float s, float c, [global::Unity.Burst.NoAlias] out global::Unity.Mathematics.float2 result);

		internal static class RotatePositionFrom_000000A1_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.CCD2D.RotatePositionFrom_000000A1_0024PostfixBurstDelegate>(RotatePositionFrom).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke([global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 position, [global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 pivot, float s, float c, [global::Unity.Burst.NoAlias] out global::Unity.Mathematics.float2 result)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float2, ref global::Unity.Mathematics.float2, float, float, ref global::Unity.Mathematics.float2, void>)functionPointer)(ref position, ref pivot, s, c, ref result);
						return;
					}
				}
				RotatePositionFrom_0024BurstManaged(in position, in pivot, s, c, out result);
			}
		}

		public static bool Solve(global::UnityEngine.Vector3 targetPosition, global::UnityEngine.Vector3 forward, int solverLimit, float tolerance, float velocity, ref global::UnityEngine.Vector3[] positions)
		{
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions2 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(positions.Length, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < positions.Length; i++)
			{
				positions2[i] = new global::Unity.Mathematics.float2(positions[i].x, positions[i].y);
			}
			bool result = Solve((global::Unity.Mathematics.float2)(global::UnityEngine.Vector2)targetPosition, solverLimit, tolerance, velocity, ref positions2);
			for (int j = 0; j < positions.Length; j++)
			{
				positions[j] = (global::UnityEngine.Vector2)positions2[j];
			}
			positions2.Dispose();
			return result;
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002ESolve_0000009F_0024PostfixBurstDelegate))]
		internal static bool Solve(in global::Unity.Mathematics.float2 targetPosition, int solverLimit, float tolerance, float velocity, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			return global::UnityEngine.U2D.IK.CCD2D.Solve_0000009F_0024BurstDirectCall.Invoke(in targetPosition, solverLimit, tolerance, velocity, ref positions);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002EDoIteration_000000A0_0024PostfixBurstDelegate))]
		private static void DoIteration([global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 targetPosition, [global::Unity.Burst.CompilerServices.AssumeRange(1L, 2147483647L)] int last, float velocity, [global::Unity.Burst.NoAlias] ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			global::UnityEngine.U2D.IK.CCD2D.DoIteration_000000A0_0024BurstDirectCall.Invoke(in targetPosition, last, velocity, ref positions);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002ERotatePositionFrom_000000A1_0024PostfixBurstDelegate))]
		private static void RotatePositionFrom([global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 position, [global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 pivot, float s, float c, [global::Unity.Burst.NoAlias] out global::Unity.Mathematics.float2 result)
		{
			global::UnityEngine.U2D.IK.CCD2D.RotatePositionFrom_000000A1_0024BurstDirectCall.Invoke(in position, in pivot, s, c, out result);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static bool Solve_0024BurstManaged(in global::Unity.Mathematics.float2 targetPosition, int solverLimit, float tolerance, float velocity, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			int num = positions.Length - 1;
			int num2 = 0;
			float num3 = tolerance * tolerance;
			float num4 = global::Unity.Mathematics.math.lengthsq(targetPosition - positions[num]);
			while (num4 > num3)
			{
				DoIteration(in targetPosition, num, velocity, ref positions);
				num4 = global::Unity.Mathematics.math.lengthsq(targetPosition - positions[num]);
				if (++num2 >= solverLimit)
				{
					break;
				}
			}
			return num2 != 0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void DoIteration_0024BurstManaged([global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 targetPosition, [global::Unity.Burst.CompilerServices.AssumeRange(1L, 2147483647L)] int last, float velocity, [global::Unity.Burst.NoAlias] ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> positions)
		{
			for (int num = last - 1; num >= 0; num--)
			{
				global::Unity.Mathematics.float2 pivot = positions[num];
				global::Unity.Mathematics.float2 to = targetPosition - pivot;
				global::Unity.Mathematics.math.sincos(global::UnityEngine.U2D.IK.IKMathUtility.SignedAngle(positions[last] - pivot, in to) * velocity, out var s, out var c);
				for (int num2 = last; num2 > num; num2--)
				{
					RotatePositionFrom(positions[num2], in pivot, s, c, out var result);
					positions[num2] = result;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void RotatePositionFrom_0024BurstManaged([global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 position, [global::Unity.Burst.NoAlias] in global::Unity.Mathematics.float2 pivot, float s, float c, [global::Unity.Burst.NoAlias] out global::Unity.Mathematics.float2 result)
		{
			global::Unity.Mathematics.float2 float5 = position - pivot;
			global::Unity.Mathematics.float2 float6 = default(global::Unity.Mathematics.float2);
			float6.x = c * float5.x - s * float5.y;
			float6.y = s * float5.x + c * float5.y;
			result = pivot + float6;
		}
	}
}
