namespace UnityEngine.Rendering.Universal
{
	public static class URP2D_GraphicsExtensions
	{
		public static global::UnityEngine.SpriteMaskInteraction GetSpriteMaskInteraction(this global::UnityEngine.MeshRenderer meshRenderer)
		{
			return meshRenderer.Internal_GetSpriteMaskInteraction();
		}

		public static global::UnityEngine.SpriteMaskInteraction GetSpriteMaskInteraction(this global::UnityEngine.SkinnedMeshRenderer skinnedMeshRenderer)
		{
			return skinnedMeshRenderer.Internal_GetSpriteMaskInteraction();
		}

		public static void SetSpriteMaskInteraction(this global::UnityEngine.MeshRenderer meshRenderer, global::UnityEngine.SpriteMaskInteraction maskInteraction)
		{
			meshRenderer.Internal_SetSpriteMaskInteraction(maskInteraction);
		}

		public static void SetSpriteMaskInteraction(this global::UnityEngine.SkinnedMeshRenderer skinnedMeshRenderer, global::UnityEngine.SpriteMaskInteraction maskInteraction)
		{
			skinnedMeshRenderer.Internal_SetSpriteMaskInteraction(maskInteraction);
		}
	}
}
