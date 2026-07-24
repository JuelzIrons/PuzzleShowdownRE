namespace Unity.VisualScripting
{
	internal class ExtensionMethodCache
	{
		internal readonly global::System.Reflection.MethodInfo[] Cache;

		internal ExtensionMethodCache()
		{
			Cache = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.SelectMany(global::System.Linq.Enumerable.Where(global::Unity.VisualScripting.RuntimeCodebase.types, (global::System.Type type) => type.IsStatic() && !type.IsGenericType && !type.IsNested), (global::System.Type type) => type.GetMethods()), (global::System.Reflection.MethodInfo method) => method.IsExtension()));
		}
	}
}
