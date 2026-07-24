namespace UnityEngine.UI
{
	public static class Clipping
	{
		public static global::UnityEngine.Rect FindCullAndClipWorldRect(global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D> rectMaskParents, out bool validRect)
		{
			if (rectMaskParents.Count == 0)
			{
				validRect = false;
				return default(global::UnityEngine.Rect);
			}
			global::UnityEngine.Rect canvasRect = rectMaskParents[0].canvasRect;
			global::UnityEngine.Vector4 padding = rectMaskParents[0].padding;
			float num = canvasRect.xMin + padding.x;
			float num2 = canvasRect.xMax - padding.z;
			float num3 = canvasRect.yMin + padding.y;
			float num4 = canvasRect.yMax - padding.w;
			int count = rectMaskParents.Count;
			for (int i = 1; i < count; i++)
			{
				canvasRect = rectMaskParents[i].canvasRect;
				padding = rectMaskParents[i].padding;
				if (num < canvasRect.xMin + padding.x)
				{
					num = canvasRect.xMin + padding.x;
				}
				if (num3 < canvasRect.yMin + padding.y)
				{
					num3 = canvasRect.yMin + padding.y;
				}
				if (num2 > canvasRect.xMax - padding.z)
				{
					num2 = canvasRect.xMax - padding.z;
				}
				if (num4 > canvasRect.yMax - padding.w)
				{
					num4 = canvasRect.yMax - padding.w;
				}
			}
			validRect = num2 > num && num4 > num3;
			if (!validRect)
			{
				return default(global::UnityEngine.Rect);
			}
			return new global::UnityEngine.Rect(num, num3, num2 - num, num4 - num3);
		}
	}
}
