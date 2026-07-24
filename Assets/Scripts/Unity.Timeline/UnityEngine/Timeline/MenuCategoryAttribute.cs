namespace UnityEngine.Timeline
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	internal class MenuCategoryAttribute : global::System.Attribute
	{
		public readonly string category;

		public MenuCategoryAttribute(string category)
		{
			this.category = category ?? string.Empty;
		}
	}
}
