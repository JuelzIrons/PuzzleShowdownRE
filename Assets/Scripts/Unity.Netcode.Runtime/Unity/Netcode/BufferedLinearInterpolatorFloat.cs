namespace Unity.Netcode
{
	public class BufferedLinearInterpolatorFloat : global::Unity.Netcode.BufferedLinearInterpolator<float>
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected override float InterpolateUnclamped(float start, float end, float time)
		{
			return global::UnityEngine.Mathf.LerpUnclamped(start, end, time);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected override float Interpolate(float start, float end, float time)
		{
			return global::UnityEngine.Mathf.Lerp(start, end, time);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private protected override bool IsApproximately(float first, float second, float precision = 1E-06f)
		{
			return global::UnityEngine.Mathf.Approximately(first, second);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private protected override float SmoothDamp(float current, float target, ref float rateOfChange, float duration, float deltaTime, float maxSpeed = float.PositiveInfinity)
		{
			return global::UnityEngine.Mathf.SmoothDamp(current, target, ref rateOfChange, duration, maxSpeed, deltaTime);
		}
	}
}
