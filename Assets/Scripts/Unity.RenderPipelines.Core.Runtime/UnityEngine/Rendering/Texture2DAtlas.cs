namespace UnityEngine.Rendering
{
	public class Texture2DAtlas
	{
		private enum BlitType
		{
			Default = 0,
			CubeTo2DOctahedral = 1,
			SingleChannel = 2,
			CubeTo2DOctahedralSingleChannel = 3
		}

		private protected const int kGPUTexInvalid = 0;

		private protected const int kGPUTexValidMip0 = 1;

		private protected const int kGPUTexValidMipAll = 2;

		private protected global::UnityEngine.Rendering.RTHandle m_AtlasTexture;

		private protected int m_Width;

		private protected int m_Height;

		private protected global::UnityEngine.Experimental.Rendering.GraphicsFormat m_Format;

		private protected bool m_UseMipMaps;

		private bool m_IsAtlasTextureOwner;

		private global::UnityEngine.Rendering.AtlasAllocator m_AtlasAllocator;

		private global::System.Collections.Generic.Dictionary<int, (global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Vector2Int size)> m_AllocationCache = new global::System.Collections.Generic.Dictionary<int, (global::UnityEngine.Vector4, global::UnityEngine.Vector2Int)>();

		private global::System.Collections.Generic.Dictionary<int, int> m_IsGPUTextureUpToDate = new global::System.Collections.Generic.Dictionary<int, int>();

		private global::System.Collections.Generic.Dictionary<int, int> m_TextureHashes = new global::System.Collections.Generic.Dictionary<int, int>();

		private static readonly global::UnityEngine.Vector4 fullScaleOffset = new global::UnityEngine.Vector4(1f, 1f, 0f, 0f);

		private static readonly int s_MaxMipLevelPadding = 10;

		public static int maxMipLevelPadding => s_MaxMipLevelPadding;

		public global::UnityEngine.Rendering.RTHandle AtlasTexture => m_AtlasTexture;

		public Texture2DAtlas(int width, int height, global::UnityEngine.Experimental.Rendering.GraphicsFormat format, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, bool powerOfTwoPadding = false, string name = "", bool useMipMap = true)
		{
			m_Width = width;
			m_Height = height;
			m_Format = format;
			m_UseMipMaps = useMipMap;
			m_AtlasTexture = global::UnityEngine.Rendering.RTHandles.Alloc(m_Width, m_Height, m_Format, 1, filterMode, global::UnityEngine.TextureWrapMode.Clamp, global::UnityEngine.Rendering.TextureDimension.Tex2D, enableRandomWrite: false, useMipMap, autoGenerateMips: false, isShadowMap: false, 1, 0f, global::UnityEngine.Rendering.MSAASamples.None, bindTextureMS: false, useDynamicScale: false, useDynamicScaleExplicit: false, global::UnityEngine.RenderTextureMemoryless.None, global::UnityEngine.VRTextureUsage.None, name);
			m_IsAtlasTextureOwner = true;
			int num = ((!useMipMap) ? 1 : GetTextureMipmapCount(m_Width, m_Height));
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.Graphics.SetRenderTarget(m_AtlasTexture, i);
				global::UnityEngine.GL.Clear(clearDepth: false, clearColor: true, global::UnityEngine.Color.clear);
			}
			m_AtlasAllocator = new global::UnityEngine.Rendering.AtlasAllocator(width, height, powerOfTwoPadding);
		}

		public void Release()
		{
			ResetAllocator();
			if (m_IsAtlasTextureOwner)
			{
				global::UnityEngine.Rendering.RTHandles.Release(m_AtlasTexture);
			}
		}

		public void ResetAllocator()
		{
			m_AtlasAllocator.Reset();
			m_AllocationCache.Clear();
			m_IsGPUTextureUpToDate.Clear();
		}

		public void ClearTarget(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			int num = ((!m_UseMipMaps) ? 1 : GetTextureMipmapCount(m_Width, m_Height));
			for (int i = 0; i < num; i++)
			{
				cmd.SetRenderTarget(m_AtlasTexture, i);
				global::UnityEngine.Rendering.Blitter.BlitQuad(cmd, global::UnityEngine.Texture2D.blackTexture, fullScaleOffset, fullScaleOffset, i, bilinear: true);
			}
			m_IsGPUTextureUpToDate.Clear();
		}

		private protected int GetTextureMipmapCount(int width, int height)
		{
			if (!m_UseMipMaps)
			{
				return 1;
			}
			return global::UnityEngine.Rendering.CoreUtils.GetMipCount((float)global::UnityEngine.Mathf.Max(width, height));
		}

		private protected bool Is2D(global::UnityEngine.Texture texture)
		{
			global::UnityEngine.RenderTexture renderTexture = texture as global::UnityEngine.RenderTexture;
			if (!(texture is global::UnityEngine.Texture2D))
			{
				if ((object)renderTexture == null)
				{
					return false;
				}
				return renderTexture.dimension == global::UnityEngine.Rendering.TextureDimension.Tex2D;
			}
			return true;
		}

		private protected bool IsSingleChannelBlit(global::UnityEngine.Texture source, global::UnityEngine.Texture destination)
		{
			uint componentCount = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetComponentCount(source.graphicsFormat);
			uint componentCount2 = global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetComponentCount(destination.graphicsFormat);
			if (componentCount == 1 || componentCount2 == 1)
			{
				if (componentCount != componentCount2)
				{
					return true;
				}
				int num = (1 << (int)(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleA(source.graphicsFormat) & (global::UnityEngine.Rendering.FormatSwizzle)7) << 24) | (1 << (int)(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleB(source.graphicsFormat) & (global::UnityEngine.Rendering.FormatSwizzle)7) << 16) | (1 << (int)(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleG(source.graphicsFormat) & (global::UnityEngine.Rendering.FormatSwizzle)7) << 8) | (1 << (int)(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) & (global::UnityEngine.Rendering.FormatSwizzle)7));
				int num2 = (1 << (int)(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleA(destination.graphicsFormat) & (global::UnityEngine.Rendering.FormatSwizzle)7) << 24) | (1 << (int)(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleB(destination.graphicsFormat) & (global::UnityEngine.Rendering.FormatSwizzle)7) << 16) | (1 << (int)(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleG(destination.graphicsFormat) & (global::UnityEngine.Rendering.FormatSwizzle)7) << 8) | (1 << (int)(global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.GetSwizzleR(destination.graphicsFormat) & (global::UnityEngine.Rendering.FormatSwizzle)7));
				if (num != num2)
				{
					return true;
				}
			}
			return false;
		}

		private void Blit2DTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, global::UnityEngine.Vector4 sourceScaleOffset, bool blitMips, global::UnityEngine.Rendering.Texture2DAtlas.BlitType blitType)
		{
			int num = GetTextureMipmapCount(texture.width, texture.height);
			if (!blitMips)
			{
				num = 1;
			}
			for (int i = 0; i < num; i++)
			{
				cmd.SetRenderTarget(m_AtlasTexture, i);
				switch (blitType)
				{
				case global::UnityEngine.Rendering.Texture2DAtlas.BlitType.Default:
					global::UnityEngine.Rendering.Blitter.BlitQuad(cmd, texture, sourceScaleOffset, scaleOffset, i, bilinear: true);
					break;
				case global::UnityEngine.Rendering.Texture2DAtlas.BlitType.CubeTo2DOctahedral:
					global::UnityEngine.Rendering.Blitter.BlitCubeToOctahedral2DQuad(cmd, texture, scaleOffset, i);
					break;
				case global::UnityEngine.Rendering.Texture2DAtlas.BlitType.SingleChannel:
					global::UnityEngine.Rendering.Blitter.BlitQuadSingleChannel(cmd, texture, sourceScaleOffset, scaleOffset, i);
					break;
				case global::UnityEngine.Rendering.Texture2DAtlas.BlitType.CubeTo2DOctahedralSingleChannel:
					global::UnityEngine.Rendering.Blitter.BlitCubeToOctahedral2DQuadSingleChannel(cmd, texture, scaleOffset, i);
					break;
				}
			}
		}

		private protected void MarkGPUTextureValid(int instanceId, bool mipAreValid = false)
		{
			m_IsGPUTextureUpToDate[instanceId] = ((!mipAreValid) ? 1 : 2);
		}

		private protected void MarkGPUTextureInvalid(int instanceId)
		{
			m_IsGPUTextureUpToDate[instanceId] = 0;
		}

		public virtual void BlitTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, global::UnityEngine.Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (Is2D(texture))
			{
				global::UnityEngine.Rendering.Texture2DAtlas.BlitType blitType = global::UnityEngine.Rendering.Texture2DAtlas.BlitType.Default;
				if (IsSingleChannelBlit(texture, m_AtlasTexture.m_RT))
				{
					blitType = global::UnityEngine.Rendering.Texture2DAtlas.BlitType.SingleChannel;
				}
				Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, blitType);
				int num = ((overrideInstanceID != -1) ? overrideInstanceID : GetTextureID(texture));
				MarkGPUTextureValid(num, blitMips);
				m_TextureHashes[num] = global::UnityEngine.Rendering.CoreUtils.GetTextureHash(texture);
			}
		}

		public virtual void BlitOctahedralTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, global::UnityEngine.Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			BlitTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, overrideInstanceID);
		}

		public virtual void BlitCubeTexture2D(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (texture.dimension == global::UnityEngine.Rendering.TextureDimension.Cube)
			{
				global::UnityEngine.Rendering.Texture2DAtlas.BlitType blitType = global::UnityEngine.Rendering.Texture2DAtlas.BlitType.CubeTo2DOctahedral;
				if (IsSingleChannelBlit(texture, m_AtlasTexture.m_RT))
				{
					blitType = global::UnityEngine.Rendering.Texture2DAtlas.BlitType.CubeTo2DOctahedralSingleChannel;
				}
				Blit2DTexture(cmd, scaleOffset, texture, new global::UnityEngine.Vector4(1f, 1f, 0f, 0f), blitMips, blitType);
				int num = ((overrideInstanceID != -1) ? overrideInstanceID : GetTextureID(texture));
				MarkGPUTextureValid(num, blitMips);
				m_TextureHashes[num] = global::UnityEngine.Rendering.CoreUtils.GetTextureHash(texture);
			}
		}

		public virtual bool AllocateTexture(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			int num = ((overrideInstanceID != -1) ? overrideInstanceID : GetTextureID(texture));
			bool num2 = AllocateTextureWithoutBlit(num, width, height, ref scaleOffset);
			if (num2)
			{
				if (Is2D(texture))
				{
					BlitTexture(cmd, scaleOffset, texture, fullScaleOffset);
				}
				else
				{
					BlitCubeTexture2D(cmd, scaleOffset, texture);
				}
				MarkGPUTextureValid(num, mipAreValid: true);
				m_TextureHashes[num] = global::UnityEngine.Rendering.CoreUtils.GetTextureHash(texture);
			}
			return num2;
		}

		public bool AllocateTextureWithoutBlit(global::UnityEngine.Texture texture, int width, int height, ref global::UnityEngine.Vector4 scaleOffset)
		{
			return AllocateTextureWithoutBlit(texture.GetInstanceID(), width, height, ref scaleOffset);
		}

		public virtual bool AllocateTextureWithoutBlit(int instanceId, int width, int height, ref global::UnityEngine.Vector4 scaleOffset)
		{
			scaleOffset = global::UnityEngine.Vector4.zero;
			if (m_AtlasAllocator.Allocate(ref scaleOffset, width, height))
			{
				scaleOffset.Scale(new global::UnityEngine.Vector4(1f / (float)m_Width, 1f / (float)m_Height, 1f / (float)m_Width, 1f / (float)m_Height));
				m_AllocationCache[instanceId] = (scaleOffset, new global::UnityEngine.Vector2Int(width, height));
				MarkGPUTextureInvalid(instanceId);
				m_TextureHashes[instanceId] = -1;
				return true;
			}
			return false;
		}

		private protected int GetTextureHash(global::UnityEngine.Texture textureA, global::UnityEngine.Texture textureB)
		{
			return global::UnityEngine.Rendering.CoreUtils.GetTextureHash(textureA) + 23 * global::UnityEngine.Rendering.CoreUtils.GetTextureHash(textureB);
		}

		public int GetTextureID(global::UnityEngine.Texture texture)
		{
			return texture.GetInstanceID();
		}

		public int GetTextureID(global::UnityEngine.Texture textureA, global::UnityEngine.Texture textureB)
		{
			return GetTextureID(textureA) + 23 * GetTextureID(textureB);
		}

		public bool IsCached(out global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture textureA, global::UnityEngine.Texture textureB)
		{
			return IsCached(out scaleOffset, GetTextureID(textureA, textureB));
		}

		public bool IsCached(out global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture)
		{
			return IsCached(out scaleOffset, GetTextureID(texture));
		}

		public bool IsCached(out global::UnityEngine.Vector4 scaleOffset, int id)
		{
			(global::UnityEngine.Vector4, global::UnityEngine.Vector2Int) value;
			bool result = m_AllocationCache.TryGetValue(id, out value);
			(scaleOffset, _) = value;
			return result;
		}

		internal global::UnityEngine.Vector2Int GetCachedTextureSize(int id)
		{
			m_AllocationCache.TryGetValue(id, out (global::UnityEngine.Vector4, global::UnityEngine.Vector2Int) value);
			return value.Item2;
		}

		public virtual bool NeedsUpdate(global::UnityEngine.Texture texture, bool needMips = false)
		{
			global::UnityEngine.RenderTexture renderTexture = texture as global::UnityEngine.RenderTexture;
			int textureID = GetTextureID(texture);
			int textureHash = global::UnityEngine.Rendering.CoreUtils.GetTextureHash(texture);
			if (renderTexture != null)
			{
				if (m_IsGPUTextureUpToDate.TryGetValue(textureID, out var value))
				{
					if (renderTexture.updateCount != value)
					{
						m_IsGPUTextureUpToDate[textureID] = (int)renderTexture.updateCount;
						return true;
					}
				}
				else
				{
					m_IsGPUTextureUpToDate[textureID] = (int)renderTexture.updateCount;
				}
			}
			else
			{
				if (m_TextureHashes.TryGetValue(textureID, out var value2) && value2 != textureHash)
				{
					m_TextureHashes[textureID] = textureHash;
					return true;
				}
				if (m_IsGPUTextureUpToDate.TryGetValue(textureID, out var value3))
				{
					if (value3 != 0)
					{
						if (needMips)
						{
							return value3 == 1;
						}
						return false;
					}
					return true;
				}
			}
			return false;
		}

		public virtual bool NeedsUpdate(int id, int updateCount, bool needMips = false)
		{
			if (m_IsGPUTextureUpToDate.TryGetValue(id, out var value))
			{
				if (updateCount != value)
				{
					m_IsGPUTextureUpToDate[id] = updateCount;
					return true;
				}
			}
			else
			{
				m_IsGPUTextureUpToDate[id] = updateCount;
			}
			return false;
		}

		public virtual bool NeedsUpdate(global::UnityEngine.Texture textureA, global::UnityEngine.Texture textureB, bool needMips = false)
		{
			global::UnityEngine.RenderTexture renderTexture = textureA as global::UnityEngine.RenderTexture;
			global::UnityEngine.RenderTexture renderTexture2 = textureB as global::UnityEngine.RenderTexture;
			int textureID = GetTextureID(textureA, textureB);
			int textureHash = GetTextureHash(textureA, textureB);
			if (renderTexture != null || renderTexture2 != null)
			{
				if (m_IsGPUTextureUpToDate.TryGetValue(textureID, out var value))
				{
					if (renderTexture != null && renderTexture2 != null && global::System.Math.Min(renderTexture.updateCount, renderTexture2.updateCount) != value)
					{
						m_IsGPUTextureUpToDate[textureID] = (int)global::System.Math.Min(renderTexture.updateCount, renderTexture2.updateCount);
						return true;
					}
					if (renderTexture != null && renderTexture.updateCount != value)
					{
						m_IsGPUTextureUpToDate[textureID] = (int)renderTexture.updateCount;
						return true;
					}
					if (renderTexture2 != null && renderTexture2.updateCount != value)
					{
						m_IsGPUTextureUpToDate[textureID] = (int)renderTexture2.updateCount;
						return true;
					}
				}
				else
				{
					m_IsGPUTextureUpToDate[textureID] = textureHash;
				}
			}
			else
			{
				if (m_TextureHashes.TryGetValue(textureID, out var value2) && value2 != textureHash)
				{
					m_TextureHashes[textureID] = textureID;
					return true;
				}
				if (m_IsGPUTextureUpToDate.TryGetValue(textureID, out var value3))
				{
					if (value3 != 0)
					{
						if (needMips)
						{
							return value3 == 1;
						}
						return false;
					}
					return true;
				}
			}
			return false;
		}

		public virtual bool AddTexture(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Texture texture)
		{
			if (IsCached(out scaleOffset, texture))
			{
				return true;
			}
			return AllocateTexture(cmd, ref scaleOffset, texture, texture.width, texture.height);
		}

		public virtual bool UpdateTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture oldTexture, global::UnityEngine.Texture newTexture, ref global::UnityEngine.Vector4 scaleOffset, global::UnityEngine.Vector4 sourceScaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			if (IsCached(out scaleOffset, oldTexture))
			{
				if (updateIfNeeded && NeedsUpdate(newTexture))
				{
					if (Is2D(newTexture))
					{
						BlitTexture(cmd, scaleOffset, newTexture, sourceScaleOffset, blitMips);
					}
					else
					{
						BlitCubeTexture2D(cmd, scaleOffset, newTexture, blitMips);
					}
					MarkGPUTextureValid(GetTextureID(newTexture), blitMips);
				}
				return true;
			}
			return AllocateTexture(cmd, ref scaleOffset, newTexture, newTexture.width, newTexture.height);
		}

		public virtual bool UpdateTexture(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture texture, ref global::UnityEngine.Vector4 scaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			return UpdateTexture(cmd, texture, texture, ref scaleOffset, fullScaleOffset, updateIfNeeded, blitMips);
		}

		internal bool EnsureTextureSlot(out bool isUploadNeeded, ref global::UnityEngine.Vector4 scaleBias, int key, int width, int height)
		{
			isUploadNeeded = false;
			if (m_AllocationCache.TryGetValue(key, out (global::UnityEngine.Vector4, global::UnityEngine.Vector2Int) value))
			{
				(scaleBias, _) = value;
				return true;
			}
			if (!m_AtlasAllocator.Allocate(ref scaleBias, width, height))
			{
				return false;
			}
			isUploadNeeded = true;
			scaleBias.Scale(new global::UnityEngine.Vector4(1f / (float)m_Width, 1f / (float)m_Height, 1f / (float)m_Width, 1f / (float)m_Height));
			m_AllocationCache.Add(key, (scaleBias, new global::UnityEngine.Vector2Int(width, height)));
			return true;
		}
	}
}
