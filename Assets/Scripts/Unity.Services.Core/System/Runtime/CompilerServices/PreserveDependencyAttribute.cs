namespace System.Runtime.CompilerServices
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Constructor | global::System.AttributeTargets.Method | global::System.AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class PreserveDependencyAttribute : global::System.Attribute
	{
		public string Condition { get; set; }

		public PreserveDependencyAttribute(string memberSignature)
		{
		}

		public PreserveDependencyAttribute(string memberSignature, string typeName)
		{
		}

		public PreserveDependencyAttribute(string memberSignature, string typeName, string assembly)
		{
		}
	}
}
