namespace Unity.VisualScripting
{
	public sealed class ReflectionFieldAccessor : global::Unity.VisualScripting.IOptimizedAccessor
	{
		private readonly global::System.Reflection.FieldInfo fieldInfo;

		public ReflectionFieldAccessor(global::System.Reflection.FieldInfo fieldInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.Ensure.That("fieldInfo").IsNotNull(fieldInfo);
			}
			this.fieldInfo = fieldInfo;
		}

		public void Compile()
		{
		}

		public object GetValue(object target)
		{
			return fieldInfo.GetValue(target);
		}

		public void SetValue(object target, object value)
		{
			fieldInfo.SetValue(target, value);
		}
	}
}
