namespace UnityEngine.Rendering
{
	internal struct OccluderContext : global::System.IDisposable
	{
		private static class ShaderIDs
		{
			public static readonly int _SrcDepth = global::UnityEngine.Shader.PropertyToID("_SrcDepth");

			public static readonly int _DstDepth = global::UnityEngine.Shader.PropertyToID("_DstDepth");

			public static readonly int OccluderDepthPyramidConstants = global::UnityEngine.Shader.PropertyToID("OccluderDepthPyramidConstants");
		}

		public const int k_FirstDepthMipIndex = 3;

		public const int k_MaxOccluderMips = 8;

		public const int k_MaxSilhouettePlanes = 6;

		public const int k_MaxSubviewsPerView = 6;

		public int version;

		public global::UnityEngine.Vector2Int depthBufferSize;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OccluderDerivedData> subviewData;

		public int subviewValidMask;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OccluderMipBounds> occluderMipBounds;

		public global::UnityEngine.Vector2Int occluderMipLayoutSize;

		public global::UnityEngine.Vector2Int occluderDepthPyramidSize;

		public global::UnityEngine.Rendering.RTHandle occluderDepthPyramid;

		public int occlusionDebugOverlaySize;

		public global::UnityEngine.GraphicsBuffer occlusionDebugOverlay;

		public bool debugNeedsClear;

		public global::UnityEngine.ComputeBuffer constantBuffer;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OccluderDepthPyramidConstants> constantBufferData;

		public int subviewCount => subviewData.Length;

		public global::UnityEngine.Vector2 depthBufferSizeInOccluderPixels
		{
			get
			{
				int num = 8;
				return new global::UnityEngine.Vector2((float)depthBufferSize.x / (float)num, (float)depthBufferSize.y / (float)num);
			}
		}

		public bool IsSubviewValid(int subviewIndex)
		{
			if (subviewIndex < subviewCount)
			{
				return (subviewValidMask & (1 << subviewIndex)) != 0;
			}
			return false;
		}

		public void Dispose()
		{
			if (subviewData.IsCreated)
			{
				subviewData.Dispose();
			}
			if (occluderMipBounds.IsCreated)
			{
				occluderMipBounds.Dispose();
			}
			if (occluderDepthPyramid != null)
			{
				occluderDepthPyramid.Release();
				occluderDepthPyramid = null;
			}
			if (occlusionDebugOverlay != null)
			{
				occlusionDebugOverlay.Release();
				occlusionDebugOverlay = null;
			}
			if (constantBuffer != null)
			{
				constantBuffer.Release();
				constantBuffer = null;
			}
			if (constantBufferData.IsCreated)
			{
				constantBufferData.Dispose();
			}
		}

		private void UpdateMipBounds()
		{
			int num = 8;
			global::UnityEngine.Vector2Int vector2Int = (depthBufferSize + (num - 1) * global::UnityEngine.Vector2Int.one) / num;
			global::UnityEngine.Vector2Int zero = global::UnityEngine.Vector2Int.zero;
			global::UnityEngine.Vector2Int zero2 = global::UnityEngine.Vector2Int.zero;
			global::UnityEngine.Vector2Int size = vector2Int;
			if (!occluderMipBounds.IsCreated)
			{
				occluderMipBounds = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OccluderMipBounds>(8, global::Unity.Collections.Allocator.Persistent);
			}
			for (int i = 0; i < 8; i++)
			{
				occluderMipBounds[i] = new global::UnityEngine.Rendering.OccluderMipBounds
				{
					offset = zero2,
					size = size
				};
				zero.x = global::UnityEngine.Mathf.Max(zero.x, zero2.x + size.x);
				zero.y = global::UnityEngine.Mathf.Max(zero.y, zero2.y + size.y);
				if (i == 0)
				{
					zero2.x = 0;
					zero2.y += size.y;
				}
				else
				{
					zero2.x += size.x;
				}
				size.x = (size.x + 1) / 2;
				size.y = (size.y + 1) / 2;
			}
			occluderMipLayoutSize = zero;
		}

