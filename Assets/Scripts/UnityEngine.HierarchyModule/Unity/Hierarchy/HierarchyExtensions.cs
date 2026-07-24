namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.UIToolkitAuthoringModule" })]
	internal static class HierarchyExtensions
	{
		public static T GetNodeTypeHandler<T>(this global::Unity.Hierarchy.Hierarchy hierarchy) where T : global::Unity.Hierarchy.HierarchyNodeTypeHandler
		{
			return hierarchy.GetNodeTypeHandlerBase<T>();
		}

		public static global::Unity.Hierarchy.HierarchyNodeTypeHandler GetNodeTypeHandler(this global::Unity.Hierarchy.Hierarchy hierarchy, in global::Unity.Hierarchy.HierarchyNode node)
		{
			global::Unity.Hierarchy.HierarchyNodeTypeHandlerBase nodeTypeHandlerBase = hierarchy.GetNodeTypeHandlerBase(in node);
			return (nodeTypeHandlerBase is global::Unity.Hierarchy.HierarchyNodeTypeHandler hierarchyNodeTypeHandler) ? hierarchyNodeTypeHandler : null;
		}

		public static global::Unity.Hierarchy.HierarchyNodeTypeHandler GetNodeTypeHandler(this global::Unity.Hierarchy.Hierarchy hierarchy, string nodeTypeName)
		{
			global::Unity.Hierarchy.HierarchyNodeTypeHandlerBase nodeTypeHandlerBase = hierarchy.GetNodeTypeHandlerBase(nodeTypeName);
			return (nodeTypeHandlerBase is global::Unity.Hierarchy.HierarchyNodeTypeHandler hierarchyNodeTypeHandler) ? hierarchyNodeTypeHandler : null;
		}

		public static global::Unity.Hierarchy.HierarchyNodeTypeHandlerEnumerable EnumerateNodeTypeHandlers(this global::Unity.Hierarchy.Hierarchy hierarchy)
		{
			return new global::Unity.Hierarchy.HierarchyNodeTypeHandlerEnumerable(hierarchy);
		}

		public static global::Unity.Hierarchy.HierarchyNode GetNode(this global::Unity.Hierarchy.Hierarchy hierarchy, global::UnityEngine.EntityId entityId)
		{
			if (entityId == global::UnityEngine.EntityId.None)
			{
				return global::Unity.Hierarchy.HierarchyNode.Null;
			}
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in hierarchy.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEntityIdConverter hierarchyEntityIdConverter)
				{
					global::Unity.Hierarchy.HierarchyNode lhs = hierarchyEntityIdConverter.GetNode(entityId);
					if (lhs != global::Unity.Hierarchy.HierarchyNode.Null)
					{
						return lhs;
					}
				}
			}
			return global::Unity.Hierarchy.HierarchyNode.Null;
		}

		public static void GetNodes(this global::Unity.Hierarchy.Hierarchy hierarchy, global::System.ReadOnlySpan<global::UnityEngine.EntityId> entityIds, global::System.Span<global::Unity.Hierarchy.HierarchyNode> outNodes)
		{
			if (outNodes.Length != entityIds.Length)
			{
				throw new global::System.ArgumentException("entityIds and outNodes must have the same length.");
			}
			outNodes.Clear();
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in hierarchy.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEntityIdConverter hierarchyEntityIdConverter)
				{
					hierarchyEntityIdConverter.GetNodes(entityIds, outNodes);
				}
			}
		}

		public static global::UnityEngine.EntityId GetEntityId(this global::Unity.Hierarchy.Hierarchy hierarchy, in global::Unity.Hierarchy.HierarchyNode node)
		{
			if (node == global::Unity.Hierarchy.HierarchyNode.Null)
			{
				return global::UnityEngine.EntityId.None;
			}
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in hierarchy.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEntityIdConverter hierarchyEntityIdConverter)
				{
					global::UnityEngine.EntityId entityId = hierarchyEntityIdConverter.GetEntityId(in node);
					if (entityId != global::UnityEngine.EntityId.None)
					{
						return entityId;
					}
				}
			}
			return global::UnityEngine.EntityId.None;
		}

		public static void GetEntityIds(this global::Unity.Hierarchy.Hierarchy hierarchy, global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::System.Span<global::UnityEngine.EntityId> outEntityIds)
		{
			if (outEntityIds.Length != nodes.Length)
			{
				throw new global::System.ArgumentException("nodes and outEntityIds must have the same length.");
			}
			outEntityIds.Clear();
			foreach (global::Unity.Hierarchy.HierarchyNodeTypeHandler item in hierarchy.EnumerateNodeTypeHandlers())
			{
				if (item is global::Unity.Hierarchy.IHierarchyEntityIdConverter hierarchyEntityIdConverter)
				{
					hierarchyEntityIdConverter.GetEntityIds(nodes, outEntityIds);
				}
			}
		}
	}
}
