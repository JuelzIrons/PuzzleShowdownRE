namespace Unity.VisualScripting
{
	public abstract class UnitPortDefinition : global::Unity.VisualScripting.IUnitPortDefinition
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.InspectorDelayed]
		[global::Unity.VisualScripting.WarnBeforeEditing("Edit Port Key", "Changing the key of this definition will break any existing connection to this port. Are you sure you want to continue?", new object[] { null, "" })]
		public string key { get; set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		public string label { get; set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.InspectorTextArea]
		public string summary { get; set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		public bool hideLabel { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public virtual bool isValid => !string.IsNullOrEmpty(key);
	}
}
