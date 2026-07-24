namespace Unity.VisualScripting
{
	public sealed class GraphPointerException : global::System.Exception
	{
		public global::Unity.VisualScripting.GraphPointer pointer { get; }

		public GraphPointerException(string message, global::Unity.VisualScripting.GraphPointer pointer)
			: base(message + "\n" + pointer)
		{
			this.pointer = pointer;
		}
	}
}
