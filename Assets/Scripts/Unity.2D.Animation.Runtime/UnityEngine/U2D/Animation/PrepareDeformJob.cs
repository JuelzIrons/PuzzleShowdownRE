namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct PrepareDeformJob : global::Unity.Jobs.IJob
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.PerSkinJobData> perSkinJobData;

		[global::Unity.Collections.ReadOnly]
		public int batchDataSize;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> boneLookupData;

		public void Execute()
		{
			for (int i = 0; i < batchDataSize; i++)
			{
				global::UnityEngine.U2D.Animation.PerSkinJobData perSkinJobData = this.perSkinJobData[i];
				int num = 0;
				int num2 = perSkinJobData.bindPosesIndex.x;
				while (num2 < perSkinJobData.bindPosesIndex.y)
				{
					boneLookupData[num2] = new global::Unity.Mathematics.int2(i, num);
					num2++;
					num++;
				}
			}
		}
	}
}
