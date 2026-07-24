namespace Unity.Networking.Transport
{
	public struct TransportFunctionPointer<T> where T : global::System.Delegate
	{
		public readonly global::Unity.Burst.FunctionPointer<T> Ptr;

		public TransportFunctionPointer(T executeDelegate)
		{
			Ptr = global::Unity.Burst.BurstCompiler.CompileFunctionPointer(executeDelegate);
		}

		public TransportFunctionPointer(global::Unity.Burst.FunctionPointer<T> pointer)
		{
			Ptr = pointer;
		}

		public static global::Unity.Networking.Transport.TransportFunctionPointer<T> Burst(T burstCompilableDelegate)
		{
			return new global::Unity.Networking.Transport.TransportFunctionPointer<T>(global::Unity.Burst.BurstCompiler.CompileFunctionPointer(burstCompilableDelegate));
		}

		public static global::Unity.Networking.Transport.TransportFunctionPointer<T> Managed(T managedDelegate)
		{
			global::System.Runtime.InteropServices.GCHandle.Alloc(managedDelegate);
			return new global::Unity.Networking.Transport.TransportFunctionPointer<T>(new global::Unity.Burst.FunctionPointer<T>(global::System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(managedDelegate)));
		}
	}
}
