namespace Unity.Burst
{
	public static class BurstRuntime
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct HashCode32<T>
		{
			public static readonly int Value = HashStringWithFNV1A32(typeof(T).AssemblyQualifiedName);
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct HashCode64<T>
		{
			public static readonly long Value = HashStringWithFNV1A64(typeof(T).AssemblyQualifiedName);
		}

		internal class PreserveAttribute : global::System.Attribute
		{
		}

		public static int GetHashCode32<T>()
		{
			return global::Unity.Burst.BurstRuntime.HashCode32<T>.Value;
		}

		public static int GetHashCode32(global::System.Type type)
		{
			return HashStringWithFNV1A32(type.AssemblyQualifiedName);
		}

		public static long GetHashCode64<T>()
		{
			return global::Unity.Burst.BurstRuntime.HashCode64<T>.Value;
		}

		public static long GetHashCode64(global::System.Type type)
		{
			return HashStringWithFNV1A64(type.AssemblyQualifiedName);
		}

		internal static int HashStringWithFNV1A32(string text)
		{
			uint num = 2166136261u;
			foreach (char c in text)
			{
				num = 16777619 * (num ^ (byte)(c & 0xFF));
				num = 16777619 * (num ^ (byte)((int)c >> 8));
			}
			return (int)num;
		}

		internal static long HashStringWithFNV1A64(string text)
		{
			ulong num = 14695981039346656037uL;
			foreach (char c in text)
			{
				num = 1099511628211L * (num ^ (byte)(c & 0xFF));
				num = 1099511628211L * (num ^ (byte)((int)c >> 8));
			}
			return (long)num;
		}

		public static bool LoadAdditionalLibrary(string pathToLibBurstGenerated)
		{
			if (global::Unity.Burst.BurstCompiler.IsLoadAdditionalLibrarySupported())
			{
				return LoadAdditionalLibraryInternal(pathToLibBurstGenerated);
			}
			return false;
		}

		internal static bool LoadAdditionalLibraryInternal(string pathToLibBurstGenerated)
		{
			return (bool)typeof(global::Unity.Burst.LowLevel.BurstCompilerService).GetMethod("LoadBurstLibrary").Invoke(null, new object[1] { pathToLibBurstGenerated });
		}

		[global::Unity.Burst.BurstRuntime.Preserve]
		internal unsafe static void RuntimeLog(byte* message, int logType, byte* fileName, int lineNumber)
		{
			global::Unity.Burst.LowLevel.BurstCompilerService.RuntimeLog(null, (global::Unity.Burst.LowLevel.BurstCompilerService.BurstLogType)logType, message, fileName, lineNumber);
		}

		internal static void Initialize()
		{
		}

		[global::Unity.Burst.BurstRuntime.Preserve]
		internal static void PreventRequiredAttributeStrip()
		{
			new global::Unity.Burst.BurstDiscardAttribute();
			new global::System.Diagnostics.ConditionalAttribute("HEJSA");
			new global::Unity.Jobs.LowLevel.Unsafe.JobProducerTypeAttribute(typeof(global::Unity.Burst.BurstRuntime));
		}

		[global::Unity.Burst.BurstRuntime.Preserve]
		internal unsafe static void Log(byte* message, int logType, byte* fileName, int lineNumber)
		{
			global::Unity.Burst.LowLevel.BurstCompilerService.Log(null, (global::Unity.Burst.LowLevel.BurstCompilerService.BurstLogType)logType, message, null, lineNumber);
		}

		public unsafe static byte* GetUTF8LiteralPointer(string str, out int byteCount)
		{
			throw new global::System.NotImplementedException("This function only works from Burst");
		}
	}
}
