namespace UnityEngine.Tilemaps
{
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/AutoTile.html")]
	public class AutoTile : global::UnityEngine.Tilemaps.TileBase
	{
		[global::System.Serializable]
		internal abstract class SerializedDictionary<TKey, TValue> : global::System.Collections.Generic.Dictionary<TKey, TValue>, global::UnityEngine.ISerializationCallbackReceiver
		{
			[global::UnityEngine.SerializeField]
			[global::UnityEngine.HideInInspector]
			private global::System.Collections.Generic.List<TKey> keyData = new global::System.Collections.Generic.List<TKey>();

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.HideInInspector]
			private global::System.Collections.Generic.List<TValue> valueData = new global::System.Collections.Generic.List<TValue>();

			void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
			{
				Clear();
				for (int i = 0; i < keyData.Count && i < valueData.Count; i++)
				{
					base[keyData[i]] = valueData[i];
				}
				keyData.Clear();
				valueData.Clear();
			}

			void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
			{
				keyData.Clear();
				valueData.Clear();
				using global::System.Collections.Generic.Dictionary<TKey, TValue>.Enumerator enumerator = GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::System.Collections.Generic.KeyValuePair<TKey, TValue> current = enumerator.Current;
					keyData.Add(current.Key);
					valueData.Add(current.Value);
				}
			}
		}

		[global::System.Serializable]
		internal class AutoTileData
		{
			[global::UnityEngine.SerializeField]
			public global::System.Collections.Generic.List<global::UnityEngine.Sprite> spriteList = new global::System.Collections.Generic.List<global::UnityEngine.Sprite>();

			[global::UnityEngine.SerializeField]
			public global::System.Collections.Generic.List<global::UnityEngine.Texture2D> textureList = new global::System.Collections.Generic.List<global::UnityEngine.Texture2D>();
		}

		[global::System.Serializable]
		internal class AutoTileDictionary : global::UnityEngine.Tilemaps.AutoTile.SerializedDictionary<uint, global::UnityEngine.Tilemaps.AutoTile.AutoTileData>
		{
		}

		public enum AutoTileMaskType
		{
			Mask_2x2 = 0,
			Mask_3x3 = 1
		}

		internal static readonly float s_DefaultTextureScale = 1f;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Sprite m_DefaultSprite;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.GameObject m_DefaultGameObject;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Tilemaps.Tile.ColliderType m_DefaultColliderType = global::UnityEngine.Tilemaps.Tile.ColliderType.Sprite;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Tilemaps.AutoTile.AutoTileMaskType m_MaskType;

		[global::UnityEngine.SerializeField]
		private bool m_Random;

		[global::UnityEngine.SerializeField]
		private bool m_PhysicsShapeCheck;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		internal global::UnityEngine.Tilemaps.AutoTile.AutoTileDictionary m_AutoTileDictionary = new global::UnityEngine.Tilemaps.AutoTile.AutoTileDictionary();

		[global::UnityEngine.SerializeField]
		public global::System.Collections.Generic.List<global::UnityEngine.Texture2D> m_TextureList = new global::System.Collections.Generic.List<global::UnityEngine.Texture2D>();

		[global::UnityEngine.SerializeField]
		public global::System.Collections.Generic.List<float> m_TextureScaleList = new global::System.Collections.Generic.List<float>();

		private readonly global::UnityEngine.Tilemaps.TileBase[] m_CachedTiles = new global::UnityEngine.Tilemaps.TileBase[9];

		public bool random
		{
			get
			{
				return m_Random;
			}
			set
			{
				m_Random = value;
			}
		}

		internal bool physicsShapeCheck
		{
			get
			{
				return m_PhysicsShapeCheck;
			}
			set
			{
				m_PhysicsShapeCheck = value;
			}
		}

		public override void RefreshTile(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap)
		{
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					tilemap.RefreshTile(new global::UnityEngine.Vector3Int(position.x + j, position.y + i, position.z));
				}
			}
		}

		public override void GetTileData(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap itilemap, ref global::UnityEngine.Tilemaps.TileData tileData)
		{
			global::UnityEngine.Matrix4x4 identity = global::UnityEngine.Matrix4x4.identity;
			tileData.sprite = m_DefaultSprite;
			tileData.gameObject = m_DefaultGameObject;
			tileData.colliderType = m_DefaultColliderType;
			tileData.flags = global::UnityEngine.Tilemaps.TileFlags.LockAll;
			tileData.transform = identity;
			uint num = 0u;
			int num2 = 0;
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					global::UnityEngine.Vector3Int position2 = new global::UnityEngine.Vector3Int(position.x + j, position.y + i, position.z);
					m_CachedTiles[num2] = itilemap.GetTile(position2);
					if (m_CachedTiles[num2] == this)
					{
						num |= (uint)(1 << num2);
					}
					num2++;
				}
			}
			num = m_MaskType switch
			{
				global::UnityEngine.Tilemaps.AutoTile.AutoTileMaskType.Mask_2x2 => Convert2x2Mask(num), 
				global::UnityEngine.Tilemaps.AutoTile.AutoTileMaskType.Mask_3x3 => Convert3x3Mask(num), 
				_ => num, 
			};
			if (m_AutoTileDictionary.TryGetValue(num, out var value))
			{
				global::UnityEngine.Sprite sprite = m_DefaultSprite;
				if (value.spriteList.Count > 0)
				{
					if (m_Random)
					{
						long num3 = position.x;
						num3 = num3 + 2882343476u + (num3 << 15);
						num3 = (num3 + 159903659) ^ (num3 >> 11);
						num3 ^= position.y;
						num3 = num3 + 1185682173 + (num3 << 7);
						num3 = (num3 + 3197579439u) ^ (num3 << 11);
						global::UnityEngine.Random.State state = global::UnityEngine.Random.state;
						global::UnityEngine.Random.InitState((int)num3);
						sprite = value.spriteList[global::UnityEngine.Random.Range(0, value.spriteList.Count)];
						global::UnityEngine.Random.state = state;
					}
					else
					{
						sprite = value.spriteList[0];
					}
				}
				tileData.sprite = sprite;
			}
			if (physicsShapeCheck && tileData.sprite != null && tileData.colliderType == global::UnityEngine.Tilemaps.Tile.ColliderType.Sprite)
			{
				tileData.colliderType = ((tileData.sprite.GetPhysicsShapeCount() > 0) ? global::UnityEngine.Tilemaps.Tile.ColliderType.Sprite : global::UnityEngine.Tilemaps.Tile.ColliderType.None);
			}
		}

		internal void AddSprite(global::UnityEngine.Sprite sprite, global::UnityEngine.Texture2D texture, uint mask)
		{
			if ((m_MaskType == global::UnityEngine.Tilemaps.AutoTile.AutoTileMaskType.Mask_2x2 && mask >> 4 != 0) || mask >> 9 != 0)
			{
				throw new global::System.ArgumentOutOfRangeException($"Mask {mask} is not valid for {m_MaskType}");
			}
			if (!m_AutoTileDictionary.TryGetValue(mask, out var value))
			{
				value = new global::UnityEngine.Tilemaps.AutoTile.AutoTileData();
				m_AutoTileDictionary.Add(mask, value);
			}
			bool flag = false;
			foreach (global::UnityEngine.Sprite sprite2 in value.spriteList)
			{
				flag = sprite2 == sprite;
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				value.spriteList.Add(sprite);
				value.textureList.Add(texture);
			}
		}

		internal void RemoveSprite(global::UnityEngine.Sprite sprite, uint mask)
		{
			if (m_AutoTileDictionary.TryGetValue(mask, out var value))
			{
				int num = value.spriteList.IndexOf(sprite);
				if (num >= 0)
				{
					value.spriteList.RemoveAt(num);
					value.textureList.RemoveAt(num);
				}
			}
		}

		public void Validate()
		{
			if (m_MaskType == global::UnityEngine.Tilemaps.AutoTile.AutoTileMaskType.Mask_2x2)
			{
				foreach (uint item2 in new global::System.Collections.Generic.List<uint>(m_AutoTileDictionary.Keys))
				{
					if (item2 >> 4 != 0)
					{
						m_AutoTileDictionary.Remove(item2);
					}
				}
			}
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::UnityEngine.Tilemaps.AutoTile.AutoTileData> item3 in m_AutoTileDictionary)
			{
				global::UnityEngine.Tilemaps.AutoTile.AutoTileData value = item3.Value;
				int num = 0;
				while (num < value.spriteList.Count)
				{
					_ = value.spriteList[num];
					global::UnityEngine.Texture2D item = value.textureList[num];
					if (m_TextureList.Contains(item))
					{
						num++;
						continue;
					}
					value.spriteList.RemoveAt(num);
					value.textureList.RemoveAt(num);
				}
			}
			if (m_TextureList.Count == m_TextureScaleList.Count)
			{
				return;
			}
			if (m_TextureList.Count > m_TextureScaleList.Count)
			{
				while (m_TextureList.Count - m_TextureScaleList.Count > 0)
				{
					m_TextureScaleList.Add(s_DefaultTextureScale);
				}
			}
			else if (m_TextureList.Count < m_TextureScaleList.Count)
			{
				while (m_TextureScaleList.Count - m_TextureList.Count > 0)
				{
					m_TextureScaleList.RemoveAt(m_TextureScaleList.Count - 1);
				}
			}
		}

		private uint Convert2x2Mask(uint mask)
		{
			uint num = 0u;
			if ((mask & 1) != 0 && (mask & 2) != 0 && (mask & 8) != 0)
			{
				num |= 1;
			}
			if ((mask & 2) != 0 && (mask & 4) != 0 && (mask & 0x20) != 0)
			{
				num |= 2;
			}
			if ((mask & 8) != 0 && (mask & 0x40) != 0 && (mask & 0x80) != 0)
			{
				num |= 4;
			}
			if ((mask & 0x20) != 0 && (mask & 0x80) != 0 && (mask & 0x100) != 0)
			{
				num |= 8;
			}
			return num;
		}

		private uint Convert3x3Mask(uint mask)
		{
			switch (mask)
			{
			case 49u:
			case 52u:
			case 53u:
			case 112u:
			case 113u:
			case 116u:
			case 117u:
			case 304u:
			case 305u:
			case 308u:
			case 309u:
			case 368u:
			case 369u:
			case 372u:
			case 373u:
				mask = 48u;
				break;
			case 25u:
			case 28u:
			case 29u:
			case 88u:
			case 89u:
			case 92u:
			case 93u:
			case 280u:
			case 281u:
			case 284u:
			case 285u:
			case 344u:
			case 345u:
			case 348u:
			case 349u:
				mask = 24u;
				break;
			case 19u:
			case 22u:
			case 23u:
			case 82u:
			case 83u:
			case 86u:
			case 87u:
			case 274u:
			case 275u:
			case 278u:
			case 279u:
			case 338u:
			case 339u:
			case 342u:
			case 343u:
				mask = 18u;
				break;
			case 145u:
			case 148u:
			case 149u:
			case 208u:
			case 209u:
			case 212u:
			case 213u:
			case 400u:
			case 401u:
			case 404u:
			case 405u:
			case 464u:
			case 465u:
			case 468u:
			case 469u:
				mask = 144u;
				break;
			case 147u:
			case 150u:
			case 151u:
			case 210u:
			case 211u:
			case 214u:
			case 215u:
			case 402u:
			case 403u:
			case 406u:
			case 407u:
			case 455u:
			case 466u:
			case 467u:
			case 470u:
			case 471u:
				mask = 146u;
				break;
			case 57u:
			case 60u:
			case 61u:
			case 120u:
			case 121u:
			case 124u:
			case 125u:
			case 312u:
			case 313u:
			case 316u:
			case 317u:
			case 376u:
			case 377u:
			case 380u:
			case 381u:
				mask = 56u;
				break;
			case 55u:
			case 118u:
			case 119u:
			case 310u:
			case 311u:
			case 374u:
			case 375u:
				mask = 54u;
				break;
			case 433u:
			case 436u:
			case 437u:
			case 496u:
			case 497u:
			case 500u:
			case 501u:
				mask = 432u;
				break;
			case 31u:
			case 91u:
			case 95u:
			case 283u:
			case 287u:
			case 347u:
			case 351u:
				mask = 27u;
				break;
			case 217u:
			case 220u:
			case 221u:
			case 472u:
			case 473u:
			case 476u:
			case 477u:
				mask = 216u;
				break;
			case 127u:
			case 319u:
			case 383u:
				mask = 63u;
				break;
			case 505u:
			case 508u:
			case 509u:
				mask = 504u;
				break;
			case 439u:
			case 502u:
			case 503u:
				mask = 438u;
				break;
			case 223u:
			case 475u:
			case 479u:
				mask = 219u;
				break;
			case 51u:
			case 114u:
			case 115u:
			case 306u:
			case 307u:
			case 370u:
			case 371u:
				mask = 50u;
				break;
			case 177u:
			case 180u:
			case 181u:
			case 240u:
			case 241u:
			case 244u:
			case 245u:
				mask = 176u;
				break;
			case 30u:
			case 90u:
			case 94u:
			case 282u:
			case 286u:
			case 346u:
			case 350u:
				mask = 26u;
				break;
			case 153u:
			case 156u:
			case 157u:
			case 408u:
			case 409u:
			case 412u:
			case 413u:
				mask = 152u;
				break;
			case 158u:
			case 410u:
			case 414u:
				mask = 154u;
				break;
			case 179u:
			case 242u:
			case 243u:
				mask = 178u;
				break;
			case 185u:
			case 188u:
			case 189u:
				mask = 184u;
				break;
			case 122u:
			case 314u:
			case 378u:
				mask = 58u;
				break;
			case 126u:
			case 318u:
			case 382u:
				mask = 62u;
				break;
			case 441u:
			case 444u:
			case 445u:
				mask = 440u;
				break;
			case 123u:
			case 315u:
			case 379u:
				mask = 59u;
				break;
			case 249u:
			case 252u:
			case 253u:
				mask = 248u;
				break;
			case 183u:
			case 246u:
			case 247u:
				mask = 182u;
				break;
			case 435u:
			case 498u:
			case 499u:
				mask = 434u;
				break;
			case 159u:
			case 411u:
			case 415u:
				mask = 155u;
				break;
			case 222u:
			case 474u:
			case 478u:
				mask = 218u;
				break;
			case 17u:
			case 20u:
			case 21u:
			case 80u:
			case 81u:
			case 84u:
			case 85u:
			case 272u:
			case 273u:
			case 276u:
			case 277u:
			case 336u:
			case 337u:
			case 340u:
			case 341u:
				mask = 16u;
				break;
			}
			return mask;
		}
	}
}
