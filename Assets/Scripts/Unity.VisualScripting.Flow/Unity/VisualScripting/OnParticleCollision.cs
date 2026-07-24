namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Physics")]
	public sealed class OnParticleCollision : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.GameObject>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnParticleCollisionMessageListener);

		protected override string hookName => "OnParticleCollision";

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput other { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput collisionEvents { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			other = ValueOutput<global::UnityEngine.GameObject>("other");
			collisionEvents = ValueOutput<global::System.Collections.Generic.List<global::UnityEngine.ParticleCollisionEvent>>("collisionEvents");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.GameObject other)
		{
			flow.SetValue(this.other, other);
			global::System.Collections.Generic.List<global::UnityEngine.ParticleCollisionEvent> value = new global::System.Collections.Generic.List<global::UnityEngine.ParticleCollisionEvent>();
			global::UnityEngine.ParticlePhysicsExtensions.GetCollisionEvents(flow.stack.GetElementData<global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.GameObject>.Data>(this).target.GetComponent<global::UnityEngine.ParticleSystem>(), other, value);
			flow.SetValue(collisionEvents, value);
		}
	}
}
