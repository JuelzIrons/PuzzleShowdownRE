namespace UnityEngine.Rendering.Universal
{
	[global::Unity.Burst.BurstCompile]
	internal struct ReflectionProbeMinMaxZJob : global::Unity.Jobs.IJobFor
	{
		public global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4x4> worldToViews;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleReflectionProbe> reflectionProbes;

		[global::Unity.Collections.ReadOnly]
		public bool reflectionProbeRotation;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> minMaxZs;

		public void Execute(int index)
		{
			global::Unity.Mathematics.float2 value = global::Unity.Mathematics.math.float2(float.MaxValue, float.MinValue);
			int index2 = index % reflectionProbes.Length;
			global::UnityEngine.Rendering.VisibleReflectionProbe visibleReflectionProbe = reflectionProbes[index2];
			int index3 = index / reflectionProbes.Length;
			global::Unity.Mathematics.float4x4 a = worldToViews[index3];
			global::Unity.Mathematics.float3 float5 = visibleReflectionProbe.bounds.center;
			global::Unity.Mathematics.float3 float6 = visibleReflectionProbe.bounds.extents;
			global::Unity.Mathematics.quaternion q = ((!reflectionProbeRotation) ? global::Unity.Mathematics.quaternion.identity : ((global::Unity.Mathematics.quaternion)visibleReflectionProbe.localToWorldMatrix.rotation));
			for (int i = 0; i < 8; i++)
			{
				int num = ((i << 1) & 2) - 1;
				int num2 = (i & 2) - 1;
				int num3 = ((i >> 1) & 2) - 1;
				global::Unity.Mathematics.float3 v = float6 * global::Unity.Mathematics.math.float3(num, num2, num3);
				global::Unity.Mathematics.float3 float7 = global::Unity.Mathematics.math.rotate(q, v);
				global::Unity.Mathematics.float4 float8 = global::Unity.Mathematics.math.mul(a, global::Unity.Mathematics.math.float4(float7 + float5, 1f));
				float8.z *= -1f;
				value.x = global::Unity.Mathematics.math.min(value.x, float8.z);
				value.y = global::Unity.Mathematics.math.max(value.y, float8.z);
			}
			minMaxZs[index] = value;
		}
	}
}
