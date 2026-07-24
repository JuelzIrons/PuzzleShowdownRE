namespace UnityEngine.Rendering
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Field)]
	public sealed class ReloadAttribute : global::System.Attribute
	{
		public enum Package
		{
			Builtin = 0,
			Root = 1,
			BuiltinExtra = 2
		}

		public ReloadAttribute(string[] paths, global::UnityEngine.Rendering.ReloadAttribute.Package package = global::UnityEngine.Rendering.ReloadAttribute.Package.Root)
		{
		}

		public ReloadAttribute(string path, global::UnityEngine.Rendering.ReloadAttribute.Package package = global::UnityEngine.Rendering.ReloadAttribute.Package.Root)
			: this(new string[1] { path }, package)
		{
		}

		public ReloadAttribute(string pathFormat, int rangeMin, int rangeMax, global::UnityEngine.Rendering.ReloadAttribute.Package package = global::UnityEngine.Rendering.ReloadAttribute.Package.Root)
		{
		}
	}
}
