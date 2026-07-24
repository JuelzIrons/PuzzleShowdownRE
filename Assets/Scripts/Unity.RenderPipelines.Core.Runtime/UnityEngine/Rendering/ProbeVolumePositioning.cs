namespace UnityEngine.Rendering
{
	internal static class ProbeVolumePositioning
	{
		internal static global::UnityEngine.Vector3[] m_Axes = new global::UnityEngine.Vector3[6];

		internal static global::UnityEngine.Vector3[] m_AABBCorners = new global::UnityEngine.Vector3[8];

		public static bool OBBIntersect(in global::UnityEngine.Rendering.ProbeReferenceVolume.Volume a, in global::UnityEngine.Rendering.ProbeReferenceVolume.Volume b)
		{
			a.CalculateCenterAndSize(out var center, out var size);
			b.CalculateCenterAndSize(out var center2, out var size2);
			float num = size.sqrMagnitude / 2f;
			float num2 = size2.sqrMagnitude / 2f;
			if (global::UnityEngine.Vector3.SqrMagnitude(center - center2) > num + num2)
			{
				return false;
			}
			m_Axes[0] = a.X.normalized;
			m_Axes[1] = a.Y.normalized;
			m_Axes[2] = a.Z.normalized;
			m_Axes[3] = b.X.normalized;
			m_Axes[4] = b.Y.normalized;
			m_Axes[5] = b.Z.normalized;
			for (int i = 0; i < 6; i++)
			{
				global::UnityEngine.Vector2 vector = ProjectOBB(in a, m_Axes[i]);
				global::UnityEngine.Vector2 vector2 = ProjectOBB(in b, m_Axes[i]);
				if (vector.y < vector2.x || vector2.y < vector.x)
				{
					return false;
				}
			}
			return true;
		}

		public static bool OBBContains(in global::UnityEngine.Rendering.ProbeReferenceVolume.Volume obb, global::UnityEngine.Vector3 point)
		{
			float sqrMagnitude = obb.X.sqrMagnitude;
			float sqrMagnitude2 = obb.Y.sqrMagnitude;
			float sqrMagnitude3 = obb.Z.sqrMagnitude;
			point -= obb.corner;
			point = new global::UnityEngine.Vector3(global::UnityEngine.Vector3.Dot(point, obb.X), global::UnityEngine.Vector3.Dot(point, obb.Y), global::UnityEngine.Vector3.Dot(point, obb.Z));
			if (0f < point.x && point.x < sqrMagnitude && 0f < point.y && point.y < sqrMagnitude2)
			{
				if (0f < point.z)
				{
					return point.z < sqrMagnitude3;
				}
				return false;
			}
			return false;
		}

		public static bool OBBAABBIntersect(in global::UnityEngine.Rendering.ProbeReferenceVolume.Volume a, in global::UnityEngine.Bounds b, in global::UnityEngine.Bounds aAABB)
		{
			if (!aAABB.Intersects(b))
			{
				return false;
			}
			global::UnityEngine.Vector3 min = b.min;
			global::UnityEngine.Vector3 max = b.max;
			m_AABBCorners[0] = new global::UnityEngine.Vector3(min.x, min.y, min.z);
			m_AABBCorners[1] = new global::UnityEngine.Vector3(max.x, min.y, min.z);
			m_AABBCorners[2] = new global::UnityEngine.Vector3(max.x, max.y, min.z);
			m_AABBCorners[3] = new global::UnityEngine.Vector3(min.x, max.y, min.z);
			m_AABBCorners[4] = new global::UnityEngine.Vector3(min.x, min.y, max.z);
			m_AABBCorners[5] = new global::UnityEngine.Vector3(max.x, min.y, max.z);
			m_AABBCorners[6] = new global::UnityEngine.Vector3(max.x, max.y, max.z);
			m_AABBCorners[7] = new global::UnityEngine.Vector3(min.x, max.y, max.z);
			m_Axes[0] = a.X.normalized;
			m_Axes[1] = a.Y.normalized;
			m_Axes[2] = a.Z.normalized;
			for (int i = 0; i < 3; i++)
			{
				global::UnityEngine.Vector2 vector = ProjectOBB(in a, m_Axes[i]);
				global::UnityEngine.Vector2 vector2 = ProjectAABB(in m_AABBCorners, m_Axes[i]);
				if (vector.y < vector2.x || vector2.y < vector.x)
				{
					return false;
				}
			}
			return true;
		}

		private static global::UnityEngine.Vector2 ProjectOBB(in global::UnityEngine.Rendering.ProbeReferenceVolume.Volume a, global::UnityEngine.Vector3 axis)
		{
			float num = global::UnityEngine.Vector3.Dot(axis, a.corner);
			float num2 = num;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 2; k++)
					{
						global::UnityEngine.Vector3 rhs = a.corner + a.X * i + a.Y * j + a.Z * k;
						float num3 = global::UnityEngine.Vector3.Dot(axis, rhs);
						if (num3 < num)
						{
							num = num3;
						}
						else if (num3 > num2)
						{
							num2 = num3;
						}
					}
				}
			}
			return new global::UnityEngine.Vector2(num, num2);
		}

		private static global::UnityEngine.Vector2 ProjectAABB(in global::UnityEngine.Vector3[] corners, global::UnityEngine.Vector3 axis)
		{
			float num = global::UnityEngine.Vector3.Dot(axis, corners[0]);
			float num2 = num;
			for (int i = 1; i < 8; i++)
			{
				float num3 = global::UnityEngine.Vector3.Dot(axis, corners[i]);
				if (num3 < num)
				{
					num = num3;
				}
				else if (num3 > num2)
				{
					num2 = num3;
				}
			}
			return new global::UnityEngine.Vector2(num, num2);
		}
	}
}
