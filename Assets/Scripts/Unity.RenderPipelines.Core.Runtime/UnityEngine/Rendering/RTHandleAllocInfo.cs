namespace UnityEngine.Rendering
{
	public struct RTHandleAllocInfo
	{
		public int slices { get; set; }

		public global::UnityEngine.Experimental.Rendering.GraphicsFormat format { get; set; }

		public global::UnityEngine.FilterMode filterMode { get; set; }

		public global::UnityEngine.TextureWrapMode wrapModeU { get; set; }

		public global::UnityEngine.TextureWrapMode wrapModeV { get; set; }

		public global::UnityEngine.TextureWrapMode wrapModeW { get; set; }

		public global::UnityEngine.Rendering.TextureDimension dimension { get; set; }

		public bool enableRandomWrite { get; set; }

		public bool useMipMap { get; set; }

		public bool autoGenerateMips { get; set; }

		public bool isShadowMap { get; set; }

		public int anisoLevel { get; set; }

		public float mipMapBias { get; set; }

		public global::UnityEngine.Rendering.MSAASamples msaaSamples { get; set; }

		public bool bindTextureMS { get; set; }

		public bool useDynamicScale { get; set; }

		public bool useDynamicScaleExplicit { get; set; }

		public global::UnityEngine.RenderTextureMemoryless memoryless { get; set; }

		public global::UnityEngine.VRTextureUsage vrUsage { get; set; }

		public bool enableShadingRate { get; set; }

		public string name { get; set; }

		public RTHandleAllocInfo(string name = "")
		{
			slices = 1;
			format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB;
			filterMode = global::UnityEngine.FilterMode.Point;
			wrapModeU = global::UnityEngine.TextureWrapMode.Repeat;
			wrapModeV = global::UnityEngine.TextureWrapMode.Repeat;
			wrapModeW = global::UnityEngine.TextureWrapMode.Repeat;
			dimension = global::UnityEngine.Rendering.TextureDimension.Tex2D;
			enableRandomWrite = false;
			useMipMap = false;
			autoGenerateMips = true;
			isShadowMap = false;
			anisoLevel = 1;
			mipMapBias = 0f;
			msaaSamples = global::UnityEngine.Rendering.MSAASamples.None;
			bindTextureMS = false;
			useDynamicScale = false;
			useDynamicScaleExplicit = false;
			memoryless = global::UnityEngine.RenderTextureMemoryless.None;
			vrUsage = global::UnityEngine.VRTextureUsage.None;
			enableShadingRate = false;
			this.name = name;
		}
	}
}