		private void AllocateTexturesIfNecessary(bool debugOverlayEnabled)
		{
			global::UnityEngine.Vector2Int vector2Int = new global::UnityEngine.Vector2Int(occluderMipLayoutSize.x, occluderMipLayoutSize.y * subviewCount);
			if (occluderDepthPyramidSize.x < vector2Int.x || occluderDepthPyramidSize.y < vector2Int.y)
			{
				if (occluderDepthPyramid != null)
				{
					occluderDepthPyramid.Release();
				}
				occluderDepthPyramidSize = vector2Int;
				occluderDepthPyramid = global::UnityEngine.Rendering.RTHandles.Alloc(occluderDepthPyramidSize.x, occluderDepthPyramidSize.y, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat, 1, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Clamp, global::UnityEngine.Rendering.TextureDimension.Tex2D, enableRandomWrite: true, useMipMap: false, autoGenerateMips: true, isShadowMap: false, 1, 0f, global::UnityEngine.Rendering.MSAASamples.None, bindTextureMS: false, useDynamicScale: false, useDynamicScaleExplicit: false, global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage.None, "Occluder Depths");
			}
			int num = (debugOverlayEnabled ? (vector2Int.x * vector2Int.y) : 0);
			if (occlusionDebugOverlaySize < num)
			{
				if (occlusionDebugOverlay != null)
				{
					occlusionDebugOverlay.Release();
				}
				occlusionDebugOverlaySize = num;
				debugNeedsClear = true;
				occlusionDebugOverlay = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, global::UnityEngine.GraphicsBuffer.UsageFlags.None, occlusionDebugOverlaySize + 4, 4);
			}
			if (num == 0)
			{
				if (occlusionDebugOverlay != null)
				{
					occlusionDebugOverlay.Release();
					occlusionDebugOverlay = null;
				}
				occlusionDebugOverlaySize = num;
			}
			if (constantBuffer == null)
			{
				constantBuffer = new global::UnityEngine.ComputeBuffer(1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Rendering.OccluderDepthPyramidConstants>(), global::UnityEngine.ComputeBufferType.Constant);
			}
			if (!constantBufferData.IsCreated)
			{
				constantBufferData = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OccluderDepthPyramidConstants>(1, global::Unity.Collections.Allocator.Persistent);
			}
		}

		internal static void SetKeyword(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, global::UnityEngine.ComputeShader cs, in global::UnityEngine.Rendering.LocalKeyword keyword, bool value)
		{
			if (value)
			{
				cmd.EnableKeyword(cs, in keyword);
			}
			else
			{
				cmd.DisableKeyword(cs, in keyword);
			}
		}

		private unsafe global::UnityEngine.Rendering.OccluderDepthPyramidConstants SetupFarDepthPyramidConstants(global::System.ReadOnlySpan<global::UnityEngine.Rendering.OccluderSubviewUpdate> occluderSubviewUpdates, global::Unity.Collections.NativeArray<global::UnityEngine.Plane> silhouettePlanes)
		{
			global::UnityEngine.Rendering.OccluderDepthPyramidConstants result = new global::UnityEngine.Rendering.OccluderDepthPyramidConstants
			{
				_OccluderMipLayoutSizeX = (uint)occluderMipLayoutSize.x,
				_OccluderMipLayoutSizeY = (uint)occluderMipLayoutSize.y
			};
			int length = occluderSubviewUpdates.Length;
			for (int i = 0; i < length; i++)
			{
				ref readonly global::UnityEngine.Rendering.OccluderSubviewUpdate reference = ref occluderSubviewUpdates[i];
				int subviewIndex = reference.subviewIndex;
				subviewData[subviewIndex] = global::UnityEngine.Rendering.OccluderDerivedData.FromParameters(in reference);
				subviewValidMask |= 1 << reference.subviewIndex;
				global::UnityEngine.Matrix4x4 inverse = (reference.gpuProjMatrix * reference.viewMatrix * global::UnityEngine.Matrix4x4.Translate(-reference.viewOffsetWorldSpace)).inverse;
				for (int j = 0; j < 16; j++)
				{
					result._InvViewProjMatrix[16 * i + j] = inverse[j];
				}
				result._SrcOffset[4 * i] = (uint)reference.depthOffset.x;
				result._SrcOffset[4 * i + 1] = (uint)reference.depthOffset.y;
				result._SrcOffset[4 * i + 2] = 0u;
				result._SrcOffset[4 * i + 3] = 0u;
				result._SrcSliceIndices |= (uint)((reference.depthSliceIndex & 0xF) << 4 * i);
				result._DstSubviewIndices |= (uint)(subviewIndex << 4 * i);
			}
			for (int k = 0; k < 6; k++)
			{
				global::UnityEngine.Plane plane = new global::UnityEngine.Plane(global::UnityEngine.Vector3.zero, 0f);
				if (k < silhouettePlanes.Length)
				{
					plane = silhouettePlanes[k];
				}
				result._SilhouettePlanes[4 * k] = plane.normal.x;
				result._SilhouettePlanes[4 * k + 1] = plane.normal.y;
				result._SilhouettePlanes[4 * k + 2] = plane.normal.z;
				result._SilhouettePlanes[4 * k + 3] = plane.distance;
			}
			result._SilhouettePlaneCount = (uint)silhouettePlanes.Length;
			return result;
		}

