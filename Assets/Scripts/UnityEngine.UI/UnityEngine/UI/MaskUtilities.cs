namespace UnityEngine.UI
{
	public class MaskUtilities
	{
		public static void Notify2DMaskStateChanged(global::UnityEngine.Component mask)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			mask.GetComponentsInChildren(list);
			for (int i = 0; i < list.Count; i++)
			{
				if (!(list[i] == null) && !(list[i].gameObject == mask.gameObject) && list[i] is global::UnityEngine.UI.IClippable clippable)
				{
					clippable.RecalculateClipping();
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
		}

		public static void NotifyStencilStateChanged(global::UnityEngine.Component mask)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			mask.GetComponentsInChildren(list);
			for (int i = 0; i < list.Count; i++)
			{
				if (!(list[i] == null) && !(list[i].gameObject == mask.gameObject) && list[i] is global::UnityEngine.UI.IMaskable maskable)
				{
					maskable.RecalculateMasking();
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
		}

		public static global::UnityEngine.Transform FindRootSortOverrideCanvas(global::UnityEngine.Transform start)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Canvas> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Get();
			start.GetComponentsInParent(includeInactive: false, list);
			global::UnityEngine.Canvas canvas = null;
			for (int i = 0; i < list.Count; i++)
			{
				canvas = list[i];
				if (canvas.overrideSorting)
				{
					break;
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Release(list);
			if (!(canvas != null))
			{
				return null;
			}
			return canvas.transform;
		}

		public static int GetStencilDepth(global::UnityEngine.Transform transform, global::UnityEngine.Transform stopAfter)
		{
			int num = 0;
			if (transform == stopAfter)
			{
				return num;
			}
			global::UnityEngine.Transform parent = transform.parent;
			global::System.Collections.Generic.List<global::UnityEngine.UI.Mask> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UI.Mask>, global::UnityEngine.UI.Mask>.Get();
			while (parent != null)
			{
				parent.GetComponents(list);
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] != null && list[i].MaskEnabled() && list[i].graphic.IsActive())
					{
						num++;
						break;
					}
				}
				if (parent == stopAfter)
				{
					break;
				}
				parent = parent.parent;
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UI.Mask>, global::UnityEngine.UI.Mask>.Release(list);
			return num;
		}

		public static bool IsDescendantOrSelf(global::UnityEngine.Transform father, global::UnityEngine.Transform child)
		{
			if (father == null || child == null)
			{
				return false;
			}
			if (father == child)
			{
				return true;
			}
			while (child.parent != null)
			{
				if (child.parent == father)
				{
					return true;
				}
				child = child.parent;
			}
			return false;
		}

		public static global::UnityEngine.UI.RectMask2D GetRectMaskForClippable(global::UnityEngine.UI.IClippable clippable)
		{
			global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D>, global::UnityEngine.UI.RectMask2D>.Get();
			global::System.Collections.Generic.List<global::UnityEngine.Canvas> list2 = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Get();
			global::UnityEngine.UI.RectMask2D rectMask2D = null;
			clippable.gameObject.GetComponentsInParent(includeInactive: false, list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					rectMask2D = list[i];
					if (rectMask2D.gameObject == clippable.gameObject)
					{
						rectMask2D = null;
						continue;
					}
					if (!rectMask2D.isActiveAndEnabled)
					{
						rectMask2D = null;
						continue;
					}
					clippable.gameObject.GetComponentsInParent(includeInactive: false, list2);
					for (int num = list2.Count - 1; num >= 0; num--)
					{
						if (!IsDescendantOrSelf(list2[num].transform, rectMask2D.transform) && list2[num].overrideSorting)
						{
							rectMask2D = null;
							break;
						}
					}
					break;
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D>, global::UnityEngine.UI.RectMask2D>.Release(list);
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Release(list2);
			return rectMask2D;
		}

		public static void GetRectMasksForClip(global::UnityEngine.UI.RectMask2D clipper, global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D> masks)
		{
			masks.Clear();
			global::System.Collections.Generic.List<global::UnityEngine.Canvas> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Get();
			global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D> list2 = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D>, global::UnityEngine.UI.RectMask2D>.Get();
			clipper.transform.GetComponentsInParent(includeInactive: false, list2);
			if (list2.Count > 0)
			{
				clipper.transform.GetComponentsInParent(includeInactive: false, list);
				for (int num = list2.Count - 1; num >= 0; num--)
				{
					if (list2[num].IsActive())
					{
						bool flag = true;
						for (int num2 = list.Count - 1; num2 >= 0; num2--)
						{
							if (!IsDescendantOrSelf(list[num2].transform, list2[num].transform) && list[num2].overrideSorting)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							masks.Add(list2[num]);
						}
					}
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D>, global::UnityEngine.UI.RectMask2D>.Release(list2);
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Release(list);
		}
	}
}
