namespace UnityEngine.Rendering
{
	public static class CoreUtils
	{
		public static class Sections
		{
			public const int section1 = 10000;

			public const int section2 = 20000;

			public const int section3 = 30000;

			public const int section4 = 40000;

			public const int section5 = 50000;

			public const int section6 = 60000;

			public const int section7 = 70000;

			public const int section8 = 80000;
		}

		public static class Priorities
		{
			public const int assetsCreateShaderMenuPriority = 83;

			public const int assetsCreateRenderingMenuPriority = 308;

			public const int editMenuPriority = 320;

			public const int gameObjectMenuPriority = 10;

			public const int srpLensFlareMenuPriority = 9;

			public const int scriptingPriority = 40;
		}

		public static readonly global::UnityEngine.Vector3[] lookAtList = new global::UnityEngine.Vector3[6]
		{
			new global::UnityEngine.Vector3(1f, 0f, 0f),
			new global::UnityEngine.Vector3(-1f, 0f, 0f),
			new global::UnityEngine.Vector3(0f, 1f, 0f),
			new global::UnityEngine.Vector3(0f, -1f, 0f),
			new global::UnityEngine.Vector3(0f, 0f, 1f),
			new global::UnityEngine.Vector3(0f, 0f, -1f)
		};

		public static readonly global::UnityEngine.Vector3[] upVectorList = new global::UnityEngine.Vector3[6]
		{
			new global::UnityEngine.Vector3(0f, 1f, 0f),
			new global::UnityEngine.Vector3(0f, 1f, 0f),
			new global::UnityEngine.Vector3(0f, 0f, -1f),
			new global::UnityEngine.Vector3(0f, 0f, 1f),
			new global::UnityEngine.Vector3(0f, 1f, 0f),
			new global::UnityEngine.Vector3(0f, 1f, 0f)
		};

		private const string obsoletePriorityMessage = "Use CoreUtils.Priorities instead. #from(2021.2)";

		[global::System.Obsolete("Use CoreUtils.Priorities instead. #from(2021.2)")]
		public const int editMenuPriority1 = 320;

		[global::System.Obsolete("Use CoreUtils.Priorities instead. #from(2021.2)")]
		public const int editMenuPriority2 = 331;

		[global::System.Obsolete("Use CoreUtils.Priorities instead. #from(2021.2)")]
		public const int editMenuPriority3 = 342;

		[global::System.Obsolete("Use CoreUtils.Priorities instead. #from(2021.2)")]
		public const int editMenuPriority4 = 353;

		[global::System.Obsolete("Use CoreUtils.Priorities instead. #from(2021.2)")]
		public const int assetCreateMenuPriority1 = 230;

		[global::System.Obsolete("Use CoreUtils.Priorities instead. #from(2021.2)")]
		public const int assetCreateMenuPriority2 = 241;

		[global::System.Obsolete("Use CoreUtils.Priorities instead. #from(2021.2)")]
		public const int assetCreateMenuPriority3 = 300;

		[global::System.Obsolete("Use CoreUtils.Priorities instead. #from(2021.2)")]
		public const int gameObjectMenuPriority = 10;

		private static global::UnityEngine.Cubemap m_BlackCubeTexture;

		private static global::UnityEngine.Cubemap m_MagentaCubeTexture;

		private static global::UnityEngine.CubemapArray m_MagentaCubeTextureArray;

		private static global::UnityEngine.Cubemap m_WhiteCubeTexture;

		private static global::UnityEngine.RenderTexture m_EmptyUAV;

		private static global::UnityEngine.GraphicsBuffer m_EmptyBuffer;

		private static global::UnityEngine.Texture3D m_BlackVolumeTexture;

		internal static global::UnityEngine.Texture3D m_WhiteVolumeTexture;

		private static global::System.Collections.Generic.IEnumerable<global::System.Type> s_AssemblyTypes;

		public static global::UnityEngine.Cubemap blackCubeTexture
		{
			get
			{
				if (m_BlackCubeTexture == null)
				{
					m_BlackCubeTexture = new global::UnityEngine.Cubemap(1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						m_BlackCubeTexture.SetPixel((global::UnityEngine.CubemapFace)i, 0, 0, global::UnityEngine.Color.black);
					}
					m_BlackCubeTexture.Apply();
				}
				return m_BlackCubeTexture;
			}
		}

