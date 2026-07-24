namespace Unity.Multiplayer.Tools.NetStats
{
	internal static class UnitExtensions
	{
		internal static global::Unity.Multiplayer.Tools.NetStats.BaseUnits GetBaseUnits(this global::Unity.Multiplayer.Tools.NetStats.Units units)
		{
			return units switch
			{
				global::Unity.Multiplayer.Tools.NetStats.Units.None => default(global::Unity.Multiplayer.Tools.NetStats.BaseUnits), 
				global::Unity.Multiplayer.Tools.NetStats.Units.Bytes => new global::Unity.Multiplayer.Tools.NetStats.BaseUnits(1, 0), 
				global::Unity.Multiplayer.Tools.NetStats.Units.BytesPerSecond => new global::Unity.Multiplayer.Tools.NetStats.BaseUnits(1, -1), 
				global::Unity.Multiplayer.Tools.NetStats.Units.Seconds => new global::Unity.Multiplayer.Tools.NetStats.BaseUnits(0, 1), 
				global::Unity.Multiplayer.Tools.NetStats.Units.Hertz => new global::Unity.Multiplayer.Tools.NetStats.BaseUnits(0, -1), 
				_ => throw new global::System.ArgumentOutOfRangeException("units", units, null), 
			};
		}
	}
}
