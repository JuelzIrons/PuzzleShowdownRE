namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Physics 2D")]
	public abstract class TriggerEvent2DUnit : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.Collider2D>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput collider { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			collider = ValueOutput<global::UnityEngine.Collider2D>("collider");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.Collider2D other)
		{
			flow.SetValue(collider, other);
		}
	}
}
