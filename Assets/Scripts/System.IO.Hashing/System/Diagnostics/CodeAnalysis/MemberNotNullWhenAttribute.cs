namespace System.Diagnostics.CodeAnalysis
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Method | global::System.AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullWhenAttribute : global::System.Attribute
	{
		public bool ReturnValue { get; }

		public string[] Members { get; }

		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			ReturnValue = returnValue;
			Members = new string[1] { member };
		}

		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			ReturnValue = returnValue;
			Members = members;
		}
	}
}
