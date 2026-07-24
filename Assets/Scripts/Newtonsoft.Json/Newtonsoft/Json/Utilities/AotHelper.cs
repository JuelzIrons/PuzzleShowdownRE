namespace Newtonsoft.Json.Utilities
{
	public static class AotHelper
	{
		private static bool s_alwaysFalse = global::System.DateTime.UtcNow.Year < 0;

		public static void Ensure(global::System.Action action)
		{
			if (IsFalse())
			{
				try
				{
					action();
				}
				catch (global::System.Exception innerException)
				{
					throw new global::System.InvalidOperationException("", innerException);
				}
			}
		}

		public static void EnsureType<T>() where T : new()
		{
			Ensure(delegate
			{
				new T();
			});
		}

		public static void EnsureList<T>()
		{
			Ensure(delegate
			{
				global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
				new global::System.Collections.Generic.HashSet<T>();
				new global::Newtonsoft.Json.Utilities.CollectionWrapper<T>((global::System.Collections.IList)list);
				new global::Newtonsoft.Json.Utilities.CollectionWrapper<T>((global::System.Collections.Generic.ICollection<T>)list);
			});
		}

		public static void EnsureDictionary<TKey, TValue>()
		{
			Ensure(delegate
			{
				new global::System.Collections.Generic.Dictionary<TKey, TValue>();
				new global::Newtonsoft.Json.Utilities.DictionaryWrapper<TKey, TValue>((global::System.Collections.IDictionary)null);
				new global::Newtonsoft.Json.Utilities.DictionaryWrapper<TKey, TValue>((global::System.Collections.Generic.IDictionary<TKey, TValue>)null);
				new global::Newtonsoft.Json.Serialization.DefaultContractResolver.EnumerableDictionaryWrapper<TKey, TValue>(null);
			});
		}

		public static bool IsFalse()
		{
			return s_alwaysFalse;
		}
	}
}
