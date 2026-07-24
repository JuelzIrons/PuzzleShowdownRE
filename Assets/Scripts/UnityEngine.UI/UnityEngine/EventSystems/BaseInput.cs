namespace UnityEngine.EventSystems
{
	public class BaseInput : global::UnityEngine.EventSystems.UIBehaviour
	{
		public virtual string compositionString => global::UnityEngine.Input.compositionString;

		public virtual global::UnityEngine.IMECompositionMode imeCompositionMode
		{
			get
			{
				return global::UnityEngine.Input.imeCompositionMode;
			}
			set
			{
				global::UnityEngine.Input.imeCompositionMode = value;
			}
		}

		public virtual global::UnityEngine.Vector2 compositionCursorPos
		{
			get
			{
				return global::UnityEngine.Input.compositionCursorPos;
			}
			set
			{
				global::UnityEngine.Input.compositionCursorPos = value;
			}
		}

		public virtual bool mousePresent => global::UnityEngine.Input.mousePresent;

		public virtual global::UnityEngine.Vector2 mousePosition => global::UnityEngine.Input.mousePosition;

		public virtual global::UnityEngine.Vector2 mouseScrollDelta => global::UnityEngine.Input.mouseScrollDelta;

		public virtual float mouseScrollDeltaPerTick => 1f;

		public virtual bool touchSupported => global::UnityEngine.Input.touchSupported;

		public virtual int touchCount => global::UnityEngine.Input.touchCount;

		public virtual bool GetMouseButtonDown(int button)
		{
			return global::UnityEngine.Input.GetMouseButtonDown(button);
		}

		public virtual bool GetMouseButtonUp(int button)
		{
			return global::UnityEngine.Input.GetMouseButtonUp(button);
		}

		public virtual bool GetMouseButton(int button)
		{
			return global::UnityEngine.Input.GetMouseButton(button);
		}

		public virtual global::UnityEngine.Touch GetTouch(int index)
		{
			return global::UnityEngine.Input.GetTouch(index);
		}

		public virtual float GetAxisRaw(string axisName)
		{
			return global::UnityEngine.Input.GetAxisRaw(axisName);
		}

		public virtual bool GetButtonDown(string buttonName)
		{
			return global::UnityEngine.Input.GetButtonDown(buttonName);
		}
	}
}
