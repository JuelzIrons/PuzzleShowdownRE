namespace UnityEngine.Rendering
{
	public static class TileLayoutUtils
	{
		public static bool TryLayoutByTiles(global::UnityEngine.RectInt src, uint tileSize, out global::UnityEngine.RectInt main, out global::UnityEngine.RectInt topRow, out global::UnityEngine.RectInt rightCol, out global::UnityEngine.RectInt topRight)
		{
			if (src.width < tileSize || src.height < tileSize)
			{
				main = new global::UnityEngine.RectInt(0, 0, 0, 0);
				topRow = new global::UnityEngine.RectInt(0, 0, 0, 0);
				rightCol = new global::UnityEngine.RectInt(0, 0, 0, 0);
				topRight = new global::UnityEngine.RectInt(0, 0, 0, 0);
				return false;
			}
			int num = src.height / (int)tileSize;
			int num2 = src.width / (int)tileSize * (int)tileSize;
			int num3 = num * (int)tileSize;
			main = new global::UnityEngine.RectInt
			{
				x = src.x,
				y = src.y,
				width = num2,
				height = num3
			};
			topRow = new global::UnityEngine.RectInt
			{
				x = src.x,
				y = src.y + num3,
				width = num2,
				height = src.height - num3
			};
			rightCol = new global::UnityEngine.RectInt
			{
				x = src.x + num2,
				y = src.y,
				width = src.width - num2,
				height = num3
			};
			topRight = new global::UnityEngine.RectInt
			{
				x = src.x + num2,
				y = src.y + num3,
				width = src.width - num2,
				height = src.height - num3
			};
			return true;
		}

		public static bool TryLayoutByRow(global::UnityEngine.RectInt src, uint tileSize, out global::UnityEngine.RectInt main, out global::UnityEngine.RectInt other)
		{
			if (src.height < tileSize)
			{
				main = new global::UnityEngine.RectInt(0, 0, 0, 0);
				other = new global::UnityEngine.RectInt(0, 0, 0, 0);
				return false;
			}
			int num = src.height / (int)tileSize * (int)tileSize;
			main = new global::UnityEngine.RectInt
			{
				x = src.x,
				y = src.y,
				width = src.width,
				height = num
			};
			other = new global::UnityEngine.RectInt
			{
				x = src.x,
				y = src.y + num,
				width = src.width,
				height = src.height - num
			};
			return true;
		}

		public static bool TryLayoutByCol(global::UnityEngine.RectInt src, uint tileSize, out global::UnityEngine.RectInt main, out global::UnityEngine.RectInt other)
		{
			if (src.width < tileSize)
			{
				main = new global::UnityEngine.RectInt(0, 0, 0, 0);
				other = new global::UnityEngine.RectInt(0, 0, 0, 0);
				return false;
			}
			int num = src.width / (int)tileSize * (int)tileSize;
			main = new global::UnityEngine.RectInt
			{
				x = src.x,
				y = src.y,
				width = num,
				height = src.height
			};
			other = new global::UnityEngine.RectInt
			{
				x = src.x + num,
				y = src.y,
				width = src.width - num,
				height = src.height
			};
			return true;
		}
	}
}
