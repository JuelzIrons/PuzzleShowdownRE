namespace UnityEngine.UI
{
	internal static class MultipleDisplayUtilities
	{
		public static bool GetRelativeMousePositionForDrag(global::UnityEngine.EventSystems.PointerEventData eventData, ref global::UnityEngine.Vector2 position)
		{
			int displayIndex = eventData.pointerPressRaycast.displayIndex;
			global::UnityEngine.Vector3 vector = RelativeMouseAtScaled(eventData.position, eventData.displayIndex);
			if ((int)vector.z != displayIndex)
			{
				return false;
			}
			position = ((displayIndex != 0) ? ((global::UnityEngine.Vector2)vector) : eventData.position);
			return true;
		}

		internal static global::UnityEngine.Vector3 GetRelativeMousePositionForRaycast(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::UnityEngine.Vector3 vector = RelativeMouseAtScaled(eventData.position, eventData.displayIndex);
			if (vector == global::UnityEngine.Vector3.zero)
			{
				vector = eventData.position;
			}
			if (eventData.displayIndex > 0)
			{
				vector.z = eventData.displayIndex;
			}
			return vector;
		}

		public static global::UnityEngine.Vector3 RelativeMouseAtScaled(global::UnityEngine.Vector2 position, int displayIndex)
		{
			global::UnityEngine.Display main = global::UnityEngine.Display.main;
			if (!global::UnityEngine.Screen.fullScreen)
			{
				return new global::UnityEngine.Vector3(position.x, position.y, displayIndex);
			}
			if (displayIndex >= global::UnityEngine.Display.displays.Length)
			{
				displayIndex = 0;
			}
			main = global::UnityEngine.Display.displays[displayIndex];
			if (main.renderingWidth != main.systemWidth || main.renderingHeight != main.systemHeight)
			{
				float num = (float)main.systemWidth / (float)main.systemHeight;
				global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(main.renderingWidth, main.renderingHeight);
				global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
				if (global::UnityEngine.Screen.fullScreen)
				{
					float num2 = (float)global::UnityEngine.Screen.width / (float)global::UnityEngine.Screen.height;
					if ((float)main.systemHeight * num2 < (float)main.systemWidth)
					{
						vector.x = (float)main.renderingHeight * num;
						zero.x = (vector.x - (float)main.renderingWidth) * 0.5f;
					}
					else
					{
						vector.y = (float)main.renderingWidth / num;
						zero.y = (vector.y - (float)main.renderingHeight) * 0.5f;
					}
				}
				global::UnityEngine.Vector2 vector2 = vector - zero;
				if (position.y < 0f - zero.y || position.y > vector2.y || position.x < 0f - zero.x || position.x > vector2.x)
				{
					global::UnityEngine.Vector2 vector3 = position;
					if (!global::UnityEngine.Screen.fullScreen)
					{
						vector3.x -= (float)(main.renderingWidth - main.systemWidth) * 0.5f;
						vector3.y -= (float)(main.renderingHeight - main.systemHeight) * 0.5f;
					}
					else
					{
						vector3 += zero;
						vector3.x *= (float)main.systemWidth / vector.x;
						vector3.y *= (float)main.systemHeight / vector.y;
					}
					global::UnityEngine.Vector3 result = new global::UnityEngine.Vector3(vector3.x, vector3.y, displayIndex);
					if (result.z != 0f)
					{
						return result;
					}
				}
				return new global::UnityEngine.Vector3(position.x, position.y, 0f);
			}
			return new global::UnityEngine.Vector3(position.x, position.y, displayIndex);
		}
	}
}
