namespace UnityEngine.Rendering.Universal
{
	public static class PostProcessUtils
	{
		private static class ShaderConstants
		{
			public static readonly int _Grain_Texture = global::UnityEngine.Shader.PropertyToID("_Grain_Texture");

			public static readonly int _Grain_Params = global::UnityEngine.Shader.PropertyToID("_Grain_Params");

			public static readonly int _Grain_TilingParams = global::UnityEngine.Shader.PropertyToID("_Grain_TilingParams");

			public static readonly int _BlueNoise_Texture = global::UnityEngine.Shader.PropertyToID("_BlueNoise_Texture");

			public static readonly int _Dithering_Params = global::UnityEngine.Shader.PropertyToID("_Dithering_Params");

			public static readonly int _SourceSize = global::UnityEngine.Shader.PropertyToID("_SourceSize");
		}

		[global::System.Obsolete("This method is obsolete. Use ConfigureDithering override that takes camera pixel width and height instead. #from(2021.1)")]
		public static int ConfigureDithering(global::UnityEngine.Rendering.Universal.PostProcessData data, int index, global::UnityEngine.Camera camera, global::UnityEngine.Material material)
		{
			return ConfigureDithering(data, index, camera.pixelWidth, camera.pixelHeight, material);
		}

		public static int ConfigureDithering(global::UnityEngine.Rendering.Universal.PostProcessData data, int index, int cameraPixelWidth, int cameraPixelHeight, global::UnityEngine.Material material)
		{
			global::UnityEngine.Texture2D[] blueNoise16LTex = data.textures.blueNoise16LTex;
			if (blueNoise16LTex == null || blueNoise16LTex.Length == 0)
			{
				return 0;
			}
			if (++index >= blueNoise16LTex.Length)
			{
				index = 0;
			}
			global::UnityEngine.Random.State state = global::UnityEngine.Random.state;
			global::UnityEngine.Random.InitState(global::UnityEngine.Time.frameCount);
			float value = global::UnityEngine.Random.value;
			float value2 = global::UnityEngine.Random.value;
			global::UnityEngine.Random.state = state;
			global::UnityEngine.Texture2D texture2D = blueNoise16LTex[index];
			material.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessUtils.ShaderConstants._BlueNoise_Texture, texture2D);
			material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessUtils.ShaderConstants._Dithering_Params, new global::UnityEngine.Vector4((float)cameraPixelWidth / (float)texture2D.width, (float)cameraPixelHeight / (float)texture2D.height, value, value2));
			return index;
		}

		[global::System.Obsolete("This method is obsolete. Use ConfigureFilmGrain override that takes camera pixel width and height instead. #from(2021.1)")]
		public static void ConfigureFilmGrain(global::UnityEngine.Rendering.Universal.PostProcessData data, global::UnityEngine.Rendering.Universal.FilmGrain settings, global::UnityEngine.Camera camera, global::UnityEngine.Material material)
		{
			ConfigureFilmGrain(data, settings, camera.pixelWidth, camera.pixelHeight, material);
		}

		public static void ConfigureFilmGrain(global::UnityEngine.Rendering.Universal.PostProcessData data, global::UnityEngine.Rendering.Universal.FilmGrain settings, int cameraPixelWidth, int cameraPixelHeight, global::UnityEngine.Material material)
		{
			global::UnityEngine.Texture texture = settings.texture.value;
			if (settings.type.value != global::UnityEngine.Rendering.Universal.FilmGrainLookup.Custom)
			{
				texture = data.textures.filmGrainTex[(int)settings.type.value];
			}
			global::UnityEngine.Random.State state = global::UnityEngine.Random.state;
			global::UnityEngine.Random.InitState(global::UnityEngine.Time.frameCount);
			float value = global::UnityEngine.Random.value;
			float value2 = global::UnityEngine.Random.value;
			global::UnityEngine.Random.state = state;
			global::UnityEngine.Vector4 value3 = ((texture == null) ? global::UnityEngine.Vector4.zero : new global::UnityEngine.Vector4((float)cameraPixelWidth / (float)texture.width, (float)cameraPixelHeight / (float)texture.height, value, value2));
			material.SetTexture(global::UnityEngine.Rendering.Universal.PostProcessUtils.ShaderConstants._Grain_Texture, texture);
			material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessUtils.ShaderConstants._Grain_Params, new global::UnityEngine.Vector2(settings.intensity.value * 4f, settings.response.value));
			material.SetVector(global::UnityEngine.Rendering.Universal.PostProcessUtils.ShaderConstants._Grain_TilingParams, value3);
		}

		internal static void SetSourceSize(global::UnityEngine.Rendering.RasterCommandBuffer cmd, float width, float height, global::UnityEngine.RenderTexture rt)
		{
			if (rt != null && rt.useDynamicScale)
			{
				width *= global::UnityEngine.ScalableBufferManager.widthScaleFactor;
				height *= global::UnityEngine.ScalableBufferManager.heightScaleFactor;
			}
			cmd.SetGlobalVector(global::UnityEngine.Rendering.Universal.PostProcessUtils.ShaderConstants._SourceSize, new global::UnityEngine.Vector4(width, height, 1f / width, 1f / height));
		}

		internal static void SetSourceSize(global::UnityEngine.Rendering.CommandBuffer cmd, float width, float height, global::UnityEngine.RenderTexture rt)
		{
			SetSourceSize(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd), width, height, rt);
		}

		internal static void SetSourceSize(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source)
		{
			SetSourceSize(cmd, source.rt.width, source.rt.height, source.rt);
		}

		internal static void SetSourceSize(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source)
		{
			SetSourceSize(global::UnityEngine.Rendering.CommandBufferHelpers.GetRasterCommandBuffer(cmd), source);
		}
	}
}
