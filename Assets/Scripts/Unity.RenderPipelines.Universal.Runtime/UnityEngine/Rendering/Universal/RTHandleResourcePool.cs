namespace UnityEngine.Rendering.Universal
{
	internal class RTHandleResourcePool
	{
		protected global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle resource, int frameIndex)>> m_ResourcePool = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle, int)>>();

		protected global::System.Collections.Generic.List<int> m_RemoveList = new global::System.Collections.Generic.List<int>(32);

		protected static int s_CurrentStaleResourceCount = 0;

		protected static int s_StaleResourceLifetime = 3;

		protected static int s_StaleResourceMaxCapacity = 32;

		internal int staleResourceCapacity
		{
			get
			{
				return s_StaleResourceMaxCapacity;
			}
			set
			{
				if (s_StaleResourceMaxCapacity != value)
				{
					s_StaleResourceMaxCapacity = value;
					Cleanup();
				}
			}
		}

		internal bool AddResourceToPool(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc texDesc, global::UnityEngine.Rendering.RTHandle resource, int currentFrameIndex)
		{
			if (s_CurrentStaleResourceCount >= s_StaleResourceMaxCapacity)
			{
				return false;
			}
			int hashCodeWithNameHash = GetHashCodeWithNameHash(in texDesc);
			if (!m_ResourcePool.TryGetValue(hashCodeWithNameHash, out global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle, int)> value))
			{
				value = new global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle, int)>(s_StaleResourceMaxCapacity);
				m_ResourcePool.Add(hashCodeWithNameHash, value);
			}
			value.Add(resource.GetInstanceID(), (resource, currentFrameIndex));
			s_CurrentStaleResourceCount++;
			return true;
		}

		internal bool TryGetResource(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc texDesc, out global::UnityEngine.Rendering.RTHandle resource, bool usepool = true)
		{
			int hashCodeWithNameHash = GetHashCodeWithNameHash(in texDesc);
			if (usepool && m_ResourcePool.TryGetValue(hashCodeWithNameHash, out global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle, int)> value) && value.Count > 0)
			{
				resource = value.Values[value.Count - 1].Item1;
				value.RemoveAt(value.Count - 1);
				s_CurrentStaleResourceCount--;
				return true;
			}
			resource = null;
			return false;
		}

		internal void Cleanup()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle, int)>> item in m_ResourcePool)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<int, (global::UnityEngine.Rendering.RTHandle, int)> item2 in item.Value)
				{
					item2.Value.Item1.Release();
				}
			}
			m_ResourcePool.Clear();
			s_CurrentStaleResourceCount = 0;
		}

		protected static bool ShouldReleaseResource(int lastUsedFrameIndex, int currentFrameIndex)
		{
			return lastUsedFrameIndex + s_StaleResourceLifetime < currentFrameIndex;
		}

		internal void PurgeUnusedResources(int currentFrameIndex)
		{
			m_RemoveList.Clear();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle, int)>> item in m_ResourcePool)
			{
				global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle, int)> value = item.Value;
				global::System.Collections.Generic.IList<int> keys = value.Keys;
				global::System.Collections.Generic.IList<(global::UnityEngine.Rendering.RTHandle, int)> values = value.Values;
				for (int i = 0; i < value.Count; i++)
				{
					(global::UnityEngine.Rendering.RTHandle, int) tuple = values[i];
					if (ShouldReleaseResource(tuple.Item2, currentFrameIndex))
					{
						tuple.Item1.Release();
						m_RemoveList.Add(keys[i]);
						s_CurrentStaleResourceCount--;
					}
				}
				foreach (int remove in m_RemoveList)
				{
					value.Remove(remove);
				}
			}
		}

		internal void LogDebugInfo()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.AppendFormat("RTHandleResourcePool for frame {0}, Total stale resources {1}", global::UnityEngine.Time.frameCount, s_CurrentStaleResourceCount);
			stringBuilder.AppendLine();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle, int)>> item in m_ResourcePool)
			{
				global::System.Collections.Generic.SortedList<int, (global::UnityEngine.Rendering.RTHandle, int)> value = item.Value;
				_ = value.Keys;
				global::System.Collections.Generic.IList<(global::UnityEngine.Rendering.RTHandle, int)> values = value.Values;
				for (int i = 0; i < value.Count; i++)
				{
					(global::UnityEngine.Rendering.RTHandle, int) tuple = values[i];
					stringBuilder.AppendFormat("Resrouce in pool: Name {0} Last active frame index {1} Size {2} x {3} x {4}", tuple.Item1.name, tuple.Item2, tuple.Item1.rt.descriptor.width, tuple.Item1.rt.descriptor.height, tuple.Item1.rt.descriptor.volumeDepth);
					stringBuilder.AppendLine();
				}
			}
			global::UnityEngine.Debug.Log(stringBuilder);
		}

		internal int GetHashCodeWithNameHash(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc texDesc)
		{
			return texDesc.GetHashCode() * 23 + texDesc.name.GetHashCode();
		}

		internal static global::UnityEngine.Rendering.RenderGraphModule.TextureDesc CreateTextureDesc(global::UnityEngine.RenderTextureDescriptor desc, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode textureSizeMode = global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit, int anisoLevel = 1, float mipMapBias = 0f, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Clamp, string name = "")
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format = ((desc.depthStencilFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None) ? desc.depthStencilFormat : desc.graphicsFormat);
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc result = new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(desc.width, desc.height);
			result.sizeMode = textureSizeMode;
			result.slices = desc.volumeDepth;
			result.format = format;
			result.filterMode = filterMode;
			result.wrapMode = wrapMode;
			result.dimension = desc.dimension;
			result.enableRandomWrite = desc.enableRandomWrite;
			result.useMipMap = desc.useMipMap;
			result.autoGenerateMips = desc.autoGenerateMips;
			result.isShadowMap = desc.shadowSamplingMode != global::UnityEngine.Rendering.ShadowSamplingMode.None;
			result.anisoLevel = anisoLevel;
			result.mipMapBias = mipMapBias;
			result.msaaSamples = (global::UnityEngine.Rendering.MSAASamples)desc.msaaSamples;
			result.bindTextureMS = desc.bindMS;
			result.useDynamicScale = desc.useDynamicScale;
			result.memoryless = global::UnityEngine.RenderTextureMemoryless.None;
			result.vrUsage = global::UnityEngine.VRTextureUsage.None;
			result.name = name;
			result.enableShadingRate = desc.enableShadingRate;
			return result;
		}
	}
}
