namespace Newtonsoft.Json.Linq.JsonPath
{
	internal class RootFilter : global::Newtonsoft.Json.Linq.JsonPath.PathFilter
	{
		public static readonly global::Newtonsoft.Json.Linq.JsonPath.RootFilter Instance = new global::Newtonsoft.Json.Linq.JsonPath.RootFilter();

		private RootFilter()
		{
		}

		public override global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> ExecuteFilter(global::Newtonsoft.Json.Linq.JToken root, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> current, global::Newtonsoft.Json.Linq.JsonSelectSettings? settings)
		{
			return new global::Newtonsoft.Json.Linq.JToken[1] { root };
		}
	}
}
