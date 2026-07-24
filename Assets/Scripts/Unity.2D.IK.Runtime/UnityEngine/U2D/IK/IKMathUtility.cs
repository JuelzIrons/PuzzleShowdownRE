namespace UnityEngine.U2D.IK
{
	[global::Unity.Burst.BurstCompile]
	internal static class IKMathUtility
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate float Angle_00000069_0024PostfixBurstDelegate(in global::Unity.Mathematics.float3 from, in global::Unity.Mathematics.float3 to);

		internal static class Angle_00000069_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.IKMathUtility.Angle_00000069_0024PostfixBurstDelegate>(Angle).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static float Invoke(in global::Unity.Mathematics.float3 from, in global::Unity.Mathematics.float3 to)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float3, ref global::Unity.Mathematics.float3, float>)functionPointer)(ref from, ref to);
					}
				}
				return Angle_0024BurstManaged(in from, in to);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate float Angle_0000006A_0024PostfixBurstDelegate(in global::Unity.Mathematics.float2 from, in global::Unity.Mathematics.float2 to);

		internal static class Angle_0000006A_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.IKMathUtility.Angle_0000006A_0024PostfixBurstDelegate>(global::UnityEngine.U2D.IK.IKMathUtility.Angle).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static float Invoke(in global::Unity.Mathematics.float2 from, in global::Unity.Mathematics.float2 to)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float2, ref global::Unity.Mathematics.float2, float>)functionPointer)(ref from, ref to);
					}
				}
				return Angle_0024BurstManaged(in from, in to);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate float SignedAngle_0000006B_0024PostfixBurstDelegate(in global::Unity.Mathematics.float3 from, in global::Unity.Mathematics.float3 to, in global::Unity.Mathematics.float3 axis);

		internal static class SignedAngle_0000006B_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.IKMathUtility.SignedAngle_0000006B_0024PostfixBurstDelegate>(SignedAngle).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static float Invoke(in global::Unity.Mathematics.float3 from, in global::Unity.Mathematics.float3 to, in global::Unity.Mathematics.float3 axis)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float3, ref global::Unity.Mathematics.float3, ref global::Unity.Mathematics.float3, float>)functionPointer)(ref from, ref to, ref axis);
					}
				}
				return SignedAngle_0024BurstManaged(in from, in to, in axis);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate float SignedAngle_0000006C_0024PostfixBurstDelegate(in global::Unity.Mathematics.float2 from, in global::Unity.Mathematics.float2 to);

		internal static class SignedAngle_0000006C_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.IK.IKMathUtility.SignedAngle_0000006C_0024PostfixBurstDelegate>(SignedAngle).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static float Invoke(in global::Unity.Mathematics.float2 from, in global::Unity.Mathematics.float2 to)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Mathematics.float2, ref global::Unity.Mathematics.float2, float>)functionPointer)(ref from, ref to);
					}
				}
				return SignedAngle_0024BurstManaged(in from, in to);
			}
		}

		internal const float kEpsilonNormalSqrt = 1E-15f;

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002EAngle_00000069_0024PostfixBurstDelegate))]
		internal static float Angle(in global::Unity.Mathematics.float3 from, in global::Unity.Mathematics.float3 to)
		{
			return global::UnityEngine.U2D.IK.IKMathUtility.Angle_00000069_0024BurstDirectCall.Invoke(in from, in to);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002EAngle_0000006A_0024PostfixBurstDelegate))]
		internal static float Angle(in global::Unity.Mathematics.float2 from, in global::Unity.Mathematics.float2 to)
		{
			return global::UnityEngine.U2D.IK.IKMathUtility.Angle_0000006A_0024BurstDirectCall.Invoke(in from, in to);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002ESignedAngle_0000006B_0024PostfixBurstDelegate))]
		internal static float SignedAngle(in global::Unity.Mathematics.float3 from, in global::Unity.Mathematics.float3 to, in global::Unity.Mathematics.float3 axis)
		{
			return global::UnityEngine.U2D.IK.IKMathUtility.SignedAngle_0000006B_0024BurstDirectCall.Invoke(in from, in to, in axis);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EIK_002ESignedAngle_0000006C_0024PostfixBurstDelegate))]
		internal static float SignedAngle(in global::Unity.Mathematics.float2 from, in global::Unity.Mathematics.float2 to)
		{
			return global::UnityEngine.U2D.IK.IKMathUtility.SignedAngle_0000006C_0024BurstDirectCall.Invoke(in from, in to);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static float Angle_0024BurstManaged(in global::Unity.Mathematics.float3 from, in global::Unity.Mathematics.float3 to)
		{
			float num = global::Unity.Mathematics.math.sqrt(global::Unity.Mathematics.math.lengthsq(from) * global::Unity.Mathematics.math.lengthsq(to));
			if (num < 1E-15f)
			{
				return 0f;
			}
			return global::Unity.Mathematics.math.acos(global::Unity.Mathematics.math.clamp(global::Unity.Mathematics.math.dot(from, to) / num, -1f, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static float Angle_0024BurstManaged(in global::Unity.Mathematics.float2 from, in global::Unity.Mathematics.float2 to)
		{
			float num = global::Unity.Mathematics.math.sqrt(global::Unity.Mathematics.math.lengthsq(from) * global::Unity.Mathematics.math.lengthsq(to));
			if (num < 1E-15f)
			{
				return 0f;
			}
			return global::Unity.Mathematics.math.acos(global::Unity.Mathematics.math.clamp(global::Unity.Mathematics.math.dot(from, to) / num, -1f, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static float SignedAngle_0024BurstManaged(in global::Unity.Mathematics.float3 from, in global::Unity.Mathematics.float3 to, in global::Unity.Mathematics.float3 axis)
		{
			float num = Angle(in from, in to);
			float num2 = global::Unity.Mathematics.math.sign(global::Unity.Mathematics.math.dot(y: global::Unity.Mathematics.math.cross(from, to), x: axis));
			return num * num2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static float SignedAngle_0024BurstManaged(in global::Unity.Mathematics.float2 from, in global::Unity.Mathematics.float2 to)
		{
			float num = Angle(in from, in to);
			float num2 = global::Unity.Mathematics.math.sign(from.x * to.y - from.y * to.x);
			return num * num2;
		}
	}
}
