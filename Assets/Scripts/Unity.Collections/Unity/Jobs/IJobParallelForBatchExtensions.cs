namespace Unity.Jobs
{
	public static class IJobParallelForBatchExtensions
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct JobParallelForBatchProducer<T> where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			internal delegate void ExecuteJobFunction(ref T jobData, global::System.IntPtr additionalPtr, global::System.IntPtr bufferRangePatchData, ref global::Unity.Jobs.LowLevel.Unsafe.JobRanges ranges, int jobIndex);

			internal static readonly global::Unity.Burst.SharedStatic<global::System.IntPtr> jobReflectionData = global::Unity.Burst.SharedStatic<global::System.IntPtr>.GetOrCreate<global::Unity.Jobs.IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>>();

			[global::Unity.Burst.BurstDiscard]
			internal static void Initialize()
			{
				if (jobReflectionData.Data == global::System.IntPtr.Zero)
				{
					jobReflectionData.Data = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.CreateJobReflectionData(typeof(T), new global::Unity.Jobs.IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.ExecuteJobFunction(Execute));
				}
			}

			public static void Execute(ref T jobData, global::System.IntPtr additionalPtr, global::System.IntPtr bufferRangePatchData, ref global::Unity.Jobs.LowLevel.Unsafe.JobRanges ranges, int jobIndex)
			{
				int beginIndex;
				int endIndex;
				while (global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out beginIndex, out endIndex))
				{
					jobData.Execute(beginIndex, endIndex - beginIndex);
				}
			}
		}

		public static void EarlyJobInit<T>() where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			global::Unity.Jobs.IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.Initialize();
		}

		private static global::System.IntPtr GetReflectionData<T>() where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			global::Unity.Jobs.IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.Initialize();
			return global::Unity.Jobs.IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.jobReflectionData.Data;
		}

		public unsafe static global::Unity.Jobs.JobHandle Schedule<T>(this T jobData, int arrayLength, int indicesPerJobCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref jobData), GetReflectionData<T>(), dependsOn, global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Single);
			return global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ScheduleParallelFor(ref parameters, arrayLength, indicesPerJobCount);
		}

		public unsafe static global::Unity.Jobs.JobHandle ScheduleByRef<T>(this ref T jobData, int arrayLength, int indicesPerJobCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref jobData), GetReflectionData<T>(), dependsOn, global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Single);
			return global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ScheduleParallelFor(ref parameters, arrayLength, indicesPerJobCount);
		}

		public unsafe static global::Unity.Jobs.JobHandle ScheduleParallel<T>(this T jobData, int arrayLength, int indicesPerJobCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref jobData), GetReflectionData<T>(), dependsOn, global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Batched);
			return global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ScheduleParallelFor(ref parameters, arrayLength, indicesPerJobCount);
		}

		public unsafe static global::Unity.Jobs.JobHandle ScheduleParallelByRef<T>(this ref T jobData, int arrayLength, int indicesPerJobCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref jobData), GetReflectionData<T>(), dependsOn, global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Batched);
			return global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ScheduleParallelFor(ref parameters, arrayLength, indicesPerJobCount);
		}

		public static global::Unity.Jobs.JobHandle ScheduleBatch<T>(this T jobData, int arrayLength, int indicesPerJobCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			return ScheduleParallel(jobData, arrayLength, indicesPerJobCount, dependsOn);
		}

		public static global::Unity.Jobs.JobHandle ScheduleBatchByRef<T>(this ref T jobData, int arrayLength, int indicesPerJobCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			return ScheduleParallelByRef(ref jobData, arrayLength, indicesPerJobCount, dependsOn);
		}

		public unsafe static void Run<T>(this T jobData, int arrayLength, int indicesPerJobCount) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref jobData), GetReflectionData<T>(), default(global::Unity.Jobs.JobHandle), global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Run);
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ScheduleParallelFor(ref parameters, arrayLength, arrayLength);
		}

		public unsafe static void RunByRef<T>(this ref T jobData, int arrayLength, int indicesPerJobCount) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref jobData), GetReflectionData<T>(), default(global::Unity.Jobs.JobHandle), global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Run);
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ScheduleParallelFor(ref parameters, arrayLength, arrayLength);
		}

		public static void RunBatch<T>(this T jobData, int arrayLength) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			jobData.Run(arrayLength, arrayLength);
		}

		public static void RunBatchByRef<T>(this ref T jobData, int arrayLength) where T : struct, global::Unity.Jobs.IJobParallelForBatch
		{
			RunByRef(ref jobData, arrayLength, arrayLength);
		}
	}
}
