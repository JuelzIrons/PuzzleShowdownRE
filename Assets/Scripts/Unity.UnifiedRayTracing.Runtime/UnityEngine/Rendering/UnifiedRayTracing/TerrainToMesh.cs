namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal static class TerrainToMesh
	{
		private static global::UnityEngine.Rendering.UnifiedRayTracing.AsyncTerrainToMeshRequest MakeAsyncTerrainToMeshRequest(int width, int height, global::UnityEngine.Vector3 heightmapScale, float[,] heightmap, bool[,] holes)
		{
			int num = width * height;
			global::UnityEngine.Rendering.UnifiedRayTracing.ComputeTerrainMeshJob computeTerrainMeshJob = new global::UnityEngine.Rendering.UnifiedRayTracing.ComputeTerrainMeshJob
			{
				heightmap = new global::Unity.Collections.NativeArray<float>(num, global::Unity.Collections.Allocator.Persistent)
			};
			for (int i = 0; i < num; i++)
			{
				computeTerrainMeshJob.heightmap[i] = heightmap[i / width, i % width];
			}
			computeTerrainMeshJob.holes = new global::Unity.Collections.NativeArray<bool>((width - 1) * (height - 1), global::Unity.Collections.Allocator.Persistent);
			for (int j = 0; j < (width - 1) * (height - 1); j++)
			{
				computeTerrainMeshJob.holes[j] = holes[j / (width - 1), j % (width - 1)];
			}
			computeTerrainMeshJob.width = width;
			computeTerrainMeshJob.height = height;
			computeTerrainMeshJob.heightmapScale = heightmapScale;
			computeTerrainMeshJob.positions = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3>(num, global::Unity.Collections.Allocator.Persistent);
			computeTerrainMeshJob.uvs = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(num, global::Unity.Collections.Allocator.Persistent);
			computeTerrainMeshJob.normals = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3>(num, global::Unity.Collections.Allocator.Persistent);
			computeTerrainMeshJob.indices = new global::Unity.Collections.NativeArray<int>((width - 1) * (height - 1) * 6, global::Unity.Collections.Allocator.Persistent);
			global::Unity.Jobs.JobHandle jobHandle = global::Unity.Jobs.IJobParallelForExtensions.Schedule(computeTerrainMeshJob, num, global::Unity.Mathematics.math.max(width, 128));
			return new global::UnityEngine.Rendering.UnifiedRayTracing.AsyncTerrainToMeshRequest(computeTerrainMeshJob, jobHandle);
		}

		public static global::UnityEngine.Rendering.UnifiedRayTracing.AsyncTerrainToMeshRequest ConvertAsync(global::UnityEngine.Terrain terrain)
		{
			global::UnityEngine.TerrainData terrainData = terrain.terrainData;
			int width = terrainData.heightmapTexture.width;
			int height = terrainData.heightmapTexture.height;
			float[,] heights = terrain.terrainData.GetHeights(0, 0, width, height);
			bool[,] holes = terrain.terrainData.GetHoles(0, 0, width - 1, height - 1);
			return MakeAsyncTerrainToMeshRequest(width, height, terrainData.heightmapScale, heights, holes);
		}

		public static global::UnityEngine.Rendering.UnifiedRayTracing.AsyncTerrainToMeshRequest ConvertAsync(int heightmapWidth, int heightmapHeight, short[] heightmapData, global::UnityEngine.Vector3 heightmapScale, int holeWidth, int holeHeight, byte[] holedata)
		{
			float[,] array = new float[heightmapWidth, heightmapHeight];
			for (int i = 0; i < heightmapHeight; i++)
			{
				for (int j = 0; j < heightmapWidth; j++)
				{
					array[i, j] = (float)heightmapData[i * heightmapWidth + j] / 32766f;
				}
			}
			bool[,] array2 = new bool[heightmapWidth - 1, heightmapHeight - 1];
			if (holedata != null)
			{
				for (int k = 0; k < heightmapHeight - 1; k++)
				{
					for (int l = 0; l < heightmapWidth - 1; l++)
					{
						array2[k, l] = holedata[k * holeWidth + l] != 0;
					}
				}
			}
			else
			{
				for (int m = 0; m < heightmapHeight - 1; m++)
				{
					for (int n = 0; n < heightmapWidth - 1; n++)
					{
						array2[n, m] = true;
					}
				}
			}
			return MakeAsyncTerrainToMeshRequest(heightmapWidth, heightmapHeight, heightmapScale, array, array2);
		}

		public static global::UnityEngine.Mesh Convert(global::UnityEngine.Terrain terrain)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.AsyncTerrainToMeshRequest asyncTerrainToMeshRequest = ConvertAsync(terrain);
			asyncTerrainToMeshRequest.WaitForCompletion();
			return asyncTerrainToMeshRequest.GetMesh();
		}

		public static global::UnityEngine.Mesh Convert(int heightmapWidth, int heightmapHeight, short[] heightmapData, global::UnityEngine.Vector3 heightmapScale, int holeWidth, int holeHeight, byte[] holedata)
		{
			global::UnityEngine.Rendering.UnifiedRayTracing.AsyncTerrainToMeshRequest asyncTerrainToMeshRequest = ConvertAsync(heightmapWidth, heightmapHeight, heightmapData, heightmapScale, holeWidth, holeHeight, holedata);
			asyncTerrainToMeshRequest.WaitForCompletion();
			return asyncTerrainToMeshRequest.GetMesh();
		}
	}
}
