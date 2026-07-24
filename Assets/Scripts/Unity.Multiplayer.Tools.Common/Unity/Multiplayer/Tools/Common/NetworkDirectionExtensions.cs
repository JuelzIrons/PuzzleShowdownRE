namespace Unity.Multiplayer.Tools.Common
{
	internal static class NetworkDirectionExtensions
	{
		public static string DisplayName(this global::Unity.Multiplayer.Tools.Common.NetworkDirection direction)
		{
			return direction switch
			{
				global::Unity.Multiplayer.Tools.Common.NetworkDirection.None => "None", 
				global::Unity.Multiplayer.Tools.Common.NetworkDirection.Received => "Received", 
				global::Unity.Multiplayer.Tools.Common.NetworkDirection.Sent => "Sent", 
				global::Unity.Multiplayer.Tools.Common.NetworkDirection.SentAndReceived => "Sent And Received", 
				_ => throw new global::System.ArgumentOutOfRangeException(string.Format("Unknow {0} {1}", "NetworkDirection", direction)), 
			};
		}
	}
}
