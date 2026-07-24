[global::Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__1652832624114795843
{
	public static void CreateJobReflectionData()
	{
		try
		{
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.CollectionHelper.DummyJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeBitArrayDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeHashMapDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeListDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeQueueDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeReferenceDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeRingQueueDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeStream.ConstructJobList>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeStream.ConstructJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeStreamDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.NativeTextDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.UnsafeQueueDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.LowLevel.Unsafe.UnsafeDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.DisposeJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.ConstructJobList>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.ConstructJob>();
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
