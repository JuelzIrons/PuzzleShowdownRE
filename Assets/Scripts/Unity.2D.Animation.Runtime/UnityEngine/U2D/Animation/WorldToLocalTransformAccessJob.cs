namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal struct WorldToLocalTransformAccessJob : global::UnityEngine.Jobs.IJobParallelForTransform
	{
		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> outMatrix;

		public void Execute(int index, global::UnityEngine.Jobs.TransformAccess transform)
		{
			if (transform.isValid)
			{
				outMatrix[index] = transform.worldToLocalMatrix;
			}
		}
	}
}
