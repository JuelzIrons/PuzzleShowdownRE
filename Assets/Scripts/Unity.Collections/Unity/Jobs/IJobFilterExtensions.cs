namespace Unity.Jobs
{
	public static class IJobFilterExtensions
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct JobFilterProducer<T> where T : struct, global::Unity.Jobs.IJobFilter
		{
			public struct JobWrapper
			{
				[global::Unity.Collections.NativeDisableParallelForRestriction]
				public global::Unity.Collections.NativeList<int> outputIndices;

				public int appendCount;

				public T JobData;
			}

			public delegate void ExecuteJobFunction(ref global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper jobWrapper, global::System.IntPtr additionalPtr, global::System.IntPtr bufferRangePatchData, ref global::Unity.Jobs.LowLevel.Unsafe.JobRanges ranges, int jobIndex);

			internal static readonly global::Unity.Burst.SharedStatic<global::System.IntPtr> jobReflectionData = global::Unity.Burst.SharedStatic<global::System.IntPtr>.GetOrCreate<global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>>();

			[global::Unity.Burst.BurstDiscard]
			internal static void Initialize()
			{
				if (jobReflectionData.Data == global::System.IntPtr.Zero)
				{
					jobReflectionData.Data = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.CreateJobReflectionData(typeof(global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper), typeof(T), new global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.ExecuteJobFunction(Execute));
				}
			}

			public static void Execute(ref global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper jobWrapper, global::System.IntPtr additionalPtr, global::System.IntPtr bufferRangePatchData, ref global::Unity.Jobs.LowLevel.Unsafe.JobRanges ranges, int jobIndex)
			{
				if (jobWrapper.appendCount == -1)
				{
					ExecuteFilter(ref jobWrapper, bufferRangePatchData);
				}
				else
				{
					ExecuteAppend(ref jobWrapper, bufferRangePatchData);
				}
			}

			public unsafe static void ExecuteAppend(ref global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper jobWrapper, global::System.IntPtr bufferRangePatchData)
			{
				int length = jobWrapper.outputIndices.Length;
				jobWrapper.outputIndices.Capacity = global::Unity.Mathematics.math.max(jobWrapper.appendCount + length, jobWrapper.outputIndices.Capacity);
				int* unsafePtr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(jobWrapper.outputIndices);
				int num = length;
				for (int i = 0; i != jobWrapper.appendCount; i++)
				{
					if (jobWrapper.JobData.Execute(i))
					{
						unsafePtr[num] = i;
						num++;
					}
				}
				jobWrapper.outputIndices.ResizeUninitialized(num);
			}

			public unsafe static void ExecuteFilter(ref global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper jobWrapper, global::System.IntPtr bufferRangePatchData)
			{
				int* unsafePtr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(jobWrapper.outputIndices);
				int length = jobWrapper.outputIndices.Length;
				int num = 0;
				for (int i = 0; i != length; i++)
				{
					int num2 = unsafePtr[i];
					if (jobWrapper.JobData.Execute(num2))
					{
						unsafePtr[num] = num2;
						num++;
					}
				}
				jobWrapper.outputIndices.ResizeUninitialized(num);
			}
		}

		public static void EarlyJobInit<T>() where T : struct, global::Unity.Jobs.IJobFilter
		{
			global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.Initialize();
		}

		private static global::System.IntPtr GetReflectionData<T>() where T : struct, global::Unity.Jobs.IJobFilter
		{
			global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.Initialize();
			return global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.jobReflectionData.Data;
		}

		public static global::Unity.Jobs.JobHandle ScheduleAppend<T>(this T jobData, global::Unity.Collections.NativeList<int> indices, int arrayLength, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobFilter
		{
			return ScheduleAppendByRef(ref jobData, indices, arrayLength, dependsOn);
		}

		public static global::Unity.Jobs.JobHandle ScheduleFilter<T>(this T jobData, global::Unity.Collections.NativeList<int> indices, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobFilter
		{
			return ScheduleFilterByRef(ref jobData, indices, dependsOn);
		}

		public static void RunAppend<T>(this T jobData, global::Unity.Collections.NativeList<int> indices, int arrayLength) where T : struct, global::Unity.Jobs.IJobFilter
		{
			RunAppendByRef(ref jobData, indices, arrayLength);
		}

		public static void RunFilter<T>(this T jobData, global::Unity.Collections.NativeList<int> indices) where T : struct, global::Unity.Jobs.IJobFilter
		{
			RunFilterByRef(ref jobData, indices);
		}

		public unsafe static global::Unity.Jobs.JobHandle ScheduleAppendByRef<T>(this ref T jobData, global::Unity.Collections.NativeList<int> indices, int arrayLength, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobFilter
		{
			global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper output = new global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper
			{
				JobData = jobData,
				outputIndices = indices,
				appendCount = arrayLength
			};
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), GetReflectionData<T>(), dependsOn, global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Single);
			return global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.Schedule(ref parameters);
		}

		public unsafe static global::Unity.Jobs.JobHandle ScheduleFilterByRef<T>(this ref T jobData, global::Unity.Collections.NativeList<int> indices, global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJobFilter
		{
			global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper output = new global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper
			{
				JobData = jobData,
				outputIndices = indices,
				appendCount = -1
			};
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), GetReflectionData<T>(), dependsOn, global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Single);
			return global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.Schedule(ref parameters);
		}

		public unsafe static void RunAppendByRef<T>(this ref T jobData, global::Unity.Collections.NativeList<int> indices, int arrayLength) where T : struct, global::Unity.Jobs.IJobFilter
		{
			global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper output = new global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper
			{
				JobData = jobData,
				outputIndices = indices,
				appendCount = arrayLength
			};
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), GetReflectionData<T>(), default(global::Unity.Jobs.JobHandle), global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Run);
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.Schedule(ref parameters);
		}

		public unsafe static void RunFilterByRef<T>(this ref T jobData, global::Unity.Collections.NativeList<int> indices) where T : struct, global::Unity.Jobs.IJobFilter
		{
			global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper output = new global::Unity.Jobs.IJobFilterExtensions.JobFilterProducer<T>.JobWrapper
			{
				JobData = jobData,
				outputIndices = indices,
				appendCount = -1
			};
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters parameters = new global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), GetReflectionData<T>(), default(global::Unity.Jobs.JobHandle), global::Unity.Jobs.LowLevel.Unsafe.ScheduleMode.Run);
			global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.Schedule(ref parameters);
		}
	}
}
