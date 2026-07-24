namespace UnityEngine.Rendering.RadeonRays
{
	internal class AABB
	{
		public global::Unity.Mathematics.float3 Min;

		public global::Unity.Mathematics.float3 Max;

		public AABB()
		{
			Min = new global::Unity.Mathematics.float3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			Max = new global::Unity.Mathematics.float3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
		}

		public AABB(global::Unity.Mathematics.float3 min, global::Unity.Mathematics.float3 max)
		{
			Min = min;
			Max = max;
		}

		public void Encapsulate(global::UnityEngine.Rendering.RadeonRays.AABB aabb)
		{
			Min = global::Unity.Mathematics.math.min(Min, aabb.Min);
			Max = global::Unity.Mathematics.math.max(Max, aabb.Max);
		}

		public void Encapsulate(global::Unity.Mathematics.float3 point)
		{
			Min = global::Unity.Mathematics.math.min(Min, point);
			Max = global::Unity.Mathematics.math.max(Max, point);
		}

		public bool Contains(global::UnityEngine.Rendering.RadeonRays.AABB rhs)
		{
			if (rhs.Min.x >= Min.x && rhs.Min.y >= Min.y && rhs.Min.z >= Min.z && rhs.Max.x <= Max.x && rhs.Max.y <= Max.y)
			{
				return rhs.Max.z <= Max.z;
			}
			return false;
		}

		public bool IsValid()
		{
			if (Min.x <= Max.x && Min.y <= Max.y)
			{
				return Min.z <= Max.z;
			}
			return false;
		}
	}
}
