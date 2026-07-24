namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile]
	internal static class LODGroupDataPoolBurst
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate int FreeLODGroupData_000002F2_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedLODGroupsID, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> lodGroupsData, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex> freeLODGroupDataHandles);

		internal static class FreeLODGroupData_000002F2_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.LODGroupDataPoolBurst.FreeLODGroupData_000002F2_0024PostfixBurstDelegate>(FreeLODGroupData).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static int Invoke(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedLODGroupsID, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> lodGroupsData, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex> freeLODGroupDataHandles)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData>, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex>, int>)functionPointer)(ref destroyedLODGroupsID, ref lodGroupsData, ref lodGroupDataHash, ref freeLODGroupDataHandles);
					}
				}
				return FreeLODGroupData_0024BurstManaged(in destroyedLODGroupsID, ref lodGroupsData, ref lodGroupDataHash, ref freeLODGroupDataHandles);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate int AllocateOrGetLODGroupDataInstances_000002F3_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> lodGroupsID, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> lodGroupsData, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex> freeLODGroupDataHandles, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupInstances);

		internal static class AllocateOrGetLODGroupDataInstances_000002F3_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.LODGroupDataPoolBurst.AllocateOrGetLODGroupDataInstances_000002F3_0024PostfixBurstDelegate>(AllocateOrGetLODGroupDataInstances).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static int Invoke(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> lodGroupsID, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> lodGroupsData, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex> freeLODGroupDataHandles, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupInstances)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData>, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex>, int>)functionPointer)(ref lodGroupsID, ref lodGroupsData, ref lodGroupCullingData, ref lodGroupDataHash, ref freeLODGroupDataHandles, ref lodGroupInstances);
					}
				}
				return AllocateOrGetLODGroupDataInstances_0024BurstManaged(in lodGroupsID, ref lodGroupsData, ref lodGroupCullingData, ref lodGroupDataHash, ref freeLODGroupDataHandles, ref lodGroupInstances);
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EFreeLODGroupData_000002F2_0024PostfixBurstDelegate))]
		public static int FreeLODGroupData(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedLODGroupsID, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> lodGroupsData, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex> freeLODGroupDataHandles)
		{
			return global::UnityEngine.Rendering.LODGroupDataPoolBurst.FreeLODGroupData_000002F2_0024BurstDirectCall.Invoke(in destroyedLODGroupsID, ref lodGroupsData, ref lodGroupDataHash, ref freeLODGroupDataHandles);
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EAllocateOrGetLODGroupDataInstances_000002F3_0024PostfixBurstDelegate))]
		public static int AllocateOrGetLODGroupDataInstances(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> lodGroupsID, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> lodGroupsData, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex> freeLODGroupDataHandles, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupInstances)
		{
			return global::UnityEngine.Rendering.LODGroupDataPoolBurst.AllocateOrGetLODGroupDataInstances_000002F3_0024BurstDirectCall.Invoke(in lodGroupsID, ref lodGroupsData, ref lodGroupCullingData, ref lodGroupDataHash, ref freeLODGroupDataHandles, ref lodGroupInstances);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal static int FreeLODGroupData_0024BurstManaged(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedLODGroupsID, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> lodGroupsData, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex> freeLODGroupDataHandles)
		{
			int num = 0;
			foreach (global::UnityEngine.EntityId item2 in destroyedLODGroupsID)
			{
				int key = item2;
				if (lodGroupDataHash.TryGetValue(key, out var item))
				{
					lodGroupDataHash.Remove(key);
					freeLODGroupDataHandles.Add(in item);
					ref global::UnityEngine.Rendering.LODGroupData reference = ref lodGroupsData.ElementAt(item.index);
					num += reference.rendererCount;
					reference.valid = false;
				}
			}
			return num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal static int AllocateOrGetLODGroupDataInstances_0024BurstManaged(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> lodGroupsID, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupData> lodGroupsData, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData, ref global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupDataHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceIndex> freeLODGroupDataHandles, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> lodGroupInstances)
		{
			int num = freeLODGroupDataHandles.Length;
			int length = lodGroupsData.Length;
			int num2 = 0;
			for (int i = 0; i < lodGroupsID.Length; i++)
			{
				int key = lodGroupsID[i];
				if (!lodGroupDataHash.TryGetValue(key, out var item))
				{
					item = ((num != 0) ? freeLODGroupDataHandles[--num] : new global::UnityEngine.Rendering.GPUInstanceIndex
					{
						index = length++
					});
					lodGroupDataHash.TryAdd(key, item);
				}
				else
				{
					num2 += lodGroupsData.ElementAt(item.index).rendererCount;
				}
				lodGroupInstances[i] = item;
			}
			freeLODGroupDataHandles.ResizeUninitialized(num);
			lodGroupsData.ResizeUninitialized(length);
			lodGroupCullingData.ResizeUninitialized(length);
			return num2;
		}
	}
}
