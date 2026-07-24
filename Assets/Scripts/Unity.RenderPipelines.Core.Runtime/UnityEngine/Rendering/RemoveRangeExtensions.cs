namespace UnityEngine.Rendering
{
	public static class RemoveRangeExtensions
	{
		[global::JetBrains.Annotations.CollectionAccess(global::JetBrains.Annotations.CollectionAccessType.ModifyExistingContent)]
		[global::JetBrains.Annotations.MustUseReturnValue]
		public static bool TryRemoveElementsInRange<TValue>([global::System.Diagnostics.CodeAnalysis.DisallowNull] this global::System.Collections.Generic.IList<TValue> list, int index, int count, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(false)] out global::System.Exception error)
		{
			try
			{
				if (list is global::System.Collections.Generic.List<TValue> list2)
				{
					list2.RemoveRange(index, count);
				}
				else
				{
					for (int num = count; num > 0; num--)
					{
						list.RemoveAt(index);
					}
				}
			}
			catch (global::System.Exception ex)
			{
				error = ex;
				return false;
			}
			error = null;
			return true;
		}
	}
}
