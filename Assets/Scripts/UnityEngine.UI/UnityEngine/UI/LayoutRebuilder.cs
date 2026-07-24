namespace UnityEngine.UI
{
	public class LayoutRebuilder : global::UnityEngine.UI.ICanvasElement
	{
		private global::UnityEngine.RectTransform m_ToRebuild;

		private int m_CachedHashFromTransform;

		private static global::UnityEngine.Pool.ObjectPool<global::UnityEngine.UI.LayoutRebuilder> s_Rebuilders;

		public global::UnityEngine.Transform transform => m_ToRebuild;

		private void Initialize(global::UnityEngine.RectTransform controller)
		{
			m_ToRebuild = controller;
			m_CachedHashFromTransform = controller.GetHashCode();
		}

		private void Clear()
		{
			m_ToRebuild = null;
			m_CachedHashFromTransform = 0;
		}

		static LayoutRebuilder()
		{
			s_Rebuilders = new global::UnityEngine.Pool.ObjectPool<global::UnityEngine.UI.LayoutRebuilder>(() => new global::UnityEngine.UI.LayoutRebuilder(), null, delegate(global::UnityEngine.UI.LayoutRebuilder x)
			{
				x.Clear();
			});
			global::UnityEngine.RectTransform.reapplyDrivenProperties += ReapplyDrivenProperties;
		}

		private static void ReapplyDrivenProperties(global::UnityEngine.RectTransform driven)
		{
			MarkLayoutForRebuild(driven);
		}

		public bool IsDestroyed()
		{
			return m_ToRebuild == null;
		}

		private static void StripDisabledBehavioursFromList(global::System.Collections.Generic.List<global::UnityEngine.Component> components)
		{
			components.RemoveAll((global::UnityEngine.Component e) => e is global::UnityEngine.Behaviour && !((global::UnityEngine.Behaviour)e).isActiveAndEnabled);
		}

		public static void ForceRebuildLayoutImmediate(global::UnityEngine.RectTransform layoutRoot)
		{
			global::UnityEngine.UI.LayoutRebuilder layoutRebuilder = s_Rebuilders.Get();
			layoutRebuilder.Initialize(layoutRoot);
			layoutRebuilder.Rebuild(global::UnityEngine.UI.CanvasUpdate.Layout);
			s_Rebuilders.Release(layoutRebuilder);
		}

		public void Rebuild(global::UnityEngine.UI.CanvasUpdate executing)
		{
			if (executing == global::UnityEngine.UI.CanvasUpdate.Layout)
			{
				PerformLayoutCalculation(m_ToRebuild, delegate(global::UnityEngine.Component e)
				{
					(e as global::UnityEngine.UI.ILayoutElement).CalculateLayoutInputHorizontal();
				});
				PerformLayoutControl(m_ToRebuild, delegate(global::UnityEngine.Component e)
				{
					(e as global::UnityEngine.UI.ILayoutController).SetLayoutHorizontal();
				});
				PerformLayoutCalculation(m_ToRebuild, delegate(global::UnityEngine.Component e)
				{
					(e as global::UnityEngine.UI.ILayoutElement).CalculateLayoutInputVertical();
				});
				PerformLayoutControl(m_ToRebuild, delegate(global::UnityEngine.Component e)
				{
					(e as global::UnityEngine.UI.ILayoutController).SetLayoutVertical();
				});
			}
		}

		private void PerformLayoutControl(global::UnityEngine.RectTransform rect, global::UnityEngine.Events.UnityAction<global::UnityEngine.Component> action)
		{
			if (rect == null)
			{
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			rect.GetComponents(typeof(global::UnityEngine.UI.ILayoutController), list);
			StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] is global::UnityEngine.UI.ILayoutSelfController)
					{
						action(list[i]);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j] is global::UnityEngine.UI.ILayoutSelfController)
					{
						continue;
					}
					global::UnityEngine.Component component = list[j];
					if ((bool)component && component is global::UnityEngine.UI.ScrollRect)
					{
						if (((global::UnityEngine.UI.ScrollRect)component).content != rect)
						{
							action(list[j]);
						}
					}
					else
					{
						action(list[j]);
					}
				}
				for (int k = 0; k < rect.childCount; k++)
				{
					PerformLayoutControl(rect.GetChild(k) as global::UnityEngine.RectTransform, action);
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
		}

		private void PerformLayoutCalculation(global::UnityEngine.RectTransform rect, global::UnityEngine.Events.UnityAction<global::UnityEngine.Component> action)
		{
			if (rect == null)
			{
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			rect.GetComponents(typeof(global::UnityEngine.UI.ILayoutElement), list);
			StripDisabledBehavioursFromList(list);
			if (list.Count > 0 || rect.TryGetComponent(typeof(global::UnityEngine.UI.ILayoutGroup), out var _))
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					PerformLayoutCalculation(rect.GetChild(i) as global::UnityEngine.RectTransform, action);
				}
				for (int j = 0; j < list.Count; j++)
				{
					action(list[j]);
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
		}

		public static void MarkLayoutForRebuild(global::UnityEngine.RectTransform rect)
		{
			if (rect == null || rect.gameObject == null)
			{
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			bool flag = true;
			global::UnityEngine.RectTransform rectTransform = rect;
			global::UnityEngine.RectTransform rectTransform2 = rectTransform.parent as global::UnityEngine.RectTransform;
			while (flag && !(rectTransform2 == null) && !(rectTransform2.gameObject == null))
			{
				flag = false;
				rectTransform2.GetComponents(typeof(global::UnityEngine.UI.ILayoutGroup), list);
				for (int i = 0; i < list.Count; i++)
				{
					global::UnityEngine.Component component = list[i];
					if (component != null && component is global::UnityEngine.Behaviour && ((global::UnityEngine.Behaviour)component).isActiveAndEnabled)
					{
						flag = true;
						rectTransform = rectTransform2;
						break;
					}
				}
				rectTransform2 = rectTransform2.parent as global::UnityEngine.RectTransform;
			}
			if (rectTransform == rect && !ValidController(rectTransform, list))
			{
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
				return;
			}
			MarkLayoutRootForRebuild(rectTransform);
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
		}

		private static bool ValidController(global::UnityEngine.RectTransform layoutRoot, global::System.Collections.Generic.List<global::UnityEngine.Component> comps)
		{
			if (layoutRoot == null || layoutRoot.gameObject == null)
			{
				return false;
			}
			layoutRoot.GetComponents(typeof(global::UnityEngine.UI.ILayoutController), comps);
			for (int i = 0; i < comps.Count; i++)
			{
				global::UnityEngine.Component component = comps[i];
				if (component != null && component is global::UnityEngine.Behaviour && ((global::UnityEngine.Behaviour)component).isActiveAndEnabled)
				{
					return true;
				}
			}
			return false;
		}

		private static void MarkLayoutRootForRebuild(global::UnityEngine.RectTransform controller)
		{
			if (!(controller == null))
			{
				global::UnityEngine.UI.LayoutRebuilder layoutRebuilder = s_Rebuilders.Get();
				layoutRebuilder.Initialize(controller);
				if (!global::UnityEngine.UI.CanvasUpdateRegistry.TryRegisterCanvasElementForLayoutRebuild(layoutRebuilder))
				{
					s_Rebuilders.Release(layoutRebuilder);
				}
			}
		}

		public void LayoutComplete()
		{
			s_Rebuilders.Release(this);
		}

		public void GraphicUpdateComplete()
		{
		}

		public override int GetHashCode()
		{
			return m_CachedHashFromTransform;
		}

		public override bool Equals(object obj)
		{
			return obj.GetHashCode() == GetHashCode();
		}

		public override string ToString()
		{
			return "(Layout Rebuilder for) " + m_ToRebuild;
		}
	}
}
