namespace System.Diagnostics.CodeAnalysis
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Parameter, AllowMultiple = false)]
	internal sealed class NotNullWhenAttribute : global::System.Attribute
	{
		public bool ReturnValue { get; }

		public NotNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
	}
}
