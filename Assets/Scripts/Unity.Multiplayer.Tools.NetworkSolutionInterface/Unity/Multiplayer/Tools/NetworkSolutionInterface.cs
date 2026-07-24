namespace Unity.Multiplayer.Tools
{
	internal static class NetworkSolutionInterface
	{
		private static global::Unity.Multiplayer.Tools.NetworkSolutionInterfaceParameters s_Parameters;

		internal static global::Unity.Multiplayer.Tools.INetworkObjectProvider NetworkObjectProvider => s_Parameters.NetworkObjectProvider;

		public static void SetInterface(global::Unity.Multiplayer.Tools.NetworkSolutionInterfaceParameters parameters)
		{
			ref global::Unity.Multiplayer.Tools.INetworkObjectProvider networkObjectProvider = ref parameters.NetworkObjectProvider;
			if (networkObjectProvider == null)
			{
				networkObjectProvider = new global::Unity.Multiplayer.Tools.NullNetworkObjectProvider();
			}
			s_Parameters = parameters;
		}
	}
}
