namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("Wait For Next Frame")]
	[global::Unity.VisualScripting.UnitOrder(4)]
	public class WaitForNextFrameUnit : global::Unity.VisualScripting.WaitUnit
	{
		protected override global::System.Collections.IEnumerator Await(global::Unity.VisualScripting.Flow flow)
		{
			yield return null;
			yield return base.exit;
		}
	}
}
