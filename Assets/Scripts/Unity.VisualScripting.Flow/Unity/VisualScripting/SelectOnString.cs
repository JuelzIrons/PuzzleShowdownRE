namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitTitle("Select On String")]
	[global::Unity.VisualScripting.UnitShortTitle("Select")]
	[global::Unity.VisualScripting.UnitSubtitle("On String")]
	[global::Unity.VisualScripting.UnitOrder(7)]
	public class SelectOnString : global::Unity.VisualScripting.SelectUnit<string>
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
