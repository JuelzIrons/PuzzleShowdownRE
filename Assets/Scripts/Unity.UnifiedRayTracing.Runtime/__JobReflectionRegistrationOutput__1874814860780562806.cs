[global::Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__1874814860780562806
{
	public static void CreateJobReflectionData()
	{
		try
		{
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.Rendering.UnifiedRayTracing.ComputeTerrainMeshJob>();
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
