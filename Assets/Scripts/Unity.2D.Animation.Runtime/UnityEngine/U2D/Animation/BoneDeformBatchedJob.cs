namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct BoneDeformBatchedJob : global::Unity.Jobs.IJobParallelFor
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> boneTransform;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> rootTransform;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> boneLookupData;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.SpriteSkinData> spriteSkinData;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeHashMap<int, global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData> rootTransformIndex;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeHashMap<int, global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData> boneTransformIndex;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> finalBoneTransforms;

		public void Execute(int i)
		{
			int x = boneLookupData[i].x;
			int y = boneLookupData[i].y;
			global::UnityEngine.U2D.Animation.SpriteSkinData spriteSkinData = this.spriteSkinData[x];
			int key = spriteSkinData.boneTransformId[y];
			int transformIndex = boneTransformIndex[key].transformIndex;
			if (transformIndex >= 0)
			{
				global::Unity.Mathematics.float4x4 a = boneTransform[transformIndex];
				global::UnityEngine.Matrix4x4 matrix4x = spriteSkinData.bindPoses[y];
				int transformIndex2 = rootTransformIndex[spriteSkinData.transformId].transformIndex;
				finalBoneTransforms[i] = global::Unity.Mathematics.math.mul(rootTransform[transformIndex2], global::Unity.Mathematics.math.mul(a, matrix4x));
			}
		}
	}
}
