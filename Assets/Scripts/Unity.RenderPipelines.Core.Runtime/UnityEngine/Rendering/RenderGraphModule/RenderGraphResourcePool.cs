namespace UnityEngine.Rendering.RenderGraphModule
{
	internal abstract class RenderGraphResourcePool<Type> : global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResourcePool where Type : class
	{
		private struct ResourceLogInfo
		{
			public string name;

			public long size;
		}

		protected global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.SortedList<int, (Type resource, int frameIndex)>> m_ResourcePool = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.SortedList<int, (Type, int)>>();

		private global::System.Collections.Generic.List<(int, Type)> m_FrameAllocatedResources = new global::System.Collections.Generic.List<(int, Type)>();

		private const int kStaleResourceLifetime = 10;

		private static global::System.Collections.Generic.List<int> s_ToRemoveList = new global::System.Collections.Generic.List<int>(32);

		protected abstract void ReleaseInternalResource(Type res);

		protected abstract string GetResourceName(in Type res);

		protected abstract long GetResourceSize(in Type res);

		protected abstract string GetResourceTypeName();

		protected abstract int GetSortIndex(Type res);

		public void ReleaseResource(int hash, Type resource, int currentFrameIndex)
		{
			if (!m_ResourcePool.TryGetValue(hash, out global::System.Collections.Generic.SortedList<int, (Type, int)> value))
			{
				value = new global::System.Collections.Generic.SortedList<int, (Type, int)>();
				m_ResourcePool.Add(hash, value);
			}
			value.Add(GetSortIndex(resource), (resource, currentFrameIndex));
		}

		public bool TryGetResource(int hashCode, out Type resource)
		{
			if (m_ResourcePool.TryGetValue(hashCode, out global::System.Collections.Generic.SortedList<int, (Type, int)> value) && value.Count > 0)
			{
				int index = value.Count - 1;
				resource = value.Values[index].Item1;
				value.RemoveAt(index);
				return true;
			}
			resource = null;
			return false;
		}

		public override void Cleanup()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.SortedList<int, (Type, int)>> item in m_ResourcePool)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<int, (Type, int)> item2 in item.Value)
				{
					ReleaseInternalResource(item2.Value.Item1);
				}
			}
			m_ResourcePool.Clear();
			m_FrameAllocatedResources.Clear();
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		public void RegisterFrameAllocation(int hash, Type value)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks && hash != -1)
			{
				m_FrameAllocatedResources.Add((hash, value));
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		public void UnregisterFrameAllocation(int hash, Type value)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks && hash != -1)
			{
				m_FrameAllocatedResources.Remove((hash, value));
			}
		}

		public override void CheckFrameAllocation(bool onException, int frameIndex)
		{
		}

		public override void LogResources(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger logger)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<Type>.ResourceLogInfo> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<Type>.ResourceLogInfo>();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.SortedList<int, (Type, int)>> item2 in m_ResourcePool)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<int, (Type, int)> item3 in item2.Value)
				{
					global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<Type>.ResourceLogInfo item = default(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<Type>.ResourceLogInfo);
					(Type, int) value = item3.Value;
					item.name = GetResourceName(in value.Item1);
					(Type, int) value2 = item3.Value;
					item.size = GetResourceSize(in value2.Item1);
					list.Add(item);
				}
			}
			logger.LogLine("== " + GetResourceTypeName() + " Resources ==");
			list.Sort((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<Type>.ResourceLogInfo a, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<Type>.ResourceLogInfo b) => (a.size < b.size) ? 1 : (-1));
			int num = 0;
			float num2 = 0f;
			foreach (global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<Type>.ResourceLogInfo item4 in list)
			{
				float num3 = (float)item4.size / 1048576f;
				num2 += num3;
				logger.LogLine($"[{num++:D2}]\t[{num3:0.00} MB]\t{item4.name}");
			}
			logger.LogLine($"\nTotal Size [{num2:0.00}]");
		}

		public float GetMemorySizeInMB()
		{
			float num = 0f;
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.SortedList<int, (Type, int)>> item in m_ResourcePool)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<int, (Type, int)> item2 in item.Value)
				{
					float num2 = num;
					(Type, int) value = item2.Value;
					num = num2 + (float)GetResourceSize(in value.Item1) / 1048576f;
				}
			}
			return num;
		}

		public int GetNumResourcesAvailable()
		{
			int num = 0;
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.SortedList<int, (Type, int)>> item in m_ResourcePool)
			{
				num += item.Value.Count;
			}
			return num;
		}

		public override void PurgeUnusedResources(int currentFrameIndex)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.SortedList<int, (Type, int)>> item2 in m_ResourcePool)
			{
				s_ToRemoveList.Clear();
				global::System.Collections.Generic.SortedList<int, (Type, int)> value = item2.Value;
				global::System.Collections.Generic.IList<int> keys = value.Keys;
				global::System.Collections.Generic.IList<(Type, int)> values = value.Values;
				for (int i = 0; i < value.Count; i++)
				{
					(Type, int) tuple = values[i];
					int item = keys[i];
					if (tuple.Item2 + 10 < currentFrameIndex)
					{
						ReleaseInternalResource(tuple.Item1);
						s_ToRemoveList.Add(item);
					}
				}
				for (int j = 0; j < s_ToRemoveList.Count; j++)
				{
					value.Remove(s_ToRemoveList[j]);
				}
			}
		}
	}
}
