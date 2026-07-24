namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamNetworkingErrMsg
	{
		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1024)]
		public byte[] m_SteamNetworkingErrMsg;
	}
}
