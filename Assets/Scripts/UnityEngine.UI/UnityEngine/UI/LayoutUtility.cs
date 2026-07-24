namespace UnityEngine.UI
{
	public static class LayoutUtility
	{
		public static float GetMinSize(global::UnityEngine.RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return GetMinHeight(rect);
			}
			return GetMinWidth(rect);
		}

		public static float GetPreferredSize(global::UnityEngine.RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return GetPreferredHeight(rect);
			}
			return GetPreferredWidth(rect);
		}

		public static float GetFlexibleSize(global::UnityEngine.RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return GetFlexibleHeight(rect);
			}
			return GetFlexibleWidth(rect);
		}

		public static float GetMinWidth(global::UnityEngine.RectTransform rect)
		{
			return GetLayoutProperty(rect, (global::UnityEngine.UI.ILayoutElement e) => e.minWidth, 0f);
		}

		public static float GetPreferredWidth(global::UnityEngine.RectTransform rect)
		{
			return global::UnityEngine.Mathf.Max(GetLayoutProperty(rect, (global::UnityEngine.UI.ILayoutElement e) => e.minWidth, 0f), GetLayoutProperty(rect, (global::UnityEngine.UI.ILayoutElement e) => e.preferredWidth, 0f));
		}

		public static float GetFlexibleWidth(global::UnityEngine.RectTransform rect)
		{
			return GetLayoutProperty(rect, (global::UnityEngine.UI.ILayoutElement e) => e.flexibleWidth, 0f);
		}

		public static float GetMinHeight(global::UnityEngine.RectTransform rect)
		{
			return GetLayoutProperty(rect, (global::UnityEngine.UI.ILayoutElement e) => e.minHeight, 0f);
		}

		public static float GetPreferredHeight(global::UnityEngine.RectTransform rect)
		{
			return global::UnityEngine.Mathf.Max(GetLayoutProperty(rect, (global::UnityEngine.UI.ILayoutElement e) => e.minHeight, 0f), GetLayoutProperty(rect, (global::UnityEngine.UI.ILayoutElement e) => e.preferredHeight, 0f));
		}

		public static float GetFlexibleHeight(global::UnityEngine.RectTransform rect)
		{
			return GetLayoutProperty(rect, (global::UnityEngine.UI.ILayoutElement e) => e.flexibleHeight, 0f);
		}

		public static float GetLayoutProperty(global::UnityEngine.RectTransform rect, global::System.Func<global::UnityEngine.UI.ILayoutElement, float> property, float defaultValue)
		{
			global::UnityEngine.UI.ILayoutElement source;
			return GetLayoutProperty(rect, property, defaultValue, out source);
		}

		public static float GetLayoutProperty(global::UnityEngine.RectTransform rect, global::System.Func<global::UnityEngine.UI.ILayoutElement, float> property, float defaultValue, out global::UnityEngine.UI.ILayoutElement source)
		{
			source = null;
			if (rect == null)
			{
				return 0f;
			}
			float num = defaultValue;
			int num2 = int.MinValue;
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			rect.GetComponents(typeof(global::UnityEngine.UI.ILayoutElement), list);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.UI.ILayoutElement layoutElement = list[i] as global::UnityEngine.UI.ILayoutElement;
				if (layoutElement is global::UnityEngine.Behaviour && !((global::UnityEngine.Behaviour)layoutElement).isActiveAndEnabled)
				{
					continue;
				}
				int layoutPriority = layoutElement.layoutPriority;
				if (layoutPriority < num2)
				{
					continue;
				}
				float num3 = property(layoutElement);
				if (!(num3 < 0f))
				{
					if (layoutPriority > num2)
					{
						num = num3;
						num2 = layoutPriority;
						source = layoutElement;
					}
					else if (num3 > num)
					{
						num = num3;
						source = layoutElement;
					}
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
			return num;
		}
	}
}
