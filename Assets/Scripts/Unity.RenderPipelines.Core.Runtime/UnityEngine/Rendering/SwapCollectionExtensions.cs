namespace UnityEngine.Rendering
{
	public static class SwapCollectionExtensions
	{
		[global::JetBrains.Annotations.CollectionAccess(global::JetBrains.Annotations.CollectionAccessType.ModifyExistingContent)]
		[global::JetBrains.Annotations.MustUseReturnValue]
		public static bool TrySwap<TValue>([global::System.Diagnostics.CodeAnalysis.DisallowNull] this global::System.Collections.Generic.IList<TValue> list, int from, int to, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(false)] out global::System.Exception error)
		{
			error = null;
			if (list == null)
			{
				error = new global::System.ArgumentNullException("list");
			}
			else
			{
				if (from < 0 || from >= list.Count)
				{
					error = new global::System.ArgumentOutOfRangeException("from");
				}
				if (to < 0 || to >= list.Count)
				{
					error = new global::System.ArgumentOutOfRangeException("to");
				}
			}
			if (error != null)
			{
				return false;
			}
			TValue val = list[from];
			TValue val2 = list[to];
			TValue val3 = (list[to] = val);
			val3 = (list[from] = val2);
			return true;
		}
	}
}
