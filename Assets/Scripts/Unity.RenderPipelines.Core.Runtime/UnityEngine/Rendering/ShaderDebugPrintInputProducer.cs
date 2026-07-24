namespace UnityEngine.Rendering
{
	public static class ShaderDebugPrintInputProducer
	{
		public static global::UnityEngine.Rendering.ShaderDebugPrintInput Get()
		{
			global::UnityEngine.Rendering.ShaderDebugPrintInput result = default(global::UnityEngine.Rendering.ShaderDebugPrintInput);
			result.pos = global::UnityEngine.Input.mousePosition;
			result.leftDown = global::UnityEngine.Input.GetMouseButton(0);
			result.rightDown = global::UnityEngine.Input.GetMouseButton(1);
			result.middleDown = global::UnityEngine.Input.GetMouseButton(2);
			global::UnityEngine.InputSystem.Mouse current = global::UnityEngine.InputSystem.Mouse.current;
			result.pos = current.position.ReadValue();
			result.leftDown = current.leftButton.isPressed;
			result.rightDown = current.rightButton.isPressed;
			result.middleDown = current.middleButton.isPressed;
			return result;
		}
	}
}
