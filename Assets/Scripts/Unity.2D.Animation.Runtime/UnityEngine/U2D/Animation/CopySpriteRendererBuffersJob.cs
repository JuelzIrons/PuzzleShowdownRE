namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct CopySpriteRendererBuffersJob : global::Unity.Jobs.IJobParallelFor
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<bool> isSpriteSkinValidForDeformArray;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.SpriteSkinData> spriteSkinData;

		[global::Unity.Collections.ReadOnly]
		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public global::System.IntPtr ptrVertices;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<global::System.IntPtr> buffers;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<int> bufferSizes;

		public void Execute(int i)
		{
			global::UnityEngine.U2D.Animation.SpriteSkinData spriteSkinData = this.spriteSkinData[i];
			global::System.IntPtr value = default(global::System.IntPtr);
			int value2 = 0;
			if (isSpriteSkinValidForDeformArray[i])
			{
				value = ptrVertices + spriteSkinData.deformVerticesStartPos;
				value2 = spriteSkinData.spriteVertexCount * spriteSkinData.spriteVertexStreamSize;
			}
			buffers[i] = value;
			bufferSizes[i] = value2;
		}
	}
}
