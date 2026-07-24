namespace UnityEngine.InputSystem.Utilities
{
	internal static class SpriteUtilities
	{
		public unsafe static global::UnityEngine.Sprite CreateCircleSprite(int radius, global::UnityEngine.Color32 colour)
		{
			int num = radius * 2;
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(num, num, global::UnityEngine.Experimental.Rendering.DefaultFormat.LDR, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
			global::Unity.Collections.NativeArray<global::UnityEngine.Color32> rawTextureData = texture2D.GetRawTextureData<global::UnityEngine.Color32>();
			global::UnityEngine.Color32* unsafePtr = (global::UnityEngine.Color32*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(rawTextureData);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemSet(unsafePtr, 0, rawTextureData.Length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Color32>());
			uint* ptr = (uint*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref colour);
			ulong num2 = (ulong)((*(long*)ptr << 32) | *ptr);
			float num3 = radius * radius;
			for (int i = -radius; i < radius; i++)
			{
				int num4 = (int)global::UnityEngine.Mathf.Sqrt(num3 - (float)(i * i));
				global::UnityEngine.Color32* ptr2 = unsafePtr + (i + radius) * num + radius - num4;
				for (int j = 0; j < num4; j++)
				{
					*(ulong*)ptr2 = num2;
					ptr2 += 2;
				}
			}
			texture2D.Apply();
			return global::UnityEngine.Sprite.Create(texture2D, new global::UnityEngine.Rect(0f, 0f, num, num), new global::UnityEngine.Vector2(radius, radius), 1f, 0u, global::UnityEngine.SpriteMeshType.FullRect);
		}
	}
}
