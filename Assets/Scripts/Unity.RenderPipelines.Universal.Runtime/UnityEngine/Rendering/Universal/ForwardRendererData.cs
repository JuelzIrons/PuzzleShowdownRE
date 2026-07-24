namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::System.Obsolete("ForwardRendererData has been deprecated #from(2021.2) #breakingFrom(2021.2) (UnityUpgradable) -> UniversalRendererData", true)]
	[global::UnityEngine.Rendering.ReloadGroup]
	[global::UnityEngine.ExcludeFromPreset]
	public class ForwardRendererData : global::UnityEngine.Rendering.Universal.ScriptableRendererData
	{
		[global::System.Serializable]
		[global::UnityEngine.Rendering.ReloadGroup]
		public sealed class ShaderResources
		{
			[global::UnityEngine.Rendering.Reload("Shaders/Utils/Blit.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader blitPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Utils/CopyDepth.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader copyDepthPS;

			[global::System.Obsolete("Obsolete, this feature will be supported by new 'ScreenSpaceShadows' renderer feature. #from(2021.1) #breakingFrom(2023.1)", true)]
			public global::UnityEngine.Shader screenSpaceShadowPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Utils/Sampling.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader samplingPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Utils/StencilDeferred.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader stencilDeferredPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Utils/FallbackError.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader fallbackErrorPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Utils/FallbackLoading.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader fallbackLoadingPS;

			[global::System.Obsolete("Use fallbackErrorPS instead. #from(2022.2) #breakingFrom(2023.1)", true)]
			[global::UnityEngine.Rendering.Reload("Shaders/Utils/MaterialError.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader materialErrorPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Utils/CoreBlit.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			[global::UnityEngine.SerializeField]
			internal global::UnityEngine.Shader coreBlitPS;

			[global::UnityEngine.Rendering.Reload("Shaders/Utils/CoreBlitColorAndDepth.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			[global::UnityEngine.SerializeField]
			internal global::UnityEngine.Shader coreBlitColorAndDepthPS;

			[global::UnityEngine.Rendering.Reload("Shaders/CameraMotionVectors.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader cameraMotionVector;

			[global::UnityEngine.Rendering.Reload("Shaders/ObjectMotionVectors.shader", global::UnityEngine.Rendering.ReloadAttribute.Package.Root)]
			public global::UnityEngine.Shader objectMotionVector;
		}

		private const string k_ErrorMessage = "ForwardRendererData has been deprecated. Use UniversalRendererData instead";

		public global::UnityEngine.Rendering.Universal.ForwardRendererData.ShaderResources shaders;

		public global::UnityEngine.Rendering.Universal.PostProcessData postProcessData;

		public global::UnityEngine.Rendering.Universal.XRSystemData xrSystemData;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.LayerMask m_OpaqueLayerMask;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.LayerMask m_TransparentLayerMask;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.StencilStateData m_DefaultStencilState;

		[global::UnityEngine.SerializeField]
		private bool m_ShadowTransparentReceive;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.RenderingMode m_RenderingMode;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.DepthPrimingMode m_DepthPrimingMode;

		[global::UnityEngine.SerializeField]
		private bool m_AccurateGbufferNormals;

		[global::UnityEngine.SerializeField]
		private bool m_ClusteredRendering;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.TileSize m_TileSize;

		public global::UnityEngine.LayerMask opaqueLayerMask
		{
			get
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		public global::UnityEngine.LayerMask transparentLayerMask
		{
			get
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		public global::UnityEngine.Rendering.Universal.StencilStateData defaultStencilState
		{
			get
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		public bool shadowTransparentReceive
		{
			get
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		public global::UnityEngine.Rendering.Universal.RenderingMode renderingMode
		{
			get
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		public bool accurateGbufferNormals
		{
			get
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
			set
			{
				throw new global::System.NotSupportedException("ForwardRendererData has been deprecated. Use UniversalRendererData instead");
			}
		}

		protected override global::UnityEngine.Rendering.Universal.ScriptableRenderer Create()
		{
			global::UnityEngine.Debug.LogWarning("Forward Renderer Data has been deprecated, " + base.name + " will be upgraded to a UniversalRendererData.");
			return null;
		}
	}
}
