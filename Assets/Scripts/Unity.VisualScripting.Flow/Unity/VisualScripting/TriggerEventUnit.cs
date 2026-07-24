namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Physics")]
	public abstract class TriggerEventUnit : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.Collider>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput collider { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			collider = ValueOutput<global::UnityEngine.Collider>("collider");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.Collider other)
		{
			flow.SetValue(collider, other);
		}
	}
}
