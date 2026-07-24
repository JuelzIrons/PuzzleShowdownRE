namespace UnityEngine.Rendering.Universal.Internal
{
	public class ColorGradingLutPass : global::UnityEngine.Rendering.Universal.ScriptableRenderPass
	{
		private class PassData
		{
			internal global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData;

			internal global::UnityEngine.Rendering.Universal.UniversalPostProcessingData postProcessingData;

			internal global::UnityEngine.Material lutBuilderLdr;

			internal global::UnityEngine.Material lutBuilderHdr;

			internal bool allowColorGradingACESHDR;

			internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle internalLut;
		}

		private static class ShaderConstants
		{
			public static readonly int _Lut_Params = global::UnityEngine.Shader.PropertyToID("_Lut_Params");

			public static readonly int _ColorBalance = global::UnityEngine.Shader.PropertyToID("_ColorBalance");

			public static readonly int _ColorFilter = global::UnityEngine.Shader.PropertyToID("_ColorFilter");

			public static readonly int _ChannelMixerRed = global::UnityEngine.Shader.PropertyToID("_ChannelMixerRed");

			public static readonly int _ChannelMixerGreen = global::UnityEngine.Shader.PropertyToID("_ChannelMixerGreen");

			public static readonly int _ChannelMixerBlue = global::UnityEngine.Shader.PropertyToID("_ChannelMixerBlue");

			public static readonly int _HueSatCon = global::UnityEngine.Shader.PropertyToID("_HueSatCon");

			public static readonly int _Lift = global::UnityEngine.Shader.PropertyToID("_Lift");

			public static readonly int _Gamma = global::UnityEngine.Shader.PropertyToID("_Gamma");

			public static readonly int _Gain = global::UnityEngine.Shader.PropertyToID("_Gain");

			public static readonly int _Shadows = global::UnityEngine.Shader.PropertyToID("_Shadows");

			public static readonly int _Midtones = global::UnityEngine.Shader.PropertyToID("_Midtones");

			public static readonly int _Highlights = global::UnityEngine.Shader.PropertyToID("_Highlights");

			public static readonly int _ShaHiLimits = global::UnityEngine.Shader.PropertyToID("_ShaHiLimits");

			public static readonly int _SplitShadows = global::UnityEngine.Shader.PropertyToID("_SplitShadows");

			public static readonly int _SplitHighlights = global::UnityEngine.Shader.PropertyToID("_SplitHighlights");

			public static readonly int _CurveMaster = global::UnityEngine.Shader.PropertyToID("_CurveMaster");

			public static readonly int _CurveRed = global::UnityEngine.Shader.PropertyToID("_CurveRed");

			public static readonly int _CurveGreen = global::UnityEngine.Shader.PropertyToID("_CurveGreen");

			public static readonly int _CurveBlue = global::UnityEngine.Shader.PropertyToID("_CurveBlue");

			public static readonly int _CurveHueVsHue = global::UnityEngine.Shader.PropertyToID("_CurveHueVsHue");

			public static readonly int _CurveHueVsSat = global::UnityEngine.Shader.PropertyToID("_CurveHueVsSat");

			public static readonly int _CurveLumVsSat = global::UnityEngine.Shader.PropertyToID("_CurveLumVsSat");

			public static readonly int _CurveSatVsSat = global::UnityEngine.Shader.PropertyToID("_CurveSatVsSat");
		}

		private readonly global::UnityEngine.Material m_LutBuilderLdr;

		private readonly global::UnityEngine.Material m_LutBuilderHdr;

		internal readonly global::UnityEngine.Experimental.Rendering.GraphicsFormat m_HdrLutFormat;

		internal readonly global::UnityEngine.Experimental.Rendering.GraphicsFormat m_LdrLutFormat;

		private bool m_AllowColorGradingACESHDR = true;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public override void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public ColorGradingLutPass(global::UnityEngine.Rendering.Universal.RenderPassEvent evt, global::UnityEngine.Rendering.Universal.PostProcessData data)
		{
			base.profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("Blit Color LUT");
			base.renderPassEvent = evt;
			m_LutBuilderLdr = Load(data.shaders.lutBuilderLdrPS);
			m_LutBuilderHdr = Load(data.shaders.lutBuilderHdrPS);
			if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend))
			{
				m_HdrLutFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat;
			}
			else if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Blend))
			{
				m_HdrLutFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32;
			}
			else
			{
				m_HdrLutFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
			}
			m_LdrLutFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
			if (global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 && global::UnityEngine.Graphics.minOpenGLESVersion <= global::UnityEngine.Rendering.OpenGLESVersion.OpenGLES30 && global::UnityEngine.SystemInfo.graphicsDeviceName.StartsWith("Adreno (TM) 3"))
			{
				m_AllowColorGradingACESHDR = false;
			}
			static global::UnityEngine.Material Load(global::UnityEngine.Shader shader)
			{
				if (shader == null)
				{
					global::UnityEngine.Debug.LogError("Missing shader. ColorGradingLutPass render pass will not execute. Check for missing reference in the renderer resources.");
					return null;
				}
				return global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(shader);
			}
		}

		public void Setup(in global::UnityEngine.Rendering.RTHandle internalLut)
		{
		}

		public void ConfigureDescriptor(in global::UnityEngine.Rendering.Universal.PostProcessingData postProcessingData, out global::UnityEngine.RenderTextureDescriptor descriptor, out global::UnityEngine.FilterMode filterMode)
		{
			ConfigureDescriptor(postProcessingData.universalPostProcessingData, out descriptor, out filterMode);
		}

		public void ConfigureDescriptor(in global::UnityEngine.Rendering.Universal.UniversalPostProcessingData postProcessingData, out global::UnityEngine.RenderTextureDescriptor descriptor, out global::UnityEngine.FilterMode filterMode)
		{
			bool num = postProcessingData.gradingMode == global::UnityEngine.Rendering.Universal.ColorGradingMode.HighDynamicRange;
			int lutSize = postProcessingData.lutSize;
			int width = lutSize * lutSize;
			global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat = (num ? m_HdrLutFormat : m_LdrLutFormat);
			descriptor = new global::UnityEngine.RenderTextureDescriptor(width, lutSize, colorFormat, 0);
			descriptor.vrUsage = global::UnityEngine.VRTextureUsage.None;
			filterMode = global::UnityEngine.FilterMode.Bilinear;
		}

		private static void ExecutePass(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.PassData passData, global::UnityEngine.Rendering.RTHandle internalLutTarget)
		{
			global::UnityEngine.Material lutBuilderLdr = passData.lutBuilderLdr;
			global::UnityEngine.Material lutBuilderHdr = passData.lutBuilderHdr;
			bool allowColorGradingACESHDR = passData.allowColorGradingACESHDR;
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.ColorGradingLUT)))
			{
				global::UnityEngine.Rendering.VolumeStack stack = global::UnityEngine.Rendering.VolumeManager.instance.stack;
				global::UnityEngine.Rendering.Universal.ChannelMixer component = stack.GetComponent<global::UnityEngine.Rendering.Universal.ChannelMixer>();
				global::UnityEngine.Rendering.Universal.ColorAdjustments component2 = stack.GetComponent<global::UnityEngine.Rendering.Universal.ColorAdjustments>();
				global::UnityEngine.Rendering.Universal.ColorCurves component3 = stack.GetComponent<global::UnityEngine.Rendering.Universal.ColorCurves>();
				global::UnityEngine.Rendering.Universal.LiftGammaGain component4 = stack.GetComponent<global::UnityEngine.Rendering.Universal.LiftGammaGain>();
				global::UnityEngine.Rendering.Universal.ShadowsMidtonesHighlights component5 = stack.GetComponent<global::UnityEngine.Rendering.Universal.ShadowsMidtonesHighlights>();
				global::UnityEngine.Rendering.Universal.SplitToning component6 = stack.GetComponent<global::UnityEngine.Rendering.Universal.SplitToning>();
				global::UnityEngine.Rendering.Universal.Tonemapping component7 = stack.GetComponent<global::UnityEngine.Rendering.Universal.Tonemapping>();
				global::UnityEngine.Rendering.Universal.WhiteBalance component8 = stack.GetComponent<global::UnityEngine.Rendering.Universal.WhiteBalance>();
				bool num = passData.postProcessingData.gradingMode == global::UnityEngine.Rendering.Universal.ColorGradingMode.HighDynamicRange;
				global::UnityEngine.Material material = (num ? lutBuilderHdr : lutBuilderLdr);
				global::UnityEngine.Vector3 vector = global::UnityEngine.Rendering.ColorUtils.ColorBalanceToLMSCoeffs(component8.temperature.value, component8.tint.value);
				global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(component2.hueShift.value / 360f, component2.saturation.value / 100f + 1f, component2.contrast.value / 100f + 1f, 0f);
				global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4(component.redOutRedIn.value / 100f, component.redOutGreenIn.value / 100f, component.redOutBlueIn.value / 100f, 0f);
				global::UnityEngine.Vector4 value3 = new global::UnityEngine.Vector4(component.greenOutRedIn.value / 100f, component.greenOutGreenIn.value / 100f, component.greenOutBlueIn.value / 100f, 0f);
				global::UnityEngine.Vector4 value4 = new global::UnityEngine.Vector4(component.blueOutRedIn.value / 100f, component.blueOutGreenIn.value / 100f, component.blueOutBlueIn.value / 100f, 0f);
				global::UnityEngine.Vector4 value5 = new global::UnityEngine.Vector4(component5.shadowsStart.value, component5.shadowsEnd.value, component5.highlightsStart.value, component5.highlightsEnd.value);
				(global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::UnityEngine.Vector4) tuple = global::UnityEngine.Rendering.ColorUtils.PrepareShadowsMidtonesHighlights(component5.shadows.value, component5.midtones.value, component5.highlights.value);
				global::UnityEngine.Vector4 item = tuple.Item1;
				global::UnityEngine.Vector4 item2 = tuple.Item2;
				global::UnityEngine.Vector4 item3 = tuple.Item3;
				(global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::UnityEngine.Vector4) tuple2 = global::UnityEngine.Rendering.ColorUtils.PrepareLiftGammaGain(component4.lift.value, component4.gamma.value, component4.gain.value);
				global::UnityEngine.Vector4 item4 = tuple2.Item1;
				global::UnityEngine.Vector4 item5 = tuple2.Item2;
				global::UnityEngine.Vector4 item6 = tuple2.Item3;
				(global::UnityEngine.Vector4, global::UnityEngine.Vector4) tuple3 = global::UnityEngine.Rendering.ColorUtils.PrepareSplitToning((global::UnityEngine.Vector4)component6.shadows.value, (global::UnityEngine.Vector4)component6.highlights.value, component6.balance.value);
				global::UnityEngine.Vector4 item7 = tuple3.Item1;
				global::UnityEngine.Vector4 item8 = tuple3.Item2;
				int lutSize = passData.postProcessingData.lutSize;
				int num2 = lutSize * lutSize;
				material.SetVector(value: new global::UnityEngine.Vector4(lutSize, 0.5f / (float)num2, 0.5f / (float)lutSize, (float)lutSize / ((float)lutSize - 1f)), nameID: global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._Lut_Params);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._ColorBalance, vector);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._ColorFilter, component2.colorFilter.value.linear);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._ChannelMixerRed, value2);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._ChannelMixerGreen, value3);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._ChannelMixerBlue, value4);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._HueSatCon, value);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._Lift, item4);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._Gamma, item5);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._Gain, item6);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._Shadows, item);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._Midtones, item2);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._Highlights, item3);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._ShaHiLimits, value5);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._SplitShadows, item7);
				material.SetVector(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._SplitHighlights, item8);
				material.SetTexture(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._CurveMaster, component3.master.value.GetTexture());
				material.SetTexture(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._CurveRed, component3.red.value.GetTexture());
				material.SetTexture(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._CurveGreen, component3.green.value.GetTexture());
				material.SetTexture(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._CurveBlue, component3.blue.value.GetTexture());
				material.SetTexture(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._CurveHueVsHue, component3.hueVsHue.value.GetTexture());
				material.SetTexture(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._CurveHueVsSat, component3.hueVsSat.value.GetTexture());
				material.SetTexture(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._CurveLumVsSat, component3.lumVsSat.value.GetTexture());
				material.SetTexture(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.ShaderConstants._CurveSatVsSat, component3.satVsSat.value.GetTexture());
				if (num)
				{
					material.shaderKeywords = null;
					switch (component7.mode.value)
					{
					case global::UnityEngine.Rendering.Universal.TonemappingMode.Neutral:
						material.EnableKeyword("_TONEMAP_NEUTRAL");
						break;
					case global::UnityEngine.Rendering.Universal.TonemappingMode.ACES:
						material.EnableKeyword(allowColorGradingACESHDR ? "_TONEMAP_ACES" : "_TONEMAP_NEUTRAL");
						break;
					}
					if (passData.cameraData.isHDROutputActive)
					{
						global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.GetHDROutputLuminanceParameters(passData.cameraData.hdrDisplayInformation, passData.cameraData.hdrDisplayColorGamut, component7, out var hdrOutputParameters);
						global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.GetHDROutputGradingParameters(component7, out var hdrOutputParameters2);
						material.SetVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.hdrOutputLuminanceParams, hdrOutputParameters);
						material.SetVector(global::UnityEngine.Rendering.Universal.ShaderPropertyId.hdrOutputGradingParams, hdrOutputParameters2);
						global::UnityEngine.Rendering.HDROutputUtils.ConfigureHDROutput(material, passData.cameraData.hdrDisplayColorGamut, global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion);
					}
				}
				passData.cameraData.xr.StopSinglePass(cmd);
				global::UnityEngine.Rendering.Blitter.BlitTexture(cmd, internalLutTarget, global::UnityEngine.Vector2.one, material, 0);
				passData.cameraData.xr.StartSinglePass(cmd);
			}
		}

		internal void Render(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData, out global::UnityEngine.Rendering.RenderGraphModule.TextureHandle internalColorLut)
		{
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalPostProcessingData postProcessingData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();
			global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.PassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.PassData>(base.passName, out passData, base.profilingSampler, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\Runtime\\Passes\\ColorGradingLutPass.cs", 294);
			ConfigureDescriptor(in postProcessingData, out var descriptor, out var filterMode);
			internalColorLut = global::UnityEngine.Rendering.Universal.UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_InternalGradingLut", clear: true, filterMode);
			passData.cameraData = cameraData;
			passData.postProcessingData = postProcessingData;
			passData.internalLut = internalColorLut;
			rasterRenderGraphBuilder.SetRenderAttachment(internalColorLut, 0, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.WriteAll);
			passData.lutBuilderLdr = m_LutBuilderLdr;
			passData.lutBuilderHdr = m_LutBuilderHdr;
			passData.allowColorGradingACESHDR = m_AllowColorGradingACESHDR;
			rasterRenderGraphBuilder.AllowPassCulling(value: false);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.Universal.Internal.ColorGradingLutPass.PassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext context)
			{
				ExecutePass(context.cmd, data, data.internalLut);
			});
		}

		public override void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Rendering.Universal.UniversalResourceData universalResourceData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalResourceData>();
			Render(renderGraph, frameData, out var internalColorLut);
			universalResourceData.internalColorLut = internalColorLut;
		}

		public void Cleanup()
		{
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_LutBuilderLdr);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_LutBuilderHdr);
		}
	}
}
