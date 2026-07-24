[global::Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__15867191014387474753
{
	public static void CreateJobReflectionData()
	{
		try
		{
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.GPUResidentDrawer.FindRenderersFromMaterialOrMeshJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.AnimateCrossFadeJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.CullingJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.AllocateBinsPerBatch>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::UnityEngine.Rendering.PrefixSumDrawsAndInstances>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.DrawCommandOutputPerBatch>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.CompactVisibilityMasksJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::UnityEngine.Rendering.PrefixSumDrawInstancesJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.BuildDrawListsJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.FindDrawInstancesJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.FindMaterialDrawInstancesJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.FindNonRegisteredMeshesJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.FindNonRegisteredMaterialsJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.RegisterNewMeshesJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.RegisterNewMaterialsJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::UnityEngine.Rendering.UpdatePackedMaterialDataCacheJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.GPUInstanceDataBuffer.ConvertCPUInstancesToGPUInstancesJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.WriteInstanceDataParameterJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.QueryRendererGroupInstancesCountJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.ComputeInstancesOffsetAndResizeInstancesArrayJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.QueryRendererGroupInstancesJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.QueryRendererGroupInstancesMultiJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.QuerySortedMeshInstancesJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.CalculateInterpolatedLightAndOcclusionProbesBatchJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.ScatterTetrahedronCacheIndicesJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.TransformUpdateJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.ProbesUpdateJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.MotionUpdateJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.UpdateRendererInstancesJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.CollectInstancesLODGroupsAndMasksJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.GetVisibleNonProcessedTreeInstancesJob>();
			global::Unity.Jobs.IJobParallelForBatchExtensions.EarlyJobInit<global::UnityEngine.Rendering.InstanceDataSystem.UpdateCompactedInstanceVisibilityJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.UpdateLODGroupTransformJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.UpdateLODGroupDataJob>();
			global::Unity.Jobs.IJobForExtensions.EarlyJobInit<global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortBucketCountJob>();
			global::Unity.Jobs.IJobForExtensions.EarlyJobInit<global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortBatchPrefixSumJob>();
			global::Unity.Jobs.IJobForExtensions.EarlyJobInit<global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortPrefixSumJob>();
			global::Unity.Jobs.IJobForExtensions.EarlyJobInit<global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortBucketSortJob>();
		}
		catch (global::System.Exception ex)
		{
			global::Unity.Jobs.EarlyInitHelpers.JobReflectionDataCreationFailed(ex);
		}
	}

	[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	public static void EarlyInit()
	{
		CreateJobReflectionData();
	}
}
