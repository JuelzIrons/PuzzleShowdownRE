namespace UnityEngine.U2D
{
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.spriteshape@latest/index.html?subfolder=/manual/SSProfile.html")]
	public class SpriteShape : global::UnityEngine.ScriptableObject
	{
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.AngleRange> m_Angles = new global::System.Collections.Generic.List<global::UnityEngine.U2D.AngleRange>();

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Texture2D m_FillTexture;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.CornerSprite> m_CornerSprites = new global::System.Collections.Generic.List<global::UnityEngine.U2D.CornerSprite>();

		[global::UnityEngine.SerializeField]
		private float m_FillOffset;

		[global::UnityEngine.SerializeField]
		private bool m_UseSpriteBorders = true;

		public global::System.Collections.Generic.List<global::UnityEngine.U2D.AngleRange> angleRanges
		{
			get
			{
				return m_Angles;
			}
			set
			{
				m_Angles = value;
			}
		}

		public global::UnityEngine.Texture2D fillTexture
		{
			get
			{
				return m_FillTexture;
			}
			set
			{
				m_FillTexture = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.U2D.CornerSprite> cornerSprites
		{
			get
			{
				return m_CornerSprites;
			}
			set
			{
				m_CornerSprites = value;
			}
		}

		public float fillOffset
		{
			get
			{
				return m_FillOffset;
			}
			set
			{
				m_FillOffset = value;
			}
		}

		public bool useSpriteBorders
		{
			get
			{
				return m_UseSpriteBorders;
			}
			set
			{
				m_UseSpriteBorders = value;
			}
		}

		private global::UnityEngine.U2D.CornerSprite GetCornerSprite(global::UnityEngine.U2D.CornerType cornerType)
		{
			global::UnityEngine.U2D.CornerSprite cornerSprite = new global::UnityEngine.U2D.CornerSprite();
			cornerSprite.cornerType = cornerType;
			cornerSprite.sprites = new global::System.Collections.Generic.List<global::UnityEngine.Sprite>();
			cornerSprite.sprites.Insert(0, null);
			return cornerSprite;
		}

		private void ResetCornerList()
		{
			m_CornerSprites.Clear();
			m_CornerSprites.Insert(0, GetCornerSprite(global::UnityEngine.U2D.CornerType.OuterTopLeft));
			m_CornerSprites.Insert(1, GetCornerSprite(global::UnityEngine.U2D.CornerType.OuterTopRight));
			m_CornerSprites.Insert(2, GetCornerSprite(global::UnityEngine.U2D.CornerType.OuterBottomLeft));
			m_CornerSprites.Insert(3, GetCornerSprite(global::UnityEngine.U2D.CornerType.OuterBottomRight));
			m_CornerSprites.Insert(4, GetCornerSprite(global::UnityEngine.U2D.CornerType.InnerTopLeft));
			m_CornerSprites.Insert(5, GetCornerSprite(global::UnityEngine.U2D.CornerType.InnerTopRight));
			m_CornerSprites.Insert(6, GetCornerSprite(global::UnityEngine.U2D.CornerType.InnerBottomLeft));
			m_CornerSprites.Insert(7, GetCornerSprite(global::UnityEngine.U2D.CornerType.InnerBottomRight));
		}

		private void OnValidate()
		{
			if (m_CornerSprites.Count != 8)
			{
				ResetCornerList();
			}
		}

		private void Reset()
		{
			m_Angles.Clear();
			ResetCornerList();
		}

		internal static int GetSpriteShapeHashCode(global::UnityEngine.U2D.SpriteShape spriteShape)
		{
			int num = -2128831035;
			num = (num * 16777619) ^ spriteShape.angleRanges.Count;
			for (int i = 0; i < spriteShape.angleRanges.Count; i++)
			{
				num = (num * 16777619) ^ (spriteShape.angleRanges[i].GetHashCode() + i);
			}
			num = (num * 16777619) ^ spriteShape.cornerSprites.Count;
			for (int j = 0; j < spriteShape.cornerSprites.Count; j++)
			{
				num = (num * 16777619) ^ (spriteShape.cornerSprites[j].GetHashCode() + j);
			}
			return num;
		}
	}
}
