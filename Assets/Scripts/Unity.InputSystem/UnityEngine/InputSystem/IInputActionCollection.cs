namespace UnityEngine.InputSystem
{
	public interface IInputActionCollection : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputAction>, global::System.Collections.IEnumerable
	{
		global::UnityEngine.InputSystem.InputBinding? bindingMask { get; set; }

		global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? devices { get; set; }

		global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControlScheme> controlSchemes { get; }

		bool Contains(global::UnityEngine.InputSystem.InputAction action);

		void Enable();

		void Disable();
	}
}
