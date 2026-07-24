namespace Newtonsoft.Json.Serialization
{
	internal static class CachedAttributeGetter<T> where T : global::System.Attribute
	{
		private static readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<object, T?> TypeAttributeCache = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<object, T>(global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<T>);

		public static T? GetAttribute(object type)
		{
			return TypeAttributeCache.Get(type);
		}
	}
}
