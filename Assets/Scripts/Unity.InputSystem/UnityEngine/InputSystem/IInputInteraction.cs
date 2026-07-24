namespace UnityEngine.InputSystem
{
	public interface IInputInteraction
	{
		void Process(ref global::UnityEngine.InputSystem.InputInteractionContext context);

		void Reset();
	}
	public interface IInputInteraction<TValue> : global::UnityEngine.InputSystem.IInputInteraction where TValue : struct
	{
	}
}
