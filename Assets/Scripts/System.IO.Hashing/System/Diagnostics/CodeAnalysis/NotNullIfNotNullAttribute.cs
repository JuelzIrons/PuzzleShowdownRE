namespace System.Diagnostics.CodeAnalysis
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Parameter | global::System.AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	internal sealed class NotNullIfNotNullAttribute : global::System.Attribute
	{
		public string ParameterName { get; }

		public NotNullIfNotNullAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}
	}
}
