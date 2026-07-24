namespace Unity.VisualScripting
{
	public static class EnumUtility
	{
		public static bool HasFlag(this global::System.Enum value, global::System.Enum flag)
		{
			long num = global::System.Convert.ToInt64(value);
			long num2 = global::System.Convert.ToInt64(flag);
			return (num & num2) == num2;
		}

		public static global::System.Collections.Generic.Dictionary<string, global::System.Enum> ValuesByNames(global::System.Type enumType, bool obsolete = false)
		{
			global::Unity.VisualScripting.Ensure.That("enumType").IsNotNull(enumType);
			global::System.Collections.Generic.IEnumerable<global::System.Reflection.FieldInfo> source = enumType.GetFields(global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
			if (!obsolete)
			{
				source = global::System.Linq.Enumerable.Where(source, (global::System.Reflection.FieldInfo f) => !f.IsDefined(typeof(global::System.ObsoleteAttribute), inherit: false));
			}
			return global::System.Linq.Enumerable.ToDictionary(source, (global::System.Reflection.FieldInfo f) => f.Name, (global::System.Reflection.FieldInfo f) => (global::System.Enum)f.GetValue(null));
		}

		public static global::System.Collections.Generic.Dictionary<string, T> ValuesByNames<T>(bool obsolete = false)
		{
			global::System.Collections.Generic.IEnumerable<global::System.Reflection.FieldInfo> source = typeof(T).GetFields(global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
			if (!obsolete)
			{
				source = global::System.Linq.Enumerable.Where(source, (global::System.Reflection.FieldInfo f) => !f.IsDefined(typeof(global::System.ObsoleteAttribute), inherit: false));
			}
			return global::System.Linq.Enumerable.ToDictionary(source, (global::System.Reflection.FieldInfo f) => f.Name, (global::System.Reflection.FieldInfo f) => (T)f.GetValue(null));
		}
	}
}
