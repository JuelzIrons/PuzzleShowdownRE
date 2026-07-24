namespace Unity.Jobs
{
	public static class IJobParallelForDeferExtensions
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct JobParallelForDeferProducer<T> where T : struct, global::Unity.Jobs.IJobParallelForDefer
		{
			public delegate void ExecuteJobFunction(ref T jobData, global::System.IntPtr additionalPtr, global::System.IntPtr bufferRangePatchData, ref global::Unity.Jobs.LowLevel.Unsafe.JobRanges ranges, int jobIndex);

			internal static readonly global::Unity.Burst.SharedStatic<global::System.IntPtr> jobReflectionData = global::Unity.Burst.SharedStatic<global::System.IntPtr>.GetOrCreate<global::Unity.Jobs.IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>>();

			[global::Unity.Burst.BurstDiscard]
			internal static void Initialize()
			{
				if (jobReflectionData.Data == global::System.IntPtr.Zero)
				{
					jobReflectionData.Data = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.CreateJobReflectionData(typeof(T), new global::Unity.Jobs.IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.ExecuteJobFunction(Execute));
				}
			}

			public static void Execute(ref T jobData, global::System.IntPtr additionalPtr, global::System.IntPtr bufferRangePatchData, ref global::Unity.Jobs.LowLevel.Unsafe.JobRanges ranges, int jobIndex)
			{
				int beginIndex;
				int endIndex;
				while (global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out beginIndex, out endIndex))
				{
					int num = endIndex;
					for (int i = beginIndex; i < num; i++)
					{
						jobData.Execute(i);
					}
				}
			}
		}

		public static void EarlyJobInit<T>() where T : struct, global::Unity.Jobs.IJobParallelForDefer
		{
			global::Unity.Jobs.IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.Initialize();
		}

		public unsafe static global::Unity.Jobs.JobHandle Schedule<T, U>(this T jobData, global::Unity.Collections.NativeList<U> list, int innerloopBatchCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForDefer where U : unmanaged
		{
			void* atomicSafetyHandlePtr = null;
			return ScheduleInternal(ref jobData, innerloopBatchCount, global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetInternalListDataPtrUnchecked(ref list), atomicSafetyHandlePtr, dependsOn);
		}

		public unsafe static global::Unity.Jobs.JobHandle ScheduleByRef<T, U>(this ref T jobData, global::Unity.Collections.NativeList<U> list, int innerloopBatchCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForDefer where U : unmanaged
		{
			void* atomicSafetyHandlePtr = null;
			return ScheduleInternal(ref jobData, innerloopBatchCount, global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetInternalListDataPtrUnchecked(ref list), atomicSafetyHandlePtr, dependsOn);
		}

		public unsafe static global::Unity.Jobs.JobHandle Schedule<T>(this T jobData, int* forEachCount, int innerloopBatchCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForDefer
		{
			byte* forEachListPtr = (byte*)forEachCount - sizeof(void*);
			return ScheduleInternal(ref jobData, innerloopBatchCount, forEachListPtr, null, dependsOn);
		}

		public unsafe static global::Unity.Jobs.JobHandle ScheduleByRef<T>(this ref T jobData, int* forEachCount, int innerloopBatchCount, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobParallelForDefer
		{
			byte* forEachListPtr = (byte*)forEachCount - sizeof(void*);
			return ScheduleInternal(ref jobData, innerloopBatchCount, forEachListPtr, null, dependsOn);
		}

		private unsafe static global::Unity.Jobs.JobHandle ScheduleInternal<T>(ref T jobData, int innerloopBatchCount, void* forEachListPtr, void* atomicSafetyHandlePtr, global::Unity.Jobs.JobHandle dependsOn) where T : struct, global::Unity.Jobs.IJobParallelForDefer
		{
			global::Unity.Jobs.IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.Initialize();
			global::System.IntPtr data = global::Unity.Jobs.IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.jobReflectionData.Data;
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref jobData), data, dependsOn, global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Batched);
			return global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ScheduleParallelForDeferArraySize(ref parameters, innerloopBatchCount, forEachListPtr, atomicSafetyHandlePtr);
		}
	}
}
