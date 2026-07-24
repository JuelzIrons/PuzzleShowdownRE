namespace Unity.Services.DistributedAuthority.ErrorMitigation
{
	internal class RetryPolicy<T> : global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T>
	{
		private global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicyConfig _retryPolicyConfig = new global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicyConfig();

		private global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private global::System.Func<int, global::System.Threading.Tasks.Task<T>> CreateOperation { get; set; }

		private global::System.Func<T, global::System.Threading.Tasks.Task<bool>> RetryCondition { get; set; }

		private RetryPolicy(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::System.Func<int, global::System.Threading.Tasks.Task<T>> createAsyncOp)
		{
			m_ActionScheduler = actionScheduler;
			CreateOperation = createAsyncOp;
		}

		private RetryPolicy(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::System.Func<global::System.Threading.Tasks.Task<T>> createAsyncOp)
		{
			m_ActionScheduler = actionScheduler;
			CreateOperation = (int _) => createAsyncOp();
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

		public global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> WithJitterMagnitude(float magnitude)
		{
			_retryPolicyConfig.JitterMagnitude = magnitude;
			return this;
		}

		public global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> WithDelayScale(float scale)
		{
			_retryPolicyConfig.DelayScale = scale;
			return this;
		}

		public global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> WithMaxDelayTime(float time)
		{
			_retryPolicyConfig.MaxDelayTime = time;
			return this;
		}

		public static global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicy<T> ForOperation(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::System.Func<int, global::System.Threading.Tasks.Task<T>> operation)
		{
			return new global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicy<T>(actionScheduler, operation);
		}

		public static global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicy<T> ForOperation(global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::System.Func<global::System.Threading.Tasks.Task<T>> operation)
		{
			return new global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicy<T>(actionScheduler, operation);
		}

		public global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> WithRetryCondition(global::System.Func<T, global::System.Threading.Tasks.Task<bool>> shouldRetry)
		{
			RetryCondition = shouldRetry;
			return this;
		}

		public global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> UptoMaximumRetries(uint amount)
		{
			_retryPolicyConfig.MaxRetries = amount;
			return this;
		}

		public global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> HandleException<TException>() where TException : global::System.Exception
		{
			_retryPolicyConfig.HandleException<TException>();
			return this;
		}

		public global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicy<T> HandleException<TException>(global::System.Func<TException, bool> condition) where TException : global::System.Exception
		{
			_retryPolicyConfig.HandleException(condition);
			return this;
		}

		public async global::System.Threading.Tasks.Task<T> RunAsync(global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicyConfig retryPolicyConfig = null)
		{
			T opResult = default(T);
			if (retryPolicyConfig == null)
			{
				retryPolicyConfig = _retryPolicyConfig;
			}
			int attempt = 0;
			while (attempt <= retryPolicyConfig.MaxRetries)
			{
				try
				{
					opResult = await CreateOperation(attempt + 1);
				}
				catch (global::System.Exception e)
				{
					if (!retryPolicyConfig.IsHandledException(e))
					{
						throw;
					}
				}
				if (RetryCondition != null && opResult != null)
				{
					if (!(await RetryCondition(opResult)))
					{
						break;
					}
				}
				else if (opResult != null)
				{
					break;
				}
				float num = CalculateDelay(attempt, retryPolicyConfig.MaxDelayTime, retryPolicyConfig.DelayScale, retryPolicyConfig.JitterMagnitude);
				global::System.Threading.Tasks.TaskCompletionSource<object> completionSource = new global::System.Threading.Tasks.TaskCompletionSource<object>();
				m_ActionScheduler.ScheduleAction(delegate
				{
					completionSource.SetResult(null);
				}, (int)num);
				await completionSource.Task;
				int num2 = attempt + 1;
				attempt = num2;
			}
			return opResult;
		}
	}
}
