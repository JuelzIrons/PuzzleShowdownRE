namespace UnityEngine.Rendering.Universal
{
	internal struct PerLight2D
	{
		internal global::Unity.Mathematics.float4x4 InvMatrix;

		internal global::Unity.Mathematics.float4 Color;

		internal global::Unity.Mathematics.float4 Position;

		internal float FalloffIntensity;

		internal float FalloffDistance;

		internal float OuterAngle;

		internal float InnerAngle;

		internal float InnerRadiusMult;

		internal float VolumeOpacity;

		internal float ShadowIntensity;

		internal int LightType;
	}
}
