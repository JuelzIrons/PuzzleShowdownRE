namespace UnityEngine.InputSystem.LowLevel
{
	public struct KeyboardState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		private const int kSizeInBits = 126;

		internal const int kSizeInBytes = 16;

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "anyKey", displayName = "Any Key", layout = "AnyKey", bit = 1u, sizeInBits = 126u, synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "escape", displayName = "Escape", layout = "Key", usages = new string[] { "Back", "Cancel" }, bit = 60u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "space", displayName = "Space", layout = "Key", bit = 1u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "enter", displayName = "Enter", layout = "Key", usage = "Submit", bit = 2u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "tab", displayName = "Tab", layout = "Key", bit = 3u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "backquote", displayName = "`", layout = "Key", bit = 4u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "quote", displayName = "'", layout = "Key", bit = 5u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "semicolon", displayName = ";", layout = "Key", bit = 6u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "comma", displayName = ",", layout = "Key", bit = 7u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "period", displayName = ".", layout = "Key", bit = 8u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "slash", displayName = "/", layout = "Key", bit = 9u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "backslash", displayName = "\\", layout = "Key", bit = 10u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftBracket", displayName = "[", layout = "Key", bit = 11u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightBracket", displayName = "]", layout = "Key", bit = 12u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "minus", displayName = "-", layout = "Key", bit = 13u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "equals", displayName = "=", layout = "Key", bit = 14u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "upArrow", displayName = "Up Arrow", layout = "Key", bit = 63u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "downArrow", displayName = "Down Arrow", layout = "Key", bit = 64u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftArrow", displayName = "Left Arrow", layout = "Key", bit = 61u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightArrow", displayName = "Right Arrow", layout = "Key", bit = 62u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "a", displayName = "A", layout = "Key", bit = 15u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "b", displayName = "B", layout = "Key", bit = 16u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "c", displayName = "C", layout = "Key", bit = 17u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "d", displayName = "D", layout = "Key", bit = 18u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "e", displayName = "E", layout = "Key", bit = 19u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f", displayName = "F", layout = "Key", bit = 20u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "g", displayName = "G", layout = "Key", bit = 21u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "h", displayName = "H", layout = "Key", bit = 22u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "i", displayName = "I", layout = "Key", bit = 23u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "j", displayName = "J", layout = "Key", bit = 24u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "k", displayName = "K", layout = "Key", bit = 25u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "l", displayName = "L", layout = "Key", bit = 26u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "m", displayName = "M", layout = "Key", bit = 27u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "n", displayName = "N", layout = "Key", bit = 28u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "o", displayName = "O", layout = "Key", bit = 29u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "p", displayName = "P", layout = "Key", bit = 30u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "q", displayName = "Q", layout = "Key", bit = 31u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "r", displayName = "R", layout = "Key", bit = 32u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "s", displayName = "S", layout = "Key", bit = 33u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "t", displayName = "T", layout = "Key", bit = 34u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "u", displayName = "U", layout = "Key", bit = 35u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "v", displayName = "V", layout = "Key", bit = 36u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "w", displayName = "W", layout = "Key", bit = 37u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "x", displayName = "X", layout = "Key", bit = 38u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "y", displayName = "Y", layout = "Key", bit = 39u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "z", displayName = "Z", layout = "Key", bit = 40u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "1", displayName = "1", layout = "Key", bit = 41u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "2", displayName = "2", layout = "Key", bit = 42u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "3", displayName = "3", layout = "Key", bit = 43u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "4", displayName = "4", layout = "Key", bit = 44u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "5", displayName = "5", layout = "Key", bit = 45u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "6", displayName = "6", layout = "Key", bit = 46u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "7", displayName = "7", layout = "Key", bit = 47u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "8", displayName = "8", layout = "Key", bit = 48u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "9", displayName = "9", layout = "Key", bit = 49u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "0", displayName = "0", layout = "Key", bit = 50u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftShift", displayName = "Left Shift", layout = "Key", usage = "Modifier", bit = 51u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightShift", displayName = "Right Shift", layout = "Key", usage = "Modifier", bit = 52u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "shift", displayName = "Shift", layout = "DiscreteButton", usage = "Modifier", bit = 51u, sizeInBits = 2u, synthetic = true, parameters = "minValue=1,maxValue=3,writeMode=1")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftAlt", displayName = "Left Alt", layout = "Key", usage = "Modifier", bit = 53u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightAlt", displayName = "Right Alt", layout = "Key", usage = "Modifier", bit = 54u, alias = "AltGr")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "alt", displayName = "Alt", layout = "DiscreteButton", usage = "Modifier", bit = 53u, sizeInBits = 2u, synthetic = true, parameters = "minValue=1,maxValue=3,writeMode=1")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftCtrl", displayName = "Left Control", layout = "Key", usage = "Modifier", bit = 55u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightCtrl", displayName = "Right Control", layout = "Key", usage = "Modifier", bit = 56u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "ctrl", displayName = "Control", layout = "DiscreteButton", usage = "Modifier", bit = 55u, sizeInBits = 2u, synthetic = true, parameters = "minValue=1,maxValue=3,writeMode=1")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftMeta", displayName = "Left System", layout = "Key", usage = "Modifier", bit = 57u, aliases = new string[] { "LeftWindows", "LeftApple", "LeftCommand" })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightMeta", displayName = "Right System", layout = "Key", usage = "Modifier", bit = 58u, aliases = new string[] { "RightWindows", "RightApple", "RightCommand" })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "contextMenu", displayName = "Context Menu", layout = "Key", usage = "Modifier", bit = 59u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "backspace", displayName = "Backspace", layout = "Key", bit = 65u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "pageDown", displayName = "Page Down", layout = "Key", bit = 66u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "pageUp", displayName = "Page Up", layout = "Key", bit = 67u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "home", displayName = "Home", layout = "Key", bit = 68u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "end", displayName = "End", layout = "Key", bit = 69u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "insert", displayName = "Insert", layout = "Key", bit = 70u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "delete", displayName = "Delete", layout = "Key", bit = 71u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "capsLock", displayName = "Caps Lock", layout = "Key", bit = 72u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numLock", displayName = "Num Lock", layout = "Key", bit = 73u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "printScreen", displayName = "Print Screen", layout = "Key", bit = 74u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "scrollLock", displayName = "Scroll Lock", layout = "Key", bit = 75u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "pause", displayName = "Pause/Break", layout = "Key", bit = 76u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpadEnter", displayName = "Numpad Enter", layout = "Key", bit = 77u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpadDivide", displayName = "Numpad /", layout = "Key", bit = 78u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpadMultiply", displayName = "Numpad *", layout = "Key", bit = 79u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpadPlus", displayName = "Numpad +", layout = "Key", bit = 80u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpadMinus", displayName = "Numpad -", layout = "Key", bit = 81u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpadPeriod", displayName = "Numpad .", layout = "Key", bit = 82u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpadEquals", displayName = "Numpad =", layout = "Key", bit = 83u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad1", displayName = "Numpad 1", layout = "Key", bit = 85u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad2", displayName = "Numpad 2", layout = "Key", bit = 86u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad3", displayName = "Numpad 3", layout = "Key", bit = 87u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad4", displayName = "Numpad 4", layout = "Key", bit = 88u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad5", displayName = "Numpad 5", layout = "Key", bit = 89u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad6", displayName = "Numpad 6", layout = "Key", bit = 90u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad7", displayName = "Numpad 7", layout = "Key", bit = 91u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad8", displayName = "Numpad 8", layout = "Key", bit = 92u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad9", displayName = "Numpad 9", layout = "Key", bit = 93u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "numpad0", displayName = "Numpad 0", layout = "Key", bit = 84u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f1", displayName = "F1", layout = "Key", bit = 94u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f2", displayName = "F2", layout = "Key", bit = 95u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f3", displayName = "F3", layout = "Key", bit = 96u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f4", displayName = "F4", layout = "Key", bit = 97u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f5", displayName = "F5", layout = "Key", bit = 98u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f6", displayName = "F6", layout = "Key", bit = 99u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f7", displayName = "F7", layout = "Key", bit = 100u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f8", displayName = "F8", layout = "Key", bit = 101u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f9", displayName = "F9", layout = "Key", bit = 102u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f10", displayName = "F10", layout = "Key", bit = 103u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f11", displayName = "F11", layout = "Key", bit = 104u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f12", displayName = "F12", layout = "Key", bit = 105u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "OEM1", layout = "Key", bit = 106u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "OEM2", layout = "Key", bit = 107u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "OEM3", layout = "Key", bit = 108u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "OEM4", layout = "Key", bit = 109u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "OEM5", layout = "Key", bit = 110u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f13", displayName = "F13", layout = "Key", bit = 112u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f14", displayName = "F14", layout = "Key", bit = 113u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f15", displayName = "F15", layout = "Key", bit = 114u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f16", displayName = "F16", layout = "Key", bit = 115u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f17", displayName = "F17", layout = "Key", bit = 116u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f18", displayName = "F18", layout = "Key", bit = 117u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f19", displayName = "F19", layout = "Key", bit = 118u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f20", displayName = "F20", layout = "Key", bit = 119u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f21", displayName = "F21", layout = "Key", bit = 120u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f22", displayName = "F22", layout = "Key", bit = 121u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f23", displayName = "F23", layout = "Key", bit = 122u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "f24", displayName = "F24", layout = "Key", bit = 123u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "mediaPlayPause", displayName = "MediaPlayPause", layout = "Key", bit = 124u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "mediaRewind", displayName = "MediaRewind", layout = "Key", bit = 125u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "mediaForward", displayName = "MediaForward", layout = "Key", bit = 126u)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "IMESelected", layout = "Button", bit = 127u, synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "IMESelectedObsoleteKey", layout = "Key", bit = 127u, synthetic = true)]
		public unsafe fixed byte keys[16];

		public static global::UnityEngine.InputSystem.Utilities.FourCC Format => new global::UnityEngine.InputSystem.Utilities.FourCC('K', 'E', 'Y', 'S');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => Format;

		public KeyboardState(params global::UnityEngine.InputSystem.Key[] pressedKeys)
			: this(IMESelected: false, pressedKeys)
		{
		}

		public unsafe KeyboardState(bool IMESelected, params global::UnityEngine.InputSystem.Key[] pressedKeys)
		{
			if (pressedKeys == null)
			{
				throw new global::System.ArgumentNullException("pressedKeys");
			}
			fixed (byte* ptr = keys)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(ptr, 16L);
				if (IMESelected)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, 111u, value: true);
				}
				for (int i = 0; i < pressedKeys.Length; i++)
				{
					global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, (uint)pressedKeys[i], value: true);
				}
			}
		}

		public unsafe void Set(global::UnityEngine.InputSystem.Key key, bool state)
		{
			fixed (byte* ptr = keys)
			{
				global::UnityEngine.InputSystem.Utilities.MemoryHelpers.WriteSingleBit(ptr, (uint)key, state);
			}
		}

		internal unsafe bool Get(global::UnityEngine.InputSystem.Key key)
		{
			fixed (byte* ptr = keys)
			{
				return global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadSingleBit(ptr, (uint)key);
			}
		}

		public void Press(global::UnityEngine.InputSystem.Key key)
		{
			Set(key, state: true);
		}

		public void Release(global::UnityEngine.InputSystem.Key key)
		{
			Set(key, state: false);
		}
	}
}
