namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public struct NetworkParameterConstants
	{
		public const int ConnectTimeoutMS = 1000;

		public const int MaxConnectAttempts = 60;

		public const int DisconnectTimeoutMS = 30000;

		public const int HeartbeatTimeoutMS = 500;

		public const int ReconnectionTimeoutMS = 2000;

		public const int ReceiveQueueCapacity = 512;

		public const int SendQueueCapacity = 512;

		public const int MaxMessageSize = 1400;

		public const int MTU = 1400;

		internal const int InitialEventQueueSize = 100;

		internal const int AbsoluteMaxMessageSize = 1472;

		internal const int AbsoluteMinimumMtuSize = 1024;
	}
}
