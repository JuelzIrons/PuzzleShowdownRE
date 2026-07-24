namespace UnityEngine.Rendering.Universal
{
	internal static class TileSizeExtensions
	{
		public static bool IsValid(this global::UnityEngine.Rendering.Universal.TileSize tileSize)
		{
			if (tileSize != global::UnityEngine.Rendering.Universal.TileSize._8 && tileSize != global::UnityEngine.Rendering.Universal.TileSize._16 && tileSize != global::UnityEngine.Rendering.Universal.TileSize._32)
			{
				return tileSize == global::UnityEngine.Rendering.Universal.TileSize._64;
			}
			return true;
		}
	}
}
