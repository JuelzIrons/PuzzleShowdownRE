namespace UnityEngine.Rendering
{
	public class PowerOfTwoTextureAtlas : global::UnityEngine.Rendering.Texture2DAtlas
	{
		private enum BlitType
		{
			Padding = 0,
			PaddingMultiply = 1,
			OctahedralPadding = 2,
			OctahedralPaddingMultiply = 3
		}

		private readonly int m_MipPadding;

		private const float k_MipmapFactorApprox = 1.33f;

		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Vector2Int> m_RequestedTextures = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Vector2Int>();

		public int mipPadding => m_MipPadding;

		public PowerOfTwoTextureAtlas(int size, int mipPadding, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, string name = "", bool useMipMap = true)
			: base(size, size, format, filterMode, powerOfTwoPadding: true, name, useMipMap)
		{
			m_MipPadding = mipPadding;
			_ = size & (size - 1);
		}

		private int GetTexturePadding()
		{
			return (int)global::UnityEngine.Mathf.Pow(2f, m_MipPadding) * 2;
		}

		public global::UnityEngine.Vector4 GetPayloadScaleOffset(global::UnityEngine.Texture texture, in global::UnityEngine.Vector4 scaleOffset)
		{
			int texturePadding = GetTexturePadding();
			global::UnityEngine.Vector2 paddingSize = global::UnityEngine.Vector2.one * texturePadding;
			return GetPayloadScaleOffset(GetPowerOfTwoTextureSize(texture), in paddingSize, in scaleOffset);
		}

		public static global::UnityEngine.Vector4 GetPayloadScaleOffset(in global::UnityEngine.Vector2 textureSize, in global::UnityEngine.Vector2 paddingSize, in global::UnityEngine.Vector4 scaleOffset)
		{
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(scaleOffset.x, scaleOffset.y);
			global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(scaleOffset.z, scaleOffset.w);
			global::UnityEngine.Vector2 vector3 = (textureSize + paddingSize) / textureSize;
			global::UnityEngine.Vector2 vector4 = paddingSize / 2f / (textureSize + paddingSize);
			global::UnityEngine.Vector2 vector5 = vector / vector3;
			global::UnityEngine.Vector2 vector6 = vector2 + vector * vector4;
			return new global::UnityEngine.Vector4(vector5.x, vector5.y, vector6.x, vector6.y);
		}

		private void Blit2DTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, global::UnityEngine.Vector4 sourceScaleOffset, bool blitMips, global::UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType blitType)
		{
			int num = GetTextureMipmapCount(texture.width, texture.height);
			int texturePadding = GetTexturePadding();
			global::UnityEngine.Vector2 powerOfTwoTextureSize = GetPowerOfTwoTextureSize(texture);
			bool bilinear = texture.filterMode != global::UnityEngine.FilterMode.Point;
			if (!blitMips)
			{
				num = 1;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.CoreProfileId.BlitTextureInPotAtlas)))
			{
				for (int i = 0; i < num; i++)
				{
					cmd.SetRenderTarget(m_AtlasTexture, i);
					switch (blitType)
					{
					case global::UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType.Padding:
						global::UnityEngine.Rendering.Blitter.BlitQuadWithPadding(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, bilinear, texturePadding);
						break;
					case global::UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType.PaddingMultiply:
						global::UnityEngine.Rendering.Blitter.BlitQuadWithPaddingMultiply(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, bilinear, texturePadding);
						break;
					case global::UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType.OctahedralPadding:
						global::UnityEngine.Rendering.Blitter.BlitOctahedralWithPadding(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, bilinear, texturePadding);
						break;
					case global::UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType.OctahedralPaddingMultiply:
						global::UnityEngine.Rendering.Blitter.BlitOctahedralWithPaddingMultiply(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, bilinear, texturePadding);
						break;
					}
				}
			}
		}

		public override void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, global::UnityEngine.Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (Is2D(texture))
			{
				Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, global::UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType.Padding);
				MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		public void BlitTextureMultiply(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, global::UnityEngine.Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (Is2D(texture))
			{
				Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, global::UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType.PaddingMultiply);
				MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		public override void BlitOctahedralTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, global::UnityEngine.Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (Is2D(texture))
			{
				Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, global::UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType.OctahedralPadding);
				MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		public void BlitOctahedralTextureMultiply(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, global::UnityEngine.Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (Is2D(texture))
			{
				Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, global::UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType.OctahedralPaddingMultiply);
				MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		private void TextureSizeToPowerOfTwo(global::UnityEngine.Texture texture, ref int width, ref int height)
		{
			width = global::UnityEngine.Mathf.NextPowerOfTwo(width);
			height = global::UnityEngine.Mathf.NextPowerOfTwo(height);
		}

		private global::UnityEngine.Vector2 GetPowerOfTwoTextureSize(global::UnityEngine.Texture texture)
		{
			int width = texture.width;
			int height = texture.height;
			TextureSizeToPowerOfTwo(texture, ref width, ref height);
			return new global::UnityEngine.Vector2(width, height);
		}

		public override bool AllocateTexture(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			if (height != width)
			{
				global::UnityEngine.Debug.LogError("Can't place " + texture?.ToString() + " in the atlas " + m_AtlasTexture.name + ": Only squared texture are allowed in this atlas.");
				return false;
			}
			TextureSizeToPowerOfTwo(texture, ref height, ref width);
			return base.AllocateTexture(cmd, ref scaleOffset, texture, width, height);
		}

		public void ResetRequestedTexture()
		{
			m_RequestedTextures.Clear();
		}

		public bool ReserveSpace(global::UnityEngine.Texture texture)
		{
			return ReserveSpace(texture, texture.width, texture.height);
		}

		public bool ReserveSpace(global::UnityEngine.Texture texture, int width, int height)
		{
			return ReserveSpace(GetTextureID(texture), width, height);
		}

		public bool ReserveSpace(global::UnityEngine.Texture textureA, global::UnityEngine.Texture textureB, int width, int height)
		{
			return ReserveSpace(GetTextureID(textureA, textureB), width, height);
		}

		public bool ReserveSpace(int id, int width, int height)
		{
			m_RequestedTextures[id] = new global::UnityEngine.Vector2Int(width, height);
			global::UnityEngine.Vector2Int cachedTextureSize = GetCachedTextureSize(id);
			if (!IsCached(out var _, id) || cachedTextureSize.x != width || cachedTextureSize.y != height)
			{
				global::UnityEngine.Vector4 scaleOffset2 = global::UnityEngine.Vector4.zero;
				if (!AllocateTextureWithoutBlit(id, width, height, ref scaleOffset2))
				{
					return false;
				}
			}
			return true;
		}

		public bool RelayoutEntries()
		{
			global::System.Collections.Generic.List<(int, global::UnityEngine.Vector2Int)> list = new global::System.Collections.Generic.List<(int, global::UnityEngine.Vector2Int)>();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::UnityEngine.Vector2Int> requestedTexture in m_RequestedTextures)
			{
				list.Add((requestedTexture.Key, requestedTexture.Value));
			}
			ResetAllocator();
			list.Sort(((int instanceId, global::UnityEngine.Vector2Int size) c1, (int instanceId, global::UnityEngine.Vector2Int size) c2) => c2.size.magnitude.CompareTo(c1.size.magnitude));
			bool flag = true;
			global::UnityEngine.Vector4 scaleOffset = global::UnityEngine.Vector4.zero;
			foreach (var item in list)
			{
				flag &= AllocateTextureWithoutBlit(item.Item1, item.Item2.x, item.Item2.y, ref scaleOffset);
			}
			return flag;
		}

		public static long GetApproxCacheSizeInByte(int nbElement, int resolution, bool hasMipmap, global::UnityEngine.Experimental.Rendering.GraphicsFormat format)
		{
			return (long)((double)(nbElement * resolution * resolution) * (double)((hasMipmap ? 1.33f : 1f) * (float)global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetBlockSize(format)));
		}

		public static int GetMaxCacheSizeForWeightInByte(int weight, bool hasMipmap, global::UnityEngine.Experimental.Rendering.GraphicsFormat format)
		{
			float num = (float)global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetBlockSize(format) * (hasMipmap ? 1.33f : 1f);
			return global::UnityEngine.Rendering.CoreUtils.PreviousPowerOfTwo((int)global::UnityEngine.Mathf.Sqrt((float)weight / num));
		}
	}
}
