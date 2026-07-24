namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct CopySpriteRendererBoneTransformBuffersJob : global::Unity.Jobs.IJobParallelFor
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<bool> isSpriteSkinValidForDeformArray;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.SpriteSkinData> spriteSkinData;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.PerSkinJobData> perSkinJobData;

		[global::Unity.Collections.ReadOnly]
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public global::System.IntPtr ptrBoneTransforms;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<global::System.IntPtr> buffers;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<int> bufferSizes;

		public void Execute(int i)
		{
			global::UnityEngine.U2D.Animation.SpriteSkinData spriteSkinData = this.spriteSkinData[i];
			global::UnityEngine.U2D.Animation.PerSkinJobData perSkinJobData = this.perSkinJobData[i];
			global::System.IntPtr value = default(global::System.IntPtr);
			int value2 = 0;
			if (isSpriteSkinValidForDeformArray[i])
			{
				value = ptrBoneTransforms + perSkinJobData.bindPosesIndex.x * 64;
				value2 = spriteSkinData.boneTransformId.Length;
			}
			buffers[i] = value;
			bufferSizes[i] = value2;
		}
	}
}
