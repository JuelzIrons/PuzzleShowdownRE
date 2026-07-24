namespace Unity.VisualScripting
{
	public class ReflectionInvoker : global::Unity.VisualScripting.IOptimizedInvoker
	{
		private readonly global::System.Reflection.MethodInfo methodInfo;

		private static readonly object[] EmptyObjects = new object[0];

		public ReflectionInvoker(global::System.Reflection.MethodInfo methodInfo)
		{
			if (global::Unity.VisualScripting.OptimizedReflection.safeMode)
			{
				global::Unity.VisualScripting.Ensure.That("methodInfo").IsNotNull(methodInfo);
			}
			this.methodInfo = methodInfo;
		}

		public void Compile()
		{
		}

		public object Invoke(object target, params object[] args)
		{
			return methodInfo.Invoke(target, args);
		}

		public object Invoke(object target)
		{
			return methodInfo.Invoke(target, EmptyObjects);
		}

		public object Invoke(object target, object arg0)
		{
			return methodInfo.Invoke(target, new object[1] { arg0 });
		}

		public object Invoke(object target, object arg0, object arg1)
		{
			return methodInfo.Invoke(target, new object[2] { arg0, arg1 });
		}

		public object Invoke(object target, object arg0, object arg1, object arg2)
		{
			return methodInfo.Invoke(target, new object[3] { arg0, arg1, arg2 });
		}

		public object Invoke(object target, object arg0, object arg1, object arg2, object arg3)
		{
			return methodInfo.Invoke(target, new object[4] { arg0, arg1, arg2, arg3 });
		}

		public object Invoke(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			return methodInfo.Invoke(target, new object[5] { arg0, arg1, arg2, arg3, arg4 });
		}

		public global::System.Type[] GetParameterTypes()
		{
			return global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(methodInfo.GetParameters(), (global::System.Reflection.ParameterInfo pi) => pi.ParameterType));
		}
	}
}
