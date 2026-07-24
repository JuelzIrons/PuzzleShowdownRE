namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile]
	internal static class GPUResidentDrawerBurst
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void ClassifyMaterials_000000EA_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID>.ReadOnly batchMaterialHash, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> supportedMaterialIDs, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedMaterialIDs, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> supportedPackedMaterialDatas);

		internal static class ClassifyMaterials_000000EA_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.GPUResidentDrawerBurst.ClassifyMaterials_000000EA_0024PostfixBurstDelegate>(ClassifyMaterials).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID>.ReadOnly batchMaterialHash, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> supportedMaterialIDs, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedMaterialIDs, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> supportedPackedMaterialDatas)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID>.ReadOnly, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId>, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>, void>)functionPointer)(ref materialIDs, ref batchMaterialHash, ref supportedMaterialIDs, ref unsupportedMaterialIDs, ref supportedPackedMaterialDatas);
						return;
					}
				}
				ClassifyMaterials_0024BurstManaged(in materialIDs, in batchMaterialHash, ref supportedMaterialIDs, ref unsupportedMaterialIDs, ref supportedPackedMaterialDatas);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void FindUnsupportedRenderers_000000EB_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> unsupportedMaterials, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray>.ReadOnly materialIDArrays, in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroups, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedRenderers);

		internal static class FindUnsupportedRenderers_000000EB_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.GPUResidentDrawerBurst.FindUnsupportedRenderers_000000EB_0024PostfixBurstDelegate>(FindUnsupportedRenderers).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> unsupportedMaterials, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray>.ReadOnly materialIDArrays, in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroups, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedRenderers)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray>.ReadOnly, ref global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId>, void>)functionPointer)(ref unsupportedMaterials, ref materialIDArrays, ref rendererGroups, ref unsupportedRenderers);
						return;
					}
				}
				FindUnsupportedRenderers_0024BurstManaged(in unsupportedMaterials, in materialIDArrays, in rendererGroups, ref unsupportedRenderers);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void GetMaterialsWithChangedPackedMaterial_000000EC_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDatas, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>.ReadOnly packedMaterialHash, ref global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId> filteredMaterials);

		internal static class GetMaterialsWithChangedPackedMaterial_000000EC_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.GPUResidentDrawerBurst.GetMaterialsWithChangedPackedMaterial_000000EC_0024PostfixBurstDelegate>(GetMaterialsWithChangedPackedMaterial).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDatas, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>.ReadOnly packedMaterialHash, ref global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId> filteredMaterials)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>.ReadOnly, ref global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId>, void>)functionPointer)(ref materialIDs, ref packedMaterialDatas, ref packedMaterialHash, ref filteredMaterials);
						return;
					}
				}
				GetMaterialsWithChangedPackedMaterial_0024BurstManaged(in materialIDs, in packedMaterialDatas, in packedMaterialHash, ref filteredMaterials);
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EClassifyMaterials_000000EA_0024PostfixBurstDelegate))]
		public static void ClassifyMaterials(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID>.ReadOnly batchMaterialHash, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> supportedMaterialIDs, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedMaterialIDs, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> supportedPackedMaterialDatas)
		{
			global::UnityEngine.Rendering.GPUResidentDrawerBurst.ClassifyMaterials_000000EA_0024BurstDirectCall.Invoke(in materialIDs, in batchMaterialHash, ref supportedMaterialIDs, ref unsupportedMaterialIDs, ref supportedPackedMaterialDatas);
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EFindUnsupportedRenderers_000000EB_0024PostfixBurstDelegate))]
		public static void FindUnsupportedRenderers(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> unsupportedMaterials, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray>.ReadOnly materialIDArrays, in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroups, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedRenderers)
		{
			global::UnityEngine.Rendering.GPUResidentDrawerBurst.FindUnsupportedRenderers_000000EB_0024BurstDirectCall.Invoke(in unsupportedMaterials, in materialIDArrays, in rendererGroups, ref unsupportedRenderers);
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EGetMaterialsWithChangedPackedMaterial_000000EC_0024PostfixBurstDelegate))]
		public static void GetMaterialsWithChangedPackedMaterial(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDatas, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>.ReadOnly packedMaterialHash, ref global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId> filteredMaterials)
		{
			global::UnityEngine.Rendering.GPUResidentDrawerBurst.GetMaterialsWithChangedPackedMaterial_000000EC_0024BurstDirectCall.Invoke(in materialIDs, in packedMaterialDatas, in packedMaterialHash, ref filteredMaterials);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal static void ClassifyMaterials_0024BurstManaged(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID>.ReadOnly batchMaterialHash, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> supportedMaterialIDs, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedMaterialIDs, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> supportedPackedMaterialDatas)
		{
			global::Unity.Collections.NativeList<global::UnityEngine.EntityId> nativeList = new global::Unity.Collections.NativeList<global::UnityEngine.EntityId>(4, global::Unity.Collections.Allocator.Temp);
			foreach (global::UnityEngine.EntityId materialID in materialIDs)
			{
				global::UnityEngine.EntityId value = materialID;
				if (batchMaterialHash.ContainsKey(value))
				{
					nativeList.Add(in value);
				}
			}
			if (nativeList.IsEmpty)
			{
				nativeList.Dispose();
				return;
			}
			unsupportedMaterialIDs.Resize(nativeList.Length, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			supportedMaterialIDs.Resize(nativeList.Length, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			supportedPackedMaterialDatas.Resize(nativeList.Length, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int num = global::UnityEngine.Rendering.GPUDrivenProcessor.ClassifyMaterials(nativeList.AsArray(), unsupportedMaterialIDs.AsArray(), supportedMaterialIDs.AsArray(), supportedPackedMaterialDatas.AsArray());
			unsupportedMaterialIDs.Resize(num, global::Unity.Collections.NativeArrayOptions.ClearMemory);
			supportedMaterialIDs.Resize(nativeList.Length - num, global::Unity.Collections.NativeArrayOptions.ClearMemory);
			supportedPackedMaterialDatas.Resize(supportedMaterialIDs.Length, global::Unity.Collections.NativeArrayOptions.ClearMemory);
			nativeList.Dispose();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal static void FindUnsupportedRenderers_0024BurstManaged(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> unsupportedMaterials, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray>.ReadOnly materialIDArrays, in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroups, ref global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedRenderers)
		{
			for (int i = 0; i < materialIDArrays.Length; i++)
			{
				global::UnityEngine.Rendering.SmallEntityIdArray smallEntityIdArray = materialIDArrays[i];
				global::UnityEngine.EntityId value = rendererGroups[i];
				for (int j = 0; j < smallEntityIdArray.Length; j++)
				{
					global::UnityEngine.EntityId value2 = smallEntityIdArray[j];
					if (global::Unity.Collections.NativeArrayExtensions.Contains(unsupportedMaterials, value2))
					{
						unsupportedRenderers.Add(in value);
						break;
					}
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal static void GetMaterialsWithChangedPackedMaterial_0024BurstManaged(in global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materialIDs, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDatas, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>.ReadOnly packedMaterialHash, ref global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId> filteredMaterials)
		{
			for (int i = 0; i < materialIDs.Length; i++)
			{
				global::UnityEngine.EntityId entityId = materialIDs[i];
				global::UnityEngine.Rendering.GPUDrivenPackedMaterialData other = packedMaterialDatas[i];
				if (!packedMaterialHash.TryGetValue(entityId, out var item) || !item.Equals(other))
				{
					filteredMaterials.Add(entityId);
				}
			}
		}
	}
}
