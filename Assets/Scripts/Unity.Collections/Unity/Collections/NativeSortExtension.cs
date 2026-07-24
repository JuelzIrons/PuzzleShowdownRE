namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class NativeSortExtension
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct DefaultComparer<T> : global::System.Collections.Generic.IComparer<T> where T : global::System.IComparable<T>
		{
			public int Compare(T x, T y)
			{
				return x.CompareTo(y);
			}
		}

		private const int k_IntrosortSizeThreshold = 16;

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void Sort<T>(T* array, int length) where T : unmanaged, global::System.IComparable<T>
		{
			IntroSort<T, global::Unity.Collections.NativeSortExtension.DefaultComparer<T>>(array, length, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(T* array, int length, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			IntroSort<T, U>(array, length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static global::Unity.Collections.SortJob<T, global::Unity.Collections.NativeSortExtension.DefaultComparer<T>> SortJob<T>(T* array, int length) where T : unmanaged, global::System.IComparable<T>
		{
			return new global::Unity.Collections.SortJob<T, global::Unity.Collections.NativeSortExtension.DefaultComparer<T>>
			{
				Data = array,
				Length = length,
				Comp = default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>)
			};
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static global::Unity.Collections.SortJob<T, U> SortJob<T, U>(T* array, int length, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return new global::Unity.Collections.SortJob<T, U>
			{
				Data = array,
				Length = length,
				Comp = comp
			};
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static int BinarySearch<T>(T* ptr, int length, T value) where T : unmanaged, global::System.IComparable<T>
		{
			return BinarySearch(ptr, length, value, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<T, U>(T* ptr, int length, T value, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			int num = 0;
			for (int num2 = length; num2 != 0; num2 >>= 1)
			{
				int num3 = num + (num2 >> 1);
				T y = ptr[num3];
				int num4 = comp.Compare(value, y);
				if (num4 == 0)
				{
					return num3;
				}
				if (num4 > 0)
				{
					num = num3 + 1;
					num2--;
				}
			}
			return ~num;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void Sort<T>(this global::Unity.Collections.NativeArray<T> array) where T : unmanaged, global::System.IComparable<T>
		{
			IntroSortStruct<T, global::Unity.Collections.NativeSortExtension.DefaultComparer<T>>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(array), array.Length, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(this global::Unity.Collections.NativeArray<T> array, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			T* unsafePtr = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(array);
			int length = array.Length;
			IntroSortStruct<T, U>(unsafePtr, length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static global::Unity.Collections.SortJob<T, global::Unity.Collections.NativeSortExtension.DefaultComparer<T>> SortJob<T>(this global::Unity.Collections.NativeArray<T> array) where T : unmanaged, global::System.IComparable<T>
		{
			return SortJob((T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(array), array.Length, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static global::Unity.Collections.SortJob<T, U> SortJob<T, U>(this global::Unity.Collections.NativeArray<T> array, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			T* unsafeBufferPointerWithoutChecks = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(array);
			int length = array.Length;
			return new global::Unity.Collections.SortJob<T, U>
			{
				Data = unsafeBufferPointerWithoutChecks,
				Length = length,
				Comp = comp
			};
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static int BinarySearch<T>(this global::Unity.Collections.NativeArray<T> array, T value) where T : unmanaged, global::System.IComparable<T>
		{
			return array.BinarySearch(value, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<T, U>(this global::Unity.Collections.NativeArray<T> array, T value, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return BinarySearch((T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), array.Length, value, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static int BinarySearch<T>(this global::Unity.Collections.NativeArray<T>.ReadOnly array, T value) where T : unmanaged, global::System.IComparable<T>
		{
			return array.BinarySearch(value, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<T, U>(this global::Unity.Collections.NativeArray<T>.ReadOnly array, T value, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return BinarySearch((T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), array.Length, value, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static void Sort<T>(this global::Unity.Collections.NativeList<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			list.Sort(default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(this global::Unity.Collections.NativeList<T> list, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			IntroSort<T, U>(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(list), list.Length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static global::Unity.Collections.SortJob<T, global::Unity.Collections.NativeSortExtension.DefaultComparer<T>> SortJob<T>(this global::Unity.Collections.NativeList<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			return list.SortJob(default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static global::Unity.Collections.SortJob<T, U> SortJob<T, U>(this global::Unity.Collections.NativeList<T> list, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return SortJob(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(list), list.Length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static global::Unity.Collections.SortJobDefer<T, global::Unity.Collections.NativeSortExtension.DefaultComparer<T>> SortJobDefer<T>(this global::Unity.Collections.NativeList<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			return list.SortJobDefer(default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public static global::Unity.Collections.SortJobDefer<T, U> SortJobDefer<T, U>(this global::Unity.Collections.NativeList<T> list, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return new global::Unity.Collections.SortJobDefer<T, U>
			{
				Data = list,
				Comp = comp
			};
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static int BinarySearch<T>(this global::Unity.Collections.NativeList<T> list, T value) where T : unmanaged, global::System.IComparable<T>
		{
			return list.AsReadOnly().BinarySearch(value, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public static int BinarySearch<T, U>(this global::Unity.Collections.NativeList<T> list, T value, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return list.AsReadOnly().BinarySearch(value, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static void Sort<T>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			list.Sort(default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			IntroSort<T, U>(list.Ptr, list.Length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static global::Unity.Collections.SortJob<T, global::Unity.Collections.NativeSortExtension.DefaultComparer<T>> SortJob<T>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			return SortJob(list.Ptr, list.Length, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static global::Unity.Collections.SortJob<T, U> SortJob<T, U>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return SortJob(list.Ptr, list.Length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static int BinarySearch<T>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list, T value) where T : unmanaged, global::System.IComparable<T>
		{
			return list.BinarySearch(value, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<T, U>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list, T value, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return BinarySearch(list.Ptr, list.Length, value, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static void Sort<T>(this global::Unity.Collections.NativeSlice<T> slice) where T : unmanaged, global::System.IComparable<T>
		{
			slice.Sort(default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(this global::Unity.Collections.NativeSlice<T> slice, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			T* unsafePtr = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafePtr(slice);
			int length = slice.Length;
			IntroSortStruct<T, U>(unsafePtr, length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static global::Unity.Collections.SortJob<T, global::Unity.Collections.NativeSortExtension.DefaultComparer<T>> SortJob<T>(this global::Unity.Collections.NativeSlice<T> slice) where T : unmanaged, global::System.IComparable<T>
		{
			return SortJob((T*)global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafePtr(slice), slice.Length, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static global::Unity.Collections.SortJob<T, U> SortJob<T, U>(this global::Unity.Collections.NativeSlice<T> slice, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return SortJob((T*)global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafePtr(slice), slice.Length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static int BinarySearch<T>(this global::Unity.Collections.NativeSlice<T> slice, T value) where T : unmanaged, global::System.IComparable<T>
		{
			return slice.BinarySearch(value, default(global::Unity.Collections.NativeSortExtension.DefaultComparer<T>));
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<T, U>(this global::Unity.Collections.NativeSlice<T> slice, T value, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			return BinarySearch((T*)global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafeReadOnlyPtr(slice), slice.Length, value, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		internal unsafe static void IntroSort<T, U>(void* array, int length, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			IntroSort_R<T, U>(array, 0, length - 1, 2 * global::Unity.Collections.CollectionHelper.Log2Floor(length), comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		internal unsafe static void IntroSort_R<T, U>(void* array, int lo, int hi, int depth, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			while (hi > lo)
			{
				int num = hi - lo + 1;
				if (num <= 16)
				{
					switch (num)
					{
					case 1:
						break;
					case 2:
						SwapIfGreaterWithItems<T, U>(array, lo, hi, comp);
						break;
					case 3:
						SwapIfGreaterWithItems<T, U>(array, lo, hi - 1, comp);
						SwapIfGreaterWithItems<T, U>(array, lo, hi, comp);
						SwapIfGreaterWithItems<T, U>(array, hi - 1, hi, comp);
						break;
					default:
						InsertionSort<T, U>(array, lo, hi, comp);
						break;
					}
					break;
				}
				if (depth == 0)
				{
					HeapSort<T, U>(array, lo, hi, comp);
					break;
				}
				depth--;
				int num2 = Partition<T, U>(array, lo, hi, comp);
				IntroSort_R<T, U>(array, num2 + 1, hi, depth, comp);
				hi = num2 - 1;
			}
		}

		private unsafe static void InsertionSort<T, U>(void* array, int lo, int hi, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T val = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, i + 1);
				while (num >= lo && comp.Compare(val, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, num)) < 0)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, num + 1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, num));
					num--;
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, num + 1, val);
			}
		}

		private unsafe static int Partition<T, U>(void* array, int lo, int hi, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			int num = lo + (hi - lo) / 2;
			SwapIfGreaterWithItems<T, U>(array, lo, num, comp);
			SwapIfGreaterWithItems<T, U>(array, lo, hi, comp);
			SwapIfGreaterWithItems<T, U>(array, num, hi, comp);
			T x = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, num);
			Swap<T>(array, num, hi - 1);
			int num2 = lo;
			int num3 = hi - 1;
			while (num2 < num3)
			{
				while (num2 < hi)
				{
					T y = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, ++num2);
					if (comp.Compare(x, y) <= 0)
					{
						break;
					}
				}
				while (num3 > num2)
				{
					T y2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, --num3);
					if (comp.Compare(x, y2) >= 0)
					{
						break;
					}
				}
				if (num2 >= num3)
				{
					break;
				}
				Swap<T>(array, num2, num3);
			}
			Swap<T>(array, num2, hi - 1);
			return num2;
		}

		private unsafe static void HeapSort<T, U>(void* array, int lo, int hi, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			int num = hi - lo + 1;
			for (int num2 = num / 2; num2 >= 1; num2--)
			{
				Heapify<T, U>(array, num2, num, lo, comp);
			}
			for (int num3 = num; num3 > 1; num3--)
			{
				Swap<T>(array, lo, lo + num3 - 1);
				Heapify<T, U>(array, 1, num3 - 1, lo, comp);
			}
		}

		private unsafe static void Heapify<T, U>(void* array, int i, int n, int lo, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			T val = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + i - 1);
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n)
				{
					T x = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1);
					T y = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + num);
					if (comp.Compare(x, y) < 0)
					{
						num++;
					}
				}
				T x2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1);
				if (comp.Compare(x2, val) < 0)
				{
					break;
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, lo + i - 1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1));
				i = num;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, lo + i - 1, val);
		}

		private unsafe static void Swap<T>(void* array, int lhs, int rhs) where T : unmanaged
		{
			T value = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lhs);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, lhs, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, rhs));
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, rhs, value);
		}

		private unsafe static void SwapIfGreaterWithItems<T, U>(void* array, int lhs, int rhs, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			if (lhs != rhs && comp.Compare(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lhs), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, rhs)) > 0)
			{
				Swap<T>(array, lhs, rhs);
			}
		}

		private unsafe static void IntroSortStruct<T, U>(void* array, int length, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			IntroSortStruct_R<T, U>(array, 0, length - 1, 2 * global::Unity.Collections.CollectionHelper.Log2Floor(length), comp);
		}

		private unsafe static void IntroSortStruct_R<T, U>(void* array, in int lo, in int _hi, int depth, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			int hi = _hi;
			while (hi > lo)
			{
				int num = hi - lo + 1;
				if (num <= 16)
				{
					switch (num)
					{
					case 1:
						break;
					case 2:
						SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi, comp);
						break;
					case 3:
						SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi - 1, comp);
						SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi, comp);
						SwapIfGreaterWithItemsStruct<T, U>(array, hi - 1, hi, comp);
						break;
					default:
						InsertionSortStruct<T, U>(array, in lo, in hi, comp);
						break;
					}
					break;
				}
				if (depth == 0)
				{
					HeapSortStruct<T, U>(array, in lo, in hi, comp);
					break;
				}
				depth--;
				int num2 = PartitionStruct<T, U>(array, in lo, in hi, comp);
				IntroSortStruct_R<T, U>(array, num2 + 1, in hi, depth, comp);
				hi = num2 - 1;
			}
		}

		private unsafe static void InsertionSortStruct<T, U>(void* array, in int lo, in int hi, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T val = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, i + 1);
				while (num >= lo && comp.Compare(val, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, num)) < 0)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, num + 1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, num));
					num--;
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, num + 1, val);
			}
		}

		private unsafe static int PartitionStruct<T, U>(void* array, in int lo, in int hi, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			int num = lo + (hi - lo) / 2;
			SwapIfGreaterWithItemsStruct<T, U>(array, lo, num, comp);
			SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi, comp);
			SwapIfGreaterWithItemsStruct<T, U>(array, num, hi, comp);
			T x = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, num);
			SwapStruct<T>(array, num, hi - 1);
			int num2 = lo;
			int num3 = hi - 1;
			while (num2 < num3)
			{
				while (num2 < hi)
				{
					T y = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, ++num2);
					if (comp.Compare(x, y) <= 0)
					{
						break;
					}
				}
				while (num3 > num2)
				{
					T y2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, --num3);
					if (comp.Compare(x, y2) >= 0)
					{
						break;
					}
				}
				if (num2 >= num3)
				{
					break;
				}
				SwapStruct<T>(array, num2, num3);
			}
			SwapStruct<T>(array, num2, hi - 1);
			return num2;
		}

		private unsafe static void HeapSortStruct<T, U>(void* array, in int lo, in int hi, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			int num = hi - lo + 1;
			for (int num2 = num / 2; num2 >= 1; num2--)
			{
				HeapifyStruct<T, U>(array, num2, num, in lo, comp);
			}
			for (int num3 = num; num3 > 1; num3--)
			{
				SwapStruct<T>(array, lo, lo + num3 - 1);
				HeapifyStruct<T, U>(array, 1, num3 - 1, in lo, comp);
			}
		}

		private unsafe static void HeapifyStruct<T, U>(void* array, int i, int n, in int lo, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			T val = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + i - 1);
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n)
				{
					T x = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1);
					T y = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + num);
					if (comp.Compare(x, y) < 0)
					{
						num++;
					}
				}
				T x2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1);
				if (comp.Compare(x2, val) < 0)
				{
					break;
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, lo + i - 1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1));
				i = num;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, lo + i - 1, val);
		}

		private unsafe static void SwapStruct<T>(void* array, int lhs, int rhs) where T : unmanaged
		{
			T value = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lhs);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, lhs, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, rhs));
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, rhs, value);
		}

		private unsafe static void SwapIfGreaterWithItemsStruct<T, U>(void* array, int lhs, int rhs, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			if (lhs != rhs && comp.Compare(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, lhs), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, rhs)) > 0)
			{
				SwapStruct<T>(array, lhs, rhs);
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckStrideMatchesSize<T>(int stride) where T : unmanaged
		{
			if (stride != global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>())
			{
				throw new global::System.InvalidOperationException("Sort requires that stride matches the size of the source type");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private unsafe static void CheckComparer<T, U>(T* array, int length, U comp) where T : unmanaged where U : global::System.Collections.Generic.IComparer<T>
		{
			if (length <= 0)
			{
				return;
			}
			T val = *array;
			if (comp.Compare(val, val) != 0)
			{
				throw new global::System.InvalidOperationException("Comparison function is incorrect. Compare(a, a) must return 0/equal.");
			}
			int i = 1;
			for (int num = global::Unity.Mathematics.math.min(length, 8); i < num; i++)
			{
				T val2 = array[i];
				if (comp.Compare(val, val2) != 0 || comp.Compare(val2, val) != 0)
				{
					if (comp.Compare(val, val2) == 0)
					{
						throw new global::System.InvalidOperationException("Comparison function is incorrect. Compare(a, b) of two different values should not return 0/equal.");
					}
					if (comp.Compare(val2, val) == 0)
					{
						throw new global::System.InvalidOperationException("Comparison function is incorrect. Compare(b, a) of two different values should not return 0/equal.");
					}
					if (comp.Compare(val, val2) == comp.Compare(val2, val))
					{
						throw new global::System.InvalidOperationException("Comparison function is incorrect. Compare(a, b) when a and b are different values should not return the same value as Compare(b, a).");
					}
					break;
				}
			}
		}
	}
}
