namespace UnityEngine.Rendering.Universal
{
	internal class DecalEntityManager : global::System.IDisposable
	{
		private struct CombinedChunks
		{
			public global::UnityEngine.Rendering.Universal.DecalEntityChunk entityChunk;

			public global::UnityEngine.Rendering.Universal.DecalCachedChunk cachedChunk;

			public global::UnityEngine.Rendering.Universal.DecalCulledChunk culledChunk;

			public global::UnityEngine.Rendering.Universal.DecalDrawCallChunk drawCallChunk;

			public int previousChunkIndex;

			public bool valid;
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalEntityChunk> entityChunks = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalEntityChunk>();

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalCachedChunk> cachedChunks = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalCachedChunk>();

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalCulledChunk> culledChunks = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalCulledChunk>();

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalDrawCallChunk> drawCallChunks = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalDrawCallChunk>();

		public int chunkCount;

		private global::UnityEngine.Rendering.ProfilingSampler m_AddDecalSampler;

		private global::UnityEngine.Rendering.ProfilingSampler m_ResizeChunks;

		private global::UnityEngine.Rendering.ProfilingSampler m_SortChunks;

		private global::UnityEngine.Rendering.Universal.DecalEntityIndexer m_DecalEntityIndexer = new global::UnityEngine.Rendering.Universal.DecalEntityIndexer();

		private global::System.Collections.Generic.Dictionary<global::UnityEngine.Material, int> m_MaterialToChunkIndex = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Material, int>();

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalEntityManager.CombinedChunks> m_CombinedChunks = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalEntityManager.CombinedChunks>();

		private global::System.Collections.Generic.List<int> m_CombinedChunkRemmap = new global::System.Collections.Generic.List<int>();

		private global::UnityEngine.Material m_ErrorMaterial;

		private global::UnityEngine.Mesh m_DecalProjectorMesh;

		public global::UnityEngine.Material errorMaterial
		{
			get
			{
				if (m_ErrorMaterial == null)
				{
					m_ErrorMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(global::UnityEngine.Shader.Find("Hidden/InternalErrorShader"));
				}
				return m_ErrorMaterial;
			}
		}

		public global::UnityEngine.Mesh decalProjectorMesh
		{
			get
			{
				if (m_DecalProjectorMesh == null)
				{
					m_DecalProjectorMesh = global::UnityEngine.Rendering.CoreUtils.CreateCubeMesh(new global::UnityEngine.Vector4(-0.5f, -0.5f, -0.5f, 1f), new global::UnityEngine.Vector4(0.5f, 0.5f, 0.5f, 1f));
				}
				return m_DecalProjectorMesh;
			}
		}

		public DecalEntityManager()
		{
			m_AddDecalSampler = new global::UnityEngine.Rendering.ProfilingSampler("DecalEntityManager.CreateDecalEntity");
			m_ResizeChunks = new global::UnityEngine.Rendering.ProfilingSampler("DecalEntityManager.ResizeChunks");
			m_SortChunks = new global::UnityEngine.Rendering.ProfilingSampler("DecalEntityManager.SortChunks");
		}

		public bool IsValid(global::UnityEngine.Rendering.Universal.DecalEntity decalEntity)
		{
			return m_DecalEntityIndexer.IsValid(decalEntity);
		}

		public global::UnityEngine.Rendering.Universal.DecalEntity CreateDecalEntity(global::UnityEngine.Rendering.Universal.DecalProjector decalProjector)
		{
			global::UnityEngine.Material material = decalProjector.material;
			if (material == null)
			{
				material = errorMaterial;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(m_AddDecalSampler))
			{
				int num = CreateChunkIndex(material);
				int count = entityChunks[num].count;
				global::UnityEngine.Rendering.Universal.DecalEntity decalEntity = m_DecalEntityIndexer.CreateDecalEntity(count, num);
				global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk = entityChunks[num];
				global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk = cachedChunks[num];
				global::UnityEngine.Rendering.Universal.DecalCulledChunk decalCulledChunk = culledChunks[num];
				global::UnityEngine.Rendering.Universal.DecalDrawCallChunk decalDrawCallChunk = drawCallChunks[num];
				if (entityChunks[num].capacity == entityChunks[num].count)
				{
					using (new global::UnityEngine.Rendering.ProfilingScope(m_ResizeChunks))
					{
						int y = entityChunks[num].capacity + entityChunks[num].capacity;
						y = global::Unity.Mathematics.math.max(8, y);
						decalEntityChunk.SetCapacity(y);
						decalCachedChunk.SetCapacity(y);
						decalCulledChunk.SetCapacity(y);
						decalDrawCallChunk.SetCapacity(y);
					}
				}
				decalEntityChunk.Push();
				decalCachedChunk.Push();
				decalCulledChunk.Push();
				decalDrawCallChunk.Push();
				decalEntityChunk.decalProjectors[count] = decalProjector;
				decalEntityChunk.decalEntities[count] = decalEntity;
				decalEntityChunk.transformAccessArray.Add(decalProjector.transform);
				UpdateDecalEntityData(decalEntity, decalProjector);
				return decalEntity;
			}
		}

