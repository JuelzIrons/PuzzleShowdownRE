namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SpecialUnit]
	[global::Unity.VisualScripting.UnitTitle("Node script is missing!")]
	[global::Unity.VisualScripting.UnitShortTitle("Missing Script!")]
	public sealed class MissingType : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.Serialize]
		public string formerType { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		public string formerValue { get; private set; }

		protected override void Definition()
		{
		}
	}
}
