namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("Wait While")]
	[global::Unity.VisualScripting.UnitShortTitle("Wait While")]
	[global::Unity.VisualScripting.UnitOrder(3)]
	public class WaitWhileUnit : global::Unity.VisualScripting.WaitUnit
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
			yield return new global::UnityEngine.WaitWhile(() => flow.GetValue<bool>(condition));
			yield return base.exit;
		}
	}
}
