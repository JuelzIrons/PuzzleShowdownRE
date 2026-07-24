namespace Unity.Multiplayer.Tools.Common
{
	internal static class ListUtil
	{
		public static void Resize<T>(this global::System.Collections.Generic.List<T> list, int size, T element = default(T))
		{
			int count = list.Count;
			int num = size - count;
			if (num < 0)
			{
				list.RemoveRange(size, count - size);
			}
			else if (num > 0)
			{
				if (size > list.Capacity)
				{
					list.Capacity = size;
				}
				for (int i = 0; i < num; i++)
				{
					list.Add(element);
				}
			}
		}

		public static void Resize<T>(this global::System.Collections.Generic.List<T> list, int size, global::System.Func<T> generator)
		{
			int count = list.Count;
			int num = size - count;
			if (num < 0)
			{
				list.RemoveRange(size, count - size);
			}
			else if (num > 0)
			{
				if (size > list.Capacity)
				{
					list.Capacity = size;
				}
				for (int i = 0; i < num; i++)
				{
					list.Add(generator());
				}
			}
		}
	}
}
