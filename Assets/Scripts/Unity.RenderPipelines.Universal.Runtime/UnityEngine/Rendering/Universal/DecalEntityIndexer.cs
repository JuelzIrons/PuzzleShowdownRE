namespace UnityEngine.Rendering.Universal
{
	internal class DecalEntityIndexer
	{
		public struct DecalEntityItem
		{
			public int chunkIndex;

			public int arrayIndex;

			public int version;
		}

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem> m_Entities = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem>();

		private global::System.Collections.Generic.Queue<int> m_FreeIndices = new global::System.Collections.Generic.Queue<int>();

		public bool IsValid(global::UnityEngine.Rendering.Universal.DecalEntity decalEntity)
		{
			if (m_Entities.Count <= decalEntity.index)
			{
				return false;
			}
			return m_Entities[decalEntity.index].version == decalEntity.version;
		}

		public global::UnityEngine.Rendering.Universal.DecalEntity CreateDecalEntity(int arrayIndex, int chunkIndex)
		{
			if (m_FreeIndices.Count != 0)
			{
				int index = m_FreeIndices.Dequeue();
				int version = m_Entities[index].version + 1;
				m_Entities[index] = new global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem
				{
					arrayIndex = arrayIndex,
					chunkIndex = chunkIndex,
					version = version
				};
				return new global::UnityEngine.Rendering.Universal.DecalEntity
				{
					index = index,
					version = version
				};
			}
			int count = m_Entities.Count;
			int version2 = 1;
			m_Entities.Add(new global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem
			{
				arrayIndex = arrayIndex,
				chunkIndex = chunkIndex,
				version = version2
			});
			return new global::UnityEngine.Rendering.Universal.DecalEntity
			{
				index = count,
				version = version2
			};
		}

		public void DestroyDecalEntity(global::UnityEngine.Rendering.Universal.DecalEntity decalEntity)
		{
			m_FreeIndices.Enqueue(decalEntity.index);
			global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem value = m_Entities[decalEntity.index];
			value.version++;
			m_Entities[decalEntity.index] = value;
		}

		public global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem GetItem(global::UnityEngine.Rendering.Universal.DecalEntity decalEntity)
		{
			return m_Entities[decalEntity.index];
		}

		public void UpdateIndex(global::UnityEngine.Rendering.Universal.DecalEntity decalEntity, int newArrayIndex)
		{
			global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem value = m_Entities[decalEntity.index];
			value.arrayIndex = newArrayIndex;
			value.version = decalEntity.version;
			m_Entities[decalEntity.index] = value;
		}

		public void RemapChunkIndices(global::System.Collections.Generic.List<int> remaper)
		{
			for (int i = 0; i < m_Entities.Count; i++)
			{
				int chunkIndex = remaper[m_Entities[i].chunkIndex];
				global::UnityEngine.Rendering.Universal.DecalEntityIndexer.DecalEntityItem value = m_Entities[i];
				value.chunkIndex = chunkIndex;
				m_Entities[i] = value;
			}
		}

		public void Clear()
		{
			m_Entities.Clear();
			m_FreeIndices.Clear();
		}
	}
}
