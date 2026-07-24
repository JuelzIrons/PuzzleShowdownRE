namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("Wait For End of Frame")]
	[global::Unity.VisualScripting.UnitOrder(5)]
	public class WaitForEndOfFrameUnit : global::Unity.VisualScripting.WaitUnit
	{
		protected override global::System.Collections.IEnumerator Await(global::Unity.VisualScripting.Flow flow)
		{
			yield return new global::UnityEngine.WaitForEndOfFrame();
			yield return base.exit;
		}
	}
}
