namespace UnityEngine
{
	public class RuleTile<T> : global::UnityEngine.RuleTile
	{
		public sealed override global::System.Type m_NeighborType => typeof(T);
	}
	[global::System.Serializable]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/RuleTile.html")]
	public class RuleTile : global::UnityEngine.Tilemaps.TileBase
	{
		[global::System.Serializable]
		public class TilingRuleOutput
		{
			public enum OutputSprite
			{
				Single = 0,
				Random = 1,
				Animation = 2
			}

			public enum Transform
			{
				Fixed = 0,
				Rotated = 1,
				MirrorX = 2,
				MirrorY = 3,
				MirrorXY = 4,
				RotatedMirror = 5
			}

			public class Neighbor
			{
				public const int This = 1;

				public const int NotThis = 2;
			}

			public int m_Id;

			public global::UnityEngine.Sprite[] m_Sprites = new global::UnityEngine.Sprite[1];

			public global::UnityEngine.GameObject m_GameObject;

			[global::UnityEngine.Serialization.FormerlySerializedAs("m_AnimationSpeed")]
			public float m_MinAnimationSpeed = 1f;

			[global::UnityEngine.Serialization.FormerlySerializedAs("m_AnimationSpeed")]
			public float m_MaxAnimationSpeed = 1f;

			public float m_PerlinScale = 0.5f;

			public global::UnityEngine.RuleTile.TilingRuleOutput.OutputSprite m_Output;

			public global::UnityEngine.Tilemaps.Tile.ColliderType m_ColliderType = global::UnityEngine.Tilemaps.Tile.ColliderType.Sprite;

			public global::UnityEngine.RuleTile.TilingRuleOutput.Transform m_RandomTransform;
		}

		[global::System.Serializable]
		public class TilingRule : global::UnityEngine.RuleTile.TilingRuleOutput
		{
			public global::System.Collections.Generic.List<int> m_Neighbors = new global::System.Collections.Generic.List<int>();

			public global::System.Collections.Generic.List<global::UnityEngine.Vector3Int> m_NeighborPositions = new global::System.Collections.Generic.List<global::UnityEngine.Vector3Int>
			{
				new global::UnityEngine.Vector3Int(-1, 1, 0),
				new global::UnityEngine.Vector3Int(0, 1, 0),
				new global::UnityEngine.Vector3Int(1, 1, 0),
				new global::UnityEngine.Vector3Int(-1, 0, 0),
				new global::UnityEngine.Vector3Int(1, 0, 0),
				new global::UnityEngine.Vector3Int(-1, -1, 0),
				new global::UnityEngine.Vector3Int(0, -1, 0),
				new global::UnityEngine.Vector3Int(1, -1, 0)
			};

			public global::UnityEngine.RuleTile.TilingRuleOutput.Transform m_RuleTransform;

			public global::UnityEngine.RuleTile.TilingRule Clone()
			{
				global::UnityEngine.RuleTile.TilingRule tilingRule = new global::UnityEngine.RuleTile.TilingRule
				{
					m_Neighbors = new global::System.Collections.Generic.List<int>(m_Neighbors),
					m_NeighborPositions = new global::System.Collections.Generic.List<global::UnityEngine.Vector3Int>(m_NeighborPositions),
					m_RuleTransform = m_RuleTransform,
					m_Sprites = new global::UnityEngine.Sprite[m_Sprites.Length],
					m_GameObject = m_GameObject,
					m_MinAnimationSpeed = m_MinAnimationSpeed,
					m_MaxAnimationSpeed = m_MaxAnimationSpeed,
					m_PerlinScale = m_PerlinScale,
					m_Output = m_Output,
					m_ColliderType = m_ColliderType,
					m_RandomTransform = m_RandomTransform
				};
				global::System.Array.Copy(m_Sprites, tilingRule.m_Sprites, m_Sprites.Length);
				return tilingRule;
			}

