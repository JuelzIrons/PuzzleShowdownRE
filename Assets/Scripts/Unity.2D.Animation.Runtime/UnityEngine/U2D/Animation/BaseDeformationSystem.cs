namespace UnityEngine.U2D.Animation
{
	internal abstract class BaseDeformationSystem
	{
		protected static class Profiling
		{
			public static readonly global::Unity.Profiling.ProfilerMarker transformAccessJob = new global::Unity.Profiling.ProfilerMarker("BaseDeformationSystem.TransformAccessJob");

			public static readonly global::Unity.Profiling.ProfilerMarker boneTransformsChangeDetection = new global::Unity.Profiling.ProfilerMarker("BaseDeformationSystem.BoneTransformsChangeDetection");

			public static readonly global::Unity.Profiling.ProfilerMarker getSpriteSkinBatchData = new global::Unity.Profiling.ProfilerMarker("BaseDeformationSystem.GetSpriteSkinBatchData");

			public static readonly global::Unity.Profiling.ProfilerMarker scheduleJobs = new global::Unity.Profiling.ProfilerMarker("BaseDeformationSystem.ScheduleJobs");

			public static readonly global::Unity.Profiling.ProfilerMarker setBatchDeformableBufferAndLocalAABB = new global::Unity.Profiling.ProfilerMarker("BaseDeformationSystem.SetBatchDeformableBufferAndLocalAABB");

			public static readonly global::Unity.Profiling.ProfilerMarker setBoneTransformsArray = new global::Unity.Profiling.ProfilerMarker("BaseDeformationSystem.SetBoneTransformsArray");
		}

		protected int m_ObjectId;

		protected readonly global::System.Collections.Generic.HashSet<global::UnityEngine.U2D.Animation.SpriteSkin> m_SpriteSkins = new global::System.Collections.Generic.HashSet<global::UnityEngine.U2D.Animation.SpriteSkin>();

		protected global::UnityEngine.SpriteRenderer[] m_SpriteRenderers = new global::UnityEngine.SpriteRenderer[0];

		private readonly global::System.Collections.Generic.HashSet<global::UnityEngine.U2D.Animation.SpriteSkin> m_SpriteSkinsToAdd = new global::System.Collections.Generic.HashSet<global::UnityEngine.U2D.Animation.SpriteSkin>();

		private readonly global::System.Collections.Generic.HashSet<global::UnityEngine.U2D.Animation.SpriteSkin> m_SpriteSkinsToRemove = new global::System.Collections.Generic.HashSet<global::UnityEngine.U2D.Animation.SpriteSkin>();

		private readonly global::System.Collections.Generic.List<int> m_TransformIdsToRemove = new global::System.Collections.Generic.List<int>();

		protected global::UnityEngine.U2D.Animation.NativeByteArray m_DeformedVerticesBuffer;

		protected global::UnityEngine.U2D.Animation.NativeByteArray m_PreviousDeformedVerticesBuffer;

		protected global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> m_FinalBoneTransforms;

		protected global::Unity.Collections.NativeArray<bool> m_IsSpriteSkinActiveForDeform;

		protected global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.SpriteSkinData> m_SpriteSkinData;

		protected global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.PerSkinJobData> m_PerSkinJobData;

		protected global::Unity.Collections.NativeArray<global::UnityEngine.Bounds> m_BoundsData;

		protected global::Unity.Collections.NativeArray<global::System.IntPtr> m_Buffers;

		protected global::Unity.Collections.NativeArray<int> m_BufferSizes;

		protected global::Unity.Collections.NativeArray<global::System.IntPtr> m_BoneTransformBuffers;

		protected global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> m_BoneLookupData;

		protected global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.PerSkinJobData> m_SkinBatchArray;

		protected global::Unity.Collections.NativeArray<bool> m_HasBoneTransformsChanged;

		protected global::Unity.Collections.NativeArray<int> m_LastDeformedFrame;

		protected global::UnityEngine.U2D.Animation.TransformAccessJob m_LocalToWorldTransformAccessJob;

		protected global::UnityEngine.U2D.Animation.TransformAccessJob m_WorldToLocalTransformAccessJob;

		protected global::Unity.Jobs.JobHandle m_DeformJobHandle;

		public abstract global::UnityEngine.U2D.Animation.DeformationMethods deformationMethod { get; }

		internal void RemoveBoneTransforms(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (!m_SpriteSkins.Contains(spriteSkin))
			{
				return;
			}
			m_LocalToWorldTransformAccessJob.RemoveTransformById(spriteSkin.rootBoneTransformId);
			global::Unity.Collections.NativeArray<int> boneTransformId = spriteSkin.boneTransformId;
			if (!(boneTransformId == default(global::Unity.Collections.NativeArray<int>)) && boneTransformId.IsCreated)
			{
				for (int i = 0; i < boneTransformId.Length; i++)
				{
					m_LocalToWorldTransformAccessJob.RemoveTransformById(boneTransformId[i]);
				}
			}
		}

		internal void AddBoneTransforms(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (!m_SpriteSkins.Contains(spriteSkin))
			{
				return;
			}
			m_LocalToWorldTransformAccessJob.AddTransform(spriteSkin.rootBone);
			if (spriteSkin.boneTransforms == null)
			{
				return;
			}
			global::UnityEngine.Transform[] boneTransforms = spriteSkin.boneTransforms;
			foreach (global::UnityEngine.Transform transform in boneTransforms)
			{
				if (transform != null)
				{
					m_LocalToWorldTransformAccessJob.AddTransform(transform);
				}
			}
		}

		internal virtual void UpdateMaterial(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
		}

		internal virtual bool AddSpriteSkin(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (!m_SpriteSkins.Contains(spriteSkin) && m_SpriteSkinsToAdd.Add(spriteSkin))
			{
				return true;
			}
			if (!m_SpriteSkinsToRemove.Contains(spriteSkin))
			{
				return false;
			}
			m_SpriteSkinsToAdd.Add(spriteSkin);
			return true;
		}

		internal void CopyToSpriteSkinData(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (!m_SpriteSkinData.IsCreated)
			{
				throw new global::System.InvalidOperationException("Sprite Skin Data not initialized.");
			}
			int dataIndex = spriteSkin.dataIndex;
			if (dataIndex >= 0 && dataIndex < m_SpriteSkinData.Length)
			{
				global::UnityEngine.U2D.Animation.SpriteSkinData data = default(global::UnityEngine.U2D.Animation.SpriteSkinData);
				spriteSkin.CopyToSpriteSkinData(ref data);
				m_SpriteSkinData[dataIndex] = data;
				m_SpriteRenderers[dataIndex] = spriteSkin.spriteRenderer;
			}
		}

		internal void RemoveSpriteSkin(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (!(spriteSkin == null))
			{
				if (m_SpriteSkins.Contains(spriteSkin) && m_SpriteSkinsToRemove.Add(spriteSkin))
				{
					m_TransformIdsToRemove.Add(spriteSkin.transform.GetInstanceID());
				}
				m_SpriteSkinsToAdd.Remove(spriteSkin);
				RemoveBoneTransforms(spriteSkin);
			}
		}

		internal global::System.Collections.Generic.HashSet<global::UnityEngine.U2D.Animation.SpriteSkin> GetSpriteSkins()
		{
			return m_SpriteSkins;
		}

		internal void Initialize(int objectId)
		{
			m_ObjectId = objectId;
			if (m_LocalToWorldTransformAccessJob == null)
			{
				m_LocalToWorldTransformAccessJob = new global::UnityEngine.U2D.Animation.TransformAccessJob();
			}
			if (m_WorldToLocalTransformAccessJob == null)
			{
				m_WorldToLocalTransformAccessJob = new global::UnityEngine.U2D.Animation.TransformAccessJob();
			}
			InitializeArrays();
			BatchRemoveSpriteSkins();
			BatchAddSpriteSkins();
			int num = 0;
			foreach (global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin in m_SpriteSkins)
			{
				spriteSkin.SetDataIndex(num++);
				CopyToSpriteSkinData(spriteSkin);
			}
		}

		protected virtual void InitializeArrays()
		{
			m_FinalBoneTransforms = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4>(0, global::Unity.Collections.Allocator.Persistent);
			m_BoneLookupData = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(0, global::Unity.Collections.Allocator.Persistent);
			m_SkinBatchArray = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.PerSkinJobData>(0, global::Unity.Collections.Allocator.Persistent);
			m_IsSpriteSkinActiveForDeform = new global::Unity.Collections.NativeArray<bool>(0, global::Unity.Collections.Allocator.Persistent);
			m_PerSkinJobData = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.PerSkinJobData>(0, global::Unity.Collections.Allocator.Persistent);
			m_SpriteSkinData = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Animation.SpriteSkinData>(0, global::Unity.Collections.Allocator.Persistent);
			m_BoundsData = new global::Unity.Collections.NativeArray<global::UnityEngine.Bounds>(0, global::Unity.Collections.Allocator.Persistent);
			m_Buffers = new global::Unity.Collections.NativeArray<global::System.IntPtr>(0, global::Unity.Collections.Allocator.Persistent);
			m_BufferSizes = new global::Unity.Collections.NativeArray<int>(0, global::Unity.Collections.Allocator.Persistent);
			m_HasBoneTransformsChanged = new global::Unity.Collections.NativeArray<bool>(0, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_LastDeformedFrame = new global::Unity.Collections.NativeArray<int>(0, global::Unity.Collections.Allocator.Persistent);
		}

		protected void BatchRemoveSpriteSkins()
		{
			m_WorldToLocalTransformAccessJob.RemoveTransformsIfNull();
			int count = m_SpriteSkinsToRemove.Count;
			if (count == 0)
			{
				return;
			}
			m_WorldToLocalTransformAccessJob.RemoveTransformsByIds(m_TransformIdsToRemove);
			int num = global::System.Math.Max(m_SpriteSkins.Count - count, 0);
			if (num == 0)
			{
				m_SpriteSkins.Clear();
			}
			else
			{
				foreach (global::UnityEngine.U2D.Animation.SpriteSkin item in m_SpriteSkinsToRemove)
				{
					m_SpriteSkins.Remove(item);
				}
			}
			int num2 = 0;
			foreach (global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin in m_SpriteSkins)
			{
				spriteSkin.SetDataIndex(num2++);
				CopyToSpriteSkinData(spriteSkin);
			}
			global::System.Array.Resize(ref m_SpriteRenderers, num);
			ResizeAndCopyArrays(num);
			m_TransformIdsToRemove.Clear();
			m_SpriteSkinsToRemove.Clear();
		}

		protected void BatchAddSpriteSkins()
		{
			if (m_SpriteSkinsToAdd.Count == 0)
			{
				return;
			}
			if (!m_IsSpriteSkinActiveForDeform.IsCreated)
			{
				throw new global::System.InvalidOperationException("SpriteSkinActiveForDeform not initialized.");
			}
			int num = m_SpriteSkins.Count + m_SpriteSkinsToAdd.Count;
			global::System.Array.Resize(ref m_SpriteRenderers, num);
			ResizeAndCopyArrays(num);
			foreach (global::UnityEngine.U2D.Animation.SpriteSkin item in m_SpriteSkinsToAdd)
			{
				if (!m_SpriteSkins.Add(item))
				{
					global::UnityEngine.Debug.LogError("Skin already exists! Name=" + item.name);
					continue;
				}
				UpdateMaterial(item);
				int count = m_SpriteSkins.Count;
				m_SpriteRenderers[count - 1] = item.spriteRenderer;
				m_WorldToLocalTransformAccessJob.AddTransform(item.transform);
				AddBoneTransforms(item);
				item.SetDataIndex(count - 1);
				CopyToSpriteSkinData(item);
			}
			m_SpriteSkinsToAdd.Clear();
		}

		protected virtual void ResizeAndCopyArrays(int updatedCount)
		{
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeAndCopyIfNeeded(ref m_IsSpriteSkinActiveForDeform, updatedCount);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeAndCopyIfNeeded(ref m_PerSkinJobData, updatedCount);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeAndCopyIfNeeded(ref m_Buffers, updatedCount);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeAndCopyIfNeeded(ref m_BufferSizes, updatedCount);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeAndCopyIfNeeded(ref m_SpriteSkinData, updatedCount);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeAndCopyIfNeeded(ref m_BoundsData, updatedCount);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeIfNeeded(ref m_HasBoneTransformsChanged, updatedCount, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeAndCopyIfNeeded(ref m_LastDeformedFrame, updatedCount);
		}

		protected virtual void ResizeBuffers(int vertexBufferSize, in global::UnityEngine.U2D.Animation.PerSkinJobData skinBatch)
		{
			if (m_DeformedVerticesBuffer != null)
			{
				m_PreviousDeformedVerticesBuffer = m_DeformedVerticesBuffer;
			}
			else
			{
				m_PreviousDeformedVerticesBuffer = global::UnityEngine.U2D.Animation.BufferManager.instance.GetBuffer(m_ObjectId, vertexBufferSize);
			}
			m_DeformedVerticesBuffer = global::UnityEngine.U2D.Animation.BufferManager.instance.GetBuffer(m_ObjectId, vertexBufferSize);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeIfNeeded(ref m_FinalBoneTransforms, skinBatch.bindPosesIndex.y);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeIfNeeded(ref m_BoneLookupData, skinBatch.bindPosesIndex.y);
		}

		internal virtual void Cleanup()
		{
			m_DeformJobHandle.Complete();
			m_SpriteSkins.Clear();
			m_SpriteRenderers = new global::UnityEngine.SpriteRenderer[0];
			global::UnityEngine.U2D.Animation.BufferManager.instance.ReturnBuffer(m_ObjectId);
			m_IsSpriteSkinActiveForDeform.DisposeIfCreated();
			m_PerSkinJobData.DisposeIfCreated();
			m_Buffers.DisposeIfCreated();
			m_BufferSizes.DisposeIfCreated();
			m_SpriteSkinData.DisposeIfCreated();
			m_BoneLookupData.DisposeIfCreated();
			m_SkinBatchArray.DisposeIfCreated();
			m_FinalBoneTransforms.DisposeIfCreated();
			m_BoundsData.DisposeIfCreated();
			m_HasBoneTransformsChanged.DisposeIfCreated();
			m_LastDeformedFrame.DisposeIfCreated();
			m_LocalToWorldTransformAccessJob.Destroy();
			m_WorldToLocalTransformAccessJob.Destroy();
		}

		internal abstract void Update();

		protected void PrepareDataForDeformation(out global::Unity.Jobs.JobHandle localToWorldJobHandle, out global::Unity.Jobs.JobHandle worldToLocalJobHandle)
		{
			ValidateSpriteSkinData();
			using (global::UnityEngine.U2D.Animation.BaseDeformationSystem.Profiling.transformAccessJob.Auto())
			{
				localToWorldJobHandle = m_LocalToWorldTransformAccessJob.StartLocalToWorldAndChangeDetectionJob();
				worldToLocalJobHandle = m_WorldToLocalTransformAccessJob.StartWorldToLocalJob();
			}
			using (global::UnityEngine.U2D.Animation.BaseDeformationSystem.Profiling.boneTransformsChangeDetection.Auto())
			{
				global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.U2D.Animation.BoneTransformsChangeDetectionJob
				{
					transformChanged = m_LocalToWorldTransformAccessJob.transformChanged,
					boneTransformIndex = m_LocalToWorldTransformAccessJob.transformData,
					spriteSkinData = m_SpriteSkinData,
					hasBoneTransformsChanged = m_HasBoneTransformsChanged
				}, m_SpriteSkinData.Length, 64, localToWorldJobHandle).Complete();
			}
			using (global::UnityEngine.U2D.Animation.BaseDeformationSystem.Profiling.getSpriteSkinBatchData.Auto())
			{
				global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeIfNeeded(ref m_SkinBatchArray, 1);
				global::Unity.Jobs.IJobExtensions.Run(new global::UnityEngine.U2D.Animation.FillPerSkinJobSingleThread
				{
					isSpriteSkinValidForDeformArray = m_IsSpriteSkinActiveForDeform,
					combinedSkinBatchArray = m_SkinBatchArray,
					spriteSkinDataArray = m_SpriteSkinData,
					perSkinJobDataArray = m_PerSkinJobData
				});
			}
		}

		private void ValidateSpriteSkinData()
		{
			foreach (global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin in m_SpriteSkins)
			{
				int dataIndex = spriteSkin.dataIndex;
				m_IsSpriteSkinActiveForDeform[dataIndex] = spriteSkin.BatchValidate();
				if (m_IsSpriteSkinActiveForDeform[dataIndex] && spriteSkin.NeedToUpdateDeformationCache())
				{
					CopyToSpriteSkinData(spriteSkin);
				}
			}
		}

		protected bool GotVerticesToDeform(out int vertexBufferSize)
		{
			vertexBufferSize = m_SkinBatchArray[0].deformVerticesStartPos;
			return vertexBufferSize > 0;
		}

		protected global::Unity.Jobs.JobHandle SchedulePrepareJob(int batchCount)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::UnityEngine.U2D.Animation.PrepareDeformJob
			{
				batchDataSize = batchCount,
				perSkinJobData = m_PerSkinJobData,
				boneLookupData = m_BoneLookupData
			});
		}

		protected global::Unity.Jobs.JobHandle ScheduleBoneJobBatched(global::Unity.Jobs.JobHandle jobHandle, global::UnityEngine.U2D.Animation.PerSkinJobData skinBatch)
		{
			jobHandle = global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.U2D.Animation.BoneDeformBatchedJob
			{
				boneTransform = m_LocalToWorldTransformAccessJob.transformMatrix,
				rootTransform = m_WorldToLocalTransformAccessJob.transformMatrix,
				spriteSkinData = m_SpriteSkinData,
				boneLookupData = m_BoneLookupData,
				finalBoneTransforms = m_FinalBoneTransforms,
				rootTransformIndex = m_WorldToLocalTransformAccessJob.transformData,
				boneTransformIndex = m_LocalToWorldTransformAccessJob.transformData
			}, skinBatch.bindPosesIndex.y, 8, jobHandle);
			return jobHandle;
		}

		protected global::Unity.Jobs.JobHandle ScheduleSkinDeformBatchedJob(global::Unity.Jobs.JobHandle jobHandle, global::UnityEngine.U2D.Animation.PerSkinJobData skinBatch, int spriteCount, int frameCount)
		{
			return global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.U2D.Animation.SkinDeformBatchedJob
			{
				spriteSkinData = m_SpriteSkinData,
				perSkinJobData = m_PerSkinJobData,
				finalBoneTransforms = m_FinalBoneTransforms,
				vertices = m_DeformedVerticesBuffer.array,
				previousVertices = m_PreviousDeformedVerticesBuffer.array,
				isSpriteSkinValidForDeformArray = m_IsSpriteSkinActiveForDeform,
				hasBoneTransformsChanged = m_HasBoneTransformsChanged,
				bounds = m_BoundsData,
				lastDeformedFrame = m_LastDeformedFrame,
				frameCount = frameCount
			}, spriteCount, 1, jobHandle);
		}

		protected unsafe global::Unity.Jobs.JobHandle ScheduleCopySpriteRendererBuffersJob(global::Unity.Jobs.JobHandle jobHandle, int batchCount)
		{
			return global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.U2D.Animation.CopySpriteRendererBuffersJob
			{
				isSpriteSkinValidForDeformArray = m_IsSpriteSkinActiveForDeform,
				spriteSkinData = m_SpriteSkinData,
				ptrVertices = (global::System.IntPtr)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(m_DeformedVerticesBuffer.array),
				buffers = m_Buffers,
				bufferSizes = m_BufferSizes
			}, batchCount, 16, jobHandle);
		}

		protected void DeactivateDeformableBuffers()
		{
			for (int i = 0; i < m_IsSpriteSkinActiveForDeform.Length; i++)
			{
				if (!m_IsSpriteSkinActiveForDeform[i] && !global::UnityEngine.U2D.Common.InternalEngineBridge.IsUsingDeformableBuffer(m_SpriteRenderers[i], global::System.IntPtr.Zero))
				{
					m_SpriteRenderers[i].DeactivateDeformableBuffer();
				}
			}
		}

		internal bool IsSpriteSkinActiveForDeformation(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			return m_IsSpriteSkinActiveForDeform[spriteSkin.dataIndex];
		}

		internal int GetLastDeformedFrame(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			return m_LastDeformedFrame[spriteSkin.dataIndex];
		}

		internal unsafe global::Unity.Collections.NativeArray<byte> GetDeformableBufferForSpriteSkin(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (!m_SpriteSkins.Contains(spriteSkin))
			{
				return default(global::Unity.Collections.NativeArray<byte>);
			}
			if (!m_DeformJobHandle.IsCompleted)
			{
				m_DeformJobHandle.Complete();
			}
			global::UnityEngine.U2D.Animation.SpriteSkinData spriteSkinData = m_SpriteSkinData[spriteSkin.dataIndex];
			if (spriteSkinData.deformVerticesStartPos < 0)
			{
				return default(global::Unity.Collections.NativeArray<byte>);
			}
			int length = spriteSkinData.spriteVertexCount * spriteSkinData.spriteVertexStreamSize;
			byte* unsafeReadOnlyPtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(m_DeformedVerticesBuffer.array);
			unsafeReadOnlyPtr += spriteSkinData.deformVerticesStartPos;
			return global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(unsafeReadOnlyPtr, length, global::Unity.Collections.Allocator.None);
		}
	}
}
