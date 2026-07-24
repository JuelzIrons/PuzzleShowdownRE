namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.KeyboardState), isGenericTypeOfDevice = true)]
	public class Keyboard : global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.LowLevel.ITextInputReceiver, global::UnityEngine.InputSystem.LowLevel.IEventPreProcessor
	{
		public const int KeyCount = 110;

		internal const int ExtendedKeyCount = 126;

		private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::System.Action<char>> m_TextInputListeners;

		private string m_KeyboardLayoutName;

		private global::UnityEngine.InputSystem.Controls.KeyControl[] m_Keys;

		private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::System.Action<global::UnityEngine.InputSystem.LowLevel.IMECompositionString>> m_ImeCompositionListeners;

		public string keyboardLayout
		{
			get
			{
				RefreshConfigurationIfNeeded();
				return m_KeyboardLayoutName;
			}
			protected set
			{
				m_KeyboardLayoutName = value;
			}
		}

		public global::UnityEngine.InputSystem.Controls.AnyKeyControl anyKey { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.KeyControl spaceKey => this[global::UnityEngine.InputSystem.Key.Space];

		public global::UnityEngine.InputSystem.Controls.KeyControl enterKey => this[global::UnityEngine.InputSystem.Key.Enter];

		public global::UnityEngine.InputSystem.Controls.KeyControl tabKey => this[global::UnityEngine.InputSystem.Key.Tab];

		public global::UnityEngine.InputSystem.Controls.KeyControl backquoteKey => this[global::UnityEngine.InputSystem.Key.Backquote];

		public global::UnityEngine.InputSystem.Controls.KeyControl quoteKey => this[global::UnityEngine.InputSystem.Key.Quote];

		public global::UnityEngine.InputSystem.Controls.KeyControl semicolonKey => this[global::UnityEngine.InputSystem.Key.Semicolon];

		public global::UnityEngine.InputSystem.Controls.KeyControl commaKey => this[global::UnityEngine.InputSystem.Key.Comma];

		public global::UnityEngine.InputSystem.Controls.KeyControl periodKey => this[global::UnityEngine.InputSystem.Key.Period];

		public global::UnityEngine.InputSystem.Controls.KeyControl slashKey => this[global::UnityEngine.InputSystem.Key.Slash];

		public global::UnityEngine.InputSystem.Controls.KeyControl backslashKey => this[global::UnityEngine.InputSystem.Key.Backslash];

		public global::UnityEngine.InputSystem.Controls.KeyControl leftBracketKey => this[global::UnityEngine.InputSystem.Key.LeftBracket];

		public global::UnityEngine.InputSystem.Controls.KeyControl rightBracketKey => this[global::UnityEngine.InputSystem.Key.RightBracket];

		public global::UnityEngine.InputSystem.Controls.KeyControl minusKey => this[global::UnityEngine.InputSystem.Key.Minus];

		public global::UnityEngine.InputSystem.Controls.KeyControl equalsKey => this[global::UnityEngine.InputSystem.Key.Equals];

		public global::UnityEngine.InputSystem.Controls.KeyControl aKey => this[global::UnityEngine.InputSystem.Key.A];

		public global::UnityEngine.InputSystem.Controls.KeyControl bKey => this[global::UnityEngine.InputSystem.Key.B];

		public global::UnityEngine.InputSystem.Controls.KeyControl cKey => this[global::UnityEngine.InputSystem.Key.C];

		public global::UnityEngine.InputSystem.Controls.KeyControl dKey => this[global::UnityEngine.InputSystem.Key.D];

		public global::UnityEngine.InputSystem.Controls.KeyControl eKey => this[global::UnityEngine.InputSystem.Key.E];

		public global::UnityEngine.InputSystem.Controls.KeyControl fKey => this[global::UnityEngine.InputSystem.Key.F];

		public global::UnityEngine.InputSystem.Controls.KeyControl gKey => this[global::UnityEngine.InputSystem.Key.G];

		public global::UnityEngine.InputSystem.Controls.KeyControl hKey => this[global::UnityEngine.InputSystem.Key.H];

		public global::UnityEngine.InputSystem.Controls.KeyControl iKey => this[global::UnityEngine.InputSystem.Key.I];

		public global::UnityEngine.InputSystem.Controls.KeyControl jKey => this[global::UnityEngine.InputSystem.Key.J];

		public global::UnityEngine.InputSystem.Controls.KeyControl kKey => this[global::UnityEngine.InputSystem.Key.K];

		public global::UnityEngine.InputSystem.Controls.KeyControl lKey => this[global::UnityEngine.InputSystem.Key.L];

		public global::UnityEngine.InputSystem.Controls.KeyControl mKey => this[global::UnityEngine.InputSystem.Key.M];

		public global::UnityEngine.InputSystem.Controls.KeyControl nKey => this[global::UnityEngine.InputSystem.Key.N];

		public global::UnityEngine.InputSystem.Controls.KeyControl oKey => this[global::UnityEngine.InputSystem.Key.O];

		public global::UnityEngine.InputSystem.Controls.KeyControl pKey => this[global::UnityEngine.InputSystem.Key.P];

		public global::UnityEngine.InputSystem.Controls.KeyControl qKey => this[global::UnityEngine.InputSystem.Key.Q];

		public global::UnityEngine.InputSystem.Controls.KeyControl rKey => this[global::UnityEngine.InputSystem.Key.R];

		public global::UnityEngine.InputSystem.Controls.KeyControl sKey => this[global::UnityEngine.InputSystem.Key.S];

		public global::UnityEngine.InputSystem.Controls.KeyControl tKey => this[global::UnityEngine.InputSystem.Key.T];

		public global::UnityEngine.InputSystem.Controls.KeyControl uKey => this[global::UnityEngine.InputSystem.Key.U];

		public global::UnityEngine.InputSystem.Controls.KeyControl vKey => this[global::UnityEngine.InputSystem.Key.V];

		public global::UnityEngine.InputSystem.Controls.KeyControl wKey => this[global::UnityEngine.InputSystem.Key.W];

		public global::UnityEngine.InputSystem.Controls.KeyControl xKey => this[global::UnityEngine.InputSystem.Key.X];

		public global::UnityEngine.InputSystem.Controls.KeyControl yKey => this[global::UnityEngine.InputSystem.Key.Y];

		public global::UnityEngine.InputSystem.Controls.KeyControl zKey => this[global::UnityEngine.InputSystem.Key.Z];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit1Key => this[global::UnityEngine.InputSystem.Key.Digit1];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit2Key => this[global::UnityEngine.InputSystem.Key.Digit2];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit3Key => this[global::UnityEngine.InputSystem.Key.Digit3];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit4Key => this[global::UnityEngine.InputSystem.Key.Digit4];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit5Key => this[global::UnityEngine.InputSystem.Key.Digit5];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit6Key => this[global::UnityEngine.InputSystem.Key.Digit6];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit7Key => this[global::UnityEngine.InputSystem.Key.Digit7];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit8Key => this[global::UnityEngine.InputSystem.Key.Digit8];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit9Key => this[global::UnityEngine.InputSystem.Key.Digit9];

		public global::UnityEngine.InputSystem.Controls.KeyControl digit0Key => this[global::UnityEngine.InputSystem.Key.Digit0];

		public global::UnityEngine.InputSystem.Controls.KeyControl leftShiftKey => this[global::UnityEngine.InputSystem.Key.LeftShift];

		public global::UnityEngine.InputSystem.Controls.KeyControl rightShiftKey => this[global::UnityEngine.InputSystem.Key.RightShift];

		public global::UnityEngine.InputSystem.Controls.KeyControl leftAltKey => this[global::UnityEngine.InputSystem.Key.LeftAlt];

		public global::UnityEngine.InputSystem.Controls.KeyControl rightAltKey => this[global::UnityEngine.InputSystem.Key.RightAlt];

		public global::UnityEngine.InputSystem.Controls.KeyControl leftCtrlKey => this[global::UnityEngine.InputSystem.Key.LeftCtrl];

		public global::UnityEngine.InputSystem.Controls.KeyControl rightCtrlKey => this[global::UnityEngine.InputSystem.Key.RightCtrl];

		public global::UnityEngine.InputSystem.Controls.KeyControl leftMetaKey => this[global::UnityEngine.InputSystem.Key.LeftMeta];

		public global::UnityEngine.InputSystem.Controls.KeyControl rightMetaKey => this[global::UnityEngine.InputSystem.Key.RightMeta];

		public global::UnityEngine.InputSystem.Controls.KeyControl leftWindowsKey => this[global::UnityEngine.InputSystem.Key.LeftMeta];

		public global::UnityEngine.InputSystem.Controls.KeyControl rightWindowsKey => this[global::UnityEngine.InputSystem.Key.RightMeta];

		public global::UnityEngine.InputSystem.Controls.KeyControl leftAppleKey => this[global::UnityEngine.InputSystem.Key.LeftMeta];

		public global::UnityEngine.InputSystem.Controls.KeyControl rightAppleKey => this[global::UnityEngine.InputSystem.Key.RightMeta];

		public global::UnityEngine.InputSystem.Controls.KeyControl leftCommandKey => this[global::UnityEngine.InputSystem.Key.LeftMeta];

		public global::UnityEngine.InputSystem.Controls.KeyControl rightCommandKey => this[global::UnityEngine.InputSystem.Key.RightMeta];

		public global::UnityEngine.InputSystem.Controls.KeyControl contextMenuKey => this[global::UnityEngine.InputSystem.Key.ContextMenu];

		public global::UnityEngine.InputSystem.Controls.KeyControl escapeKey => this[global::UnityEngine.InputSystem.Key.Escape];

		public global::UnityEngine.InputSystem.Controls.KeyControl leftArrowKey => this[global::UnityEngine.InputSystem.Key.LeftArrow];

		public global::UnityEngine.InputSystem.Controls.KeyControl rightArrowKey => this[global::UnityEngine.InputSystem.Key.RightArrow];

		public global::UnityEngine.InputSystem.Controls.KeyControl upArrowKey => this[global::UnityEngine.InputSystem.Key.UpArrow];

		public global::UnityEngine.InputSystem.Controls.KeyControl downArrowKey => this[global::UnityEngine.InputSystem.Key.DownArrow];

		public global::UnityEngine.InputSystem.Controls.KeyControl backspaceKey => this[global::UnityEngine.InputSystem.Key.Backspace];

		public global::UnityEngine.InputSystem.Controls.KeyControl pageDownKey => this[global::UnityEngine.InputSystem.Key.PageDown];

		public global::UnityEngine.InputSystem.Controls.KeyControl pageUpKey => this[global::UnityEngine.InputSystem.Key.PageUp];

		public global::UnityEngine.InputSystem.Controls.KeyControl homeKey => this[global::UnityEngine.InputSystem.Key.Home];

		public global::UnityEngine.InputSystem.Controls.KeyControl endKey => this[global::UnityEngine.InputSystem.Key.End];

		public global::UnityEngine.InputSystem.Controls.KeyControl insertKey => this[global::UnityEngine.InputSystem.Key.Insert];

		public global::UnityEngine.InputSystem.Controls.KeyControl deleteKey => this[global::UnityEngine.InputSystem.Key.Delete];

		public global::UnityEngine.InputSystem.Controls.KeyControl capsLockKey => this[global::UnityEngine.InputSystem.Key.CapsLock];

		public global::UnityEngine.InputSystem.Controls.KeyControl scrollLockKey => this[global::UnityEngine.InputSystem.Key.ScrollLock];

		public global::UnityEngine.InputSystem.Controls.KeyControl numLockKey => this[global::UnityEngine.InputSystem.Key.NumLock];

		public global::UnityEngine.InputSystem.Controls.KeyControl printScreenKey => this[global::UnityEngine.InputSystem.Key.PrintScreen];

		public global::UnityEngine.InputSystem.Controls.KeyControl pauseKey => this[global::UnityEngine.InputSystem.Key.Pause];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpadEnterKey => this[global::UnityEngine.InputSystem.Key.NumpadEnter];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpadDivideKey => this[global::UnityEngine.InputSystem.Key.NumpadDivide];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpadMultiplyKey => this[global::UnityEngine.InputSystem.Key.NumpadMultiply];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpadMinusKey => this[global::UnityEngine.InputSystem.Key.NumpadMinus];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpadPlusKey => this[global::UnityEngine.InputSystem.Key.NumpadPlus];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpadPeriodKey => this[global::UnityEngine.InputSystem.Key.NumpadPeriod];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpadEqualsKey => this[global::UnityEngine.InputSystem.Key.NumpadEquals];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad0Key => this[global::UnityEngine.InputSystem.Key.Numpad0];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad1Key => this[global::UnityEngine.InputSystem.Key.Numpad1];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad2Key => this[global::UnityEngine.InputSystem.Key.Numpad2];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad3Key => this[global::UnityEngine.InputSystem.Key.Numpad3];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad4Key => this[global::UnityEngine.InputSystem.Key.Numpad4];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad5Key => this[global::UnityEngine.InputSystem.Key.Numpad5];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad6Key => this[global::UnityEngine.InputSystem.Key.Numpad6];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad7Key => this[global::UnityEngine.InputSystem.Key.Numpad7];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad8Key => this[global::UnityEngine.InputSystem.Key.Numpad8];

		public global::UnityEngine.InputSystem.Controls.KeyControl numpad9Key => this[global::UnityEngine.InputSystem.Key.Numpad9];

		public global::UnityEngine.InputSystem.Controls.KeyControl f1Key => this[global::UnityEngine.InputSystem.Key.F1];

		public global::UnityEngine.InputSystem.Controls.KeyControl f2Key => this[global::UnityEngine.InputSystem.Key.F2];

		public global::UnityEngine.InputSystem.Controls.KeyControl f3Key => this[global::UnityEngine.InputSystem.Key.F3];

		public global::UnityEngine.InputSystem.Controls.KeyControl f4Key => this[global::UnityEngine.InputSystem.Key.F4];

		public global::UnityEngine.InputSystem.Controls.KeyControl f5Key => this[global::UnityEngine.InputSystem.Key.F5];

		public global::UnityEngine.InputSystem.Controls.KeyControl f6Key => this[global::UnityEngine.InputSystem.Key.F6];

		public global::UnityEngine.InputSystem.Controls.KeyControl f7Key => this[global::UnityEngine.InputSystem.Key.F7];

		public global::UnityEngine.InputSystem.Controls.KeyControl f8Key => this[global::UnityEngine.InputSystem.Key.F8];

		public global::UnityEngine.InputSystem.Controls.KeyControl f9Key => this[global::UnityEngine.InputSystem.Key.F9];

		public global::UnityEngine.InputSystem.Controls.KeyControl f10Key => this[global::UnityEngine.InputSystem.Key.F10];

		public global::UnityEngine.InputSystem.Controls.KeyControl f11Key => this[global::UnityEngine.InputSystem.Key.F11];

		public global::UnityEngine.InputSystem.Controls.KeyControl f12Key => this[global::UnityEngine.InputSystem.Key.F12];

		public global::UnityEngine.InputSystem.Controls.KeyControl oem1Key => this[global::UnityEngine.InputSystem.Key.OEM1];

		public global::UnityEngine.InputSystem.Controls.KeyControl oem2Key => this[global::UnityEngine.InputSystem.Key.OEM2];

		public global::UnityEngine.InputSystem.Controls.KeyControl oem3Key => this[global::UnityEngine.InputSystem.Key.OEM3];

		public global::UnityEngine.InputSystem.Controls.KeyControl oem4Key => this[global::UnityEngine.InputSystem.Key.OEM4];

		public global::UnityEngine.InputSystem.Controls.KeyControl oem5Key => this[global::UnityEngine.InputSystem.Key.OEM5];

		public global::UnityEngine.InputSystem.Controls.KeyControl f13Key => this[global::UnityEngine.InputSystem.Key.F13];

		public global::UnityEngine.InputSystem.Controls.KeyControl f14Key => this[global::UnityEngine.InputSystem.Key.F14];

		public global::UnityEngine.InputSystem.Controls.KeyControl f15Key => this[global::UnityEngine.InputSystem.Key.F15];

		public global::UnityEngine.InputSystem.Controls.KeyControl f16Key => this[global::UnityEngine.InputSystem.Key.F16];

		public global::UnityEngine.InputSystem.Controls.KeyControl f17Key => this[global::UnityEngine.InputSystem.Key.F17];

		public global::UnityEngine.InputSystem.Controls.KeyControl f18Key => this[global::UnityEngine.InputSystem.Key.F18];

		public global::UnityEngine.InputSystem.Controls.KeyControl f19Key => this[global::UnityEngine.InputSystem.Key.F19];

		public global::UnityEngine.InputSystem.Controls.KeyControl f20Key => this[global::UnityEngine.InputSystem.Key.F20];

		public global::UnityEngine.InputSystem.Controls.KeyControl f21Key => this[global::UnityEngine.InputSystem.Key.F21];

		public global::UnityEngine.InputSystem.Controls.KeyControl f22Key => this[global::UnityEngine.InputSystem.Key.F22];

		public global::UnityEngine.InputSystem.Controls.KeyControl f23Key => this[global::UnityEngine.InputSystem.Key.F23];

		public global::UnityEngine.InputSystem.Controls.KeyControl f24Key => this[global::UnityEngine.InputSystem.Key.F24];

		public global::UnityEngine.InputSystem.Controls.KeyControl mediaPlayPause => this[global::UnityEngine.InputSystem.Key.MediaPlayPause];

		public global::UnityEngine.InputSystem.Controls.KeyControl mediaRewind => this[global::UnityEngine.InputSystem.Key.MediaRewind];

		public global::UnityEngine.InputSystem.Controls.KeyControl mediaForward => this[global::UnityEngine.InputSystem.Key.MediaForward];

		public global::UnityEngine.InputSystem.Controls.ButtonControl shiftKey { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl ctrlKey { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl altKey { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl imeSelected { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.KeyControl this[global::UnityEngine.InputSystem.Key key]
		{
			get
			{
				int num = (int)(key - 1);
				if (num < 0 || num >= m_Keys.Length)
				{
					throw new global::System.ArgumentOutOfRangeException(string.Format("{0}: {1}", "key", key));
				}
				return m_Keys[num];
			}
		}

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Controls.KeyControl> allKeys => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Controls.KeyControl>(m_Keys);

		public static global::UnityEngine.InputSystem.Keyboard current { get; private set; }

		protected global::UnityEngine.InputSystem.Controls.KeyControl[] keys
		{
			get
			{
				return m_Keys;
			}
			set
			{
				m_Keys = value;
			}
		}

		public event global::System.Action<char> onTextInput
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				if (!global::UnityEngine.InputSystem.Utilities.InputArrayExtensions.Contains(m_TextInputListeners, value))
				{
					m_TextInputListeners.Append(value);
				}
			}
			remove
			{
				m_TextInputListeners.Remove(value);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.LowLevel.IMECompositionString> onIMECompositionChange
		{
			add
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				if (!global::UnityEngine.InputSystem.Utilities.InputArrayExtensions.Contains(m_ImeCompositionListeners, value))
				{
					m_ImeCompositionListeners.Append(value);
				}
			}
			remove
			{
				m_ImeCompositionListeners.Remove(value);
			}
		}

		public void SetIMEEnabled(bool enabled)
		{
			global::UnityEngine.InputSystem.LowLevel.EnableIMECompositionCommand command = global::UnityEngine.InputSystem.LowLevel.EnableIMECompositionCommand.Create(enabled);
			ExecuteCommand(ref command);
		}

		public void SetIMECursorPosition(global::UnityEngine.Vector2 position)
		{
			global::UnityEngine.InputSystem.LowLevel.SetIMECursorPositionCommand command = global::UnityEngine.InputSystem.LowLevel.SetIMECursorPositionCommand.Create(position);
			ExecuteCommand(ref command);
		}

		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (current == this)
			{
				current = null;
			}
		}

		protected override void FinishSetup()
		{
			string[] array = new string[126]
			{
				"space", "enter", "tab", "backquote", "quote", "semicolon", "comma", "period", "slash", "backslash",
				"leftbracket", "rightbracket", "minus", "equals", "a", "b", "c", "d", "e", "f",
				"g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
				"q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
				"1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
				"leftshift", "rightshift", "leftalt", "rightalt", "leftctrl", "rightctrl", "leftmeta", "rightmeta", "contextmenu", "escape",
				"leftarrow", "rightarrow", "uparrow", "downarrow", "backspace", "pagedown", "pageup", "home", "end", "insert",
				"delete", "capslock", "numlock", "printscreen", "scrolllock", "pause", "numpadenter", "numpaddivide", "numpadmultiply", "numpadplus",
				"numpadminus", "numpadperiod", "numpadequals", "numpad0", "numpad1", "numpad2", "numpad3", "numpad4", "numpad5", "numpad6",
				"numpad7", "numpad8", "numpad9", "f1", "f2", "f3", "f4", "f5", "f6", "f7",
				"f8", "f9", "f10", "f11", "f12", "oem1", "oem2", "oem3", "oem4", "oem5",
				"IMESelectedObsoleteKey", "f13", "f14", "f15", "f16", "f17", "f18", "f19", "f20", "f21",
				"f22", "f23", "f24", "mediaPlayPause", "mediaRewind", "mediaForward"
			};
			m_Keys = new global::UnityEngine.InputSystem.Controls.KeyControl[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				m_Keys[i] = GetChildControl<global::UnityEngine.InputSystem.Controls.KeyControl>(array[i]);
				m_Keys[i].keyCode = (global::UnityEngine.InputSystem.Key)(i + 1);
			}
			anyKey = GetChildControl<global::UnityEngine.InputSystem.Controls.AnyKeyControl>("anyKey");
			shiftKey = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("shift");
			ctrlKey = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("ctrl");
			altKey = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("alt");
			imeSelected = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("IMESelected");
			base.FinishSetup();
		}

		protected override void RefreshConfiguration()
		{
			keyboardLayout = null;
			global::UnityEngine.InputSystem.LowLevel.QueryKeyboardLayoutCommand command = global::UnityEngine.InputSystem.LowLevel.QueryKeyboardLayoutCommand.Create();
			if (ExecuteCommand(ref command) >= 0)
			{
				keyboardLayout = command.ReadLayoutName();
			}
		}

		public void OnTextInput(char character)
		{
			for (int i = 0; i < m_TextInputListeners.length; i++)
			{
				m_TextInputListeners[i](character);
			}
		}

		public global::UnityEngine.InputSystem.Controls.KeyControl FindKeyOnCurrentKeyboardLayout(string displayName)
		{
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Controls.KeyControl> readOnlyArray = allKeys;
			for (int i = 0; i < readOnlyArray.Count; i++)
			{
				if (string.Equals(readOnlyArray[i].displayName, displayName, global::System.StringComparison.CurrentCultureIgnoreCase))
				{
					return readOnlyArray[i];
				}
			}
			return null;
		}

		public void OnIMECompositionChanged(global::UnityEngine.InputSystem.LowLevel.IMECompositionString compositionString)
		{
			if (m_ImeCompositionListeners.length > 0)
			{
				for (int i = 0; i < m_ImeCompositionListeners.length; i++)
				{
					m_ImeCompositionListeners[i](compositionString);
				}
			}
		}

		unsafe bool global::UnityEngine.InputSystem.LowLevel.IEventPreProcessor.PreProcessEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr)
		{
			if (currentEventPtr.type == 1398030676)
			{
				global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(currentEventPtr);
				if (ptr->stateFormat == global::UnityEngine.InputSystem.LowLevel.KeyboardState.Format)
				{
					global::UnityEngine.InputSystem.LowLevel.KeyboardState* ptr2 = (global::UnityEngine.InputSystem.LowLevel.KeyboardState*)ptr->stateData;
					if (ptr2->Get(global::UnityEngine.InputSystem.Key.IMESelected))
					{
						ptr2->Set(global::UnityEngine.InputSystem.Key.IMESelected, state: false);
						ptr2->Set((global::UnityEngine.InputSystem.Key)127, state: true);
					}
				}
			}
			return true;
		}
	}
}
