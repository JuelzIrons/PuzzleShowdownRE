namespace Unity.VisualScripting
{
	public static class XGettable
	{
		public static object GetValue(this global::Unity.VisualScripting.IGettable gettable, global::System.Type type)
		{
			return global::Unity.VisualScripting.ConversionUtility.Convert(gettable.GetValue(), type);
		}

		public static T GetValue<T>(this global::Unity.VisualScripting.IGettable gettable)
		{
			return (T)gettable.GetValue(typeof(T));
		}
	}
}
