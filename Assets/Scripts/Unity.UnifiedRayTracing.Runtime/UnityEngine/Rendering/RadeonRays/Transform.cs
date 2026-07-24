namespace UnityEngine.Rendering.RadeonRays
{
	internal struct Transform
	{
		public global::Unity.Mathematics.float4 row0;

		public global::Unity.Mathematics.float4 row1;

		public global::Unity.Mathematics.float4 row2;

		public Transform(global::Unity.Mathematics.float4 row0, global::Unity.Mathematics.float4 row1, global::Unity.Mathematics.float4 row2)
		{
			this.row0 = row0;
			this.row1 = row1;
			this.row2 = row2;
		}

		public static global::UnityEngine.Rendering.RadeonRays.Transform Identity()
		{
			return new global::UnityEngine.Rendering.RadeonRays.Transform(new global::Unity.Mathematics.float4(1f, 0f, 0f, 0f), new global::Unity.Mathematics.float4(0f, 1f, 0f, 0f), new global::Unity.Mathematics.float4(0f, 0f, 1f, 0f));
		}

		public static global::UnityEngine.Rendering.RadeonRays.Transform Translation(global::Unity.Mathematics.float3 translation)
		{
			return new global::UnityEngine.Rendering.RadeonRays.Transform(new global::Unity.Mathematics.float4(1f, 0f, 0f, translation.x), new global::Unity.Mathematics.float4(0f, 1f, 0f, translation.y), new global::Unity.Mathematics.float4(0f, 0f, 1f, translation.z));
		}

		public static global::UnityEngine.Rendering.RadeonRays.Transform Scale(global::Unity.Mathematics.float3 scale)
		{
			return new global::UnityEngine.Rendering.RadeonRays.Transform(new global::Unity.Mathematics.float4(scale.x, 0f, 0f, 0f), new global::Unity.Mathematics.float4(0f, scale.y, 0f, 0f), new global::Unity.Mathematics.float4(0f, 0f, scale.z, 0f));
		}

		public static global::UnityEngine.Rendering.RadeonRays.Transform TRS(global::Unity.Mathematics.float3 translation, global::Unity.Mathematics.float3 rotation, global::Unity.Mathematics.float3 scale)
		{
			global::Unity.Mathematics.float3x3 float3x5 = global::Unity.Mathematics.float3x3.Euler(rotation);
			float3x5.c0 *= scale.x;
			float3x5.c1 *= scale.y;
			float3x5.c2 *= scale.z;
			return new global::UnityEngine.Rendering.RadeonRays.Transform(new global::Unity.Mathematics.float4(float3x5.c0.x, float3x5.c1.x, float3x5.c2.x, translation.x), new global::Unity.Mathematics.float4(float3x5.c0.y, float3x5.c1.y, float3x5.c2.y, translation.y), new global::Unity.Mathematics.float4(float3x5.c0.z, float3x5.c1.z, float3x5.c2.z, translation.z));
		}

		public global::UnityEngine.Rendering.RadeonRays.Transform Inverse()
		{
			global::Unity.Mathematics.float3x3 m = default(global::Unity.Mathematics.float3x3);
			m[0] = new global::Unity.Mathematics.float3(row0.x, row1.x, row2.x);
			m[1] = new global::Unity.Mathematics.float3(row0.y, row1.y, row2.y);
			m[2] = new global::Unity.Mathematics.float3(row0.z, row1.z, row2.z);
			m = global::Unity.Mathematics.math.inverse(m);
			global::Unity.Mathematics.float3 float5 = -global::Unity.Mathematics.math.mul(m, new global::Unity.Mathematics.float3(row0.w, row1.w, row2.w));
			global::UnityEngine.Rendering.RadeonRays.Transform result = default(global::UnityEngine.Rendering.RadeonRays.Transform);
			result.row0 = new global::Unity.Mathematics.float4(m[0].x, m[1].x, m[2].x, float5.x);
			result.row1 = new global::Unity.Mathematics.float4(m[0].y, m[1].y, m[2].y, float5.y);
			result.row2 = new global::Unity.Mathematics.float4(m[0].z, m[1].z, m[2].z, float5.z);
			return result;
		}
	}
}
