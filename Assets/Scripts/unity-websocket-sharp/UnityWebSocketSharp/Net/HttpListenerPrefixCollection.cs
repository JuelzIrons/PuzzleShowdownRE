namespace UnityWebSocketSharp.Net
{
	internal class HttpListenerPrefixCollection : global::System.Collections.Generic.ICollection<string>, global::System.Collections.Generic.IEnumerable<string>, global::System.Collections.IEnumerable
	{
		private global::UnityWebSocketSharp.Net.HttpListener _listener;

		private global::System.Collections.Generic.List<string> _prefixes;

		public int Count => _prefixes.Count;

		public bool IsReadOnly => false;

		public bool IsSynchronized => false;

		internal HttpListenerPrefixCollection(global::UnityWebSocketSharp.Net.HttpListener listener)
		{
			_listener = listener;
			_prefixes = new global::System.Collections.Generic.List<string>();
		}

		public void Add(string uriPrefix)
		{
			_listener.CheckDisposed();
			global::UnityWebSocketSharp.Net.HttpListenerPrefix.CheckPrefix(uriPrefix);
			if (!_prefixes.Contains(uriPrefix))
			{
				if (_listener.IsListening)
				{
					global::UnityWebSocketSharp.Net.EndPointManager.AddPrefix(uriPrefix, _listener);
				}
				_prefixes.Add(uriPrefix);
			}
		}

		public void Clear()
		{
			_listener.CheckDisposed();
			if (_listener.IsListening)
			{
				global::UnityWebSocketSharp.Net.EndPointManager.RemoveListener(_listener);
			}
			_prefixes.Clear();
		}

		public bool Contains(string uriPrefix)
		{
			_listener.CheckDisposed();
			if (uriPrefix == null)
			{
				throw new global::System.ArgumentNullException("uriPrefix");
			}
			return _prefixes.Contains(uriPrefix);
		}

		public void CopyTo(string[] array, int offset)
		{
			_listener.CheckDisposed();
			_prefixes.CopyTo(array, offset);
		}

		public global::System.Collections.Generic.IEnumerator<string> GetEnumerator()
		{
			return _prefixes.GetEnumerator();
		}

		public bool Remove(string uriPrefix)
		{
			_listener.CheckDisposed();
			if (uriPrefix == null)
			{
				throw new global::System.ArgumentNullException("uriPrefix");
			}
			if (!_prefixes.Contains(uriPrefix))
			{
				return false;
			}
			if (_listener.IsListening)
			{
				global::UnityWebSocketSharp.Net.EndPointManager.RemovePrefix(uriPrefix, _listener);
			}
			return _prefixes.Remove(uriPrefix);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return _prefixes.GetEnumerator();
		}
	}
}