		private int CreateChunkIndex(global::UnityEngine.Material material)
		{
			if (!m_MaterialToChunkIndex.TryGetValue(material, out var value))
			{
				global::UnityEngine.MaterialPropertyBlock materialPropertyBlock = new global::UnityEngine.MaterialPropertyBlock();
				materialPropertyBlock.SetMatrixArray("_NormalToWorld", new global::UnityEngine.Matrix4x4[global::UnityEngine.Rendering.Universal.DecalDrawSystem.MaxBatchSize]);
				materialPropertyBlock.SetFloatArray("_DecalLayerMaskFromDecal", new float[global::UnityEngine.Rendering.Universal.DecalDrawSystem.MaxBatchSize]);
				entityChunks.Add(new global::UnityEngine.Rendering.Universal.DecalEntityChunk
				{
					material = material
				});
				cachedChunks.Add(new global::UnityEngine.Rendering.Universal.DecalCachedChunk
				{
					propertyBlock = materialPropertyBlock
				});
				culledChunks.Add(new global::UnityEngine.Rendering.Universal.DecalCulledChunk());
				drawCallChunks.Add(new global::UnityEngine.Rendering.Universal.DecalDrawCallChunk
				{
					subCallCounts = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent)
				});
				m_CombinedChunks.Add(default(global::UnityEngine.Rendering.Universal.DecalEntityManager.CombinedChunks));
				m_CombinedChunkRemmap.Add(0);
				m_MaterialToChunkIndex.Add(material, chunkCount);
				return chunkCount++;
			}
			return value;
		}

		public void UpdateAllDecalEntitiesData()
		{
			foreach (global::UnityEngine.Rendering.Universal.DecalEntityChunk entityChunk in entityChunks)
			{
				for (int i = 0; i < entityChunk.count; i++)
				{
					global::UnityEngine.Rendering.Universal.DecalProjector decalProjector = entityChunk.decalProjectors[i];
					if (!(decalProjector == null))
					{
						global::UnityEngine.Rendering.Universal.DecalEntity decalEntity = entityChunk.decalEntities[i];
						if (IsValid(decalEntity))
						{
							UpdateDecalEntityData(decalEntity, decalProjector);
						}
					}
				}
			}
		}

