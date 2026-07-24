namespace UnityEngine.Rendering.Universal
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct Sorting
	{
		public static global::UnityEngine.Rendering.ProfilingSampler s_QuickSortSampler = new global::UnityEngine.Rendering.ProfilingSampler("QuickSort");

		public static global::UnityEngine.Rendering.ProfilingSampler s_InsertionSortSampler = new global::UnityEngine.Rendering.ProfilingSampler("InsertionSort");

		public static void QuickSort<T>(T[] data, global::System.Func<T, T, int> compare)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(s_QuickSortSampler))
			{
				QuickSort(data, 0, data.Length - 1, compare);
			}
		}

		public static void QuickSort<T>(T[] data, int start, int end, global::System.Func<T, T, int> compare)
		{
			int num = end - start;
			if (num < 1)
			{
				return;
			}
			if (num < 8)
			{
				InsertionSort(data, start, end, compare);
			}
			else if (start < end)
			{
				int num2 = Partition(data, start, end, compare);
				if (num2 >= 1)
				{
					QuickSort(data, start, num2, compare);
				}
				if (num2 + 1 < end)
				{
					QuickSort(data, num2 + 1, end, compare);
				}
			}
		}

		private static T Median3Pivot<T>(T[] data, int start, int pivot, int end, global::System.Func<T, T, int> compare)
		{
			if (compare(data[end], data[start]) < 0)
			{
				Swap(start, end);
			}
			if (compare(data[pivot], data[start]) < 0)
			{
				Swap(start, pivot);
			}
			if (compare(data[end], data[pivot]) < 0)
			{
				Swap(pivot, end);
			}
			return data[pivot];
			void Swap(int a, int b)
			{
				T val = data[a];
				data[a] = data[b];
				data[b] = val;
			}
		}

		private static int Partition<T>(T[] data, int start, int end, global::System.Func<T, T, int> compare)
		{
			int num = end - start;
			int pivot = start + num / 2;
			T arg = Median3Pivot(data, start, pivot, end, compare);
			while (true)
			{
				if (compare(data[start], arg) < 0)
				{
					start++;
					continue;
				}
				while (compare(data[end], arg) > 0)
				{
					end--;
				}
				if (start >= end)
				{
					break;
				}
				T val = data[start];
				data[start++] = data[end];
				data[end--] = val;
			}
			return end;
		}

		public static void InsertionSort<T>(T[] data, global::System.Func<T, T, int> compare)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(s_InsertionSortSampler))
			{
				InsertionSort(data, 0, data.Length - 1, compare);
			}
		}

		public static void InsertionSort<T>(T[] data, int start, int end, global::System.Func<T, T, int> compare)
		{
			for (int i = start + 1; i < end + 1; i++)
			{
				T val = data[i];
				int num = i - 1;
				while (num >= 0 && compare(val, data[num]) < 0)
				{
					data[num + 1] = data[num];
					num--;
				}
				data[num + 1] = val;
			}
		}
	}
}
