namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct FillPerSkinJobSingleThread : global::Unity.Jobs.IJob
	{
		public global::UnityEngine.U2D.Animation.PerSkinJobData combinedSkinBatch;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<bool> isSpriteSkinValidForDeformArray;

		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.SpriteSkinData> spriteSkinDataArray;

		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.PerSkinJobData> perSkinJobDataArray;

		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.PerSkinJobData> combinedSkinBatchArray;

		public void Execute()
		{
			int length = spriteSkinDataArray.Length;
			for (int i = 0; i < length; i++)
			{
				global::UnityEngine.U2D.Animation.SpriteSkinData value = spriteSkinDataArray[i];
				value.previousDeformVerticesStartPos = value.deformVerticesStartPos;
				value.deformVerticesStartPos = -1;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				if (isSpriteSkinValidForDeformArray[i])
				{
					value.deformVerticesStartPos = combinedSkinBatch.deformVerticesStartPos;
					num = value.spriteVertexCount * value.spriteVertexStreamSize;
					num2 = value.spriteVertexCount;
					num3 = value.bindPoses.Length;
				}
				combinedSkinBatch.verticesIndex.x = combinedSkinBatch.verticesIndex.y;
				combinedSkinBatch.verticesIndex.y = combinedSkinBatch.verticesIndex.x + num2;
				combinedSkinBatch.bindPosesIndex.x = combinedSkinBatch.bindPosesIndex.y;
				combinedSkinBatch.bindPosesIndex.y = combinedSkinBatch.bindPosesIndex.x + num3;
				spriteSkinDataArray[i] = value;
				perSkinJobDataArray[i] = combinedSkinBatch;
				combinedSkinBatch.deformVerticesStartPos += num;
			}
			combinedSkinBatchArray[0] = combinedSkinBatch;
		}
	}
}
