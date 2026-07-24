namespace UnityEngine.Rendering.Universal
{
	internal static class RenderingLayerUtils
	{
		public enum Event
		{
			DepthNormalPrePass = 0,
			Opaque = 1
		}

		public enum MaskSize
		{
			Bits8 = 0,
			Bits16 = 1,
			Bits24 = 2,
			Bits32 = 3
		}

		public static void CombineRendererEvents(bool isDeferred, int msaaSampleCount, global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event rendererEvent, ref global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event combinedEvent)
		{
			if (msaaSampleCount > 1 && !isDeferred)
			{
				combinedEvent = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.DepthNormalPrePass;
			}
			else
			{
				combinedEvent = Combine(combinedEvent, rendererEvent);
			}
		}

		public static bool RequireRenderingLayers(global::UnityEngine.Rendering.Universal.UniversalRenderer universalRenderer, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRendererFeature> rendererFeatures, int msaaSampleCount, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event combinedEvent, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize combinedMaskSize)
		{
			global::UnityEngine.Rendering.Universal.RenderingMode renderingModeActual = universalRenderer.renderingModeActual;
			bool accurateGbufferNormals = universalRenderer.accurateGbufferNormals;
			return RequireRenderingLayers(rendererFeatures, renderingModeActual, accurateGbufferNormals, msaaSampleCount, out combinedEvent, out combinedMaskSize);
		}

		internal static bool RequireRenderingLayers(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ScriptableRendererFeature> rendererFeatures, global::UnityEngine.Rendering.Universal.RenderingMode renderingMode, bool accurateGbufferNormals, int msaaSampleCount, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event combinedEvent, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize combinedMaskSize)
		{
			combinedEvent = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.Opaque;
			combinedMaskSize = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits8;
			bool isDeferred = renderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.Deferred || renderingMode == global::UnityEngine.Rendering.Universal.RenderingMode.DeferredPlus;
			bool flag = false;
			foreach (global::UnityEngine.Rendering.Universal.ScriptableRendererFeature rendererFeature in rendererFeatures)
			{
				if (rendererFeature.isActive)
				{
					flag |= rendererFeature.RequireRenderingLayers(isDeferred, accurateGbufferNormals, out var atEvent, out var maskSize);
					combinedEvent = Combine(combinedEvent, atEvent);
					combinedMaskSize = Combine(combinedMaskSize, maskSize);
				}
			}
			if (msaaSampleCount > 1 && combinedEvent == global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.Opaque)
			{
				combinedEvent = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.DepthNormalPrePass;
			}
			if ((bool)global::UnityEngine.Rendering.RenderPipelineGlobalSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineGlobalSettings, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline>.instance)
			{
				global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize2 = GetMaskSize(global::UnityEngine.RenderingLayerMask.GetRenderingLayerCount());
				combinedMaskSize = Combine(combinedMaskSize, maskSize2);
			}
			return flag;
		}

		public static void SetupProperties(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize)
		{
			SetupProperties(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd), maskSize);
		}

		internal static void SetupProperties(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize)
		{
			int bits = GetBits(maskSize);
			uint value = ((bits != 32) ? ((uint)((1 << bits) - 1)) : uint.MaxValue);
			cmd.SetGlobalInt(global::UnityEngine.Rendering.Universal.ShaderPropertyId.renderingLayerMaxInt, (int)value);
		}

		public static global::UnityEngine.Experimental.Rendering.GraphicsFormat GetFormat(global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize)
		{
			switch (maskSize)
			{
			case global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits8:
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UInt;
			case global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits16:
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_UInt;
			case global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits24:
			case global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits32:
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_UInt;
			default:
				throw new global::System.NotImplementedException();
			}
		}

		public static uint ToValidRenderingLayers(uint renderingLayers)
		{
			if ((bool)global::UnityEngine.Rendering.RenderPipelineGlobalSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineGlobalSettings, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline>.instance)
			{
				return global::UnityEngine.RenderingLayerMask.GetDefinedRenderingLayersCombinedMaskValue() & renderingLayers;
			}
			return renderingLayers;
		}

		private static global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize GetMaskSize(int bits)
		{
			return ((bits + 7) / 8) switch
			{
				0 => global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits8, 
				1 => global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits8, 
				2 => global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits16, 
				3 => global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits24, 
				4 => global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits32, 
				_ => global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits32, 
			};
		}

		private static int GetBits(global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize)
		{
			return maskSize switch
			{
				global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits8 => 8, 
				global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits16 => 16, 
				global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits24 => 24, 
				global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits32 => 32, 
				_ => throw new global::System.NotImplementedException(), 
			};
		}

		private static global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event Combine(global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event a, global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event b)
		{
			return (global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event)global::UnityEngine.Mathf.Min((int)a, (int)b);
		}

		private static global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize Combine(global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize a, global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize b)
		{
			return (global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize)global::UnityEngine.Mathf.Max((int)a, (int)b);
		}
	}
}
