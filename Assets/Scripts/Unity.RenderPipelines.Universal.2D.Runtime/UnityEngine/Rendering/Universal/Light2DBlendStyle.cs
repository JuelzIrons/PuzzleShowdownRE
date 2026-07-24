namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", "Unity.RenderPipelines.Universal.Runtime", null)]
	public struct Light2DBlendStyle
	{
		internal enum TextureChannel
		{
			None = 0,
			R = 1,
			G = 2,
			B = 3,
			A = 4,
			OneMinusR = 5,
			OneMinusG = 6,
			OneMinusB = 7,
			OneMinusA = 8
		}

		internal struct MaskChannelFilter
		{
			public global::UnityEngine.Vector4 mask { get; private set; }

			public global::UnityEngine.Vector4 inverted { get; private set; }

			public MaskChannelFilter(global::UnityEngine.Vector4 m, global::UnityEngine.Vector4 i)
			{
				mask = m;
				inverted = i;
			}
		}

		internal enum BlendMode
		{
			Additive = 0,
			Multiply = 1,
			Subtractive = 2
		}

		[global::System.Serializable]
		internal struct BlendFactors
		{
			public float multiplicative;

			public float additive;
		}

		public string name;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.Light2DBlendStyle.TextureChannel maskTextureChannel;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.Light2DBlendStyle.BlendMode blendMode;

		internal global::UnityEngine.Vector2 blendFactors
		{
			get
			{
				global::UnityEngine.Vector2 result = default(global::UnityEngine.Vector2);
				switch (blendMode)
				{
				case global::UnityEngine.Rendering.Universal.Light2DBlendStyle.BlendMode.Additive:
					result.x = 0f;
					result.y = 1f;
					break;
				case global::UnityEngine.Rendering.Universal.Light2DBlendStyle.BlendMode.Multiply:
					result.x = 1f;
					result.y = 0f;
					break;
				case global::UnityEngine.Rendering.Universal.Light2DBlendStyle.BlendMode.Subtractive:
					result.x = 0f;
					result.y = -1f;
					break;
				default:
					result.x = 1f;
					result.y = 0f;
					break;
				}
				return result;
			}
		}

		internal global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter maskTextureChannelFilter => maskTextureChannel switch
		{
			global::UnityEngine.Rendering.Universal.Light2DBlendStyle.TextureChannel.R => new global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter(new global::UnityEngine.Vector4(1f, 0f, 0f, 0f), new global::UnityEngine.Vector4(0f, 0f, 0f, 0f)), 
			global::UnityEngine.Rendering.Universal.Light2DBlendStyle.TextureChannel.OneMinusR => new global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter(new global::UnityEngine.Vector4(1f, 0f, 0f, 0f), new global::UnityEngine.Vector4(1f, 0f, 0f, 0f)), 
			global::UnityEngine.Rendering.Universal.Light2DBlendStyle.TextureChannel.G => new global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter(new global::UnityEngine.Vector4(0f, 1f, 0f, 0f), new global::UnityEngine.Vector4(0f, 0f, 0f, 0f)), 
			global::UnityEngine.Rendering.Universal.Light2DBlendStyle.TextureChannel.OneMinusG => new global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter(new global::UnityEngine.Vector4(0f, 1f, 0f, 0f), new global::UnityEngine.Vector4(0f, 1f, 0f, 0f)), 
			global::UnityEngine.Rendering.Universal.Light2DBlendStyle.TextureChannel.B => new global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter(new global::UnityEngine.Vector4(0f, 0f, 1f, 0f), new global::UnityEngine.Vector4(0f, 0f, 0f, 0f)), 
			global::UnityEngine.Rendering.Universal.Light2DBlendStyle.TextureChannel.OneMinusB => new global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter(new global::UnityEngine.Vector4(0f, 0f, 1f, 0f), new global::UnityEngine.Vector4(0f, 0f, 1f, 0f)), 
			global::UnityEngine.Rendering.Universal.Light2DBlendStyle.TextureChannel.A => new global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter(new global::UnityEngine.Vector4(0f, 0f, 0f, 1f), new global::UnityEngine.Vector4(0f, 0f, 0f, 0f)), 
			global::UnityEngine.Rendering.Universal.Light2DBlendStyle.TextureChannel.OneMinusA => new global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter(new global::UnityEngine.Vector4(0f, 0f, 0f, 1f), new global::UnityEngine.Vector4(0f, 0f, 0f, 1f)), 
			_ => new global::UnityEngine.Rendering.Universal.Light2DBlendStyle.MaskChannelFilter(global::UnityEngine.Vector4.zero, global::UnityEngine.Vector4.zero), 
		};
	}
}
