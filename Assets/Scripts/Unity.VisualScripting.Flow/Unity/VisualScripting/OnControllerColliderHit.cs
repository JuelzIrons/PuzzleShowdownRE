namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Physics")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.CharacterController))]
	public sealed class OnControllerColliderHit : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.ControllerColliderHit>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnControllerColliderHitMessageListener);

		protected override string hookName => "OnControllerColliderHit";

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput collider { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput controller { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput moveDirection { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput moveLength { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput normal { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput point { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput data { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			collider = ValueOutput<global::UnityEngine.Collider>("collider");
			controller = ValueOutput<global::UnityEngine.CharacterController>("controller");
			moveDirection = ValueOutput<global::UnityEngine.Vector3>("moveDirection");
			moveLength = ValueOutput<float>("moveLength");
			normal = ValueOutput<global::UnityEngine.Vector3>("normal");
			point = ValueOutput<global::UnityEngine.Vector3>("point");
			data = ValueOutput<global::UnityEngine.ControllerColliderHit>("data");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.ControllerColliderHit hitData)
		{
			flow.SetValue(collider, hitData.collider);
			flow.SetValue(controller, hitData.controller);
			flow.SetValue(moveDirection, hitData.moveDirection);
			flow.SetValue(moveLength, hitData.moveLength);
			flow.SetValue(normal, hitData.normal);
			flow.SetValue(point, hitData.point);
			flow.SetValue(data, hitData);
		}
	}
}
