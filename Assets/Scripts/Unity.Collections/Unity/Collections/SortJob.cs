namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
	})]
	public struct SortJob<T, U> where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
	{
		[global::Unity.Burst.BurstCompile]
		public struct SegmentSort : global::Unity.Jobs.IJobParallelFor
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe T* Data;

			internal U Comp;

			internal int Length;

			internal int SegmentWidth;

			public unsafe void Execute(int index)
			{
				int num = index * SegmentWidth;
				int length = ((Length - num < SegmentWidth) ? (Length - num) : SegmentWidth);
				global::Unity.Collections.NativeSortExtension.Sort(Data + num, length, Comp);
			}
		}

		[global::Unity.Burst.BurstCompile]
		public struct SegmentSortMerge : global::Unity.Jobs.IJob
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe T* Data;

			internal U Comp;

			internal int Length;

			internal int SegmentWidth;

			public unsafe void Execute()
			{
				int num = (Length + (SegmentWidth - 1)) / SegmentWidth;
				int* ptr = stackalloc int[num];
				T* ptr2 = (T*)global::Unity.Collections.Memory.Unmanaged.Allocate(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * Length, 16, global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < Length; i++)
				{
					int num2 = -1;
					T val = default(T);
					for (int j = 0; j < num; j++)
					{
						int num3 = j * SegmentWidth;
						int num4 = ptr[j];
						int num5 = ((Length - num3 < SegmentWidth) ? (Length - num3) : SegmentWidth);
						if (num4 != num5)
						{
							T val2 = Data[num3 + num4];
							if (num2 == -1 || Comp.Compare(val2, val) <= 0)
							{
								val = val2;
								num2 = j;
							}
						}
					}
					ptr[num2]++;
					ptr2[i] = val;
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Data, ptr2, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * Length);
			}
		}

		public unsafe T* Data;

		public U Comp;

		public int Length;

		public unsafe global::Unity.Jobs.JobHandle Schedule(global::Unity.Jobs.JobHandle inputDeps = default(global::Unity.Jobs.JobHandle))
		{
			if (Length == 0)
			{
				return inputDeps;
			}
			int num = (Length + 1023) / 1024;
			int threadIndexCount = global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.ThreadIndexCount;
			int num2 = global::Unity.Mathematics.math.max(1, threadIndexCount);
			int innerloopBatchCount = num / num2;
			global::Unity.Jobs.JobHandle dependsOn = global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::Unity.Collections.SortJob<T, U>.SegmentSort
			{
				Data = Data,
				Comp = Comp,
				Length = Length,
				SegmentWidth = 1024
			}, num, innerloopBatchCount, inputDeps);
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.SortJob<T, U>.SegmentSortMerge
			{
				Data = Data,
				Comp = Comp,
				Length = Length,
				SegmentWidth = 1024
			}, dependsOn);
		}
	}
}
