namespace UnityEngine.InputSystem.LowLevel
{
	public interface ITextInputReceiver
	{
		void OnTextInput(char character);

		void OnIMECompositionChanged(global::UnityEngine.InputSystem.LowLevel.IMECompositionString compositionString);
	}
}
