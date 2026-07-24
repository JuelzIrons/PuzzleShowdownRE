namespace UnityEngine.Rendering
{
	internal class CPUDrawInstanceData
	{
		private global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> m_RangeHash;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> m_DrawRanges;

		private global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> m_BatchHash;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> m_DrawBatches;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> m_DrawInstances;

		private global::Unity.Collections.NativeList<int> m_DrawInstanceIndices;

		private global::Unity.Collections.NativeList<int> m_DrawBatchIndices;

		private bool m_NeedsRebuild;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances => m_DrawInstances;

		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash => m_BatchHash;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches => m_DrawBatches;

		public global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash => m_RangeHash;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges => m_DrawRanges;

		public global::Unity.Collections.NativeArray<int> drawBatchIndices => m_DrawBatchIndices.AsArray();

		public global::Unity.Collections.NativeArray<int> drawInstanceIndices => m_DrawInstanceIndices.AsArray();

		public bool valid => m_DrawInstances.IsCreated;

		public void Initialize()
		{
			m_RangeHash = new global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int>(1024, global::Unity.Collections.Allocator.Persistent);
			m_DrawRanges = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange>(global::Unity.Collections.Allocator.Persistent);
			m_BatchHash = new global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int>(1024, global::Unity.Collections.Allocator.Persistent);
			m_DrawBatches = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch>(global::Unity.Collections.Allocator.Persistent);
			m_DrawInstances = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance>(1024, global::Unity.Collections.Allocator.Persistent);
			m_DrawInstanceIndices = new global::Unity.Collections.NativeList<int>(1024, global::Unity.Collections.Allocator.Persistent);
			m_DrawBatchIndices = new global::Unity.Collections.NativeList<int>(1024, global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			if (m_DrawBatchIndices.IsCreated)
			{
				m_DrawBatchIndices.Dispose();
			}
			if (m_DrawInstanceIndices.IsCreated)
			{
				m_DrawInstanceIndices.Dispose();
			}
			if (m_DrawInstances.IsCreated)
			{
				m_DrawInstances.Dispose();
			}
			if (m_DrawBatches.IsCreated)
			{
				m_DrawBatches.Dispose();
			}
			if (m_BatchHash.IsCreated)
			{
				m_BatchHash.Dispose();
			}
			if (m_DrawRanges.IsCreated)
			{
				m_DrawRanges.Dispose();
			}
			if (m_RangeHash.IsCreated)
			{
				m_RangeHash.Dispose();
			}
		}

		public void RebuildDrawListsIfNeeded()
		{
			if (m_NeedsRebuild)
			{
				m_NeedsRebuild = false;
				m_DrawInstanceIndices.ResizeUninitialized(m_DrawInstances.Length);
				m_DrawBatchIndices.ResizeUninitialized(m_DrawBatches.Length);
				global::Unity.Collections.NativeArray<int> internalDrawIndex = new global::Unity.Collections.NativeArray<int>(drawBatches.Length * 16, global::Unity.Collections.Allocator.TempJob);
				global::Unity.Jobs.JobHandle dependsOn = global::Unity.Jobs.IJobExtensions.Schedule(new global::UnityEngine.Rendering.PrefixSumDrawInstancesJob
				{
					rangeHash = m_RangeHash,
					drawRanges = m_DrawRanges,
					drawBatches = m_DrawBatches,
					drawBatchIndices = m_DrawBatchIndices.AsArray()
				});
				global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.BuildDrawListsJob
				{
					drawInstances = m_DrawInstances,
					batchHash = m_BatchHash,
					drawBatches = m_DrawBatches,
					internalDrawIndex = internalDrawIndex,
					drawInstanceIndices = m_DrawInstanceIndices.AsArray()
				}, m_DrawInstances.Length, 128, dependsOn).Complete();
				internalDrawIndex.Dispose();
			}
		}

		public void DestroyDrawInstanceIndices(global::Unity.Collections.NativeArray<int> drawInstanceIndicesToDestroy)
		{
			drawInstanceIndicesToDestroy.ParallelSort().Complete();
			global::UnityEngine.Rendering.InstanceCullingBatcherBurst.RemoveDrawInstanceIndices(in drawInstanceIndicesToDestroy, ref m_DrawInstances, ref m_RangeHash, ref m_BatchHash, ref m_DrawRanges, ref m_DrawBatches);
		}

		public void DestroyDrawInstances(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> destroyedInstances)
		{
			if (!m_DrawInstances.IsEmpty && destroyedInstances.Length != 0)
			{
				NeedsRebuild();
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instancesSorted = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(destroyedInstances, global::Unity.Collections.Allocator.TempJob);
				instancesSorted.Reinterpret<int>().ParallelSort().Complete();
				global::Unity.Collections.NativeList<int> nativeList = new global::Unity.Collections.NativeList<int>(m_DrawInstances.Length, global::Unity.Collections.Allocator.TempJob);
				global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.FindDrawInstancesJob
				{
					instancesSorted = instancesSorted,
					drawInstances = m_DrawInstances,
					outDrawInstanceIndicesWriter = nativeList.AsParallelWriter()
				}, m_DrawInstances.Length, 128).Complete();
				DestroyDrawInstanceIndices(nativeList.AsArray());
				instancesSorted.Dispose();
				nativeList.Dispose();
			}
		}

		public void DestroyMaterialDrawInstances(global::Unity.Collections.NativeArray<uint> destroyedBatchMaterials)
		{
			if (!m_DrawInstances.IsEmpty && destroyedBatchMaterials.Length != 0)
			{
				NeedsRebuild();
				global::Unity.Collections.NativeArray<uint> materialsSorted = new global::Unity.Collections.NativeArray<uint>(destroyedBatchMaterials, global::Unity.Collections.Allocator.TempJob);
				materialsSorted.Reinterpret<int>().ParallelSort().Complete();
				global::Unity.Collections.NativeList<int> nativeList = new global::Unity.Collections.NativeList<int>(m_DrawInstances.Length, global::Unity.Collections.Allocator.TempJob);
				global::Unity.Jobs.IJobParallelForBatchExtensions.ScheduleBatch(new global::UnityEngine.Rendering.FindMaterialDrawInstancesJob
				{
					materialsSorted = materialsSorted,
					drawInstances = m_DrawInstances,
					outDrawInstanceIndicesWriter = nativeList.AsParallelWriter()
				}, m_DrawInstances.Length, 128).Complete();
				DestroyDrawInstanceIndices(nativeList.AsArray());
				materialsSorted.Dispose();
				nativeList.Dispose();
			}
		}

		public void NeedsRebuild()
		{
			m_NeedsRebuild = true;
		}
	}
}
