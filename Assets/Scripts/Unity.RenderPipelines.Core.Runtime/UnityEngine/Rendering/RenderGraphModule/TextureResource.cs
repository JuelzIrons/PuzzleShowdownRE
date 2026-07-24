namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("TextureResource ({desc.name})")]
	internal class TextureResource : global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.TextureDesc, global::UnityEngine.Rendering.RTHandle>
	{
		private static int m_TextureCreationIndex;

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection textureUVOrigin;

		public override string GetName()
		{
			if (imported && !shared)
			{
				if (graphicsResource == null)
				{
					return "null resource";
				}
				return graphicsResource.name;
			}
			return desc.name;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override int GetDescHashCode()
		{
			return desc.GetHashCode();
		}

		public override void CreateGraphicsResource()
		{
			string text = GetName();
			if (text == "")
			{
				text = $"RenderGraphTexture_{m_TextureCreationIndex++}";
			}
			global::UnityEngine.Rendering.RTHandleAllocInfo rTHandleAllocInfo = new global::UnityEngine.Rendering.RTHandleAllocInfo(text);
			rTHandleAllocInfo.slices = desc.slices;
			rTHandleAllocInfo.format = desc.format;
			rTHandleAllocInfo.filterMode = desc.filterMode;
			rTHandleAllocInfo.wrapModeU = desc.wrapMode;
			rTHandleAllocInfo.wrapModeV = desc.wrapMode;
			rTHandleAllocInfo.wrapModeW = desc.wrapMode;
			rTHandleAllocInfo.dimension = desc.dimension;
			rTHandleAllocInfo.enableRandomWrite = desc.enableRandomWrite;
			rTHandleAllocInfo.useMipMap = desc.useMipMap;
			rTHandleAllocInfo.autoGenerateMips = desc.autoGenerateMips;
			rTHandleAllocInfo.anisoLevel = desc.anisoLevel;
			rTHandleAllocInfo.mipMapBias = desc.mipMapBias;
			rTHandleAllocInfo.isShadowMap = desc.isShadowMap;
			rTHandleAllocInfo.msaaSamples = desc.msaaSamples;
			rTHandleAllocInfo.bindTextureMS = desc.bindTextureMS;
			rTHandleAllocInfo.useDynamicScale = desc.useDynamicScale;
			rTHandleAllocInfo.useDynamicScaleExplicit = desc.useDynamicScaleExplicit;
			rTHandleAllocInfo.memoryless = desc.memoryless;
			rTHandleAllocInfo.vrUsage = desc.vrUsage;
			rTHandleAllocInfo.enableShadingRate = desc.enableShadingRate;
			global::UnityEngine.Rendering.RTHandleAllocInfo info = rTHandleAllocInfo;
			switch (desc.sizeMode)
			{
			case global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit:
				graphicsResource = global::UnityEngine.Rendering.RTHandles.Alloc(desc.width, desc.height, info);
				break;
			case global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Scale:
				graphicsResource = global::UnityEngine.Rendering.RTHandles.Alloc(desc.scale, info);
				break;
			case global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Functor:
				graphicsResource = global::UnityEngine.Rendering.RTHandles.Alloc(desc.func, info);
				break;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override void UpdateGraphicsResource()
		{
			if (graphicsResource != null)
			{
				graphicsResource.m_Name = GetName();
			}
		}

		public override void ReleaseGraphicsResource()
		{
			if (graphicsResource != null)
			{
				graphicsResource.Release();
			}
			base.ReleaseGraphicsResource();
		}

		public override void LogCreation(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger logger)
		{
			logger.LogLine($"Created Texture: {desc.name} (Cleared: {desc.clearBuffer})");
		}

		public override void LogRelease(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger logger)
		{
			logger.LogLine("Released Texture: " + desc.name);
		}
	}
}
