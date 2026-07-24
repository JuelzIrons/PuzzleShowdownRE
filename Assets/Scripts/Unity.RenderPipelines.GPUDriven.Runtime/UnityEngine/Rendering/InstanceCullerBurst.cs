namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile]
	internal static class InstanceCullerBurst
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal unsafe delegate void SetupCullingJobInput_0000014D_0024PostfixBurstDelegate(float lodBias, float meshLodThreshold, global::UnityEngine.Rendering.BatchCullingContext* context, global::UnityEngine.Rendering.ReceiverPlanes* receiverPlanes, global::UnityEngine.Rendering.ReceiverSphereCuller* receiverSphereCuller, global::UnityEngine.Rendering.FrustumPlaneCuller* frustumPlaneCuller, float* screenRelativeMetric, float* meshLodConstant);

		internal static class SetupCullingJobInput_0000014D_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.InstanceCullerBurst.SetupCullingJobInput_0000014D_0024PostfixBurstDelegate>(SetupCullingJobInput).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float lodBias, float meshLodThreshold, global::UnityEngine.Rendering.BatchCullingContext* context, global::UnityEngine.Rendering.ReceiverPlanes* receiverPlanes, global::UnityEngine.Rendering.ReceiverSphereCuller* receiverSphereCuller, global::UnityEngine.Rendering.FrustumPlaneCuller* frustumPlaneCuller, float* screenRelativeMetric, float* meshLodConstant)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float, float, global::UnityEngine.Rendering.BatchCullingContext*, global::UnityEngine.Rendering.ReceiverPlanes*, global::UnityEngine.Rendering.ReceiverSphereCuller*, global::UnityEngine.Rendering.FrustumPlaneCuller*, float*, float*, void>)functionPointer)(lodBias, meshLodThreshold, context, receiverPlanes, receiverSphereCuller, frustumPlaneCuller, screenRelativeMetric, meshLodConstant);
						return;
					}
				}
				SetupCullingJobInput_0024BurstManaged(lodBias, meshLodThreshold, context, receiverPlanes, receiverSphereCuller, frustumPlaneCuller, screenRelativeMetric, meshLodConstant);
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002ESetupCullingJobInput_0000014D_0024PostfixBurstDelegate))]
		public unsafe static void SetupCullingJobInput(float lodBias, float meshLodThreshold, global::UnityEngine.Rendering.BatchCullingContext* context, global::UnityEngine.Rendering.ReceiverPlanes* receiverPlanes, global::UnityEngine.Rendering.ReceiverSphereCuller* receiverSphereCuller, global::UnityEngine.Rendering.FrustumPlaneCuller* frustumPlaneCuller, float* screenRelativeMetric, float* meshLodConstant)
		{
			global::UnityEngine.Rendering.InstanceCullerBurst.SetupCullingJobInput_0000014D_0024BurstDirectCall.Invoke(lodBias, meshLodThreshold, context, receiverPlanes, receiverSphereCuller, frustumPlaneCuller, screenRelativeMetric, meshLodConstant);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal unsafe static void SetupCullingJobInput_0024BurstManaged(float lodBias, float meshLodThreshold, global::UnityEngine.Rendering.BatchCullingContext* context, global::UnityEngine.Rendering.ReceiverPlanes* receiverPlanes, global::UnityEngine.Rendering.ReceiverSphereCuller* receiverSphereCuller, global::UnityEngine.Rendering.FrustumPlaneCuller* frustumPlaneCuller, float* screenRelativeMetric, float* meshLodConstant)
		{
			*receiverPlanes = global::UnityEngine.Rendering.ReceiverPlanes.Create(in *context, global::Unity.Collections.Allocator.TempJob);
			*receiverSphereCuller = global::UnityEngine.Rendering.ReceiverSphereCuller.Create(in *context, global::Unity.Collections.Allocator.TempJob);
			*frustumPlaneCuller = global::UnityEngine.Rendering.FrustumPlaneCuller.Create(in *context, receiverPlanes->planes.AsArray(), in *receiverSphereCuller, global::Unity.Collections.Allocator.TempJob);
			*screenRelativeMetric = global::UnityEngine.Rendering.LODRenderingUtils.CalculateScreenRelativeMetricNoBias(context->lodParameters);
			*meshLodConstant = global::UnityEngine.Rendering.LODRenderingUtils.CalculateMeshLodConstant(context->lodParameters, *screenRelativeMetric, meshLodThreshold);
			*screenRelativeMetric /= lodBias;
		}
	}
}
