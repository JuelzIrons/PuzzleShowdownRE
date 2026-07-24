namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SpecialUnit]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.Self")]
	[global::Unity.VisualScripting.RenamedFrom("Unity.VisualScripting.Self")]
	public sealed class This : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.PortLabel("This")]
		public global::Unity.VisualScripting.ValueOutput self { get; private set; }

		protected override void Definition()
		{
			self = ValueOutput("self", Result).PredictableIf(IsPredictable);
		}

		private global::UnityEngine.GameObject Result(global::Unity.VisualScripting.Flow flow)
		{
			return flow.stack.self;
		}

		private bool IsPredictable(global::Unity.VisualScripting.Flow flow)
		{
			return flow.stack.self != null;
		}
	}
}
