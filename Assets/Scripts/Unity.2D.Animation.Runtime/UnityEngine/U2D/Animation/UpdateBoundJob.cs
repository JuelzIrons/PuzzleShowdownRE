namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct UpdateBoundJob : global::Unity.Jobs.IJobParallelFor
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<int> rootTransformId;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<int> rootBoneTransformId;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> rootTransform;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> boneTransform;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeHashMap<int, global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData> rootTransformIndex;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeHashMap<int, global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData> boneTransformIndex;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Bounds> spriteSkinBound;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Bounds> bounds;

		public void Execute(int i)
		{
			global::UnityEngine.Bounds bounds = spriteSkinBound[i];
			int transformIndex = rootTransformIndex[rootTransformId[i]].transformIndex;
			int transformIndex2 = boneTransformIndex[rootBoneTransformId[i]].transformIndex;
			if (transformIndex >= 0 && transformIndex2 >= 0)
			{
				global::Unity.Mathematics.float4x4 a = rootTransform[transformIndex];
				global::Unity.Mathematics.float4x4 b = boneTransform[transformIndex2];
				global::Unity.Mathematics.float4x4 a2 = global::Unity.Mathematics.math.mul(a, b);
				global::Unity.Mathematics.float4 float5 = new global::Unity.Mathematics.float4(bounds.center, 1f);
				global::Unity.Mathematics.float4 float6 = new global::Unity.Mathematics.float4(bounds.extents, 0f);
				global::Unity.Mathematics.float4 x = global::Unity.Mathematics.math.mul(a2, float5 + new global::Unity.Mathematics.float4(0f - float6.x, 0f - float6.y, float6.z, float6.w));
				global::Unity.Mathematics.float4 x2 = global::Unity.Mathematics.math.mul(a2, float5 + new global::Unity.Mathematics.float4(0f - float6.x, float6.y, float6.z, float6.w));
				global::Unity.Mathematics.float4 x3 = global::Unity.Mathematics.math.mul(a2, float5 + float6);
				global::Unity.Mathematics.float4 y = global::Unity.Mathematics.math.mul(a2, float5 + new global::Unity.Mathematics.float4(float6.x, 0f - float6.y, float6.z, float6.w));
				global::Unity.Mathematics.float4 float7 = global::Unity.Mathematics.math.min(x, global::Unity.Mathematics.math.min(x2, global::Unity.Mathematics.math.min(x3, y)));
				float6 = (global::Unity.Mathematics.math.max(x, global::Unity.Mathematics.math.max(x2, global::Unity.Mathematics.math.max(x3, y))) - float7) * 0.5f;
				float5 = float7 + float6;
				this.bounds[i] = new global::UnityEngine.Bounds
				{
					center = new global::UnityEngine.Vector3(float5.x, float5.y, float5.z),
					extents = new global::UnityEngine.Vector3(float6.x, float6.y, float6.z)
				};
			}
		}
	}
}
