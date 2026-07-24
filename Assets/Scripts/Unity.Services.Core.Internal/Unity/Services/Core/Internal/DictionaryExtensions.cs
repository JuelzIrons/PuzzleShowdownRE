namespace Unity.Services.Core.Internal
{
	internal static class DictionaryExtensions
	{
		public static TDictionary MergeNoOverride<TDictionary, TKey, TValue>(this TDictionary self, [global::JetBrains.Annotations.NotNull] global::System.Collections.Generic.IDictionary<TKey, TValue> dictionary) where TDictionary : global::System.Collections.Generic.IDictionary<TKey, TValue>
		{
			foreach (global::System.Collections.Generic.KeyValuePair<TKey, TValue> item in dictionary)
			{
				TKey key = item.Key;
				if (!self.ContainsKey(key))
				{
					TKey key2 = item.Key;
					TValue value = item.Value;
					self[key2] = value;
				}
			}
			return self;
		}

		public static TDictionary MergeAllowOverride<TDictionary, TKey, TValue>(this TDictionary self, [global::JetBrains.Annotations.NotNull] global::System.Collections.Generic.IDictionary<TKey, TValue> dictionary) where TDictionary : global::System.Collections.Generic.IDictionary<TKey, TValue>
		{
			foreach (global::System.Collections.Generic.KeyValuePair<TKey, TValue> item in dictionary)
			{
				self[item.Key] = item.Value;
			}
			return self;
		}

		public static bool ValueEquals<TKey, TValue>(this global::System.Collections.Generic.IDictionary<TKey, TValue> x, global::System.Collections.Generic.IDictionary<TKey, TValue> y)
		{
			return x.ValueEquals(y, global::System.Collections.Generic.EqualityComparer<TValue>.Default);
		}

		public static bool ValueEquals<TKey, TValue, TComparer>(this global::System.Collections.Generic.IDictionary<TKey, TValue> x, global::System.Collections.Generic.IDictionary<TKey, TValue> y, TComparer valueComparer) where TComparer : global::System.Collections.Generic.IEqualityComparer<TValue>
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null || x.Count != y.Count)
			{
				return false;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<TKey, TValue> item in x)
			{
				if (!y.TryGetValue(item.Key, out var value) || !valueComparer.Equals(item.Value, value))
				{
					return false;
				}
			}
			return true;
		}
	}
}
