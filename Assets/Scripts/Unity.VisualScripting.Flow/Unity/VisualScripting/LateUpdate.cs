namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Lifecycle")]
	[global::Unity.VisualScripting.UnitOrder(5)]
	[global::Unity.VisualScripting.UnitTitle("On Late Update")]
	public sealed class LateUpdate : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "LateUpdate";
	}
}
