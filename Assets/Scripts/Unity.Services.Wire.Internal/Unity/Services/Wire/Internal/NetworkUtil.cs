namespace Unity.Services.Wire.Internal
{
	internal class NetworkUtil : global::Unity.Services.Wire.Internal.INetworkUtil
	{
		public bool IsInternetReachable()
		{
			return global::UnityEngine.Application.internetReachability != global::UnityEngine.NetworkReachability.NotReachable;
		}
	}
}
