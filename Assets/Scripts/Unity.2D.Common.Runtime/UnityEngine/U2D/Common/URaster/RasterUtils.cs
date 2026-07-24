namespace UnityEngine.U2D.Common.URaster
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct RasterUtils
	{
		internal unsafe static global::UnityEngine.Color32* GetPixelOffsetBuffer(int offset, global::UnityEngine.Color32* pixels)
		{
			return pixels + offset;
		}

		internal unsafe static global::UnityEngine.Color32 GetPixel(global::UnityEngine.Color32* pixels, ref global::Unity.Mathematics.int2 textureCfg, int x, int y)
		{
			int num = x + y * textureCfg.x;
			return pixels[num];
		}

		internal static byte Color32ToByte(global::UnityEngine.Color32 rgba)
		{
			int num = global::Unity.Mathematics.math.min(rgba.r / 128 + ((rgba.r != 0) ? 1 : 0), 3);
			int num2 = global::Unity.Mathematics.math.min(rgba.g / 128 + ((rgba.g != 0) ? 1 : 0), 3);
			int num3 = global::Unity.Mathematics.math.min(rgba.b / 128 + ((rgba.b != 0) ? 1 : 0), 3);
			return (byte)(((rgba.a != 0) ? 3 : 0) | (num3 << 2) | (num2 << 4) | (num << 6));
		}

		internal static global::UnityEngine.Color32 ByteToColor32(byte rgba)
		{
			global::UnityEngine.Color32 result = default(global::UnityEngine.Color32);
			result.r = (byte)(((rgba >> 6) & 3) * 64);
			result.g = (byte)(((rgba >> 4) & 3) * 64);
			result.b = (byte)(((rgba >> 2) & 3) * 64);
			result.a = (byte)(((rgba & 3) != 0) ? 255u : 0u);
			return result;
		}

		internal static float Min3(float a, float b, float c)
		{
			float y = global::Unity.Mathematics.math.min(b, c);
			return global::Unity.Mathematics.math.min(a, y);
		}

		internal static float Max3(float a, float b, float c)
		{
			float y = global::Unity.Mathematics.math.max(b, c);
			return global::Unity.Mathematics.math.max(a, y);
		}

		internal static int Orient2d(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2 b, global::Unity.Mathematics.float2 c)
		{
			return (int)((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x));
		}

		internal static bool IsValidColorByte(byte c)
		{
			if ((c & 0xFC) != 0)
			{
				return (c & 3) != 0;
			}
			return false;
		}

		internal unsafe static byte Pixelate(ref global::UnityEngine.U2D.Common.URaster.Pixels pixelMask, ref global::Unity.Mathematics.int2 textureCfg, global::UnityEngine.Color32* pixels, byte fillColorByte, int sx, int sy, int x, int y)
		{
			int num = x - pixelMask.texrect.x;
			int num2 = y - pixelMask.texrect.y;
			byte b = fillColorByte;
			b = Color32ToByte(GetPixel(pixels, ref textureCfg, sx, sy));
			b = (IsValidColorByte(b) ? b : fillColorByte);
			pixelMask.data[num2 * pixelMask.size.x + num] = b;
			pixelMask.minmax.x = global::Unity.Mathematics.math.min(num, pixelMask.minmax.x);
			pixelMask.minmax.y = global::Unity.Mathematics.math.min(num2, pixelMask.minmax.y);
			pixelMask.minmax.z = global::Unity.Mathematics.math.max(num, pixelMask.minmax.z);
			pixelMask.minmax.w = global::Unity.Mathematics.math.max(num2, pixelMask.minmax.w);
			return b;
		}

		internal static void Pad(ref global::UnityEngine.U2D.Common.URaster.Pixels pixelMask, byte srcColorByte, byte tgtColorByte, int dx, int dy, int padx, int pady)
		{
			if (!IsValidColorByte(srcColorByte))
			{
				return;
			}
			for (int i = -pady; i < pady; i++)
			{
				for (int j = -padx; j < padx; j++)
				{
					int num = global::Unity.Mathematics.math.min(global::Unity.Mathematics.math.max(dx + j, 0), pixelMask.size.x) - pixelMask.texrect.x;
					int num2 = global::Unity.Mathematics.math.min(global::Unity.Mathematics.math.max(dy + i, 0), pixelMask.size.y) - pixelMask.texrect.y;
					if (num >= 0 && num2 >= 0 && num <= pixelMask.size.x && num2 <= pixelMask.size.y && pixelMask.data[num2 * pixelMask.size.x + num] == 0)
					{
						pixelMask.data[num2 * pixelMask.size.x + num] = tgtColorByte;
						pixelMask.minmax.x = global::Unity.Mathematics.math.min(num, pixelMask.minmax.x);
						pixelMask.minmax.y = global::Unity.Mathematics.math.min(num2, pixelMask.minmax.y);
						pixelMask.minmax.z = global::Unity.Mathematics.math.max(num, pixelMask.minmax.z);
						pixelMask.minmax.w = global::Unity.Mathematics.math.max(num2, pixelMask.minmax.w);
					}
				}
			}
		}

		internal unsafe static void RasterizeTriangle(ref global::UnityEngine.U2D.Common.URaster.Pixels pixelMask, global::UnityEngine.Color32* pixels, ref global::Unity.Mathematics.int2 textureCfg, byte fillColorByte, ref global::Unity.Mathematics.float2 v0, ref global::Unity.Mathematics.float2 v1, ref global::Unity.Mathematics.float2 v2, int padx, int pady)
		{
			int x = (int)Min3(v0.x, v1.x, v2.x);
			int x2 = (int)Min3(v0.y, v1.y, v2.y);
			int x3 = (int)Max3(v0.x, v1.x, v2.x);
			int x4 = (int)Max3(v0.y, v1.y, v2.y);
			byte tgtColorByte = Color32ToByte(new global::UnityEngine.Color32(64, 64, 254, 254));
			x = global::Unity.Mathematics.math.max(x, 0);
			x2 = global::Unity.Mathematics.math.max(x2, 0);
			x3 = global::Unity.Mathematics.math.min(x3, pixelMask.rect.x - 1);
			x4 = global::Unity.Mathematics.math.min(x4, pixelMask.rect.y - 1);
			int num = (int)(v0.y - v1.y);
			int num2 = (int)(v1.x - v0.x);
			int num3 = (int)(v1.y - v2.y);
			int num4 = (int)(v2.x - v1.x);
			int num5 = (int)(v2.y - v0.y);
			int num6 = (int)(v0.x - v2.x);
			global::Unity.Mathematics.float2 c = new global::Unity.Mathematics.float2(x, x2);
			int num7 = Orient2d(v1, v2, c);
			int num8 = Orient2d(v2, v0, c);
			int num9 = Orient2d(v0, v1, c);
			for (int i = x2; i <= x4; i++)
			{
				int num10 = num7;
				int num11 = num8;
				int num12 = num9;
				for (int j = x; j <= x3; j++)
				{
					if ((num10 | num11 | num12) >= 0)
					{
						int num13 = j + padx;
						int num14 = i + pady;
						byte srcColorByte = Pixelate(ref pixelMask, ref textureCfg, pixels, fillColorByte, j, i, num13, num14);
						Pad(ref pixelMask, srcColorByte, tgtColorByte, num13, num14, padx, pady);
					}
					num10 += num3;
					num11 += num5;
					num12 += num;
				}
				num7 += num4;
				num8 += num6;
				num9 += num2;
			}
		}

		internal unsafe static bool Rasterize(global::UnityEngine.Color32* pixels, ref global::Unity.Mathematics.int2 textureCfg, global::UnityEngine.Vector2* vertices, int vertexCount, int* indices, int indexCount, ref global::UnityEngine.U2D.Common.URaster.Pixels pixelMask, int padx, int pady)
		{
			_ = global::Unity.Mathematics.float2.zero;
			byte fillColorByte = Color32ToByte(new global::UnityEngine.Color32(64, 254, 64, 254));
			for (int i = 0; i < indexCount; i += 3)
			{
				int num = indices[i];
				int num2 = indices[i + 1];
				int num3 = indices[i + 2];
				global::Unity.Mathematics.float2 v = vertices[num];
				global::Unity.Mathematics.float2 v2 = vertices[num2];
				global::Unity.Mathematics.float2 v3 = vertices[num3];
				if (Orient2d(v, v2, v3) < 0)
				{
					global::Unity.Mathematics.float2 obj = v;
					v = v2;
					v2 = obj;
				}
				RasterizeTriangle(ref pixelMask, pixels, ref textureCfg, fillColorByte, ref v, ref v2, ref v3, padx, pady);
			}
			return true;
		}

		internal static void SaveImage(global::Unity.Collections.NativeArray<byte> image, int w, int h, string path)
		{
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(w, h, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
			global::Unity.Collections.NativeArray<global::UnityEngine.Color32> data = new global::Unity.Collections.NativeArray<global::UnityEngine.Color32>(image.Length, global::Unity.Collections.Allocator.Persistent);
			for (int i = 0; i < image.Length; i++)
			{
				data[i] = ByteToColor32(image[i]);
			}
			texture2D.SetPixelData(data, 0);
			byte[] bytes = texture2D.EncodeToPNG();
			global::System.IO.File.WriteAllBytes(path, bytes);
		}
	}
}
