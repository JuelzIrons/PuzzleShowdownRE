namespace UnityEngine.Rendering.Universal
{
	public static class RenderingUtils
	{
		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> m_LegacyShaderPassNames = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId>
		{
			new global::UnityEngine.Rendering.ShaderTagId("Always"),
			new global::UnityEngine.Rendering.ShaderTagId("ForwardBase"),
			new global::UnityEngine.Rendering.ShaderTagId("PrepassBase"),
			new global::UnityEngine.Rendering.ShaderTagId("Vertex"),
			new global::UnityEngine.Rendering.ShaderTagId("VertexLMRGBM"),
			new global::UnityEngine.Rendering.ShaderTagId("VertexLM")
		};

		private static global::UnityEngine.Rendering.AttachmentDescriptor s_EmptyAttachment = new global::UnityEngine.Rendering.AttachmentDescriptor(global::UnityEngine.Experimental.Rendering.GraphicsFormat.None);

		private static global::UnityEngine.Mesh s_FullscreenMesh = null;

		private static global::UnityEngine.Material s_ErrorMaterial;

		private static global::UnityEngine.Rendering.ShaderTagId[] s_ShaderTagValues = new global::UnityEngine.Rendering.ShaderTagId[1];

		private static global::UnityEngine.Rendering.RenderStateBlock[] s_RenderStateBlocks = new global::UnityEngine.Rendering.RenderStateBlock[1];

		private static global::System.Collections.Generic.Dictionary<global::UnityEngine.RenderTextureFormat, bool> m_RenderTextureFormatSupport = new global::System.Collections.Generic.Dictionary<global::UnityEngine.RenderTextureFormat, bool>();

		internal static global::UnityEngine.Rendering.AttachmentDescriptor emptyAttachment => s_EmptyAttachment;

		[global::System.Obsolete("Use Blitter.BlitCameraTexture instead of CommandBuffer.DrawMesh(fullscreenMesh, ...). #from(2022.2)")]
		public static global::UnityEngine.Mesh fullscreenMesh
		{
			get
			{
				if (s_FullscreenMesh != null)
				{
					return s_FullscreenMesh;
				}
				float y = 1f;
				float y2 = 0f;
				s_FullscreenMesh = new global::UnityEngine.Mesh
				{
					name = "Fullscreen Quad"
				};
				s_FullscreenMesh.SetVertices(new global::System.Collections.Generic.List<global::UnityEngine.Vector3>
				{
					new global::UnityEngine.Vector3(-1f, -1f, 0f),
					new global::UnityEngine.Vector3(-1f, 1f, 0f),
					new global::UnityEngine.Vector3(1f, -1f, 0f),
					new global::UnityEngine.Vector3(1f, 1f, 0f)
				});
				s_FullscreenMesh.SetUVs(0, new global::System.Collections.Generic.List<global::UnityEngine.Vector2>
				{
					new global::UnityEngine.Vector2(0f, y2),
					new global::UnityEngine.Vector2(0f, y),
					new global::UnityEngine.Vector2(1f, y2),
					new global::UnityEngine.Vector2(1f, y)
				});
				s_FullscreenMesh.SetIndices(new int[6] { 0, 1, 2, 2, 1, 3 }, global::UnityEngine.MeshTopology.Triangles, 0, calculateBounds: false);
				s_FullscreenMesh.UploadMeshData(markNoLongerReadable: true);
				return s_FullscreenMesh;
			}
		}

		internal static bool useStructuredBuffer => false;

		private static global::UnityEngine.Material errorMaterial
		{
			get
			{
				if (s_ErrorMaterial == null)
				{
					try
					{
						s_ErrorMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find("Hidden/Universal Render Pipeline/FallbackError"));
					}
					catch
					{
					}
				}
				return s_ErrorMaterial;
			}
		}

		internal static bool SupportsLightLayers(global::UnityEngine.Rendering.GraphicsDeviceType type)
		{
			return true;
		}

		public static void SetViewAndProjectionMatrices(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Matrix4x4 viewMatrix, global::UnityEngine.Matrix4x4 projectionMatrix, bool setInverseMatrices)
		{
			SetViewAndProjectionMatrices(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd), viewMatrix, projectionMatrix, setInverseMatrices);
		}

		public static void SetViewAndProjectionMatrices(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Matrix4x4 viewMatrix, global::UnityEngine.Matrix4x4 projectionMatrix, bool setInverseMatrices)
		{
			global::UnityEngine.Matrix4x4 value = projectionMatrix * viewMatrix;
			cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.viewMatrix, viewMatrix);
			cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.projectionMatrix, projectionMatrix);
			cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.viewAndProjectionMatrix, value);
			if (setInverseMatrices)
			{
				global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.Inverse(viewMatrix);
				global::UnityEngine.Matrix4x4 matrix4x2 = global::UnityEngine.Matrix4x4.Inverse(projectionMatrix);
				global::UnityEngine.Matrix4x4 value2 = matrix4x * matrix4x2;
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.inverseViewMatrix, matrix4x);
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.inverseProjectionMatrix, matrix4x2);
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.inverseViewAndProjectionMatrix, value2);
			}
		}

		internal static void SetScaleBiasRt(global::UnityEngine.Rendering.RasterCommandBuffer cmd, in global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.RTHandle rTHandle)
		{
			float num = ((cameraData.cameraType != global::UnityEngine.CameraType.Game || !(rTHandle.nameID == global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget) || !(cameraData.camera.targetTexture == null)) ? (-1f) : 1f);
			global::UnityEngine.Vector4 value = ((num < 0f) ? new global::UnityEngine.Vector4(num, 1f, -1f, 1f) : new global::UnityEngine.Vector4(num, 0f, 1f, 1f));
			cmd.SetGlobalVector(global::UnityEngine.Shader.PropertyToID("_ScaleBiasRt"), value);
		}

		internal static void SetupOffscreenUIViewportParams(global::UnityEngine.Material material, ref global::UnityEngine.Rect pixelRect, bool isRenderToBackBufferTarget)
		{
			global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(0f, 0f, 1f, 1f);
			if (isRenderToBackBufferTarget)
			{
				global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(1f / (float)global::UnityEngine.Screen.width, 1f / (float)global::UnityEngine.Screen.height);
				value = new global::UnityEngine.Vector4(pixelRect.x * vector.x, pixelRect.y * vector.y, pixelRect.width * vector.x, pixelRect.height * vector.y);
			}
			material.SetVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.offscreenUIViewportParams, value);
		}

		internal static void Blit(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rect viewport, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, global::UnityEngine.Material material, int passIndex = 0)
		{
			global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destination, loadAction, storeAction, global::UnityEngine.Rendering.ClearFlag.None, global::UnityEngine.Color.clear);
			cmd.SetViewport(viewport);
			global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, vector, material, passIndex);
		}

		internal static void Blit(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rect viewport, global::UnityEngine.Rendering.RTHandle destinationColor, global::UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, global::UnityEngine.Rendering.RTHandle destinationDepthStencil, global::UnityEngine.Rendering.RenderBufferLoadAction depthStencilLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction depthStencilStoreAction, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, global::UnityEngine.Material material, int passIndex = 0)
		{
			global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destinationColor, colorLoadAction, colorStoreAction, destinationDepthStencil, depthStencilLoadAction, depthStencilStoreAction, clearFlag, clearColor);
			cmd.SetViewport(viewport);
			global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, vector, material, passIndex);
		}

		internal static void FinalBlit(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, global::UnityEngine.Material material, int passIndex)
		{
			bool flag = !cameraData.isSceneViewCamera;
			if (cameraData.xr.enabled)
			{
				flag = new global::UnityEngine.Rendering.RenderTargetIdentifier(destination.nameID, 0, global::UnityEngine.CubemapFace.Unknown, -1) == new global::UnityEngine.Rendering.RenderTargetIdentifier(cameraData.xr.renderTarget, 0, global::UnityEngine.CubemapFace.Unknown, -1);
			}
			global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			global::UnityEngine.Vector4 scaleBias = ((flag && cameraData.targetTexture == null && global::UnityEngine.SystemInfo.graphicsUVStartsAtTop) ? new global::UnityEngine.Vector4(vector.x, 0f - vector.y, 0f, vector.y) : new global::UnityEngine.Vector4(vector.x, vector.y, 0f, 0f));
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destination, loadAction, storeAction, global::UnityEngine.Rendering.ClearFlag.None, global::UnityEngine.Color.clear);
			if (flag)
			{
				cmd.SetViewport(cameraData.pixelRect);
			}
			if (global::UnityEngine.GL.wireframe && cameraData.isSceneViewCamera)
			{
				cmd.SetRenderTarget(global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget, loadAction, storeAction, global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare, global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare);
				if (global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.Vulkan)
				{
					cmd.SetWireframe(enable: false);
					cmd.Blit(source, destination);
					cmd.SetWireframe(enable: true);
				}
				else
				{
					cmd.Blit(source, destination);
				}
			}
			else if (source.rt == null)
			{
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source.nameID, scaleBias, material, passIndex);
			}
			else
			{
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, source, scaleBias, material, passIndex);
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal static void CreateRendererParamsObjectsWithError(ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Camera camera, global::UnityEngine.Rendering.FilteringSettings filterSettings, global::UnityEngine.Rendering.SortingCriteria sortFlags, ref global::UnityEngine.Rendering.RendererListParams param)
		{
			global::UnityEngine.Rendering.SortingSettings sortingSettings = new global::UnityEngine.Rendering.SortingSettings(camera);
			sortingSettings.criteria = sortFlags;
			global::UnityEngine.Rendering.SortingSettings sortingSettings2 = sortingSettings;
			global::UnityEngine.Rendering.DrawingSettings drawingSettings = new global::UnityEngine.Rendering.DrawingSettings(m_LegacyShaderPassNames[0], sortingSettings2);
			drawingSettings.perObjectData = global::UnityEngine.Rendering.PerObjectData.None;
			drawingSettings.overrideMaterial = errorMaterial;
			drawingSettings.overrideMaterialPassIndex = 0;
			global::UnityEngine.Rendering.DrawingSettings drawSettings = drawingSettings;
			for (int i = 1; i < m_LegacyShaderPassNames.Count; i++)
			{
				drawSettings.SetShaderPassName(i, m_LegacyShaderPassNames[i]);
			}
			param = new global::UnityEngine.Rendering.RendererListParams(cullResults, drawSettings, filterSettings);
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal static void CreateRendererListObjectsWithError(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Camera camera, global::UnityEngine.Rendering.FilteringSettings filterSettings, global::UnityEngine.Rendering.SortingCriteria sortFlags, ref global::UnityEngine.Rendering.RendererList rl)
		{
			if (errorMaterial == null)
			{
				rl = global::UnityEngine.Rendering.RendererList.nullRendererList;
				return;
			}
			global::UnityEngine.Rendering.RendererListParams param = default(global::UnityEngine.Rendering.RendererListParams);
			rl = context.CreateRendererList(ref param);
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal static void CreateRendererListObjectsWithError(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Camera camera, global::UnityEngine.Rendering.FilteringSettings filterSettings, global::UnityEngine.Rendering.SortingCriteria sortFlags, ref global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rl)
		{
			if (errorMaterial == null)
			{
				rl = default(global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle);
			}
			else
			{
				rl = renderGraph.CreateRendererList(default(global::UnityEngine.Rendering.RendererListParams));
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal static void DrawRendererListObjectsWithError(global::UnityEngine.Rendering.RasterCommandBuffer cmd, ref global::UnityEngine.Rendering.RendererList rl)
		{
			cmd.DrawRendererList(rl);
		}

		internal unsafe static void CreateRendererListWithRenderStateBlock(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Rendering.DrawingSettings ds, global::UnityEngine.Rendering.FilteringSettings fs, global::UnityEngine.Rendering.RenderStateBlock rsb, ref global::UnityEngine.Rendering.RendererList rl)
		{
			global::UnityEngine.Rendering.RendererListParams rendererListParams = default(global::UnityEngine.Rendering.RendererListParams);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderStateBlock> value = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::UnityEngine.Rendering.RenderStateBlock>(&rsb, 1, global::Unity.Collections.Allocator.None);
			global::UnityEngine.Rendering.ShaderTagId none = global::UnityEngine.Rendering.ShaderTagId.none;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ShaderTagId> value2 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::UnityEngine.Rendering.ShaderTagId>(&none, 1, global::Unity.Collections.Allocator.None);
			global::UnityEngine.Rendering.RendererListParams rendererListParams2 = new global::UnityEngine.Rendering.RendererListParams(cullResults, ds, fs);
			rendererListParams2.tagValues = value2;
			rendererListParams2.stateBlocks = value;
			rendererListParams = rendererListParams2;
			rl = context.CreateRendererList(ref rendererListParams);
		}

		internal static void CreateRendererListWithRenderStateBlock(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ref global::UnityEngine.Rendering.CullingResults cullResults, global::UnityEngine.Rendering.DrawingSettings ds, global::UnityEngine.Rendering.FilteringSettings fs, global::UnityEngine.Rendering.RenderStateBlock rsb, ref global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rl)
		{
			s_ShaderTagValues[0] = global::UnityEngine.Rendering.ShaderTagId.none;
			s_RenderStateBlocks[0] = rsb;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ShaderTagId> value = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ShaderTagId>(s_ShaderTagValues, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderStateBlock> value2 = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderStateBlock>(s_RenderStateBlocks, global::Unity.Collections.Allocator.Temp);
			global::UnityEngine.Rendering.RendererListParams rendererListParams = new global::UnityEngine.Rendering.RendererListParams(cullResults, ds, fs);
			rendererListParams.tagValues = value;
			rendererListParams.stateBlocks = value2;
			rendererListParams.isPassTagName = false;
			global::UnityEngine.Rendering.RendererListParams desc = rendererListParams;
			rl = renderGraph.CreateRendererList(in desc);
		}

		internal static void ClearSystemInfoCache()
		{
			m_RenderTextureFormatSupport.Clear();
		}

		public static bool SupportsRenderTextureFormat(global::UnityEngine.RenderTextureFormat format)
		{
			if (!m_RenderTextureFormatSupport.TryGetValue(format, out var value))
			{
				value = global::UnityEngine.SystemInfo.SupportsRenderTextureFormat(format);
				m_RenderTextureFormatSupport.Add(format, value);
			}
			return value;
		}

		[global::System.Obsolete("Use SystemInfo.IsFormatSupported instead. #from(2023.2)")]
		public static bool SupportsGraphicsFormat(global::UnityEngine.Experimental.Rendering.GraphicsFormat format, global::UnityEngine.Experimental.Rendering.FormatUsage usage)
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage usage2 = (global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage)(1 << (int)usage);
			return global::UnityEngine.SystemInfo.IsFormatSupported(format, usage2);
		}

		internal static int GetLastValidColorBufferIndex(global::UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers)
		{
			int num = colorBuffers.Length - 1;
			while (num >= 0 && !(colorBuffers[num] != 0))
			{
				num--;
			}
			return num;
		}

		internal static uint GetValidColorBufferCount(global::UnityEngine.Rendering.RTHandle[] colorBuffers)
		{
			uint num = 0u;
			if (colorBuffers != null)
			{
				foreach (global::UnityEngine.Rendering.RTHandle rTHandle in colorBuffers)
				{
					if (rTHandle != null && rTHandle.nameID != 0)
					{
						num++;
					}
				}
			}
			return num;
		}

		internal static bool IsMRT(global::UnityEngine.Rendering.RTHandle[] colorBuffers)
		{
			return GetValidColorBufferCount(colorBuffers) > 1;
		}

		internal static bool Contains(global::UnityEngine.Rendering.RenderTargetIdentifier[] source, global::UnityEngine.Rendering.RenderTargetIdentifier value)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] == value)
				{
					return true;
				}
			}
			return false;
		}

		internal static int IndexOf(global::UnityEngine.Rendering.RTHandle[] source, global::UnityEngine.Rendering.RenderTargetIdentifier value)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		internal static int IndexOf(global::UnityEngine.Rendering.RTHandle[] source, global::UnityEngine.Rendering.RTHandle value)
		{
			return IndexOf(source, value.nameID);
		}

		internal static uint CountDistinct(global::UnityEngine.Rendering.RTHandle[] source, global::UnityEngine.Rendering.RTHandle value)
		{
			uint num = 0u;
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] != null && source[i].nameID != 0 && source[i].nameID != value.nameID)
				{
					num++;
				}
			}
			return num;
		}

		internal static int LastValid(global::UnityEngine.Rendering.RTHandle[] source)
		{
			for (int num = source.Length - 1; num >= 0; num--)
			{
				if (source[num] != null && source[num].nameID != 0)
				{
					return num;
				}
			}
			return -1;
		}

		internal static bool Contains(global::UnityEngine.Rendering.ClearFlag a, global::UnityEngine.Rendering.ClearFlag b)
		{
			return (a & b) == b;
		}

		internal static bool SequenceEqual(global::UnityEngine.Rendering.RTHandle[] left, global::UnityEngine.Rendering.RTHandle[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i]?.nameID != right[i]?.nameID)
				{
					return false;
				}
			}
			return true;
		}

		internal static bool MultisampleDepthResolveSupported()
		{
			if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXEditor || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXPlayer)
			{
				return false;
			}
			if (global::UnityEngine.SystemInfo.supportsMultisampleResolveDepth)
			{
				return global::UnityEngine.SystemInfo.supportsMultisampleResolveStencil;
			}
			return false;
		}

		internal static bool RTHandleNeedsReAlloc(global::UnityEngine.Rendering.RTHandle handle, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor, bool scaled)
		{
			if (handle == null || handle.rt == null)
			{
				return true;
			}
			if (handle.useScaling != scaled)
			{
				return true;
			}
			if (!scaled && (handle.rt.width != descriptor.width || handle.rt.height != descriptor.height))
			{
				return true;
			}
			if (handle.rt.enableShadingRate && handle.rt.graphicsFormat != descriptor.colorFormat)
			{
				return true;
			}
			global::UnityEngine.RenderTextureDescriptor descriptor2 = handle.rt.descriptor;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat num = ((descriptor2.depthStencilFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None) ? descriptor2.depthStencilFormat : descriptor2.graphicsFormat);
			bool flag = descriptor2.shadowSamplingMode != global::UnityEngine.Rendering.ShadowSamplingMode.None;
			if (num == descriptor.format && descriptor2.dimension == descriptor.dimension && descriptor2.volumeDepth == descriptor.slices && descriptor2.enableRandomWrite == descriptor.enableRandomWrite && descriptor2.enableShadingRate == descriptor.enableShadingRate && descriptor2.useMipMap == descriptor.useMipMap && descriptor2.autoGenerateMips == descriptor.autoGenerateMips && flag == descriptor.isShadowMap && descriptor2.msaaSamples == (int)descriptor.msaaSamples && descriptor2.bindMS == descriptor.bindTextureMS && descriptor2.useDynamicScale == descriptor.useDynamicScale && descriptor2.useDynamicScaleExplicit == descriptor.useDynamicScaleExplicit && descriptor2.memoryless == descriptor.memoryless && handle.rt.filterMode == descriptor.filterMode && handle.rt.wrapMode == descriptor.wrapMode && handle.rt.anisoLevel == descriptor.anisoLevel && !(global::UnityEngine.Mathf.Abs(handle.rt.mipMapBias - descriptor.mipMapBias) > global::UnityEngine.Mathf.Epsilon))
			{
				return handle.name != descriptor.name;
			}
			return true;
		}

		internal static global::UnityEngine.Rendering.RenderTargetIdentifier GetCameraTargetIdentifier(ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
			ref global::UnityEngine.Rendering.Universal.CameraData cameraData = ref renderingData.cameraData;
			global::UnityEngine.Rendering.RenderTargetIdentifier result = ((cameraData.targetTexture != null) ? new global::UnityEngine.Rendering.RenderTargetIdentifier(cameraData.targetTexture) : ((global::UnityEngine.Rendering.RenderTargetIdentifier)global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget));
			if (cameraData.xr.enabled)
			{
				if (cameraData.xr.singlePassEnabled)
				{
					result = cameraData.xr.renderTarget;
				}
				else
				{
					int textureArraySlice = cameraData.xr.GetTextureArraySlice();
					result = new global::UnityEngine.Rendering.RenderTargetIdentifier(cameraData.xr.renderTarget, 0, global::UnityEngine.CubemapFace.Unknown, textureArraySlice);
				}
			}
			return result;
		}

		[global::System.Obsolete("This method will be removed in a future release. Please use ReAllocateHandleIfNeeded instead. #from(2023.3)")]
		public static bool ReAllocateIfNeeded(ref global::UnityEngine.Rendering.RTHandle handle, in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor2 = global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit, anisoLevel, 0f, filterMode, wrapMode, name);
			if (RTHandleNeedsReAlloc(handle, in descriptor2, scaled: false))
			{
				if (handle != null && handle.rt != null)
				{
					AddStaleResourceToPoolOrRelease(global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode, handle.name), handle);
				}
				if (global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in descriptor2, out handle))
				{
					return true;
				}
				handle = global::UnityEngine.Rendering.RTHandles.Alloc(in descriptor, filterMode, wrapMode, isShadowMap, anisoLevel, mipMapBias, name);
				return true;
			}
			return false;
		}

		[global::System.Obsolete("This method will be removed in a future release. Please use ReAllocateHandleIfNeeded instead. #from(2023.3)")]
		public static bool ReAllocateIfNeeded(ref global::UnityEngine.Rendering.RTHandle handle, global::UnityEngine.Vector2 scaleFactor, in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			bool num = handle != null && handle.useScaling && handle.scaleFactor == scaleFactor;
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc texDesc = global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Scale, anisoLevel, 0f, filterMode, wrapMode);
			if (!num || RTHandleNeedsReAlloc(handle, in texDesc, scaled: true))
			{
				if (handle != null && handle.rt != null)
				{
					AddStaleResourceToPoolOrRelease(global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Scale, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode), handle);
				}
				if (global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in texDesc, out handle))
				{
					return true;
				}
				handle = global::UnityEngine.Rendering.RTHandles.Alloc(scaleFactor, in descriptor, filterMode, wrapMode, isShadowMap, anisoLevel, mipMapBias, name);
				return true;
			}
			return false;
		}

		[global::System.Obsolete("This method will be removed in a future release. Please use ReAllocateHandleIfNeeded instead. #from(2023.3)")]
		public static bool ReAllocateIfNeeded(ref global::UnityEngine.Rendering.RTHandle handle, global::UnityEngine.Rendering.ScaleFunc scaleFunc, in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			bool num = handle != null && handle.useScaling && handle.scaleFactor == global::UnityEngine.Vector2.zero;
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc texDesc = global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Functor, anisoLevel, 0f, filterMode, wrapMode);
			if (!num || RTHandleNeedsReAlloc(handle, in texDesc, scaled: true))
			{
				if (handle != null && handle.rt != null)
				{
					AddStaleResourceToPoolOrRelease(global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Functor, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode), handle);
				}
				if (global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in texDesc, out handle))
				{
					return true;
				}
				handle = global::UnityEngine.Rendering.RTHandles.Alloc(scaleFunc, in descriptor, filterMode, wrapMode, isShadowMap, anisoLevel, mipMapBias, name);
				return true;
			}
			return false;
		}

		public static bool ReAllocateHandleIfNeeded(ref global::UnityEngine.Rendering.RTHandle handle, in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor2 = global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit, anisoLevel, 0f, filterMode, wrapMode, name);
			if (RTHandleNeedsReAlloc(handle, in descriptor2, scaled: false))
			{
				if (handle != null && handle.rt != null)
				{
					AddStaleResourceToPoolOrRelease(global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode, handle.name), handle);
				}
				if (global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in descriptor2, out handle))
				{
					return true;
				}
				global::UnityEngine.Rendering.RTHandleAllocInfo info = CreateRTHandleAllocInfo(in descriptor, filterMode, wrapMode, anisoLevel, mipMapBias, name);
				handle = global::UnityEngine.Rendering.RTHandles.Alloc(descriptor.width, descriptor.height, info);
				return true;
			}
			return false;
		}

		public static bool ReAllocateHandleIfNeeded(ref global::UnityEngine.Rendering.RTHandle handle, global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor, string name)
		{
			descriptor.name = name;
			descriptor.sizeMode = global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit;
			if (RTHandleNeedsReAlloc(handle, in descriptor, scaled: false))
			{
				if (handle != null && handle.rt != null)
				{
					AddStaleResourceToPoolOrRelease(global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode, handle.name), handle);
				}
				if (global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in descriptor, out handle))
				{
					return true;
				}
				global::UnityEngine.Rendering.RTHandleAllocInfo info = CreateRTHandleAllocInfo(in descriptor, name);
				handle = global::UnityEngine.Rendering.RTHandles.Alloc(descriptor.width, descriptor.height, info);
				return true;
			}
			return false;
		}

		public static bool ReAllocateHandleIfNeeded(ref global::UnityEngine.Rendering.RTHandle handle, global::UnityEngine.Vector2 scaleFactor, in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			bool num = handle != null && handle.useScaling && handle.scaleFactor == scaleFactor;
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc texDesc = global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Scale, anisoLevel, 0f, filterMode, wrapMode);
			if (!num || RTHandleNeedsReAlloc(handle, in texDesc, scaled: true))
			{
				if (handle != null && handle.rt != null)
				{
					AddStaleResourceToPoolOrRelease(global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Scale, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode), handle);
				}
				if (global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in texDesc, out handle))
				{
					return true;
				}
				global::UnityEngine.Rendering.RTHandleAllocInfo info = CreateRTHandleAllocInfo(in descriptor, filterMode, wrapMode, anisoLevel, mipMapBias, name);
				handle = global::UnityEngine.Rendering.RTHandles.Alloc(scaleFactor, info);
				return true;
			}
			return false;
		}

		public static bool ReAllocateHandleIfNeeded(ref global::UnityEngine.Rendering.RTHandle handle, global::UnityEngine.Rendering.ScaleFunc scaleFunc, in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			bool num = handle != null && handle.useScaling && handle.scaleFactor == global::UnityEngine.Vector2.zero;
			global::UnityEngine.Rendering.RenderGraphModule.TextureDesc texDesc = global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Functor, anisoLevel, 0f, filterMode, wrapMode);
			if (!num || RTHandleNeedsReAlloc(handle, in texDesc, scaled: true))
			{
				if (handle != null && handle.rt != null)
				{
					AddStaleResourceToPoolOrRelease(global::UnityEngine.Rendering.Universal.RTHandleResourcePool.CreateTextureDesc(handle.rt.descriptor, global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Functor, handle.rt.anisoLevel, handle.rt.mipMapBias, handle.rt.filterMode, handle.rt.wrapMode), handle);
				}
				if (global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool.TryGetResource(in texDesc, out handle))
				{
					return true;
				}
				global::UnityEngine.Rendering.RTHandleAllocInfo info = CreateRTHandleAllocInfo(in descriptor, filterMode, wrapMode, anisoLevel, mipMapBias, name);
				handle = global::UnityEngine.Rendering.RTHandles.Alloc(scaleFunc, info);
				return true;
			}
			return false;
		}

		public static bool SetMaxRTHandlePoolCapacity(int capacity)
		{
			if (global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool == null)
			{
				return false;
			}
			global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool.staleResourceCapacity = capacity;
			return true;
		}

		internal static void AddStaleResourceToPoolOrRelease(global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc, global::UnityEngine.Rendering.RTHandle handle)
		{
			if (!global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.s_RTHandlePool.AddResourceToPool(in desc, handle, global::UnityEngine.Time.frameCount))
			{
				global::UnityEngine.Rendering.RTHandles.Release(handle);
			}
		}

		public static global::UnityEngine.Rendering.DrawingSettings CreateDrawingSettings(global::UnityEngine.Rendering.ShaderTagId shaderTagId, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData, global::UnityEngine.Rendering.SortingCriteria sortingCriteria)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData2 = renderingData.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = renderingData.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = renderingData.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			return CreateDrawingSettings(shaderTagId, renderingData2, cameraData, lightData, sortingCriteria);
		}

		public static global::UnityEngine.Rendering.DrawingSettings CreateDrawingSettings(global::UnityEngine.Rendering.ShaderTagId shaderTagId, global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.SortingCriteria sortingCriteria)
		{
			global::UnityEngine.Camera camera = cameraData.camera;
			global::UnityEngine.Rendering.SortingSettings sortingSettings = new global::UnityEngine.Rendering.SortingSettings(camera);
			sortingSettings.criteria = sortingCriteria;
			global::UnityEngine.Rendering.SortingSettings sortingSettings2 = sortingSettings;
			global::UnityEngine.Rendering.DrawingSettings result = new global::UnityEngine.Rendering.DrawingSettings(shaderTagId, sortingSettings2);
			result.perObjectData = renderingData.perObjectData;
			result.mainLightIndex = lightData.mainLightIndex;
			result.enableDynamicBatching = renderingData.supportsDynamicBatching;
			result.enableInstancing = camera.cameraType != global::UnityEngine.CameraType.Preview;
			result.lodCrossFadeStencilMask = (renderingData.stencilLodCrossFadeEnabled ? 12 : 0);
			return result;
		}

		public static global::UnityEngine.Rendering.DrawingSettings CreateDrawingSettings(global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> shaderTagIdList, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData, global::UnityEngine.Rendering.SortingCriteria sortingCriteria)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData2 = renderingData.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = renderingData.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = renderingData.frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			return CreateDrawingSettings(shaderTagIdList, renderingData2, cameraData, lightData, sortingCriteria);
		}

		public static global::UnityEngine.Rendering.DrawingSettings CreateDrawingSettings(global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> shaderTagIdList, global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.SortingCriteria sortingCriteria)
		{
			if (shaderTagIdList == null || shaderTagIdList.Count == 0)
			{
				global::UnityEngine.Debug.LogWarning("ShaderTagId list is invalid. DrawingSettings is created with default pipeline ShaderTagId");
				return CreateDrawingSettings(new global::UnityEngine.Rendering.ShaderTagId("UniversalPipeline"), renderingData, cameraData, lightData, sortingCriteria);
			}
			global::UnityEngine.Rendering.DrawingSettings result = CreateDrawingSettings(shaderTagIdList[0], renderingData, cameraData, lightData, sortingCriteria);
			for (int i = 1; i < shaderTagIdList.Count; i++)
			{
				result.SetShaderPassName(i, shaderTagIdList[i]);
			}
			return result;
		}

		internal static bool IsHandleYFlipped(in global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext renderGraphContext, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle)
		{
			return renderGraphContext.GetTextureUVOrigin(in textureHandle) == global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft;
		}

		internal static global::UnityEngine.Vector4 GetFinalBlitScaleBias(in global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext renderGraphContext, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle source, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle destination)
		{
			global::UnityEngine.Rendering.RTHandle rTHandle = source;
			global::UnityEngine.Vector2 vector = ((rTHandle != null && rTHandle.useScaling) ? new global::UnityEngine.Vector2(rTHandle.rtHandleProperties.rtHandleScale.x, rTHandle.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			if (renderGraphContext.GetTextureUVOrigin(in source) == renderGraphContext.GetTextureUVOrigin(in destination))
			{
				return new global::UnityEngine.Vector4(vector.x, vector.y, 0f, 0f);
			}
			return new global::UnityEngine.Vector4(vector.x, 0f - vector.y, 0f, vector.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::UnityEngine.Rendering.RTHandleAllocInfo CreateRTHandleAllocInfo(in global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode, global::UnityEngine.TextureWrapMode wrapMode, int anisoLevel, float mipMapBias, string name)
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format = ((descriptor.graphicsFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None) ? descriptor.graphicsFormat : descriptor.depthStencilFormat);
			global::UnityEngine.Rendering.RTHandleAllocInfo result = default(global::UnityEngine.Rendering.RTHandleAllocInfo);
			result.slices = descriptor.volumeDepth;
			result.format = format;
			result.filterMode = filterMode;
			result.wrapModeU = wrapMode;
			result.wrapModeV = wrapMode;
			result.wrapModeW = wrapMode;
			result.dimension = descriptor.dimension;
			result.enableRandomWrite = descriptor.enableRandomWrite;
			result.enableShadingRate = descriptor.enableShadingRate;
			result.useMipMap = descriptor.useMipMap;
			result.autoGenerateMips = descriptor.autoGenerateMips;
			result.anisoLevel = anisoLevel;
			result.mipMapBias = mipMapBias;
			result.isShadowMap = descriptor.shadowSamplingMode != global::UnityEngine.Rendering.ShadowSamplingMode.None;
			result.msaaSamples = (global::UnityEngine.Rendering.MSAASamples)descriptor.msaaSamples;
			result.bindTextureMS = descriptor.bindMS;
			result.useDynamicScale = descriptor.useDynamicScale;
			result.useDynamicScaleExplicit = descriptor.useDynamicScaleExplicit;
			result.memoryless = descriptor.memoryless;
			result.vrUsage = descriptor.vrUsage;
			result.enableShadingRate = descriptor.enableShadingRate;
			result.name = name;
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::UnityEngine.Rendering.RTHandleAllocInfo CreateRTHandleAllocInfo(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc descriptor, string name)
		{
			global::UnityEngine.Rendering.RTHandleAllocInfo result = default(global::UnityEngine.Rendering.RTHandleAllocInfo);
			result.slices = descriptor.slices;
			result.format = descriptor.format;
			result.filterMode = descriptor.filterMode;
			result.wrapModeU = descriptor.wrapMode;
			result.wrapModeV = descriptor.wrapMode;
			result.wrapModeW = descriptor.wrapMode;
			result.dimension = descriptor.dimension;
			result.enableRandomWrite = descriptor.enableRandomWrite;
			result.enableShadingRate = descriptor.enableShadingRate;
			result.useMipMap = descriptor.useMipMap;
			result.autoGenerateMips = descriptor.autoGenerateMips;
			result.anisoLevel = descriptor.anisoLevel;
			result.mipMapBias = descriptor.mipMapBias;
			result.isShadowMap = descriptor.isShadowMap;
			result.msaaSamples = descriptor.msaaSamples;
			result.bindTextureMS = descriptor.bindTextureMS;
			result.useDynamicScale = descriptor.useDynamicScale;
			result.useDynamicScaleExplicit = descriptor.useDynamicScaleExplicit;
			result.memoryless = descriptor.memoryless;
			result.vrUsage = descriptor.vrUsage;
			result.enableShadingRate = descriptor.enableShadingRate;
			result.name = name;
			return result;
		}
	}
}
