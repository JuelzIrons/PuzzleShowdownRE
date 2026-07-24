namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	public sealed class GraphGroup : global::Unity.VisualScripting.GraphElement<global::Unity.VisualScripting.IGraph>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public static readonly global::UnityEngine.Color defaultColor = new global::UnityEngine.Color(0f, 0f, 0f);

		[global::Unity.VisualScripting.Serialize]
		public global::UnityEngine.Rect position { get; set; }

		[global::Unity.VisualScripting.Serialize]
		public string label { get; set; } = "Group";

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectorTextArea(minLines = 1f, maxLines = 10f)]
		public string comment { get; set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		public global::UnityEngine.Color color { get; set; } = defaultColor;
	}
}
