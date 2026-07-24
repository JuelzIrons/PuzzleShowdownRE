namespace Unity.SpriteShape.External.LibTessDotNet
{
	internal struct Vec3
	{
		public static readonly global::Unity.SpriteShape.External.LibTessDotNet.Vec3 Zero;

		public float X;

		public float Y;

		public float Z;

		public float this[int index]
		{
			get
			{
				return index switch
				{
					0 => X, 
					1 => Y, 
					2 => Z, 
					_ => throw new global::System.IndexOutOfRangeException(), 
				};
			}
			set
			{
				switch (index)
				{
				case 0:
					X = value;
					break;
				case 1:
					Y = value;
					break;
				case 2:
					Z = value;
					break;
				default:
					throw new global::System.IndexOutOfRangeException();
				}
			}
		}

		public static void Sub(ref global::Unity.SpriteShape.External.LibTessDotNet.Vec3 lhs, ref global::Unity.SpriteShape.External.LibTessDotNet.Vec3 rhs, out global::Unity.SpriteShape.External.LibTessDotNet.Vec3 result)
		{
			result.X = lhs.X - rhs.X;
			result.Y = lhs.Y - rhs.Y;
			result.Z = lhs.Z - rhs.Z;
		}

		public static void Neg(ref global::Unity.SpriteShape.External.LibTessDotNet.Vec3 v)
		{
			v.X = 0f - v.X;
			v.Y = 0f - v.Y;
			v.Z = 0f - v.Z;
		}

		public static void Dot(ref global::Unity.SpriteShape.External.LibTessDotNet.Vec3 u, ref global::Unity.SpriteShape.External.LibTessDotNet.Vec3 v, out float dot)
		{
			dot = u.X * v.X + u.Y * v.Y + u.Z * v.Z;
		}

		public static void Normalize(ref global::Unity.SpriteShape.External.LibTessDotNet.Vec3 v)
		{
			float num = v.X * v.X + v.Y * v.Y + v.Z * v.Z;
			num = 1f / (float)global::System.Math.Sqrt(num);
			v.X *= num;
			v.Y *= num;
			v.Z *= num;
		}

		public static int LongAxis(ref global::Unity.SpriteShape.External.LibTessDotNet.Vec3 v)
		{
			int num = 0;
			if (global::System.Math.Abs(v.Y) > global::System.Math.Abs(v.X))
			{
				num = 1;
			}
			if (global::System.Math.Abs(v.Z) > global::System.Math.Abs((num == 0) ? v.X : v.Y))
			{
				num = 2;
			}
			return num;
		}

		public override string ToString()
		{
			return $"{X}, {Y}, {Z}";
		}
	}
}
