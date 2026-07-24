namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitTitle("Switch On String")]
	[global::Unity.VisualScripting.UnitShortTitle("Switch")]
	[global::Unity.VisualScripting.UnitSubtitle("On String")]
	[global::Unity.VisualScripting.UnitOrder(4)]
	public class SwitchOnString : global::Unity.VisualScripting.SwitchUnit<string>
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Ignore Case")]
		[global::Unity.VisualScripting.InspectorToggleLeft]
		public bool ignoreCase { get; set; }

		protected override bool Matches(string a, string b)
		{
			if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
			{
				return true;
			}
			return string.Equals(a, b, ignoreCase ? global::System.StringComparison.OrdinalIgnoreCase : global::System.StringComparison.Ordinal);
		}
	}
}
