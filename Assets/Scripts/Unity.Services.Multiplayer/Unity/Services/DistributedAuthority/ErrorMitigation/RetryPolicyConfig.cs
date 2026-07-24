namespace Unity.Services.DistributedAuthority.ErrorMitigation
{
	internal class RetryPolicyConfig
	{
		private float _jitterMagnitude = 1f;

		private float _delayScale = 1f;

		private float _maxDelayTime = 8f;

		private global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.ErrorMitigation.ExceptionPredicate> _exceptionsToHandle = new global::System.Collections.Generic.List<global::Unity.Services.DistributedAuthority.ErrorMitigation.ExceptionPredicate>();

		public uint MaxRetries { get; set; } = 4u;

		public float JitterMagnitude
		{
			get
			{
				return _jitterMagnitude;
			}
			set
			{
				_jitterMagnitude = global::UnityEngine.Mathf.Clamp(value, 0.001f, 1f);
			}
		}

		public float DelayScale
		{
			get
			{
				return _delayScale;
			}
			set
			{
				_delayScale = global::UnityEngine.Mathf.Clamp(value, 0.05f, 1f);
			}
		}

		public float MaxDelayTime
		{
			get
			{
				return _maxDelayTime;
			}
			set
			{
				_maxDelayTime = global::UnityEngine.Mathf.Clamp(value, 0.1f, 60f);
			}
		}

		public void HandleException<TException>() where TException : global::System.Exception
		{
			_exceptionsToHandle.Add((global::System.Exception exception) => (!(exception is TException)) ? null : exception);
		}

		public void HandleException<TException>(global::System.Func<TException, bool> condition) where TException : global::System.Exception
		{
			_exceptionsToHandle.Add((global::System.Exception exception) => (!(exception is TException arg) || !condition(arg)) ? null : exception);
		}

		public bool IsHandledException(global::System.Exception e)
		{
			if (_exceptionsToHandle != null)
			{
				foreach (global::Unity.Services.DistributedAuthority.ErrorMitigation.ExceptionPredicate item in _exceptionsToHandle)
				{
					if (item(e) == e)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
