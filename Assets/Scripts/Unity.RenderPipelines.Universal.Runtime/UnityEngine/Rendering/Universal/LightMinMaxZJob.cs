namespace UnityEngine.Rendering.Universal
{
	[global::Unity.Burst.BurstCompile]
	internal struct LightMinMaxZJob : global::Unity.Jobs.IJobFor
	{
		public global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4x4> worldToViews;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> lights;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> minMaxZs;

		public void Execute(int index)
		{
			int index2 = index % lights.Length;
			global::UnityEngine.Rendering.VisibleLight visibleLight = lights[index2];
			global::Unity.Mathematics.float4x4 float4x5 = visibleLight.localToWorldMatrix;
			global::Unity.Mathematics.float3 xyz = float4x5.c3.xyz;
			int index3 = index / lights.Length;
			global::Unity.Mathematics.float4x4 a = worldToViews[index3];
			global::Unity.Mathematics.float3 xyz2 = global::Unity.Mathematics.math.mul(a, global::Unity.Mathematics.math.float4(xyz, 1f)).xyz;
			xyz2.z *= -1f;
			global::Unity.Mathematics.float2 value = global::Unity.Mathematics.math.float2(xyz2.z - visibleLight.range, xyz2.z + visibleLight.range);
			if (visibleLight.lightType == global::UnityEngine.LightType.Spot)
			{
				float num = global::Unity.Mathematics.math.radians(visibleLight.spotAngle) * 0.5f;
				float num2 = global::Unity.Mathematics.math.cos(num);
				float num3 = visibleLight.range * num2;
				global::Unity.Mathematics.float3 xyz3 = float4x5.c2.xyz;
				global::Unity.Mathematics.float3 xyz4 = xyz + xyz3 * num3;
				global::Unity.Mathematics.float3 xyz5 = global::Unity.Mathematics.math.mul(a, global::Unity.Mathematics.math.float4(xyz4, 1f)).xyz;
				xyz5.z *= -1f;
				float x = global::System.MathF.PI / 2f - num;
				float num4 = visibleLight.range * num2 * global::Unity.Mathematics.math.sin(num) / global::Unity.Mathematics.math.sin(x);
				global::Unity.Mathematics.float3 float5 = xyz5 - xyz2;
				float num5 = global::Unity.Mathematics.math.sqrt(1f - float5.z * float5.z / global::Unity.Mathematics.math.dot(float5, float5));
				if (0f - float5.z < num3 * num2)
				{
					value.x = global::Unity.Mathematics.math.min(xyz2.z, xyz5.z - num5 * num4);
				}
				if (float5.z < num3 * num2)
				{
					value.y = global::Unity.Mathematics.math.max(xyz2.z, xyz5.z + num5 * num4);
				}
			}
			else if (visibleLight.lightType != global::UnityEngine.LightType.Point)
			{
				value.x = float.MaxValue;
				value.y = float.MinValue;
			}
			value.x = global::Unity.Mathematics.math.max(value.x, 0f);
			value.y = global::Unity.Mathematics.math.max(value.y, 0f);
			minMaxZs[index] = value;
		}
	}
}
