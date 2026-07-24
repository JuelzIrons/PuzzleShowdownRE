namespace UnityEngine.InputSystem.Utilities
{
	internal class TakeNObservable<TValue> : global::System.IObservable<TValue>
	{
		private class Take : global::System.IObserver<TValue>
		{
			private global::System.IObserver<TValue> m_Observer;

			private int m_Remaining;

			public Take(global::UnityEngine.InputSystem.Utilities.TakeNObservable<TValue> observable, global::System.IObserver<TValue> observer)
			{
				m_Observer = observer;
				m_Remaining = observable.m_Count;
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
				if (m_Remaining > 0)
				{
					m_Remaining--;
					m_Observer.OnNext(evt);
					if (m_Remaining == 0)
					{
						m_Observer.OnCompleted();
						m_Observer = null;
					}
				}
			}
		}

		private global::System.IObservable<TValue> m_Source;

		private int m_Count;

		public TakeNObservable(global::System.IObservable<TValue> source, int count)
		{
			m_Source = source;
			m_Count = count;
		}

		public global::System.IDisposable Subscribe(global::System.IObserver<TValue> observer)
		{
			return m_Source.Subscribe(new global::UnityEngine.InputSystem.Utilities.TakeNObservable<TValue>.Take(this, observer));
		}
	}
}
