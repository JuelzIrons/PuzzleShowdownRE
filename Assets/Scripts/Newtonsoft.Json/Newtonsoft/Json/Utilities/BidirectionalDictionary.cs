namespace Newtonsoft.Json.Utilities
{
	internal class BidirectionalDictionary<TFirst, TSecond> where TFirst : notnull where TSecond : notnull
	{
		private readonly global::System.Collections.Generic.IDictionary<TFirst, TSecond> _firstToSecond;

		private readonly global::System.Collections.Generic.IDictionary<TSecond, TFirst> _secondToFirst;

		private readonly string _duplicateFirstErrorMessage;

		private readonly string _duplicateSecondErrorMessage;

		public BidirectionalDictionary()
			: this((global::System.Collections.Generic.IEqualityComparer<TFirst>)global::System.Collections.Generic.EqualityComparer<TFirst>.Default, (global::System.Collections.Generic.IEqualityComparer<TSecond>)global::System.Collections.Generic.EqualityComparer<TSecond>.Default)
		{
		}

		public BidirectionalDictionary(global::System.Collections.Generic.IEqualityComparer<TFirst> firstEqualityComparer, global::System.Collections.Generic.IEqualityComparer<TSecond> secondEqualityComparer)
			: this(firstEqualityComparer, secondEqualityComparer, "Duplicate item already exists for '{0}'.", "Duplicate item already exists for '{0}'.")
		{
		}

		public BidirectionalDictionary(global::System.Collections.Generic.IEqualityComparer<TFirst> firstEqualityComparer, global::System.Collections.Generic.IEqualityComparer<TSecond> secondEqualityComparer, string duplicateFirstErrorMessage, string duplicateSecondErrorMessage)
		{
			_firstToSecond = new global::System.Collections.Generic.Dictionary<TFirst, TSecond>(firstEqualityComparer);
			_secondToFirst = new global::System.Collections.Generic.Dictionary<TSecond, TFirst>(secondEqualityComparer);
			_duplicateFirstErrorMessage = duplicateFirstErrorMessage;
			_duplicateSecondErrorMessage = duplicateSecondErrorMessage;
		}

		public void Set(TFirst first, TSecond second)
		{
			if (_firstToSecond.TryGetValue(first, out var value) && !value.Equals(second))
			{
				throw new global::System.ArgumentException(_duplicateFirstErrorMessage.FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, first));
			}
			if (_secondToFirst.TryGetValue(second, out var value2) && !value2.Equals(first))
			{
				throw new global::System.ArgumentException(_duplicateSecondErrorMessage.FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, second));
			}
			_firstToSecond.Add(first, second);
			_secondToFirst.Add(second, first);
		}

		public bool TryGetByFirst(TFirst first, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out TSecond? second)
		{
			return _firstToSecond.TryGetValue(first, out second);
		}

		public bool TryGetBySecond(TSecond second, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out TFirst? first)
		{
			return _secondToFirst.TryGetValue(second, out first);
		}
	}
}
