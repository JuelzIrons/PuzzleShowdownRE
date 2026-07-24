[global::Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__2329194265940350739
{
	public static void CreateJobReflectionData()
	{
		try
		{
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.QoS.QosJob>();
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
