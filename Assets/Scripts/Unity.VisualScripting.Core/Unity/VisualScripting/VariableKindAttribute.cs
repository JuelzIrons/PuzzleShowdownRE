namespace Unity.VisualScripting
{
	[global::System.Obsolete("Set VariableKind via VariableDeclarations.Kind")]
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class VariableKindAttribute : global::System.Attribute
	{
		public global::Unity.VisualScripting.VariableKind kind { get; }

		public VariableKindAttribute(global::Unity.VisualScripting.VariableKind kind)
		{
			this.kind = kind;
		}
	}
}
