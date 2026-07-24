namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.UnitOrder(21)]
	public sealed class OnMove : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.EventSystems.AxisEventData>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnMoveMessageListener);

		protected override string hookName => "OnMove";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput data { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			data = ValueOutput<global::UnityEngine.EventSystems.AxisEventData>("data");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.EventSystems.AxisEventData data)
		{
			flow.SetValue(this.data, data);
		}
	}
}
