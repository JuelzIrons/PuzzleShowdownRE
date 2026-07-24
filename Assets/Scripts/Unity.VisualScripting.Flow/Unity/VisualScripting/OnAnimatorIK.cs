namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Animation")]
	public sealed class OnAnimatorIK : global::Unity.VisualScripting.GameObjectEventUnit<int>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.AnimatorMessageListener);

		protected override string hookName => "OnAnimatorIK";

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput layerIndex { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			layerIndex = ValueOutput<int>("layerIndex");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, int layerIndex)
		{
			flow.SetValue(this.layerIndex, layerIndex);
		}
	}
}
