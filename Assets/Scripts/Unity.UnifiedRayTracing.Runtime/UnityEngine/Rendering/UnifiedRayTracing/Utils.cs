namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal static class Utils
	{
		public static void Destroy(global::UnityEngine.Object obj)
		{
			if (obj != null)
			{
				global::UnityEngine.Object.Destroy(obj);
			}
		}

		[global::System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
		public static void CheckArgIsNotNull(object obj, string argName)
		{
			if (obj == null)
			{
				throw new global::System.ArgumentNullException(argName);
			}
		}

		[global::System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
		public static void CheckArg(bool condition, string message)
		{
			if (!condition)
			{
				throw new global::System.ArgumentException(message);
			}
		}

		[global::System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
		public static void CheckArgRange<T>(T value, T minIncluded, T maxExcluded, string argName) where T : global::System.IComparable
		{
			if (value.CompareTo(minIncluded) < 0 || value.CompareTo(maxExcluded) >= 0)
			{
				string message = $"{argName}={value}, it must be in the range [{minIncluded}, {maxExcluded}[";
				throw new global::System.ArgumentOutOfRangeException(argName, message);
			}
		}
	}
}
