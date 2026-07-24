namespace UnityEngine.InputSystem.Utilities
{
	internal static class DelegateHelpers
	{
		public static void InvokeCallbacksSafe(ref global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action> callbacks, global::Unity.Profiling.ProfilerMarker marker, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i]();
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogException(ex);
					if (context != null)
					{
						global::UnityEngine.Debug.LogError($"{ex.GetType().Name} while executing '{callbackName}' callbacks of '{context}'");
					}
					else
					{
						global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		public static void InvokeCallbacksSafe<TValue>(ref global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<TValue>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i](argument);
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogException(ex);
					if (context != null)
					{
						global::UnityEngine.Debug.LogError($"{ex.GetType().Name} while executing '{callbackName}' callbacks of '{context}'");
					}
					else
					{
						global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		public static void InvokeCallbacksSafe<TValue1, TValue2>(ref global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<TValue1, TValue2>> callbacks, TValue1 argument1, TValue2 argument2, global::Unity.Profiling.ProfilerMarker marker, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i](argument1, argument2);
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogException(ex);
					if (context != null)
					{
						global::UnityEngine.Debug.LogError($"{ex.GetType().Name} while executing '{callbackName}' callbacks of '{context}'");
					}
					else
					{
						global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		public static bool InvokeCallbacksSafe_AnyCallbackReturnsTrue<TValue1, TValue2>(ref global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Func<TValue1, TValue2, bool>> callbacks, TValue1 argument1, TValue2 argument2, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return true;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					if (callbacks[i](argument1, argument2))
					{
						callbacks.UnlockForChanges();
						return true;
					}
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogException(ex);
					if (context != null)
					{
						global::UnityEngine.Debug.LogError($"{ex.GetType().Name} while executing '{callbackName}' callbacks of '{context}'");
					}
					else
					{
						global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
			return false;
		}

		public static void InvokeCallbacksSafe_AndInvokeReturnedActions<TValue>(ref global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Func<TValue, global::System.Action>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i](argument)?.Invoke();
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogException(ex);
					if (context != null)
					{
						global::UnityEngine.Debug.LogError($"{ex.GetType().Name} while executing '{callbackName}' callbacks of '{context}'");
					}
					else
					{
						global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		public static bool InvokeCallbacksSafe_AnyCallbackReturnsObject<TValue, TReturn>(ref global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Func<TValue, TReturn>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return false;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					if (callbacks[i](argument) != null)
					{
						callbacks.UnlockForChanges();
						return true;
					}
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogException(ex);
					if (context != null)
					{
						global::UnityEngine.Debug.LogError($"{ex.GetType().Name} while executing '{callbackName}' callbacks of '{context}'");
					}
					else
					{
						global::UnityEngine.Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
			return false;
		}
	}
}
