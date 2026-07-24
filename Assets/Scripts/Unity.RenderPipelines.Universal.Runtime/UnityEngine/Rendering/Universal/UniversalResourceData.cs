namespace UnityEngine.Rendering.Universal
{
	public class UniversalResourceData : global::UnityEngine.Rendering.Universal.UniversalResourceDataBase
	{
		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _backBufferColor;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _backBufferDepth;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _cameraColor;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _cameraDepth;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _mainShadowsTexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _additionalShadowsTexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] _gBuffer = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[7];

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _cameraOpaqueTexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _cameraDepthTexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _cameraNormalsTexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _motionVectorColor;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _motionVectorDepth;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _internalColorLut;

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _debugScreenColor;

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _debugScreenDepth;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _afterPostProcessColor;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _overlayUITexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _renderingLayersTexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] _dBuffer = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[3];

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _dBufferDepth;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _ssaoTexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _irradianceTexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _stpDebugView;

		internal global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID activeColorID { get; set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle activeColorTexture
		{
			get
			{
				if (!CheckAndWarnAboutAccessibility())
				{
					return global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
				}
				return activeColorID switch
				{
					global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.Camera => cameraColor, 
					global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer => backBufferColor, 
					_ => throw new global::System.ArgumentOutOfRangeException(), 
				};
			}
		}

		internal global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID activeDepthID { get; set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle activeDepthTexture
		{
			get
			{
				if (!CheckAndWarnAboutAccessibility())
				{
					return global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
				}
				return activeDepthID switch
				{
					global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.Camera => cameraDepth, 
					global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer => backBufferDepth, 
					_ => throw new global::System.ArgumentOutOfRangeException(), 
				};
			}
		}

		public bool isActiveTargetBackBuffer
		{
			get
			{
				if (!base.isAccessible)
				{
					global::UnityEngine.Debug.LogError("Trying to access frameData outside of the current frame setup.");
					return false;
				}
				return activeColorID == global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle backBufferColor
		{
			get
			{
				return CheckAndGetTextureHandle(ref _backBufferColor);
			}
			internal set
			{
				CheckAndSetTextureHandle(ref _backBufferColor, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle backBufferDepth
		{
			get
			{
				return CheckAndGetTextureHandle(ref _backBufferDepth);
			}
			internal set
			{
				CheckAndSetTextureHandle(ref _backBufferDepth, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraColor
		{
			get
			{
				return CheckAndGetTextureHandle(ref _cameraColor);
			}
			set
			{
				CheckAndSetTextureHandle(ref _cameraColor, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepth
		{
			get
			{
				return CheckAndGetTextureHandle(ref _cameraDepth);
			}
			set
			{
				CheckAndSetTextureHandle(ref _cameraDepth, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle mainShadowsTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _mainShadowsTexture);
			}
			set
			{
				CheckAndSetTextureHandle(ref _mainShadowsTexture, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle additionalShadowsTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _additionalShadowsTexture);
			}
			set
			{
				CheckAndSetTextureHandle(ref _additionalShadowsTexture, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] gBuffer
		{
			get
			{
				return CheckAndGetTextureHandle(ref _gBuffer);
			}
			set
			{
				CheckAndSetTextureHandle(ref _gBuffer, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraOpaqueTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _cameraOpaqueTexture);
			}
			internal set
			{
				CheckAndSetTextureHandle(ref _cameraOpaqueTexture, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraDepthTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _cameraDepthTexture);
			}
			internal set
			{
				CheckAndSetTextureHandle(ref _cameraDepthTexture, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraNormalsTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _cameraNormalsTexture);
			}
			internal set
			{
				CheckAndSetTextureHandle(ref _cameraNormalsTexture, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle motionVectorColor
		{
			get
			{
				return CheckAndGetTextureHandle(ref _motionVectorColor);
			}
			set
			{
				CheckAndSetTextureHandle(ref _motionVectorColor, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle motionVectorDepth
		{
			get
			{
				return CheckAndGetTextureHandle(ref _motionVectorDepth);
			}
			set
			{
				CheckAndSetTextureHandle(ref _motionVectorDepth, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle internalColorLut
		{
			get
			{
				return CheckAndGetTextureHandle(ref _internalColorLut);
			}
			set
			{
				CheckAndSetTextureHandle(ref _internalColorLut, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle debugScreenColor
		{
			get
			{
				return CheckAndGetTextureHandle(ref _debugScreenColor);
			}
			set
			{
				CheckAndSetTextureHandle(ref _debugScreenColor, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle debugScreenDepth
		{
			get
			{
				return CheckAndGetTextureHandle(ref _debugScreenDepth);
			}
			set
			{
				CheckAndSetTextureHandle(ref _debugScreenDepth, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle afterPostProcessColor
		{
			get
			{
				return CheckAndGetTextureHandle(ref _afterPostProcessColor);
			}
			internal set
			{
				CheckAndSetTextureHandle(ref _afterPostProcessColor, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle overlayUITexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _overlayUITexture);
			}
			internal set
			{
				CheckAndSetTextureHandle(ref _overlayUITexture, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle renderingLayersTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _renderingLayersTexture);
			}
			internal set
			{
				CheckAndSetTextureHandle(ref _renderingLayersTexture, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] dBuffer
		{
			get
			{
				return CheckAndGetTextureHandle(ref _dBuffer);
			}
			set
			{
				CheckAndSetTextureHandle(ref _dBuffer, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle dBufferDepth
		{
			get
			{
				return CheckAndGetTextureHandle(ref _dBufferDepth);
			}
			set
			{
				CheckAndSetTextureHandle(ref _dBufferDepth, value);
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ssaoTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _ssaoTexture);
			}
			internal set
			{
				CheckAndSetTextureHandle(ref _ssaoTexture, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle irradianceTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _irradianceTexture);
			}
			set
			{
				CheckAndSetTextureHandle(ref _irradianceTexture, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle stpDebugView
		{
			get
			{
				return CheckAndGetTextureHandle(ref _stpDebugView);
			}
			set
			{
				CheckAndSetTextureHandle(ref _stpDebugView, value);
			}
		}

		public void SwitchActiveTexturesToBackbuffer()
		{
			activeColorID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
			activeDepthID = global::UnityEngine.Rendering.Universal.UniversalResourceDataBase.ActiveID.BackBuffer;
		}

		public override void Reset()
		{
			_backBufferColor = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_backBufferDepth = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_cameraColor = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_cameraDepth = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_mainShadowsTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_additionalShadowsTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_cameraOpaqueTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_cameraDepthTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_cameraNormalsTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_motionVectorColor = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_motionVectorDepth = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_internalColorLut = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_debugScreenColor = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_debugScreenDepth = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_afterPostProcessColor = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_overlayUITexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_renderingLayersTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_dBufferDepth = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_ssaoTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_irradianceTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_stpDebugView = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			for (int i = 0; i < _gBuffer.Length; i++)
			{
				_gBuffer[i] = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			for (int j = 0; j < _dBuffer.Length; j++)
			{
				_dBuffer[j] = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
		}
	}
}
