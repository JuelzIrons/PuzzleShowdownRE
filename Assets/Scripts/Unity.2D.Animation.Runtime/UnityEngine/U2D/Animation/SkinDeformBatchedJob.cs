namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct SkinDeformBatchedJob : global::Unity.Jobs.IJobParallelFor
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal unsafe delegate void CopyBuffer_00000028_0024PostfixBurstDelegate(byte* currentPosStart, byte* previousPosStart, int streamSize, int vertexCount);

		internal static class CopyBuffer_00000028_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.Animation.SkinDeformBatchedJob.CopyBuffer_00000028_0024PostfixBurstDelegate>(CopyBuffer).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(byte* currentPosStart, byte* previousPosStart, int streamSize, int vertexCount)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<byte*, byte*, int, int, void>)functionPointer)(currentPosStart, previousPosStart, streamSize, vertexCount);
						return;
					}
				}
				CopyBuffer_0024BurstManaged(currentPosStart, previousPosStart, streamSize, vertexCount);
			}
		}

		public global::Unity.Collections.NativeSlice<byte> vertices;

		public global::Unity.Collections.NativeSlice<byte> previousVertices;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.SpriteSkinData> spriteSkinData;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.PerSkinJobData> perSkinJobData;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> finalBoneTransforms;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<bool> isSpriteSkinValidForDeformArray;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<bool> hasBoneTransformsChanged;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Bounds> bounds;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<int> lastDeformedFrame;

		public int frameCount;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EAnimation_002ECopyBuffer_00000028_0024PostfixBurstDelegate))]
		private unsafe static void CopyBuffer(byte* currentPosStart, byte* previousPosStart, int streamSize, int vertexCount)
		{
			global::UnityEngine.U2D.Animation.SkinDeformBatchedJob.CopyBuffer_00000028_0024BurstDirectCall.Invoke(currentPosStart, previousPosStart, streamSize, vertexCount);
		}

		public unsafe void Execute(int spriteIndex)
		{
			if (!isSpriteSkinValidForDeformArray[spriteIndex])
			{
				return;
			}
			global::UnityEngine.U2D.Animation.SpriteSkinData spriteSkinData = this.spriteSkinData[spriteIndex];
			global::UnityEngine.U2D.Animation.PerSkinJobData perSkinJobData = this.perSkinJobData[spriteIndex];
			if (!hasBoneTransformsChanged[spriteIndex] && spriteSkinData.previousDeformVerticesStartPos >= 0)
			{
				byte* currentPosStart = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafePtr(vertices) + spriteSkinData.deformVerticesStartPos;
				byte* previousPosStart = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafePtr(previousVertices) + spriteSkinData.previousDeformVerticesStartPos;
				int spriteVertexStreamSize = spriteSkinData.spriteVertexStreamSize;
				int spriteVertexCount = spriteSkinData.spriteVertexCount;
				switch (spriteVertexStreamSize)
				{
				case 12:
					CopyBuffer(currentPosStart, previousPosStart, 12, spriteVertexCount);
					break;
				case 28:
					CopyBuffer(currentPosStart, previousPosStart, 28, spriteVertexCount);
					break;
				default:
					CopyBuffer(currentPosStart, previousPosStart, spriteVertexStreamSize, spriteVertexCount);
					break;
				}
				return;
			}
			lastDeformedFrame[spriteIndex] = frameCount;
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafePtr(vertices);
			byte* num = unsafePtr + spriteSkinData.deformVerticesStartPos;
			global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float3> nativeSlice = global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<global::Unity.Mathematics.float3>(num, spriteSkinData.spriteVertexStreamSize, spriteSkinData.spriteVertexCount);
			global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float4> nativeSlice2 = global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<global::Unity.Mathematics.float4>(num + spriteSkinData.tangentVertexOffset, spriteSkinData.spriteVertexStreamSize, spriteSkinData.spriteVertexCount);
			global::Unity.Mathematics.float3 float5 = float.MaxValue;
			global::Unity.Mathematics.float3 float6 = float.MinValue;
			if (spriteSkinData.boneTransformId.Length != 1)
			{
				for (int i = 0; i < spriteSkinData.spriteVertexCount; i++)
				{
					global::Unity.Mathematics.float3 b = spriteSkinData.vertices[i];
					global::Unity.Mathematics.float4 float7 = spriteSkinData.tangents[i];
					global::UnityEngine.BoneWeight boneWeight = spriteSkinData.boneWeights[i];
					int index = boneWeight.boneIndex0 + perSkinJobData.bindPosesIndex.x;
					int index2 = boneWeight.boneIndex1 + perSkinJobData.bindPosesIndex.x;
					int index3 = boneWeight.boneIndex2 + perSkinJobData.bindPosesIndex.x;
					int index4 = boneWeight.boneIndex3 + perSkinJobData.bindPosesIndex.x;
					if (spriteSkinData.hasTangents)
					{
						global::Unity.Mathematics.float4 b2 = new global::Unity.Mathematics.float4(float7.xyz, 0f);
						nativeSlice2[i] = new global::Unity.Mathematics.float4(global::Unity.Mathematics.math.normalize((global::Unity.Mathematics.math.mul(finalBoneTransforms[index], b2) * boneWeight.weight0 + global::Unity.Mathematics.math.mul(finalBoneTransforms[index2], b2) * boneWeight.weight1 + global::Unity.Mathematics.math.mul(finalBoneTransforms[index3], b2) * boneWeight.weight2 + global::Unity.Mathematics.math.mul(finalBoneTransforms[index4], b2) * boneWeight.weight3).xyz), float7.w);
					}
					nativeSlice[i] = global::Unity.Mathematics.math.transform(finalBoneTransforms[index], b) * boneWeight.weight0 + global::Unity.Mathematics.math.transform(finalBoneTransforms[index2], b) * boneWeight.weight1 + global::Unity.Mathematics.math.transform(finalBoneTransforms[index3], b) * boneWeight.weight2 + global::Unity.Mathematics.math.transform(finalBoneTransforms[index4], b) * boneWeight.weight3;
					float5 = global::Unity.Mathematics.math.min(float5, nativeSlice[i]);
					float6 = global::Unity.Mathematics.math.max(float6, nativeSlice[i]);
				}
			}
			else
			{
				int index5 = spriteSkinData.boneWeights[0].boneIndex0 + perSkinJobData.bindPosesIndex.x;
				if (spriteSkinData.hasTangents)
				{
					for (int j = 0; j < spriteSkinData.spriteVertexCount; j++)
					{
						global::Unity.Mathematics.float4 float8 = spriteSkinData.tangents[j];
						nativeSlice2[j] = new global::Unity.Mathematics.float4(global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.mul(b: new global::Unity.Mathematics.float4(float8.xyz, 0f), a: finalBoneTransforms[index5]).xyz), float8.w);
					}
				}
				for (int k = 0; k < spriteSkinData.spriteVertexCount; k++)
				{
					global::Unity.Mathematics.float3 b3 = spriteSkinData.vertices[k];
					nativeSlice[k] = global::Unity.Mathematics.math.transform(finalBoneTransforms[index5], b3);
					float5 = global::Unity.Mathematics.math.min(float5, nativeSlice[k]);
					float6 = global::Unity.Mathematics.math.max(float6, nativeSlice[k]);
				}
			}
			global::Unity.Mathematics.float3 float9 = (float6 - float5) * 0.5f;
			global::Unity.Mathematics.float3 float10 = float5 + float9;
			bounds[spriteIndex] = new global::UnityEngine.Bounds(float10, float9 * 2f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void CopyBuffer_0024BurstManaged(byte* currentPosStart, byte* previousPosStart, int streamSize, int vertexCount)
		{
			for (int i = 0; i < vertexCount; i++)
			{
				byte* source = previousPosStart + i * streamSize;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(currentPosStart + i * streamSize, source, streamSize);
			}
		}
	}
}
