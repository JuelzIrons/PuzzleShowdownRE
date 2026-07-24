namespace UnityEngine.Rendering.Universal
{
	internal class DecalCreateDrawCallSystem
	{
		[global::Unity.Burst.BurstCompile]
		private struct DrawCallJob : global::Unity.Jobs.IJob
		{
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> decalToWorlds;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> normalToWorlds;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> sizeOffsets;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> drawDistances;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> angleFades;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4> uvScaleBiases;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> layerMasks;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<ulong> sceneLayerMasks;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<float> fadeFactors;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.BoundingSphere> boundingSpheres;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<uint> renderingLayerMasks;

			public global::UnityEngine.Vector3 cameraPosition;

			public ulong sceneCullingMask;

			public int cullingMask;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> visibleDecalIndices;

			public int visibleDecalCount;

			public float maxDrawDistance;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> decalToWorldsDraw;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> normalToDecalsDraw;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<float> renderingLayerMasksDraw;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.DecalSubDrawCall> subCalls;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<int> subCallCount;

			public void Execute()
			{
				int value = 0;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < visibleDecalCount; i++)
				{
					int index = visibleDecalIndices[i];
					int num3 = 1 << layerMasks[index];
					if ((cullingMask & num3) == 0)
					{
						continue;
					}
					global::UnityEngine.BoundingSphere boundingSphere = boundingSpheres[index];
					global::Unity.Mathematics.float2 float5 = drawDistances[index];
					float magnitude = (cameraPosition - boundingSphere.position).magnitude;
					float num4 = global::Unity.Mathematics.math.min(float5.x, maxDrawDistance) + boundingSphere.radius;
					if (!(magnitude > num4))
					{
						decalToWorldsDraw[num] = decalToWorlds[index];
						float num5 = fadeFactors[index];
						global::Unity.Mathematics.float2 float6 = angleFades[index];
						global::Unity.Mathematics.float4 float7 = uvScaleBiases[index];
						global::Unity.Mathematics.float4x4 value2 = normalToWorlds[index];
						float num6 = num5 * global::Unity.Mathematics.math.clamp((num4 - magnitude) / (num4 * (1f - float5.y)), 0f, 1f);
						value2.c0.w = float7.x;
						value2.c1.w = float7.y;
						value2.c2.w = float7.z;
						value2.c3 = new global::Unity.Mathematics.float4(num6 * 1f, float6.x, float6.y, float7.w);
						normalToDecalsDraw[num] = value2;
						renderingLayerMasksDraw[num] = global::Unity.Mathematics.math.asfloat(renderingLayerMasks[index]);
						num++;
						if (num - num2 >= global::UnityEngine.Rendering.Universal.DecalDrawSystem.MaxBatchSize)
						{
							subCalls[value++] = new global::UnityEngine.Rendering.Universal.DecalSubDrawCall
							{
								start = num2,
								end = num
							};
							num2 = num;
						}
					}
				}
				if (num - num2 != 0)
				{
					subCalls[value++] = new global::UnityEngine.Rendering.Universal.DecalSubDrawCall
					{
						start = num2,
						end = num
					};
				}
				subCallCount[0] = value;
			}
		}

		private global::UnityEngine.Rendering.Universal.DecalEntityManager m_EntityManager;

		private global::UnityEngine.Rendering.ProfilingSampler m_Sampler;

		private float m_MaxDrawDistance;

		public float maxDrawDistance
		{
			get
			{
				return m_MaxDrawDistance;
			}
			set
			{
				m_MaxDrawDistance = value;
			}
		}

		public DecalCreateDrawCallSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager, float maxDrawDistance)
		{
			m_EntityManager = entityManager;
			m_Sampler = new global::UnityEngine.Rendering.ProfilingSampler("DecalCreateDrawCallSystem.Execute");
			m_MaxDrawDistance = maxDrawDistance;
		}

		public void Execute()
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(m_Sampler))
			{
				for (int i = 0; i < m_EntityManager.chunkCount; i++)
				{
					Execute(m_EntityManager.cachedChunks[i], m_EntityManager.culledChunks[i], m_EntityManager.drawCallChunks[i], m_EntityManager.cachedChunks[i].count);
				}
			}
		}

		private void Execute(global::UnityEngine.Rendering.Universal.DecalCachedChunk cachedChunk, global::UnityEngine.Rendering.Universal.DecalCulledChunk culledChunk, global::UnityEngine.Rendering.Universal.DecalDrawCallChunk drawCallChunk, int count)
		{
			if (count != 0)
			{
				global::Unity.Jobs.JobHandle currentJobHandle = (drawCallChunk.currentJobHandle = global::Unity.Jobs.IJobExtensions.Schedule(new global::UnityEngine.Rendering.Universal.DecalCreateDrawCallSystem.DrawCallJob
				{
					decalToWorlds = cachedChunk.decalToWorlds,
					normalToWorlds = cachedChunk.normalToWorlds,
					sizeOffsets = cachedChunk.sizeOffsets,
					drawDistances = cachedChunk.drawDistances,
					angleFades = cachedChunk.angleFades,
					uvScaleBiases = cachedChunk.uvScaleBias,
					layerMasks = cachedChunk.layerMasks,
					sceneLayerMasks = cachedChunk.sceneLayerMasks,
					fadeFactors = cachedChunk.fadeFactors,
					boundingSpheres = cachedChunk.boundingSpheres,
					renderingLayerMasks = cachedChunk.renderingLayerMasks,
					cameraPosition = culledChunk.cameraPosition,
					sceneCullingMask = culledChunk.sceneCullingMask,
					cullingMask = culledChunk.cullingMask,
					visibleDecalIndices = culledChunk.visibleDecalIndices,
					visibleDecalCount = culledChunk.visibleDecalCount,
					maxDrawDistance = m_MaxDrawDistance,
					decalToWorldsDraw = drawCallChunk.decalToWorlds,
					normalToDecalsDraw = drawCallChunk.normalToDecals,
					renderingLayerMasksDraw = drawCallChunk.renderingLayerMasks,
					subCalls = drawCallChunk.subCalls,
					subCallCount = drawCallChunk.subCallCounts
				}, cachedChunk.currentJobHandle));
				cachedChunk.currentJobHandle = currentJobHandle;
			}
		}
	}
}