		public unsafe void CreateFarDepthPyramid(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, in global::UnityEngine.Rendering.OccluderParameters occluderParams, global::System.ReadOnlySpan<global::UnityEngine.Rendering.OccluderSubviewUpdate> occluderSubviewUpdates, in global::UnityEngine.Rendering.OccluderHandles occluderHandles, global::Unity.Collections.NativeArray<global::UnityEngine.Plane> silhouettePlanes, global::UnityEngine.ComputeShader occluderDepthPyramidCS, int occluderDepthDownscaleKernel)
		{
			global::UnityEngine.Rendering.OccluderDepthPyramidConstants value = SetupFarDepthPyramidConstants(occluderSubviewUpdates, silhouettePlanes);
			global::UnityEngine.Rendering.LocalKeyword keyword = new global::UnityEngine.Rendering.LocalKeyword(occluderDepthPyramidCS, "USE_SRC");
			global::UnityEngine.Rendering.LocalKeyword keyword2 = new global::UnityEngine.Rendering.LocalKeyword(occluderDepthPyramidCS, "SRC_IS_ARRAY");
			global::UnityEngine.Rendering.LocalKeyword keyword3 = new global::UnityEngine.Rendering.LocalKeyword(occluderDepthPyramidCS, "SRC_IS_MSAA");
			bool depthIsArray = occluderParams.depthIsArray;
			bool flag = ((global::UnityEngine.Rendering.RTHandle)occluderParams.depthTexture)?.isMSAAEnabled ?? false;
			int num = 11;
			for (int i = 0; i < num - 1; i += 4)
			{
				cmd.SetComputeTextureParam(occluderDepthPyramidCS, occluderDepthDownscaleKernel, global::UnityEngine.Rendering.OccluderContext.ShaderIDs._DstDepth, occluderHandles.occluderDepthPyramid);
				bool flag2 = i == 0;
				SetKeyword(cmd, occluderDepthPyramidCS, in keyword, flag2);
				SetKeyword(cmd, occluderDepthPyramidCS, in keyword2, flag2 && depthIsArray);
				SetKeyword(cmd, occluderDepthPyramidCS, in keyword3, flag2 && flag);
				if (flag2)
				{
					cmd.SetComputeTextureParam(occluderDepthPyramidCS, occluderDepthDownscaleKernel, global::UnityEngine.Rendering.OccluderContext.ShaderIDs._SrcDepth, occluderParams.depthTexture);
				}
				value._MipCount = (uint)global::System.Math.Min(num - 1 - i, 4);
				global::UnityEngine.Vector2Int vector2Int = global::UnityEngine.Vector2Int.zero;
				for (int j = 0; j < 5; j++)
				{
					global::UnityEngine.Vector2Int vector2Int2 = global::UnityEngine.Vector2Int.zero;
					global::UnityEngine.Vector2Int vector2Int3 = global::UnityEngine.Vector2Int.zero;
					int num2 = i + j;
					if (num2 == 0)
					{
						vector2Int3 = occluderParams.depthSize;
					}
					else
					{
						int num3 = num2 - 3;
						if (0 <= num3 && num3 < 8)
						{
							vector2Int2 = occluderMipBounds[num3].offset;
							vector2Int3 = occluderMipBounds[num3].size;
						}
					}
					if (j == 0)
					{
						vector2Int = vector2Int3;
					}
					value._MipOffsetAndSize[4 * j] = (uint)vector2Int2.x;
					value._MipOffsetAndSize[4 * j + 1] = (uint)vector2Int2.y;
					value._MipOffsetAndSize[4 * j + 2] = (uint)vector2Int3.x;
					value._MipOffsetAndSize[4 * j + 3] = (uint)vector2Int3.y;
				}
				constantBufferData[0] = value;
				cmd.SetBufferData(constantBuffer, constantBufferData);
				cmd.SetComputeConstantBufferParam(occluderDepthPyramidCS, global::UnityEngine.Rendering.OccluderContext.ShaderIDs.OccluderDepthPyramidConstants, constantBuffer, 0, constantBuffer.stride);
				cmd.DispatchCompute(occluderDepthPyramidCS, occluderDepthDownscaleKernel, (vector2Int.x + 15) / 16, (vector2Int.y + 15) / 16, occluderSubviewUpdates.Length);
			}
		}

