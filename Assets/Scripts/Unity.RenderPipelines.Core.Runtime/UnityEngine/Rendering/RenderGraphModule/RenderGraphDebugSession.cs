namespace UnityEngine.Rendering.RenderGraphModule
{
	internal abstract class RenderGraphDebugSession : global::System.IDisposable
	{
		protected class DebugDataContainer
		{
			private readonly global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData>> m_Container = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData>>();

			public bool AddGraph(string graphName)
			{
				if (m_Container.ContainsKey(graphName))
				{
					return false;
				}
				m_Container.Add(graphName, new global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData>());
				return true;
			}

			public bool RemoveGraph(string graphName)
			{
				return m_Container.Remove(graphName);
			}

			public bool AddExecution(string graphName, global::UnityEngine.EntityId executionId, string executionName)
			{
				if (m_Container[graphName].ContainsKey(executionId))
				{
					return false;
				}
				m_Container[graphName][executionId] = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData(executionName);
				return true;
			}

			public global::System.Collections.Generic.List<string> GetRenderGraphs()
			{
				return new global::System.Collections.Generic.List<string>(m_Container.Keys);
			}

			public global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem> GetExecutions(string graphName)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem>();
				if (!string.IsNullOrEmpty(graphName) && m_Container.TryGetValue(graphName, out var value))
				{
					foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.EntityId, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData> item2 in value)
					{
						item2.Deconstruct(out var key, out var value2);
						global::UnityEngine.EntityId id = key;
						global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData debugData = value2;
						global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem item = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem(id, debugData.executionName);
						list.Add(item);
					}
				}
				return list;
			}

			public global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData GetDebugData(string renderGraph, global::UnityEngine.EntityId executionId)
			{
				if (!m_Container.TryGetValue(renderGraph, out var value))
				{
					throw new global::System.InvalidOperationException();
				}
				return value[executionId];
			}

			public void SetDebugData(string renderGraph, global::UnityEngine.EntityId executionId, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData data)
			{
				if (m_Container.TryGetValue(renderGraph, out var value))
				{
					value[executionId] = data;
				}
			}

			public void DeleteExecutionIds(string renderGraph, global::System.Collections.Generic.List<global::UnityEngine.EntityId> executionIds)
			{
				if (!m_Container.TryGetValue(renderGraph, out var value))
				{
					return;
				}
				foreach (global::UnityEngine.EntityId executionId in executionIds)
				{
					value.Remove(executionId);
				}
			}

			public void Clear()
			{
				m_Container.Clear();
			}

			public void Invalidate()
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData>> item in m_Container)
				{
					item.Deconstruct(out var _, out var value);
					foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.EntityId, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData> item2 in value)
					{
						item2.Deconstruct(out var _, out var value2);
						value2.Clear();
					}
				}
			}
		}

		private static global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession s_CurrentDebugSession;

		public static global::System.Collections.Generic.List<string> s_EmptyRegisteredGraphs = new global::System.Collections.Generic.List<string>();

		public static global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem> s_EmptyExecutions = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem>();

		public abstract bool isActive { get; }

		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession.DebugDataContainer debugDataContainer { get; }

		public static bool hasActiveDebugSession => s_CurrentDebugSession?.isActive ?? false;

		public static global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession currentDebugSession => s_CurrentDebugSession;

		public static event global::System.Action onRegisteredGraphsChanged;

		public static event global::System.Action<string, global::UnityEngine.EntityId> onDebugDataUpdated;

		protected RenderGraphDebugSession()
		{
			debugDataContainer = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession.DebugDataContainer();
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.onGraphRegistered += RegisterGraph;
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.onGraphUnregistered += UnregisterGraph;
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.onExecutionRegistered += RegisterExecution;
		}

		protected void RegisterGraph(string graphName)
		{
			if (debugDataContainer.AddGraph(graphName))
			{
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession.onRegisteredGraphsChanged?.Invoke();
			}
		}

		protected void UnregisterGraph(string graphName)
		{
			if (debugDataContainer.RemoveGraph(graphName))
			{
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession.onRegisteredGraphsChanged?.Invoke();
			}
		}

		protected void RegisterExecution(string graphName, global::UnityEngine.EntityId executionId, string executionName)
		{
			if (debugDataContainer.AddExecution(graphName, executionId, executionName))
			{
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession.onRegisteredGraphsChanged?.Invoke();
			}
		}

		public virtual void Dispose()
		{
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.onGraphRegistered -= RegisterGraph;
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.onGraphUnregistered -= UnregisterGraph;
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.onExecutionRegistered -= RegisterExecution;
			debugDataContainer.Clear();
		}

		protected void InvalidateData()
		{
			debugDataContainer.Invalidate();
		}

		public static void Create<TSession>() where TSession : global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession, new()
		{
			EndSession();
			s_CurrentDebugSession = new TSession();
		}

		public static void EndSession()
		{
			if (s_CurrentDebugSession != null)
			{
				s_CurrentDebugSession.Dispose();
				s_CurrentDebugSession = null;
			}
		}

		public static global::System.Collections.Generic.List<string> GetRegisteredGraphs()
		{
			if (s_CurrentDebugSession == null || s_CurrentDebugSession.debugDataContainer == null)
			{
				return s_EmptyRegisteredGraphs;
			}
			return s_CurrentDebugSession.debugDataContainer.GetRenderGraphs();
		}

		public static global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem> GetExecutions(string graphName)
		{
			if (s_CurrentDebugSession == null || s_CurrentDebugSession.debugDataContainer == null)
			{
				return s_EmptyExecutions;
			}
			return s_CurrentDebugSession.debugDataContainer.GetExecutions(graphName);
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData GetDebugData(string renderGraph, global::UnityEngine.EntityId executionId)
		{
			return s_CurrentDebugSession.debugDataContainer.GetDebugData(renderGraph, executionId);
		}

		public static void SetDebugData(string renderGraph, global::UnityEngine.EntityId executionId, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData data)
		{
			s_CurrentDebugSession.debugDataContainer.SetDebugData(renderGraph, executionId, data);
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession.onDebugDataUpdated?.Invoke(renderGraph, executionId);
		}

		public static void DeleteExecutionIds(string renderGraph, global::System.Collections.Generic.List<global::UnityEngine.EntityId> executionIds)
		{
			s_CurrentDebugSession.debugDataContainer.DeleteExecutionIds(renderGraph, executionIds);
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugSession.onRegisteredGraphsChanged?.Invoke();
		}

		protected void RegisterAllLocallyKnownGraphsAndExecutions()
		{
			foreach (var (renderGraph2, list2) in global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.GetRegisteredExecutions())
			{
				RegisterGraph(renderGraph2.name);
				foreach (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem item in list2)
				{
					RegisterExecution(renderGraph2.name, item.id, item.name);
				}
			}
		}
	}
}
