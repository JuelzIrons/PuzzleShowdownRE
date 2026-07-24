namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Physics")]
	public sealed class OnJointBreak : global::Unity.VisualScripting.GameObjectEventUnit<float>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnJointBreakMessageListener);

		protected override string hookName => "OnJointBreak";

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput breakForce { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			breakForce = ValueOutput<float>("breakForce");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, float breakForce)
		{
			flow.SetValue(this.breakForce, breakForce);
		}
	}
}
