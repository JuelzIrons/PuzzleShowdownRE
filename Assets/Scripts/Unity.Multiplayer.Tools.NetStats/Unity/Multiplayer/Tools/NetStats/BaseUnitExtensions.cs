namespace Unity.Multiplayer.Tools.NetStats
{
	internal static class BaseUnitExtensions
	{
		public static string GetSymbol(this global::Unity.Multiplayer.Tools.NetStats.BaseUnit unit)
		{
			return unit switch
			{
				global::Unity.Multiplayer.Tools.NetStats.BaseUnit.Byte => "B", 
				global::Unity.Multiplayer.Tools.NetStats.BaseUnit.Second => "s", 
				_ => throw new global::System.ArgumentException($"Unhandled BaseUnit {unit}"), 
			};
		}
	}
}
