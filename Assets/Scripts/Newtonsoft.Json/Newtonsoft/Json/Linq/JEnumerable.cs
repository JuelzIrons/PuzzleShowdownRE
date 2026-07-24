namespace Newtonsoft.Json.Linq
{
	public readonly struct JEnumerable<T> : global::Newtonsoft.Json.Linq.IJEnumerable<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable, global::System.IEquatable<global::Newtonsoft.Json.Linq.JEnumerable<T>> where T : global::Newtonsoft.Json.Linq.JToken
	{
		public static readonly global::Newtonsoft.Json.Linq.JEnumerable<T> Empty = new global::Newtonsoft.Json.Linq.JEnumerable<T>(global::System.Linq.Enumerable.Empty<T>());

		private readonly global::System.Collections.Generic.IEnumerable<T> _enumerable;

		public global::Newtonsoft.Json.Linq.IJEnumerable<global::Newtonsoft.Json.Linq.JToken> this[object key]
		{
			get
			{
				if (_enumerable == null)
				{
					return global::Newtonsoft.Json.Linq.JEnumerable<global::Newtonsoft.Json.Linq.JToken>.Empty;
				}
				return new global::Newtonsoft.Json.Linq.JEnumerable<global::Newtonsoft.Json.Linq.JToken>(_enumerable.Values<T, global::Newtonsoft.Json.Linq.JToken>(key));
			}
		}

		public JEnumerable(global::System.Collections.Generic.IEnumerable<T> enumerable)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			_enumerable = enumerable;
		}

		public global::System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			return ((global::System.Collections.Generic.IEnumerable<T>)(_enumerable ?? ((object)Empty))).GetEnumerator();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool Equals(global::Newtonsoft.Json.Linq.JEnumerable<T> other)
		{
			return object.Equals(_enumerable, other._enumerable);
		}

		public override bool Equals(object? obj)
		{
			if (obj is global::Newtonsoft.Json.Linq.JEnumerable<T> other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			if (_enumerable == null)
			{
				return 0;
			}
			return _enumerable.GetHashCode();
		}
	}
}
