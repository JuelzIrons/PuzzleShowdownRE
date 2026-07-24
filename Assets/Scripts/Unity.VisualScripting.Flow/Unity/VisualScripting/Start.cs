namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Lifecycle")]
	[global::Unity.VisualScripting.UnitOrder(2)]
	[global::Unity.VisualScripting.UnitTitle("On Start")]
	public sealed class Start : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "Start";
	}
}
