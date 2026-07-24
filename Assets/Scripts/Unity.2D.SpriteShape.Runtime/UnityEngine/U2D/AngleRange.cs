namespace UnityEngine.U2D
{
	[global::System.Serializable]
	public class AngleRange : global::System.ICloneable
	{
		[global::UnityEngine.SerializeField]
		private float m_Start;

		[global::UnityEngine.SerializeField]
		private float m_End;

		[global::UnityEngine.SerializeField]
		private int m_Order;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.Sprite> m_Sprites = new global::System.Collections.Generic.List<global::UnityEngine.Sprite>();

		public float start
		{
			get
			{
				return m_Start;
			}
			set
			{
				m_Start = value;
			}
		}

		public float end
		{
			get
			{
				return m_End;
			}
			set
			{
				m_End = value;
			}
		}

		public int order
		{
			get
			{
				return m_Order;
			}
			set
			{
				m_Order = value;
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
			global::UnityEngine.U2D.AngleRange obj = MemberwiseClone() as global::UnityEngine.U2D.AngleRange;
			obj.sprites = new global::System.Collections.Generic.List<global::UnityEngine.Sprite>(obj.sprites);
			return obj;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is global::UnityEngine.U2D.AngleRange angleRange))
			{
				return false;
			}
			if (!start.Equals(angleRange.start) || !end.Equals(angleRange.end) || !order.Equals(angleRange.order))
			{
				return false;
			}
			if (sprites.Count != angleRange.sprites.Count)
			{
				return false;
			}
			for (int i = 0; i < sprites.Count; i++)
			{
				if (sprites[i] != angleRange.sprites[i])
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int num = start.GetHashCode() ^ end.GetHashCode() ^ order.GetHashCode();
			if (sprites != null)
			{
				for (int i = 0; i < sprites.Count; i++)
				{
					global::UnityEngine.Sprite sprite = sprites[i];
					if ((bool)sprite)
					{
						num = (num * 16777619) ^ (sprite.GetHashCode() + i);
					}
				}
			}
			return num;
		}
	}
}
