namespace UnityEngine.Rendering.Universal
{
	internal class Universal2DResourceData : global::UnityEngine.Rendering.Universal.UniversalResourceDataBase
	{
		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[][] _lightTextures = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[0][];

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] _cameraNormalsTexture = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[0];

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _normalsDepth;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[][] _shadowTextures = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[0][];

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _shadowDepth;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _upscaleTexture;

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle _cameraSortingLayerTexture;

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[][] lightTextures
		{
			get
			{
				return CheckAndGetTextureHandle(ref _lightTextures);
			}
			set
			{
				CheckAndSetTextureHandle(ref _lightTextures, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] normalsTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _cameraNormalsTexture);
			}
			set
			{
				CheckAndSetTextureHandle(ref _cameraNormalsTexture, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle normalsDepth
		{
			get
			{
				return CheckAndGetTextureHandle(ref _normalsDepth);
			}
			set
			{
				CheckAndSetTextureHandle(ref _normalsDepth, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[][] shadowTextures
		{
			get
			{
				return CheckAndGetTextureHandle(ref _shadowTextures);
			}
			set
			{
				CheckAndSetTextureHandle(ref _shadowTextures, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle shadowDepth
		{
			get
			{
				return CheckAndGetTextureHandle(ref _shadowDepth);
			}
			set
			{
				CheckAndSetTextureHandle(ref _shadowDepth, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle upscaleTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _upscaleTexture);
			}
			set
			{
				CheckAndSetTextureHandle(ref _upscaleTexture, value);
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle cameraSortingLayerTexture
		{
			get
			{
				return CheckAndGetTextureHandle(ref _cameraSortingLayerTexture);
			}
			set
			{
				CheckAndSetTextureHandle(ref _cameraSortingLayerTexture, value);
			}
		}

		private global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[][] CheckAndGetTextureHandle(ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[][] handle)
		{
			if (!CheckAndWarnAboutAccessibility())
			{
				return new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[1][] { new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[1] { global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle } };
			}
			return handle;
		}

		private void CheckAndSetTextureHandle(ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[][] handle, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[][] newHandle)
		{
			if (CheckAndWarnAboutAccessibility())
			{
				if (handle == null || handle.Length != newHandle.Length)
				{
					handle = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[newHandle.Length][];
				}
				for (int i = 0; i < newHandle.Length; i++)
				{
					handle[i] = newHandle[i];
				}
			}
		}

		public override void Reset()
		{
			_normalsDepth = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_shadowDepth = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_upscaleTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			_cameraSortingLayerTexture = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			for (int i = 0; i < _cameraNormalsTexture.Length; i++)
			{
				_cameraNormalsTexture[i] = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			for (int j = 0; j < _shadowTextures.Length; j++)
			{
				for (int k = 0; k < _shadowTextures[j].Length; k++)
				{
					_shadowTextures[j][k] = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
				}
			}
			for (int l = 0; l < _lightTextures.Length; l++)
			{
				for (int m = 0; m < _lightTextures[l].Length; m++)
				{
					_lightTextures[l][m] = global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
				}
			}
		}
	}
}
