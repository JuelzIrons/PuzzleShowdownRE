[global::Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__12640072059193112690
{
	public static void CreateJobReflectionData()
	{
		try
		{
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.BoneTransformsChangeDetectionJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.PrepareDeformJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.BoneDeformBatchedJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.SkinDeformBatchedJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.FillPerSkinJobSingleThread>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.CopySpriteRendererBuffersJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.CopySpriteRendererBoneTransformBuffersJob>();
			global::UnityEngine.Jobs.IJobParallelForTransformExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.LocalToWorldAndChangeDetectionTransformAccessJob>();
			global::UnityEngine.Jobs.IJobParallelForTransformExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.WorldToLocalTransformAccessJob>();
			global::Unity.Jobs.IJobParallelForExtensions.EarlyJobInit<global::UnityEngine.U2D.Animation.UpdateBoundJob>();
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
