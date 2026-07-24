namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("Wait Until")]
	[global::Unity.VisualScripting.UnitShortTitle("Wait Until")]
	[global::Unity.VisualScripting.UnitOrder(2)]
	public class WaitUntilUnit : global::Unity.VisualScripting.WaitUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput condition { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			condition = ValueInput<bool>("condition");
			Requirement(condition, base.enter);
		}

		protected override global::System.Collections.IEnumerator Await(global::Unity.VisualScripting.Flow flow)
		{
			yield return new global::UnityEngine.WaitUntil(() => flow.GetValue<bool>(condition));
			yield return base.exit;
		}
	}
}
