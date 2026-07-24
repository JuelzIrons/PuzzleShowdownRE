namespace Newtonsoft.Json.Linq
{
	public class JsonMergeSettings
	{
		private global::Newtonsoft.Json.Linq.MergeArrayHandling _mergeArrayHandling;

		private global::Newtonsoft.Json.Linq.MergeNullValueHandling _mergeNullValueHandling;

		private global::System.StringComparison _propertyNameComparison;

		public global::Newtonsoft.Json.Linq.MergeArrayHandling MergeArrayHandling
		{
			get
			{
				return _mergeArrayHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.Linq.MergeArrayHandling.Concat || value > global::Newtonsoft.Json.Linq.MergeArrayHandling.Merge)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_mergeArrayHandling = value;
			}
		}

		public global::Newtonsoft.Json.Linq.MergeNullValueHandling MergeNullValueHandling
		{
			get
			{
				return _mergeNullValueHandling;
			}
			set
			{
				if (value < global::Newtonsoft.Json.Linq.MergeNullValueHandling.Ignore || value > global::Newtonsoft.Json.Linq.MergeNullValueHandling.Merge)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_mergeNullValueHandling = value;
			}
		}

		public global::System.StringComparison PropertyNameComparison
		{
			get
			{
				return _propertyNameComparison;
			}
			set
			{
				if (value < global::System.StringComparison.CurrentCulture || value > global::System.StringComparison.OrdinalIgnoreCase)
				{
					throw new global::System.ArgumentOutOfRangeException("value");
				}
				_propertyNameComparison = value;
			}
		}

		public JsonMergeSettings()
		{
			_propertyNameComparison = global::System.StringComparison.Ordinal;
		}
	}
}
