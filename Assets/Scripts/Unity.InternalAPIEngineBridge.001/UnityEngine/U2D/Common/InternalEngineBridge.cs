namespace UnityEngine.U2D.Common
{
	internal static class InternalEngineBridge
	{
		public static void SetLocalAABB(global::UnityEngine.SpriteRenderer spriteRenderer, global::UnityEngine.Bounds aabb)
		{
			spriteRenderer.SetLocalAABB(aabb);
		}

		public static void SetDeformableBuffer(global::UnityEngine.SpriteRenderer spriteRenderer, global::Unity.Collections.NativeArray<byte> src)
		{
			spriteRenderer.SetDeformableBuffer(src);
		}

		public static void SetBoneTransforms(global::UnityEngine.SpriteRenderer spriteRenderer, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> src)
		{
			spriteRenderer.SetBoneTransforms(src);
		}

		public static bool IsUsingDeformableBuffer(global::UnityEngine.SpriteRenderer spriteRenderer, global::System.IntPtr buffer)
		{
			return spriteRenderer.IsUsingDeformableBuffer(buffer);
		}

		public static void SetupMaterialProperties(global::UnityEngine.SpriteRenderer spriteRenderer)
		{
			global::UnityEngine.U2D.SpriteRendererDataAccessExtensions.SetupMaterialProperties(spriteRenderer);
		}

		public static global::UnityEngine.Vector2 GUIUnclip(global::UnityEngine.Vector2 v)
		{
			return global::UnityEngine.GUIClip.Unclip(v);
		}

		public static global::UnityEngine.Rect GetGUIClipTopMostRect()
		{
			return global::UnityEngine.GUIClip.topmostRect;
		}

		public static global::UnityEngine.Rect GetGUIClipTopRect()
		{
			return global::UnityEngine.GUIClip.GetTopRect();
		}

		public static global::UnityEngine.Rect GetGUIClipVisibleRect()
		{
			return global::UnityEngine.GUIClip.visibleRect;
		}

		public static bool IsGPUSkinningEnabled()
		{
			return global::UnityEngine.U2D.SpriteRendererDataAccessExtensions.IsGPUSkinningEnabled();
		}

		public static bool IsSRPBatchingEnabled(global::UnityEngine.SpriteRenderer spriteRenderer)
		{
			return spriteRenderer.IsSRPBatchingEnabled();
		}

		public static void SetBatchDeformableBufferAndLocalAABBArray(global::UnityEngine.SpriteRenderer[] spriteRenderers, global::Unity.Collections.NativeArray<global::System.IntPtr> buffers, global::Unity.Collections.NativeArray<int> bufferSizes, global::Unity.Collections.NativeArray<global::UnityEngine.Bounds> bounds)
		{
			global::UnityEngine.U2D.SpriteRendererDataAccessExtensions.SetBatchDeformableBufferAndLocalAABBArray(spriteRenderers, buffers, bufferSizes, bounds);
		}

		public static void SetBatchBoneTransformsAABBArray(global::UnityEngine.SpriteRenderer[] spriteRenderers, global::Unity.Collections.NativeArray<global::System.IntPtr> buffers, global::Unity.Collections.NativeArray<int> bufferSizes, global::Unity.Collections.NativeArray<global::UnityEngine.Bounds> bounds)
		{
			global::UnityEngine.U2D.SpriteRendererDataAccessExtensions.SetBoneTransformsArray(spriteRenderers, buffers, bufferSizes, bounds);
		}

		public static int ConvertFloatToInt(float f)
		{
			return global::UnityEngine.Animations.DiscreteEvaluationAttributeUtilities.ConvertFloatToDiscreteInt(f);
		}

		public static float ConvertIntToFloat(int i)
		{
			return global::UnityEngine.Animations.DiscreteEvaluationAttributeUtilities.ConvertDiscreteIntToFloat(i);
		}

		public static void MarkDirty(this global::UnityEngine.Object obj)
		{
			obj.MarkDirty();
		}
	}
}
