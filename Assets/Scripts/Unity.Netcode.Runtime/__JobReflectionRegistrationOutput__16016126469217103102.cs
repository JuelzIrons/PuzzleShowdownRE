[global::Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__16016126469217103102
{
	public static void CreateJobReflectionData()
	{
		try
		{
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Netcode.Transports.UTP.UnityTransport.SendBatchedMessagesJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::Unity.Netcode.Components.RigidbodyContactEventManager.GetCollisionsJob>();
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
