namespace UnityEngine
{
	public class HexagonalRuleTile<T> : global::UnityEngine.HexagonalRuleTile
	{
		public sealed override global::System.Type m_NeighborType => typeof(T);
	}
	[global::System.Serializable]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/RuleTile.html")]
	public class HexagonalRuleTile : global::UnityEngine.RuleTile
	{
		private static float[] m_CosAngleArr1 = new float[6]
		{
			global::UnityEngine.Mathf.Cos(0f),
			global::UnityEngine.Mathf.Cos(-global::System.MathF.PI / 3f),
			global::UnityEngine.Mathf.Cos(global::System.MathF.PI * -2f / 3f),
			global::UnityEngine.Mathf.Cos(-global::System.MathF.PI),
			global::UnityEngine.Mathf.Cos(-4.1887903f),
			global::UnityEngine.Mathf.Cos(-5.2359877f)
		};

		private static float[] m_SinAngleArr1 = new float[6]
		{
			global::UnityEngine.Mathf.Sin(0f),
			global::UnityEngine.Mathf.Sin(-global::System.MathF.PI / 3f),
			global::UnityEngine.Mathf.Sin(global::System.MathF.PI * -2f / 3f),
			global::UnityEngine.Mathf.Sin(-global::System.MathF.PI),
			global::UnityEngine.Mathf.Sin(-4.1887903f),
			global::UnityEngine.Mathf.Sin(-5.2359877f)
		};

		private static float[] m_CosAngleArr2 = new float[6]
		{
			global::UnityEngine.Mathf.Cos(0f),
			global::UnityEngine.Mathf.Cos(global::System.MathF.PI / 3f),
			global::UnityEngine.Mathf.Cos(global::System.MathF.PI * 2f / 3f),
			global::UnityEngine.Mathf.Cos(global::System.MathF.PI),
			global::UnityEngine.Mathf.Cos(4.1887903f),
			global::UnityEngine.Mathf.Cos(5.2359877f)
		};

		private static float[] m_SinAngleArr2 = new float[6]
		{
			global::UnityEngine.Mathf.Sin(0f),
			global::UnityEngine.Mathf.Sin(global::System.MathF.PI / 3f),
			global::UnityEngine.Mathf.Sin(global::System.MathF.PI * 2f / 3f),
			global::UnityEngine.Mathf.Sin(global::System.MathF.PI),
			global::UnityEngine.Mathf.Sin(4.1887903f),
			global::UnityEngine.Mathf.Sin(5.2359877f)
		};

		private static float m_TilemapToWorldYScale = global::UnityEngine.Mathf.Pow(1f - global::UnityEngine.Mathf.Pow(0.5f, 2f), 0.5f);

		[global::UnityEngine.RuleTile.DontOverride]
		public bool m_FlatTop;

		public override int m_RotationAngle => 60;

		public static global::UnityEngine.Vector3 TilemapPositionToWorldPosition(global::UnityEngine.Vector3Int tilemapPosition)
		{
			global::UnityEngine.Vector3 result = new global::UnityEngine.Vector3(tilemapPosition.x, tilemapPosition.y);
			if (tilemapPosition.y % 2 != 0)
			{
				result.x += 0.5f;
			}
			result.y *= m_TilemapToWorldYScale;
			return result;
		}

		public static global::UnityEngine.Vector3Int WorldPositionToTilemapPosition(global::UnityEngine.Vector3 worldPosition)
		{
			worldPosition.y /= m_TilemapToWorldYScale;
			global::UnityEngine.Vector3Int result = new global::UnityEngine.Vector3Int
			{
				y = global::UnityEngine.Mathf.RoundToInt(worldPosition.y)
			};
			if (result.y % 2 != 0)
			{
				result.x = global::UnityEngine.Mathf.RoundToInt(worldPosition.x - 0.5f);
			}
			else
			{
				result.x = global::UnityEngine.Mathf.RoundToInt(worldPosition.x);
			}
			return result;
		}

		public override global::UnityEngine.Vector3Int GetOffsetPosition(global::UnityEngine.Vector3Int position, global::UnityEngine.Vector3Int offset)
		{
			global::UnityEngine.Vector3Int result = position + offset;
			if (offset.y % 2 != 0 && position.y % 2 != 0)
			{
				result.x++;
			}
			return result;
		}

		public override global::UnityEngine.Vector3Int GetOffsetPositionReverse(global::UnityEngine.Vector3Int position, global::UnityEngine.Vector3Int offset)
		{
			return GetOffsetPosition(position, GetRotatedPosition(offset, 180));
		}

		public override global::UnityEngine.Vector3Int GetRotatedPosition(global::UnityEngine.Vector3Int position, int rotation)
		{
			if (rotation != 0)
			{
				global::UnityEngine.Vector3 vector = TilemapPositionToWorldPosition(position);
				int num = rotation / 60;
				vector = ((!m_FlatTop) ? new global::UnityEngine.Vector3(vector.x * m_CosAngleArr1[num] - vector.y * m_SinAngleArr1[num], vector.x * m_SinAngleArr1[num] + vector.y * m_CosAngleArr1[num]) : new global::UnityEngine.Vector3(vector.x * m_CosAngleArr2[num] - vector.y * m_SinAngleArr2[num], vector.x * m_SinAngleArr2[num] + vector.y * m_CosAngleArr2[num]));
				position = WorldPositionToTilemapPosition(vector);
			}
			return position;
		}

		public override global::UnityEngine.Vector3Int GetMirroredPosition(global::UnityEngine.Vector3Int position, bool mirrorX, bool mirrorY)
		{
			if (mirrorX || mirrorY)
			{
				global::UnityEngine.Vector3 worldPosition = TilemapPositionToWorldPosition(position);
				if (m_FlatTop)
				{
					if (mirrorX)
					{
						worldPosition.y *= -1f;
					}
					if (mirrorY)
					{
						worldPosition.x *= -1f;
					}
				}
				else
				{
					if (mirrorX)
					{
						worldPosition.x *= -1f;
					}
					if (mirrorY)
					{
						worldPosition.y *= -1f;
					}
				}
				position = WorldPositionToTilemapPosition(worldPosition);
			}
			return position;
		}
	}
}
