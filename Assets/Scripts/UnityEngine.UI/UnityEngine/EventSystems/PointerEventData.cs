namespace UnityEngine.EventSystems
{
	public class PointerEventData : global::UnityEngine.EventSystems.BaseEventData
	{
		public enum InputButton
		{
			Left = 0,
			Right = 1,
			Middle = 2
		}

		public enum FramePressState
		{
			Pressed = 0,
			Released = 1,
			PressedAndReleased = 2,
			NotChanged = 3
		}

		private global::UnityEngine.GameObject m_PointerPress;

		public global::System.Collections.Generic.List<global::UnityEngine.GameObject> hovered = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		public global::UnityEngine.GameObject pointerEnter { get; set; }

		public global::UnityEngine.GameObject lastPress { get; private set; }

		public global::UnityEngine.GameObject rawPointerPress { get; set; }

		public global::UnityEngine.GameObject pointerDrag { get; set; }

		public global::UnityEngine.GameObject pointerClick { get; set; }

		public global::UnityEngine.EventSystems.RaycastResult pointerCurrentRaycast { get; set; }

		public global::UnityEngine.EventSystems.RaycastResult pointerPressRaycast { get; set; }

		public bool eligibleForClick { get; set; }

		public int displayIndex { get; set; }

		public int pointerId { get; set; }

		public global::UnityEngine.Vector2 position { get; set; }

		public global::UnityEngine.Vector2 delta { get; set; }

		public global::UnityEngine.Vector2 pressPosition { get; set; }

		[global::System.Obsolete("Use either pointerCurrentRaycast.worldPosition or pointerPressRaycast.worldPosition")]
		public global::UnityEngine.Vector3 worldPosition { get; set; }

		[global::System.Obsolete("Use either pointerCurrentRaycast.worldNormal or pointerPressRaycast.worldNormal")]
		public global::UnityEngine.Vector3 worldNormal { get; set; }

		public float clickTime { get; set; }

		public int clickCount { get; set; }

		public global::UnityEngine.Vector2 scrollDelta { get; set; }

		public bool useDragThreshold { get; set; }

		public bool dragging { get; set; }

		public global::UnityEngine.EventSystems.PointerEventData.InputButton button { get; set; }

		public float pressure { get; set; }

		public float tangentialPressure { get; set; }

		public float altitudeAngle { get; set; }

		public float azimuthAngle { get; set; }

		public float twist { get; set; }

		public global::UnityEngine.Vector2 tilt { get; set; }

		public global::UnityEngine.PenStatus penStatus { get; set; }

		public global::UnityEngine.Vector2 radius { get; set; }

		public global::UnityEngine.Vector2 radiusVariance { get; set; }

		public bool fullyExited { get; set; }

		public bool reentered { get; set; }

		public global::UnityEngine.Camera enterEventCamera
		{
			get
			{
				if (!(pointerCurrentRaycast.module == null))
				{
					return pointerCurrentRaycast.module.eventCamera;
				}
				return null;
			}
		}

		public global::UnityEngine.Camera pressEventCamera
		{
			get
			{
				if (!(pointerPressRaycast.module == null))
				{
					return pointerPressRaycast.module.eventCamera;
				}
				return null;
			}
		}

		public global::UnityEngine.GameObject pointerPress
		{
			get
			{
				return m_PointerPress;
			}
			set
			{
				if (!(m_PointerPress == value))
				{
					lastPress = m_PointerPress;
					m_PointerPress = value;
				}
			}
		}

		public PointerEventData(global::UnityEngine.EventSystems.EventSystem eventSystem)
			: base(eventSystem)
		{
			eligibleForClick = false;
			displayIndex = 0;
			pointerId = -1;
			position = global::UnityEngine.Vector2.zero;
			delta = global::UnityEngine.Vector2.zero;
			pressPosition = global::UnityEngine.Vector2.zero;
			clickTime = 0f;
			clickCount = 0;
			scrollDelta = global::UnityEngine.Vector2.zero;
			useDragThreshold = true;
			dragging = false;
			button = global::UnityEngine.EventSystems.PointerEventData.InputButton.Left;
			pressure = 0f;
			tangentialPressure = 0f;
			altitudeAngle = 0f;
			azimuthAngle = 0f;
			twist = 0f;
			tilt = new global::UnityEngine.Vector2(0f, 0f);
			penStatus = global::UnityEngine.PenStatus.None;
			radius = global::UnityEngine.Vector2.zero;
			radiusVariance = global::UnityEngine.Vector2.zero;
		}

		public bool IsPointerMoving()
		{
			return delta.sqrMagnitude > 0f;
		}

		public bool IsScrolling()
		{
			return scrollDelta.sqrMagnitude > 0f;
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.AppendLine("<b>Position</b>: " + position);
			stringBuilder.AppendLine("<b>delta</b>: " + delta);
			stringBuilder.AppendLine("<b>eligibleForClick</b>: " + eligibleForClick);
			stringBuilder.AppendLine("<b>pointerEnter</b>: " + pointerEnter);
			stringBuilder.AppendLine("<b>pointerPress</b>: " + pointerPress);
			stringBuilder.AppendLine("<b>lastPointerPress</b>: " + lastPress);
			stringBuilder.AppendLine("<b>pointerDrag</b>: " + pointerDrag);
			stringBuilder.AppendLine("<b>Use Drag Threshold</b>: " + useDragThreshold);
			stringBuilder.AppendLine("<b>Current Raycast:</b>");
			stringBuilder.AppendLine(pointerCurrentRaycast.ToString());
			stringBuilder.AppendLine("<b>Press Raycast:</b>");
			stringBuilder.AppendLine(pointerPressRaycast.ToString());
			stringBuilder.AppendLine("<b>Display Index:</b>");
			stringBuilder.AppendLine(displayIndex.ToString());
			stringBuilder.AppendLine("<b>pressure</b>: " + pressure);
			stringBuilder.AppendLine("<b>tangentialPressure</b>: " + tangentialPressure);
			stringBuilder.AppendLine("<b>altitudeAngle</b>: " + altitudeAngle);
			stringBuilder.AppendLine("<b>azimuthAngle</b>: " + azimuthAngle);
			stringBuilder.AppendLine("<b>twist</b>: " + twist);
			stringBuilder.AppendLine("<b>tilt</b>: " + tilt);
			stringBuilder.AppendLine("<b>penStatus</b>: " + penStatus);
			stringBuilder.AppendLine("<b>radius</b>: " + radius);
			stringBuilder.AppendLine("<b>radiusVariance</b>: " + radiusVariance);
			return stringBuilder.ToString();
		}
	}
}
