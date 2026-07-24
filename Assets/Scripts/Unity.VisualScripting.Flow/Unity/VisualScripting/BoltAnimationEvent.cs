namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Animation")]
	[global::Unity.VisualScripting.UnitShortTitle("Animation Event")]
	[global::Unity.VisualScripting.UnitTitle("Animation Event")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.AnimationClip))]
	[global::System.ComponentModel.DisplayName("Visual Scripting Animation Event")]
	public sealed class BoltAnimationEvent : global::Unity.VisualScripting.MachineEventUnit<global::UnityEngine.AnimationEvent>
	{
		protected override string hookName => "AnimationEvent";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("String")]
		public global::Unity.VisualScripting.ValueOutput stringParameter { get; private set; }

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
			stringParameter = ValueOutput<string>("stringParameter");
			floatParameter = ValueOutput<float>("floatParameter");
			intParameter = ValueOutput<int>("intParameter");
			objectReferenceParameter = ValueOutput<global::UnityEngine.Object>("objectReferenceParameter");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.AnimationEvent args)
		{
			flow.SetValue(stringParameter, args.stringParameter);
			flow.SetValue(floatParameter, args.floatParameter);
			flow.SetValue(intParameter, args.intParameter);
			flow.SetValue(objectReferenceParameter, args.objectReferenceParameter);
		}
	}
}
