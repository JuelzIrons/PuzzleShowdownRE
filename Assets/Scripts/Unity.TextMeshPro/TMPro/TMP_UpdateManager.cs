namespace TMPro
{
	public class TMP_UpdateManager
	{
		private static global::TMPro.TMP_UpdateManager s_Instance;

		private readonly global::System.Collections.Generic.HashSet<int> m_LayoutQueueLookup = new global::System.Collections.Generic.HashSet<int>();

		private readonly global::System.Collections.Generic.List<global::TMPro.TMP_Text> m_LayoutRebuildQueue = new global::System.Collections.Generic.List<global::TMPro.TMP_Text>();

		private readonly global::System.Collections.Generic.HashSet<int> m_GraphicQueueLookup = new global::System.Collections.Generic.HashSet<int>();

		private readonly global::System.Collections.Generic.List<global::TMPro.TMP_Text> m_GraphicRebuildQueue = new global::System.Collections.Generic.List<global::TMPro.TMP_Text>();

		private readonly global::System.Collections.Generic.HashSet<int> m_InternalUpdateLookup = new global::System.Collections.Generic.HashSet<int>();

		private readonly global::System.Collections.Generic.List<global::TMPro.TMP_Text> m_InternalUpdateQueue = new global::System.Collections.Generic.List<global::TMPro.TMP_Text>();

		private readonly global::System.Collections.Generic.HashSet<int> m_CullingUpdateLookup = new global::System.Collections.Generic.HashSet<int>();

		private readonly global::System.Collections.Generic.List<global::TMPro.TMP_Text> m_CullingUpdateQueue = new global::System.Collections.Generic.List<global::TMPro.TMP_Text>();

		private static global::Unity.Profiling.ProfilerMarker k_RegisterTextObjectForUpdateMarker = new global::Unity.Profiling.ProfilerMarker("TMP.RegisterTextObjectForUpdate");

		private static global::Unity.Profiling.ProfilerMarker k_RegisterTextElementForGraphicRebuildMarker = new global::Unity.Profiling.ProfilerMarker("TMP.RegisterTextElementForGraphicRebuild");

		private static global::Unity.Profiling.ProfilerMarker k_RegisterTextElementForCullingUpdateMarker = new global::Unity.Profiling.ProfilerMarker("TMP.RegisterTextElementForCullingUpdate");

		private static global::Unity.Profiling.ProfilerMarker k_UnregisterTextObjectForUpdateMarker = new global::Unity.Profiling.ProfilerMarker("TMP.UnregisterTextObjectForUpdate");

		private static global::Unity.Profiling.ProfilerMarker k_UnregisterTextElementForGraphicRebuildMarker = new global::Unity.Profiling.ProfilerMarker("TMP.UnregisterTextElementForGraphicRebuild");

		private static global::TMPro.TMP_UpdateManager instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::TMPro.TMP_UpdateManager();
				}
				return s_Instance;
			}
		}

		private TMP_UpdateManager()
		{
			global::UnityEngine.Canvas.willRenderCanvases += DoRebuilds;
		}

		internal static void RegisterTextObjectForUpdate(global::TMPro.TMP_Text textObject)
		{
			instance.InternalRegisterTextObjectForUpdate(textObject);
		}

		private void InternalRegisterTextObjectForUpdate(global::TMPro.TMP_Text textObject)
		{
			int instanceID = textObject.GetInstanceID();
			if (!m_InternalUpdateLookup.Contains(instanceID))
			{
				m_InternalUpdateLookup.Add(instanceID);
				m_InternalUpdateQueue.Add(textObject);
			}
		}

		public static void RegisterTextElementForLayoutRebuild(global::TMPro.TMP_Text element)
		{
			instance.InternalRegisterTextElementForLayoutRebuild(element);
		}

		private void InternalRegisterTextElementForLayoutRebuild(global::TMPro.TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (!m_LayoutQueueLookup.Contains(instanceID))
			{
				m_LayoutQueueLookup.Add(instanceID);
				m_LayoutRebuildQueue.Add(element);
			}
		}

		public static void RegisterTextElementForGraphicRebuild(global::TMPro.TMP_Text element)
		{
			instance.InternalRegisterTextElementForGraphicRebuild(element);
		}

		private void InternalRegisterTextElementForGraphicRebuild(global::TMPro.TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (!m_GraphicQueueLookup.Contains(instanceID))
			{
				m_GraphicQueueLookup.Add(instanceID);
				m_GraphicRebuildQueue.Add(element);
			}
		}

		public static void RegisterTextElementForCullingUpdate(global::TMPro.TMP_Text element)
		{
			instance.InternalRegisterTextElementForCullingUpdate(element);
		}

		private void InternalRegisterTextElementForCullingUpdate(global::TMPro.TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (!m_CullingUpdateLookup.Contains(instanceID))
			{
				m_CullingUpdateLookup.Add(instanceID);
				m_CullingUpdateQueue.Add(element);
			}
		}

		private void OnCameraPreCull()
		{
			DoRebuilds();
		}

		private void DoRebuilds()
		{
			for (int i = 0; i < m_InternalUpdateQueue.Count; i++)
			{
				m_InternalUpdateQueue[i].InternalUpdate();
			}
			for (int j = 0; j < m_LayoutRebuildQueue.Count; j++)
			{
				m_LayoutRebuildQueue[j].Rebuild(global::UnityEngine.UI.CanvasUpdate.Prelayout);
			}
			if (m_LayoutRebuildQueue.Count > 0)
			{
				m_LayoutRebuildQueue.Clear();
				m_LayoutQueueLookup.Clear();
			}
			for (int k = 0; k < m_GraphicRebuildQueue.Count; k++)
			{
				m_GraphicRebuildQueue[k].Rebuild(global::UnityEngine.UI.CanvasUpdate.PreRender);
			}
			if (m_GraphicRebuildQueue.Count > 0)
			{
				m_GraphicRebuildQueue.Clear();
				m_GraphicQueueLookup.Clear();
			}
			for (int l = 0; l < m_CullingUpdateQueue.Count; l++)
			{
				m_CullingUpdateQueue[l].UpdateCulling();
			}
			if (m_CullingUpdateQueue.Count > 0)
			{
				m_CullingUpdateQueue.Clear();
				m_CullingUpdateLookup.Clear();
			}
		}

		internal static void UnRegisterTextObjectForUpdate(global::TMPro.TMP_Text textObject)
		{
			instance.InternalUnRegisterTextObjectForUpdate(textObject);
		}

		public static void UnRegisterTextElementForRebuild(global::TMPro.TMP_Text element)
		{
			instance.InternalUnRegisterTextElementForGraphicRebuild(element);
			instance.InternalUnRegisterTextElementForLayoutRebuild(element);
			instance.InternalUnRegisterTextObjectForUpdate(element);
		}

		private void InternalUnRegisterTextElementForGraphicRebuild(global::TMPro.TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			m_GraphicRebuildQueue.Remove(element);
			m_GraphicQueueLookup.Remove(instanceID);
		}

		private void InternalUnRegisterTextElementForLayoutRebuild(global::TMPro.TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			m_LayoutRebuildQueue.Remove(element);
			m_LayoutQueueLookup.Remove(instanceID);
		}

		private void InternalUnRegisterTextObjectForUpdate(global::TMPro.TMP_Text textObject)
		{
			int instanceID = textObject.GetInstanceID();
			m_InternalUpdateQueue.Remove(textObject);
			m_InternalUpdateLookup.Remove(instanceID);
		}
	}
}
