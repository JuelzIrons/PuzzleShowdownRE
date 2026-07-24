namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Lifecycle")]
	[global::Unity.VisualScripting.UnitOrder(4)]
	[global::Unity.VisualScripting.UnitTitle("On Fixed Update")]
	public sealed class FixedUpdate : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "FixedUpdate";
	}
}
