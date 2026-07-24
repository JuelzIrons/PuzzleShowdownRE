namespace Unity.VisualScripting
{
	public abstract class InstanceFunctionInvokerBase<TTarget, TResult> : global::Unity.VisualScripting.InstanceInvokerBase<TTarget>
	{
		protected InstanceFunctionInvokerBase(global::System.Reflection.MethodInfo methodInfo)
			: base(methodInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode && methodInfo.ReturnType != typeof(TResult))
			{
				throw new global::System.ArgumentException("Return type of method info doesn't match generic type.", "methodInfo");
			}
		}
	}
}
