namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	public class DisableAnnotationAttribute : global::System.Attribute
	{
		public bool disableIcon { get; set; } = true;

		public bool disableGizmo { get; set; }
	}
}
