namespace Unity.Multiplayer.Tools.Common
{
	internal static class BytesSentAndReceivedExtensions
	{
		public static global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived Sum<T>(this global::System.Collections.Generic.IEnumerable<T> ts, global::System.Func<T, global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived> f)
		{
			global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived result = default(global::Unity.Multiplayer.Tools.Common.BytesSentAndReceived);
			foreach (T t in ts)
			{
				result += f(t);
			}
			return result;
		}
	}
}
