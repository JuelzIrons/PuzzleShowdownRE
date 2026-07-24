namespace Newtonsoft.Json.Utilities
{
	internal class FSharpFunction
	{
		private readonly object? _instance;

		private readonly global::Newtonsoft.Json.Utilities.MethodCall<object?, object> _invoker;

		public FSharpFunction(object? instance, global::Newtonsoft.Json.Utilities.MethodCall<object?, object> invoker)
		{
			_instance = instance;
			_invoker = invoker;
		}

		public object Invoke(params object[] args)
		{
			return _invoker(_instance, args);
		}
	}
}
