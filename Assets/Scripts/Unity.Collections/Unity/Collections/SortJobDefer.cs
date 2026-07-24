namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
	})]
	public struct SortJobDefer<T, U> where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
	{
		[global::Unity.Burst.BurstCompile]
		public struct SegmentSort : global::Unity.Jobs.IJobParallelForDefer
		{
			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeList<T> DataRO;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* Data;

			internal U Comp;

			internal int SegmentWidth;

			public unsafe void Execute(int index)
			{
				int num = index * SegmentWidth;
				int length = ((Data->Length - num < SegmentWidth) ? (Data->Length - num) : SegmentWidth);
				global::Unity.Collections.NativeSortExtension.Sort(Data->Ptr + num, length, Comp);
			}
		}

		[global::Unity.Burst.BurstCompile]
		public struct SegmentSortMerge : global::Unity.Jobs.IJob
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal global::Unity.Collections.NativeList<T> Data;

			internal U Comp;

			internal int SegmentWidth;

			public unsafe void Execute()
			{
				int length = Data.Length;
				T* unsafePtr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(Data);
				int num = (length + (SegmentWidth - 1)) / SegmentWidth;
				int* ptr = stackalloc int[num];
				T* ptr2 = (T*)global::Unity.Collections.Memory.Unmanaged.Allocate(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * length, 16, global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < length; i++)
				{
					int num2 = -1;
					T val = default(T);
					for (int j = 0; j < num; j++)
					{
						int num3 = j * SegmentWidth;
						int num4 = ptr[j];
						int num5 = ((length - num3 < SegmentWidth) ? (length - num3) : SegmentWidth);
						if (num4 != num5)
						{
							T val2 = unsafePtr[num3 + num4];
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
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(unsafePtr, ptr2, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * length);
			}
		}

		public global::Unity.Collections.NativeList<T> Data;

		public U Comp;

		public unsafe global::Unity.Jobs.JobHandle Schedule(global::Unity.Jobs.JobHandle inputDeps = default(global::Unity.Jobs.JobHandle))
		{
			global::Unity.Collections.SortJobDefer<T, U>.SegmentSort jobData = new global::Unity.Collections.SortJobDefer<T, U>.SegmentSort
			{
				DataRO = Data,
				Data = Data.m_ListData,
				Comp = Comp,
				SegmentWidth = 1024
			};
			global::Unity.Jobs.JobHandle dependsOn = global::Unity.Jobs.IJobParallelForDeferExtensions.ScheduleByRef(ref jobData, Data, 1024, inputDeps);
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.SortJobDefer<T, U>.SegmentSortMerge
			{
				Data = Data,
				Comp = Comp,
				SegmentWidth = 1024
			}, dependsOn);
		}
	}
}
