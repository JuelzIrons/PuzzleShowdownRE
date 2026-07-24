namespace UnityEngine.U2D.Common.UAi
{
	internal static class kMeans
	{
		private static float CalculateDistance(global::UnityEngine.U2D.Common.UAi.MatrixMxN<float> data, int dataIndex, global::UnityEngine.U2D.Common.UAi.MatrixMxN<float> centroid, int centroidIndex)
		{
			float num = 0f;
			for (int i = 0; i < data.DimensionY; i++)
			{
				num += global::UnityEngine.Mathf.Pow(centroid.Get(centroidIndex, i) - data.Get(dataIndex, i), 2f);
			}
			return global::UnityEngine.Mathf.Sqrt(num);
		}

		private unsafe static float CalculateClustering(global::UnityEngine.U2D.Common.UAi.MatrixMxN<float> data, global::Unity.Collections.NativeArray<int> clusters, ref global::UnityEngine.U2D.Common.UAi.MatrixMxN<float> means, ref global::Unity.Collections.NativeArray<int> centroids, int clusterCount, ref global::Unity.Collections.NativeArray<int> clusterItems)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemSet(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(means.GetArray()), 0, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<int>() * means.Length);
			for (int i = 0; i < data.DimensionX; i++)
			{
				int num = clusters[i];
				clusterItems[num]++;
				for (int j = 0; j < data.DimensionY; j++)
				{
					float num2 = means.Get(num, j);
					means.Set(num, j, data.Get(i, j) + num2);
				}
			}
			for (int k = 0; k < means.DimensionX; k++)
			{
				for (int l = 0; l < means.DimensionY; l++)
				{
					int num3 = clusterItems[k];
					float num4 = means.Get(k, l);
					num4 /= (float)((num3 <= 0) ? 1 : num3);
					means.Set(k, l, num4);
				}
			}
			float num5 = 0f;
			global::Unity.Collections.NativeArray<float> nativeArray = new global::Unity.Collections.NativeArray<float>(clusterCount, global::Unity.Collections.Allocator.Temp);
			for (int m = 0; m < clusterCount; m++)
			{
				nativeArray[m] = float.MaxValue;
			}
			for (int n = 0; n < data.DimensionX; n++)
			{
				int num6 = clusters[n];
				float num7 = CalculateDistance(data, n, means, num6);
				num5 += num7;
				if (num7 < nativeArray[num6])
				{
					nativeArray[num6] = num7;
					centroids[num6] = n;
				}
			}
			nativeArray.Dispose();
			return num5;
		}

		private static bool AssignClustering(global::UnityEngine.U2D.Common.UAi.MatrixMxN<float> data, global::Unity.Collections.NativeArray<int> clusters, ref global::Unity.Collections.NativeArray<int> centroidIdx, int clusterCount)
		{
			bool result = false;
			for (int i = 0; i < data.DimensionX; i++)
			{
				float num = float.MaxValue;
				int num2 = -1;
				for (int j = 0; j < clusterCount; j++)
				{
					int centroidIndex = centroidIdx[j];
					float num3 = CalculateDistance(data, i, data, centroidIndex);
					if (num3 < num)
					{
						num = num3;
						num2 = j;
					}
				}
				if (num2 != -1 && clusters[i] != num2)
				{
					result = true;
					clusters[i] = num2;
				}
			}
			return result;
		}

		private unsafe static void ClusterInternal(global::UnityEngine.U2D.Common.UAi.MatrixMxN<float> data, global::Unity.Collections.NativeArray<int> clusters, global::UnityEngine.U2D.Common.UAi.MatrixMxN<float> means, global::Unity.Collections.NativeArray<int> centroids, global::Unity.Collections.NativeArray<int> clusterItems, int clusterCount, int maxIterations)
		{
			bool flag = true;
			int num = 0;
			global::Unity.Mathematics.Random random = new global::Unity.Mathematics.Random(1u);
			for (int i = 0; i < clusters.Length; i++)
			{
				clusters[i] = random.NextInt(0, clusterCount);
			}
			while (flag && num++ < maxIterations)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemSet(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(clusterItems), 0, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<int>() * clusterCount);
				CalculateClustering(data, clusters, ref means, ref centroids, clusterCount, ref clusterItems);
				flag = AssignClustering(data, clusters, ref centroids, clusterCount);
			}
		}

		public static int[] Cluster3(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> items, int clusterCount, global::Unity.Collections.Allocator alloc, int maxIterations = 64)
		{
			global::UnityEngine.U2D.Common.UAi.MatrixMxN<float> data = new global::UnityEngine.U2D.Common.UAi.MatrixMxN<float>(items.Length, 3, alloc, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<int> clusters = new global::Unity.Collections.NativeArray<int>(items.Length, alloc, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::UnityEngine.U2D.Common.UAi.MatrixMxN<float> means = new global::UnityEngine.U2D.Common.UAi.MatrixMxN<float>(clusterCount, 3, alloc, global::Unity.Collections.NativeArrayOptions.ClearMemory);
			for (int i = 0; i < items.Length; i++)
			{
				data.Set(i, 0, items[i].x);
				data.Set(i, 1, items[i].y);
				data.Set(i, 2, items[i].z);
			}
			global::Unity.Collections.NativeArray<int> centroids = new global::Unity.Collections.NativeArray<int>(clusterCount, alloc, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<int> clusterItems = new global::Unity.Collections.NativeArray<int>(clusterCount, alloc, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			ClusterInternal(data, clusters, means, centroids, clusterItems, clusterCount, maxIterations);
			int[] result = centroids.ToArray();
			clusterItems.Dispose();
			centroids.Dispose();
			means.Dispose();
			clusters.Dispose();
			data.Dispose();
			return result;
		}
	}
}