		public static global::UnityEngine.Cubemap magentaCubeTexture
		{
			get
			{
				if (m_MagentaCubeTexture == null)
				{
					m_MagentaCubeTexture = new global::UnityEngine.Cubemap(1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						m_MagentaCubeTexture.SetPixel((global::UnityEngine.CubemapFace)i, 0, 0, global::UnityEngine.Color.magenta);
					}
					m_MagentaCubeTexture.Apply();
				}
				return m_MagentaCubeTexture;
			}
		}

		public static global::UnityEngine.CubemapArray magentaCubeTextureArray
		{
			get
			{
				if (m_MagentaCubeTextureArray == null)
				{
					m_MagentaCubeTextureArray = new global::UnityEngine.CubemapArray(1, 1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32G32B32A32_SFloat, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						global::UnityEngine.Color[] colors = new global::UnityEngine.Color[1] { global::UnityEngine.Color.magenta };
						m_MagentaCubeTextureArray.SetPixels(colors, (global::UnityEngine.CubemapFace)i, 0);
					}
					m_MagentaCubeTextureArray.Apply();
				}
				return m_MagentaCubeTextureArray;
			}
		}

		public static global::UnityEngine.Cubemap whiteCubeTexture
		{
			get
			{
				if (m_WhiteCubeTexture == null)
				{
					m_WhiteCubeTexture = new global::UnityEngine.Cubemap(1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						m_WhiteCubeTexture.SetPixel((global::UnityEngine.CubemapFace)i, 0, 0, global::UnityEngine.Color.white);
					}
					m_WhiteCubeTexture.Apply();
				}
				return m_WhiteCubeTexture;
			}
		}

		public static global::UnityEngine.RenderTexture emptyUAV
		{
			get
			{
				if (m_EmptyUAV == null)
				{
					m_EmptyUAV = new global::UnityEngine.RenderTexture(1, 1, 0);
					m_EmptyUAV.enableRandomWrite = true;
					m_EmptyUAV.Create();
				}
				return m_EmptyUAV;
			}
		}

		public static global::UnityEngine.GraphicsBuffer emptyBuffer
		{
			get
			{
				if (m_EmptyBuffer == null || !m_EmptyBuffer.IsValid())
				{
					m_EmptyBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, 1, 4);
				}
				return m_EmptyBuffer;
			}
		}

