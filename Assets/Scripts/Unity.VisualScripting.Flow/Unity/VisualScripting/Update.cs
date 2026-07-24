namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Lifecycle")]
	[global::Unity.VisualScripting.UnitOrder(3)]
	[global::Unity.VisualScripting.UnitTitle("On Update")]
	public sealed class Update : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "Update";
	}
}
