namespace DG.Tweening
{
	public struct Color2
	{
		public global::UnityEngine.Color ca;

		public global::UnityEngine.Color cb;

		public Color2(global::UnityEngine.Color ca, global::UnityEngine.Color cb)
		{
			this.ca = ca;
			this.cb = cb;
		}

		public static global::DG.Tweening.Color2 operator +(global::DG.Tweening.Color2 c1, global::DG.Tweening.Color2 c2)
		{
			return new global::DG.Tweening.Color2(c1.ca + c2.ca, c1.cb + c2.cb);
		}

		public static global::DG.Tweening.Color2 operator -(global::DG.Tweening.Color2 c1, global::DG.Tweening.Color2 c2)
		{
			return new global::DG.Tweening.Color2(c1.ca - c2.ca, c1.cb - c2.cb);
		}

		public static global::DG.Tweening.Color2 operator *(global::DG.Tweening.Color2 c1, float f)
		{
			return new global::DG.Tweening.Color2(c1.ca * f, c1.cb * f);
		}
	}
}
