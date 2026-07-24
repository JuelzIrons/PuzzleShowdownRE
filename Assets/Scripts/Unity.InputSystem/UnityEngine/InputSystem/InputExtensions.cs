namespace UnityEngine.InputSystem
{
	public static class InputExtensions
	{
		public static bool IsInProgress(this global::UnityEngine.InputSystem.InputActionPhase phase)
		{
			if (phase != global::UnityEngine.InputSystem.InputActionPhase.Started)
			{
				return phase == global::UnityEngine.InputSystem.InputActionPhase.Performed;
			}
			return true;
		}

		public static bool IsEndedOrCanceled(this global::UnityEngine.InputSystem.TouchPhase phase)
		{
			if (phase != global::UnityEngine.InputSystem.TouchPhase.Canceled)
			{
				return phase == global::UnityEngine.InputSystem.TouchPhase.Ended;
			}
			return true;
		}

		public static bool IsActive(this global::UnityEngine.InputSystem.TouchPhase phase)
		{
			if ((uint)(phase - 1) <= 1u || phase == global::UnityEngine.InputSystem.TouchPhase.Stationary)
			{
				return true;
			}
			return false;
		}

		public static bool IsModifierKey(this global::UnityEngine.InputSystem.Key key)
		{
			if ((uint)(key - 51) <= 7u)
			{
				return true;
			}
			return false;
		}

		public static bool IsTextInputKey(this global::UnityEngine.InputSystem.Key key)
		{
			switch (key)
			{
			case global::UnityEngine.InputSystem.Key.None:
			case global::UnityEngine.InputSystem.Key.Space:
			case global::UnityEngine.InputSystem.Key.Enter:
			case global::UnityEngine.InputSystem.Key.Tab:
			case global::UnityEngine.InputSystem.Key.LeftShift:
			case global::UnityEngine.InputSystem.Key.RightShift:
			case global::UnityEngine.InputSystem.Key.LeftAlt:
			case global::UnityEngine.InputSystem.Key.RightAlt:
			case global::UnityEngine.InputSystem.Key.LeftCtrl:
			case global::UnityEngine.InputSystem.Key.RightCtrl:
			case global::UnityEngine.InputSystem.Key.LeftMeta:
			case global::UnityEngine.InputSystem.Key.RightMeta:
			case global::UnityEngine.InputSystem.Key.ContextMenu:
			case global::UnityEngine.InputSystem.Key.Escape:
			case global::UnityEngine.InputSystem.Key.LeftArrow:
			case global::UnityEngine.InputSystem.Key.RightArrow:
			case global::UnityEngine.InputSystem.Key.UpArrow:
			case global::UnityEngine.InputSystem.Key.DownArrow:
			case global::UnityEngine.InputSystem.Key.Backspace:
			case global::UnityEngine.InputSystem.Key.PageDown:
			case global::UnityEngine.InputSystem.Key.PageUp:
			case global::UnityEngine.InputSystem.Key.Home:
			case global::UnityEngine.InputSystem.Key.End:
			case global::UnityEngine.InputSystem.Key.Insert:
			case global::UnityEngine.InputSystem.Key.Delete:
			case global::UnityEngine.InputSystem.Key.CapsLock:
			case global::UnityEngine.InputSystem.Key.NumLock:
			case global::UnityEngine.InputSystem.Key.PrintScreen:
			case global::UnityEngine.InputSystem.Key.ScrollLock:
			case global::UnityEngine.InputSystem.Key.Pause:
			case global::UnityEngine.InputSystem.Key.NumpadEnter:
			case global::UnityEngine.InputSystem.Key.F1:
			case global::UnityEngine.InputSystem.Key.F2:
			case global::UnityEngine.InputSystem.Key.F3:
			case global::UnityEngine.InputSystem.Key.F4:
			case global::UnityEngine.InputSystem.Key.F5:
			case global::UnityEngine.InputSystem.Key.F6:
			case global::UnityEngine.InputSystem.Key.F7:
			case global::UnityEngine.InputSystem.Key.F8:
			case global::UnityEngine.InputSystem.Key.F9:
			case global::UnityEngine.InputSystem.Key.F10:
			case global::UnityEngine.InputSystem.Key.F11:
			case global::UnityEngine.InputSystem.Key.F12:
			case global::UnityEngine.InputSystem.Key.OEM1:
			case global::UnityEngine.InputSystem.Key.OEM2:
			case global::UnityEngine.InputSystem.Key.OEM3:
			case global::UnityEngine.InputSystem.Key.OEM4:
			case global::UnityEngine.InputSystem.Key.OEM5:
			case global::UnityEngine.InputSystem.Key.IMESelected:
			case global::UnityEngine.InputSystem.Key.F13:
			case global::UnityEngine.InputSystem.Key.F14:
			case global::UnityEngine.InputSystem.Key.F15:
			case global::UnityEngine.InputSystem.Key.F16:
			case global::UnityEngine.InputSystem.Key.F17:
			case global::UnityEngine.InputSystem.Key.F18:
			case global::UnityEngine.InputSystem.Key.F19:
			case global::UnityEngine.InputSystem.Key.F20:
			case global::UnityEngine.InputSystem.Key.F21:
			case global::UnityEngine.InputSystem.Key.F22:
			case global::UnityEngine.InputSystem.Key.F23:
			case global::UnityEngine.InputSystem.Key.F24:
			case global::UnityEngine.InputSystem.Key.MediaPlayPause:
			case global::UnityEngine.InputSystem.Key.MediaRewind:
			case global::UnityEngine.InputSystem.Key.MediaForward:
				return false;
			default:
				return true;
			}
		}
	}
}
