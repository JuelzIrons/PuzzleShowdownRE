namespace UnityEngine.InputSystem.Utilities
{
	internal class WhereObservable<TValue> : global::System.IObservable<TValue>
	{
		private class Where : global::System.IObserver<TValue>
		{
			private global::UnityEngine.InputSystem.Utilities.WhereObservable<TValue> m_Observable;

			private readonly global::System.IObserver<TValue> m_Observer;

			public Where(global::UnityEngine.InputSystem.Utilities.WhereObservable<TValue> observable, global::System.IObserver<TValue> observer)
			{
				m_Observable = observable;
				m_Observer = observer;
			}

			public void OnCompleted()
			{
			}

			public void OnError(global::System.Exception error)
			{
				global::UnityEngine.Debug.LogException(error);
			}

			public void OnNext(TValue evt)
			{
				if (m_Observable.m_Predicate(evt))
				{
					m_Observer.OnNext(evt);
				}
			}
		}

		private readonly global::System.IObservable<TValue> m_Source;

		private readonly global::System.Func<TValue, bool> m_Predicate;

		public WhereObservable(global::System.IObservable<TValue> source, global::System.Func<TValue, bool> predicate)
		{
			m_Source = source;
			m_Predicate = predicate;
		}

		public global::System.IDisposable Subscribe(global::System.IObserver<TValue> observer)
		{
			return m_Source.Subscribe(new global::UnityEngine.InputSystem.Utilities.WhereObservable<TValue>.Where(this, observer));
		}
	}
}
