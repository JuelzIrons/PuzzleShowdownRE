namespace UnityEngine.Tilemaps
{
	[global::System.Serializable]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine", null, null)]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/RuleOverrideTile.html")]
	public class RuleOverrideTile : global::UnityEngine.Tilemaps.TileBase
	{
		[global::System.Serializable]
		public class TileSpritePair
		{
			public global::UnityEngine.Sprite m_OriginalSprite;

			public global::UnityEngine.Sprite m_OverrideSprite;
		}

		[global::System.Serializable]
		public class TileGameObjectPair
		{
			public global::UnityEngine.GameObject m_OriginalGameObject;

			public global::UnityEngine.GameObject m_OverrideGameObject;
		}

		public global::UnityEngine.RuleTile m_Tile;

		public global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.RuleOverrideTile.TileSpritePair> m_Sprites = new global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.RuleOverrideTile.TileSpritePair>();

		public global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.RuleOverrideTile.TileGameObjectPair> m_GameObjects = new global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.RuleOverrideTile.TileGameObjectPair>();

		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.RuleTile m_InstanceTile;

		public global::UnityEngine.Sprite this[global::UnityEngine.Sprite originalSprite]
		{
			get
			{
				foreach (global::UnityEngine.Tilemaps.RuleOverrideTile.TileSpritePair sprite in m_Sprites)
				{
					if (sprite.m_OriginalSprite == originalSprite)
					{
						return sprite.m_OverrideSprite;
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					m_Sprites = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(m_Sprites, (global::UnityEngine.Tilemaps.RuleOverrideTile.TileSpritePair spritePair) => spritePair.m_OriginalSprite != originalSprite));
					return;
				}
				foreach (global::UnityEngine.Tilemaps.RuleOverrideTile.TileSpritePair sprite in m_Sprites)
				{
					if (sprite.m_OriginalSprite == originalSprite)
					{
						sprite.m_OverrideSprite = value;
						return;
					}
				}
				m_Sprites.Add(new global::UnityEngine.Tilemaps.RuleOverrideTile.TileSpritePair
				{
					m_OriginalSprite = originalSprite,
					m_OverrideSprite = value
				});
			}
		}

		public global::UnityEngine.GameObject this[global::UnityEngine.GameObject originalGameObject]
		{
			get
			{
				foreach (global::UnityEngine.Tilemaps.RuleOverrideTile.TileGameObjectPair gameObject in m_GameObjects)
				{
					if (gameObject.m_OriginalGameObject == originalGameObject)
					{
						return gameObject.m_OverrideGameObject;
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					m_GameObjects = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(m_GameObjects, (global::UnityEngine.Tilemaps.RuleOverrideTile.TileGameObjectPair gameObjectPair) => gameObjectPair.m_OriginalGameObject != originalGameObject));
					return;
				}
				foreach (global::UnityEngine.Tilemaps.RuleOverrideTile.TileGameObjectPair gameObject in m_GameObjects)
				{
					if (gameObject.m_OriginalGameObject == originalGameObject)
					{
						gameObject.m_OverrideGameObject = value;
						return;
					}
				}
				m_GameObjects.Add(new global::UnityEngine.Tilemaps.RuleOverrideTile.TileGameObjectPair
				{
					m_OriginalGameObject = originalGameObject,
					m_OverrideGameObject = value
				});
			}
		}

		public void OnEnable()
		{
			if (!(m_Tile == null) && m_InstanceTile == null)
			{
				Override();
			}
		}

		private void CreateInstanceTile()
		{
			global::UnityEngine.RuleTile ruleTile = global::UnityEngine.ScriptableObject.CreateInstance(m_Tile.GetType()) as global::UnityEngine.RuleTile;
			ruleTile.hideFlags = global::UnityEngine.HideFlags.NotEditable;
			ruleTile.name = m_Tile.name + " (Override)";
			m_InstanceTile = ruleTile;
		}

		public void ApplyOverrides(global::System.Collections.Generic.IList<global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Sprite, global::UnityEngine.Sprite>> overrides)
		{
			if (overrides == null)
			{
				throw new global::System.ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this[overrides[i].Key] = overrides[i].Value;
			}
		}

		public void ApplyOverrides(global::System.Collections.Generic.IList<global::System.Collections.Generic.KeyValuePair<global::UnityEngine.GameObject, global::UnityEngine.GameObject>> overrides)
		{
			if (overrides == null)
			{
				throw new global::System.ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this[overrides[i].Key] = overrides[i].Value;
			}
		}

		public void GetOverrides(global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Sprite, global::UnityEngine.Sprite>> overrides, ref int validCount)
		{
			if (overrides == null)
			{
				throw new global::System.ArgumentNullException("overrides");
			}
			overrides.Clear();
			global::System.Collections.Generic.List<global::UnityEngine.Sprite> list = new global::System.Collections.Generic.List<global::UnityEngine.Sprite>();
			if ((bool)m_Tile)
			{
				if ((bool)m_Tile.m_DefaultSprite)
				{
					list.Add(m_Tile.m_DefaultSprite);
				}
				foreach (global::UnityEngine.RuleTile.TilingRule tilingRule in m_Tile.m_TilingRules)
				{
					global::UnityEngine.Sprite[] sprites = tilingRule.m_Sprites;
					foreach (global::UnityEngine.Sprite sprite in sprites)
					{
						if ((bool)sprite && !list.Contains(sprite))
						{
							list.Add(sprite);
						}
					}
				}
			}
			validCount = list.Count;
			foreach (global::UnityEngine.Tilemaps.RuleOverrideTile.TileSpritePair sprite2 in m_Sprites)
			{
				if (!list.Contains(sprite2.m_OriginalSprite))
				{
					list.Add(sprite2.m_OriginalSprite);
				}
			}
			foreach (global::UnityEngine.Sprite item in list)
			{
				overrides.Add(new global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Sprite, global::UnityEngine.Sprite>(item, this[item]));
			}
		}

		public void GetOverrides(global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::UnityEngine.GameObject, global::UnityEngine.GameObject>> overrides, ref int validCount)
		{
			if (overrides == null)
			{
				throw new global::System.ArgumentNullException("overrides");
			}
			overrides.Clear();
			global::System.Collections.Generic.List<global::UnityEngine.GameObject> list = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();
			if ((bool)m_Tile)
			{
				if ((bool)m_Tile.m_DefaultGameObject)
				{
					list.Add(m_Tile.m_DefaultGameObject);
				}
				foreach (global::UnityEngine.RuleTile.TilingRule tilingRule in m_Tile.m_TilingRules)
				{
					if ((bool)tilingRule.m_GameObject && !list.Contains(tilingRule.m_GameObject))
					{
						list.Add(tilingRule.m_GameObject);
					}
				}
			}
			validCount = list.Count;
			foreach (global::UnityEngine.Tilemaps.RuleOverrideTile.TileGameObjectPair gameObject in m_GameObjects)
			{
				if (!list.Contains(gameObject.m_OriginalGameObject))
				{
					list.Add(gameObject.m_OriginalGameObject);
				}
			}
			foreach (global::UnityEngine.GameObject item in list)
			{
				overrides.Add(new global::System.Collections.Generic.KeyValuePair<global::UnityEngine.GameObject, global::UnityEngine.GameObject>(item, this[item]));
			}
		}

		public virtual void Override()
		{
			if (!m_Tile)
			{
				return;
			}
			if (!m_InstanceTile)
			{
				CreateInstanceTile();
			}
			PrepareOverride();
			global::UnityEngine.RuleTile instanceTile = m_InstanceTile;
			instanceTile.m_DefaultSprite = this[instanceTile.m_DefaultSprite] ?? instanceTile.m_DefaultSprite;
			instanceTile.m_DefaultGameObject = this[instanceTile.m_DefaultGameObject] ?? instanceTile.m_DefaultGameObject;
			foreach (global::UnityEngine.RuleTile.TilingRule tilingRule in instanceTile.m_TilingRules)
			{
				for (int i = 0; i < tilingRule.m_Sprites.Length; i++)
				{
					global::UnityEngine.Sprite sprite = tilingRule.m_Sprites[i];
					tilingRule.m_Sprites[i] = this[sprite] ?? sprite;
				}
				tilingRule.m_GameObject = this[tilingRule.m_GameObject] ?? tilingRule.m_GameObject;
			}
		}

		public void PrepareOverride()
		{
			global::UnityEngine.RuleTile tempTile = global::UnityEngine.Object.Instantiate(m_InstanceTile);
			global::System.Collections.Generic.Dictionary<global::System.Reflection.FieldInfo, object> dictionary = global::System.Linq.Enumerable.ToDictionary(m_InstanceTile.GetCustomFields(isOverrideInstance: true), (global::System.Reflection.FieldInfo field) => field, (global::System.Reflection.FieldInfo field) => field.GetValue(tempTile));
			global::UnityEngine.JsonUtility.FromJsonOverwrite(global::UnityEngine.JsonUtility.ToJson(m_Tile), m_InstanceTile);
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Reflection.FieldInfo, object> item in dictionary)
			{
				item.Key.SetValue(m_InstanceTile, item.Value);
			}
		}

		public override bool GetTileAnimationData(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, ref global::UnityEngine.Tilemaps.TileAnimationData tileAnimationData)
		{
			if (!m_InstanceTile)
			{
				return false;
			}
			return m_InstanceTile.GetTileAnimationData(position, tilemap, ref tileAnimationData);
		}

		public override void GetTileData(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, ref global::UnityEngine.Tilemaps.TileData tileData)
		{
			if ((bool)m_InstanceTile)
			{
				m_InstanceTile.GetTileData(position, tilemap, ref tileData);
			}
		}

		public override void RefreshTile(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap)
		{
			if ((bool)m_InstanceTile)
			{
				m_InstanceTile.RefreshTile(position, tilemap);
			}
		}

		public override bool StartUp(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, global::UnityEngine.GameObject go)
		{
			if (!m_InstanceTile)
			{
				return true;
			}
			return m_InstanceTile.StartUp(position, tilemap, go);
		}
	}
}
