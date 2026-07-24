namespace Unity.Jobs
{
	[global::System.Obsolete("'JobParallelIndexListExtensions' has been deprecated; Use 'IJobFilterExtensions' instead.", false)]
	public static class JobParallelIndexListExtensions
	{
		[global::System.Obsolete("The signature for 'ScheduleAppend' has changed. 'innerloopBatchCount' is no longer part of this API.", false)]
		public static global::Unity.Jobs.JobHandle ScheduleAppend<T>(this T jobData, global::Unity.Collections.NativeList<int> indices, int arrayLength, int innerloopBatchCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobFilter
		{
			return jobData.ScheduleAppend(indices, arrayLength, dependsOn);
		}

		[global::System.Obsolete("The signature for 'ScheduleFilter' has changed. 'innerloopBatchCount' is no longer part of this API.")]
		public static global::Unity.Jobs.JobHandle ScheduleFilter<T>(this T jobData, global::Unity.Collections.NativeList<int> indices, int innerloopBatchCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobFilter
		{
			return jobData.ScheduleFilter(indices, dependsOn);
		}
	}
}
