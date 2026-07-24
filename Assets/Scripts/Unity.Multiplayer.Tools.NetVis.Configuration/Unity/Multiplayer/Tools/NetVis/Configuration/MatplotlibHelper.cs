namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	[global::Unity.Burst.BurstCompile]
	internal static class MatplotlibHelper
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void GetViridisColor_00000020_0024PostfixBurstDelegate(float x, out global::Unity.Mathematics.float3 rgb);

		internal static class GetViridisColor_00000020_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetViridisColor_00000020_0024PostfixBurstDelegate>(GetViridisColor).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float x, out global::Unity.Mathematics.float3 rgb)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float, ref global::Unity.Mathematics.float3, void>)functionPointer)(x, ref rgb);
						return;
					}
				}
				GetViridisColor_0024BurstManaged(x, out rgb);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void GetPlasmaColor_00000021_0024PostfixBurstDelegate(float x, out global::Unity.Mathematics.float3 rgb);

		internal static class GetPlasmaColor_00000021_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetPlasmaColor_00000021_0024PostfixBurstDelegate>(GetPlasmaColor).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float x, out global::Unity.Mathematics.float3 rgb)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float, ref global::Unity.Mathematics.float3, void>)functionPointer)(x, ref rgb);
						return;
					}
				}
				GetPlasmaColor_0024BurstManaged(x, out rgb);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void GetMagmaColor_00000022_0024PostfixBurstDelegate(float x, out global::Unity.Mathematics.float3 rgb);

		internal static class GetMagmaColor_00000022_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetMagmaColor_00000022_0024PostfixBurstDelegate>(GetMagmaColor).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float x, out global::Unity.Mathematics.float3 rgb)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float, ref global::Unity.Mathematics.float3, void>)functionPointer)(x, ref rgb);
						return;
					}
				}
				GetMagmaColor_0024BurstManaged(x, out rgb);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void GetInfernoColor_00000023_0024PostfixBurstDelegate(float x, out global::Unity.Mathematics.float3 rgb);

		internal static class GetInfernoColor_00000023_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetInfernoColor_00000023_0024PostfixBurstDelegate>(GetInfernoColor).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float x, out global::Unity.Mathematics.float3 rgb)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float, ref global::Unity.Mathematics.float3, void>)functionPointer)(x, ref rgb);
						return;
					}
				}
				GetInfernoColor_0024BurstManaged(x, out rgb);
			}
		}

		private static readonly global::Unity.Mathematics.float3x4 Viridis1 = global::Unity.Mathematics.math.float3x4(0.27772734f, 0.10509304f, -0.33086184f, -4.6342306f, 0.0054073445f, 1.4046135f, 0.21484756f, -5.799101f, 0.3340998f, 1.3845901f, 0.095095165f, -19.332441f);

		private static readonly global::Unity.Mathematics.float3x3 Viridis2 = global::Unity.Mathematics.math.float3x3(6.22827f, 4.776385f, -5.435456f, 14.179934f, -13.745146f, 4.6458526f, 56.69055f, -65.353035f, 26.312435f);

		private static readonly global::Unity.Mathematics.float3x4 Plasma1 = global::Unity.Mathematics.math.float3x4(0.058732346f, 2.1765146f, -2.6894605f, 6.130348f, 0.023336709f, 0.23838341f, -7.455851f, 42.346188f, 0.5433402f, 0.75396043f, 3.1108f, -28.518854f);

		private static readonly global::Unity.Mathematics.float3x3 Plasma2 = global::Unity.Mathematics.math.float3x3(-11.107436f, 10.023066f, -3.6587138f, -82.66631f, 71.41362f, -22.931534f, 60.139847f, -54.072186f, 18.191908f);

		private static readonly global::Unity.Mathematics.float3x4 Magma1 = global::Unity.Mathematics.math.float3x4(-0.002136485f, 0.25166056f, 8.353717f, -27.668734f, -0.00074965507f, 0.67752326f, -3.5777194f, 14.26473f, -0.0053861276f, 2.4940267f, 0.3144679f, -13.649213f);

		private static readonly global::Unity.Mathematics.float3x3 Magma2 = global::Unity.Mathematics.math.float3x3(52.17614f, -50.768524f, 18.655704f, -27.943605f, 29.046583f, -11.489774f, 12.944169f, 4.234153f, -5.6019616f);

		private static readonly global::Unity.Mathematics.float3x4 Inferno1 = global::Unity.Mathematics.math.float3x4(0.00021894037f, 0.10651342f, 11.602493f, -41.703995f, 0.0016510047f, 0.56395644f, -3.972854f, 17.4364f, -0.019480899f, 3.9327123f, -15.942394f, 44.354145f);

		private static readonly global::Unity.Mathematics.float3x3 Inferno2 = global::Unity.Mathematics.math.float3x3(77.16293f, -71.31943f, 25.131126f, -33.40236f, 32.626064f, -12.242669f, -81.80731f, 73.20952f, -23.070326f);

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(Unity_002EMultiplayer_002ETools_002ENetVis_002EConfiguration_002EGetViridisColor_00000020_0024PostfixBurstDelegate))]
		public static void GetViridisColor(float x, out global::Unity.Mathematics.float3 rgb)
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetViridisColor_00000020_0024BurstDirectCall.Invoke(x, out rgb);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(Unity_002EMultiplayer_002ETools_002ENetVis_002EConfiguration_002EGetPlasmaColor_00000021_0024PostfixBurstDelegate))]
		public static void GetPlasmaColor(float x, out global::Unity.Mathematics.float3 rgb)
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetPlasmaColor_00000021_0024BurstDirectCall.Invoke(x, out rgb);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(Unity_002EMultiplayer_002ETools_002ENetVis_002EConfiguration_002EGetMagmaColor_00000022_0024PostfixBurstDelegate))]
		public static void GetMagmaColor(float x, out global::Unity.Mathematics.float3 rgb)
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetMagmaColor_00000022_0024BurstDirectCall.Invoke(x, out rgb);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(Unity_002EMultiplayer_002ETools_002ENetVis_002EConfiguration_002EGetInfernoColor_00000023_0024PostfixBurstDelegate))]
		public static void GetInfernoColor(float x, out global::Unity.Mathematics.float3 rgb)
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetInfernoColor_00000023_0024BurstDirectCall.Invoke(x, out rgb);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void GetViridisColor_0024BurstManaged(float x, out global::Unity.Mathematics.float3 rgb)
		{
			global::Unity.Mathematics.float4 b = global::Unity.Mathematics.math.float4(1f, x, x * x, x * x * x);
			rgb = global::Unity.Mathematics.math.mul(Viridis1, b) + global::Unity.Mathematics.math.mul(Viridis2, b.yzw * b.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void GetPlasmaColor_0024BurstManaged(float x, out global::Unity.Mathematics.float3 rgb)
		{
			global::Unity.Mathematics.float4 b = global::Unity.Mathematics.math.float4(1f, x, x * x, x * x * x);
			rgb = global::Unity.Mathematics.math.mul(Plasma1, b) + global::Unity.Mathematics.math.mul(Plasma2, b.yzw * b.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void GetMagmaColor_0024BurstManaged(float x, out global::Unity.Mathematics.float3 rgb)
		{
			global::Unity.Mathematics.float4 b = global::Unity.Mathematics.math.float4(1f, x, x * x, x * x * x);
			rgb = global::Unity.Mathematics.math.mul(Magma1, b) + global::Unity.Mathematics.math.mul(Magma2, b.yzw * b.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void GetInfernoColor_0024BurstManaged(float x, out global::Unity.Mathematics.float3 rgb)
		{
			global::Unity.Mathematics.float4 b = global::Unity.Mathematics.math.float4(1f, x, x * x, x * x * x);
			rgb = global::Unity.Mathematics.math.mul(Inferno1, b) + global::Unity.Mathematics.math.mul(Inferno2, b.yzw * b.w);
		}
	}
}
