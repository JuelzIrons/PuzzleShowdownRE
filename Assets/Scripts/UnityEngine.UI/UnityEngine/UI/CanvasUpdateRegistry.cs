namespace UnityEngine.UI
{
	public class CanvasUpdateRegistry
	{
		private static global::UnityEngine.UI.CanvasUpdateRegistry s_Instance;

		private bool m_PerformingLayoutUpdate;

		private bool m_PerformingGraphicUpdate;

		private string[] m_CanvasUpdateProfilerStrings = new string[5] { "CanvasUpdate.Prelayout", "CanvasUpdate.Layout", "CanvasUpdate.PostLayout", "CanvasUpdate.PreRender", "CanvasUpdate.LatePreRender" };

		private const string m_CullingUpdateProfilerString = "ClipperRegistry.Cull";

		private readonly global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.ICanvasElement> m_LayoutRebuildQueue = new global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.ICanvasElement>();

		private readonly global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.ICanvasElement> m_GraphicRebuildQueue = new global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.ICanvasElement>();

		private static readonly global::System.Comparison<global::UnityEngine.UI.ICanvasElement> s_SortLayoutFunction = SortLayoutList;

		public static global::UnityEngine.UI.CanvasUpdateRegistry instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::UnityEngine.UI.CanvasUpdateRegistry();
				}
				return s_Instance;
			}
		}

		protected CanvasUpdateRegistry()
		{
			global::UnityEngine.Canvas.willRenderCanvases += PerformUpdate;
		}

		private bool ObjectValidForUpdate(global::UnityEngine.UI.ICanvasElement element)
		{
			bool result = element != null;
			if (element is global::UnityEngine.Object)
			{
				result = element as global::UnityEngine.Object != null;
			}
			return result;
		}

		private void CleanInvalidItems()
		{
			for (int num = m_LayoutRebuildQueue.Count - 1; num >= 0; num--)
			{
				global::UnityEngine.UI.ICanvasElement canvasElement = m_LayoutRebuildQueue[num];
				if (canvasElement == null)
				{
					m_LayoutRebuildQueue.RemoveAt(num);
				}
				else if (canvasElement.IsDestroyed())
				{
					m_LayoutRebuildQueue.RemoveAt(num);
					canvasElement.LayoutComplete();
				}
			}
			for (int num2 = m_GraphicRebuildQueue.Count - 1; num2 >= 0; num2--)
			{
				global::UnityEngine.UI.ICanvasElement canvasElement2 = m_GraphicRebuildQueue[num2];
				if (canvasElement2 == null)
				{
					m_GraphicRebuildQueue.RemoveAt(num2);
				}
				else if (canvasElement2.IsDestroyed())
				{
					m_GraphicRebuildQueue.RemoveAt(num2);
					canvasElement2.GraphicUpdateComplete();
				}
			}
		}

		private void PerformUpdate()
		{
			global::UnityEngine.UISystemProfilerApi.BeginSample(global::UnityEngine.UISystemProfilerApi.SampleType.Layout);
			CleanInvalidItems();
			m_PerformingLayoutUpdate = true;
			m_LayoutRebuildQueue.Sort(s_SortLayoutFunction);
			for (int i = 0; i <= 2; i++)
			{
				for (int j = 0; j < m_LayoutRebuildQueue.Count; j++)
				{
					global::UnityEngine.UI.ICanvasElement canvasElement = m_LayoutRebuildQueue[j];
					try
					{
						if (ObjectValidForUpdate(canvasElement))
						{
							canvasElement.Rebuild((global::UnityEngine.UI.CanvasUpdate)i);
						}
					}
					catch (global::System.Exception exception)
					{
						global::UnityEngine.Debug.LogException(exception, canvasElement.transform);
					}
				}
			}
			for (int k = 0; k < m_LayoutRebuildQueue.Count; k++)
			{
				m_LayoutRebuildQueue[k].LayoutComplete();
			}
			m_LayoutRebuildQueue.Clear();
			m_PerformingLayoutUpdate = false;
			global::UnityEngine.UISystemProfilerApi.EndSample(global::UnityEngine.UISystemProfilerApi.SampleType.Layout);
			global::UnityEngine.UISystemProfilerApi.BeginSample(global::UnityEngine.UISystemProfilerApi.SampleType.Render);
			global::UnityEngine.UI.ClipperRegistry.instance.Cull();
			m_PerformingGraphicUpdate = true;
			for (int l = 3; l < 5; l++)
			{
				for (int m = 0; m < m_GraphicRebuildQueue.Count; m++)
				{
					try
					{
						global::UnityEngine.UI.ICanvasElement canvasElement2 = m_GraphicRebuildQueue[m];
						if (ObjectValidForUpdate(canvasElement2))
						{
							canvasElement2.Rebuild((global::UnityEngine.UI.CanvasUpdate)l);
						}
					}
					catch (global::System.Exception exception2)
					{
						global::UnityEngine.Debug.LogException(exception2, m_GraphicRebuildQueue[m].transform);
					}
				}
			}
			for (int n = 0; n < m_GraphicRebuildQueue.Count; n++)
			{
				m_GraphicRebuildQueue[n].GraphicUpdateComplete();
			}
			m_GraphicRebuildQueue.Clear();
			m_PerformingGraphicUpdate = false;
			global::UnityEngine.UISystemProfilerApi.EndSample(global::UnityEngine.UISystemProfilerApi.SampleType.Render);
		}

		private static int ParentCount(global::UnityEngine.Transform child)
		{
			if (child == null)
			{
				return 0;
			}
			global::UnityEngine.Transform parent = child.parent;
			int num = 0;
			while (parent != null)
			{
				num++;
				parent = parent.parent;
			}
			return num;
		}

		private static int SortLayoutList(global::UnityEngine.UI.ICanvasElement x, global::UnityEngine.UI.ICanvasElement y)
		{
			global::UnityEngine.Transform transform = x.transform;
			global::UnityEngine.Transform transform2 = y.transform;
			return ParentCount(transform) - ParentCount(transform2);
		}

		public static void RegisterCanvasElementForLayoutRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		public static bool TryRegisterCanvasElementForLayoutRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			return instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		private bool InternalRegisterCanvasElementForLayoutRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			if (m_LayoutRebuildQueue.Contains(element))
			{
				return false;
			}
			return m_LayoutRebuildQueue.AddUnique(element);
		}

		public static void RegisterCanvasElementForGraphicRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		public static bool TryRegisterCanvasElementForGraphicRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			return instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		private bool InternalRegisterCanvasElementForGraphicRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			if (m_PerformingGraphicUpdate)
			{
				global::UnityEngine.Debug.LogError($"Trying to add {element} for graphic rebuild while we are already inside a graphic rebuild loop. This is not supported.");
				return false;
			}
			return m_GraphicRebuildQueue.AddUnique(element);
		}

		public static void UnRegisterCanvasElementForRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
			instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
		}

		public static void DisableCanvasElementForRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			instance.InternalDisableCanvasElementForLayoutRebuild(element);
			instance.InternalDisableCanvasElementForGraphicRebuild(element);
		}

		private void InternalUnRegisterCanvasElementForLayoutRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			if (m_PerformingLayoutUpdate)
			{
				global::UnityEngine.Debug.LogError($"Trying to remove {element} from rebuild list while we are already inside a rebuild loop. This is not supported.");
				return;
			}
			element.LayoutComplete();
			instance.m_LayoutRebuildQueue.Remove(element);
		}

		private void InternalUnRegisterCanvasElementForGraphicRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			if (m_PerformingGraphicUpdate)
			{
				global::UnityEngine.Debug.LogError($"Trying to remove {element} from rebuild list while we are already inside a rebuild loop. This is not supported.");
				return;
			}
			element.GraphicUpdateComplete();
			instance.m_GraphicRebuildQueue.Remove(element);
		}

		private void InternalDisableCanvasElementForLayoutRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			if (m_PerformingLayoutUpdate)
			{
				global::UnityEngine.Debug.LogError($"Trying to remove {element} from rebuild list while we are already inside a rebuild loop. This is not supported.");
				return;
			}
			element.LayoutComplete();
			instance.m_LayoutRebuildQueue.DisableItem(element);
		}

		private void InternalDisableCanvasElementForGraphicRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			if (m_PerformingGraphicUpdate)
			{
				global::UnityEngine.Debug.LogError($"Trying to remove {element} from rebuild list while we are already inside a rebuild loop. This is not supported.");
				return;
			}
			element.GraphicUpdateComplete();
			instance.m_GraphicRebuildQueue.DisableItem(element);
		}

		public static bool IsRebuildingLayout()
		{
			return instance.m_PerformingLayoutUpdate;
		}

		public static bool IsRebuildingGraphics()
		{
			return instance.m_PerformingGraphicUpdate;
		}
	}
}
