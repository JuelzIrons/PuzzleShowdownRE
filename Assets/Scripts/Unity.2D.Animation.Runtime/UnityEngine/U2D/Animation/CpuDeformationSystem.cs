namespace UnityEngine.U2D.Animation
{
	internal class CpuDeformationSystem : global::UnityEngine.U2D.Animation.BaseDeformationSystem
	{
		private const string k_GpuSkinningShaderKeyword = "SKINNED_SPRITE";

		private global::Unity.Jobs.JobHandle m_CopyJobHandle;

		public override global::UnityEngine.U2D.Animation.DeformationMethods deformationMethod => global::UnityEngine.U2D.Animation.DeformationMethods.Cpu;

		internal override void Cleanup()
		{
			base.Cleanup();
			m_CopyJobHandle.Complete();
		}

		internal override void UpdateMaterial(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			global::UnityEngine.Material sharedMaterial = spriteSkin.spriteRenderer.sharedMaterial;
			if (sharedMaterial.IsKeywordEnabled("SKINNED_SPRITE"))
			{
				sharedMaterial.DisableKeyword("SKINNED_SPRITE");
			}
		}

		internal override void Update()
		{
			BatchRemoveSpriteSkins();
			BatchAddSpriteSkins();
			if (m_SpriteSkins.Count == 0)
			{
				m_LocalToWorldTransformAccessJob.ResetCache();
				m_WorldToLocalTransformAccessJob.ResetCache();
				return;
			}
			PrepareDataForDeformation(out var localToWorldJobHandle, out var worldToLocalJobHandle);
			if (!GotVerticesToDeform(out var vertexBufferSize))
			{
				localToWorldJobHandle.Complete();
				worldToLocalJobHandle.Complete();
				DeactivateDeformableBuffers();
				return;
			}
			int frameCount = global::UnityEngine.Time.frameCount;
			global::UnityEngine.U2D.Animation.PerSkinJobData skinBatch = m_SkinBatchArray[0];
			ResizeBuffers(vertexBufferSize, in skinBatch);
			int length = m_SpriteSkinData.Length;
			global::Unity.Jobs.JobHandle job = SchedulePrepareJob(length);
			job = global::Unity.Jobs.JobHandle.CombineDependencies(localToWorldJobHandle, worldToLocalJobHandle, job);
			job = ScheduleBoneJobBatched(job, skinBatch);
			m_DeformJobHandle = ScheduleSkinDeformBatchedJob(job, skinBatch, length, frameCount);
			m_CopyJobHandle = ScheduleCopySpriteRendererBuffersJob(job, length);
			global::Unity.Jobs.JobHandle.ScheduleBatchedJobs();
			global::Unity.Jobs.JobHandle.CombineDependencies(m_DeformJobHandle, m_CopyJobHandle).Complete();
			using (global::UnityEngine.U2D.Animation.BaseDeformationSystem.Profiling.setBatchDeformableBufferAndLocalAABB.Auto())
			{
				global::UnityEngine.U2D.Common.InternalEngineBridge.SetBatchDeformableBufferAndLocalAABBArray(m_SpriteRenderers, m_Buffers, m_BufferSizes, m_BoundsData);
			}
			foreach (global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin in m_SpriteSkins)
			{
				if (m_IsSpriteSkinActiveForDeform[spriteSkin.dataIndex] && m_LastDeformedFrame[spriteSkin.dataIndex] == frameCount)
				{
					spriteSkin.PostDeform();
				}
			}
			DeactivateDeformableBuffers();
		}
	}
}