		public void UpdateDecalEntityData(global::UnityEngine.Rendering.Universal.DecalEntity decalEntity, global::UnityEngine.Rendering.Universal.DecalProjector decalProjector)
		{
			global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem item = m_DecalEntityIndexer.GetItem(decalEntity);
			int chunkIndex = item.chunkIndex;
			int arrayIndex = item.arrayIndex;
			global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk = cachedChunks[chunkIndex];
			decalCachedChunk.sizeOffsets[arrayIndex] = global::UnityEngine.Matrix4x4.Translate(decalProjector.decalOffset) * global::UnityEngine.Matrix4x4.Scale(decalProjector.decalSize);
			float drawDistance = decalProjector.drawDistance;
			float fadeScale = decalProjector.fadeScale;
			float startAngleFade = decalProjector.startAngleFade;
			float endAngleFade = decalProjector.endAngleFade;
			global::UnityEngine.Vector4 uvScaleBias = decalProjector.uvScaleBias;
			int layer = decalProjector.gameObject.layer;
			ulong sceneCullingMask = decalProjector.gameObject.sceneCullingMask;
			float fadeFactor = decalProjector.fadeFactor;
			decalCachedChunk.drawDistances[arrayIndex] = new global::UnityEngine.Vector2(drawDistance, fadeScale);
			if (startAngleFade == 180f)
			{
				decalCachedChunk.angleFades[arrayIndex] = new global::UnityEngine.Vector2(0f, 0f);
			}
			else
			{
				float num = startAngleFade / 180f;
				float num2 = endAngleFade / 180f;
				float num3 = global::UnityEngine.Mathf.Max(0.0001f, num2 - num);
				decalCachedChunk.angleFades[arrayIndex] = new global::UnityEngine.Vector2(1f - (0.25f - num) / num3, -0.25f / num3);
			}
			decalCachedChunk.uvScaleBias[arrayIndex] = uvScaleBias;
			decalCachedChunk.layerMasks[arrayIndex] = layer;
			decalCachedChunk.sceneLayerMasks[arrayIndex] = sceneCullingMask;
			decalCachedChunk.fadeFactors[arrayIndex] = fadeFactor;
			decalCachedChunk.scaleModes[arrayIndex] = decalProjector.scaleMode;
			decalCachedChunk.renderingLayerMasks[arrayIndex] = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.ToValidRenderingLayers(decalProjector.renderingLayerMask);
			decalCachedChunk.positions[arrayIndex] = decalProjector.transform.position;
			decalCachedChunk.rotation[arrayIndex] = decalProjector.transform.rotation;
			decalCachedChunk.scales[arrayIndex] = decalProjector.transform.lossyScale;
			decalCachedChunk.dirty[arrayIndex] = true;
		}

		public void DestroyDecalEntity(global::UnityEngine.Rendering.Universal.DecalEntity decalEntity)
		{
			if (m_DecalEntityIndexer.IsValid(decalEntity))
			{
				global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem item = m_DecalEntityIndexer.GetItem(decalEntity);
				m_DecalEntityIndexer.DestroyDecalEntity(decalEntity);
				int chunkIndex = item.chunkIndex;
				int arrayIndex = item.arrayIndex;
				global::UnityEngine.Rendering.Universal.DecalEntityChunk decalEntityChunk = entityChunks[chunkIndex];
				global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk = cachedChunks[chunkIndex];
				global::UnityEngine.Rendering.Universal.DecalCulledChunk decalCulledChunk = culledChunks[chunkIndex];
				global::UnityEngine.Rendering.Universal.DecalDrawCallChunk decalDrawCallChunk = drawCallChunks[chunkIndex];
				int num = decalEntityChunk.count - 1;
				if (arrayIndex != num)
				{
					m_DecalEntityIndexer.UpdateIndex(decalEntityChunk.decalEntities[num], arrayIndex);
				}
				decalEntityChunk.RemoveAtSwapBack(arrayIndex);
				decalCachedChunk.RemoveAtSwapBack(arrayIndex);
				decalCulledChunk.RemoveAtSwapBack(arrayIndex);
				decalDrawCallChunk.RemoveAtSwapBack(arrayIndex);
			}
		}