		public global::UnityEngine.Rendering.OccluderHandles Import(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo info = new global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo
			{
				width = occluderDepthPyramidSize.x,
				height = occluderDepthPyramidSize.y,
				volumeDepth = 1,
				msaaSamples = 1,
				format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat,
				bindMS = false
			};
			global::UnityEngine.Rendering.OccluderHandles result = new global::UnityEngine.Rendering.OccluderHandles
			{
				occluderDepthPyramid = renderGraph.ImportTexture(occluderDepthPyramid, info)
			};
			if (occlusionDebugOverlay != null)
			{
				result.occlusionDebugOverlay = renderGraph.ImportBuffer(occlusionDebugOverlay);
			}
			return result;
		}

		public void PrepareOccluders(in global::UnityEngine.Rendering.OccluderParameters occluderParams)
		{
			if (subviewCount != occluderParams.subviewCount)
			{
				if (subviewData.IsCreated)
				{
					subviewData.Dispose();
				}
				subviewData = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OccluderDerivedData>(occluderParams.subviewCount, global::Unity.Collections.Allocator.Persistent);
				subviewValidMask = 0;
			}
			depthBufferSize = occluderParams.depthSize;
			bool debugOverlayEnabled = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats()?.occlusionOverlayEnabled ?? false;
			UpdateMipBounds();
			AllocateTexturesIfNecessary(debugOverlayEnabled);
		}

		internal unsafe global::UnityEngine.Rendering.OcclusionCullingDebugOutput GetDebugOutput()
		{
			global::UnityEngine.Rendering.OcclusionCullingDebugOutput result = new global::UnityEngine.Rendering.OcclusionCullingDebugOutput
			{
				occluderDepthPyramid = occluderDepthPyramid,
				occlusionDebugOverlay = occlusionDebugOverlay
			};
			result.cb._DepthSizeInOccluderPixels = depthBufferSizeInOccluderPixels;
			result.cb._OccluderMipLayoutSizeX = (uint)occluderMipLayoutSize.x;
			result.cb._OccluderMipLayoutSizeY = (uint)occluderMipLayoutSize.y;
			for (int i = 0; i < this.occluderMipBounds.Length; i++)
			{
				global::UnityEngine.Rendering.OccluderMipBounds occluderMipBounds = this.occluderMipBounds[i];
				result.cb._OccluderMipBounds[4 * i] = (uint)occluderMipBounds.offset.x;
				result.cb._OccluderMipBounds[4 * i + 1] = (uint)occluderMipBounds.offset.y;
				result.cb._OccluderMipBounds[4 * i + 2] = (uint)occluderMipBounds.size.x;
				result.cb._OccluderMipBounds[4 * i + 3] = (uint)occluderMipBounds.size.y;
			}
			return result;
		}
	}
}
