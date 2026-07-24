namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Lifecycle")]
	[global::Unity.VisualScripting.UnitOrder(7)]
	public sealed class OnDestroy : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "OnDestroy";

		public override void StopListening(global::Unity.VisualScripting.GraphStack stack)
		{
		}

		private protected override void InternalTrigger(global::Unity.VisualScripting.GraphReference reference, global::Unity.VisualScripting.EmptyEventArgs args)
		{
			base.InternalTrigger(reference, args);
			using global::Unity.VisualScripting.GraphStack stack = reference.ToStackPooled();
			base.StopListening(stack);
		}
	}
}