		public void Update()
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(m_SortChunks))
			{
				for (int i = 0; i < chunkCount; i++)
				{
					if (entityChunks[i].material == null)
					{
						entityChunks[i].material = errorMaterial;
					}
				}
				for (int j = 0; j < chunkCount; j++)
				{
					m_CombinedChunks[j] = new global::UnityEngine.Rendering.Universal.DecalEntityManager.CombinedChunks
					{
						entityChunk = entityChunks[j],
						cachedChunk = cachedChunks[j],
						culledChunk = culledChunks[j],
						drawCallChunk = drawCallChunks[j],
						previousChunkIndex = j,
						valid = (entityChunks[j].count != 0)
					};
				}
				m_CombinedChunks.Sort(delegate(global::UnityEngine.Rendering.Universal.DecalEntityManager.CombinedChunks a, global::UnityEngine.Rendering.Universal.DecalEntityManager.CombinedChunks b)
				{
					if (a.valid && !b.valid)
					{
						return -1;
					}
					if (!a.valid && b.valid)
					{
						return 1;
					}
					if (a.cachedChunk.drawOrder < b.cachedChunk.drawOrder)
					{
						return -1;
					}
					return (a.cachedChunk.drawOrder > b.cachedChunk.drawOrder) ? 1 : a.entityChunk.material.GetHashCode().CompareTo(b.entityChunk.material.GetHashCode());
				});
				bool flag = false;
				for (int num = 0; num < chunkCount; num++)
				{
					if (m_CombinedChunks[num].previousChunkIndex != num || !m_CombinedChunks[num].valid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return;
				}
				int num2 = 0;
				m_MaterialToChunkIndex.Clear();
				for (int num3 = 0; num3 < chunkCount; num3++)
				{
					global::UnityEngine.Rendering.Universal.DecalEntityManager.CombinedChunks combinedChunks = m_CombinedChunks[num3];
					if (!m_CombinedChunks[num3].valid)
					{
						combinedChunks.entityChunk.currentJobHandle.Complete();
						combinedChunks.cachedChunk.currentJobHandle.Complete();
						combinedChunks.culledChunk.currentJobHandle.Complete();
						combinedChunks.drawCallChunk.currentJobHandle.Complete();
						combinedChunks.entityChunk.Dispose();
						combinedChunks.cachedChunk.Dispose();
						combinedChunks.culledChunk.Dispose();
						combinedChunks.drawCallChunk.Dispose();
						continue;
					}
					entityChunks[num3] = combinedChunks.entityChunk;
					cachedChunks[num3] = combinedChunks.cachedChunk;
					culledChunks[num3] = combinedChunks.culledChunk;
					drawCallChunks[num3] = combinedChunks.drawCallChunk;
					if (!m_MaterialToChunkIndex.ContainsKey(entityChunks[num3].material))
					{
						m_MaterialToChunkIndex.Add(entityChunks[num3].material, num3);
					}
					m_CombinedChunkRemmap[combinedChunks.previousChunkIndex] = num3;
					num2++;
				}
				if (chunkCount > num2)
				{
					entityChunks.RemoveRange(num2, chunkCount - num2);
					cachedChunks.RemoveRange(num2, chunkCount - num2);
					culledChunks.RemoveRange(num2, chunkCount - num2);
					drawCallChunks.RemoveRange(num2, chunkCount - num2);
					m_CombinedChunks.RemoveRange(num2, chunkCount - num2);
					chunkCount = num2;
				}
				m_DecalEntityIndexer.RemapChunkIndices(m_CombinedChunkRemmap);
			}
		}

		public void Dispose()
		{
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_ErrorMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_DecalProjectorMesh);
			foreach (global::UnityEngine.Rendering.Universal.DecalEntityChunk entityChunk in entityChunks)
			{
				entityChunk.currentJobHandle.Complete();
			}
			foreach (global::UnityEngine.Rendering.Universal.DecalCachedChunk cachedChunk in cachedChunks)
			{
				cachedChunk.currentJobHandle.Complete();
			}
			foreach (global::UnityEngine.Rendering.Universal.DecalCulledChunk culledChunk in culledChunks)
			{
				culledChunk.currentJobHandle.Complete();
			}
			foreach (global::UnityEngine.Rendering.Universal.DecalDrawCallChunk drawCallChunk in drawCallChunks)
			{
				drawCallChunk.currentJobHandle.Complete();
			}
			foreach (global::UnityEngine.Rendering.Universal.DecalEntityChunk entityChunk2 in entityChunks)
			{
				entityChunk2.Dispose();
			}
			foreach (global::UnityEngine.Rendering.Universal.DecalCachedChunk cachedChunk2 in cachedChunks)
			{
				cachedChunk2.Dispose();
			}
			foreach (global::UnityEngine.Rendering.Universal.DecalCulledChunk culledChunk2 in culledChunks)
			{
				culledChunk2.Dispose();
			}
			foreach (global::UnityEngine.Rendering.Universal.DecalDrawCallChunk drawCallChunk2 in drawCallChunks)
			{
				drawCallChunk2.Dispose();
			}
			m_DecalEntityIndexer.Clear();
			m_MaterialToChunkIndex.Clear();
			entityChunks.Clear();
			cachedChunks.Clear();
			culledChunks.Clear();
			drawCallChunks.Clear();
			m_CombinedChunks.Clear();
			chunkCount = 0;
		}
	}
}
