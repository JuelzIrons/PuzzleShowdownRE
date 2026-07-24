namespace UnityEngine.InputSystem.Utilities
{
	internal class SelectManyObservable<TSource, TResult> : global::System.IObservable<TResult>
	{
		private class Select : global::System.IObserver<TSource>
		{
			private global::UnityEngine.InputSystem.Utilities.SelectManyObservable<TSource, TResult> m_Observable;

			private readonly global::System.IObserver<TResult> m_Observer;

			public Select(global::UnityEngine.InputSystem.Utilities.SelectManyObservable<TSource, TResult> observable, global::System.IObserver<TResult> observer)
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

			public void OnNext(TSource evt)
			{
				foreach (TResult item in m_Observable.m_Filter(evt))
				{
					m_Observer.OnNext(item);
				}
			}
		}

		private readonly global::System.IObservable<TSource> m_Source;

		private readonly global::System.Func<TSource, global::System.Collections.Generic.IEnumerable<TResult>> m_Filter;

		public SelectManyObservable(global::System.IObservable<TSource> source, global::System.Func<TSource, global::System.Collections.Generic.IEnumerable<TResult>> filter)
		{
			m_Source = source;
			m_Filter = filter;
		}

		public global::System.IDisposable Subscribe(global::System.IObserver<TResult> observer)
		{
			return m_Source.Subscribe(new global::UnityEngine.InputSystem.Utilities.SelectManyObservable<TSource, TResult>.Select(this, observer));
		}
	}
}
