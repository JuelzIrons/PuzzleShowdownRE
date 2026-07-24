namespace Unity.Netcode
{
	public class BufferedLinearInterpolatorQuaternion : global::Unity.Netcode.BufferedLinearInterpolator<global::UnityEngine.Quaternion>
	{
		public bool IsSlerp;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected override global::UnityEngine.Quaternion InterpolateUnclamped(global::UnityEngine.Quaternion start, global::UnityEngine.Quaternion end, float time)
		{
			if (IsSlerp)
			{
				return global::UnityEngine.Quaternion.SlerpUnclamped(start, end, time);
			}
			return global::UnityEngine.Quaternion.LerpUnclamped(start, end, time);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected override global::UnityEngine.Quaternion Interpolate(global::UnityEngine.Quaternion start, global::UnityEngine.Quaternion end, float time)
		{
			if (IsSlerp)
			{
				return global::UnityEngine.Quaternion.Slerp(start, end, time);
			}
			return global::UnityEngine.Quaternion.Lerp(start, end, time);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private protected override global::UnityEngine.Quaternion SmoothDamp(global::UnityEngine.Quaternion current, global::UnityEngine.Quaternion target, ref global::UnityEngine.Quaternion rateOfChange, float duration, float deltaTime, float maxSpeed = float.PositiveInfinity)
		{
			global::UnityEngine.Vector3 eulerAngles = current.eulerAngles;
			global::UnityEngine.Vector3 eulerAngles2 = target.eulerAngles;
			for (int i = 0; i < 3; i++)
			{
				float currentVelocity = rateOfChange[i];
				eulerAngles[i] = global::UnityEngine.Mathf.SmoothDampAngle(eulerAngles[i], eulerAngles2[i], ref currentVelocity, duration, maxSpeed, deltaTime);
				rateOfChange[i] = currentVelocity;
			}
			return global::UnityEngine.Quaternion.Euler(eulerAngles);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private protected override bool IsApproximately(global::UnityEngine.Quaternion first, global::UnityEngine.Quaternion second, float precision = 1E-06f)
		{
			if (global::UnityEngine.Mathf.Abs(first.x - second.x) <= precision && global::UnityEngine.Mathf.Abs(first.y - second.y) <= precision && global::UnityEngine.Mathf.Abs(first.z - second.z) <= precision)
			{
				return global::UnityEngine.Mathf.Abs(first.w - second.w) <= precision;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected internal override global::UnityEngine.Quaternion OnConvertTransformSpace(global::UnityEngine.Transform transform, global::UnityEngine.Quaternion rotation, bool inLocalSpace)
		{
			if (inLocalSpace)
			{
				return global::UnityEngine.Quaternion.Inverse(transform.rotation) * rotation;
			}
			return transform.rotation * rotation;
		}
	}
}
