namespace Unity.VisualScripting
{
	public static class GraphInstances
	{
		private static readonly object @lock = new object();

		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraph, global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference>> byGraph = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraph, global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference>>();

		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphParent, global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference>> byParent = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphParent, global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference>>();

		public static void Instantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			lock (@lock)
			{
				global::Unity.VisualScripting.Ensure.That("instance").IsNotNull(instance);
				instance.CreateGraphData();
				instance.graph.Instantiate(instance);
				if (!byGraph.TryGetValue(instance.graph, out var value))
				{
					value = new global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference>();
					byGraph.Add(instance.graph, value);
				}
				if (!value.Add(instance))
				{
					global::UnityEngine.Debug.LogWarning($"Attempting to add duplicate graph instance mapping:\n{instance.graph} => {instance}");
				}
				if (!byParent.TryGetValue(instance.parent, out var value2))
				{
					value2 = new global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference>();
					byParent.Add(instance.parent, value2);
				}
				if (!value2.Add(instance))
				{
					global::UnityEngine.Debug.LogWarning($"Attempting to add duplicate parent instance mapping:\n{instance.parent.ToSafeString()} => {instance}");
				}
			}
		}

		public static void Uninstantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			lock (@lock)
			{
				instance.graph.Uninstantiate(instance);
				if (!byGraph.TryGetValue(instance.graph, out var value))
				{
					throw new global::System.InvalidOperationException("Graph instance not found via graph.");
				}
				if (value.Remove(instance))
				{
					if (value.Count == 0)
					{
						byGraph.Remove(instance.graph);
					}
				}
				else
				{
					global::UnityEngine.Debug.LogWarning($"Could not find graph instance mapping to remove:\n{instance.graph} => {instance}");
				}
				if (!byParent.TryGetValue(instance.parent, out var value2))
				{
					throw new global::System.InvalidOperationException("Graph instance not found via parent.");
				}
				if (value2.Remove(instance))
				{
					if (value2.Count == 0)
					{
						byParent.Remove(instance.parent);
					}
				}
				else
				{
					global::UnityEngine.Debug.LogWarning($"Could not find parent instance mapping to remove:\n{instance.parent.ToSafeString()} => {instance}");
				}
				instance.FreeGraphData();
			}
		}

		public static global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference> OfPooled(global::Unity.VisualScripting.IGraph graph)
		{
			global::Unity.VisualScripting.Ensure.That("graph").IsNotNull(graph);
			lock (@lock)
			{
				if (byGraph.TryGetValue(graph, out var value))
				{
					return value.ToHashSetPooled();
				}
				return global::Unity.VisualScripting.HashSetPool<global::Unity.VisualScripting.GraphReference>.New();
			}
		}

		public static global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference> ChildrenOfPooled(global::Unity.VisualScripting.IGraphParent parent)
		{
			global::Unity.VisualScripting.Ensure.That("parent").IsNotNull(parent);
			lock (@lock)
			{
				if (byParent.TryGetValue(parent, out var value))
				{
					return value.ToHashSetPooled();
				}
				return global::Unity.VisualScripting.HashSetPool<global::Unity.VisualScripting.GraphReference>.New();
			}
		}
	}
}
