namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public struct InputEventListener : global::System.IObservable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>
	{
		internal class ObserverState
		{
			public global::UnityEngine.InputSystem.Utilities.InlinedArray<global::System.IObserver<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>> observers;

			public global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice> onEventDelegate;

			public ObserverState()
			{
				onEventDelegate = delegate(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.InputDevice device)
				{
					for (int num = observers.length - 1; num >= 0; num--)
					{
						observers[num].OnNext(eventPtr);
					}
				};
			}
		}

		private class DisposableObserver : global::System.IDisposable
		{
			public global::System.IObserver<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> observer;

			public void Dispose()
			{
				int num = global::UnityEngine.InputSystem.Utilities.InputArrayExtensions.IndexOfReference(s_ObserverState.observers, observer);
				if (num >= 0)
				{
					s_ObserverState.observers.RemoveAtWithCapacity(num);
				}
				if (s_ObserverState.observers.length == 0)
				{
					global::UnityEngine.InputSystem.InputSystem.s_Manager.onEvent -= s_ObserverState.onEventDelegate;
				}
			}
		}

		internal static global::UnityEngine.InputSystem.LowLevel.InputEventListener.ObserverState s_ObserverState;

		public static global::UnityEngine.InputSystem.LowLevel.InputEventListener operator +(global::UnityEngine.InputSystem.LowLevel.InputEventListener _, global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice> callback)
		{
			if (callback == null)
			{
				throw new global::System.ArgumentNullException("callback");
			}
			lock (global::UnityEngine.InputSystem.InputSystem.s_Manager)
			{
				global::UnityEngine.InputSystem.InputSystem.s_Manager.onEvent += callback;
			}
			return default(global::UnityEngine.InputSystem.LowLevel.InputEventListener);
		}

		public static global::UnityEngine.InputSystem.LowLevel.InputEventListener operator -(global::UnityEngine.InputSystem.LowLevel.InputEventListener _, global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice> callback)
		{
			if (callback == null)
			{
				throw new global::System.ArgumentNullException("callback");
			}
			lock (global::UnityEngine.InputSystem.InputSystem.s_Manager)
			{
				global::UnityEngine.InputSystem.InputSystem.s_Manager.onEvent -= callback;
			}
			return default(global::UnityEngine.InputSystem.LowLevel.InputEventListener);
		}

		public global::System.IDisposable Subscribe(global::System.IObserver<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> observer)
		{
			if (s_ObserverState == null)
			{
				s_ObserverState = new global::UnityEngine.InputSystem.LowLevel.InputEventListener.ObserverState();
			}
			if (s_ObserverState.observers.length == 0)
			{
				global::UnityEngine.InputSystem.InputSystem.s_Manager.onEvent += s_ObserverState.onEventDelegate;
			}
			s_ObserverState.observers.AppendWithCapacity(observer);
			return new global::UnityEngine.InputSystem.LowLevel.InputEventListener.DisposableObserver
			{
				observer = observer
			};
		}
	}
}
