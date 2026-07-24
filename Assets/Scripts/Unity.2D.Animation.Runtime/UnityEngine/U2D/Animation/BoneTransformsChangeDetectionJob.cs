namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct BoneTransformsChangeDetectionJob : global::Unity.Jobs.IJobParallelFor
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<bool> transformChanged;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.SpriteSkinData> spriteSkinData;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeHashMap<int, global::UnityEngine.U2D.Animation.TransformAccessJob.TransformData> boneTransformIndex;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<bool> hasBoneTransformsChanged;

		public void Execute(int skinIndex)
		{
			global::UnityEngine.U2D.Animation.SpriteSkinData spriteSkinData = this.spriteSkinData[skinIndex];
			bool value = false;
			for (int i = 0; i < spriteSkinData.boneTransformId.Length; i++)
			{
				int key = spriteSkinData.boneTransformId[i];
				int transformIndex = boneTransformIndex[key].transformIndex;
				if (transformIndex >= 0 && transformChanged[transformIndex])
				{
					value = true;
					break;
				}
			}
			hasBoneTransformsChanged[skinIndex] = value;
		}
	}
}
