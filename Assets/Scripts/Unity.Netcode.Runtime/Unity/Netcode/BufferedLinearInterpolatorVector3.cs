namespace Unity.Netcode
{
	public class BufferedLinearInterpolatorVector3 : global::Unity.Netcode.BufferedLinearInterpolator<global::UnityEngine.Vector3>
	{
		public bool IsSlerp;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected override global::UnityEngine.Vector3 InterpolateUnclamped(global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 end, float time)
		{
			if (IsSlerp)
			{
				return global::UnityEngine.Vector3.SlerpUnclamped(start, end, time);
			}
			return global::UnityEngine.Vector3.LerpUnclamped(start, end, time);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected override global::UnityEngine.Vector3 Interpolate(global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 end, float time)
		{
			if (IsSlerp)
			{
				return global::UnityEngine.Vector3.Slerp(start, end, time);
			}
			return global::UnityEngine.Vector3.Lerp(start, end, time);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected internal override global::UnityEngine.Vector3 OnConvertTransformSpace(global::UnityEngine.Transform transform, global::UnityEngine.Vector3 position, bool inLocalSpace)
		{
			if (inLocalSpace)
			{
				return transform.InverseTransformPoint(position);
			}
			return transform.TransformPoint(position);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private protected override bool IsApproximately(global::UnityEngine.Vector3 first, global::UnityEngine.Vector3 second, float precision = 1E-06f)
		{
			if (global::System.Math.Round(global::UnityEngine.Mathf.Abs(first.x - second.x), 2) <= (double)precision && global::System.Math.Round(global::UnityEngine.Mathf.Abs(first.y - second.y), 2) <= (double)precision)
			{
				return global::System.Math.Round(global::UnityEngine.Mathf.Abs(first.z - second.z), 2) <= (double)precision;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private protected override global::UnityEngine.Vector3 SmoothDamp(global::UnityEngine.Vector3 current, global::UnityEngine.Vector3 target, ref global::UnityEngine.Vector3 rateOfChange, float duration, float deltaTime, float maxSpeed)
		{
			return global::UnityEngine.Vector3.SmoothDamp(current, target, ref rateOfChange, duration, maxSpeed, deltaTime);
		}
	}
}
