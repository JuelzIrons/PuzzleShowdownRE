namespace Unity.Profiling
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct ProfilerUtility
	{
		public static byte GetProfilerMarkerDataType<T>()
		{
			return global::System.Type.GetTypeCode(typeof(T)) switch
			{
				global::System.TypeCode.Int32 => 2, 
				global::System.TypeCode.UInt32 => 3, 
				global::System.TypeCode.Int64 => 4, 
				global::System.TypeCode.UInt64 => 5, 
				global::System.TypeCode.Single => 6, 
				global::System.TypeCode.Double => 7, 
				global::System.TypeCode.String => 9, 
				_ => throw new global::System.ArgumentException($"Type {typeof(T)} is unsupported by ProfilerCounter."), 
			};
		}
	}
}
