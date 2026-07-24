namespace UnityEngine.Rendering
{
	internal static class LODRenderingUtils
	{
		public static float CalculateFOVHalfAngle(float fieldOfView)
		{
			return global::UnityEngine.Mathf.Tan(global::System.MathF.PI / 180f * fieldOfView * 0.5f);
		}

		public static float CalculateScreenRelativeMetricNoBias(global::UnityEngine.Rendering.LODParameters lodParams)
		{
			if (lodParams.isOrthographic)
			{
				return 2f * lodParams.orthoSize;
			}
			float num = CalculateFOVHalfAngle(lodParams.fieldOfView);
			return 2f * num;
		}

		public static float CalculateMeshLodConstant(global::UnityEngine.Rendering.LODParameters lodParams, float screenRelativeMetric, float meshLodThreshold)
		{
			return meshLodThreshold * screenRelativeMetric / (float)lodParams.cameraPixelHeight;
		}

		public static float CalculatePerspectiveDistance(global::UnityEngine.Vector3 objPosition, global::UnityEngine.Vector3 camPosition, float sqrScreenRelativeMetric)
		{
			return global::UnityEngine.Mathf.Sqrt(CalculateSqrPerspectiveDistance(objPosition, camPosition, sqrScreenRelativeMetric));
		}

		public static float CalculateSqrPerspectiveDistance(global::UnityEngine.Vector3 objPosition, global::UnityEngine.Vector3 camPosition, float sqrScreenRelativeMetric)
		{
			return (objPosition - camPosition).sqrMagnitude * sqrScreenRelativeMetric;
		}

		public static global::UnityEngine.Vector3 GetWorldReferencePoint(this global::UnityEngine.LODGroup lodGroup)
		{
			return lodGroup.transform.TransformPoint(lodGroup.localReferencePoint);
		}

		public static float GetWorldSpaceScale(this global::UnityEngine.LODGroup lodGroup)
		{
			global::UnityEngine.Vector3 lossyScale = lodGroup.transform.lossyScale;
			return global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(lossyScale.x), global::UnityEngine.Mathf.Abs(lossyScale.y)), global::UnityEngine.Mathf.Abs(lossyScale.z));
		}

		public static float GetWorldSpaceSize(this global::UnityEngine.LODGroup lodGroup)
		{
			return lodGroup.GetWorldSpaceScale() * lodGroup.size;
		}

		public static float CalculateLODDistance(float relativeScreenHeight, float size)
		{
			return size / relativeScreenHeight;
		}
	}
}
