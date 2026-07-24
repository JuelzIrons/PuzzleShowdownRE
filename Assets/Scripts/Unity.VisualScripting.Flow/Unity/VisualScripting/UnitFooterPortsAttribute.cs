namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class UnitFooterPortsAttribute : global::System.Attribute
	{
		public bool ControlInputs { get; set; }

		public bool ControlOutputs { get; set; }

		public bool ValueInputs { get; set; } = true;

		public bool ValueOutputs { get; set; } = true;
	}
}
