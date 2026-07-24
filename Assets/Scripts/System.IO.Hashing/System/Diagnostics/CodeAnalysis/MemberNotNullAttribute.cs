namespace System.Diagnostics.CodeAnalysis
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Method | global::System.AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : global::System.Attribute
	{
		public string[] Members { get; }

		public MemberNotNullAttribute(string member)
		{
			Members = new string[1] { member };
		}

		public MemberNotNullAttribute(params string[] members)
		{
			Members = members;
		}
	}
}
