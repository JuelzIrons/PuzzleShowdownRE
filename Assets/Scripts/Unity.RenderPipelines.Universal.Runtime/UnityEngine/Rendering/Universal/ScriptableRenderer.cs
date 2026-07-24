namespace UnityEngine.Rendering.Universal
{
	public abstract class ScriptableRenderer : global::System.IDisposable
	{
		private static class Profiling
		{
			private const string k_Name = "ScriptableRenderer";

			public static readonly global::UnityEngine.Rendering.ProfilingSampler setPerCameraShaderVariables = new global::UnityEngine.Rendering.ProfilingSampler("ScriptableRenderer.SetPerCameraShaderVariables");

			public static readonly global::UnityEngine.Rendering.ProfilingSampler sortRenderPasses = new global::UnityEngine.Rendering.ProfilingSampler("Sort Render Passes");

			public static readonly global::UnityEngine.Rendering.ProfilingSampler recordRenderGraph = new global::UnityEngine.Rendering.ProfilingSampler("On Record Render Graph");

			public static readonly global::UnityEngine.Rendering.ProfilingSampler setupCamera = new global::UnityEngine.Rendering.ProfilingSampler("Setup Camera Properties");

			public static readonly global::UnityEngine.Rendering.ProfilingSampler vfxProcessCamera = new global::UnityEngine.Rendering.ProfilingSampler("VFX Process Camera");

			public static readonly global::UnityEngine.Rendering.ProfilingSampler addRenderPasses = new global::UnityEngine.Rendering.ProfilingSampler("ScriptableRenderer.AddRenderPasses");

			public static readonly global::UnityEngine.Rendering.ProfilingSampler clearRenderingState = new global::UnityEngine.Rendering.ProfilingSampler("ScriptableRenderer.ClearRenderingState");

			public static readonly global::UnityEngine.Rendering.ProfilingSampler internalFinishRenderingCommon = new global::UnityEngine.Rendering.ProfilingSampler("ScriptableRenderer.InternalFinishRenderingCommon");

			public static readonly global::UnityEngine.Rendering.ProfilingSampler drawGizmos = new global::UnityEngine.Rendering.ProfilingSampler("DrawGizmos");

			public static readonly global::UnityEngine.Rendering.ProfilingSampler drawWireOverlay = new global::UnityEngine.Rendering.ProfilingSampler("DrawWireOverlay");

			internal static readonly global::UnityEngine.Rendering.ProfilingSampler beginXRRendering = new global::UnityEngine.Rendering.ProfilingSampler("Begin XR Rendering");

			internal static readonly global::UnityEngine.Rendering.ProfilingSampler endXRRendering = new global::UnityEngine.Rendering.ProfilingSampler("End XR Rendering");

			internal static readonly global::UnityEngine.Rendering.ProfilingSampler initRenderGraphFrame = new global::UnityEngine.Rendering.ProfilingSampler("Initialize Frame");

			internal static readonly global::UnityEngine.Rendering.ProfilingSampler setEditorTarget = new global::UnityEngine.Rendering.ProfilingSampler("Set Editor Target");
		}

		public class RenderingFeatures
		{
			[global::System.Obsolete("cameraStacking has been deprecated use SupportedCameraRenderTypes() in ScriptableRenderer instead. #from(2022.2) #breakingFrom(2023.1)", true)]
			public bool cameraStacking { get; set; }

			public bool msaa { get; set; } = true;
		}

		private static class RenderPassBlock
		{
			public static readonly int BeforeRendering = 0;

			public static readonly int MainRenderingOpaque = 1;

			public static readonly int MainRenderingTransparent = 2;

			public static readonly int AfterRendering = 3;
		}

		private class VFXProcessCameraPassData
		{
			internal global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData;

			internal global::UnityEngine.Camera camera;

			internal global::UnityEngine.VFX.VFXCameraXRSettings cameraXRSettings;

			internal global::UnityEngine.Experimental.Rendering.XRPass xrPass;
		}

		private class DrawGizmosPassData
		{
			public global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle gizmoRenderList;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle color;

			public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depth;
		}

		private class DrawWireOverlayPassData
		{
			public global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle wireOverlayList;
		}

		private class BeginXRPassData
		{
			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;
		}

		private class EndXRPassData
		{
			public global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;
		}

		private class DummyData
		{
		}

		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer;

			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle target;

			internal global::UnityEngine.Vector2Int cameraTargetSizeCopy;
		}

		internal struct RenderBlocks : global::System.IDisposable
		{
			public struct BlockRange : global::System.IDisposable
			{
				private int m_Current;

				private int m_End;

				public int Current => m_Current;

				public BlockRange(int begin, int end)
				{
					m_Current = ((begin < end) ? begin : end);
					m_End = ((end >= begin) ? end : begin);
					m_Current--;
				}

				public global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderBlocks.BlockRange GetEnumerator()
				{
					return this;
				}

				public bool MoveNext()
				{
					return ++m_Current < m_End;
				}

				public void Dispose()
				{
				}
			}

			private global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.RenderPassEvent> m_BlockEventLimits;

			private global::Unity.Collections.NativeArray<int> m_BlockRanges;

			private global::Unity.Collections.NativeArray<int> m_BlockRangeLengths;

			public RenderBlocks(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRenderPass> activeRenderPassQueue)
			{
				m_BlockEventLimits = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.RenderPassEvent>(4, global::Unity.Collections.Allocator.Temp);
				m_BlockRanges = new global::Unity.Collections.NativeArray<int>(m_BlockEventLimits.Length + 1, global::Unity.Collections.Allocator.Temp);
				m_BlockRangeLengths = new global::Unity.Collections.NativeArray<int>(m_BlockRanges.Length, global::Unity.Collections.Allocator.Temp);
				m_BlockEventLimits[global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderPassBlock.BeforeRendering] = global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses;
				m_BlockEventLimits[global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderPassBlock.MainRenderingOpaque] = global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques;
				m_BlockEventLimits[global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderPassBlock.MainRenderingTransparent] = global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingPostProcessing;
				m_BlockEventLimits[global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderPassBlock.AfterRendering] = (global::UnityEngine.Rendering.Universal.RenderPassEvent)2147483647;
				FillBlockRanges(activeRenderPassQueue);
				m_BlockEventLimits.Dispose();
				for (int i = 0; i < m_BlockRanges.Length - 1; i++)
				{
					m_BlockRangeLengths[i] = m_BlockRanges[i + 1] - m_BlockRanges[i];
				}
			}

			public void Dispose()
			{
				m_BlockRangeLengths.Dispose();
				m_BlockRanges.Dispose();
			}

			private void FillBlockRanges(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRenderPass> activeRenderPassQueue)
			{
				int index = 0;
				int i = 0;
				m_BlockRanges[index++] = 0;
				for (int j = 0; j < m_BlockEventLimits.Length - 1; j++)
				{
					for (; i < activeRenderPassQueue.Count && activeRenderPassQueue[i].renderPassEvent < m_BlockEventLimits[j]; i++)
					{
					}
					m_BlockRanges[index++] = i;
				}
				m_BlockRanges[index] = activeRenderPassQueue.Count;
			}

			public int GetLength(int index)
			{
				return m_BlockRangeLengths[index];
			}

			public global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderBlocks.BlockRange GetRange(int index)
			{
				return new global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderBlocks.BlockRange(m_BlockRanges[index], m_BlockRanges[index + 1]);
			}
		}

		internal bool hasReleasedRTs = true;

		internal static global::UnityEngine.Rendering.Universal.ScriptableRenderer current = null;

		private global::UnityEngine.Rendering.Universal.StoreActionsOptimization m_StoreActionsOptimizationSetting;

		private static bool m_UseOptimizedStoreActions = false;

		private const int k_RenderPassBlockCount = 4;

		protected static readonly global::UnityEngine.Rendering.RTHandle k_CameraTarget = global::UnityEngine.Rendering.RTHandles.Alloc(global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget);

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRenderPass> m_ActiveRenderPassQueue = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRenderPass>(32);

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRendererFeature> m_RendererFeatures = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRendererFeature>(10);

		private global::UnityEngine.Rendering.RTHandle m_CameraColorTarget;

		private global::UnityEngine.Rendering.RTHandle m_CameraDepthTarget;

		private global::UnityEngine.Rendering.RTHandle m_CameraResolveTarget;

		private bool m_FirstTimeCameraColorTargetIsBound = true;

		private bool m_FirstTimeCameraDepthTargetIsBound = true;

		private bool m_IsPipelineExecuting;

		internal bool useRenderPassEnabled;

		private static global::UnityEngine.Rendering.RenderTargetIdentifier[] m_ActiveColorAttachmentIDs = new global::UnityEngine.Rendering.RenderTargetIdentifier[8];

		private static global::UnityEngine.Rendering.RTHandle[] m_ActiveColorAttachments = new global::UnityEngine.Rendering.RTHandle[8];

		private static global::UnityEngine.Rendering.RTHandle m_ActiveDepthAttachment;

		private global::UnityEngine.Rendering.ContextContainer m_frameData = new global::UnityEngine.Rendering.ContextContainer();

		private static global::UnityEngine.Rendering.RenderBufferStoreAction[] m_ActiveColorStoreActions = new global::UnityEngine.Rendering.RenderBufferStoreAction[8];

		private static global::UnityEngine.Rendering.RenderBufferStoreAction m_ActiveDepthStoreAction = global::UnityEngine.Rendering.RenderBufferStoreAction.Store;

		private static global::UnityEngine.Rendering.RenderTargetIdentifier[][] m_TrimmedColorAttachmentCopyIDs = new global::UnityEngine.Rendering.RenderTargetIdentifier[9][]
		{
			global::System.Array.Empty<global::UnityEngine.Rendering.RenderTargetIdentifier>(),
			new global::UnityEngine.Rendering.RenderTargetIdentifier[1],
			new global::UnityEngine.Rendering.RenderTargetIdentifier[2],
			new global::UnityEngine.Rendering.RenderTargetIdentifier[3],
			new global::UnityEngine.Rendering.RenderTargetIdentifier[4],
			new global::UnityEngine.Rendering.RenderTargetIdentifier[5],
			new global::UnityEngine.Rendering.RenderTargetIdentifier[6],
			new global::UnityEngine.Rendering.RenderTargetIdentifier[7],
			new global::UnityEngine.Rendering.RenderTargetIdentifier[8]
		};

		private static global::UnityEngine.Rendering.RTHandle[][] m_TrimmedColorAttachmentCopies = new global::UnityEngine.Rendering.RTHandle[9][]
		{
			global::System.Array.Empty<global::UnityEngine.Rendering.RTHandle>(),
			new global::UnityEngine.Rendering.RTHandle[1],
			new global::UnityEngine.Rendering.RTHandle[2],
			new global::UnityEngine.Rendering.RTHandle[3],
			new global::UnityEngine.Rendering.RTHandle[4],
			new global::UnityEngine.Rendering.RTHandle[5],
			new global::UnityEngine.Rendering.RTHandle[6],
			new global::UnityEngine.Rendering.RTHandle[7],
			new global::UnityEngine.Rendering.RTHandle[8]
		};

		private static global::UnityEngine.Plane[] s_Planes = new global::UnityEngine.Plane[6];

		private static global::UnityEngine.Vector4[] s_VectorPlanes = new global::UnityEngine.Vector4[6];

		[global::System.Obsolete("cameraDepth has been renamed to cameraDepthTarget. #from(2021.1) #breakingFrom(2023.1) (UnityUpgradable) -> cameraDepthTarget", true)]
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::UnityEngine.Rendering.RenderTargetIdentifier cameraDepth => m_CameraDepthTarget.nameID;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		protected global::UnityEngine.Rendering.ProfilingSampler profilingExecute { get; set; }

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Rendering.RTHandle cameraColorTargetHandle
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Rendering.RTHandle cameraDepthTargetHandle
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		internal global::UnityEngine.Rendering.Universal.DebugHandler DebugHandler { get; }

		[global::System.Obsolete("Use cameraColorTargetHandle. #from(2022.1) #breakingFrom(2023.2)", true)]
		public global::UnityEngine.Rendering.RenderTargetIdentifier cameraColorTarget
		{
			get
			{
				throw new global::System.NotSupportedException("cameraColorTarget has been deprecated. Use cameraColorTargetHandle instead");
			}
		}

		protected global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRendererFeature> rendererFeatures => m_RendererFeatures;

		protected global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRenderPass> activeRenderPassQueue => m_ActiveRenderPassQueue;

		public global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderingFeatures supportedRenderingFeatures { get; set; } = new global::UnityEngine.Rendering.Universal.ScriptableRenderer.RenderingFeatures();

		public global::UnityEngine.Rendering.GraphicsDeviceType[] unsupportedGraphicsDeviceTypes { get; set; } = new global::UnityEngine.Rendering.GraphicsDeviceType[0];

		internal global::UnityEngine.Rendering.ContextContainer frameData => m_frameData;

		internal bool useDepthPriming { get; set; }

		internal bool stripShadowsOffVariants { get; set; }

		internal bool stripAdditionalLightOffVariants { get; set; }

		internal virtual bool supportsNativeRenderPassRendergraphCompiler => false;

		public virtual bool supportsGPUOcclusion => false;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public static void SetCameraMatrices(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.CameraData cameraData, bool setInverseMatrices)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public static void SetCameraMatrices(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool setInverseMatrices)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ConfigureCameraTarget(global::UnityEngine.Rendering.RTHandle colorTarget, global::UnityEngine.Rendering.RTHandle depthTarget)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public virtual void Setup(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public virtual void SetupLights(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		protected void SetupRenderPasses(in global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public virtual int SupportedCameraStackingTypes()
		{
			return 0;
		}

		public bool SupportsCameraStackingType(global::UnityEngine.Rendering.Universal.CameraRenderType cameraRenderType)
		{
			return (SupportedCameraStackingTypes() & (1 << (int)cameraRenderType)) != 0;
		}

		protected internal virtual bool SupportsMotionVectors()
		{
			return false;
		}

		protected internal virtual bool SupportsCameraOpaque()
		{
			return false;
		}

		protected internal virtual bool SupportsCameraNormals()
		{
			return false;
		}

		internal static void SetCameraMatrices(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool setInverseMatrices, bool isTargetFlipped)
		{
			if (cameraData.xr.enabled)
			{
				cameraData.PushBuiltinShaderConstantsXR(cmd, isTargetFlipped);
				global::UnityEngine.Rendering.Universal.XRSystemUniversal.MarkShaderProperties(cmd, cameraData.xrUniversal, isTargetFlipped);
				return;
			}
			global::UnityEngine.Matrix4x4 viewMatrix = cameraData.GetViewMatrix();
			global::UnityEngine.Matrix4x4 projectionMatrix = cameraData.GetProjectionMatrix();
			cmd.SetViewProjectionMatrices(viewMatrix, projectionMatrix);
			if (setInverseMatrices)
			{
				global::UnityEngine.Matrix4x4 gPUProjectionMatrix = cameraData.GetGPUProjectionMatrix(isTargetFlipped);
				global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.Inverse(viewMatrix);
				global::UnityEngine.Matrix4x4 matrix4x2 = global::UnityEngine.Matrix4x4.Inverse(gPUProjectionMatrix);
				global::UnityEngine.Matrix4x4 value = matrix4x * matrix4x2;
				global::UnityEngine.Matrix4x4 value2 = global::UnityEngine.Matrix4x4.Scale(new global::UnityEngine.Vector3(1f, 1f, -1f)) * viewMatrix;
				global::UnityEngine.Matrix4x4 inverse = value2.inverse;
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.worldToCameraMatrix, value2);
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.cameraToWorldMatrix, inverse);
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.inverseViewMatrix, matrix4x);
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.inverseProjectionMatrix, matrix4x2);
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.inverseViewAndProjectionMatrix, value);
			}
		}

		private void SetPerCameraShaderVariables(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Vector2Int cameraTargetSizeCopy, bool isTargetFlipped)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.setPerCameraShaderVariables))
			{
				global::UnityEngine.Camera camera = cameraData.camera;
				float num = cameraTargetSizeCopy.x;
				float num2 = cameraTargetSizeCopy.y;
				float num3 = camera.pixelWidth;
				float num4 = camera.pixelHeight;
				if (cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay)
				{
					num3 = cameraData.pixelWidth;
					num4 = cameraData.pixelHeight;
				}
				if (cameraData.xr.enabled)
				{
					num3 = cameraTargetSizeCopy.x;
					num4 = cameraTargetSizeCopy.y;
					useRenderPassEnabled = false;
				}
				if (camera.allowDynamicResolution)
				{
					num *= global::UnityEngine.ScalableBufferManager.widthScaleFactor;
					num2 *= global::UnityEngine.ScalableBufferManager.heightScaleFactor;
				}
				float nearClipPlane = camera.nearClipPlane;
				float farClipPlane = camera.farClipPlane;
				float num5 = (global::UnityEngine.Mathf.Approximately(nearClipPlane, 0f) ? 0f : (1f / nearClipPlane));
				float num6 = (global::UnityEngine.Mathf.Approximately(farClipPlane, 0f) ? 0f : (1f / farClipPlane));
				float w = (camera.orthographic ? 1f : 0f);
				float num7 = 1f - farClipPlane * num5;
				float num8 = farClipPlane * num5;
				global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(num7, num8, num7 * num6, num8 * num6);
				if (global::UnityEngine.SystemInfo.usesReversedZBuffer)
				{
					value.y += value.x;
					value.x = 0f - value.x;
					value.w += value.z;
					value.z = 0f - value.z;
				}
				if (cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay)
				{
					float x = (isTargetFlipped ? (-1f) : 1f);
					cmd.SetGlobalVector(value: new global::UnityEngine.Vector4(x, nearClipPlane, farClipPlane, 1f * num6), nameID: global::UnityEngine.Rendering.Universal.ShaderPropertyId.projectionParams);
				}
				global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4(camera.orthographicSize * cameraData.aspectRatio, camera.orthographicSize, 0f, w);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.worldSpaceCameraPos, cameraData.worldSpaceCameraPos);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.screenParams, new global::UnityEngine.Vector4(num3, num4, 1f + 1f / num3, 1f + 1f / num4));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.scaledScreenParams, new global::UnityEngine.Vector4(num, num2, 1f + 1f / num, 1f + 1f / num2));
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.zBufferParams, value);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.orthoParams, value2);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.screenSize, new global::UnityEngine.Vector4(num, num2, 1f / num, 1f / num2));
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SCREEN_COORD_OVERRIDE, cameraData.useScreenCoordOverride);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.screenSizeOverride, cameraData.screenSizeOverride);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.screenCoordScaleBias, cameraData.screenCoordScaleBias);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.rtHandleScale, global::UnityEngine.Vector4.one);
				float val = global::System.Math.Min((float)(0.0 - global::System.Math.Log(num3 / num, 2.0)), 0f);
				float val2 = global::System.Math.Min(cameraData.taaSettings.mipBias, 0f);
				val = global::System.Math.Min(val, val2);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.globalMipBias, new global::UnityEngine.Vector2(val, global::UnityEngine.Mathf.Pow(2f, val)));
				SetCameraMatrices(cmd, cameraData, setInverseMatrices: true, isTargetFlipped);
			}
		}

		private void SetPerCameraBillboardProperties(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Matrix4x4 worldToCameraMatrix = cameraData.GetViewMatrix();
			global::UnityEngine.Vector3 worldSpaceCameraPos = cameraData.worldSpaceCameraPos;
			cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.BillboardFaceCameraPos, global::UnityEngine.QualitySettings.billboardsFaceCameraPosition);
			CalculateBillboardProperties(in worldToCameraMatrix, out var billboardTangent, out var billboardNormal, out var cameraXZAngle);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.billboardNormal, new global::UnityEngine.Vector4(billboardNormal.x, billboardNormal.y, billboardNormal.z, 0f));
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.billboardTangent, new global::UnityEngine.Vector4(billboardTangent.x, billboardTangent.y, billboardTangent.z, 0f));
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.billboardCameraParams, new global::UnityEngine.Vector4(worldSpaceCameraPos.x, worldSpaceCameraPos.y, worldSpaceCameraPos.z, cameraXZAngle));
		}

		private static void CalculateBillboardProperties(in global::UnityEngine.Matrix4x4 worldToCameraMatrix, out global::UnityEngine.Vector3 billboardTangent, out global::UnityEngine.Vector3 billboardNormal, out float cameraXZAngle)
		{
			global::UnityEngine.Matrix4x4 matrix4x = worldToCameraMatrix;
			matrix4x = matrix4x.transpose;
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(matrix4x.m00, matrix4x.m10, matrix4x.m20);
			global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(matrix4x.m01, matrix4x.m11, matrix4x.m21);
			global::UnityEngine.Vector3 lhs = new global::UnityEngine.Vector3(matrix4x.m02, matrix4x.m12, matrix4x.m22);
			global::UnityEngine.Vector3 up = global::UnityEngine.Vector3.up;
			global::UnityEngine.Vector3 vector3 = global::UnityEngine.Vector3.Cross(lhs, up);
			billboardTangent = ((!global::UnityEngine.Mathf.Approximately(vector3.sqrMagnitude, 0f)) ? vector3.normalized : vector);
			billboardNormal = global::UnityEngine.Vector3.Cross(up, billboardTangent);
			billboardNormal = ((!global::UnityEngine.Mathf.Approximately(billboardNormal.sqrMagnitude, 0f)) ? billboardNormal.normalized : vector2);
			global::UnityEngine.Vector3 vector4 = new global::UnityEngine.Vector3(0f, 0f, 1f);
			float y = vector4.x * billboardTangent.z - vector4.z * billboardTangent.x;
			float x = vector4.x * billboardTangent.x + vector4.z * billboardTangent.z;
			cameraXZAngle = global::UnityEngine.Mathf.Atan2(y, x);
			if (cameraXZAngle < 0f)
			{
				cameraXZAngle += global::System.MathF.PI * 2f;
			}
		}

		private void SetPerCameraClippingPlaneProperties(global::UnityEngine.Rendering.RasterCommandBuffer cmd, in global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, bool isTargetFlipped)
		{
			global::UnityEngine.Matrix4x4 gPUProjectionMatrix = cameraData.GetGPUProjectionMatrix(isTargetFlipped);
			global::UnityEngine.Matrix4x4 viewMatrix = cameraData.GetViewMatrix();
			global::UnityEngine.Matrix4x4 worldToProjectionMatrix = global::UnityEngine.Rendering.CoreMatrixUtils.MultiplyProjectionMatrix(gPUProjectionMatrix, viewMatrix, cameraData.camera.orthographic);
			global::UnityEngine.Plane[] array = s_Planes;
			global::UnityEngine.GeometryUtility.CalculateFrustumPlanes(worldToProjectionMatrix, array);
			global::UnityEngine.Vector4[] array2 = s_VectorPlanes;
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new global::UnityEngine.Vector4(array[i].normal.x, array[i].normal.y, array[i].normal.z, array[i].distance);
			}
			cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.ShaderPropertyId.cameraWorldClipPlanes, array2);
		}

		private static void SetShaderTimeValues(global::UnityEngine.Rendering.IBaseCommandBuffer cmd, float time, float deltaTime, float smoothDeltaTime)
		{
			float f = time / 8f;
			float f2 = time / 4f;
			float f3 = time / 2f;
			float num = time - global::UnityEngine.Rendering.Universal.ShaderUtils.PersistentDeltaTime;
			global::UnityEngine.Vector4 value = time * new global::UnityEngine.Vector4(0.05f, 1f, 2f, 3f);
			global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4(global::UnityEngine.Mathf.Sin(f), global::UnityEngine.Mathf.Sin(f2), global::UnityEngine.Mathf.Sin(f3), global::UnityEngine.Mathf.Sin(time));
			global::UnityEngine.Vector4 value3 = new global::UnityEngine.Vector4(global::UnityEngine.Mathf.Cos(f), global::UnityEngine.Mathf.Cos(f2), global::UnityEngine.Mathf.Cos(f3), global::UnityEngine.Mathf.Cos(time));
			global::UnityEngine.Vector4 value4 = new global::UnityEngine.Vector4(deltaTime, 1f / deltaTime, smoothDeltaTime, 1f / smoothDeltaTime);
			global::UnityEngine.Vector4 value5 = new global::UnityEngine.Vector4(time, global::UnityEngine.Mathf.Sin(time), global::UnityEngine.Mathf.Cos(time), 0f);
			global::UnityEngine.Vector4 value6 = new global::UnityEngine.Vector4(num, global::UnityEngine.Mathf.Sin(num), global::UnityEngine.Mathf.Cos(num), 0f);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.time, value);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.sinTime, value2);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.cosTime, value3);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.deltaTime, value4);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.timeParameters, value5);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.lastTimeParameters, value6);
		}

		public ScriptableRenderer(global::UnityEngine.Rendering.Universal.ScriptableRendererData data)
		{
			foreach (global::UnityEngine.Rendering.Universal.ScriptableRendererFeature rendererFeature in data.rendererFeatures)
			{
				if (!(rendererFeature == null))
				{
					rendererFeature.Create();
					m_RendererFeatures.Add(rendererFeature);
				}
			}
			useRenderPassEnabled = data.useNativeRenderPass;
			Clear(global::UnityEngine.Rendering.Universal.CameraRenderType.Base);
			m_ActiveRenderPassQueue.Clear();
			if ((bool)global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset)
			{
				m_StoreActionsOptimizationSetting = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.storeActionsOptimization;
			}
			m_UseOptimizedStoreActions = m_StoreActionsOptimizationSetting != global::UnityEngine.Rendering.Universal.StoreActionsOptimization.Store;
		}

		public void Dispose()
		{
			for (int i = 0; i < m_RendererFeatures.Count; i++)
			{
				if (!(rendererFeatures[i] == null))
				{
					try
					{
						rendererFeatures[i].Dispose();
					}
					catch (global::System.Exception exception)
					{
						global::UnityEngine.Debug.LogException(exception);
					}
				}
			}
			Dispose(disposing: true);
			hasReleasedRTs = true;
			global::System.GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			DebugHandler?.Dispose();
		}

		internal virtual void ReleaseRenderTargets()
		{
		}

		public virtual void SetupCullingParameters(ref global::UnityEngine.Rendering.ScriptableCullingParameters cullingParameters, ref global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
		}

		public virtual void FinishRendering(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
		}

		public virtual void OnBeginRenderGraphFrame()
		{
		}

		internal virtual void OnRecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ScriptableRenderContext context)
		{
		}

		public virtual void OnEndRenderGraphFrame()
		{
		}

		private void InitRenderGraphFrame(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.ScriptableRenderer.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.ScriptableRenderer.PassData>(global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.initRenderGraphFrame.name, out passData, global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.initRenderGraphFrame, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\ScriptableRenderer.cs", 924);
			passData.renderer = this;
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ScriptableRenderer.PassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext rgContext)
			{
				global::UnityEngine.Rendering.UnsafeCommandBuffer cmd = rgContext.cmd;
				float time = global::UnityEngine.Time.time;
				float deltaTime = global::UnityEngine.Time.deltaTime;
				float smoothDeltaTime = global::UnityEngine.Time.smoothDeltaTime;
				ClearRenderingState(cmd);
				SetShaderTimeValues(cmd, time, deltaTime, smoothDeltaTime);
			});
		}

		internal void ProcessVFXCameraCommand(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Experimental.Rendering.XRPass xr = universalCameraData.xr;
			global::UnityEngine.Rendering.Universal.ScriptableRenderer.VFXProcessCameraPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.ScriptableRenderer.VFXProcessCameraPassData>("ProcessVFXCameraCommand", out passData, global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.vfxProcessCamera, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\ScriptableRenderer.cs", 962);
			passData.camera = universalCameraData.camera;
			passData.renderingData = renderingData;
			passData.cameraXRSettings.viewTotal = ((!xr.enabled) ? 1u : 2u);
			passData.cameraXRSettings.viewCount = ((!xr.enabled) ? 1u : ((uint)xr.viewCount));
			passData.cameraXRSettings.viewOffset = (uint)xr.multipassId;
			passData.xrPass = (xr.enabled ? xr : null);
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ScriptableRenderer.VFXProcessCameraPassData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				if (data.xrPass != null)
				{
					data.xrPass.StartSinglePass(context.cmd);
				}
				global::UnityEngine.Rendering.CommandBufferHelpers.VFXManager_ProcessCameraCommand(data.camera, context.cmd, data.cameraXRSettings, data.renderingData.cullResults);
				if (data.xrPass != null)
				{
					data.xrPass.StopSinglePass(context.cmd);
				}
			});
		}

		internal void SetupRenderGraphCameraProperties(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle target)
		{
			global::UnityEngine.Rendering.Universal.ScriptableRenderer.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.ScriptableRenderer.PassData>(global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.setupCamera.name, out passData, global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.setupCamera, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\ScriptableRenderer.cs", 992);
			passData.renderer = this;
			passData.cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			passData.cameraTargetSizeCopy = new global::UnityEngine.Vector2Int(passData.cameraData.cameraTargetDescriptor.width, passData.cameraData.cameraTargetDescriptor.height);
			passData.target = target;
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ScriptableRenderer.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				bool isTargetFlipped = global::UnityEngine.SystemInfo.graphicsUVStartsAtTop && global::UnityEngine.Rendering.Universal.RenderingUtils.IsHandleYFlipped(in context, in data.target);
				if (data.cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
				{
					context.cmd.SetupCameraProperties(data.cameraData.camera);
					data.renderer.SetPerCameraShaderVariables(context.cmd, data.cameraData, data.cameraTargetSizeCopy, isTargetFlipped);
				}
				else
				{
					data.renderer.SetPerCameraShaderVariables(context.cmd, data.cameraData, data.cameraTargetSizeCopy, isTargetFlipped);
					data.renderer.SetPerCameraClippingPlaneProperties(context.cmd, in data.cameraData, isTargetFlipped);
					data.renderer.SetPerCameraBillboardProperties(context.cmd, data.cameraData);
				}
				float time = global::UnityEngine.Time.time;
				float deltaTime = global::UnityEngine.Time.deltaTime;
				float smoothDeltaTime = global::UnityEngine.Time.smoothDeltaTime;
				SetShaderTimeValues(context.cmd, time, deltaTime, smoothDeltaTime);
			});
		}

		internal void DrawRenderGraphGizmos(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle color, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle depth, global::UnityEngine.Rendering.GizmoSubset gizmoSubset)
		{
		}

		internal void DrawRenderGraphWireOverlay(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle color)
		{
		}

		internal void BeginRenderGraphXRRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			if (!universalCameraData.xr.enabled)
			{
				return;
			}
			bool flag = global::UnityEngine.Experimental.Rendering.XRSystem.GetRenderViewportScale() == 1f;
			universalCameraData.xrUniversal.canFoveateIntermediatePasses = !global::UnityEngine.Rendering.Universal.PlatformAutoDetect.isXRMobile || flag;
			global::UnityEngine.Rendering.Universal.ScriptableRenderer.BeginXRPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.ScriptableRenderer.BeginXRPassData>("BeginXRRendering", out passData, global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.beginXRRendering, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\ScriptableRenderer.cs", 1140);
			passData.cameraData = universalCameraData;
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ScriptableRenderer.BeginXRPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				if (data.cameraData.xr.enabled)
				{
					if (data.cameraData.xrUniversal.isLateLatchEnabled)
					{
						data.cameraData.xrUniversal.canMarkLateLatch = true;
					}
					data.cameraData.xr.StartSinglePass(context.cmd);
					if (data.cameraData.xr.supportsFoveatedRendering)
					{
						context.cmd.ConfigureFoveatedRendering(data.cameraData.xr.foveatedRenderingInfo);
						if (global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps.HasFlag(global::UnityEngine.Rendering.FoveatedRenderingCaps.NonUniformRaster))
						{
							context.cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.FoveatedRenderingNonUniformRaster, value: true);
						}
					}
				}
			});
		}

		internal void EndRenderGraphXRRendering(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			if (!universalCameraData.xr.enabled)
			{
				return;
			}
			global::UnityEngine.Rendering.Universal.ScriptableRenderer.EndXRPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.ScriptableRenderer.EndXRPassData>("EndXRRendering", out passData, global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.endXRRendering, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\ScriptableRenderer.cs", 1180);
			passData.cameraData = universalCameraData;
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ScriptableRenderer.EndXRPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				if (data.cameraData.xr.enabled)
				{
					data.cameraData.xr.StopSinglePass(context.cmd);
				}
				if (global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps != global::UnityEngine.Rendering.FoveatedRenderingCaps.None)
				{
					if (global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps.HasFlag(global::UnityEngine.Rendering.FoveatedRenderingCaps.NonUniformRaster))
					{
						context.cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.FoveatedRenderingNonUniformRaster, value: false);
					}
					context.cmd.ConfigureFoveatedRendering(global::System.IntPtr.Zero);
				}
			});
		}

		private void SetEditorTarget(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			global::UnityEngine.Rendering.Universal.ScriptableRenderer.DummyData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder unsafeRenderGraphBuilder = renderGraph.AddUnsafePass<global::UnityEngine.Rendering.Universal.ScriptableRenderer.DummyData>("SetEditorTarget", out passData, global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.setEditorTarget, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\ScriptableRenderer.cs", 1213);
			unsafeRenderGraphBuilder.AllowPassCulling(value: false);
			unsafeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.ScriptableRenderer.DummyData data, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext context)
			{
				context.cmd.SetRenderTarget(global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget, global::UnityEngine.Rendering.RenderBufferLoadAction.Load, global::UnityEngine.Rendering.RenderBufferStoreAction.Store, global::UnityEngine.Rendering.RenderBufferLoadAction.Load, global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare);
			});
		}

		internal void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ScriptableRenderContext context)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.RecordRenderGraph)))
			{
				OnBeginRenderGraphFrame();
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.sortRenderPasses))
				{
					SortStable(m_ActiveRenderPassQueue);
				}
				InitRenderGraphFrame(renderGraph);
				using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.recordRenderGraph))
				{
					OnRecordRenderGraph(renderGraph, context);
				}
				OnEndRenderGraphFrame();
			}
		}

		internal void FinishRenderGraphRendering(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData universalCameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			OnFinishRenderGraphRendering(cmd);
			InternalFinishRenderingCommon(cmd, universalCameraData.resolveFinalTarget);
		}

		internal virtual void OnFinishRenderGraphRendering(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
		}

		internal void RecordCustomRenderGraphPassesInEventRange(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent eventStart, global::UnityEngine.Rendering.Universal.RenderPassEvent eventEnd)
		{
			if (eventStart == eventEnd)
			{
				return;
			}
			foreach (global::UnityEngine.Rendering.Universal.ScriptableRenderPass item in m_ActiveRenderPassQueue)
			{
				if (item.renderPassEvent >= eventStart && item.renderPassEvent < eventEnd)
				{
					item.RecordRenderGraph(renderGraph, m_frameData);
				}
			}
		}

		internal void CalculateSplitEventRange(global::UnityEngine.Rendering.Universal.RenderPassEvent startInjectionPoint, global::UnityEngine.Rendering.Universal.RenderPassEvent targetEvent, out global::UnityEngine.Rendering.Universal.RenderPassEvent startEvent, out global::UnityEngine.Rendering.Universal.RenderPassEvent splitEvent, out global::UnityEngine.Rendering.Universal.RenderPassEvent endEvent)
		{
			int renderPassEventRange = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetRenderPassEventRange(startInjectionPoint);
			startEvent = startInjectionPoint;
			endEvent = startEvent + renderPassEventRange;
			splitEvent = (global::UnityEngine.Rendering.Universal.RenderPassEvent)global::System.Math.Clamp((int)targetEvent, (int)startEvent, (int)endEvent);
		}

		internal void RecordCustomRenderGraphPasses(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent startInjectionPoint, global::UnityEngine.Rendering.Universal.RenderPassEvent endInjectionPoint)
		{
			int renderPassEventRange = global::UnityEngine.Rendering.Universal.ScriptableRenderPass.GetRenderPassEventRange(endInjectionPoint);
			RecordCustomRenderGraphPassesInEventRange(renderGraph, startInjectionPoint, endInjectionPoint + renderPassEventRange);
		}

		internal void RecordCustomRenderGraphPasses(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.Universal.RenderPassEvent injectionPoint)
		{
			RecordCustomRenderGraphPasses(renderGraph, injectionPoint, injectionPoint);
		}

		public void EnqueuePass(global::UnityEngine.Rendering.Universal.ScriptableRenderPass pass)
		{
			m_ActiveRenderPassQueue.Add(pass);
		}

		protected static global::UnityEngine.Rendering.ClearFlag GetCameraClearFlag(ref global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			return GetCameraClearFlag(cameraData.universalCameraData);
		}

		protected static global::UnityEngine.Rendering.ClearFlag GetCameraClearFlag(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.CameraClearFlags clearFlags = cameraData.camera.clearFlags;
			if (cameraData.renderType == global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay)
			{
				if (!cameraData.clearDepth)
				{
					return global::UnityEngine.Rendering.ClearFlag.None;
				}
				return global::UnityEngine.Rendering.ClearFlag.DepthStencil;
			}
			global::UnityEngine.Rendering.Universal.DebugHandler debugHandler = cameraData.renderer.DebugHandler;
			if (debugHandler != null && debugHandler.IsActiveForCamera(cameraData.isPreviewCamera) && debugHandler.IsScreenClearNeeded)
			{
				return global::UnityEngine.Rendering.ClearFlag.All;
			}
			if (clearFlags == global::UnityEngine.CameraClearFlags.Skybox && global::UnityEngine.RenderSettings.skybox != null && cameraData.postProcessEnabled && cameraData.xr.enabled)
			{
				return global::UnityEngine.Rendering.ClearFlag.All;
			}
			if ((clearFlags == global::UnityEngine.CameraClearFlags.Skybox && global::UnityEngine.RenderSettings.skybox != null) || clearFlags == global::UnityEngine.CameraClearFlags.Nothing)
			{
				if (cameraData.cameraTargetDescriptor.msaaSamples > 1)
				{
					cameraData.camera.backgroundColor = global::UnityEngine.Color.black;
					return global::UnityEngine.Rendering.ClearFlag.All;
				}
				return global::UnityEngine.Rendering.ClearFlag.DepthStencil;
			}
			return global::UnityEngine.Rendering.ClearFlag.All;
		}

		internal void OnPreCullRenderPasses(in global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			for (int i = 0; i < rendererFeatures.Count; i++)
			{
				if (rendererFeatures[i].isActive)
				{
					rendererFeatures[i].OnCameraPreCull(this, in cameraData);
				}
			}
		}

		internal void AddRenderPasses(ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.addRenderPasses))
			{
				for (int i = 0; i < rendererFeatures.Count; i++)
				{
					if (rendererFeatures[i].isActive)
					{
						rendererFeatures[i].AddRenderPasses(this, ref renderingData);
					}
				}
				int count = activeRenderPassQueue.Count;
				for (int num = count - 1; num >= 0; num--)
				{
					if (activeRenderPassQueue[num] == null)
					{
						activeRenderPassQueue.RemoveAt(num);
					}
				}
				if (count > 0 && m_StoreActionsOptimizationSetting == global::UnityEngine.Rendering.Universal.StoreActionsOptimization.Auto)
				{
					m_UseOptimizedStoreActions = false;
				}
			}
		}

		private static void ClearRenderingState(global::UnityEngine.Rendering.IBaseCommandBuffer cmd)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.clearRenderingState))
			{
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadows, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MainLightShadowCascades, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.AdditionalLightsVertex, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.AdditionalLightsPixel, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ClusterLightLoop, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ForwardPlus, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.AdditionalLightShadows, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ReflectionProbeBlending, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ReflectionProbeBoxProjection, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ReflectionProbeAtlas, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadows, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsLow, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsMedium, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.SoftShadowsHigh, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.MixedLightingSubtractive, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightmapShadowMixing, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.ShadowsShadowMask, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LinearToSRGBConversion, value: false);
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightLayers, value: false);
				cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionPass.s_AmbientOcclusionParamID, global::UnityEngine.Vector4.zero);
			}
		}

		internal void Clear(global::UnityEngine.Rendering.Universal.CameraRenderType cameraType)
		{
			m_ActiveColorAttachments[0] = k_CameraTarget;
			for (int i = 1; i < m_ActiveColorAttachments.Length; i++)
			{
				m_ActiveColorAttachments[i] = null;
			}
			for (int j = 0; j < m_ActiveColorAttachments.Length; j++)
			{
				m_ActiveColorAttachmentIDs[j] = m_ActiveColorAttachments[j]?.nameID ?? ((global::UnityEngine.Rendering.RenderTargetIdentifier)0);
			}
			m_ActiveDepthAttachment = k_CameraTarget;
			m_FirstTimeCameraColorTargetIsBound = cameraType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base;
			m_FirstTimeCameraDepthTargetIsBound = true;
			m_CameraColorTarget = null;
			m_CameraDepthTarget = null;
		}

		internal bool IsSceneFilteringEnabled(global::UnityEngine.Camera camera)
		{
			return false;
		}

		internal virtual void SwapColorBuffer(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
		}

		internal virtual void EnableSwapBufferMSAA(bool enable)
		{
		}

		private void InternalFinishRenderingCommon(global::UnityEngine.Rendering.CommandBuffer cmd, bool resolveFinalTarget)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.ScriptableRenderer.Profiling.internalFinishRenderingCommon))
			{
				for (int i = 0; i < m_ActiveRenderPassQueue.Count; i++)
				{
					m_ActiveRenderPassQueue[i].FrameCleanup(cmd);
				}
				if (resolveFinalTarget)
				{
					FinishRendering(cmd);
					m_IsPipelineExecuting = false;
				}
				m_ActiveRenderPassQueue.Clear();
			}
		}

		private protected int AdjustAndGetScreenMSAASamples(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, bool useIntermediateColorTarget)
		{
			if (!global::UnityEngine.SystemInfo.supportsMultisampledBackBuffer)
			{
				return 1;
			}
			if (global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.canOptimizeScreenMSAASamples && useIntermediateColorTarget && renderGraph.nativeRenderPassesEnabled && global::UnityEngine.Screen.msaaSamples > 1)
			{
				global::UnityEngine.Screen.SetMSAASamples(1);
			}
			if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.OSXPlayer && global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.IPhonePlayer)
			{
				return global::UnityEngine.Mathf.Max(global::UnityEngine.Screen.msaaSamples, 1);
			}
			return global::UnityEngine.Mathf.Max(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.startFrameScreenMSAASamples, 1);
		}

		internal static void SortStable(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRenderPass> list)
		{
			for (int i = 1; i < list.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.ScriptableRenderPass scriptableRenderPass = list[i];
				int num = i - 1;
				while (num >= 0 && scriptableRenderPass < list[num])
				{
					list[num + 1] = list[num];
					num--;
				}
				list[num + 1] = scriptableRenderPass;
			}
		}
	}
}
