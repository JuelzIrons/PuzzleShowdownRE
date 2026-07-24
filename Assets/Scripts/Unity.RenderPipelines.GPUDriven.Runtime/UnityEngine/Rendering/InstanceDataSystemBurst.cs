namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile]
	internal static class InstanceDataSystemBurst
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void ReallocateInstances_000002A0_0024PostfixBurstDelegate(bool implicitInstanceIndices, in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedRendererData> packedRendererData, in global::Unity.Collections.NativeArray<int> instanceOffsets, in global::Unity.Collections.NativeArray<int> instanceCounts, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash);

		internal static class ReallocateInstances_000002A0_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.InstanceDataSystemBurst.ReallocateInstances_000002A0_0024PostfixBurstDelegate>(ReallocateInstances).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(bool implicitInstanceIndices, in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedRendererData> packedRendererData, in global::Unity.Collections.NativeArray<int> instanceOffsets, in global::Unity.Collections.NativeArray<int> instanceCounts, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<bool, ref global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedRendererData>, ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<int>, ref global::UnityEngine.Rendering.InstanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle>, void>)functionPointer)(implicitInstanceIndices, ref rendererGroupIDs, ref packedRendererData, ref instanceOffsets, ref instanceCounts, ref instanceAllocators, ref instanceData, ref perCameraInstanceData, ref sharedInstanceData, ref instances, ref rendererGroupInstanceMultiHash);
						return;
					}
				}
				ReallocateInstances_0024BurstManaged(implicitInstanceIndices, in rendererGroupIDs, in packedRendererData, in instanceOffsets, in instanceCounts, ref instanceAllocators, ref instanceData, ref perCameraInstanceData, ref sharedInstanceData, ref instances, ref rendererGroupInstanceMultiHash);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void FreeRendererGroupInstances_000002A1_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroupsID, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash);

		internal static class FreeRendererGroupInstances_000002A1_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.InstanceDataSystemBurst.FreeRendererGroupInstances_000002A1_0024PostfixBurstDelegate>(FreeRendererGroupInstances).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroupsID, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly, ref global::UnityEngine.Rendering.InstanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle>, void>)functionPointer)(ref rendererGroupsID, ref instanceAllocators, ref instanceData, ref perCameraInstanceData, ref sharedInstanceData, ref rendererGroupInstanceMultiHash);
						return;
					}
				}
				FreeRendererGroupInstances_0024BurstManaged(in rendererGroupsID, ref instanceAllocators, ref instanceData, ref perCameraInstanceData, ref sharedInstanceData, ref rendererGroupInstanceMultiHash);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void FreeInstances_000002A2_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>.ReadOnly instances, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash);

		internal static class FreeInstances_000002A2_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.InstanceDataSystemBurst.FreeInstances_000002A2_0024PostfixBurstDelegate>(FreeInstances).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>.ReadOnly instances, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>.ReadOnly, ref global::UnityEngine.Rendering.InstanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle>, void>)functionPointer)(ref instances, ref instanceAllocators, ref instanceData, ref perCameraInstanceData, ref sharedInstanceData, ref rendererGroupInstanceMultiHash);
						return;
					}
				}
				FreeInstances_0024BurstManaged(in instances, ref instanceAllocators, ref instanceData, ref perCameraInstanceData, ref sharedInstanceData, ref rendererGroupInstanceMultiHash);
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EReallocateInstances_000002A0_0024PostfixBurstDelegate))]
		public static void ReallocateInstances(bool implicitInstanceIndices, in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedRendererData> packedRendererData, in global::Unity.Collections.NativeArray<int> instanceOffsets, in global::Unity.Collections.NativeArray<int> instanceCounts, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash)
		{
			global::UnityEngine.Rendering.InstanceDataSystemBurst.ReallocateInstances_000002A0_0024BurstDirectCall.Invoke(implicitInstanceIndices, in rendererGroupIDs, in packedRendererData, in instanceOffsets, in instanceCounts, ref instanceAllocators, ref instanceData, ref perCameraInstanceData, ref sharedInstanceData, ref instances, ref rendererGroupInstanceMultiHash);
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EFreeRendererGroupInstances_000002A1_0024PostfixBurstDelegate))]
		public static void FreeRendererGroupInstances(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroupsID, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash)
		{
			global::UnityEngine.Rendering.InstanceDataSystemBurst.FreeRendererGroupInstances_000002A1_0024BurstDirectCall.Invoke(in rendererGroupsID, ref instanceAllocators, ref instanceData, ref perCameraInstanceData, ref sharedInstanceData, ref rendererGroupInstanceMultiHash);
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EFreeInstances_000002A2_0024PostfixBurstDelegate))]
		public static void FreeInstances(in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>.ReadOnly instances, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash)
		{
			global::UnityEngine.Rendering.InstanceDataSystemBurst.FreeInstances_000002A2_0024BurstDirectCall.Invoke(in instances, ref instanceAllocators, ref instanceData, ref perCameraInstanceData, ref sharedInstanceData, ref rendererGroupInstanceMultiHash);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal static void ReallocateInstances_0024BurstManaged(bool implicitInstanceIndices, in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedRendererData> packedRendererData, in global::Unity.Collections.NativeArray<int> instanceOffsets, in global::Unity.Collections.NativeArray<int> instanceCounts, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash)
		{
			for (int i = 0; i < rendererGroupIDs.Length; i++)
			{
				global::UnityEngine.EntityId entityId = rendererGroupIDs[i];
				bool hasTree = packedRendererData[i].hasTree;
				int num;
				int num2;
				if (implicitInstanceIndices)
				{
					num = 1;
					num2 = i;
				}
				else
				{
					num = instanceCounts[i];
					num2 = instanceOffsets[i];
				}
				global::UnityEngine.Rendering.SharedInstanceHandle sharedInstanceHandle;
				if (rendererGroupInstanceMultiHash.TryGetFirstValue(entityId, out var item, out var it))
				{
					sharedInstanceHandle = instanceData.Get_SharedInstance(item);
					if (sharedInstanceData.Get_RefCount(sharedInstanceHandle) - num > 0)
					{
						bool flag = true;
						int num3 = 0;
						for (int j = 0; j < num; j++)
						{
							flag = rendererGroupInstanceMultiHash.TryGetNextValue(out item, ref it);
						}
						while (flag)
						{
							int index = instanceData.InstanceToIndex(item);
							instanceData.Remove(item);
							perCameraInstanceData.Remove(index);
							instanceAllocators.FreeInstance(item);
							rendererGroupInstanceMultiHash.Remove(it);
							num3++;
							flag = rendererGroupInstanceMultiHash.TryGetNextValue(out item, ref it);
						}
					}
				}
				else
				{
					sharedInstanceHandle = instanceAllocators.AllocateSharedInstance();
					sharedInstanceData.AddNoGrow(sharedInstanceHandle);
				}
				if (num > 0)
				{
					sharedInstanceData.Set_RefCount(sharedInstanceHandle, num);
					for (int k = 0; k < num; k++)
					{
						int index2 = num2 + k;
						if (!instances[index2].valid)
						{
							global::UnityEngine.Rendering.InstanceHandle instanceHandle = (hasTree ? instanceAllocators.AllocateInstance(global::UnityEngine.Rendering.InstanceType.SpeedTree) : instanceAllocators.AllocateInstance(global::UnityEngine.Rendering.InstanceType.MeshRenderer));
							instanceData.AddNoGrow(instanceHandle);
							perCameraInstanceData.IncreaseInstanceCount();
							int index3 = instanceData.InstanceToIndex(instanceHandle);
							instanceData.sharedInstances[index3] = sharedInstanceHandle;
							instanceData.movedInCurrentFrameBits.Set(index3, value: false);
							instanceData.movedInPreviousFrameBits.Set(index3, value: false);
							instanceData.visibleInPreviousFrameBits.Set(index3, value: false);
							rendererGroupInstanceMultiHash.Add(entityId, instanceHandle);
							instances[index2] = instanceHandle;
						}
					}
				}
				else
				{
					sharedInstanceData.Remove(sharedInstanceHandle);
					instanceAllocators.FreeSharedInstance(sharedInstanceHandle);
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal static void FreeRendererGroupInstances_0024BurstManaged(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroupsID, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash)
		{
			foreach (global::UnityEngine.EntityId item2 in rendererGroupsID)
			{
				global::UnityEngine.Rendering.InstanceHandle item;
				global::Unity.Collections.NativeParallelMultiHashMapIterator<int> it;
				bool flag = rendererGroupInstanceMultiHash.TryGetFirstValue(item2, out item, out it);
				while (flag)
				{
					global::UnityEngine.Rendering.SharedInstanceHandle instance = instanceData.Get_SharedInstance(item);
					int index = sharedInstanceData.SharedInstanceToIndex(instance);
					int num = sharedInstanceData.refCounts[index];
					if (num > 1)
					{
						sharedInstanceData.refCounts[index] = num - 1;
					}
					else
					{
						sharedInstanceData.Remove(instance);
						instanceAllocators.FreeSharedInstance(instance);
					}
					int index2 = instanceData.InstanceToIndex(item);
					instanceData.Remove(item);
					perCameraInstanceData.Remove(index2);
					instanceAllocators.FreeInstance(item);
					flag = rendererGroupInstanceMultiHash.TryGetNextValue(out item, ref it);
				}
				rendererGroupInstanceMultiHash.Remove(item2);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal static void FreeInstances_0024BurstManaged(in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>.ReadOnly instances, ref global::UnityEngine.Rendering.InstanceAllocators instanceAllocators, ref global::UnityEngine.Rendering.CPUInstanceData instanceData, ref global::UnityEngine.Rendering.CPUPerCameraInstanceData perCameraInstanceData, ref global::UnityEngine.Rendering.CPUSharedInstanceData sharedInstanceData, ref global::Unity.Collections.NativeParallelMultiHashMap<int, global::UnityEngine.Rendering.InstanceHandle> rendererGroupInstanceMultiHash)
		{
			foreach (global::UnityEngine.Rendering.InstanceHandle instance2 in instances)
			{
				if (!instanceData.IsValidInstance(instance2))
				{
					continue;
				}
				int index = instanceData.InstanceToIndex(instance2);
				global::UnityEngine.Rendering.SharedInstanceHandle instance = instanceData.sharedInstances[index];
				int index2 = sharedInstanceData.SharedInstanceToIndex(instance);
				int num = sharedInstanceData.refCounts[index2];
				global::UnityEngine.EntityId entityId = sharedInstanceData.rendererGroupIDs[index2];
				if (num > 1)
				{
					sharedInstanceData.refCounts[index2] = num - 1;
				}
				else
				{
					sharedInstanceData.Remove(instance);
					instanceAllocators.FreeSharedInstance(instance);
				}
				int index3 = instanceData.InstanceToIndex(instance2);
				instanceData.Remove(instance2);
				perCameraInstanceData.Remove(index3);
				instanceAllocators.FreeInstance(instance2);
				global::UnityEngine.Rendering.InstanceHandle item;
				global::Unity.Collections.NativeParallelMultiHashMapIterator<int> it;
				bool flag = rendererGroupInstanceMultiHash.TryGetFirstValue(entityId, out item, out it);
				while (flag)
				{
					if (instance2.Equals(item))
					{
						rendererGroupInstanceMultiHash.Remove(it);
						break;
					}
					flag = rendererGroupInstanceMultiHash.TryGetNextValue(out item, ref it);
				}
			}
		}
	}
}
