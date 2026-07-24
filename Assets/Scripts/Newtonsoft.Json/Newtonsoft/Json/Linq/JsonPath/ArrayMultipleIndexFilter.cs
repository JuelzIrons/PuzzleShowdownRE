namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class ArrayMultipleIndexFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		internal global::System.Collections.Generic.List<int> Indexes;

		public ArrayMultipleIndexFilter(global::System.Collections.Generic.List<int> indexes)
		{
			Indexes = indexes;
		}

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			foreach (global::Newtonsoft.Json.Linq.JToken t in current)
			{
				foreach (int index in Indexes)
				{
					global::Newtonsoft.Json.Linq.JToken tokenIndex = global::Newtonsoft.Json.Linq.JsonPath.PathFilter.GetTokenIndex(t, settings, index);
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
			}
		}
	}
}
