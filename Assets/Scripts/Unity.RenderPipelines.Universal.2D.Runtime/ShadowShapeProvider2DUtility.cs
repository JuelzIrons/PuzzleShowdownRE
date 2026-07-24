internal static class ShadowShapeProvider2DUtility
{
	public static float GetTrimEdgeFromBounds(global::UnityEngine.Bounds bounds, float trimMultipler)
	{
		global::UnityEngine.Vector3 size = bounds.size;
		float num = trimMultipler * ((size.x < size.y) ? size.x : size.y);
		float num2 = global::UnityEngine.Mathf.Pow(10f, 0f - global::UnityEngine.Mathf.Floor(global::UnityEngine.Mathf.Log10(num)));
		return global::UnityEngine.Mathf.Floor(num * num2) / num2;
	}

	public static bool IsUsingGpuDeformation()
	{
		return global::UnityEngine.U2D.Animation.SpriteSkinUtility.IsUsingGpuDeformation();
	}
}
