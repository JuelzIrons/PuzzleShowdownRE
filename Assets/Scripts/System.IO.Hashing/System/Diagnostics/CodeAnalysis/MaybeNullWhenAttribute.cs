namespace System.Diagnostics.CodeAnalysis
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : global::System.Attribute
	{
		public bool ReturnValue { get; }

		public MaybeNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
	}
}
