namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("Wait For Seconds")]
	[global::Unity.VisualScripting.UnitOrder(1)]
	public class WaitForSecondsUnit : global::Unity.VisualScripting.WaitUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Delay")]
		public global::Unity.VisualScripting.ValueInput seconds { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Unscaled")]
		public global::Unity.VisualScripting.ValueInput unscaledTime { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			seconds = ValueInput("seconds", 0f);
			unscaledTime = ValueInput("unscaledTime", @default: false);
			Requirement(seconds, base.enter);
			Requirement(unscaledTime, base.enter);
		}

		protected override global::System.Collections.IEnumerator Await(global::Unity.VisualScripting.Flow flow)
		{
			float value = flow.GetValue<float>(seconds);
			if (flow.GetValue<bool>(unscaledTime))
			{
				yield return new global::UnityEngine.WaitForSecondsRealtime(value);
			}
			else
			{
				yield return new global::UnityEngine.WaitForSeconds(value);
			}
			yield return base.exit;
		}
	}
}
