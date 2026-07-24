namespace UnityEngine.Rendering
{
	public class GPUResidentDrawer
	{
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct FindRenderersFromMaterialOrMeshJob : global::Unity.Jobs.IJobParallelForBatch
		{
			public const int k_BatchSize = 128;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId>.ReadOnly materialIDs;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray>.ReadOnly materialIDArrays;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly meshIDs;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly meshIDArray;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroupIDs;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly sortedExcludeRendererIDs;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeList<global::UnityEngine.EntityId>.ParallelWriter selectedRenderGroupsForMaterials;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeList<global::UnityEngine.EntityId>.ParallelWriter selectedRenderGroupsForMeshes;

			public unsafe void Execute(int startIndex, int count)
			{
				int* ptr = stackalloc int[128];
				global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int> unsafeList = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int>(ptr, 128);
				unsafeList.Length = 0;
				int* ptr2 = stackalloc int[128];
				global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int> unsafeList2 = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int>(ptr2, 128);
				unsafeList2.Length = 0;
				for (int i = 0; i < count; i++)
				{
					int index = startIndex + i;
					global::UnityEngine.EntityId entityId = rendererGroupIDs[index];
					if (global::Unity.Collections.NativeSortExtension.BinarySearch(sortedExcludeRendererIDs, entityId) >= 0)
					{
						continue;
					}
					global::UnityEngine.EntityId value = meshIDArray[index];
					if (global::Unity.Collections.NativeArrayExtensions.Contains(meshIDs, value))
					{
						unsafeList2.AddNoResize(entityId);
						continue;
					}
					global::UnityEngine.Rendering.SmallEntityIdArray smallEntityIdArray = materialIDArrays[index];
					for (int j = 0; j < smallEntityIdArray.Length; j++)
					{
						global::UnityEngine.EntityId item = smallEntityIdArray[j];
						if (materialIDs.Contains(item))
						{
							unsafeList.AddNoResize(entityId);
							break;
						}
					}
				}
				selectedRenderGroupsForMaterials.AddRangeNoResize(ptr, unsafeList.Length);
				selectedRenderGroupsForMeshes.AddRangeNoResize(ptr2, unsafeList2.Length);
			}
		}

		private static class Strings
		{
			public static readonly string drawerModeDisabled = "GPUResidentDrawer Drawer mode is disabled. Enable it on your current RenderPipelineAsset";

			public static readonly string allowInEditModeDisabled = "GPUResidentDrawer The current mode does not allow the resident drawer. Check setting Allow In Edit Mode";

			public static readonly string notGPUResidentRenderPipeline = "GPUResidentDrawer Disabled due to current render pipeline not being of type IGPUResidentRenderPipeline";

			public static readonly string rawBufferNotSupportedByPlatform = string.Format("{0} The current platform does not support {1}", "GPUResidentDrawer", global::UnityEngine.Rendering.BatchBufferTarget.RawBuffer.GetType());

			public static readonly string kernelNotPresent = "GPUResidentDrawer Kernel not present, please ensure the player settings includes a supported graphics API.";

			public static readonly string batchRendererGroupShaderStrippingModeInvalid = "GPUResidentDrawer \"BatchRendererGroup Variants\" setting must be \"Keep All\".  The current setting will cause errors when building a player because all DOTS instancing shaders will be stripped To fix, modify Graphics settings and set \"BatchRendererGroup Variants\" to \"Keep All\".";

			public static readonly string visionOSNotSupported = "GPUResidentDrawer Disabled on VisionOS as it is non applicable. This platform uses a custom rendering path and doesn't go through the resident drawer.";
		}

		private static global::UnityEngine.Rendering.GPUResidentDrawer s_Instance;

		private global::System.IntPtr m_ContextIntPtr = global::System.IntPtr.Zero;

		private global::UnityEngine.Rendering.GPUResidentDrawerSettings m_Settings;

		private global::UnityEngine.Rendering.GPUDrivenProcessor m_GPUDrivenProcessor;

		private global::UnityEngine.Rendering.RenderersBatchersContext m_BatchersContext;

		private global::UnityEngine.Rendering.GPUResidentBatcher m_Batcher;

		private global::UnityEngine.ObjectDispatcher m_Dispatcher;

		internal static global::UnityEngine.Rendering.GPUResidentDrawer instance => s_Instance;

		internal static bool MaintainContext { get; set; }

		internal static bool ForceOcclusion { get; set; }

		internal global::UnityEngine.Rendering.GPUResidentBatcher batcher => m_Batcher;

		internal global::UnityEngine.Rendering.GPUResidentDrawerSettings settings => m_Settings;

		public static bool IsInstanceOcclusionCullingEnabled()
		{
			if (s_Instance == null)
			{
				return false;
			}
			if (s_Instance.settings.mode != global::UnityEngine.Rendering.GPUResidentDrawerMode.InstancedDrawing)
			{
				return false;
			}
			if (s_Instance.settings.enableOcclusionCulling)
			{
				return true;
			}
			return false;
		}

		public static void PostCullBeginCameraRendering(global::UnityEngine.Rendering.RenderRequestBatcherContext context)
		{
			s_Instance?.batcher.PostCullBeginCameraRendering(context);
		}

		public static void OnSetupAmbientProbe()
		{
			s_Instance?.batcher.OnSetupAmbientProbe();
		}

		public static void InstanceOcclusionTest(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.OcclusionCullingSettings settings, global::System.ReadOnlySpan<global::UnityEngine.Rendering.SubviewOcclusionTest> subviewOcclusionTests)
		{
			s_Instance?.batcher.InstanceOcclusionTest(renderGraph, in settings, subviewOcclusionTests);
		}

		public static void UpdateInstanceOccluders(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.OccluderParameters occluderParameters, global::System.ReadOnlySpan<global::UnityEngine.Rendering.OccluderSubviewUpdate> occluderSubviewUpdates)
		{
			s_Instance?.batcher.UpdateInstanceOccluders(renderGraph, in occluderParameters, occluderSubviewUpdates);
		}

		public static void ReinitializeIfNeeded()
		{
		}

		public static void RenderDebugOcclusionTestOverlay(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer debugSettings, int viewInstanceID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorBuffer)
		{
			s_Instance?.batcher.occlusionCullingCommon.RenderDebugOcclusionTestOverlay(renderGraph, debugSettings, viewInstanceID, in colorBuffer);
		}

		public static void RenderDebugOccluderOverlay(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer debugSettings, global::UnityEngine.Vector2 screenPos, float maxHeight, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorBuffer)
		{
			s_Instance?.batcher.occlusionCullingCommon.RenderDebugOccluderOverlay(renderGraph, debugSettings, screenPos, maxHeight, in colorBuffer);
		}

		internal static global::UnityEngine.Rendering.DebugRendererBatcherStats GetDebugStats()
		{
			return s_Instance?.m_BatchersContext.debugStats;
		}

		private void InsertIntoPlayerLoop()
		{
			global::UnityEngine.LowLevel.PlayerLoopSystem currentPlayerLoop = global::UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();
			bool flag = false;
			for (int i = 0; i < currentPlayerLoop.subSystemList.Length; i++)
			{
				global::UnityEngine.LowLevel.PlayerLoopSystem playerLoopSystem = currentPlayerLoop.subSystemList[i];
				if (flag || !(playerLoopSystem.type == typeof(global::UnityEngine.PlayerLoop.PostLateUpdate)))
				{
					continue;
				}
				global::System.Collections.Generic.List<global::UnityEngine.LowLevel.PlayerLoopSystem> list = new global::System.Collections.Generic.List<global::UnityEngine.LowLevel.PlayerLoopSystem>();
				global::UnityEngine.LowLevel.PlayerLoopSystem[] subSystemList = playerLoopSystem.subSystemList;
				for (int j = 0; j < subSystemList.Length; j++)
				{
					global::UnityEngine.LowLevel.PlayerLoopSystem item = subSystemList[j];
					if (item.type == typeof(global::UnityEngine.PlayerLoop.PostLateUpdate.FinishFrameRendering))
					{
						global::UnityEngine.LowLevel.PlayerLoopSystem item2 = default(global::UnityEngine.LowLevel.PlayerLoopSystem);
						ref global::UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction updateDelegate = ref item2.updateDelegate;
						updateDelegate = (global::UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction)global::System.Delegate.Combine(updateDelegate, new global::UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction(PostPostLateUpdateStatic));
						item2.type = GetType();
						list.Add(item2);
						flag = true;
					}
					list.Add(item);
				}
				playerLoopSystem.subSystemList = list.ToArray();
				currentPlayerLoop.subSystemList[i] = playerLoopSystem;
			}
			global::UnityEngine.LowLevel.PlayerLoop.SetPlayerLoop(currentPlayerLoop);
		}

		private void RemoveFromPlayerLoop()
		{
			global::UnityEngine.LowLevel.PlayerLoopSystem currentPlayerLoop = global::UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();
			for (int i = 0; i < currentPlayerLoop.subSystemList.Length; i++)
			{
				global::UnityEngine.LowLevel.PlayerLoopSystem playerLoopSystem = currentPlayerLoop.subSystemList[i];
				if (playerLoopSystem.type != typeof(global::UnityEngine.PlayerLoop.PostLateUpdate))
				{
					continue;
				}
				global::System.Collections.Generic.List<global::UnityEngine.LowLevel.PlayerLoopSystem> list = new global::System.Collections.Generic.List<global::UnityEngine.LowLevel.PlayerLoopSystem>();
				global::UnityEngine.LowLevel.PlayerLoopSystem[] subSystemList = playerLoopSystem.subSystemList;
				for (int j = 0; j < subSystemList.Length; j++)
				{
					global::UnityEngine.LowLevel.PlayerLoopSystem item = subSystemList[j];
					if (item.type != GetType())
					{
						list.Add(item);
					}
				}
				playerLoopSystem.subSystemList = list.ToArray();
				currentPlayerLoop.subSystemList[i] = playerLoopSystem;
			}
			global::UnityEngine.LowLevel.PlayerLoop.SetPlayerLoop(currentPlayerLoop);
		}

		internal static bool IsEnabled()
		{
			return s_Instance != null;
		}

		internal static global::UnityEngine.Rendering.GPUResidentDrawerSettings GetGlobalSettingsFromRPAsset()
		{
			if (!(global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline is global::UnityEngine.Rendering.IGPUResidentRenderPipeline { gpuResidentDrawerSettings: var gpuResidentDrawerSettings }))
			{
				return default(global::UnityEngine.Rendering.GPUResidentDrawerSettings);
			}
			if (IsForcedOnViaCommandLine())
			{
				gpuResidentDrawerSettings.mode = global::UnityEngine.Rendering.GPUResidentDrawerMode.InstancedDrawing;
			}
			if (IsOcclusionForcedOnViaCommandLine() || ForceOcclusion)
			{
				gpuResidentDrawerSettings.enableOcclusionCulling = true;
			}
			return gpuResidentDrawerSettings;
		}

		internal static bool IsForcedOnViaCommandLine()
		{
			return false;
		}

		internal static bool IsOcclusionForcedOnViaCommandLine()
		{
			return false;
		}

		internal static void Reinitialize()
		{
			Recreate(GetGlobalSettingsFromRPAsset());
		}

		private static void CleanUp()
		{
			if (s_Instance != null)
			{
				s_Instance.Dispose();
				s_Instance = null;
			}
		}

		private static void Recreate(global::UnityEngine.Rendering.GPUResidentDrawerSettings settings)
		{
			CleanUp();
			if (IsGPUResidentDrawerSupportedBySRP(settings, out var message, out var severity))
			{
				s_Instance = new global::UnityEngine.Rendering.GPUResidentDrawer(settings, 4096, 0);
			}
			else
			{
				LogMessage(message, severity);
			}
		}

		private GPUResidentDrawer(global::UnityEngine.Rendering.GPUResidentDrawerSettings settings, int maxInstanceCount, int maxTreeInstanceCount)
		{
			global::UnityEngine.Rendering.GPUResidentDrawerResources renderPipelineSettings = global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.GPUResidentDrawerResources>();
			_ = global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline;
			m_Settings = settings;
			global::UnityEngine.Rendering.RenderersBatchersContextDesc desc = global::UnityEngine.Rendering.RenderersBatchersContextDesc.NewDefault();
			desc.instanceNumInfo = new global::UnityEngine.Rendering.InstanceNumInfo(maxInstanceCount, maxTreeInstanceCount);
			desc.supportDitheringCrossFade = settings.supportDitheringCrossFade;
			desc.smallMeshScreenPercentage = settings.smallMeshScreenPercentage;
			desc.enableBoundingSpheresInstanceData = settings.enableOcclusionCulling;
			desc.enableCullerDebugStats = true;
			global::UnityEngine.Rendering.InstanceCullingBatcherDesc instanceCullerBatcherDesc = global::UnityEngine.Rendering.InstanceCullingBatcherDesc.NewDefault();
			m_GPUDrivenProcessor = new global::UnityEngine.Rendering.GPUDrivenProcessor();
			m_BatchersContext = new global::UnityEngine.Rendering.RenderersBatchersContext(in desc, m_GPUDrivenProcessor, renderPipelineSettings);
			m_Batcher = new global::UnityEngine.Rendering.GPUResidentBatcher(m_BatchersContext, instanceCullerBatcherDesc, m_GPUDrivenProcessor);
			m_Dispatcher = new global::UnityEngine.ObjectDispatcher();
			m_Dispatcher.EnableTypeTracking<global::UnityEngine.LODGroup>(global::UnityEngine.ObjectDispatcher.TypeTrackingFlags.SceneObjects);
			m_Dispatcher.EnableTypeTracking<global::UnityEngine.Mesh>();
			m_Dispatcher.EnableTypeTracking<global::UnityEngine.Material>();
			m_Dispatcher.EnableTypeTracking<global::UnityEngine.MeshRenderer>(global::UnityEngine.ObjectDispatcher.TypeTrackingFlags.SceneObjects);
			m_Dispatcher.EnableTypeTracking<global::UnityEngine.Camera>(global::UnityEngine.ObjectDispatcher.TypeTrackingFlags.SceneObjects | global::UnityEngine.ObjectDispatcher.TypeTrackingFlags.EditorOnlyObjects);
			m_Dispatcher.EnableTransformTracking<global::UnityEngine.MeshRenderer>(global::UnityEngine.ObjectDispatcher.TransformTrackingType.GlobalTRS);
			m_Dispatcher.EnableTransformTracking<global::UnityEngine.LODGroup>(global::UnityEngine.ObjectDispatcher.TransformTrackingType.GlobalTRS);
			global::UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
			global::UnityEngine.Rendering.RenderPipelineManager.beginContextRendering += OnBeginContextRendering;
			global::UnityEngine.Rendering.RenderPipelineManager.endContextRendering += OnEndContextRendering;
			global::UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
			global::UnityEngine.Rendering.RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
			global::UnityEngine.Shader.EnableKeyword("USE_LEGACY_LIGHTMAPS");
			InsertIntoPlayerLoop();
		}

		private void Dispose()
		{
			global::UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
			global::UnityEngine.Rendering.RenderPipelineManager.beginContextRendering -= OnBeginContextRendering;
			global::UnityEngine.Rendering.RenderPipelineManager.endContextRendering -= OnEndContextRendering;
			global::UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
			global::UnityEngine.Rendering.RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
			RemoveFromPlayerLoop();
			global::UnityEngine.Shader.DisableKeyword("USE_LEGACY_LIGHTMAPS");
			m_Dispatcher.Dispose();
			m_Dispatcher = null;
			s_Instance = null;
			m_Batcher?.Dispose();
			m_BatchersContext.Dispose();
			m_GPUDrivenProcessor.Dispose();
			m_ContextIntPtr = global::System.IntPtr.Zero;
		}

		private void OnSceneLoaded(global::UnityEngine.SceneManagement.Scene scene, global::UnityEngine.SceneManagement.LoadSceneMode mode)
		{
			if (mode == global::UnityEngine.SceneManagement.LoadSceneMode.Additive)
			{
				m_BatchersContext.UpdateAmbientProbeAndGpuBuffer(forceUpdate: true);
			}
		}

		private static void PostPostLateUpdateStatic()
		{
			s_Instance?.PostPostLateUpdate();
		}

		private void OnBeginContextRendering(global::UnityEngine.Rendering.ScriptableRenderContext context, global::System.Collections.Generic.List<global::UnityEngine.Camera> cameras)
		{
			if (s_Instance != null && m_ContextIntPtr == global::System.IntPtr.Zero)
			{
				m_ContextIntPtr = context.Internal_GetPtr();
				m_Batcher.OnBeginContextRendering();
			}
		}

		private void OnEndContextRendering(global::UnityEngine.Rendering.ScriptableRenderContext context, global::System.Collections.Generic.List<global::UnityEngine.Camera> cameras)
		{
			if (s_Instance != null && m_ContextIntPtr == context.Internal_GetPtr())
			{
				m_ContextIntPtr = global::System.IntPtr.Zero;
				m_Batcher.OnEndContextRendering();
			}
		}

		private void OnBeginCameraRendering(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera camera)
		{
			m_Batcher.OnBeginCameraRendering(camera);
		}

		private void OnEndCameraRendering(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera camera)
		{
			m_Batcher.OnEndCameraRendering(camera);
		}

		private void PostPostLateUpdate()
		{
			m_BatchersContext.UpdateAmbientProbeAndGpuBuffer(forceUpdate: false);
			global::UnityEngine.TransformDispatchData transformChangesAndClear = m_Dispatcher.GetTransformChangesAndClear<global::UnityEngine.LODGroup>(global::UnityEngine.ObjectDispatcher.TransformTrackingType.GlobalTRS, global::Unity.Collections.Allocator.TempJob);
			global::UnityEngine.TypeDispatchData typeChangesAndClear = m_Dispatcher.GetTypeChangesAndClear<global::UnityEngine.LODGroup>(global::Unity.Collections.Allocator.TempJob, sortByInstanceID: false, noScriptingArray: true);
			global::UnityEngine.TypeDispatchData typeChangesAndClear2 = m_Dispatcher.GetTypeChangesAndClear<global::UnityEngine.Mesh>(global::Unity.Collections.Allocator.TempJob, sortByInstanceID: true, noScriptingArray: true);
			global::UnityEngine.TypeDispatchData typeChangesAndClear3 = m_Dispatcher.GetTypeChangesAndClear<global::UnityEngine.Camera>(global::Unity.Collections.Allocator.TempJob, sortByInstanceID: false, noScriptingArray: true);
			global::UnityEngine.TypeDispatchData typeChangesAndClear4 = m_Dispatcher.GetTypeChangesAndClear<global::UnityEngine.Material>(global::Unity.Collections.Allocator.TempJob, sortByInstanceID: false, noScriptingArray: true);
			global::UnityEngine.TypeDispatchData typeChangesAndClear5 = m_Dispatcher.GetTypeChangesAndClear<global::UnityEngine.MeshRenderer>(global::Unity.Collections.Allocator.TempJob, sortByInstanceID: false, noScriptingArray: true);
			ClassifyMaterials(typeChangesAndClear4.changedID, out var unsupportedMaterials, out var supportedMaterials, out var supportedPackedMaterialDatas, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Collections.NativeList<global::UnityEngine.EntityId> nativeList = FindUnsupportedRenderers(unsupportedMaterials.AsArray());
			ProcessMaterials(typeChangesAndClear4.destroyedID, unsupportedMaterials.AsArray());
			ProcessMeshes(typeChangesAndClear2.destroyedID);
			ProcessLODGroups(typeChangesAndClear.changedID, typeChangesAndClear.destroyedID, transformChangesAndClear.transformedID);
			ProcessCameras(typeChangesAndClear3.changedID, typeChangesAndClear3.destroyedID);
			ProcessRenderers(typeChangesAndClear5, nativeList.AsArray());
			ProcessRendererMaterialAndMeshChanges(typeChangesAndClear5.changedID, supportedMaterials.AsArray(), supportedPackedMaterialDatas.AsArray(), typeChangesAndClear2.changedID);
			transformChangesAndClear.Dispose();
			typeChangesAndClear.Dispose();
			typeChangesAndClear2.Dispose();
			typeChangesAndClear4.Dispose();
			typeChangesAndClear3.Dispose();
			typeChangesAndClear5.Dispose();
			unsupportedMaterials.Dispose();
			nativeList.Dispose();
			supportedMaterials.Dispose();
			supportedPackedMaterialDatas.Dispose();
			m_BatchersContext.UpdateInstanceMotions();
			m_Batcher.UpdateFrame();
		}

		private void ProcessMaterials(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedID, global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> unsupportedMaterials)
		{
			if (destroyedID.Length > 0)
			{
				m_Batcher.DestroyMaterials(destroyedID);
			}
			if (unsupportedMaterials.Length > 0)
			{
				m_Batcher.DestroyMaterials(unsupportedMaterials);
			}
		}

		private void ProcessCameras(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> changedIDs, global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedIDs)
		{
			m_BatchersContext.UpdateCameras(changedIDs);
			m_BatchersContext.FreePerCameraInstanceData(destroyedIDs);
		}

		private void ProcessMeshes(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyedID)
		{
			if (destroyedID.Length != 0)
			{
				global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle>(global::Unity.Collections.Allocator.TempJob);
				ScheduleQueryMeshInstancesJob(destroyedID, instances).Complete();
				m_Batcher.DestroyDrawInstances(instances.AsArray());
				instances.Dispose();
				m_Batcher.DestroyMeshes(destroyedID);
			}
		}

		private void ProcessLODGroups(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> changedID, global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> destroyed, global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> transformedID)
		{
			m_BatchersContext.DestroyLODGroups(destroyed);
			m_BatchersContext.UpdateLODGroups(changedID);
			m_BatchersContext.TransformLODGroups(transformedID);
		}

		private void ProcessRendererMaterialAndMeshChanges(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> excludedRenderers, global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> changedMaterials, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> changedPackedMaterialDatas, global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> changedMeshes)
		{
			if (changedMaterials.Length == 0 && changedMeshes.Length == 0)
			{
				return;
			}
			global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId> materialsWithChangedPackedMaterial = GetMaterialsWithChangedPackedMaterial(changedMaterials, changedPackedMaterialDatas, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Jobs.JobHandle jobHandle = m_Batcher.SchedulePackedMaterialCacheUpdate(changedMaterials, changedPackedMaterialDatas);
			if (materialsWithChangedPackedMaterial.Count == 0 && changedMeshes.Length == 0)
			{
				materialsWithChangedPackedMaterial.Dispose();
				jobHandle.Complete();
				return;
			}
			global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>(excludedRenderers, global::Unity.Collections.Allocator.TempJob);
			if (nativeArray.Length > 0)
			{
				global::Unity.Collections.NativeSortExtension.SortJob(nativeArray).Schedule().Complete();
			}
			var (nativeList, nativeList2) = FindRenderersFromMaterialsOrMeshes(nativeArray, materialsWithChangedPackedMaterial, changedMeshes, global::Unity.Collections.Allocator.TempJob);
			materialsWithChangedPackedMaterial.Dispose();
			nativeArray.Dispose();
			jobHandle.Complete();
			if (nativeList.Length == 0 && nativeList2.Length == 0)
			{
				nativeList.Dispose();
				nativeList2.Dispose();
				return;
			}
			int length = nativeList.Length;
			int length2 = nativeList2.Length;
			int length3 = length + length2;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(length3, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> nativeArray2 = new global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>(length3, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.Copy(nativeList.AsArray(), nativeArray2, length);
			global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.Copy(nativeList2.AsArray(), nativeArray2.GetSubArray(length, length2), length2);
			ScheduleQueryRendererGroupInstancesJob(nativeArray2, instances).Complete();
			m_Batcher.DestroyDrawInstances(instances);
			m_Batcher.UpdateRenderers(nativeList.AsArray(), materialUpdateOnly: true);
			m_Batcher.UpdateRenderers(nativeList2.AsArray());
			instances.Dispose();
			nativeArray2.Dispose();
			nativeList.Dispose();
			nativeList2.Dispose();
		}

		private void ProcessRenderers(global::UnityEngine.TypeDispatchData rendererChanges, global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> unsupportedRenderers)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(rendererChanges.changedID.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			ScheduleQueryRendererGroupInstancesJob(rendererChanges.changedID, instances).Complete();
			m_Batcher.DestroyDrawInstances(instances);
			instances.Dispose();
			m_Batcher.UpdateRenderers(rendererChanges.changedID);
			FreeRendererGroupInstances(rendererChanges.destroyedID, unsupportedRenderers);
			global::UnityEngine.TransformDispatchData transformChangesAndClear = m_Dispatcher.GetTransformChangesAndClear<global::UnityEngine.MeshRenderer>(global::UnityEngine.ObjectDispatcher.TransformTrackingType.GlobalTRS, global::Unity.Collections.Allocator.TempJob);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances2 = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(transformChangesAndClear.transformedID.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			ScheduleQueryRendererGroupInstancesJob(transformChangesAndClear.transformedID, instances2).Complete();
			TransformInstances(instances2, transformChangesAndClear.localToWorldMatrices);
			instances2.Dispose();
			transformChangesAndClear.Dispose();
		}

		private void TransformInstances(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> localToWorldMatrices)
		{
			m_BatchersContext.UpdateInstanceTransforms(instances, localToWorldMatrices);
		}

		private void FreeInstances(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			m_Batcher.DestroyDrawInstances(instances);
			m_BatchersContext.FreeInstances(instances);
		}

		private void FreeRendererGroupInstances(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> unsupportedRendererGroupIDs)
		{
			m_Batcher.FreeRendererGroupInstances(rendererGroupIDs);
			if (unsupportedRendererGroupIDs.Length > 0)
			{
				m_Batcher.FreeRendererGroupInstances(unsupportedRendererGroupIDs);
				m_GPUDrivenProcessor.DisableGPUDrivenRendering(unsupportedRendererGroupIDs);
			}
		}

		private global::UnityEngine.Rendering.InstanceHandle AppendNewInstance(int rendererGroupID, in global::UnityEngine.Matrix4x4 instanceTransform)
		{
			throw new global::System.NotImplementedException();
		}

		private global::Unity.Jobs.JobHandle ScheduleQueryRendererGroupInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			return m_BatchersContext.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances);
		}

		private global::Unity.Jobs.JobHandle ScheduleQueryRendererGroupInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			return m_BatchersContext.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances);
		}

		private global::Unity.Jobs.JobHandle ScheduleQueryRendererGroupInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs, global::Unity.Collections.NativeArray<int> instancesOffset, global::Unity.Collections.NativeArray<int> instancesCount, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			return m_BatchersContext.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instancesOffset, instancesCount, instances);
		}

		private global::Unity.Jobs.JobHandle ScheduleQueryMeshInstancesJob(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> sortedMeshIDs, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceHandle> instances)
		{
			return m_BatchersContext.ScheduleQueryMeshInstancesJob(sortedMeshIDs, instances);
		}

		private void ClassifyMaterials(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materials, out global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedMaterials, out global::Unity.Collections.NativeList<global::UnityEngine.EntityId> supportedMaterials, out global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> supportedPackedMaterialDatas, global::Unity.Collections.Allocator allocator)
		{
			supportedMaterials = new global::Unity.Collections.NativeList<global::UnityEngine.EntityId>(materials.Length, allocator);
			unsupportedMaterials = new global::Unity.Collections.NativeList<global::UnityEngine.EntityId>(materials.Length, allocator);
			supportedPackedMaterialDatas = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>(materials.Length, allocator);
			if (materials.Length > 0)
			{
				global::UnityEngine.Rendering.GPUResidentDrawerBurst.ClassifyMaterials(in materials, m_Batcher.instanceCullingBatcher.batchMaterialHash.AsReadOnly(), ref supportedMaterials, ref unsupportedMaterials, ref supportedPackedMaterialDatas);
			}
		}

		private global::Unity.Collections.NativeList<global::UnityEngine.EntityId> FindUnsupportedRenderers(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> unsupportedMaterials)
		{
			global::Unity.Collections.NativeList<global::UnityEngine.EntityId> unsupportedRenderers = new global::Unity.Collections.NativeList<global::UnityEngine.EntityId>(global::Unity.Collections.Allocator.TempJob);
			if (unsupportedMaterials.Length > 0)
			{
				global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly sharedInstanceData = m_BatchersContext.sharedInstanceData;
				ref readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray>.ReadOnly materialIDArrays = ref sharedInstanceData.materialIDArrays;
				global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly sharedInstanceData2 = m_BatchersContext.sharedInstanceData;
				global::UnityEngine.Rendering.GPUResidentDrawerBurst.FindUnsupportedRenderers(in unsupportedMaterials, in materialIDArrays, in sharedInstanceData2.rendererGroupIDs, ref unsupportedRenderers);
			}
			return unsupportedRenderers;
		}

		private global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId> GetMaterialsWithChangedPackedMaterial(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> materials, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDatas, global::Unity.Collections.Allocator allocator)
		{
			global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId> filteredMaterials = new global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId>(materials.Length, allocator);
			global::UnityEngine.Rendering.GPUResidentDrawerBurst.GetMaterialsWithChangedPackedMaterial(in materials, in packedMaterialDatas, batcher.instanceCullingBatcher.packedMaterialHash.AsReadOnly(), ref filteredMaterials);
			return filteredMaterials;
		}

		private (global::Unity.Collections.NativeList<global::UnityEngine.EntityId> renderersWithMaterials, global::Unity.Collections.NativeList<global::UnityEngine.EntityId> renderersWithMeshes) FindRenderersFromMaterialsOrMeshes(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> sortedExcludeRenderers, global::Unity.Collections.NativeHashSet<global::UnityEngine.EntityId> materials, global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> meshes, global::Unity.Collections.Allocator rendererListAllocator)
		{
			global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly sharedInstanceData = m_BatchersContext.sharedInstanceData;
			global::Unity.Collections.NativeList<global::UnityEngine.EntityId> item = new global::Unity.Collections.NativeList<global::UnityEngine.EntityId>(sharedInstanceData.rendererGroupIDs.Length, rendererListAllocator);
			global::Unity.Collections.NativeList<global::UnityEngine.EntityId> item2 = new global::Unity.Collections.NativeList<global::UnityEngine.EntityId>(sharedInstanceData.rendererGroupIDs.Length, rendererListAllocator);
			global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.GPUResidentDrawer.FindRenderersFromMaterialOrMeshJob
			{
				materialIDs = materials.AsReadOnly(),
				materialIDArrays = sharedInstanceData.materialIDArrays,
				meshIDs = meshes.AsReadOnly(),
				meshIDArray = sharedInstanceData.meshIDs,
				rendererGroupIDs = sharedInstanceData.rendererGroupIDs,
				sortedExcludeRendererIDs = sortedExcludeRenderers.AsReadOnly(),
				selectedRenderGroupsForMaterials = item.AsParallelWriter(),
				selectedRenderGroupsForMeshes = item2.AsParallelWriter()
			}, sharedInstanceData.rendererGroupIDs.Length, 128).Complete();
			return (renderersWithMaterials: item, renderersWithMeshes: item2);
		}

		internal static bool IsProjectSupported()
		{
			string message;
			global::UnityEngine.LogType severity;
			return IsProjectSupported(out message, out severity);
		}

		internal static bool IsProjectSupported(out string message, out global::UnityEngine.LogType severity)
		{
			message = string.Empty;
			severity = global::UnityEngine.LogType.Log;
			if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.VisionOS)
			{
				message = global::UnityEngine.Rendering.GPUResidentDrawer.Strings.visionOSNotSupported;
				severity = global::UnityEngine.LogType.Log;
				return false;
			}
			if (global::UnityEngine.Rendering.BatchRendererGroup.BufferTarget != global::UnityEngine.Rendering.BatchBufferTarget.RawBuffer)
			{
				severity = global::UnityEngine.LogType.Warning;
				message = global::UnityEngine.Rendering.GPUResidentDrawer.Strings.rawBufferNotSupportedByPlatform;
				return false;
			}
			return true;
		}

		internal static bool IsGPUResidentDrawerSupportedBySRP(global::UnityEngine.Rendering.GPUResidentDrawerSettings settings, out string message, out global::UnityEngine.LogType severity)
		{
			message = string.Empty;
			severity = global::UnityEngine.LogType.Log;
			if (settings.mode == global::UnityEngine.Rendering.GPUResidentDrawerMode.Disabled)
			{
				message = global::UnityEngine.Rendering.GPUResidentDrawer.Strings.drawerModeDisabled;
				return false;
			}
			if (IsForcedOnViaCommandLine() || MaintainContext)
			{
				return true;
			}
			if (!(global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline is global::UnityEngine.Rendering.IGPUResidentRenderPipeline iGPUResidentRenderPipeline))
			{
				message = global::UnityEngine.Rendering.GPUResidentDrawer.Strings.notGPUResidentRenderPipeline;
				severity = global::UnityEngine.LogType.Warning;
				return false;
			}
			if (iGPUResidentRenderPipeline.IsGPUResidentDrawerSupportedBySRP(out message, out severity))
			{
				return IsProjectSupported(out message, out severity);
			}
			return false;
		}

		internal static void LogMessage(string message, global::UnityEngine.LogType severity)
		{
			switch (severity)
			{
			case global::UnityEngine.LogType.Error:
			case global::UnityEngine.LogType.Exception:
				global::UnityEngine.Debug.LogError(message);
				break;
			case global::UnityEngine.LogType.Warning:
				global::UnityEngine.Debug.LogWarning(message);
				break;
			case global::UnityEngine.LogType.Assert:
			case global::UnityEngine.LogType.Log:
				break;
			}
		}
	}
}
