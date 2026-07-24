namespace Unity.Networking.Transport
{
	public static class ManagedNetworkInterfaceExtensions
	{
		public static global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T> WrapToUnmanaged<T>(this T networkInterface) where T : global::Unity.Networking.Transport.INetworkInterface
		{
			return new global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>(ref networkInterface);
		}
	}
}
