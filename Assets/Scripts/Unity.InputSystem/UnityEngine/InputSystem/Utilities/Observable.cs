namespace UnityEngine.InputSystem.Utilities
{
	public static class Observable
	{
		public static global::System.IObservable<TValue> Where<TValue>(this global::System.IObservable<TValue> source, global::System.Func<TValue, bool> predicate)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new global::System.ArgumentNullException("predicate");
			}
			return new global::UnityEngine.InputSystem.Utilities.WhereObservable<TValue>(source, predicate);
		}

		public static global::System.IObservable<TResult> Select<TSource, TResult>(this global::System.IObservable<TSource> source, global::System.Func<TSource, TResult> filter)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (filter == null)
			{
				throw new global::System.ArgumentNullException("filter");
			}
			return new global::UnityEngine.InputSystem.LowLevel.SelectObservable<TSource, TResult>(source, filter);
		}

		public static global::System.IObservable<TResult> SelectMany<TSource, TResult>(this global::System.IObservable<TSource> source, global::System.Func<TSource, global::System.Collections.Generic.IEnumerable<TResult>> filter)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (filter == null)
			{
				throw new global::System.ArgumentNullException("filter");
			}
			return new global::UnityEngine.InputSystem.Utilities.SelectManyObservable<TSource, TResult>(source, filter);
		}

		public static global::System.IObservable<TValue> Take<TValue>(this global::System.IObservable<TValue> source, int count)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (count < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("count");
			}
			return new global::UnityEngine.InputSystem.Utilities.TakeNObservable<TValue>(source, count);
		}

		public static global::System.IObservable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> ForDevice(this global::System.IObservable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> source, global::UnityEngine.InputSystem.InputDevice device)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			return new global::UnityEngine.InputSystem.Utilities.ForDeviceEventObservable(source, null, device);
		}

		public static global::System.IObservable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> ForDevice<TDevice>(this global::System.IObservable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> source) where TDevice : global::UnityEngine.InputSystem.InputDevice
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			return new global::UnityEngine.InputSystem.Utilities.ForDeviceEventObservable(source, typeof(TDevice), null);
		}

		public static global::System.IDisposable CallOnce<TValue>(this global::System.IObservable<TValue> source, global::System.Action<TValue> action)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			global::System.IDisposable subscription = null;
			subscription = source.Take(1).Subscribe(new global::UnityEngine.InputSystem.Utilities.Observer<TValue>(action, delegate
			{
				subscription?.Dispose();
			}));
			return subscription;
		}

		public static global::System.IDisposable Call<TValue>(this global::System.IObservable<TValue> source, global::System.Action<TValue> action)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			return source.Subscribe(new global::UnityEngine.InputSystem.Utilities.Observer<TValue>(action));
		}
	}
}
