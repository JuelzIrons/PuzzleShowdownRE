namespace Unity.VisualScripting
{
	public sealed class ReflectionPropertyAccessor : global::Unity.VisualScripting.IOptimizedAccessor
	{
		private readonly global::System.Reflection.PropertyInfo propertyInfo;

		public ReflectionPropertyAccessor(global::System.Reflection.PropertyInfo propertyInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.Ensure.That("propertyInfo").IsNotNull(propertyInfo);
			}
			this.propertyInfo = propertyInfo;
		}

		public void Compile()
		{
		}

		public object GetValue(object target)
		{
			return propertyInfo.GetValue(target, null);
		}

		public void SetValue(object target, object value)
		{
			propertyInfo.SetValue(target, value, null);
		}
	}
}
