namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Animation")]
	[global::Unity.VisualScripting.UnitShortTitle("Animation Event")]
	[global::Unity.VisualScripting.UnitTitle("Named Animation Event")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.AnimationClip))]
	[global::System.ComponentModel.DisplayName("Visual Scripting Named Animation Event")]
	public sealed class BoltNamedAnimationEvent : global::Unity.VisualScripting.MachineEventUnit<global::UnityEngine.AnimationEvent>
	{
		protected override string hookName => "AnimationEvent";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput name { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Float")]
		public global::Unity.VisualScripting.ValueOutput floatParameter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Integer")]
		public global::Unity.VisualScripting.ValueOutput intParameter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Object")]
		public global::Unity.VisualScripting.ValueOutput objectReferenceParameter { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			name = ValueInput("name", string.Empty);
			floatParameter = ValueOutput<float>("floatParameter");
			intParameter = ValueOutput<int>("intParameter");
			objectReferenceParameter = ValueOutput<global::UnityEngine.GameObject>("objectReferenceParameter");
		}

		protected override bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, global::UnityEngine.AnimationEvent animationEvent)
		{
			return global::Unity.VisualScripting.EventUnit<global::UnityEngine.AnimationEvent>.CompareNames(flow, name, animationEvent.stringParameter);
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.AnimationEvent animationEvent)
		{
			flow.SetValue(floatParameter, animationEvent.floatParameter);
			flow.SetValue(intParameter, animationEvent.intParameter);
			flow.SetValue(objectReferenceParameter, animationEvent.objectReferenceParameter);
		}
	}
}
