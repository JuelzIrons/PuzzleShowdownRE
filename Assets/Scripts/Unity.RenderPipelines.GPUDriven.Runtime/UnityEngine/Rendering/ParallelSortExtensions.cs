namespace UnityEngine.Rendering
{
	internal static class ParallelSortExtensions
	{
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal struct RadixSortBucketCountJob : global::Unity.Jobs.IJobFor
		{
			[global::Unity.Collections.ReadOnly]
			public int radix;

			[global::Unity.Collections.ReadOnly]
			public int jobsCount;

			[global::Unity.Collections.ReadOnly]
			public int batchSize;

			[global::Unity.Collections.ReadOnly]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> array;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> buckets;

			public void Execute(int index)
			{
				int num = index * batchSize;
				int num2 = global::Unity.Mathematics.math.min(num + batchSize, array.Length);
				int num3 = index * 256;
				for (int i = num; i < num2; i++)
				{
					int num4 = (array[i] >> radix * 8) & 0xFF;
					buckets[num3 + num4]++;
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal struct RadixSortBatchPrefixSumJob : global::Unity.Jobs.IJobFor
		{
			[global::Unity.Collections.ReadOnly]
			public int radix;

			[global::Unity.Collections.ReadOnly]
			public int jobsCount;

			[global::Unity.Collections.ReadOnly]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> array;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> counter;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> indicesSum;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> buckets;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> indices;

			private unsafe static int AtomicIncrement(global::Unity.Collections.NativeArray<int> counter)
			{
				return global::System.Threading.Interlocked.Increment(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<int>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(counter)));
			}

			private int JobIndexPrefixSum(int sum, int i)
			{
				for (int j = 0; j < jobsCount; j++)
				{
					int index = i + j * 256;
					indices[index] = sum;
					sum += buckets[index];
					buckets[index] = 0;
				}
				return sum;
			}

			public void Execute(int index)
			{
				int num = index * 16;
				int num2 = num + 16;
				int num3 = 0;
				for (int i = num; i < num2; i++)
				{
					num3 = JobIndexPrefixSum(num3, i);
				}
				indicesSum[index] = num3;
				if (AtomicIncrement(counter) != 16)
				{
					return;
				}
				int num4 = 0;
				if (radix < 3)
				{
					for (int j = 0; j < 16; j++)
					{
						int num5 = indicesSum[j];
						indicesSum[j] = num4;
						num4 += num5;
					}
				}
				else
				{
					for (int k = 8; k < 16; k++)
					{
						int num6 = indicesSum[k];
						indicesSum[k] = num4;
						num4 += num6;
					}
					for (int l = 0; l < 8; l++)
					{
						int num7 = indicesSum[l];
						indicesSum[l] = num4;
						num4 += num7;
					}
				}
				counter[0] = 0;
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal struct RadixSortPrefixSumJob : global::Unity.Jobs.IJobFor
		{
			[global::Unity.Collections.ReadOnly]
			public int jobsCount;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> indicesSum;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> indices;

			public void Execute(int index)
			{
				int num = index * 16;
				int num2 = num + 16;
				int num3 = indicesSum[index];
				for (int i = 0; i < jobsCount; i++)
				{
					for (int j = num; j < num2; j++)
					{
						indices[i * 256 + j] += num3;
					}
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal struct RadixSortBucketSortJob : global::Unity.Jobs.IJobFor
		{
			[global::Unity.Collections.ReadOnly]
			public int radix;

			[global::Unity.Collections.ReadOnly]
			public int batchSize;

			[global::Unity.Collections.ReadOnly]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> array;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> indices;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			public global::Unity.Collections.NativeArray<int> arraySorted;

			public void Execute(int index)
			{
				int num = index * batchSize;
				int num2 = global::Unity.Mathematics.math.min(num + batchSize, array.Length);
				int num3 = index * 256;
				for (int i = num; i < num2; i++)
				{
					int num4 = array[i];
					int num5 = (num4 >> radix * 8) & 0xFF;
					int index2 = indices[num3 + num5]++;
					arraySorted[index2] = num4;
				}
			}
		}

		private const int kMinRadixSortArraySize = 2048;

		private const int kMinRadixSortBatchSize = 256;

		internal static global::Unity.Jobs.JobHandle ParallelSort(this global::Unity.Collections.NativeArray<int> array)
		{
			if (array.Length <= 1)
			{
				return default(global::Unity.Jobs.JobHandle);
			}
			global::Unity.Jobs.JobHandle jobHandle = default(global::Unity.Jobs.JobHandle);
			if (array.Length >= 2048)
			{
				int num = global::UnityEngine.Mathf.Max(global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobWorkerCount + 1, 1);
				int num2 = global::UnityEngine.Mathf.Max(256, global::UnityEngine.Mathf.CeilToInt((float)array.Length / (float)num));
				int num3 = global::UnityEngine.Mathf.CeilToInt((float)array.Length / (float)num2);
				global::Unity.Collections.NativeArray<int> nativeArray = new global::Unity.Collections.NativeArray<int>(array.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				global::Unity.Collections.NativeArray<int> counter = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.TempJob);
				global::Unity.Collections.NativeArray<int> buckets = new global::Unity.Collections.NativeArray<int>(num3 * 256, global::Unity.Collections.Allocator.TempJob);
				global::Unity.Collections.NativeArray<int> indices = new global::Unity.Collections.NativeArray<int>(num3 * 256, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				global::Unity.Collections.NativeArray<int> indicesSum = new global::Unity.Collections.NativeArray<int>(16, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				global::Unity.Collections.NativeArray<int> a = array;
				global::Unity.Collections.NativeArray<int> b = nativeArray;
				for (int i = 0; i < 4; i++)
				{
					global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortBucketCountJob jobData = new global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortBucketCountJob
					{
						radix = i,
						jobsCount = num3,
						batchSize = num2,
						buckets = buckets,
						array = a
					};
					global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortBatchPrefixSumJob jobData2 = new global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortBatchPrefixSumJob
					{
						radix = i,
						jobsCount = num3,
						array = a,
						counter = counter,
						buckets = buckets,
						indices = indices,
						indicesSum = indicesSum
					};
					global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortPrefixSumJob jobData3 = new global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortPrefixSumJob
					{
						jobsCount = num3,
						indices = indices,
						indicesSum = indicesSum
					};
					global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortBucketSortJob jobData4 = new global::UnityEngine.Rendering.ParallelSortExtensions.RadixSortBucketSortJob
					{
						radix = i,
						batchSize = num2,
						indices = indices,
						array = a,
						arraySorted = b
					};
					jobHandle = global::Unity.Jobs.IJobForExtensions.ScheduleParallel(jobData, num3, 1, jobHandle);
					jobHandle = global::Unity.Jobs.IJobForExtensions.ScheduleParallel(jobData2, 16, 1, jobHandle);
					jobHandle = global::Unity.Jobs.IJobForExtensions.ScheduleParallel(jobData3, 16, 1, jobHandle);
					jobHandle = global::Unity.Jobs.IJobForExtensions.ScheduleParallel(jobData4, num3, 1, jobHandle);
					global::Unity.Jobs.JobHandle.ScheduleBatchedJobs();
					Swap(ref a, ref b);
				}
				nativeArray.Dispose(jobHandle);
				counter.Dispose(jobHandle);
				buckets.Dispose(jobHandle);
				indices.Dispose(jobHandle);
				indicesSum.Dispose(jobHandle);
			}
			else
			{
				jobHandle = global::Unity.Collections.NativeSortExtension.SortJob(array).Schedule();
			}
			return jobHandle;
			static void Swap(ref global::Unity.Collections.NativeArray<int> reference, ref global::Unity.Collections.NativeArray<int> reference2)
			{
				global::Unity.Collections.NativeArray<int> nativeArray2 = reference;
				reference = reference2;
				reference2 = nativeArray2;
			}
		}
	}
}
