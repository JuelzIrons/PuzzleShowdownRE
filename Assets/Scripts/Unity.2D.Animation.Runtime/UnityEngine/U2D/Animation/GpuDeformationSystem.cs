namespace UnityEngine.U2D.Animation
{
	internal class GpuDeformationSystem : global::UnityEngine.U2D.Animation.BaseDeformationSystem
	{
		private const string k_GpuSkinningShaderKeyword = "SKINNED_SPRITE";

		private const string k_GlobalSpriteBoneBufferId = "_SpriteBoneTransforms";

		private readonly global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Material> m_KeywordEnabledMaterials = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Material>();

		private global::Unity.Collections.NativeArray<int> m_BoneTransformBufferSizes;

		private global::UnityEngine.ComputeBuffer m_BoneTransformsComputeBuffer;

		private static global::UnityEngine.ComputeBuffer s_FallbackBuffer;

		public override global::UnityEngine.U2D.Animation.DeformationMethods deformationMethod => global::UnityEngine.U2D.Animation.DeformationMethods.Gpu;

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void CreateFallbackBuffer()
		{
			if (s_FallbackBuffer == null)
			{
				s_FallbackBuffer = new global::UnityEngine.ComputeBuffer(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Mathematics.float4x4>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Mathematics.float4x4>(), global::UnityEngine.ComputeBufferType.Default);
			}
			global::UnityEngine.Shader.SetGlobalBuffer("_SpriteBoneTransforms", s_FallbackBuffer);
		}

		private static void ClearFallbackBuffer()
		{
			if (s_FallbackBuffer != null)
			{
				s_FallbackBuffer.Release();
			}
			s_FallbackBuffer = null;
		}

		internal static bool DoesShaderSupportGpuDeformation(global::UnityEngine.Material material)
		{
			if (material == null)
			{
				return false;
			}
			global::UnityEngine.Shader shader = material.shader;
			if (shader == null)
			{
				return false;
			}
			global::UnityEngine.Rendering.LocalKeyword[] keywords = shader.keywordSpace.keywords;
			for (int i = 0; i < keywords.Length; i++)
			{
				if (keywords[i].name == "SKINNED_SPRITE")
				{
					return true;
				}
			}
			return false;
		}

		private static bool IsComputeBufferValid(global::UnityEngine.ComputeBuffer buffer)
		{
			return buffer?.IsValid() ?? false;
		}

		protected override void InitializeArrays()
		{
			base.InitializeArrays();
			m_BoneTransformBuffers = new global::Unity.Collections.NativeArray<global::System.IntPtr>(0, global::Unity.Collections.Allocator.Persistent);
			m_BoneTransformBufferSizes = new global::Unity.Collections.NativeArray<int>(0, global::Unity.Collections.Allocator.Persistent);
			CreateFallbackBuffer();
		}

		internal override void Cleanup()
		{
			base.Cleanup();
			m_BoneTransformBuffers.DisposeIfCreated();
			m_BoneTransformBufferSizes.DisposeIfCreated();
			CleanupComputeResources();
			ClearFallbackBuffer();
		}

		protected override void ResizeAndCopyArrays(int updatedCount)
		{
			base.ResizeAndCopyArrays(updatedCount);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeAndCopyIfNeeded(ref m_BoneTransformBuffers, updatedCount);
			global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeAndCopyIfNeeded(ref m_BoneTransformBufferSizes, updatedCount);
			if (updatedCount == 0)
			{
				CleanupComputeResources();
			}
		}

		private void CleanupComputeResources()
		{
			if (IsComputeBufferValid(m_BoneTransformsComputeBuffer))
			{
				m_BoneTransformsComputeBuffer.Release();
			}
			m_BoneTransformsComputeBuffer = null;
			foreach (global::UnityEngine.Material value in m_KeywordEnabledMaterials.Values)
			{
				value.DisableKeyword("SKINNED_SPRITE");
			}
			m_KeywordEnabledMaterials.Clear();
			global::UnityEngine.Shader.SetGlobalBuffer("_SpriteBoneTransforms", s_FallbackBuffer);
		}

		internal override void UpdateMaterial(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			global::UnityEngine.Material sharedMaterial = spriteSkin.spriteRenderer.sharedMaterial;
			if (!sharedMaterial.IsKeywordEnabled("SKINNED_SPRITE"))
			{
				sharedMaterial.EnableKeyword("SKINNED_SPRITE");
			}
		}

		internal override bool AddSpriteSkin(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			bool result = base.AddSpriteSkin(spriteSkin);
			global::UnityEngine.Material sharedMaterial = spriteSkin.spriteRenderer.sharedMaterial;
			if (!sharedMaterial.IsKeywordEnabled("SKINNED_SPRITE"))
			{
				sharedMaterial.EnableKeyword("SKINNED_SPRITE");
				m_KeywordEnabledMaterials.TryAdd(sharedMaterial.GetInstanceID(), sharedMaterial);
			}
			return result;
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
			global::UnityEngine.U2D.Animation.PerSkinJobData skinBatch = m_SkinBatchArray[0];
			ResizeBuffers(vertexBufferSize, in skinBatch);
			int length = m_SpriteSkinData.Length;
			global::Unity.Jobs.JobHandle job = SchedulePrepareJob(length);
			job = global::Unity.Jobs.JobHandle.CombineDependencies(localToWorldJobHandle, worldToLocalJobHandle, job);
			job = ScheduleBoneJobBatched(job, skinBatch);
			m_DeformJobHandle = ScheduleSkinDeformBatchedJob(job, skinBatch, length, global::UnityEngine.Time.frameCount);
			job = ScheduleCopySpriteRendererBoneTransformBuffersJob(m_DeformJobHandle, length);
			global::Unity.Jobs.JobHandle.ScheduleBatchedJobs();
			job.Complete();
			using (global::UnityEngine.U2D.Animation.BaseDeformationSystem.Profiling.setBoneTransformsArray.Auto())
			{
				global::UnityEngine.U2D.Common.InternalEngineBridge.SetBatchBoneTransformsAABBArray(m_SpriteRenderers, m_BoneTransformBuffers, m_BoneTransformBufferSizes, m_BoundsData);
			}
			SetComputeBuffer();
			DeactivateDeformableBuffers();
		}

		protected override void ResizeBuffers(int vertexBufferSize, in global::UnityEngine.U2D.Animation.PerSkinJobData skinBatch)
		{
			base.ResizeBuffers(vertexBufferSize, in skinBatch);
			int y = skinBatch.bindPosesIndex.y;
			if (!IsComputeBufferValid(m_BoneTransformsComputeBuffer) || m_BoneTransformsComputeBuffer.count < y)
			{
				CreateComputeBuffer(y);
			}
		}

		private void CreateComputeBuffer(int bufferSize)
		{
			if (IsComputeBufferValid(m_BoneTransformsComputeBuffer))
			{
				m_BoneTransformsComputeBuffer.Release();
			}
			m_BoneTransformsComputeBuffer = new global::UnityEngine.ComputeBuffer(bufferSize, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Mathematics.float4x4>(), global::UnityEngine.ComputeBufferType.Default);
			SetComputeBuffer();
		}

		private void SetComputeBuffer()
		{
			m_BoneTransformsComputeBuffer.SetData(m_FinalBoneTransforms, 0, 0, m_FinalBoneTransforms.Length);
			global::UnityEngine.Shader.SetGlobalBuffer("_SpriteBoneTransforms", m_BoneTransformsComputeBuffer);
		}

		private unsafe global::Unity.Jobs.JobHandle ScheduleCopySpriteRendererBoneTransformBuffersJob(global::Unity.Jobs.JobHandle jobHandle, int batchCount)
		{
			return global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.U2D.Animation.CopySpriteRendererBoneTransformBuffersJob
			{
				isSpriteSkinValidForDeformArray = m_IsSpriteSkinActiveForDeform,
				spriteSkinData = m_SpriteSkinData,
				ptrBoneTransforms = (global::System.IntPtr)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(m_FinalBoneTransforms),
				perSkinJobData = m_PerSkinJobData,
				buffers = m_BoneTransformBuffers,
				bufferSizes = m_BoneTransformBufferSizes
			}, batchCount, 16, jobHandle);
		}
	}
}
