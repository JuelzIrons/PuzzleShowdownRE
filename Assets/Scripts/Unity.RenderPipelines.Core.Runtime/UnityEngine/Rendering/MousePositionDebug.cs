namespace UnityEngine.Rendering
{
	public class MousePositionDebug
	{
		private static global::UnityEngine.Rendering.MousePositionDebug s_Instance;

		public static global::UnityEngine.Rendering.MousePositionDebug instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::UnityEngine.Rendering.MousePositionDebug();
				}
				return s_Instance;
			}
		}

		public void Build()
		{
		}

		public void Cleanup()
		{
		}

		public global::UnityEngine.Vector2 GetMousePosition(float ScreenHeight, bool sceneView)
		{
			return GetInputMousePosition();
		}

		private global::UnityEngine.Vector2 GetInputMousePosition()
		{
			if (global::UnityEngine.InputSystem.Pointer.current == null)
			{
				return new global::UnityEngine.Vector2(-1f, -1f);
			}
			return global::UnityEngine.InputSystem.Pointer.current.position.ReadValue();
		}

		public global::UnityEngine.Vector2 GetMouseClickPosition(float ScreenHeight)
		{
			return global::UnityEngine.Vector2.zero;
		}
	}
}