			public global::System.Collections.Generic.Dictionary<global::UnityEngine.Vector3Int, int> GetNeighbors()
			{
				global::System.Collections.Generic.Dictionary<global::UnityEngine.Vector3Int, int> dictionary = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Vector3Int, int>();
				for (int i = 0; i < m_Neighbors.Count && i < m_NeighborPositions.Count; i++)
				{
					dictionary.Add(m_NeighborPositions[i], m_Neighbors[i]);
				}
				return dictionary;
			}

			public void ApplyNeighbors(global::System.Collections.Generic.Dictionary<global::UnityEngine.Vector3Int, int> dict)
			{
				m_NeighborPositions = global::System.Linq.Enumerable.ToList(dict.Keys);
				m_Neighbors = global::System.Linq.Enumerable.ToList(dict.Values);
			}

			public global::UnityEngine.BoundsInt GetBounds()
			{
				global::UnityEngine.BoundsInt result = new global::UnityEngine.BoundsInt(global::UnityEngine.Vector3Int.zero, global::UnityEngine.Vector3Int.one);
				foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Vector3Int, int> neighbor in GetNeighbors())
				{
					result.xMin = global::UnityEngine.Mathf.Min(result.xMin, neighbor.Key.x);
					result.yMin = global::UnityEngine.Mathf.Min(result.yMin, neighbor.Key.y);
					result.xMax = global::UnityEngine.Mathf.Max(result.xMax, neighbor.Key.x + 1);
					result.yMax = global::UnityEngine.Mathf.Max(result.yMax, neighbor.Key.y + 1);
				}
				return result;
			}
		}

		public class DontOverride : global::System.Attribute
		{
		}

		private static global::System.Collections.Generic.Dictionary<global::UnityEngine.Tilemaps.Tilemap, global::System.Collections.Generic.KeyValuePair<global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase>, global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>>> m_CacheTilemapsNeighborPositions = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Tilemaps.Tilemap, global::System.Collections.Generic.KeyValuePair<global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase>, global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>>>();

		private static global::UnityEngine.Tilemaps.TileBase[] m_AllocatedUsedTileArr = global::System.Array.Empty<global::UnityEngine.Tilemaps.TileBase>();

		public global::UnityEngine.Sprite m_DefaultSprite;

		public global::UnityEngine.GameObject m_DefaultGameObject;

		public global::UnityEngine.Tilemaps.Tile.ColliderType m_DefaultColliderType = global::UnityEngine.Tilemaps.Tile.ColliderType.Sprite;

		[global::UnityEngine.HideInInspector]
		public global::System.Collections.Generic.List<global::UnityEngine.RuleTile.TilingRule> m_TilingRules = new global::System.Collections.Generic.List<global::UnityEngine.RuleTile.TilingRule>();

		private global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int> m_NeighborPositions = new global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>();

		public virtual global::System.Type m_NeighborType => typeof(global::UnityEngine.RuleTile.TilingRuleOutput.Neighbor);

		public virtual int m_RotationAngle => 90;

		public int m_RotationCount => 360 / m_RotationAngle;

		public global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int> neighborPositions
		{
			get
			{
				if (m_NeighborPositions.Count == 0)
				{
					UpdateNeighborPositions();
				}
				return m_NeighborPositions;
			}
		}

