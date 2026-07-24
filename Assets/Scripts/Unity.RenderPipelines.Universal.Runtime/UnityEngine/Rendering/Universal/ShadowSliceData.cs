namespace UnityEngine.Rendering.Universal
{
	public struct ShadowSliceData
	{
		public global::UnityEngine.Matrix4x4 viewMatrix;

		public global::UnityEngine.Matrix4x4 projectionMatrix;

		public global::UnityEngine.Matrix4x4 shadowTransform;

		public int offsetX;

		public int offsetY;

		public int resolution;

		public global::UnityEngine.Rendering.ShadowSplitData splitData;

		public void Clear()
		{
			viewMatrix = global::UnityEngine.Matrix4x4.identity;
			projectionMatrix = global::UnityEngine.Matrix4x4.identity;
			shadowTransform = global::UnityEngine.Matrix4x4.identity;
			offsetX = (offsetY = 0);
			resolution = 1024;
		}
	}
}
