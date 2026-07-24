namespace UnityEngine.InputSystem.UI
{
	public class ExtendedPointerEventData : global::UnityEngine.EventSystems.PointerEventData
	{
		public global::UnityEngine.InputSystem.InputControl control { get; set; }

		public global::UnityEngine.InputSystem.InputDevice device { get; set; }

		public int touchId { get; set; }

		public global::UnityEngine.InputSystem.UI.UIPointerType pointerType { get; set; }

		public int uiToolkitPointerId { get; set; }

		public global::UnityEngine.Vector3 trackedDevicePosition { get; set; }

		public global::UnityEngine.Quaternion trackedDeviceOrientation { get; set; }

		public ExtendedPointerEventData(global::UnityEngine.EventSystems.EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendLine("button: " + base.button);
			stringBuilder.AppendLine("clickTime: " + base.clickTime);
			stringBuilder.AppendLine("clickCount: " + base.clickCount);
			stringBuilder.AppendLine("device: " + device);
			stringBuilder.AppendLine("pointerType: " + pointerType);
			stringBuilder.AppendLine("touchId: " + touchId);
			stringBuilder.AppendLine("pressPosition: " + base.pressPosition);
			stringBuilder.AppendLine("trackedDevicePosition: " + trackedDevicePosition);
			stringBuilder.AppendLine("trackedDeviceOrientation: " + trackedDeviceOrientation);
			stringBuilder.AppendLine("pressure" + base.pressure);
			stringBuilder.AppendLine("radius: " + base.radius);
			stringBuilder.AppendLine("azimuthAngle: " + base.azimuthAngle);
			stringBuilder.AppendLine("altitudeAngle: " + base.altitudeAngle);
			stringBuilder.AppendLine("twist: " + base.twist);
			stringBuilder.AppendLine("displayIndex: " + base.displayIndex);
			return stringBuilder.ToString();
		}

		internal static int MakePointerIdForTouch(int deviceId, int touchId)
		{
			return (deviceId << 24) + touchId;
		}

		internal static int TouchIdFromPointerId(int pointerId)
		{
			return pointerId & 0xFF;
		}

		internal void ReadDeviceState()
		{
			if (control.parent is global::UnityEngine.InputSystem.Pen pen)
			{
				uiToolkitPointerId = GetPenPointerId(pen);
				base.pressure = pen.pressure.magnitude;
				base.azimuthAngle = (pen.tilt.value.x + 1f) * global::System.MathF.PI / 2f;
				base.altitudeAngle = (pen.tilt.value.y + 1f) * global::System.MathF.PI / 2f;
				base.twist = pen.twist.value * global::System.MathF.PI * 2f;
				base.displayIndex = pen.displayIndex.ReadValue();
			}
			else if (control.parent is global::UnityEngine.InputSystem.Controls.TouchControl touchControl)
			{
				uiToolkitPointerId = GetTouchPointerId(touchControl);
				base.pressure = touchControl.pressure.magnitude;
				base.radius = touchControl.radius.value;
				base.displayIndex = touchControl.displayIndex.ReadValue();
			}
			else if (control.parent is global::UnityEngine.InputSystem.Touchscreen touchscreen)
			{
				uiToolkitPointerId = GetTouchPointerId(touchscreen.primaryTouch);
				base.pressure = touchscreen.pressure.magnitude;
				base.radius = touchscreen.radius.value;
				base.displayIndex = touchscreen.displayIndex.ReadValue();
			}
			else
			{
				uiToolkitPointerId = global::UnityEngine.UIElements.PointerId.mousePointerId;
			}
		}

		private static int GetPenPointerId(global::UnityEngine.InputSystem.Pen pen)
		{
			int num = 0;
			foreach (global::UnityEngine.InputSystem.InputDevice device in global::UnityEngine.InputSystem.InputSystem.devices)
			{
				if (device is global::UnityEngine.InputSystem.Pen pen2)
				{
					if (pen == pen2)
					{
						return global::UnityEngine.UIElements.PointerId.penPointerIdBase + global::UnityEngine.Mathf.Min(num, global::UnityEngine.UIElements.PointerId.penPointerCount - 1);
					}
					num++;
				}
			}
			return global::UnityEngine.UIElements.PointerId.penPointerIdBase;
		}

		private static int GetTouchPointerId(global::UnityEngine.InputSystem.Controls.TouchControl touchControl)
		{
			int value = global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.IndexOfReference(((global::UnityEngine.InputSystem.Touchscreen)touchControl.device).touches, touchControl);
			return global::UnityEngine.UIElements.PointerId.touchPointerIdBase + global::UnityEngine.Mathf.Clamp(value, 0, global::UnityEngine.UIElements.PointerId.touchPointerCount - 1);
		}
	}
}
