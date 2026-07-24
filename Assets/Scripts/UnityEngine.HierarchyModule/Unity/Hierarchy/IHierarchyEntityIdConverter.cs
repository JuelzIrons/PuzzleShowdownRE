namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.UIToolkitAuthoringModule" })]
	internal interface IHierarchyEntityIdConverter
	{
		protected internal global::Unity.Hierarchy.HierarchyNode GetNode(global::UnityEngine.EntityId entityId);

		protected internal void GetNodes(global::System.ReadOnlySpan<global::UnityEngine.EntityId> entityIds, global::System.Span<global::Unity.Hierarchy.HierarchyNode> outNodes);

		protected internal global::UnityEngine.EntityId GetEntityId(in global::Unity.Hierarchy.HierarchyNode node);

		protected internal void GetEntityIds(global::System.ReadOnlySpan<global::Unity.Hierarchy.HierarchyNode> nodes, global::System.Span<global::UnityEngine.EntityId> outEntityIds);
	}
}
