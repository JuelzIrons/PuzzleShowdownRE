namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events")]
	[global::Unity.VisualScripting.UnitTitle("UnityEvent")]
	[global::Unity.VisualScripting.UnitOrder(2)]
	[global::System.ComponentModel.DisplayName("Visual Scripting Unity Event")]
	public sealed class BoltUnityEvent : global::Unity.VisualScripting.MachineEventUnit<string>
	{
		protected override string hookName => "UnityEvent";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput name { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			name = ValueInput("name", string.Empty);
		}

		protected override bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, string name)
		{
			return global::Unity.VisualScripting.EventUnit<string>.CompareNames(flow, this.name, name);
		}
	}
}
