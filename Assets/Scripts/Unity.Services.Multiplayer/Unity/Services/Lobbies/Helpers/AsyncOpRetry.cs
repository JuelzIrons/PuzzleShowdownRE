namespace Unity.Services.Lobbies.Helpers
{
	internal static class AsyncOpRetry
	{
		public static global::Unity.Services.Lobbies.Helpers.AsyncOpRetry<T> FromCreateAsync<T>(global::System.Func<int, T> op)
		{
			return global::Unity.Services.Lobbies.Helpers.AsyncOpRetry<T>.FromCreateAsync(op);
		}
	}
	internal class AsyncOpRetry<T>
	{
		private uint MaxRetries { get; set; } = 4u;

		private float JitterMagnitude { get; set; } = 1f;

		private float DelayScale { get; set; } = 1f;

		private float MaxDelayTime { get; set; } = 8f;

		private global::System.Func<int, T> CreateOperation { get; set; }

		private global::System.Func<T, bool> RetryCondition { get; set; }

		private global::System.Action<T> OnComplete { get; set; }

		private AsyncOpRetry(global::System.Func<int, T> createAsyncOp)
		{
			CreateOperation = createAsyncOp;
		}

		private static float AddJitter(float number, float magnitude)
		{
			return number + global::UnityEngine.Random.value * magnitude;
		}

		private static float Pow2(float exponent, float scale)
		{
			return (float)(global::System.Math.Pow(2.0, exponent) * (double)scale);
		}

		private static float CalculateDelay(int attemptNumber, float maxDelayTime, float delayScale, float jitterMagnitude)
		{
			return global::System.Math.Min(AddJitter(Pow2(attemptNumber, delayScale), jitterMagnitude), maxDelayTime);
		}

		public static global::Unity.Services.Lobbies.Helpers.AsyncOpRetry<T> FromCreateAsync(global::System.Func<int, T> op)
		{
			return new global::Unity.Services.Lobbies.Helpers.AsyncOpRetry<T>(op);
		}

		public global::Unity.Services.Lobbies.Helpers.AsyncOpRetry<T> WithRetryCondition(global::System.Func<T, bool> shouldRetry)
		{
			RetryCondition = shouldRetry;
			return this;
		}

		public global::Unity.Services.Lobbies.Helpers.AsyncOpRetry<T> WhenComplete(global::System.Action<T> onComplete)
		{
			OnComplete = onComplete;
			return this;
		}

		public global::System.Collections.IEnumerator Run()
		{
			T asyncOp = default(T);
			int attempt = 0;
			while (attempt <= MaxRetries)
			{
				asyncOp = CreateOperation(attempt + 1);
				yield return asyncOp;
				global::System.Func<T, bool> retryCondition = RetryCondition;
				if (retryCondition != null && !retryCondition(asyncOp))
				{
					break;
				}
				float time = CalculateDelay(attempt, MaxDelayTime, DelayScale, JitterMagnitude);
				yield return new global::UnityEngine.WaitForSecondsRealtime(time);
				int num = attempt + 1;
				attempt = num;
			}
			OnComplete?.Invoke(asyncOp);
		}
	}
}
