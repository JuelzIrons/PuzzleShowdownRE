namespace System.Diagnostics.CodeAnalysis
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Parameter, Inherited = false)]
	internal class DoesNotReturnIfAttribute : global::System.Attribute
	{
		public bool ParameterValue { get; }

		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			ParameterValue = parameterValue;
		}
	}
}
