namespace Unity.Multiplayer.Tools.NetStats
{
	internal static class UnitPrefixExtensions
	{
		public static string GetSymbol(this global::Unity.Multiplayer.Tools.NetStats.MetricPrefix prefix)
		{
			return prefix switch
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Atto => "a", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Femto => "f", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Pico => "p", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Nano => "n", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Micro => "μ", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Milli => "m", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.None => "", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Kilo => "k", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Mega => "M", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Giga => "G", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Tera => "T", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Peta => "P", 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Exa => "E", 
				_ => throw new global::System.ArgumentException(string.Format("Unhandled {0} {1}", "MetricPrefix", prefix)), 
			};
		}

		public static float GetValueFloat(this global::Unity.Multiplayer.Tools.NetStats.MetricPrefix prefix)
		{
			return prefix switch
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Atto => 1E-18f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Femto => 1E-15f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Pico => 1E-12f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Nano => 1E-09f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Micro => 1E-06f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Milli => 0.001f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.None => 1f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Kilo => 1000f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Mega => 1000000f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Giga => 1E+09f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Tera => 1E+12f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Peta => 1E+15f, 
				global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Exa => 1E+18f, 
				_ => throw new global::System.ArgumentException(string.Format("Unhandled {0} {1}", "MetricPrefix", prefix)), 
			};
		}
	}
}
