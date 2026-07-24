namespace UnityEngine.U2D
{
	[global::System.Serializable]
	public class CornerSprite : global::System.ICloneable
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.CornerType m_CornerType;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.Sprite> m_Sprites;

		public global::UnityEngine.U2D.CornerType cornerType
		{
			get
			{
				return m_CornerType;
			}
			set
			{
				m_CornerType = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Sprite> sprites
		{
			get
			{
				return m_Sprites;
			}
			set
			{
				m_Sprites = value;
			}
		}

		public object Clone()
		{
			global::UnityEngine.U2D.CornerSprite obj = MemberwiseClone() as global::UnityEngine.U2D.CornerSprite;
			obj.sprites = new global::System.Collections.Generic.List<global::UnityEngine.Sprite>(obj.sprites);
			return obj;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is global::UnityEngine.U2D.CornerSprite cornerSprite))
			{
				return false;
			}
			if (!cornerType.Equals(cornerSprite.cornerType))
			{
				return false;
			}
			if (sprites.Count != cornerSprite.sprites.Count)
			{
				return false;
			}
			for (int i = 0; i < sprites.Count; i++)
			{
				if (sprites[i] != cornerSprite.sprites[i])
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int num = cornerType.GetHashCode();
			if (sprites != null)
			{
				for (int i = 0; i < sprites.Count; i++)
				{
					global::UnityEngine.Sprite sprite = sprites[i];
					if ((bool)sprite)
					{
						num ^= i + 1;
						num ^= sprite.GetHashCode();
					}
				}
			}
			return num;
		}
	}
}
