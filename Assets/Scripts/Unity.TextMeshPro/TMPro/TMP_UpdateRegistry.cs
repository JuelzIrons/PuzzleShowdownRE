namespace TMPro
{
	public class TMP_UpdateRegistry
	{
		private static global::TMPro.TMP_UpdateRegistry s_Instance;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.UI.ICanvasElement> m_LayoutRebuildQueue = new global::System.Collections.Generic.List<global::UnityEngine.UI.ICanvasElement>();

		private global::System.Collections.Generic.HashSet<int> m_LayoutQueueLookup = new global::System.Collections.Generic.HashSet<int>();

		private readonly global::System.Collections.Generic.List<global::UnityEngine.UI.ICanvasElement> m_GraphicRebuildQueue = new global::System.Collections.Generic.List<global::UnityEngine.UI.ICanvasElement>();

		private global::System.Collections.Generic.HashSet<int> m_GraphicQueueLookup = new global::System.Collections.Generic.HashSet<int>();

		public static global::TMPro.TMP_UpdateRegistry instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::TMPro.TMP_UpdateRegistry();
				}
				return s_Instance;
			}
		}

		protected TMP_UpdateRegistry()
		{
			global::UnityEngine.Canvas.willRenderCanvases += PerformUpdateForCanvasRendererObjects;
		}

		public static void RegisterCanvasElementForLayoutRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		private bool InternalRegisterCanvasElementForLayoutRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			if (m_LayoutQueueLookup.Contains(instanceID))
			{
				return false;
			}
			m_LayoutQueueLookup.Add(instanceID);
			m_LayoutRebuildQueue.Add(element);
			return true;
		}

		public static void RegisterCanvasElementForGraphicRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		private bool InternalRegisterCanvasElementForGraphicRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			if (m_GraphicQueueLookup.Contains(instanceID))
			{
				return false;
			}
			m_GraphicQueueLookup.Add(instanceID);
			m_GraphicRebuildQueue.Add(element);
			return true;
		}

		private void PerformUpdateForCanvasRendererObjects()
		{
			for (int i = 0; i < m_LayoutRebuildQueue.Count; i++)
			{
				instance.m_LayoutRebuildQueue[i].Rebuild(global::UnityEngine.UI.CanvasUpdate.Prelayout);
			}
			if (m_LayoutRebuildQueue.Count > 0)
			{
				m_LayoutRebuildQueue.Clear();
				m_LayoutQueueLookup.Clear();
			}
			for (int j = 0; j < m_GraphicRebuildQueue.Count; j++)
			{
				instance.m_GraphicRebuildQueue[j].Rebuild(global::UnityEngine.UI.CanvasUpdate.PreRender);
			}
			if (m_GraphicRebuildQueue.Count > 0)
			{
				m_GraphicRebuildQueue.Clear();
				m_GraphicQueueLookup.Clear();
			}
		}

		private void PerformUpdateForMeshRendererObjects()
		{
			global::UnityEngine.Debug.Log("Perform update of MeshRenderer objects.");
		}

		public static void UnRegisterCanvasElementForRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
			instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
		}

		private void InternalUnRegisterCanvasElementForLayoutRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			instance.m_LayoutRebuildQueue.Remove(element);
			m_GraphicQueueLookup.Remove(instanceID);
		}

		private void InternalUnRegisterCanvasElementForGraphicRebuild(global::UnityEngine.UI.ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			instance.m_GraphicRebuildQueue.Remove(element);
			m_LayoutQueueLookup.Remove(instanceID);
		}
	}
}