		public static global::UnityEngine.Texture3D blackVolumeTexture
		{
			get
			{
				if (m_BlackVolumeTexture == null)
				{
					global::UnityEngine.Color[] colors = new global::UnityEngine.Color[1] { global::UnityEngine.Color.black };
					m_BlackVolumeTexture = new global::UnityEngine.Texture3D(1, 1, 1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
					m_BlackVolumeTexture.SetPixels(colors, 0);
					m_BlackVolumeTexture.Apply();
				}
				return m_BlackVolumeTexture;
			}
		}

		internal static global::UnityEngine.Texture3D whiteVolumeTexture
		{
			get
			{
				if (m_WhiteVolumeTexture == null)
				{
					global::UnityEngine.Color[] colors = new global::UnityEngine.Color[1] { global::UnityEngine.Color.white };
					m_WhiteVolumeTexture = new global::UnityEngine.Texture3D(1, 1, 1, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
					m_WhiteVolumeTexture.SetPixels(colors, 0);
					m_WhiteVolumeTexture.Apply();
				}
				return m_WhiteVolumeTexture;
			}
		}

		public static void ClearRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor)
		{
			if (clearFlag != global::UnityEngine.Rendering.ClearFlag.None)
			{
				cmd.ClearRenderTarget((global::UnityEngine.Rendering.RTClearFlags)clearFlag, clearColor);
			}
		}

		private static int FixupDepthSlice(int depthSlice, global::UnityEngine.Rendering.RTHandle buffer)
		{
			if (depthSlice == -1)
			{
				global::UnityEngine.RenderTexture rt = buffer.rt;
				if ((object)rt != null && rt.dimension == global::UnityEngine.Rendering.TextureDimension.Cube)
				{
					depthSlice = 0;
				}
			}
			return depthSlice;
		}

		private static int FixupDepthSlice(int depthSlice, global::UnityEngine.CubemapFace cubemapFace)
		{
			if (depthSlice == -1 && cubemapFace != global::UnityEngine.CubemapFace.Unknown)
			{
				depthSlice = 0;
			}
			return depthSlice;
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier buffer, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = FixupDepthSlice(depthSlice, cubemapFace);
			cmd.SetRenderTarget(buffer, miplevel, cubemapFace, depthSlice);
			ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier buffer, global::UnityEngine.Rendering.ClearFlag clearFlag = global::UnityEngine.Rendering.ClearFlag.None, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd, buffer, clearFlag, global::UnityEngine.Color.clear, miplevel, cubemapFace, depthSlice);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd, colorBuffer, depthBuffer, global::UnityEngine.Rendering.ClearFlag.None, global::UnityEngine.Color.clear, miplevel, cubemapFace, depthSlice);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, global::UnityEngine.Rendering.ClearFlag clearFlag, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd, colorBuffer, depthBuffer, clearFlag, global::UnityEngine.Color.clear, miplevel, cubemapFace, depthSlice);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = FixupDepthSlice(depthSlice, cubemapFace);
			cmd.SetRenderTarget(colorBuffer, depthBuffer, miplevel, cubemapFace, depthSlice);
			ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer)
		{
			SetRenderTarget(cmd, colorBuffers, depthBuffer, global::UnityEngine.Rendering.ClearFlag.None, global::UnityEngine.Color.clear);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, global::UnityEngine.Rendering.ClearFlag clearFlag = global::UnityEngine.Rendering.ClearFlag.None)
		{
			SetRenderTarget(cmd, colorBuffers, depthBuffer, clearFlag, global::UnityEngine.Color.clear);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffers, depthBuffer, 0, global::UnityEngine.CubemapFace.Unknown, -1);
			ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier buffer, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor)
		{
			cmd.SetRenderTarget(buffer, loadAction, storeAction);
			ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier buffer, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = FixupDepthSlice(depthSlice, cubemapFace);
			buffer = new global::UnityEngine.Rendering.RenderTargetIdentifier(buffer, miplevel, cubemapFace, depthSlice);
			cmd.SetRenderTarget(buffer, loadAction, storeAction);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier buffer, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = FixupDepthSlice(depthSlice, cubemapFace);
			buffer = new global::UnityEngine.Rendering.RenderTargetIdentifier(buffer, miplevel, cubemapFace, depthSlice);
			SetRenderTarget(cmd, buffer, loadAction, storeAction, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier buffer, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, global::UnityEngine.Rendering.ClearFlag clearFlag)
		{
			SetRenderTarget(cmd, buffer, loadAction, storeAction, clearFlag, global::UnityEngine.Color.clear);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction);
			ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = FixupDepthSlice(depthSlice, cubemapFace);
			colorBuffer = new global::UnityEngine.Rendering.RenderTargetIdentifier(colorBuffer, miplevel, cubemapFace, depthSlice);
			depthBuffer = new global::UnityEngine.Rendering.RenderTargetIdentifier(depthBuffer, miplevel, cubemapFace, depthSlice);
			cmd.SetRenderTarget(colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = FixupDepthSlice(depthSlice, cubemapFace);
			colorBuffer = new global::UnityEngine.Rendering.RenderTargetIdentifier(colorBuffer, miplevel, cubemapFace, depthSlice);
			depthBuffer = new global::UnityEngine.Rendering.RenderTargetIdentifier(depthBuffer, miplevel, cubemapFace, depthSlice);
			SetRenderTarget(cmd, colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier buffer, global::UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, global::UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor)
		{
			cmd.SetRenderTarget(buffer, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
			ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, global::UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, global::UnityEngine.Rendering.ClearFlag clearFlag)
		{
			SetRenderTarget(cmd, colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction, clearFlag, global::UnityEngine.Color.clear);
		}

		private static void SetViewportAndClear(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle buffer, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor)
		{
			SetViewport(cmd, buffer);
			ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle buffer, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = FixupDepthSlice(depthSlice, buffer);
			cmd.SetRenderTarget(buffer.nameID, miplevel, cubemapFace, depthSlice);
			SetViewportAndClear(cmd, buffer, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle buffer, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd.m_WrappedCommandBuffer, buffer, clearFlag, clearColor, miplevel, cubemapFace, depthSlice);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle buffer, global::UnityEngine.Rendering.ClearFlag clearFlag = global::UnityEngine.Rendering.ClearFlag.None, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd, buffer, clearFlag, global::UnityEngine.Color.clear, miplevel, cubemapFace, depthSlice);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle colorBuffer, global::UnityEngine.Rendering.RTHandle depthBuffer, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd, colorBuffer, depthBuffer, global::UnityEngine.Rendering.ClearFlag.None, global::UnityEngine.Color.clear, miplevel, cubemapFace, depthSlice);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle colorBuffer, global::UnityEngine.Rendering.RTHandle depthBuffer, global::UnityEngine.Rendering.ClearFlag clearFlag, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd, colorBuffer, depthBuffer, clearFlag, global::UnityEngine.Color.clear, miplevel, cubemapFace, depthSlice);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle colorBuffer, global::UnityEngine.Rendering.RTHandle depthBuffer, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd, colorBuffer.nameID, depthBuffer.nameID, miplevel, cubemapFace, depthSlice);
			SetViewportAndClear(cmd, colorBuffer, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle buffer, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd, buffer.nameID, loadAction, storeAction, miplevel, cubemapFace, depthSlice);
			SetViewportAndClear(cmd, buffer, clearFlag, clearColor);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle colorBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, global::UnityEngine.Rendering.RTHandle depthBuffer, global::UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor, int miplevel = 0, global::UnityEngine.CubemapFace cubemapFace = global::UnityEngine.CubemapFace.Unknown, int depthSlice = -1)
		{
			SetRenderTarget(cmd, colorBuffer.nameID, colorLoadAction, colorStoreAction, depthBuffer.nameID, depthLoadAction, depthStoreAction, miplevel, cubemapFace, depthSlice);
			SetViewportAndClear(cmd, colorBuffer, clearFlag, clearColor);
		}

		public static void SetShadingRateFragmentSize(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.ShadingRateFragmentSize baseShadingRateFragmentSize)
		{
			cmd.SetShadingRateFragmentSize(baseShadingRateFragmentSize);
		}

		public static void SetShadingRateCombiner(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.ShadingRateCombinerStage stage, global::UnityEngine.Rendering.ShadingRateCombiner combiner)
		{
			cmd.SetShadingRateCombiner(stage, combiner);
		}

		public static void SetShadingRateImage(global::UnityEngine.Rendering.CommandBuffer cmd, in global::UnityEngine.Rendering.RenderTargetIdentifier shadingRateImage)
		{
			cmd.SetShadingRateImage(in shadingRateImage);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, global::UnityEngine.Rendering.RTHandle depthBuffer)
		{
			SetRenderTarget(cmd, colorBuffers, depthBuffer.nameID, global::UnityEngine.Rendering.ClearFlag.None, global::UnityEngine.Color.clear);
			SetViewport(cmd, depthBuffer);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, global::UnityEngine.Rendering.RTHandle depthBuffer, global::UnityEngine.Rendering.ClearFlag clearFlag = global::UnityEngine.Rendering.ClearFlag.None)
		{
			SetRenderTarget(cmd, colorBuffers, depthBuffer.nameID);
			SetViewportAndClear(cmd, depthBuffer, clearFlag, global::UnityEngine.Color.clear);
		}

		public static void SetRenderTarget(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, global::UnityEngine.Rendering.RTHandle depthBuffer, global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffers, depthBuffer.nameID, 0, global::UnityEngine.CubemapFace.Unknown, -1);
			SetViewportAndClear(cmd, depthBuffer, clearFlag, clearColor);
		}

		public static void SetViewport(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle target)
		{
			if (target.useScaling)
			{
				global::UnityEngine.Vector2Int scaledSize = target.GetScaledSize(target.rtHandleProperties.currentViewportSize);
				cmd.SetViewport(new global::UnityEngine.Rect(0f, 0f, scaledSize.x, scaledSize.y));
			}
		}

		public static string GetRenderTargetAutoName(int width, int height, int depth, global::UnityEngine.RenderTextureFormat format, string name, bool mips = false, bool enableMSAA = false, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None)
		{
			return GetRenderTargetAutoName(width, height, depth, format.ToString(), global::UnityEngine.Rendering.TextureDimension.None, name, mips, enableMSAA, msaaSamples, dynamicRes: false, dynamicResExplicit: false);
		}

		public static string GetRenderTargetAutoName(int width, int height, int depth, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, string name, bool mips = false, bool enableMSAA = false, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None)
		{
			return GetRenderTargetAutoName(width, height, depth, format.ToString(), global::UnityEngine.Rendering.TextureDimension.None, name, mips, enableMSAA, msaaSamples, dynamicRes: false, dynamicResExplicit: false);
		}

		public static string GetRenderTargetAutoName(int width, int height, int depth, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, global::UnityEngine.Rendering.TextureDimension dim, string name, bool mips = false, bool enableMSAA = false, global::UnityEngine.Rendering.MSAASamples msaaSamples = global::UnityEngine.Rendering.MSAASamples.None, bool dynamicRes = false, bool dynamicResExplicit = false)
		{
			return GetRenderTargetAutoName(width, height, depth, format.ToString(), dim, name, mips, enableMSAA, msaaSamples, dynamicRes, dynamicResExplicit);
		}

		private static string GetRenderTargetAutoName(int width, int height, int depth, string format, global::UnityEngine.Rendering.TextureDimension dim, string name, bool mips, bool enableMSAA, global::UnityEngine.Rendering.MSAASamples msaaSamples, bool dynamicRes, bool dynamicResExplicit)
		{
			string arg = $"{name}_{width}x{height}";
			if (depth > 1)
			{
				arg = $"{arg}x{depth}";
			}
			if (mips)
			{
				arg = string.Format("{0}_{1}", arg, "Mips");
			}
			arg = $"{arg}_{format}";
			if (dim != global::UnityEngine.Rendering.TextureDimension.None)
			{
				arg = $"{arg}_{dim}";
			}
			if (enableMSAA)
			{
				arg = $"{arg}_{msaaSamples.ToString()}";
			}
			if (dynamicRes)
			{
				arg = string.Format("{0}_{1}", arg, "Dynamic");
			}
			if (dynamicResExplicit)
			{
				arg = string.Format("{0}_{1}", arg, "DynamicExplicit");
			}
			return arg;
		}

		public static string GetTextureAutoName(int width, int height, global::UnityEngine.TextureFormat format, global::UnityEngine.Rendering.TextureDimension dim = global::UnityEngine.Rendering.TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			return GetTextureAutoName(width, height, format.ToString(), dim, name, mips, depth);
		}

		public static string GetTextureAutoName(int width, int height, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, global::UnityEngine.Rendering.TextureDimension dim = global::UnityEngine.Rendering.TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			return GetTextureAutoName(width, height, format.ToString(), dim, name, mips, depth);
		}

		private static string GetTextureAutoName(int width, int height, string format, global::UnityEngine.Rendering.TextureDimension dim = global::UnityEngine.Rendering.TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			return string.Format(arg2: (depth != 0) ? string.Format("{0}x{1}x{2}{3}_{4}", width, height, depth, mips ? "_Mips" : "", format) : string.Format("{0}x{1}{2}_{3}", width, height, mips ? "_Mips" : "", format), format: "{0}_{1}_{2}", arg0: (name != null && name.Length == 0) ? "Texture" : name, arg1: (dim == global::UnityEngine.Rendering.TextureDimension.None) ? "" : dim.ToString());
		}

		public static void ClearCubemap(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.RenderTexture renderTexture, global::UnityEngine.Color clearColor, bool clearMips = false)
		{
			int num = 1;
			if (renderTexture.useMipMap && clearMips)
			{
				num = (int)global::UnityEngine.Mathf.Log(renderTexture.width, 2f) + 1;
			}
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < num; j++)
				{
					SetRenderTarget(cmd, new global::UnityEngine.Rendering.RenderTargetIdentifier(renderTexture), global::UnityEngine.Rendering.ClearFlag.Color, clearColor, j, (global::UnityEngine.CubemapFace)i);
				}
			}
		}

		public static void DrawFullScreen(global::UnityEngine.Rendering.CommandBuffer commandBuffer, global::UnityEngine.Material material, global::UnityEngine.MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.DrawProcedural(global::UnityEngine.Matrix4x4.identity, material, shaderPassId, global::UnityEngine.MeshTopology.Triangles, 3, 1, properties);
		}

		public static void DrawFullScreen(global::UnityEngine.Rendering.RasterCommandBuffer commandBuffer, global::UnityEngine.Material material, global::UnityEngine.MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			DrawFullScreen(commandBuffer.m_WrappedCommandBuffer, material, properties, shaderPassId);
		}

		public static void DrawFullScreen(global::UnityEngine.Rendering.CommandBuffer commandBuffer, global::UnityEngine.Material material, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::UnityEngine.MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffer, 0, global::UnityEngine.CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(global::UnityEngine.Matrix4x4.identity, material, shaderPassId, global::UnityEngine.MeshTopology.Triangles, 3, 1, properties);
		}

		public static void DrawFullScreen(global::UnityEngine.Rendering.CommandBuffer commandBuffer, global::UnityEngine.Material material, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::UnityEngine.Rendering.RenderTargetIdentifier depthStencilBuffer, global::UnityEngine.MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffer, depthStencilBuffer, 0, global::UnityEngine.CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(global::UnityEngine.Matrix4x4.identity, material, shaderPassId, global::UnityEngine.MeshTopology.Triangles, 3, 1, properties);
		}

		public static void DrawFullScreen(global::UnityEngine.Rendering.CommandBuffer commandBuffer, global::UnityEngine.Material material, global::UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, global::UnityEngine.Rendering.RenderTargetIdentifier depthStencilBuffer, global::UnityEngine.MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffers, depthStencilBuffer, 0, global::UnityEngine.CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(global::UnityEngine.Matrix4x4.identity, material, shaderPassId, global::UnityEngine.MeshTopology.Triangles, 3, 1, properties);
		}

		public static void DrawFullScreen(global::UnityEngine.Rendering.CommandBuffer commandBuffer, global::UnityEngine.Material material, global::UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, global::UnityEngine.MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			DrawFullScreen(commandBuffer, material, colorBuffers, colorBuffers[0], properties, shaderPassId);
		}

		public static global::UnityEngine.Color ConvertSRGBToActiveColorSpace(global::UnityEngine.Color color)
		{
			if (global::UnityEngine.QualitySettings.activeColorSpace != global::UnityEngine.ColorSpace.Linear)
			{
				return color;
			}
			return color.linear;
		}

		public static global::UnityEngine.Color ConvertLinearToActiveColorSpace(global::UnityEngine.Color color)
		{
			if (global::UnityEngine.QualitySettings.activeColorSpace != global::UnityEngine.ColorSpace.Linear)
			{
				return color.gamma;
			}
			return color;
		}

		public static global::UnityEngine.Material CreateEngineMaterial(string shaderPath)
		{
			if (string.IsNullOrEmpty(shaderPath))
			{
				throw new global::System.ArgumentException("shaderPath");
			}
			global::UnityEngine.Shader shader = global::UnityEngine.Shader.Find(shaderPath);
			if (shader == null)
			{
				global::UnityEngine.Debug.LogError("Cannot create required material because shader " + shaderPath + " could not be found");
				return null;
			}
			return CreateEngineMaterial(shader);
		}

		public static global::UnityEngine.Material CreateEngineMaterial(global::UnityEngine.Shader shader)
		{
			if (shader == null)
			{
				global::UnityEngine.Debug.LogError("Cannot create required material because shader is null");
				return null;
			}
			return new global::UnityEngine.Material(shader)
			{
				hideFlags = global::UnityEngine.HideFlags.HideAndDontSave
			};
		}

		public static bool HasFlag<T>(T mask, T flag) where T : global::System.IConvertible
		{
			return (mask.ToUInt32(null) & flag.ToUInt32(null)) != 0;
		}

		public static void Swap<T>(ref T a, ref T b)
		{
			T val = a;
			a = b;
			b = val;
		}

		public static void SetKeyword(global::UnityEngine.Rendering.CommandBuffer cmd, string keyword, bool state)
		{
			if (state)
			{
				cmd.EnableShaderKeyword(keyword);
			}
			else
			{
				cmd.DisableShaderKeyword(keyword);
			}
		}

		public static void SetKeyword(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.ComputeShader cs, string keyword, bool state)
		{
			global::UnityEngine.Rendering.LocalKeyword keyword2 = new global::UnityEngine.Rendering.LocalKeyword(cs, keyword);
			if (state)
			{
				cmd.EnableKeyword(cs, in keyword2);
			}
			else
			{
				cmd.DisableKeyword(cs, in keyword2);
			}
		}

		public static void SetKeyword(global::UnityEngine.Rendering.BaseCommandBuffer cmd, string keyword, bool state)
		{
			if (state)
			{
				cmd.m_WrappedCommandBuffer.EnableShaderKeyword(keyword);
			}
			else
			{
				cmd.m_WrappedCommandBuffer.DisableShaderKeyword(keyword);
			}
		}

		public static void SetKeyword(global::UnityEngine.Material material, string keyword, bool state)
		{
			if (state)
			{
				material.EnableKeyword(keyword);
			}
			else
			{
				material.DisableKeyword(keyword);
			}
		}

		public static void SetKeyword(global::UnityEngine.Material material, global::UnityEngine.Rendering.LocalKeyword keyword, bool state)
		{
			if (state)
			{
				material.EnableKeyword(in keyword);
			}
			else
			{
				material.DisableKeyword(in keyword);
			}
		}

		public static void SetKeyword(global::UnityEngine.ComputeShader cs, string keyword, bool state)
		{
			if (state)
			{
				cs.EnableKeyword(keyword);
			}
			else
			{
				cs.DisableKeyword(keyword);
			}
		}

		public static void Destroy(global::UnityEngine.Object obj)
		{
			if (obj != null)
			{
				global::UnityEngine.Object.Destroy(obj);
			}
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> GetAllAssemblyTypes()
		{
			if (s_AssemblyTypes != null)
			{
				return s_AssemblyTypes;
			}
			global::System.Collections.Generic.List<global::System.Type> list = new global::System.Collections.Generic.List<global::System.Type>();
			global::System.Reflection.Assembly[] assemblies = global::System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (global::System.Reflection.Assembly assembly in assemblies)
			{
				try
				{
					list.AddRange(assembly.GetTypes());
				}
				catch (global::System.Exception)
				{
				}
			}
			s_AssemblyTypes = list;
			return s_AssemblyTypes;
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> GetAllTypesDerivedFrom<T>()
		{
			global::System.Collections.Generic.List<global::System.Type> list = new global::System.Collections.Generic.List<global::System.Type>();
			global::System.Type typeFromHandle = typeof(T);
			foreach (global::System.Type allAssemblyType in GetAllAssemblyTypes())
			{
				if (allAssemblyType.IsSubclassOf(typeFromHandle))
				{
					list.Add(allAssemblyType);
				}
			}
			return list;
		}

		public static void SafeRelease(global::UnityEngine.GraphicsBuffer buffer)
		{
			buffer?.Release();
		}

		public static void SafeRelease(global::UnityEngine.ComputeBuffer buffer)
		{
			buffer?.Release();
		}

		public static global::UnityEngine.Mesh CreateCubeMesh(global::UnityEngine.Vector3 min, global::UnityEngine.Vector3 max)
		{
			return new global::UnityEngine.Mesh
			{
				vertices = new global::UnityEngine.Vector3[8]
				{
					new global::UnityEngine.Vector3(min.x, min.y, min.z),
					new global::UnityEngine.Vector3(max.x, min.y, min.z),
					new global::UnityEngine.Vector3(max.x, max.y, min.z),
					new global::UnityEngine.Vector3(min.x, max.y, min.z),
					new global::UnityEngine.Vector3(min.x, min.y, max.z),
					new global::UnityEngine.Vector3(max.x, min.y, max.z),
					new global::UnityEngine.Vector3(max.x, max.y, max.z),
					new global::UnityEngine.Vector3(min.x, max.y, max.z)
				},
				triangles = new int[36]
				{
					0, 2, 1, 0, 3, 2, 1, 6, 5, 1,
					2, 6, 5, 7, 4, 5, 6, 7, 4, 3,
					0, 4, 7, 3, 3, 6, 2, 3, 7, 6,
					4, 1, 5, 4, 0, 1
				}
			};
		}

		public static bool ArePostProcessesEnabled(global::UnityEngine.Camera camera)
		{
			return true;
		}

		public static bool AreAnimatedMaterialsEnabled(global::UnityEngine.Camera camera)
		{
			return true;
		}

		public static bool IsSceneLightingDisabled(global::UnityEngine.Camera camera)
		{
			return false;
		}

		public static bool IsLightOverlapDebugEnabled(global::UnityEngine.Camera camera)
		{
			return false;
		}

		public static bool IsSceneViewFogEnabled(global::UnityEngine.Camera camera)
		{
			return true;
		}

		public static bool IsSceneFilteringEnabled()
		{
			return false;
		}

		public static bool IsSceneViewPrefabStageContextHidden()
		{
			return false;
		}

		[global::System.Obsolete("Use DrawRendererList(CommandBuffer cmd, UnityEngine.Rendering.RendererList rendererList) instead. #from(6000.3) (UnityUpgradable) -> !0")]
		public static void DrawRendererList(global::UnityEngine.Rendering.ScriptableRenderContext renderContext, global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RendererList rendererList)
		{
			cmd.DrawRendererList(rendererList);
		}

		public static void DrawRendererList(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RendererList rendererList)
		{
			cmd.DrawRendererList(rendererList);
		}

		public static void DrawRendererList(global::UnityEngine.Rendering.IRasterCommandBuffer cmd, global::UnityEngine.Rendering.RendererList rendererList)
		{
			cmd.DrawRendererList(rendererList);
		}

		public static int GetTextureHash(global::UnityEngine.Texture texture)
		{
			int hashCode = texture.GetHashCode();
			hashCode = 23 * hashCode + texture.GetInstanceID().GetHashCode();
			hashCode = 23 * hashCode + texture.graphicsFormat.GetHashCode();
			hashCode = 23 * hashCode + texture.wrapMode.GetHashCode();
			hashCode = 23 * hashCode + texture.width.GetHashCode();
			hashCode = 23 * hashCode + texture.height.GetHashCode();
			hashCode = 23 * hashCode + texture.filterMode.GetHashCode();
			hashCode = 23 * hashCode + texture.anisoLevel.GetHashCode();
			hashCode = 23 * hashCode + texture.mipmapCount.GetHashCode();
			return 23 * hashCode + texture.updateCount.GetHashCode();
		}

		public static int PreviousPowerOfTwo(int size)
		{
			if (size <= 0)
			{
				return 0;
			}
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			return size - (size >> 1);
		}

		public static int GetMipCount(int size)
		{
			return global::UnityEngine.Mathf.FloorToInt(global::UnityEngine.Mathf.Log(size, 2f)) + 1;
		}

		public static int GetMipCount(float size)
		{
			return global::UnityEngine.Mathf.FloorToInt(global::UnityEngine.Mathf.Log(size, 2f)) + 1;
		}

		public static int DivRoundUp(int value, int divisor)
		{
			return (value + (divisor - 1)) / divisor;
		}

		public static T GetLastEnumValue<T>() where T : global::System.Enum
		{
			global::System.Array values = global::System.Enum.GetValues(typeof(T));
			return (T)values.GetValue(values.Length - 1);
		}

		internal static string GetCorePath()
		{
			return "Packages/com.unity.render-pipelines.core/";
		}

		public static global::UnityEngine.Vector3[] CalculateViewSpaceCorners(global::UnityEngine.Matrix4x4 proj, float z)
		{
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[4];
			global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.Inverse(proj);
			array[0] = matrix4x.MultiplyPoint(new global::UnityEngine.Vector3(-1f, -1f, 0.95f));
			array[1] = matrix4x.MultiplyPoint(new global::UnityEngine.Vector3(1f, -1f, 0.95f));
			array[2] = matrix4x.MultiplyPoint(new global::UnityEngine.Vector3(1f, 1f, 0.95f));
			array[3] = matrix4x.MultiplyPoint(new global::UnityEngine.Vector3(-1f, 1f, 0.95f));
			for (int i = 0; i < 4; i++)
			{
				array[i] *= z / (0f - array[i].z);
			}
			return array;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::UnityEngine.Experimental.Rendering.GraphicsFormat GetDefaultDepthStencilFormat()
		{
			return global::UnityEngine.Experimental.Rendering.GraphicsFormat.D32_SFloat_S8_UInt;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::UnityEngine.Experimental.Rendering.GraphicsFormat GetDefaultDepthOnlyFormat()
		{
			return global::UnityEngine.Experimental.Rendering.GraphicsFormat.D32_SFloat;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::UnityEngine.Rendering.DepthBits GetDefaultDepthBufferBits()
		{
			return global::UnityEngine.Rendering.DepthBits.Depth32;
		}

		public static bool IsScreenFullyCoveredByCameras(global::System.Collections.Generic.List<global::UnityEngine.Camera> cameras)
		{
			if (cameras == null || cameras.Count == 0)
			{
				return false;
			}
			bool flag = false;
			global::System.Collections.Generic.List<global::UnityEngine.Rect> value;
			using (global::UnityEngine.Rendering.ListPool<global::UnityEngine.Rect>.Get(out value))
			{
				foreach (global::UnityEngine.Camera camera in cameras)
				{
					if (!(camera.targetTexture != null) && camera.cameraType == global::UnityEngine.CameraType.Game)
					{
						if (global::UnityEngine.Mathf.Approximately(camera.rect.xMin, 0f) && global::UnityEngine.Mathf.Approximately(camera.rect.yMin, 0f) && camera.rect.width >= (float)global::UnityEngine.Screen.width && camera.rect.height >= (float)global::UnityEngine.Screen.height)
						{
							return true;
						}
						value.Add(camera.rect);
					}
				}
				return global::UnityEngine.Mathf.Approximately(global::UnityEngine.Rendering.SweepLineRectUtils.CalculateRectUnionArea(value), 1f);
			}
		}
	}
}
