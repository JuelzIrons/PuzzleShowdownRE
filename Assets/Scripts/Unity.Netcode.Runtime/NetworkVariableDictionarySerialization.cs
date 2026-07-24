internal class NetworkVariableDictionarySerialization<TKey, TVal> where TKey : global::System.IEquatable<TKey>
{
	internal static bool GenericEqualsDictionary(ref global::System.Collections.Generic.Dictionary<TKey, TVal> a, ref global::System.Collections.Generic.Dictionary<TKey, TVal> b)
	{
		if (a == null != (b == null))
		{
			return false;
		}
		if (a == null)
		{
			return true;
		}
		if (a.Count != b.Count)
		{
			return false;
		}
		foreach (global::System.Collections.Generic.KeyValuePair<TKey, TVal> item in a)
		{
			if (!b.TryGetValue(item.Key, out var value))
			{
				return false;
			}
			TVal a2 = item.Value;
			if (!global::Unity.Netcode.NetworkVariableSerialization<TVal>.AreEqual(ref a2, ref value))
			{
				return false;
			}
		}
		return true;
	}
}
