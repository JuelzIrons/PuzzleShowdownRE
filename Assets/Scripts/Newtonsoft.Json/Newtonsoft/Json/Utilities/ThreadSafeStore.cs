namespace Newtonsoft.Json.Utilities
{
	internal class ThreadSafeStore<TKey, TValue> where TKey : notnull
	{
		private readonly global::System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue> _concurrentStore;

		private readonly global::System.Func<TKey, TValue> _creator;

		public ThreadSafeStore(global::System.Func<TKey, TValue> creator)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(creator, "creator");
			_creator = creator;
			_concurrentStore = new global::System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue>();
		}

		public TValue Get(TKey key)
		{
			return _concurrentStore.GetOrAdd(key, _creator);
		}
	}
}
