namespace Newtonsoft.Json
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonDictionaryAttribute : global::Newtonsoft.Json.JsonContainerAttribute
	{
		public JsonDictionaryAttribute()
		{
		}

		public JsonDictionaryAttribute(string id)
			: base(id)
		{
		}
	}
}
