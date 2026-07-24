namespace UnityEngine.Experimental.Rendering
{
	internal static class XRMirrorView
	{
		private static readonly global::UnityEngine.MaterialPropertyBlock s_MirrorViewMaterialProperty = new global::UnityEngine.MaterialPropertyBlock();

		private static readonly global::UnityEngine.Rendering.ProfilingSampler k_MirrorViewProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("XR Mirror View");

		private static readonly int k_SourceTex = global::UnityEngine.Shader.PropertyToID("_SourceTex");

		private static readonly int k_SourceTexArraySlice = global::UnityEngine.Shader.PropertyToID("_SourceTexArraySlice");

		private static readonly int k_ScaleBias = global::UnityEngine.Shader.PropertyToID("_ScaleBias");

		private static readonly int k_ScaleBiasRt = global::UnityEngine.Shader.PropertyToID("_ScaleBiasRt");

		private static readonly int k_SRGBRead = global::UnityEngine.Shader.PropertyToID("_SRGBRead");

		private static readonly int k_SRGBWrite = global::UnityEngine.Shader.PropertyToID("_SRGBWrite");

		private static readonly int k_MaxNits = global::UnityEngine.Shader.PropertyToID("_MaxNits");

		private static readonly int k_SourceMaxNits = global::UnityEngine.Shader.PropertyToID("_SourceMaxNits");

		private static readonly int k_SourceHDREncoding = global::UnityEngine.Shader.PropertyToID("_SourceHDREncoding");

		private static readonly int k_ColorTransform = global::UnityEngine.Shader.PropertyToID("_ColorTransform");

		internal static void RenderMirrorView(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Camera camera, global::UnityEngine.Material mat, global::UnityEngine.XR.XRDisplaySubsystem display)
		{
			if ((global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android && !global::UnityEngine.Rendering.XRGraphicsAutomatedTests.running) || display == null || !display.running || mat == null)
			{
				return;
			}
			int preferredMirrorBlitMode = display.GetPreferredMirrorBlitMode();
			if (display.GetMirrorViewBlitDesc(null, out var outDesc, preferredMirrorBlitMode))
			{
				using (new global::UnityEngine.Rendering.ProfilingScope(cmd, k_MirrorViewProfilingSampler))
				{
					cmd.SetRenderTarget((camera.targetTexture != null) ? ((global::UnityEngine.Rendering.RenderTargetIdentifier)camera.targetTexture) : new global::UnityEngine.Rendering.RenderTargetIdentifier(global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget));
					if (outDesc.nativeBlitAvailable)
					{
						display.AddGraphicsThreadMirrorViewBlit(cmd, outDesc.nativeBlitInvalidStates, preferredMirrorBlitMode);
					}
					else
					{
						for (int i = 0; i < outDesc.blitParamsCount; i++)
						{
							outDesc.GetBlitParameter(i, out var blitParameter);
							global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(blitParameter.srcRect.width, blitParameter.srcRect.height, blitParameter.srcRect.x, blitParameter.srcRect.y);
							global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4(blitParameter.destRect.width, blitParameter.destRect.height, blitParameter.destRect.x, blitParameter.destRect.y);
							if (camera.targetTexture != null || camera.cameraType == global::UnityEngine.CameraType.SceneView || camera.cameraType == global::UnityEngine.CameraType.Preview)
							{
								value.y = 0f - value.y;
								value.w += blitParameter.srcRect.height;
							}
							global::UnityEngine.HDROutputSettings main = global::UnityEngine.HDROutputSettings.main;
							if (blitParameter.srcHdrEncoded || main.active)
							{
								global::UnityEngine.ColorGamut gamut = (main.active ? main.displayColorGamut : global::UnityEngine.ColorGamut.sRGB);
								global::UnityEngine.ColorGamut gamut2 = (blitParameter.srcHdrEncoded ? blitParameter.srcHdrColorGamut : global::UnityEngine.ColorGamut.sRGB);
								global::UnityEngine.ColorPrimaries colorPrimaries = global::UnityEngine.ColorGamutUtility.GetColorPrimaries(gamut);
								global::UnityEngine.ColorPrimaries colorPrimaries2 = global::UnityEngine.ColorGamutUtility.GetColorPrimaries(gamut2);
								global::UnityEngine.Rendering.HDROutputUtils.ConfigureHDROutput(s_MirrorViewMaterialProperty, gamut);
								global::UnityEngine.Rendering.HDROutputUtils.ConfigureHDROutput(mat, global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion | global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding);
								global::UnityEngine.Rendering.HDROutputUtils.GetColorEncodingForGamut(gamut2, out var encoding);
								s_MirrorViewMaterialProperty.SetInteger(k_SourceHDREncoding, encoding);
								global::Unity.Mathematics.float3x3 a = global::Unity.Mathematics.float3x3.identity;
								switch (colorPrimaries2)
								{
								case global::UnityEngine.ColorPrimaries.Rec709:
									a = global::UnityEngine.Rendering.ColorSpaceUtils.Rec709ToRec2020Mat;
									break;
								case global::UnityEngine.ColorPrimaries.P3:
									a = global::UnityEngine.Rendering.ColorSpaceUtils.P3D65ToRec2020Mat;
									break;
								}
								global::Unity.Mathematics.float3x3 b = global::Unity.Mathematics.float3x3.identity;
								switch (colorPrimaries)
								{
								case global::UnityEngine.ColorPrimaries.Rec709:
									b = global::UnityEngine.Rendering.ColorSpaceUtils.Rec2020ToRec709Mat;
									break;
								case global::UnityEngine.ColorPrimaries.P3:
									b = global::UnityEngine.Rendering.ColorSpaceUtils.Rec2020ToP3D65Mat;
									break;
								}
								global::Unity.Mathematics.float3x3 float3x5 = global::Unity.Mathematics.math.mul(a, b);
								global::UnityEngine.Matrix4x4 value3 = new global::UnityEngine.Matrix4x4(new global::Unity.Mathematics.float4(float3x5.c0, 0f), new global::Unity.Mathematics.float4(float3x5.c1, 0f), new global::Unity.Mathematics.float4(float3x5.c2, 0f), new global::UnityEngine.Vector4(0f, 0f, 0f, 0f));
								s_MirrorViewMaterialProperty.SetMatrix(k_ColorTransform, value3);
								s_MirrorViewMaterialProperty.SetFloat(k_MaxNits, main.active ? ((float)main.maxToneMapLuminance) : 160f);
								s_MirrorViewMaterialProperty.SetFloat(k_SourceMaxNits, blitParameter.srcHdrEncoded ? ((float)blitParameter.srcHdrMaxLuminance) : 160f);
							}
							bool flag = !blitParameter.srcTex.sRGB && (blitParameter.srcTex.graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm || blitParameter.srcTex.graphicsFormat == global::UnityEngine.Experimental.Rendering.GraphicsFormat.B8G8R8A8_UNorm);
							s_MirrorViewMaterialProperty.SetFloat(k_SRGBRead, flag ? 1f : 0f);
							s_MirrorViewMaterialProperty.SetFloat(k_SRGBWrite, (global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear) ? 0f : 1f);
							s_MirrorViewMaterialProperty.SetTexture(k_SourceTex, blitParameter.srcTex);
							s_MirrorViewMaterialProperty.SetVector(k_ScaleBias, value);
							s_MirrorViewMaterialProperty.SetVector(k_ScaleBiasRt, value2);
							s_MirrorViewMaterialProperty.SetFloat(k_SourceTexArraySlice, blitParameter.srcTexArraySlice);
							if (global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps.HasFlag(global::UnityEngine.Rendering.FoveatedRenderingCaps.NonUniformRaster) && blitParameter.foveatedRenderingInfo != global::System.IntPtr.Zero)
							{
								cmd.ConfigureFoveatedRendering(blitParameter.foveatedRenderingInfo);
								cmd.EnableShaderKeyword("_FOVEATED_RENDERING_NON_UNIFORM_RASTER");
							}
							if (blitParameter.srcTex.dimension != global::UnityEngine.Rendering.TextureDimension.Tex2DArray)
							{
								cmd.EnableShaderKeyword("DISABLE_TEXTURE2D_X_ARRAY");
							}
							cmd.DrawProcedural(global::UnityEngine.Matrix4x4.identity, mat, 0, global::UnityEngine.MeshTopology.Quads, 4, 1, s_MirrorViewMaterialProperty);
							if (blitParameter.srcTex.dimension != global::UnityEngine.Rendering.TextureDimension.Tex2DArray && global::UnityEngine.Rendering.TextureXR.useTexArray)
							{
								cmd.DisableShaderKeyword("DISABLE_TEXTURE2D_X_ARRAY");
							}
						}
					}
				}
			}
			if (global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps.HasFlag(global::UnityEngine.Rendering.FoveatedRenderingCaps.NonUniformRaster))
			{
				cmd.DisableShaderKeyword("_FOVEATED_RENDERING_NON_UNIFORM_RASTER");
				cmd.ConfigureFoveatedRendering(global::System.IntPtr.Zero);
			}
		}
	}
}
