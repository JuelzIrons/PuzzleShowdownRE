namespace Unity.Hierarchy
{
	internal static class StopwatchExtensions
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double ElapsedMillisecondsPrecise(this global::System.Diagnostics.Stopwatch stopwatch)
		{
			return (double)stopwatch.ElapsedTicks / (double)global::System.Diagnostics.Stopwatch.Frequency * 1000.0;
		}
	}
}
