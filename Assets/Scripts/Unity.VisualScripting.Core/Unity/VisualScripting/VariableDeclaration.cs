namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	public sealed class VariableDeclaration
	{
		[global::Unity.VisualScripting.Serialize]
		public string name { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Value]
		public object value { get; set; }

		[global::Unity.VisualScripting.Serialize]
		public global::Unity.VisualScripting.SerializableType typeHandle { get; set; }

		[global::System.Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public VariableDeclaration()
		{
		}

		public VariableDeclaration(string name, object value)
		{
			this.name = name;
			this.value = value;
		}
	}
}
