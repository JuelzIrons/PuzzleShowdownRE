namespace UnityEngine.Rendering.RenderGraphModule
{
	public struct TextureDesc
	{
		public global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode sizeMode;

		public int width;

		public int height;

		public int slices;

		public global::UnityEngine.Vector2 scale;

		public global::UnityEngine.Rendering.ScaleFunc func;

		public global::UnityEngine.Experimental.Rendering.GraphicsFormat format;

		public global::UnityEngine.FilterMode filterMode;

		public global::UnityEngine.TextureWrapMode wrapMode;

		public global::UnityEngine.Rendering.TextureDimension dimension;

		public bool enableRandomWrite;

		public bool useMipMap;

		public bool autoGenerateMips;

		public bool isShadowMap;

		public int anisoLevel;

		public float mipMapBias;

		public global::UnityEngine.Rendering.MSAASamples msaaSamples;

		public bool bindTextureMS;

		public bool useDynamicScale;

		public bool useDynamicScaleExplicit;

		public global::UnityEngine.RenderTextureMemoryless memoryless;

		public global::UnityEngine.VRTextureUsage vrUsage;

		public bool enableShadingRate;

		public string name;

		public global::UnityEngine.Rendering.RenderGraphModule.FastMemoryDesc fastMemoryDesc;

		public bool fallBackToBlackTexture;

		public bool disableFallBackToImportedTexture;

		public bool clearBuffer;

		public global::UnityEngine.Color clearColor;

		public bool discardBuffer;

		public global::UnityEngine.Rendering.DepthBits depthBufferBits
		{
			get
			{
				return (global::UnityEngine.Rendering.DepthBits)global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthBits(format);
			}
			set
			{
				if (value == global::UnityEngine.Rendering.DepthBits.None)
				{
					if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthStencilFormat(format))
					{
						format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
					}
				}
				else
				{
					format = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetDepthStencilFormat((int)value);
				}
			}
		}

		public global::UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat
		{
			get
			{
				if (!global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthStencilFormat(format))
				{
					return format;
				}
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			}
			set
			{
				format = value;
			}
		}

		private void InitDefaultValues(bool dynamicResolution, bool xrReady)
		{
			useDynamicScale = dynamicResolution;
			vrUsage = global::UnityEngine.VRTextureUsage.None;
			if (xrReady)
			{
				slices = global::UnityEngine.Rendering.TextureXR.slices;
				dimension = global::UnityEngine.Rendering.TextureXR.dimension;
			}
			else
			{
				slices = 1;
				dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D;
			}
			discardBuffer = false;
		}

		public TextureDesc(int width, int height, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(global::UnityEngine.Rendering.RenderGraphModule.TextureDesc);
			sizeMode = global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit;
			this.width = width;
			this.height = height;
			msaaSamples = global::UnityEngine.Rendering.MSAASamples.None;
			InitDefaultValues(dynamicResolution, xrReady);
		}

		public TextureDesc(global::UnityEngine.Vector2 scale, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(global::UnityEngine.Rendering.RenderGraphModule.TextureDesc);
			sizeMode = global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Scale;
			this.scale = scale;
			msaaSamples = global::UnityEngine.Rendering.MSAASamples.None;
			dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D;
			InitDefaultValues(dynamicResolution, xrReady);
		}

		public TextureDesc(global::UnityEngine.Rendering.ScaleFunc func, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(global::UnityEngine.Rendering.RenderGraphModule.TextureDesc);
			sizeMode = global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Functor;
			this.func = func;
			msaaSamples = global::UnityEngine.Rendering.MSAASamples.None;
			dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D;
			InitDefaultValues(dynamicResolution, xrReady);
		}

		public TextureDesc(global::UnityEngine.Rendering.RenderGraphModule.TextureDesc input)
		{
			this = input;
		}

		public TextureDesc(global::UnityEngine.RenderTextureDescriptor input)
		{
			sizeMode = global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit;
			width = input.width;
			height = input.height;
			slices = input.volumeDepth;
			scale = global::UnityEngine.Vector2.one;
			func = null;
			format = ((input.depthStencilFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None) ? input.depthStencilFormat : input.graphicsFormat);
			filterMode = global::UnityEngine.FilterMode.Bilinear;
			wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
			dimension = input.dimension;
			enableRandomWrite = input.enableRandomWrite;
			useMipMap = input.useMipMap;
			autoGenerateMips = input.autoGenerateMips;
			isShadowMap = input.shadowSamplingMode != global::UnityEngine.Rendering.ShadowSamplingMode.None;
			anisoLevel = 1;
			mipMapBias = 0f;
			msaaSamples = (global::UnityEngine.Rendering.MSAASamples)input.msaaSamples;
			bindTextureMS = input.bindMS;
			useDynamicScale = input.useDynamicScale;
			useDynamicScaleExplicit = false;
			memoryless = input.memoryless;
			vrUsage = input.vrUsage;
			name = "UnNamedFromRenderTextureDescriptor";
			fastMemoryDesc = default(global::UnityEngine.Rendering.RenderGraphModule.FastMemoryDesc);
			fastMemoryDesc.inFastMemory = false;
			fallBackToBlackTexture = false;
			disableFallBackToImportedTexture = true;
			clearBuffer = true;
			clearColor = global::UnityEngine.Color.black;
			discardBuffer = false;
			enableShadingRate = input.enableShadingRate;
		}

		public TextureDesc(global::UnityEngine.RenderTexture input)
			: this(input.descriptor)
		{
			filterMode = input.filterMode;
			wrapMode = input.wrapMode;
			anisoLevel = input.anisoLevel;
			mipMapBias = input.mipMapBias;
			name = "UnNamedFromRenderTextureDescriptor";
		}

		public override int GetHashCode()
		{
			global::UnityEngine.Rendering.HashFNV1A32 hashFNV1A = global::UnityEngine.Rendering.HashFNV1A32.Create();
			switch (sizeMode)
			{
			case global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit:
				hashFNV1A.Append(in width);
				hashFNV1A.Append(in height);
				break;
			case global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Functor:
				if (func != null)
				{
					hashFNV1A.Append(global::UnityEngine.Rendering.DelegateHashCodeUtils.GetFuncHashCode(func));
				}
				break;
			case global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Scale:
				hashFNV1A.Append(in scale);
				break;
			}
			hashFNV1A.Append(in mipMapBias);
			hashFNV1A.Append(in slices);
			int input = (int)format;
			hashFNV1A.Append(in input);
			input = (int)filterMode;
			hashFNV1A.Append(in input);
			input = (int)wrapMode;
			hashFNV1A.Append(in input);
			input = (int)dimension;
			hashFNV1A.Append(in input);
			input = (int)memoryless;
			hashFNV1A.Append(in input);
			input = (int)vrUsage;
			hashFNV1A.Append(in input);
			hashFNV1A.Append(in anisoLevel);
			hashFNV1A.Append(in enableRandomWrite);
			hashFNV1A.Append(in useMipMap);
			hashFNV1A.Append(in autoGenerateMips);
			hashFNV1A.Append(in isShadowMap);
			hashFNV1A.Append(in bindTextureMS);
			hashFNV1A.Append(in useDynamicScale);
			input = (int)msaaSamples;
			hashFNV1A.Append(in input);
			hashFNV1A.Append(in fastMemoryDesc.inFastMemory);
			hashFNV1A.Append(in enableShadingRate);
			return hashFNV1A.value;
		}

		public global::UnityEngine.Vector2Int CalculateFinalDimensions()
		{
			return sizeMode switch
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit => new global::UnityEngine.Vector2Int(width, height), 
				global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Scale => global::UnityEngine.Rendering.RTHandles.CalculateDimensions(scale), 
				global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Functor => global::UnityEngine.Rendering.RTHandles.CalculateDimensions(func), 
				_ => throw new global::System.ArgumentOutOfRangeException(), 
			};
		}
	}
}
