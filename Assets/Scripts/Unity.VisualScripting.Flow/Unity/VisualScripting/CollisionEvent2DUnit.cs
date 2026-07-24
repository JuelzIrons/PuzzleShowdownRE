namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Physics 2D")]
	public abstract class CollisionEvent2DUnit : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.Collision2D>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput collider { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput contacts { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput relativeVelocity { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput enabled { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput data { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			collider = ValueOutput<global::UnityEngine.Collider2D>("collider");
			contacts = ValueOutput<global::UnityEngine.ContactPoint2D[]>("contacts");
			relativeVelocity = ValueOutput<global::UnityEngine.Vector2>("relativeVelocity");
			enabled = ValueOutput<bool>("enabled");
			data = ValueOutput<global::UnityEngine.Collision2D>("data");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.Collision2D collisionData)
		{
			flow.SetValue(collider, collisionData.collider);
			flow.SetValue(contacts, collisionData.contacts);
			flow.SetValue(relativeVelocity, collisionData.relativeVelocity);
			flow.SetValue(enabled, collisionData.enabled);
			flow.SetValue(data, collisionData);
		}
	}
}
