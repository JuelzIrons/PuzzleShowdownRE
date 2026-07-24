namespace UnityEngine.InputSystem.Utilities
{
	internal class Observer<TValue> : global::System.IObserver<TValue>
	{
		private global::System.Action<TValue> m_OnNext;

		private global::System.Action m_OnCompleted;

		public Observer(global::System.Action<TValue> onNext, global::System.Action onCompleted = null)
		{
			m_OnNext = onNext;
			m_OnCompleted = onCompleted;
		}

		public void OnCompleted()
		{
			m_OnCompleted?.Invoke();
		}

		public void OnError(global::System.Exception error)
		{
			global::UnityEngine.Debug.LogException(error);
		}

		public void OnNext(TValue evt)
		{
			m_OnNext?.Invoke(evt);
		}
	}
}
