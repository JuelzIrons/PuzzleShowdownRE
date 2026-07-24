namespace Unity.VisualScripting
{
	public class FlexibleDictionary<TKey, TValue> : global::System.Collections.Generic.Dictionary<TKey, TValue>
	{
		public new TValue this[TKey key]
		{
			get
			{
				return base[key];
			}
			set
			{
				if (ContainsKey(key))
				{
					base[key] = value;
				}
				else
				{
					Add(key, value);
				}
			}
		}
	}
}
