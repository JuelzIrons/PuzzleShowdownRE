namespace Unity.VisualScripting
{
	public struct OverrideLayer<T> : global::System.IDisposable
	{
		public global::Unity.VisualScripting.OverrideStack<T> stack { get; }

		internal OverrideLayer(global::Unity.VisualScripting.OverrideStack<T> stack, T item)
		{
			global::Unity.VisualScripting.Ensure.That("stack").IsNotNull(stack);
			this.stack = stack;
			stack.BeginOverride(item);
		}

		public void Dispose()
		{
			stack.EndOverride();
		}
	}
}
