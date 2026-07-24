namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Physics")]
	public abstract class CollisionEventUnit : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.Collision>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput collider { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput contacts { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput impulse { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput relativeVelocity { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput data { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			collider = ValueOutput<global::UnityEngine.Collider>("collider");
			contacts = ValueOutput<global::UnityEngine.ContactPoint[]>("contacts");
			impulse = ValueOutput<global::UnityEngine.Vector3>("impulse");
			relativeVelocity = ValueOutput<global::UnityEngine.Vector3>("relativeVelocity");
			data = ValueOutput<global::UnityEngine.Collision>("data");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.Collision collision)
		{
			flow.SetValue(collider, collision.collider);
			flow.SetValue(contacts, collision.contacts);
			flow.SetValue(impulse, collision.impulse);
			flow.SetValue(relativeVelocity, collision.relativeVelocity);
			flow.SetValue(data, collision);
		}
	}
}
