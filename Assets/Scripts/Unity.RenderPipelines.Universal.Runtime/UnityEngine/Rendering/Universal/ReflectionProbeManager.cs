namespace UnityEngine.Rendering.Universal
{
	internal struct ReflectionProbeManager : global::System.IDisposable
	{
		private struct CachedProbe
		{
			public uint updateCount;

			public global::UnityEngine.Hash128 imageContentsHash;

			public int size;

			public int mipCount;

			public unsafe fixed int dataIndices[7];

			public unsafe fixed int levels[7];

			public global::UnityEngine.Texture texture;

			public int lastUsed;

			public global::UnityEngine.Vector4 hdrData;
		}

		private static class ShaderProperties
		{
			public static readonly int BoxMin = global::UnityEngine.Shader.PropertyToID("urp_ReflProbes_BoxMin");

			public static readonly int BoxMax = global::UnityEngine.Shader.PropertyToID("urp_ReflProbes_BoxMax");

			public static readonly int ProbePosition = global::UnityEngine.Shader.PropertyToID("urp_ReflProbes_ProbePosition");

			public static readonly int MipScaleOffset = global::UnityEngine.Shader.PropertyToID("urp_ReflProbes_MipScaleOffset");

			public static readonly int Count = global::UnityEngine.Shader.PropertyToID("urp_ReflProbes_Count");

			public static readonly int Atlas = global::UnityEngine.Shader.PropertyToID("urp_ReflProbes_Atlas");

			public static readonly int Rotation = global::UnityEngine.Shader.PropertyToID("urp_ReflProbes_Rotation");
		}

		private global::Unity.Mathematics.int2 m_Resolution;

		private global::UnityEngine.RenderTexture m_AtlasTexture0;

		private global::UnityEngine.RenderTexture m_AtlasTexture1;

		private global::UnityEngine.Rendering.RTHandle m_AtlasTexture0Handle;

		private global::UnityEngine.Rendering.Universal.BuddyAllocator m_AtlasAllocator;

		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.Universal.ReflectionProbeManager.CachedProbe> m_Cache;

		private global::System.Collections.Generic.Dictionary<int, int> m_WarningCache;

		private global::System.Collections.Generic.List<int> m_NeedsUpdate;

		private global::System.Collections.Generic.List<int> m_NeedsRemove;

		private global::UnityEngine.Vector4[] m_BoxMax;

		private global::UnityEngine.Vector4[] m_BoxMin;

		private global::UnityEngine.Vector4[] m_ProbePosition;

		private global::UnityEngine.Vector4[] m_MipScaleOffset;

		private global::UnityEngine.Vector4[] m_Rotations;

		private const int k_MaxMipCount = 7;

		private const string k_ReflectionProbeAtlasName = "URP Reflection Probe Atlas";

		public global::UnityEngine.RenderTexture atlasRT => m_AtlasTexture0;

		public global::UnityEngine.Rendering.RTHandle atlasRTHandle => m_AtlasTexture0Handle;

		public static global::UnityEngine.Rendering.Universal.ReflectionProbeManager Create()
		{
			global::UnityEngine.Rendering.Universal.ReflectionProbeManager result = default(global::UnityEngine.Rendering.Universal.ReflectionProbeManager);
			result.Init();
			return result;
		}

		private void Init()
		{
			int maxVisibleReflectionProbes = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleReflectionProbes;
			m_Resolution = 1;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32;
			if (!global::UnityEngine.SystemInfo.IsFormatSupported(graphicsFormat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render))
			{
				graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat;
			}
			m_AtlasTexture0 = new global::UnityEngine.RenderTexture(new global::UnityEngine.RenderTextureDescriptor
			{
				width = m_Resolution.x,
				height = m_Resolution.y,
				volumeDepth = 1,
				dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D,
				graphicsFormat = graphicsFormat,
				useMipMap = false,
				msaaSamples = 1
			});
			m_AtlasTexture0.name = "URP Reflection Probe Atlas";
			m_AtlasTexture0.filterMode = global::UnityEngine.FilterMode.Bilinear;
			m_AtlasTexture0.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			m_AtlasTexture0.Create();
			m_AtlasTexture0Handle = global::UnityEngine.Rendering.RTHandles.Alloc(m_AtlasTexture0, transferOwnership: true);
			m_AtlasTexture1 = new global::UnityEngine.RenderTexture(m_AtlasTexture0.descriptor);
			m_AtlasTexture1.name = "URP Reflection Probe Atlas";
			m_AtlasTexture1.filterMode = global::UnityEngine.FilterMode.Bilinear;
			m_AtlasTexture1.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			m_AtlasAllocator = new global::UnityEngine.Rendering.Universal.BuddyAllocator(global::Unity.Mathematics.math.floorlog2(global::UnityEngine.SystemInfo.maxTextureSize) - 2, 2);
			m_Cache = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.Universal.ReflectionProbeManager.CachedProbe>(maxVisibleReflectionProbes);
			m_WarningCache = new global::System.Collections.Generic.Dictionary<int, int>(maxVisibleReflectionProbes);
			m_NeedsUpdate = new global::System.Collections.Generic.List<int>(maxVisibleReflectionProbes);
			m_NeedsRemove = new global::System.Collections.Generic.List<int>(maxVisibleReflectionProbes);
			m_BoxMax = new global::UnityEngine.Vector4[maxVisibleReflectionProbes];
			m_BoxMin = new global::UnityEngine.Vector4[maxVisibleReflectionProbes];
			m_ProbePosition = new global::UnityEngine.Vector4[maxVisibleReflectionProbes];
			m_MipScaleOffset = new global::UnityEngine.Vector4[maxVisibleReflectionProbes * 7];
			m_Rotations = new global::UnityEngine.Vector4[maxVisibleReflectionProbes];
		}

		public unsafe void UpdateGpuData(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.CullingResults cullResults)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleReflectionProbe> visibleReflectionProbes = cullResults.visibleReflectionProbes;
			int num = global::Unity.Mathematics.math.min(visibleReflectionProbes.Length, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleReflectionProbes);
			int renderedFrameCount = global::UnityEngine.Time.renderedFrameCount;
			int key;
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::UnityEngine.Rendering.Universal.ReflectionProbeManager.CachedProbe> item3 in m_Cache)
			{
				item3.Deconstruct(out key, out var value);
				int item = key;
				global::UnityEngine.Rendering.Universal.ReflectionProbeManager.CachedProbe cachedProbe = value;
				if (global::System.Math.Abs(cachedProbe.lastUsed - renderedFrameCount) <= 1 && (bool)cachedProbe.texture && cachedProbe.size == cachedProbe.texture.width)
				{
					continue;
				}
				m_NeedsRemove.Add(item);
				for (int i = 0; i < 7; i++)
				{
					if (cachedProbe.dataIndices[i] != -1)
					{
						m_AtlasAllocator.Free(new global::UnityEngine.Rendering.Universal.BuddyAllocation(cachedProbe.levels[i], cachedProbe.dataIndices[i]));
					}
				}
			}
			foreach (int item4 in m_NeedsRemove)
			{
				m_Cache.Remove(item4);
			}
			m_NeedsRemove.Clear();
			foreach (global::System.Collections.Generic.KeyValuePair<int, int> item5 in m_WarningCache)
			{
				item5.Deconstruct(out key, out var value2);
				int item2 = key;
				if (global::System.Math.Abs(value2 - renderedFrameCount) > 1)
				{
					m_NeedsRemove.Add(item2);
				}
			}
			foreach (int item6 in m_NeedsRemove)
			{
				m_WarningCache.Remove(item6);
			}
			m_NeedsRemove.Clear();
			bool flag = false;
			global::Unity.Mathematics.int2 int5 = global::Unity.Mathematics.math.int2(0, 0);
			for (int j = 0; j < num; j++)
			{
				global::UnityEngine.Rendering.VisibleReflectionProbe visibleReflectionProbe = visibleReflectionProbes[j];
				global::UnityEngine.Texture texture = visibleReflectionProbe.texture;
				int instanceID = visibleReflectionProbe.reflectionProbe.GetInstanceID();
				global::UnityEngine.Rendering.Universal.ReflectionProbeManager.CachedProbe value3;
				bool flag2 = m_Cache.TryGetValue(instanceID, out value3);
				if (!texture)
				{
					continue;
				}
				if (!flag2)
				{
					value3.size = texture.width;
					int num2 = global::Unity.Mathematics.math.ceillog2(value3.size * 4) + 1;
					int num3 = m_AtlasAllocator.levelCount + 2 - num2;
					value3.mipCount = global::Unity.Mathematics.math.min(num2, 7);
					value3.texture = texture;
					int k;
					for (k = 0; k < value3.mipCount; k++)
					{
						int num4 = global::Unity.Mathematics.math.min(num3 + k, m_AtlasAllocator.levelCount - 1);
						if (!m_AtlasAllocator.TryAllocate(num4, out var allocation))
						{
							break;
						}
						value3.levels[k] = allocation.level;
						value3.dataIndices[k] = allocation.index;
						global::Unity.Mathematics.int4 int6 = (global::Unity.Mathematics.int4)(GetScaleOffset(num4, allocation.index, includePadding: true, yflip: false) * m_Resolution.xyxy);
						int5 = global::Unity.Mathematics.math.max(int5, int6.zw + int6.xy);
					}
					if (k < value3.mipCount)
					{
						if (!m_WarningCache.ContainsKey(instanceID))
						{
							flag = true;
						}
						m_WarningCache[instanceID] = renderedFrameCount;
						for (int l = 0; l < k; l++)
						{
							m_AtlasAllocator.Free(new global::UnityEngine.Rendering.Universal.BuddyAllocation(value3.levels[l], value3.dataIndices[l]));
						}
						for (int m = 0; m < 7; m++)
						{
							value3.dataIndices[m] = -1;
						}
						continue;
					}
					for (; k < 7; k++)
					{
						value3.dataIndices[k] = -1;
					}
				}
				if ((!flag2 || value3.updateCount != texture.updateCount) | (value3.hdrData != visibleReflectionProbe.hdrData))
				{
					value3.updateCount = texture.updateCount;
					m_NeedsUpdate.Add(instanceID);
				}
				if (visibleReflectionProbe.reflectionProbe.mode == global::UnityEngine.Rendering.ReflectionProbeMode.Realtime && visibleReflectionProbe.reflectionProbe.refreshMode == global::UnityEngine.Rendering.ReflectionProbeRefreshMode.EveryFrame)
				{
					value3.lastUsed = -1;
				}
				else
				{
					value3.lastUsed = renderedFrameCount;
				}
				value3.hdrData = visibleReflectionProbe.hdrData;
				m_Cache[instanceID] = value3;
			}
			if (global::Unity.Mathematics.math.any(m_Resolution < int5))
			{
				int5 = global::Unity.Mathematics.math.max(m_Resolution, global::Unity.Mathematics.math.ceilpow2(int5));
				global::UnityEngine.RenderTextureDescriptor descriptor = m_AtlasTexture0.descriptor;
				descriptor.width = int5.x;
				descriptor.height = int5.y;
				m_AtlasTexture1.width = int5.x;
				m_AtlasTexture1.height = int5.y;
				m_AtlasTexture1.Create();
				if (m_AtlasTexture0.width != 1)
				{
					if (global::UnityEngine.SystemInfo.copyTextureSupport != global::UnityEngine.Rendering.CopyTextureSupport.None)
					{
						global::UnityEngine.Graphics.CopyTexture(m_AtlasTexture0, 0, 0, 0, 0, m_Resolution.x, m_Resolution.y, m_AtlasTexture1, 0, 0, 0, 0);
					}
					else
					{
						global::UnityEngine.Graphics.Blit(m_AtlasTexture0, m_AtlasTexture1, (global::Unity.Mathematics.float2)m_Resolution / (global::Unity.Mathematics.float2)int5, global::UnityEngine.Vector2.zero);
					}
				}
				m_AtlasTexture0.Release();
				global::UnityEngine.RenderTexture atlasTexture = m_AtlasTexture1;
				global::UnityEngine.RenderTexture atlasTexture2 = m_AtlasTexture0;
				m_AtlasTexture0 = atlasTexture;
				m_AtlasTexture1 = atlasTexture2;
				m_Resolution = int5;
			}
			int num5 = 0;
			for (int n = 0; n < num; n++)
			{
				global::UnityEngine.Rendering.VisibleReflectionProbe visibleReflectionProbe2 = visibleReflectionProbes[n];
				int instanceID2 = visibleReflectionProbe2.reflectionProbe.GetInstanceID();
				int num6 = n - num5;
				if (!m_Cache.TryGetValue(instanceID2, out var value4) || !visibleReflectionProbe2.texture)
				{
					num5++;
					continue;
				}
				m_BoxMax[num6] = new global::UnityEngine.Vector4(visibleReflectionProbe2.bounds.max.x, visibleReflectionProbe2.bounds.max.y, visibleReflectionProbe2.bounds.max.z, visibleReflectionProbe2.blendDistance);
				m_BoxMin[num6] = new global::UnityEngine.Vector4(visibleReflectionProbe2.bounds.min.x, visibleReflectionProbe2.bounds.min.y, visibleReflectionProbe2.bounds.min.z, visibleReflectionProbe2.importance);
				m_ProbePosition[num6] = new global::UnityEngine.Vector4(visibleReflectionProbe2.localToWorldMatrix.m03, visibleReflectionProbe2.localToWorldMatrix.m13, visibleReflectionProbe2.localToWorldMatrix.m23, (visibleReflectionProbe2.isBoxProjection ? 1 : (-1)) * value4.mipCount);
				for (int num7 = 0; num7 < value4.mipCount; num7++)
				{
					m_MipScaleOffset[num6 * 7 + num7] = GetScaleOffset(value4.levels[num7], value4.dataIndices[num7], includePadding: false, yflip: false);
				}
				global::UnityEngine.Quaternion quaternion2 = global::UnityEngine.Quaternion.Inverse(visibleReflectionProbe2.reflectionProbe.transform.rotation);
				m_Rotations[num6] = new global::UnityEngine.Vector4(quaternion2.x, quaternion2.y, quaternion2.z, quaternion2.w);
			}
			if (flag)
			{
				global::UnityEngine.Debug.LogWarning("A number of reflection probes have been skipped due to the reflection probe atlas being full.\nTo fix this, you can decrease the number or resolution of probes.");
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.UpdateReflectionProbeAtlas)))
			{
				cmd.SetRenderTarget(m_AtlasTexture0);
				foreach (int item7 in m_NeedsUpdate)
				{
					global::UnityEngine.Rendering.Universal.ReflectionProbeManager.CachedProbe cachedProbe2 = m_Cache[item7];
					for (int num8 = 0; num8 < cachedProbe2.mipCount; num8++)
					{
						int num9 = cachedProbe2.levels[num8];
						int dataIndex = cachedProbe2.dataIndices[num8];
						global::Unity.Mathematics.float4 scaleOffset = GetScaleOffset(num9, dataIndex, includePadding: true, !global::UnityEngine.SystemInfo.graphicsUVStartsAtTop);
						int num10 = (1 << m_AtlasAllocator.levelCount + 1 - num9) - 2;
						global::UnityEngine.Rendering.Blitter.BlitCubeToOctahedral2DQuadWithPadding(cmd, cachedProbe2.texture, new global::UnityEngine.Vector2(num10, num10), scaleOffset, num8, bilinear: true, 2, cachedProbe2.hdrData);
					}
				}
				cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.ReflectionProbeManager.ShaderProperties.BoxMin, m_BoxMin);
				cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.ReflectionProbeManager.ShaderProperties.BoxMax, m_BoxMax);
				cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.ReflectionProbeManager.ShaderProperties.ProbePosition, m_ProbePosition);
				cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.ReflectionProbeManager.ShaderProperties.MipScaleOffset, m_MipScaleOffset);
				cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.ReflectionProbeManager.ShaderProperties.Rotation, m_Rotations);
				cmd.SetGlobalFloat(global::UnityEngine.Rendering.Universal.ReflectionProbeManager.ShaderProperties.Count, num - num5);
				cmd.SetGlobalTexture(global::UnityEngine.Rendering.Universal.ReflectionProbeManager.ShaderProperties.Atlas, m_AtlasTexture0);
			}
			m_NeedsUpdate.Clear();
		}

		private global::Unity.Mathematics.float4 GetScaleOffset(int level, int dataIndex, bool includePadding, bool yflip)
		{
			int num = 1 << m_AtlasAllocator.levelCount + 1 - level;
			global::Unity.Mathematics.uint2 obj = global::UnityEngine.Rendering.Universal.SpaceFillingCurves.DecodeMorton2D((uint)dataIndex);
			global::Unity.Mathematics.float2 xy = (float)(num - ((!includePadding) ? 2 : 0)) / (global::Unity.Mathematics.float2)m_Resolution;
			global::Unity.Mathematics.float2 zw = ((global::Unity.Mathematics.float2)obj * (float)num + ((!includePadding) ? 1 : 0)) / m_Resolution;
			if (yflip)
			{
				zw.y = 1f - zw.y - xy.y;
			}
			return global::Unity.Mathematics.math.float4(xy, zw);
		}

		public void Dispose()
		{
			if ((bool)m_AtlasTexture0)
			{
				m_AtlasTexture0.Release();
				m_AtlasTexture0Handle.Release();
			}
			m_AtlasAllocator.Dispose();
			global::UnityEngine.Object.DestroyImmediate(m_AtlasTexture0);
			global::UnityEngine.Object.DestroyImmediate(m_AtlasTexture1);
			this = default(global::UnityEngine.Rendering.Universal.ReflectionProbeManager);
		}
	}
}
