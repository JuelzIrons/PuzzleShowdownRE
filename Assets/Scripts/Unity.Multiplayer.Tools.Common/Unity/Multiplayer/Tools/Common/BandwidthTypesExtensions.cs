namespace Unity.Multiplayer.Tools.Common
{
	internal static class BandwidthTypesExtensions
	{
		public static string DisplayName(this global::Unity.Multiplayer.Tools.Common.BandwidthTypes direction)
		{
			return direction switch
			{
				global::Unity.Multiplayer.Tools.Common.BandwidthTypes.None => "None", 
				global::Unity.Multiplayer.Tools.Common.BandwidthTypes.All => "All", 
				global::Unity.Multiplayer.Tools.Common.BandwidthTypes.Other => "Other", 
				global::Unity.Multiplayer.Tools.Common.BandwidthTypes.NetVar => "NetVars", 
				global::Unity.Multiplayer.Tools.Common.BandwidthTypes.Rpc => "RPCs", 
				global::Unity.Multiplayer.Tools.Common.BandwidthTypes.Other | global::Unity.Multiplayer.Tools.Common.BandwidthTypes.NetVar => "NetVars and Other", 
				global::Unity.Multiplayer.Tools.Common.BandwidthTypes.Other | global::Unity.Multiplayer.Tools.Common.BandwidthTypes.Rpc => "RPCs and Other", 
				global::Unity.Multiplayer.Tools.Common.BandwidthTypes.NetVar | global::Unity.Multiplayer.Tools.Common.BandwidthTypes.Rpc => "NetVars and RPCs", 
				_ => throw new global::System.ArgumentOutOfRangeException(string.Format("Unknow {0} {1}", "NetworkDirection", direction)), 
			};
		}
	}
}
