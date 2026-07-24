namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct LocalToWorldAndChangeDetectionTransformAccessJob : global::UnityEngine.Jobs.IJobParallelForTransform
	{
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> outMatrix;

		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<bool> hasChanged;

		public void Execute(int index, global::UnityEngine.Jobs.TransformAccess transform)
		{
			if (transform.isValid)
			{
				global::Unity.Mathematics.float4x4 float4x5 = transform.localToWorldMatrix;
				hasChanged[index] = !outMatrix[index].Equals(float4x5);
				outMatrix[index] = float4x5;
			}
		}
	}
}
