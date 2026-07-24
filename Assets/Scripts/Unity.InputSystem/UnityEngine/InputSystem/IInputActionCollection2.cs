namespace UnityEngine.InputSystem
{
	public interface IInputActionCollection2 : global::UnityEngine.InputSystem.IInputActionCollection, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputAction>, global::System.Collections.IEnumerable
	{
		global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputBinding> bindings { get; }

		global::UnityEngine.InputSystem.InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false);

		int FindBinding(global::UnityEngine.InputSystem.InputBinding mask, out global::UnityEngine.InputSystem.InputAction action);
	}
}
