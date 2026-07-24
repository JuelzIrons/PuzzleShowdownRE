namespace Unity.Services.Core.Internal
{
	internal class DependencyTreeSortFailedException : global::System.Exception
	{
		public DependencyTreeSortFailedException(global::Unity.Services.Core.Internal.DependencyTree tree, global::System.Collections.Generic.ICollection<int> target)
			: base(CreateExceptionMessage(tree, target))
		{
		}

		public DependencyTreeSortFailedException(global::Unity.Services.Core.Internal.DependencyTree tree, global::System.Collections.Generic.ICollection<int> target, global::System.Exception inner)
			: base(CreateExceptionMessage(tree, target, inner), inner)
		{
		}

		private static string CreateExceptionMessage(global::Unity.Services.Core.Internal.DependencyTree tree, global::System.Collections.Generic.ICollection<int> target, global::System.Exception inner = null)
		{
			string text = tree.ToJson(target);
			return string.Concat("Failed to sort tree! It is likely there is a missing required dependency:\n" + text, (inner != null) ? ("\n Error: " + inner.Message) : string.Empty);
		}
	}
}
