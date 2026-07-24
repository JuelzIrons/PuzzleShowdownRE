namespace UnityEngine.InputSystem.OnScreen
{
	internal static class UGUIOnScreenControlUtils
	{
		public static global::UnityEngine.RectTransform GetCanvasRectTransform(global::UnityEngine.Transform transform)
		{
			if (!(transform.parent != null))
			{
				return null;
			}
			return transform.parent.GetComponentInParent<global::UnityEngine.RectTransform>();
		}
	}
}
