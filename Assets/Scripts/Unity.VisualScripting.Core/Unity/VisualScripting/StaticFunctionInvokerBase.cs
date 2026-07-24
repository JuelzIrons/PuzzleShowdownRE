namespace Unity.VisualScripting
{
	public abstract class StaticFunctionInvokerBase<TResult> : global::Unity.VisualScripting.StaticInvokerBase
	{
		protected StaticFunctionInvokerBase(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode && methodInfo.ReturnType != typeof(TResult))
			{
				throw new global::System.ArgumentException("Return type of method info doesn't match generic type.", "methodInfo");
			}
		}
	}
}
