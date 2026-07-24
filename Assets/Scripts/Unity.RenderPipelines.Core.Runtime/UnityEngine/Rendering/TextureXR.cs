namespace UnityEngine.Rendering
{
	public static class TextureXR
	{
		private static int m_MaxViews = 1;

		private static global::UnityEngine.Texture m_BlackUIntTexture2DArray;

		private static global::UnityEngine.Texture m_BlackUIntTexture;

		private static global::UnityEngine.Rendering.RTHandle m_BlackUIntTexture2DArrayRTH;

		private static global::UnityEngine.Rendering.RTHandle m_BlackUIntTextureRTH;

		private static global::UnityEngine.Texture2DArray m_ClearTexture2DArray;

		private static global::UnityEngine.Texture2D m_ClearTexture;

		private static global::UnityEngine.Rendering.RTHandle m_ClearTexture2DArrayRTH;

		private static global::UnityEngine.Rendering.RTHandle m_ClearTextureRTH;

		private static global::UnityEngine.Texture2DArray m_MagentaTexture2DArray;

		private static global::UnityEngine.Texture2D m_MagentaTexture;

		private static global::UnityEngine.Rendering.RTHandle m_MagentaTexture2DArrayRTH;

		private static global::UnityEngine.Rendering.RTHandle m_MagentaTextureRTH;

		private static global::UnityEngine.Texture2D m_BlackTexture;

		private static global::UnityEngine.Texture3D m_BlackTexture3D;

		private static global::UnityEngine.Texture2DArray m_BlackTexture2DArray;

		private static global::UnityEngine.Rendering.RTHandle m_BlackTexture2DArrayRTH;

		private static global::UnityEngine.Rendering.RTHandle m_BlackTextureRTH;

		private static global::UnityEngine.Rendering.RTHandle m_BlackTexture3DRTH;

		private static global::UnityEngine.Texture2DArray m_WhiteTexture2DArray;

		private static global::UnityEngine.Rendering.RTHandle m_WhiteTexture2DArrayRTH;

		private static global::UnityEngine.Rendering.RTHandle m_WhiteTextureRTH;

		public static int maxViews
		{
			set
			{
				m_MaxViews = value;
			}
		}

		public static int slices => m_MaxViews;

		public static bool useTexArray
		{
			get
			{
				switch (global::UnityEngine.SystemInfo.graphicsDeviceType)
				{
				case global::UnityEngine.Rendering.GraphicsDeviceType.Direct3D11:
				case global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3:
				case global::UnityEngine.Rendering.GraphicsDeviceType.PlayStation4:
				case global::UnityEngine.Rendering.GraphicsDeviceType.Metal:
				case global::UnityEngine.Rendering.GraphicsDeviceType.Direct3D12:
				case global::UnityEngine.Rendering.GraphicsDeviceType.Vulkan:
				case global::UnityEngine.Rendering.GraphicsDeviceType.PlayStation5:
				case global::UnityEngine.Rendering.GraphicsDeviceType.PlayStation5NGGC:
					return true;
				default:
					return false;
				}
			}
		}

		public static global::UnityEngine.Rendering.TextureDimension dimension
		{
			get
			{
				if (!useTexArray)
				{
					return global::UnityEngine.Rendering.TextureDimension.Tex2D;
				}
				return global::UnityEngine.Rendering.TextureDimension.Tex2DArray;
			}
		}

		public static global::UnityEngine.Rendering.RTHandle GetBlackUIntTexture()
		{
			if (!useTexArray)
			{
				return m_BlackUIntTextureRTH;
			}
			return m_BlackUIntTexture2DArrayRTH;
		}

		public static global::UnityEngine.Rendering.RTHandle GetClearTexture()
		{
			if (!useTexArray)
			{
				return m_ClearTextureRTH;
			}
			return m_ClearTexture2DArrayRTH;
		}

		public static global::UnityEngine.Rendering.RTHandle GetMagentaTexture()
		{
			if (!useTexArray)
			{
				return m_MagentaTextureRTH;
			}
			return m_MagentaTexture2DArrayRTH;
		}

		public static global::UnityEngine.Rendering.RTHandle GetBlackTexture()
		{
			if (!useTexArray)
			{
				return m_BlackTextureRTH;
			}
			return m_BlackTexture2DArrayRTH;
		}

		public static global::UnityEngine.Rendering.RTHandle GetBlackTextureArray()
		{
			return m_BlackTexture2DArrayRTH;
		}

		public static global::UnityEngine.Rendering.RTHandle GetBlackTexture3D()
		{
			return m_BlackTexture3DRTH;
		}

		public static global::UnityEngine.Rendering.RTHandle GetWhiteTexture()
		{
			if (!useTexArray)
			{
				return m_WhiteTextureRTH;
			}
			return m_WhiteTexture2DArrayRTH;
		}

		public static void Initialize(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.ComputeShader clearR32_UIntShader)
		{
			if (m_BlackUIntTexture2DArray == null)
			{
				global::UnityEngine.Rendering.RTHandles.Release(m_BlackUIntTexture2DArrayRTH);
				m_BlackUIntTexture2DArray = CreateBlackUIntTextureArray(cmd, clearR32_UIntShader);
				m_BlackUIntTexture2DArrayRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_BlackUIntTexture2DArray);
				global::UnityEngine.Rendering.RTHandles.Release(m_BlackUIntTextureRTH);
				m_BlackUIntTexture = CreateBlackUintTexture(cmd, clearR32_UIntShader);
				m_BlackUIntTextureRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_BlackUIntTexture);
				global::UnityEngine.Rendering.RTHandles.Release(m_ClearTextureRTH);
				m_ClearTexture = new global::UnityEngine.Texture2D(1, 1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None)
				{
					name = "Clear Texture"
				};
				m_ClearTexture.SetPixel(0, 0, global::UnityEngine.Color.clear);
				m_ClearTexture.Apply();
				m_ClearTextureRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_ClearTexture);
				global::UnityEngine.Rendering.RTHandles.Release(m_ClearTexture2DArrayRTH);
				m_ClearTexture2DArray = CreateTexture2DArrayFromTexture2D(m_ClearTexture, "Clear Texture2DArray");
				m_ClearTexture2DArrayRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_ClearTexture2DArray);
				global::UnityEngine.Rendering.RTHandles.Release(m_MagentaTextureRTH);
				m_MagentaTexture = new global::UnityEngine.Texture2D(1, 1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None)
				{
					name = "Magenta Texture"
				};
				m_MagentaTexture.SetPixel(0, 0, global::UnityEngine.Color.magenta);
				m_MagentaTexture.Apply();
				m_MagentaTextureRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_MagentaTexture);
				global::UnityEngine.Rendering.RTHandles.Release(m_MagentaTexture2DArrayRTH);
				m_MagentaTexture2DArray = CreateTexture2DArrayFromTexture2D(m_MagentaTexture, "Magenta Texture2DArray");
				m_MagentaTexture2DArrayRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_MagentaTexture2DArray);
				global::UnityEngine.Rendering.RTHandles.Release(m_BlackTextureRTH);
				m_BlackTexture = new global::UnityEngine.Texture2D(1, 1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None)
				{
					name = "Black Texture"
				};
				m_BlackTexture.SetPixel(0, 0, global::UnityEngine.Color.black);
				m_BlackTexture.Apply();
				m_BlackTextureRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_BlackTexture);
				global::UnityEngine.Rendering.RTHandles.Release(m_BlackTexture2DArrayRTH);
				m_BlackTexture2DArray = CreateTexture2DArrayFromTexture2D(m_BlackTexture, "Black Texture2DArray");
				m_BlackTexture2DArrayRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_BlackTexture2DArray);
				global::UnityEngine.Rendering.RTHandles.Release(m_BlackTexture3DRTH);
				m_BlackTexture3D = CreateBlackTexture3D("Black Texture3D");
				m_BlackTexture3DRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_BlackTexture3D);
				global::UnityEngine.Rendering.RTHandles.Release(m_WhiteTextureRTH);
				m_WhiteTextureRTH = global::UnityEngine.Rendering.RTHandles.Alloc(global::UnityEngine.Texture2D.whiteTexture);
				global::UnityEngine.Rendering.RTHandles.Release(m_WhiteTexture2DArrayRTH);
				m_WhiteTexture2DArray = CreateTexture2DArrayFromTexture2D(global::UnityEngine.Texture2D.whiteTexture, "White Texture2DArray");
				m_WhiteTexture2DArrayRTH = global::UnityEngine.Rendering.RTHandles.Alloc(m_WhiteTexture2DArray);
			}
		}

		private static global::UnityEngine.Texture2DArray CreateTexture2DArrayFromTexture2D(global::UnityEngine.Texture2D source, string name)
		{
			global::UnityEngine.Texture2DArray texture2DArray = new global::UnityEngine.Texture2DArray(source.width, source.height, slices, source.format, mipChain: false)
			{
				name = name
			};
			for (int i = 0; i < slices; i++)
			{
				global::UnityEngine.Graphics.CopyTexture(source, 0, 0, texture2DArray, i, 0);
			}
			return texture2DArray;
		}

		private static global::UnityEngine.Texture CreateBlackUIntTextureArray(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.ComputeShader clearR32_UIntShader)
		{
			global::UnityEngine.RenderTexture renderTexture = new global::UnityEngine.RenderTexture(1, 1, 0, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_UInt)
			{
				dimension = global::UnityEngine.Rendering.TextureDimension.Tex2DArray,
				volumeDepth = slices,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture Array"
			};
			renderTexture.Create();
			int kernelIndex = clearR32_UIntShader.FindKernel("ClearUIntTextureArray");
			cmd.SetComputeTextureParam(clearR32_UIntShader, kernelIndex, "_TargetArray", renderTexture);
			cmd.DispatchCompute(clearR32_UIntShader, kernelIndex, 1, 1, slices);
			return renderTexture;
		}

		private static global::UnityEngine.Texture CreateBlackUintTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.ComputeShader clearR32_UIntShader)
		{
			global::UnityEngine.RenderTexture renderTexture = new global::UnityEngine.RenderTexture(1, 1, 0, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_UInt)
			{
				dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D,
				volumeDepth = 1,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture"
			};
			renderTexture.Create();
			int kernelIndex = clearR32_UIntShader.FindKernel("ClearUIntTexture");
			cmd.SetComputeTextureParam(clearR32_UIntShader, kernelIndex, "_Target", renderTexture);
			cmd.DispatchCompute(clearR32_UIntShader, kernelIndex, 1, 1, 1);
			return renderTexture;
		}

		private static global::UnityEngine.Texture3D CreateBlackTexture3D(string name)
		{
			global::UnityEngine.Texture3D texture3D = new global::UnityEngine.Texture3D(1, 1, 1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
			texture3D.name = name;
			texture3D.SetPixel(0, 0, 0, global::UnityEngine.Color.black, 0);
			texture3D.Apply(updateMipmaps: false);
			return texture3D;
		}
	}
}
