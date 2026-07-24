namespace Unity.Multiplayer.Center.Common
{
	[global::System.AttributeUsage(global::System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class OnboardingSectionAttribute : global::System.Attribute
	{
		public readonly string Id;

		public global::Unity.Multiplayer.Center.Common.OnboardingSectionCategory Category { get; }

		public global::Unity.Multiplayer.Center.Common.DisplayCondition DisplayCondition { get; set; }

		public global::Unity.Multiplayer.Center.Common.SelectedSolutionsData.HostingModel HostingModelDependency { get; set; }

		public global::Unity.Multiplayer.Center.Common.SelectedSolutionsData.NetcodeSolution NetcodeDependency { get; set; }

		public int Priority { get; set; }

		public int Order { get; set; }

		public string TargetPackageId { get; set; }

		public OnboardingSectionAttribute(global::Unity.Multiplayer.Center.Common.OnboardingSectionCategory category, string id)
		{
			Category = category;
			Id = id;
		}
	}
}
