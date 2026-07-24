namespace UnityEngine.Tilemaps
{
	[global::System.Serializable]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/AnimatedTile.html")]
	public class AnimatedTile : global::UnityEngine.Tilemaps.TileBase
	{
		public global::UnityEngine.Sprite[] m_AnimatedSprites;

		public float m_MinSpeed = 1f;

		public float m_MaxSpeed = 1f;

		public float m_AnimationStartTime;

		public int m_AnimationStartFrame;

		public global::UnityEngine.Tilemaps.Tile.ColliderType m_TileColliderType;

		public global::UnityEngine.Tilemaps.TileAnimationFlags m_TileAnimationFlags;

		public override void GetTileData(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, ref global::UnityEngine.Tilemaps.TileData tileData)
		{
			tileData.transform = global::UnityEngine.Matrix4x4.identity;
			tileData.color = global::UnityEngine.Color.white;
			if (m_AnimatedSprites != null && m_AnimatedSprites.Length != 0)
			{
				tileData.sprite = m_AnimatedSprites[m_AnimatedSprites.Length - 1];
				tileData.colliderType = m_TileColliderType;
			}
		}

		public override bool GetTileAnimationData(global::UnityEngine.Vector3Int position, global::UnityEngine.Tilemaps.ITilemap tilemap, ref global::UnityEngine.Tilemaps.TileAnimationData tileAnimationData)
		{
			if (m_AnimatedSprites.Length != 0)
			{
				tileAnimationData.animatedSprites = m_AnimatedSprites;
				tileAnimationData.animationSpeed = global::UnityEngine.Random.Range(m_MinSpeed, m_MaxSpeed);
				tileAnimationData.animationStartTime = m_AnimationStartTime;
				tileAnimationData.flags = m_TileAnimationFlags;
				if (0 < m_AnimationStartFrame && m_AnimationStartFrame <= m_AnimatedSprites.Length)
				{
					global::UnityEngine.Tilemaps.Tilemap component = tilemap.GetComponent<global::UnityEngine.Tilemaps.Tilemap>();
					if (component != null && component.animationFrameRate > 0f)
					{
						tileAnimationData.animationStartTime = (float)(m_AnimationStartFrame - 1) / component.animationFrameRate;
					}
				}
				return true;
			}
			return false;
		}
	}
}
