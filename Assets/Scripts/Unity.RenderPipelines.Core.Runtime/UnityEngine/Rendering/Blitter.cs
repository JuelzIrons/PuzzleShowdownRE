namespace UnityEngine.Rendering
{
	public static class Blitter
	{
		private static class BlitShaderIDs
		{
			public static readonly int _BlitTexture = global::UnityEngine.Shader.PropertyToID("_BlitTexture");

			public static readonly int _BlitCubeTexture = global::UnityEngine.Shader.PropertyToID("_BlitCubeTexture");

			public static readonly int _BlitScaleBias = global::UnityEngine.Shader.PropertyToID("_BlitScaleBias");

			public static readonly int _BlitScaleBiasRt = global::UnityEngine.Shader.PropertyToID("_BlitScaleBiasRt");

			public static readonly int _SourceResolution = global::UnityEngine.Shader.PropertyToID("_SourceResolution");

			public static readonly int _BlitMipLevel = global::UnityEngine.Shader.PropertyToID("_BlitMipLevel");

			public static readonly int _BlitTexArraySlice = global::UnityEngine.Shader.PropertyToID("_BlitTexArraySlice");

			public static readonly int _BlitTextureSize = global::UnityEngine.Shader.PropertyToID("_BlitTextureSize");

			public static readonly int _BlitPaddingSize = global::UnityEngine.Shader.PropertyToID("_BlitPaddingSize");

			public static readonly int _BlitDecodeInstructions = global::UnityEngine.Shader.PropertyToID("_BlitDecodeInstructions");

			public static readonly int _InputDepth = global::UnityEngine.Shader.PropertyToID("_InputDepthTexture");

			public static readonly int _InputDepthXR = global::UnityEngine.Shader.PropertyToID("_InputDepthTextureXR");

			public static readonly int _InputDepthXRMS = global::UnityEngine.Shader.PropertyToID("_InputDepthTextureXR_MS");
		}

		private enum BlitShaderPassNames
		{
			Nearest = 0,
			Bilinear = 1,
			NearestQuad = 2,
			BilinearQuad = 3,
			NearestQuadPadding = 4,
			BilinearQuadPadding = 5,
			NearestQuadPaddingRepeat = 6,
			BilinearQuadPaddingRepeat = 7,
			BilinearQuadPaddingOctahedral = 8,
			NearestQuadPaddingAlphaBlend = 9,
			BilinearQuadPaddingAlphaBlend = 10,
			NearestQuadPaddingAlphaBlendRepeat = 11,
			BilinearQuadPaddingAlphaBlendRepeat = 12,
			BilinearQuadPaddingAlphaBlendOctahedral = 13,
			CubeToOctahedral = 14,
			CubeToOctahedralLuminance = 15,
			CubeToOctahedralAlpha = 16,
			CubeToOctahedralRed = 17,
			BilinearQuadLuminance = 18,
			BilinearQuadAlpha = 19,
			BilinearQuadRed = 20,
			NearestCubeToOctahedralPadding = 21,
			BilinearCubeToOctahedralPadding = 22
		}

		private enum BlitColorAndDepthPassNames
		{
			ColorOnly = 0,
			ColorAndDepth = 1,
			DepthOnly = 2
		}

		private static global::UnityEngine.Material s_Copy;

		private static global::UnityEngine.Material s_Blit;

		private static global::UnityEngine.Material s_BlitTexArray;

		private static global::UnityEngine.Material s_BlitTexArraySingleSlice;

		private static global::UnityEngine.Material s_BlitColorAndDepth;

		private static global::UnityEngine.MaterialPropertyBlock s_PropertyBlock = new global::UnityEngine.MaterialPropertyBlock();

		private static global::UnityEngine.Mesh s_TriangleMesh;

		private static global::UnityEngine.Mesh s_QuadMesh;

		private static global::UnityEngine.Rendering.LocalKeyword s_DecodeHdrKeyword;

		private static global::UnityEngine.Rendering.LocalKeyword s_ResolveDepthMSAA2X;

		private static global::UnityEngine.Rendering.LocalKeyword s_ResolveDepthMSAA4X;

		private static global::UnityEngine.Rendering.LocalKeyword s_ResolveDepthMSAA8X;

		private static int[] s_BlitShaderPassIndicesMap;

		private static int[] s_BlitColorAndDepthShaderPassIndicesMap;

		public static void Initialize(global::UnityEngine.Shader blitPS, global::UnityEngine.Shader blitColorAndDepthPS)
		{
			if (s_Blit != null)
			{
				throw new global::System.Exception("Blitter is already initialized. Please only initialize the blitter once or you will leak engine resources. If you need to re-initialize the blitter with different shaders destroy & recreate it.");
			}
			s_Copy = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(global::UnityEngine.Rendering.GraphicsSettings.GetRenderPipelineSettings<global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtilsResources>().coreCopyPS);
			s_Blit = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(blitPS);
			s_BlitColorAndDepth = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(blitColorAndDepthPS);
			s_DecodeHdrKeyword = new global::UnityEngine.Rendering.LocalKeyword(blitPS, "BLIT_DECODE_HDR");
			s_ResolveDepthMSAA2X = new global::UnityEngine.Rendering.LocalKeyword(s_BlitColorAndDepth.shader, "_MSAA_2X");
			s_ResolveDepthMSAA4X = new global::UnityEngine.Rendering.LocalKeyword(s_BlitColorAndDepth.shader, "_MSAA_4X");
			s_ResolveDepthMSAA8X = new global::UnityEngine.Rendering.LocalKeyword(s_BlitColorAndDepth.shader, "_MSAA_8X");
			if (global::UnityEngine.Rendering.TextureXR.useTexArray)
			{
				s_Blit.EnableKeyword("DISABLE_TEXTURE2D_X_ARRAY");
				s_BlitColorAndDepth.EnableKeyword("DISABLE_TEXTURE2D_X_ARRAY");
				s_BlitTexArray = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(blitPS);
				s_BlitTexArraySingleSlice = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(blitPS);
				s_BlitTexArraySingleSlice.EnableKeyword("BLIT_SINGLE_SLICE");
			}
			float z = -1f;
			if (global::UnityEngine.SystemInfo.usesReversedZBuffer)
			{
				z = 1f;
			}
			if (global::UnityEngine.SystemInfo.graphicsShaderLevel < 30 && !s_TriangleMesh)
			{
				s_TriangleMesh = new global::UnityEngine.Mesh();
				s_TriangleMesh.vertices = GetFullScreenTriangleVertexPosition(z);
				s_TriangleMesh.uv = GetFullScreenTriangleTexCoord();
				s_TriangleMesh.triangles = new int[3] { 0, 1, 2 };
			}
			if (!s_QuadMesh)
			{
				s_QuadMesh = new global::UnityEngine.Mesh();
				s_QuadMesh.vertices = GetQuadVertexPosition(z);
				s_QuadMesh.uv = GetQuadTexCoord();
				s_QuadMesh.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
			}
			string[] names = global::System.Enum.GetNames(typeof(global::UnityEngine.Rendering.Blitter.BlitShaderPassNames));
			s_BlitShaderPassIndicesMap = new int[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				s_BlitShaderPassIndicesMap[i] = s_Blit.FindPass(names[i]);
			}
			names = global::System.Enum.GetNames(typeof(global::UnityEngine.Rendering.Blitter.BlitColorAndDepthPassNames));
			s_BlitColorAndDepthShaderPassIndicesMap = new int[names.Length];
			for (int j = 0; j < names.Length; j++)
			{
				s_BlitColorAndDepthShaderPassIndicesMap[j] = s_BlitColorAndDepth.FindPass(names[j]);
			}
			static global::UnityEngine.Vector2[] GetFullScreenTriangleTexCoord()
			{
				global::UnityEngine.Vector2[] array = new global::UnityEngine.Vector2[3];
				for (int k = 0; k < 3; k++)
				{
					if (global::UnityEngine.SystemInfo.graphicsUVStartsAtTop)
					{
						array[k] = new global::UnityEngine.Vector2((k << 1) & 2, 1f - (float)(k & 2));
					}
					else
					{
						array[k] = new global::UnityEngine.Vector2((k << 1) & 2, k & 2);
					}
				}
				return array;
			}
			static global::UnityEngine.Vector3[] GetFullScreenTriangleVertexPosition(float z2)
			{
				global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[3];
				for (int k = 0; k < 3; k++)
				{
					global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2((k << 1) & 2, k & 2);
					array[k] = new global::UnityEngine.Vector3(vector.x * 2f - 1f, vector.y * 2f - 1f, z2);
				}
				return array;
			}
			static global::UnityEngine.Vector2[] GetQuadTexCoord()
			{
				global::UnityEngine.Vector2[] array = new global::UnityEngine.Vector2[4];
				for (uint num = 0u; num < 4; num++)
				{
					uint num2 = num >> 1;
					uint num3 = num & 1;
					float x = num2;
					float num4 = (num2 + num3) & 1;
					if (global::UnityEngine.SystemInfo.graphicsUVStartsAtTop)
					{
						num4 = 1f - num4;
					}
					array[num] = new global::UnityEngine.Vector2(x, num4);
				}
				return array;
			}
			static global::UnityEngine.Vector3[] GetQuadVertexPosition(float z2)
			{
				global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[4];
				for (uint num = 0u; num < 4; num++)
				{
					uint num2 = num >> 1;
					uint num3 = num & 1;
					float x = num2;
					float y = (1 - (num2 + num3)) & 1;
					array[num] = new global::UnityEngine.Vector3(x, y, z2);
				}
				return array;
			}
		}

		public static void Cleanup()
		{
			global::UnityEngine.Rendering.CoreUtils.Destroy(s_Copy);
			s_Copy = null;
			global::UnityEngine.Rendering.CoreUtils.Destroy(s_Blit);
			s_Blit = null;
			global::UnityEngine.Rendering.CoreUtils.Destroy(s_BlitColorAndDepth);
			s_BlitColorAndDepth = null;
			global::UnityEngine.Rendering.CoreUtils.Destroy(s_BlitTexArray);
			s_BlitTexArray = null;
			global::UnityEngine.Rendering.CoreUtils.Destroy(s_BlitTexArraySingleSlice);
			s_BlitTexArraySingleSlice = null;
			global::UnityEngine.Rendering.CoreUtils.Destroy(s_TriangleMesh);
			s_TriangleMesh = null;
			global::UnityEngine.Rendering.CoreUtils.Destroy(s_QuadMesh);
			s_QuadMesh = null;
		}

		public static global::UnityEngine.Material GetBlitMaterial(global::UnityEngine.Rendering.TextureDimension dimension, bool singleSlice = false)
		{
			global::UnityEngine.Material material = ((dimension != global::UnityEngine.Rendering.TextureDimension.Tex2DArray) ? null : (singleSlice ? s_BlitTexArraySingleSlice : s_BlitTexArray));
			if (!(material == null))
			{
				return material;
			}
			return s_Blit;
		}

		internal static void DrawTriangle(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Material material, int shaderPass)
		{
			DrawTriangle(cmd.m_WrappedCommandBuffer, material, shaderPass);
		}

		internal static void DrawTriangle(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Material material, int shaderPass)
		{
			DrawTriangle(cmd, material, shaderPass, s_PropertyBlock);
		}

		internal static void DrawTriangle(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock propertyBlock)
		{
			if (global::UnityEngine.SystemInfo.graphicsShaderLevel < 30)
			{
				cmd.DrawMesh(s_TriangleMesh, global::UnityEngine.Matrix4x4.identity, material, 0, shaderPass, propertyBlock);
			}
			else
			{
				cmd.DrawProcedural(global::UnityEngine.Matrix4x4.identity, material, shaderPass, global::UnityEngine.MeshTopology.Triangles, 3, 1, propertyBlock);
			}
		}

		internal static void DrawQuadMesh(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock propertyBlock)
		{
			cmd.DrawMesh(s_QuadMesh, global::UnityEngine.Matrix4x4.identity, material, 0, shaderPass, propertyBlock);
		}

		internal static void DrawQuad(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock propertyBlock)
		{
			DrawQuad(cmd.m_WrappedCommandBuffer, material, shaderPass, propertyBlock);
		}

		internal static void DrawQuad(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Material material, int shaderPass)
		{
			DrawQuad(cmd, material, shaderPass, s_PropertyBlock);
		}

		internal static void DrawQuad(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock propertyBlock)
		{
			if (global::UnityEngine.SystemInfo.graphicsShaderLevel < 30)
			{
				cmd.DrawMesh(s_QuadMesh, global::UnityEngine.Matrix4x4.identity, material, 0, shaderPass, propertyBlock);
			}
			else
			{
				cmd.DrawProcedural(global::UnityEngine.Matrix4x4.identity, material, shaderPass, global::UnityEngine.MeshTopology.Quads, 4, 1, propertyBlock);
			}
		}

		internal static bool CanCopyMSAA()
		{
			if (global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.PlayStation4)
			{
				return false;
			}
			return s_Copy.passCount == 2;
		}

		internal static bool CanCopyMSAA(bool srcBindTextureMS)
		{
			bool flag = global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.Metal || global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.Vulkan || global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.Direct3D12;
			if (global::UnityEngine.SystemInfo.supportsMultisampleAutoResolve && !flag && !srcBindTextureMS)
			{
				return false;
			}
			return CanCopyMSAA();
		}

		internal static void CopyTexture(global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool isMSAA, bool force2DForXR = false)
		{
			if (force2DForXR)
			{
				cmd.EnableShaderKeyword("DISABLE_TEXTURE2D_X_ARRAY");
			}
			DrawTriangle(cmd, s_Copy, isMSAA ? 1 : 0);
			if (force2DForXR)
			{
				cmd.DisableShaderKeyword("DISABLE_TEXTURE2D_X_ARRAY");
			}
		}

		internal static void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, float sourceMipLevel, int sourceDepthSlice, bool bilinear)
		{
			BlitTexture(cmd, source, scaleBias, GetBlitMaterial(global::UnityEngine.Rendering.TextureDimension.Tex2D), s_BlitShaderPassIndicesMap[bilinear ? 1 : 0], sourceMipLevel, sourceDepthSlice);
		}

		internal static void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Material material, int pass, float sourceMipLevel, int sourceDepthSlice)
		{
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, sourceMipLevel);
			s_PropertyBlock.SetInt(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexArraySlice, sourceDepthSlice);
			BlitTexture(cmd, source, scaleBias, material, pass);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			BlitTexture(cmd.m_WrappedCommandBuffer, source, scaleBias, mipLevel, bilinear);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			global::UnityEngine.Rendering.TextureDimension dimension = ((source.rt != null) ? source.rt.dimension : global::UnityEngine.Rendering.TextureXR.dimension);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			BlitTexture(cmd, source, scaleBias, GetBlitMaterial(dimension), s_BlitShaderPassIndicesMap[bilinear ? 1 : 0]);
		}

		public static void BlitTexture2D(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			BlitTexture2D(cmd.m_WrappedCommandBuffer, source, scaleBias, mipLevel, bilinear);
		}

		public static void BlitTexture2D(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			BlitTexture(cmd, source, scaleBias, GetBlitMaterial(global::UnityEngine.Rendering.TextureDimension.Tex2D), s_BlitShaderPassIndicesMap[bilinear ? 1 : 0]);
		}

		public static void BlitColorAndDepth(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Texture sourceColor, global::UnityEngine.RenderTexture sourceDepth, global::UnityEngine.Vector4 scaleBias, float mipLevel, bool blitDepth)
		{
			BlitColorAndDepth(cmd.m_WrappedCommandBuffer, sourceColor, sourceDepth, scaleBias, mipLevel, blitDepth);
		}

		public static void BlitColorAndDepth(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture sourceColor, global::UnityEngine.RenderTexture sourceDepth, global::UnityEngine.Vector4 scaleBias, float mipLevel, bool blitDepth)
		{
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, sourceColor);
			if (blitDepth)
			{
				s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._InputDepth, sourceDepth, global::UnityEngine.Rendering.RenderTextureSubElement.Depth);
			}
			DrawTriangle(cmd, s_BlitColorAndDepth, s_BlitColorAndDepthShaderPassIndicesMap[blitDepth ? 1 : 0]);
		}

		public static void BlitDepth(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.RenderTexture sourceDepth, global::UnityEngine.Vector4 scaleBias, float mipLevel)
		{
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._SourceResolution, new global::UnityEngine.Vector2(sourceDepth.width, sourceDepth.height));
			cmd.SetKeyword(s_BlitColorAndDepth, in s_ResolveDepthMSAA2X, sourceDepth.antiAliasing == 2);
			cmd.SetKeyword(s_BlitColorAndDepth, in s_ResolveDepthMSAA4X, sourceDepth.antiAliasing == 4);
			cmd.SetKeyword(s_BlitColorAndDepth, in s_ResolveDepthMSAA8X, sourceDepth.antiAliasing == 8);
			if (sourceDepth.antiAliasing > 1)
			{
				s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._InputDepthXRMS, sourceDepth, global::UnityEngine.Rendering.RenderTextureSubElement.Depth);
			}
			else
			{
				s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._InputDepthXR, sourceDepth, global::UnityEngine.Rendering.RenderTextureSubElement.Depth);
			}
			DrawTriangle(cmd, s_BlitColorAndDepth, s_BlitColorAndDepthShaderPassIndicesMap[2]);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Material material, int pass)
		{
			BlitTexture(cmd.m_WrappedCommandBuffer, source, scaleBias, material, pass);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Material material, int pass)
		{
			BlitTexture(cmd.m_WrappedCommandBuffer, source, scaleBias, material, pass);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Material material, int pass)
		{
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			DrawTriangle(cmd, material, pass);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier source, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Material material, int pass)
		{
			BlitTexture(cmd.m_WrappedCommandBuffer, source, scaleBias, material, pass);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier source, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Material material, int pass)
		{
			s_PropertyBlock.Clear();
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			cmd.SetGlobalTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			DrawTriangle(cmd, material, pass);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier source, global::UnityEngine.Rendering.RenderTargetIdentifier destination, global::UnityEngine.Material material, int pass)
		{
			s_PropertyBlock.Clear();
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, global::UnityEngine.Vector2.one);
			cmd.SetGlobalTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			cmd.SetRenderTarget(destination);
			DrawTriangle(cmd, material, pass);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier source, global::UnityEngine.Rendering.RenderTargetIdentifier destination, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, global::UnityEngine.Material material, int pass)
		{
			s_PropertyBlock.Clear();
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, global::UnityEngine.Vector2.one);
			cmd.SetGlobalTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			cmd.SetRenderTarget(destination, loadAction, storeAction);
			DrawTriangle(cmd, material, pass);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Material material, int pass)
		{
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			DrawTriangle(cmd, material, pass);
		}

		public static void BlitTexture(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Material material, int pass)
		{
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			DrawTriangle(cmd, material, pass);
		}

		public static void BlitCameraTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, float mipLevel = 0f, bool bilinear = false)
		{
			global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destination);
			BlitTexture(cmd, source, vector, mipLevel, bilinear);
		}

		public static void BlitCameraTexture2D(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, float mipLevel = 0f, bool bilinear = false)
		{
			global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destination);
			BlitTexture2D(cmd, source, vector, mipLevel, bilinear);
		}

		public static void BlitCameraTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Material material, int pass)
		{
			global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destination);
			BlitTexture(cmd, source, vector, material, pass);
		}

		public static void BlitCameraTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Vector4 scaleBias, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, global::UnityEngine.Material material, int pass)
		{
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destination, loadAction, storeAction, global::UnityEngine.Rendering.ClearFlag.None, global::UnityEngine.Color.clear);
			BlitTexture(cmd, source, scaleBias, material, pass);
		}

		public static void BlitCameraTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, global::UnityEngine.Material material, int pass)
		{
			global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			BlitCameraTexture(cmd, source, destination, vector, loadAction, storeAction, material, pass);
		}

		public static void BlitCameraTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Vector4 scaleBias, float mipLevel = 0f, bool bilinear = false)
		{
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destination);
			BlitTexture(cmd, source, scaleBias, mipLevel, bilinear);
		}

		public static void BlitCameraTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Rect destViewport, float mipLevel = 0f, bool bilinear = false)
		{
			global::UnityEngine.Vector2 vector = (source.useScaling ? new global::UnityEngine.Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : global::UnityEngine.Vector2.one);
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, destination);
			cmd.SetViewport(destViewport);
			BlitTexture(cmd, source, vector, mipLevel, bilinear);
		}

		public static void BlitQuad(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture source, global::UnityEngine.Vector4 scaleBiasTex, global::UnityEngine.Vector4 scaleBiasRT, int mipLevelTex, bool bilinear)
		{
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevelTex);
			DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[bilinear ? 3 : 2]);
		}

		public static void BlitQuadWithPadding(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture source, global::UnityEngine.Vector2 textureSize, global::UnityEngine.Vector4 scaleBiasTex, global::UnityEngine.Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevelTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			s_PropertyBlock.SetInt(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			if (source.wrapMode == global::UnityEngine.TextureWrapMode.Repeat)
			{
				DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[bilinear ? 7 : 6]);
			}
			else
			{
				DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[bilinear ? 5 : 4]);
			}
		}

		public static void BlitQuadWithPaddingMultiply(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture source, global::UnityEngine.Vector2 textureSize, global::UnityEngine.Vector4 scaleBiasTex, global::UnityEngine.Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevelTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			s_PropertyBlock.SetInt(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			if (source.wrapMode == global::UnityEngine.TextureWrapMode.Repeat)
			{
				DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[bilinear ? 12 : 11]);
			}
			else
			{
				DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[bilinear ? 10 : 9]);
			}
		}

		public static void BlitOctahedralWithPadding(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture source, global::UnityEngine.Vector2 textureSize, global::UnityEngine.Vector4 scaleBiasTex, global::UnityEngine.Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevelTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			s_PropertyBlock.SetInt(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[8]);
		}

		public static void BlitOctahedralWithPaddingMultiply(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture source, global::UnityEngine.Vector2 textureSize, global::UnityEngine.Vector4 scaleBiasTex, global::UnityEngine.Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevelTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			s_PropertyBlock.SetInt(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[13]);
		}

		public static void BlitCubeToOctahedral2DQuad(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture source, global::UnityEngine.Vector4 scaleBiasRT, int mipLevelTex)
		{
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitCubeTexture, source);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevelTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, new global::UnityEngine.Vector4(1f, 1f, 0f, 0f));
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[14]);
		}

		public static void BlitCubeToOctahedral2DQuadWithPadding(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture source, global::UnityEngine.Vector2 textureSize, global::UnityEngine.Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels, global::UnityEngine.Vector4? decodeInstructions = null)
		{
			global::UnityEngine.Material blitMaterial = GetBlitMaterial(source.dimension);
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitCubeTexture, source);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevelTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, new global::UnityEngine.Vector4(1f, 1f, 0f, 0f));
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			s_PropertyBlock.SetInt(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			cmd.SetKeyword(blitMaterial, in s_DecodeHdrKeyword, decodeInstructions.HasValue);
			if (decodeInstructions.HasValue)
			{
				s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitDecodeInstructions, decodeInstructions.Value);
			}
			DrawQuad(cmd, blitMaterial, s_BlitShaderPassIndicesMap[bilinear ? 22 : 21]);
			cmd.SetKeyword(blitMaterial, in s_DecodeHdrKeyword, value: false);
		}

		public static void BlitCubeToOctahedral2DQuadSingleChannel(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture source, global::UnityEngine.Vector4 scaleBiasRT, int mipLevelTex)
		{
			int num = 15;
			if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetComponentCount(source.graphicsFormat) == 1)
			{
				if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsAlphaOnlyFormat(source.graphicsFormat))
				{
					num = 16;
				}
				if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) == global::UnityEngine.Rendering.FormatSwizzle.FormatSwizzleR)
				{
					num = 17;
				}
			}
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitCubeTexture, source);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevelTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, new global::UnityEngine.Vector4(1f, 1f, 0f, 0f));
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[num]);
		}

		public static void BlitQuadSingleChannel(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture source, global::UnityEngine.Vector4 scaleBiasTex, global::UnityEngine.Vector4 scaleBiasRT, int mipLevelTex)
		{
			int num = 18;
			if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetComponentCount(source.graphicsFormat) == 1)
			{
				if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsAlphaOnlyFormat(source.graphicsFormat))
				{
					num = 19;
				}
				if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) == global::UnityEngine.Rendering.FormatSwizzle.FormatSwizzleR)
				{
					num = 20;
				}
			}
			s_PropertyBlock.SetTexture(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitTexture, source);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			s_PropertyBlock.SetVector(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			s_PropertyBlock.SetFloat(global::UnityEngine.Rendering.Blitter.BlitShaderIDs._BlitMipLevel, mipLevelTex);
			DrawQuad(cmd, GetBlitMaterial(source.dimension), s_BlitShaderPassIndicesMap[num]);
		}
	}
}
