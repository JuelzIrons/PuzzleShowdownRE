namespace UnityEngine.Rendering.Universal
{
	internal class DecalUpdateCachedSystem
	{
		[global::Unity.Burst.BurstCompile]
		public struct UpdateTransformsJob : global::UnityEngine.Jobs.IJobParallelForTransform
		{
			private static readonly global::Unity.Mathematics.quaternion k_MinusYtoZRotation = global::Unity.Mathematics.quaternion.EulerXYZ(-global::System.MathF.PI / 2f, 0f, 0f);

			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> positions;

			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.quaternion> rotations;

			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> scales;

			public global::Unity.Collections.NativeArray<bool> dirty;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.DecalScaleMode> scaleModes;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> sizeOffsets;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> decalToWorlds;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> normalToWorlds;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.BoundingSphere> boundingSpheres;

			public float minDistance;

			private float DistanceBetweenQuaternions(global::Unity.Mathematics.quaternion a, global::Unity.Mathematics.quaternion b)
			{
				return global::Unity.Mathematics.math.distancesq(a.value, b.value);
			}

			public void Execute(int index, global::UnityEngine.Jobs.TransformAccess transform)
			{
				bool num = global::Unity.Mathematics.math.distancesq(transform.position, positions[index]) > minDistance;
				if (num)
				{
					positions[index] = transform.position;
				}
				bool flag = DistanceBetweenQuaternions(transform.rotation, rotations[index]) > minDistance;
				if (flag)
				{
					rotations[index] = transform.rotation;
				}
				bool flag2 = global::Unity.Mathematics.math.distancesq(transform.localScale, scales[index]) > minDistance;
				if (flag2)
				{
					scales[index] = transform.localScale;
				}
				if (num || flag || flag2 || dirty[index])
				{
					global::Unity.Mathematics.float4x4 a;
					if (scaleModes[index] == global::UnityEngine.Rendering.Universal.DecalScaleMode.InheritFromHierarchy)
					{
						a = transform.localToWorldMatrix;
						a = global::Unity.Mathematics.math.mul(a, new global::Unity.Mathematics.float4x4(k_MinusYtoZRotation, global::Unity.Mathematics.float3.zero));
					}
					else
					{
						global::Unity.Mathematics.quaternion rotation = global::Unity.Mathematics.math.mul(transform.rotation, k_MinusYtoZRotation);
						a = global::Unity.Mathematics.float4x4.TRS(positions[index], rotation, new global::Unity.Mathematics.float3(1f, 1f, 1f));
					}
					global::Unity.Mathematics.float4x4 value = a;
					global::Unity.Mathematics.float4 c = value.c1;
					value.c1 = value.c2;
					value.c2 = c;
					normalToWorlds[index] = value;
					global::Unity.Mathematics.float4x4 b = sizeOffsets[index];
					global::Unity.Mathematics.float4x4 float4x5 = global::Unity.Mathematics.math.mul(a, b);
					decalToWorlds[index] = float4x5;
					boundingSpheres[index] = GetDecalProjectBoundingSphere(float4x5);
					dirty[index] = false;
				}
			}

			private global::UnityEngine.BoundingSphere GetDecalProjectBoundingSphere(global::UnityEngine.Matrix4x4 decalToWorld)
			{
				global::Unity.Mathematics.float4 b = new global::Unity.Mathematics.float4(-0.5f, -0.5f, -0.5f, 1f);
				global::Unity.Mathematics.float4 b2 = new global::Unity.Mathematics.float4(0.5f, 0.5f, 0.5f, 1f);
				b = global::Unity.Mathematics.math.mul(decalToWorld, b);
				b2 = global::Unity.Mathematics.math.mul(decalToWorld, b2);
				global::Unity.Mathematics.float3 xyz = ((b2 + b) / 2f).xyz;
				float radius = global::Unity.Mathematics.math.length(b2 - b) / 2f;
				return new global::UnityEngine.BoundingSphere
				{
					position = xyz,
					radius = radius
				};
			}
		}

		private global::UnityEngine.Rendering.Universal.DecalEntityManager m_EntityManager;

		private global::UnityEngine.Rendering.ProfilingSampler m_Sampler;

		private global::UnityEngine.Rendering.ProfilingSampler m_SamplerJob;

		public DecalUpdateCachedSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager)
		{
			m_EntityManager = entityManager;
			m_Sampler = new global::UnityEngine.Rendering.ProfilingSampler("DecalUpdateCachedSystem.Execute");
			m_SamplerJob = new global::UnityEngine.Rendering.ProfilingSampler("DecalUpdateCachedSystem.ExecuteJob");
		}

		public void Execute()
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(m_Sampler))
			{
				for (int i = 0; i < m_EntityManager.chunkCount; i++)
				{
					Execute(m_EntityManager.entityChunks[i], m_EntityManager.cachedChunks[i], m_EntityManager.entityChunks[i].count);
				}
			}
		}

		private void Execute(global::UnityEngine.Rendering.Universal.DecalEntityChunk entityChunk, global::UnityEngine.Rendering.Universal.DecalCachedChunk cachedChunk, int count)
		{
			if (count == 0)
			{
				return;
			}
			cachedChunk.currentJobHandle.Complete();
			global::UnityEngine.Material material = entityChunk.material;
			if (material.HasProperty("_DrawOrder"))
			{
				cachedChunk.drawOrder = material.GetInt("_DrawOrder");
			}
			if (!cachedChunk.isCreated)
			{
				int passIndexDBuffer = material.FindPass("DBufferProjector");
				cachedChunk.passIndexDBuffer = passIndexDBuffer;
				int passIndexEmissive = material.FindPass("DecalProjectorForwardEmissive");
				cachedChunk.passIndexEmissive = passIndexEmissive;
				int passIndexScreenSpace = material.FindPass("DecalScreenSpaceProjector");
				cachedChunk.passIndexScreenSpace = passIndexScreenSpace;
				int passIndexGBuffer = material.FindPass("DecalGBufferProjector");
				cachedChunk.passIndexGBuffer = passIndexGBuffer;
				cachedChunk.isCreated = true;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(m_SamplerJob))
			{
				global::Unity.Jobs.JobHandle currentJobHandle = global::UnityEngine.Jobs.IJobParallelForTransformExtensions.Schedule(new global::UnityEngine.Rendering.Universal.DecalUpdateCachedSystem.UpdateTransformsJob
				{
					positions = cachedChunk.positions,
					rotations = cachedChunk.rotation,
					scales = cachedChunk.scales,
					dirty = cachedChunk.dirty,
					scaleModes = cachedChunk.scaleModes,
					sizeOffsets = cachedChunk.sizeOffsets,
					decalToWorlds = cachedChunk.decalToWorlds,
					normalToWorlds = cachedChunk.normalToWorlds,
					boundingSpheres = cachedChunk.boundingSpheres,
					minDistance = float.Epsilon
				}, entityChunk.transformAccessArray);
				cachedChunk.currentJobHandle = currentJobHandle;
			}
		}
	}
}
