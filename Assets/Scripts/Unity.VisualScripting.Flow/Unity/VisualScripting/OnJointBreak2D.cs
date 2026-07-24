namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Physics 2D")]
	public sealed class OnJointBreak2D : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.Joint2D>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnJointBreak2DMessageListener);

		protected override string hookName => "OnJointBreak2D";

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput breakForce { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput breakTorque { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput connectedBody { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput reactionForce { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput reactionTorque { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput joint { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			breakForce = ValueOutput<float>("breakForce");
			breakTorque = ValueOutput<float>("breakTorque");
			connectedBody = ValueOutput<global::UnityEngine.Rigidbody2D>("connectedBody");
			reactionForce = ValueOutput<global::UnityEngine.Vector2>("reactionForce");
			reactionTorque = ValueOutput<float>("reactionTorque");
			joint = ValueOutput<global::UnityEngine.Joint2D>("joint");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.Joint2D joint)
		{
			flow.SetValue(breakForce, joint.breakForce);
			flow.SetValue(breakTorque, joint.breakTorque);
			flow.SetValue(connectedBody, joint.connectedBody);
			flow.SetValue(reactionForce, joint.reactionForce);
			flow.SetValue(reactionTorque, joint.reactionTorque);
			flow.SetValue(this.joint, joint);
		}
	}
}
