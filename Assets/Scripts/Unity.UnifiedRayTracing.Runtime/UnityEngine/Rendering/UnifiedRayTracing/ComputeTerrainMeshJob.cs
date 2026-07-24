namespace UnityEngine.Rendering.UnifiedRayTracing
{
	[global::Unity.Burst.BurstCompile]
	internal struct ComputeTerrainMeshJob : global::Unity.Jobs.IJobParallelFor
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<float> heightmap;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<bool> holes;

		public int width;

		public int height;

		public global::Unity.Mathematics.float3 heightmapScale;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> positions;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> uvs;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> normals;

		[global::Unity.Collections.NativeDisableParallelForRestriction]
		public global::Unity.Collections.NativeArray<int> indices;

		public void DisposeArrays()
		{
			heightmap.Dispose();
			holes.Dispose();
			positions.Dispose();
			uvs.Dispose();
			normals.Dispose();
			indices.Dispose();
		}

		public void Execute(int index)
		{
			int num = index % width;
			int num2 = index / height;
			global::Unity.Mathematics.float3 float5 = new global::Unity.Mathematics.float3(num, heightmap[num2 * width + num], num2);
			positions[index] = float5 * heightmapScale;
			uvs[index] = float5.xz / new global::Unity.Mathematics.float2(width, height);
			normals[index] = CalculateTerrainNormal(heightmap, num, num2, width, height, heightmapScale);
			if (num < width - 1 && num2 < height - 1)
			{
				int num3 = num2 * width + num;
				int value = num3 + 1;
				int num4 = num3 + width;
				int value2 = num4 + 1;
				int num5 = num + num2 * (width - 1);
				if (!holes[num5])
				{
					num3 = (value = (num4 = (value2 = 0)));
				}
				indices[6 * num5] = num3;
				indices[6 * num5 + 1] = value2;
				indices[6 * num5 + 2] = value;
				indices[6 * num5 + 3] = num3;
				indices[6 * num5 + 4] = num4;
				indices[6 * num5 + 5] = value2;
			}
		}

		private static global::Unity.Mathematics.float3 CalculateTerrainNormal(global::Unity.Collections.NativeArray<float> heightmap, int x, int y, int width, int height, global::Unity.Mathematics.float3 scale)
		{
			float num = (SampleHeight(x - 1, y - 1, width, height, heightmap, scale.y) * -1f + SampleHeight(x - 1, y, width, height, heightmap, scale.y) * -2f + SampleHeight(x - 1, y + 1, width, height, heightmap, scale.y) * -1f + SampleHeight(x + 1, y - 1, width, height, heightmap, scale.y) * 1f + SampleHeight(x + 1, y, width, height, heightmap, scale.y) * 2f + SampleHeight(x + 1, y + 1, width, height, heightmap, scale.y) * 1f) / scale.x;
			float num2 = SampleHeight(x - 1, y - 1, width, height, heightmap, scale.y) * -1f;
			num2 += SampleHeight(x, y - 1, width, height, heightmap, scale.y) * -2f;
			num2 += SampleHeight(x + 1, y - 1, width, height, heightmap, scale.y) * -1f;
			num2 += SampleHeight(x - 1, y + 1, width, height, heightmap, scale.y) * 1f;
			num2 += SampleHeight(x, y + 1, width, height, heightmap, scale.y) * 2f;
			num2 += SampleHeight(x + 1, y + 1, width, height, heightmap, scale.y) * 1f;
			num2 /= scale.z;
			return global::Unity.Mathematics.math.normalize(new global::Unity.Mathematics.float3(0f - num, 8f, 0f - num2));
		}

		private static float SampleHeight(int x, int y, int width, int height, global::Unity.Collections.NativeArray<float> heightmap, float scale)
		{
			x = global::Unity.Mathematics.math.clamp(x, 0, width - 1);
			y = global::Unity.Mathematics.math.clamp(y, 0, height - 1);
			return heightmap[x + y * width] * scale;
		}
	}
}