		public void UpdateNeighborPositions()
		{
			m_CacheTilemapsNeighborPositions.Clear();
			global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int> hashSet = m_NeighborPositions;
			hashSet.Clear();
			foreach (global::UnityEngine.RuleTile.TilingRule tilingRule in m_TilingRules)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Vector3Int, int> neighbor in tilingRule.GetNeighbors())
				{
					global::UnityEngine.Vector3Int key = neighbor.Key;
					hashSet.Add(key);
					if (tilingRule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.Rotated)
					{
						for (int i = m_RotationAngle; i < 360; i += m_RotationAngle)
						{
							hashSet.Add(GetRotatedPosition(key, i));
						}
					}
					else if (tilingRule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.MirrorXY)
					{
						hashSet.Add(GetMirroredPosition(key, mirrorX: true, mirrorY: true));
						hashSet.Add(GetMirroredPosition(key, mirrorX: true, mirrorY: false));
						hashSet.Add(GetMirroredPosition(key, mirrorX: false, mirrorY: true));
					}
					else if (tilingRule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.MirrorX)
					{
						hashSet.Add(GetMirroredPosition(key, mirrorX: true, mirrorY: false));
					}
					else if (tilingRule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.MirrorY)
					{
						hashSet.Add(GetMirroredPosition(key, mirrorX: false, mirrorY: true));
					}
					else if (tilingRule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.RotatedMirror)
					{
						global::UnityEngine.Vector3Int mirroredPosition = GetMirroredPosition(key, mirrorX: true, mirrorY: false);
						for (int j = m_RotationAngle; j < 360; j += m_RotationAngle)
						{
							hashSet.Add(GetRotatedPosition(key, j));
							hashSet.Add(GetRotatedPosition(mirroredPosition, j));
						}
					}
				}
			}
		}

		public override bool StartUp(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, global::UnityEngine.GameObject instantiatedGameObject)
		{
			if (instantiatedGameObject != null)
			{
				global::UnityEngine.Tilemaps.Tilemap component = tilemap.GetComponent<global::UnityEngine.Tilemaps.Tilemap>();
				global::UnityEngine.Matrix4x4 orientationMatrix = component.orientationMatrix;
				global::UnityEngine.Matrix4x4 identity = global::UnityEngine.Matrix4x4.identity;
				global::UnityEngine.Vector3 vector = default(global::UnityEngine.Vector3);
				global::UnityEngine.Quaternion localRotation = default(global::UnityEngine.Quaternion);
				global::UnityEngine.Vector3 localScale = default(global::UnityEngine.Vector3);
				bool flag = false;
				global::UnityEngine.Matrix4x4 transform = identity;
				foreach (global::UnityEngine.RuleTile.TilingRule tilingRule in m_TilingRules)
				{
					if (RuleMatches(tilingRule, position, tilemap, ref transform))
					{
						transform = orientationMatrix * transform;
						vector = new global::UnityEngine.Vector3(transform.m03, transform.m13, transform.m23);
						localRotation = global::UnityEngine.Quaternion.LookRotation(new global::UnityEngine.Vector3(transform.m02, transform.m12, transform.m22), new global::UnityEngine.Vector3(transform.m01, transform.m11, transform.m21));
						localScale = transform.lossyScale;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					vector = new global::UnityEngine.Vector3(orientationMatrix.m03, orientationMatrix.m13, orientationMatrix.m23);
					localRotation = global::UnityEngine.Quaternion.LookRotation(new global::UnityEngine.Vector3(orientationMatrix.m02, orientationMatrix.m12, orientationMatrix.m22), new global::UnityEngine.Vector3(orientationMatrix.m01, orientationMatrix.m11, orientationMatrix.m21));
					localScale = orientationMatrix.lossyScale;
				}
				instantiatedGameObject.transform.localPosition = vector + component.CellToLocalInterpolated(position + component.tileAnchor);
				instantiatedGameObject.transform.localRotation = localRotation;
				instantiatedGameObject.transform.localScale = localScale;
			}
			return true;
		}

		public override void GetTileData(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, ref global::UnityEngine.Tilemaps.TileData tileData)
		{
			global::UnityEngine.Matrix4x4 identity = global::UnityEngine.Matrix4x4.identity;
			tileData.sprite = m_DefaultSprite;
			tileData.gameObject = m_DefaultGameObject;
			tileData.colliderType = m_DefaultColliderType;
			tileData.flags = global::UnityEngine.Tilemaps.TileFlags.LockTransform;
			tileData.transform = identity;
			global::UnityEngine.Matrix4x4 transform = identity;
			foreach (global::UnityEngine.RuleTile.TilingRule tilingRule in m_TilingRules)
			{
				if (!RuleMatches(tilingRule, position, tilemap, ref transform))
				{
					continue;
				}
				switch (tilingRule.m_Output)
				{
				case global::UnityEngine.RuleTile.TilingRuleOutput.OutputSprite.Single:
				case global::UnityEngine.RuleTile.TilingRuleOutput.OutputSprite.Animation:
					tileData.sprite = tilingRule.m_Sprites[0];
					break;
				case global::UnityEngine.RuleTile.TilingRuleOutput.OutputSprite.Random:
				{
					int num = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.FloorToInt(GetPerlinValue(position, tilingRule.m_PerlinScale, 100000f) * (float)tilingRule.m_Sprites.Length), 0, tilingRule.m_Sprites.Length - 1);
					tileData.sprite = tilingRule.m_Sprites[num];
					if (tilingRule.m_RandomTransform != global::UnityEngine.RuleTile.TilingRuleOutput.Transform.Fixed)
					{
						transform = ApplyRandomTransform(tilingRule.m_RandomTransform, transform, tilingRule.m_PerlinScale, position);
					}
					break;
				}
				}
				tileData.transform = transform;
				tileData.gameObject = tilingRule.m_GameObject;
				tileData.colliderType = tilingRule.m_ColliderType;
				break;
			}
		}

		public static float GetPerlinValue(global::UnityEngine.Vector3Int position, float scale, float offset)
		{
			return global::UnityEngine.Mathf.PerlinNoise(((float)position.x + offset) * scale, ((float)position.y + offset) * scale);
		}

		private static bool IsTilemapUsedTilesChange(global::UnityEngine.Tilemaps.Tilemap tilemap, out global::System.Collections.Generic.KeyValuePair<global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase>, global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>> hashSet)
		{
			if (!m_CacheTilemapsNeighborPositions.TryGetValue(tilemap, out hashSet))
			{
				return true;
			}
			global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase> key = hashSet.Key;
			int usedTilesCount = tilemap.GetUsedTilesCount();
			if (usedTilesCount != key.Count)
			{
				return true;
			}
			if (m_AllocatedUsedTileArr.Length < usedTilesCount)
			{
				global::System.Array.Resize(ref m_AllocatedUsedTileArr, usedTilesCount);
			}
			tilemap.GetUsedTilesNonAlloc(m_AllocatedUsedTileArr);
			for (int i = 0; i < usedTilesCount; i++)
			{
				global::UnityEngine.Tilemaps.TileBase item = m_AllocatedUsedTileArr[i];
				if (!key.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		private static global::System.Collections.Generic.KeyValuePair<global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase>, global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>> CachingTilemapNeighborPositions(global::UnityEngine.Tilemaps.Tilemap tilemap)
		{
			int usedTilesCount = tilemap.GetUsedTilesCount();
			global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase> hashSet = new global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase>();
			global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int> hashSet2 = new global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>();
			if (m_AllocatedUsedTileArr.Length < usedTilesCount)
			{
				global::System.Array.Resize(ref m_AllocatedUsedTileArr, usedTilesCount);
			}
			tilemap.GetUsedTilesNonAlloc(m_AllocatedUsedTileArr);
			for (int i = 0; i < usedTilesCount; i++)
			{
				global::UnityEngine.Tilemaps.TileBase tileBase = m_AllocatedUsedTileArr[i];
				hashSet.Add(tileBase);
				global::UnityEngine.RuleTile ruleTile = null;
				if (tileBase is global::UnityEngine.RuleTile ruleTile2)
				{
					ruleTile = ruleTile2;
				}
				else if (tileBase is global::UnityEngine.Tilemaps.RuleOverrideTile ruleOverrideTile)
				{
					ruleTile = ruleOverrideTile.m_Tile;
				}
				if (!ruleTile)
				{
					continue;
				}
				foreach (global::UnityEngine.Vector3Int neighborPosition in ruleTile.neighborPositions)
				{
					hashSet2.Add(neighborPosition);
				}
			}
			global::System.Collections.Generic.KeyValuePair<global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase>, global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>> keyValuePair = new global::System.Collections.Generic.KeyValuePair<global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase>, global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>>(hashSet, hashSet2);
			m_CacheTilemapsNeighborPositions[tilemap] = keyValuePair;
			return keyValuePair;
		}

		private static bool NeedRelease()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Tilemaps.Tilemap, global::System.Collections.Generic.KeyValuePair<global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase>, global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>>> cacheTilemapsNeighborPosition in m_CacheTilemapsNeighborPositions)
			{
				if (cacheTilemapsNeighborPosition.Key == null)
				{
					return true;
				}
			}
			return false;
		}

		private static void ReleaseDestroyedTilemapCacheData()
		{
			if (!NeedRelease())
			{
				return;
			}
			bool flag = false;
			global::UnityEngine.Tilemaps.Tilemap[] array = global::System.Linq.Enumerable.ToArray(m_CacheTilemapsNeighborPositions.Keys);
			foreach (global::UnityEngine.Tilemaps.Tilemap tilemap in array)
			{
				if (tilemap == null && m_CacheTilemapsNeighborPositions.Remove(tilemap))
				{
					flag = true;
				}
			}
			if (flag)
			{
				m_CacheTilemapsNeighborPositions = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Tilemaps.Tilemap, global::System.Collections.Generic.KeyValuePair<global::System.Collections.Generic.HashSet<global::UnityEngine.Tilemaps.TileBase>, global::System.Collections.Generic.HashSet<global::UnityEngine.Vector3Int>>>(m_CacheTilemapsNeighborPositions);
			}
		}

		public override bool GetTileAnimationData(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, ref global::UnityEngine.Tilemaps.TileAnimationData tileAnimationData)
		{
			global::UnityEngine.Matrix4x4 transform = global::UnityEngine.Matrix4x4.identity;
			foreach (global::UnityEngine.RuleTile.TilingRule tilingRule in m_TilingRules)
			{
				if (tilingRule.m_Output == global::UnityEngine.RuleTile.TilingRuleOutput.OutputSprite.Animation && RuleMatches(tilingRule, position, tilemap, ref transform))
				{
					tileAnimationData.animatedSprites = tilingRule.m_Sprites;
					tileAnimationData.animationSpeed = global::UnityEngine.Random.Range(tilingRule.m_MinAnimationSpeed, tilingRule.m_MaxAnimationSpeed);
					return true;
				}
			}
			return false;
		}

		public override void RefreshTile(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap)
		{
			base.RefreshTile(position, tilemap);
			global::UnityEngine.Tilemaps.Tilemap component = tilemap.GetComponent<global::UnityEngine.Tilemaps.Tilemap>();
			ReleaseDestroyedTilemapCacheData();
			if (IsTilemapUsedTilesChange(component, out var hashSet))
			{
				hashSet = CachingTilemapNeighborPositions(component);
			}
			foreach (global::UnityEngine.Vector3Int item in hashSet.Value)
			{
				global::UnityEngine.Vector3Int offsetPositionReverse = GetOffsetPositionReverse(position, item);
				global::UnityEngine.Tilemaps.TileBase tile = tilemap.GetTile(offsetPositionReverse);
				global::UnityEngine.RuleTile ruleTile = null;
				if (tile is global::UnityEngine.RuleTile ruleTile2)
				{
					ruleTile = ruleTile2;
				}
				else if (tile is global::UnityEngine.Tilemaps.RuleOverrideTile ruleOverrideTile)
				{
					ruleTile = ruleOverrideTile.m_Tile;
				}
				if (ruleTile == this || (ruleTile != null && ruleTile.neighborPositions.Contains(item)))
				{
					base.RefreshTile(offsetPositionReverse, tilemap);
				}
			}
		}

		public virtual bool RuleMatches(global::UnityEngine.RuleTile.TilingRule rule, global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, ref global::UnityEngine.Matrix4x4 transform)
		{
			if (RuleMatches(rule, position, tilemap, 0))
			{
				transform = global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f), global::UnityEngine.Vector3.one);
				return true;
			}
			if (rule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.Rotated)
			{
				for (int i = m_RotationAngle; i < 360; i += m_RotationAngle)
				{
					if (RuleMatches(rule, position, tilemap, i))
					{
						transform = global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.Euler(0f, 0f, -i), global::UnityEngine.Vector3.one);
						return true;
					}
				}
			}
			else if (rule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.MirrorXY)
			{
				if (RuleMatches(rule, position, tilemap, mirrorX: true, mirrorY: true))
				{
					transform = global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.identity, new global::UnityEngine.Vector3(-1f, -1f, 1f));
					return true;
				}
				if (RuleMatches(rule, position, tilemap, mirrorX: true, mirrorY: false))
				{
					transform = global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.identity, new global::UnityEngine.Vector3(-1f, 1f, 1f));
					return true;
				}
				if (RuleMatches(rule, position, tilemap, mirrorX: false, mirrorY: true))
				{
					transform = global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.identity, new global::UnityEngine.Vector3(1f, -1f, 1f));
					return true;
				}
			}
			else if (rule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.MirrorX)
			{
				if (RuleMatches(rule, position, tilemap, mirrorX: true, mirrorY: false))
				{
					transform = global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.identity, new global::UnityEngine.Vector3(-1f, 1f, 1f));
					return true;
				}
			}
			else if (rule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.MirrorY)
			{
				if (RuleMatches(rule, position, tilemap, mirrorX: false, mirrorY: true))
				{
					transform = global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.identity, new global::UnityEngine.Vector3(1f, -1f, 1f));
					return true;
				}
			}
			else if (rule.m_RuleTransform == global::UnityEngine.RuleTile.TilingRuleOutput.Transform.RotatedMirror)
			{
				for (int j = 0; j < 360; j += m_RotationAngle)
				{
					if (j != 0 && RuleMatches(rule, position, tilemap, j))
					{
						transform = global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.Euler(0f, 0f, -j), global::UnityEngine.Vector3.one);
						return true;
					}
					if (RuleMatches(rule, position, tilemap, j, mirrorX: true))
					{
						transform = global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.Euler(0f, 0f, -j), new global::UnityEngine.Vector3(-1f, 1f, 1f));
						return true;
					}
				}
			}
			return false;
		}

		public virtual global::UnityEngine.Matrix4x4 ApplyRandomTransform(global::UnityEngine.RuleTile.TilingRuleOutput.Transform type, global::UnityEngine.Matrix4x4 original, float perlinScale, global::UnityEngine.Vector3Int position)
		{
			float perlinValue = GetPerlinValue(position, perlinScale, 200000f);
			switch (type)
			{
			case global::UnityEngine.RuleTile.TilingRuleOutput.Transform.MirrorXY:
				return original * global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.identity, new global::UnityEngine.Vector3((global::System.Math.Abs((double)perlinValue - 0.5) > 0.25) ? 1f : (-1f), ((double)perlinValue < 0.5) ? 1f : (-1f), 1f));
			case global::UnityEngine.RuleTile.TilingRuleOutput.Transform.MirrorX:
				return original * global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.identity, new global::UnityEngine.Vector3(((double)perlinValue < 0.5) ? 1f : (-1f), 1f, 1f));
			case global::UnityEngine.RuleTile.TilingRuleOutput.Transform.MirrorY:
				return original * global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.identity, new global::UnityEngine.Vector3(1f, ((double)perlinValue < 0.5) ? 1f : (-1f), 1f));
			case global::UnityEngine.RuleTile.TilingRuleOutput.Transform.Rotated:
			{
				int num2 = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.FloorToInt(perlinValue * (float)m_RotationCount), 0, m_RotationCount - 1) * m_RotationAngle;
				return global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.Euler(0f, 0f, -num2), global::UnityEngine.Vector3.one);
			}
			case global::UnityEngine.RuleTile.TilingRuleOutput.Transform.RotatedMirror:
			{
				int num = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.FloorToInt(perlinValue * (float)m_RotationCount), 0, m_RotationCount - 1) * m_RotationAngle;
				return global::UnityEngine.Matrix4x4.TRS(global::UnityEngine.Vector3.zero, global::UnityEngine.Quaternion.Euler(0f, 0f, -num), new global::UnityEngine.Vector3(((double)perlinValue < 0.5) ? 1f : (-1f), 1f, 1f));
			}
			default:
				return original;
			}
		}

		public global::System.Reflection.FieldInfo[] GetCustomFields(bool isOverrideInstance)
		{
			return global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Where(GetType().GetFields(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic), (global::System.Reflection.FieldInfo field) => typeof(global::UnityEngine.RuleTile).GetField(field.Name) == null), (global::System.Reflection.FieldInfo field) => field.IsPublic || global::System.Reflection.CustomAttributeExtensions.IsDefined(field, typeof(global::UnityEngine.SerializeField))), (global::System.Reflection.FieldInfo field) => !global::System.Reflection.CustomAttributeExtensions.IsDefined(field, typeof(global::UnityEngine.HideInInspector))), (global::System.Reflection.FieldInfo field) => !isOverrideInstance || !global::System.Reflection.CustomAttributeExtensions.IsDefined(field, typeof(global::UnityEngine.RuleTile.DontOverride))));
		}

		public virtual bool RuleMatch(int neighbor, global::UnityEngine.Tilemaps.TileBase other)
		{
			if (other is global::UnityEngine.Tilemaps.RuleOverrideTile ruleOverrideTile)
			{
				other = ruleOverrideTile.m_InstanceTile;
			}
			return neighbor switch
			{
				1 => other == this, 
				2 => other != this, 
				_ => true, 
			};
		}

		public bool RuleMatches(global::UnityEngine.RuleTile.TilingRule rule, global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, int angle, bool mirrorX = false)
		{
			int num = global::System.Math.Min(rule.m_Neighbors.Count, rule.m_NeighborPositions.Count);
			for (int i = 0; i < num; i++)
			{
				int neighbor = rule.m_Neighbors[i];
				global::UnityEngine.Vector3Int position2 = rule.m_NeighborPositions[i];
				if (mirrorX)
				{
					position2 = GetMirroredPosition(position2, mirrorX: true, mirrorY: false);
				}
				global::UnityEngine.Vector3Int rotatedPosition = GetRotatedPosition(position2, angle);
				global::UnityEngine.Tilemaps.TileBase tile = tilemap.GetTile(GetOffsetPosition(position, rotatedPosition));
				if (!RuleMatch(neighbor, tile))
				{
					return false;
				}
			}
			return true;
		}

		public bool RuleMatches(global::UnityEngine.RuleTile.TilingRule rule, global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, bool mirrorX, bool mirrorY)
		{
			int num = global::System.Math.Min(rule.m_Neighbors.Count, rule.m_NeighborPositions.Count);
			for (int i = 0; i < num; i++)
			{
				int neighbor = rule.m_Neighbors[i];
				global::UnityEngine.Vector3Int mirroredPosition = GetMirroredPosition(rule.m_NeighborPositions[i], mirrorX, mirrorY);
				global::UnityEngine.Tilemaps.TileBase tile = tilemap.GetTile(GetOffsetPosition(position, mirroredPosition));
				if (!RuleMatch(neighbor, tile))
				{
					return false;
				}
			}
			return true;
		}

		public virtual global::UnityEngine.Vector3Int GetRotatedPosition(global::UnityEngine.Vector3Int position, int rotation)
		{
			return rotation switch
			{
				0 => position, 
				90 => new global::UnityEngine.Vector3Int(position.y, -position.x, 0), 
				180 => new global::UnityEngine.Vector3Int(-position.x, -position.y, 0), 
				270 => new global::UnityEngine.Vector3Int(-position.y, position.x, 0), 
				_ => position, 
			};
		}

		public virtual global::UnityEngine.Vector3Int GetMirroredPosition(global::UnityEngine.Vector3Int position, bool mirrorX, bool mirrorY)
		{
			if (mirrorX)
			{
				position.x *= -1;
			}
			if (mirrorY)
			{
				position.y *= -1;
			}
			return position;
		}

		public virtual global::UnityEngine.Vector3Int GetOffsetPosition(global::UnityEngine.Vector3Int position, global::UnityEngine.Vector3Int offset)
		{
			return position + offset;
		}

		public virtual global::UnityEngine.Vector3Int GetOffsetPositionReverse(global::UnityEngine.Vector3Int position, global::UnityEngine.Vector3Int offset)
		{
			return position - offset;
		}
	}
}
