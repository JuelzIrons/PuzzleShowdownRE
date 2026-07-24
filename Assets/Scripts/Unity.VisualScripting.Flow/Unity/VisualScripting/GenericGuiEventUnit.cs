namespace Unity.VisualScripting
{
	public abstract class GenericGuiEventUnit : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.EventSystems.BaseEventData>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput data { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			data = ValueOutput<global::UnityEngine.EventSystems.BaseEventData>("data");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.EventSystems.BaseEventData data)
		{
			flow.SetValue(this.data, data);
		}
	}
}
