namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("Event/Graphic Raycaster")]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Canvas))]
	public class GraphicRaycaster : global::UnityEngine.EventSystems.BaseRaycaster
	{
		public enum BlockingObjects
		{
			None = 0,
			TwoD = 1,
			ThreeD = 2,
			All = 3
		}

		protected const int kNoEventMaskSet = -1;

		[global::UnityEngine.Serialization.FormerlySerializedAs("ignoreReversedGraphics")]
		[global::UnityEngine.SerializeField]
		private bool m_IgnoreReversedGraphics = true;

		[global::UnityEngine.Serialization.FormerlySerializedAs("blockingObjects")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.GraphicRaycaster.BlockingObjects m_BlockingObjects;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.LayerMask m_BlockingMask = -1;

		private global::UnityEngine.Canvas m_Canvas;

		[global::System.NonSerialized]
		private global::System.Collections.Generic.List<global::UnityEngine.UI.Graphic> m_RaycastResults = new global::System.Collections.Generic.List<global::UnityEngine.UI.Graphic>();

		[global::System.NonSerialized]
		private static readonly global::System.Collections.Generic.List<global::UnityEngine.UI.Graphic> s_SortedGraphics = new global::System.Collections.Generic.List<global::UnityEngine.UI.Graphic>();

		public override int sortOrderPriority
		{
			get
			{
				if (canvas.renderMode == global::UnityEngine.RenderMode.ScreenSpaceOverlay)
				{
					return canvas.sortingOrder;
				}
				return base.sortOrderPriority;
			}
		}

		public override int renderOrderPriority
		{
			get
			{
				if (canvas.renderMode == global::UnityEngine.RenderMode.ScreenSpaceOverlay)
				{
					return canvas.rootCanvas.renderOrder;
				}
				return base.renderOrderPriority;
			}
		}

		public bool ignoreReversedGraphics
		{
			get
			{
				return m_IgnoreReversedGraphics;
			}
			set
			{
				m_IgnoreReversedGraphics = value;
			}
		}

		public global::UnityEngine.UI.GraphicRaycaster.BlockingObjects blockingObjects
		{
			get
			{
				return m_BlockingObjects;
			}
			set
			{
				m_BlockingObjects = value;
			}
		}

		public global::UnityEngine.LayerMask blockingMask
		{
			get
			{
				return m_BlockingMask;
			}
			set
			{
				m_BlockingMask = value;
			}
		}

		private global::UnityEngine.Canvas canvas
		{
			get
			{
				if (m_Canvas != null)
				{
					return m_Canvas;
				}
				m_Canvas = GetComponent<global::UnityEngine.Canvas>();
				return m_Canvas;
			}
		}

		public override global::UnityEngine.Camera eventCamera
		{
			get
			{
				global::UnityEngine.Canvas canvas = this.canvas;
				global::UnityEngine.RenderMode renderMode = canvas.renderMode;
				if (renderMode == global::UnityEngine.RenderMode.ScreenSpaceOverlay || (renderMode == global::UnityEngine.RenderMode.ScreenSpaceCamera && canvas.worldCamera == null))
				{
					return null;
				}
				return canvas.worldCamera ?? global::UnityEngine.Camera.main;
			}
		}

		protected GraphicRaycaster()
		{
		}

		public override void Raycast(global::UnityEngine.EventSystems.PointerEventData eventData, global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> resultAppendList)
		{
			if (canvas == null)
			{
				return;
			}
			global::System.Collections.Generic.IList<global::UnityEngine.UI.Graphic> raycastableGraphicsForCanvas = global::UnityEngine.UI.GraphicRegistry.GetRaycastableGraphicsForCanvas(canvas);
			if (raycastableGraphicsForCanvas == null || raycastableGraphicsForCanvas.Count == 0)
			{
				return;
			}
			global::UnityEngine.Camera camera = eventCamera;
			int num = ((canvas.renderMode != global::UnityEngine.RenderMode.ScreenSpaceOverlay && !(camera == null)) ? camera.targetDisplay : canvas.targetDisplay);
			global::UnityEngine.Vector3 relativeMousePositionForRaycast = global::UnityEngine.UI.MultipleDisplayUtilities.GetRelativeMousePositionForRaycast(eventData);
			if ((int)relativeMousePositionForRaycast.z != num)
			{
				return;
			}
			global::UnityEngine.Vector2 vector;
			if (camera == null)
			{
				float num2 = global::UnityEngine.Screen.width;
				float num3 = global::UnityEngine.Screen.height;
				if (global::UnityEngineInternal.DisplayInternal.IsASecondaryDisplayIndex(num))
				{
					num2 = global::UnityEngine.Display.displays[num].systemWidth;
					num3 = global::UnityEngine.Display.displays[num].systemHeight;
				}
				vector = new global::UnityEngine.Vector2(relativeMousePositionForRaycast.x / num2, relativeMousePositionForRaycast.y / num3);
			}
			else
			{
				vector = camera.ScreenToViewportPoint(relativeMousePositionForRaycast);
			}
			if (vector.x < 0f || vector.x > 1f || vector.y < 0f || vector.y > 1f)
			{
				return;
			}
			float num4 = float.MaxValue;
			global::UnityEngine.Ray r = default(global::UnityEngine.Ray);
			if (camera != null)
			{
				r = camera.ScreenPointToRay(relativeMousePositionForRaycast);
			}
			if (canvas.renderMode != global::UnityEngine.RenderMode.ScreenSpaceOverlay && blockingObjects != global::UnityEngine.UI.GraphicRaycaster.BlockingObjects.None)
			{
				float f = 100f;
				if (camera != null)
				{
					float z = r.direction.z;
					f = (global::UnityEngine.Mathf.Approximately(0f, z) ? float.PositiveInfinity : global::UnityEngine.Mathf.Abs((camera.farClipPlane - camera.nearClipPlane) / z));
				}
				if ((blockingObjects == global::UnityEngine.UI.GraphicRaycaster.BlockingObjects.ThreeD || blockingObjects == global::UnityEngine.UI.GraphicRaycaster.BlockingObjects.All) && global::UnityEngine.UI.ReflectionMethodsCache.Singleton.raycast3D != null && global::UnityEngine.UI.ReflectionMethodsCache.Singleton.raycast3D(r, out var hit, f, m_BlockingMask))
				{
					num4 = hit.distance;
				}
				if ((blockingObjects == global::UnityEngine.UI.GraphicRaycaster.BlockingObjects.TwoD || blockingObjects == global::UnityEngine.UI.GraphicRaycaster.BlockingObjects.All) && global::UnityEngine.UI.ReflectionMethodsCache.Singleton.raycast2D != null)
				{
					global::UnityEngine.RaycastHit2D[] array = global::UnityEngine.UI.ReflectionMethodsCache.Singleton.getRayIntersectionAll(r, f, m_BlockingMask);
					if (array.Length != 0)
					{
						num4 = array[0].distance;
					}
				}
			}
			m_RaycastResults.Clear();
			Raycast(canvas, camera, relativeMousePositionForRaycast, raycastableGraphicsForCanvas, m_RaycastResults);
			int count = m_RaycastResults.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.GameObject gameObject = m_RaycastResults[i].gameObject;
				bool flag = true;
				if (ignoreReversedGraphics)
				{
					if (camera == null)
					{
						global::UnityEngine.Vector3 rhs = gameObject.transform.rotation * global::UnityEngine.Vector3.forward;
						flag = global::UnityEngine.Vector3.Dot(global::UnityEngine.Vector3.forward, rhs) > 0f;
					}
					else
					{
						global::UnityEngine.Vector3 vector2 = camera.transform.rotation * global::UnityEngine.Vector3.forward * camera.nearClipPlane;
						flag = global::UnityEngine.Vector3.Dot(gameObject.transform.position - camera.transform.position - vector2, gameObject.transform.forward) >= 0f;
					}
				}
				if (!flag)
				{
					continue;
				}
				float num5 = 0f;
				global::UnityEngine.Transform transform = gameObject.transform;
				global::UnityEngine.Vector3 forward = transform.forward;
				if (camera == null || canvas.renderMode == global::UnityEngine.RenderMode.ScreenSpaceOverlay)
				{
					num5 = 0f;
				}
				else
				{
					num5 = global::UnityEngine.Vector3.Dot(forward, transform.position - r.origin) / global::UnityEngine.Vector3.Dot(forward, r.direction);
					if (num5 < 0f)
					{
						continue;
					}
				}
				if (!(num5 >= num4))
				{
					global::UnityEngine.EventSystems.RaycastResult item = new global::UnityEngine.EventSystems.RaycastResult
					{
						gameObject = gameObject,
						module = this,
						distance = num5,
						screenPosition = relativeMousePositionForRaycast,
						displayIndex = num,
						index = resultAppendList.Count,
						depth = m_RaycastResults[i].depth,
						sortingLayer = canvas.sortingLayerID,
						sortingOrder = canvas.sortingOrder,
						worldPosition = r.origin + r.direction * num5,
						worldNormal = -forward
					};
					resultAppendList.Add(item);
				}
			}
		}

		private static void Raycast(global::UnityEngine.Canvas canvas, global::UnityEngine.Camera eventCamera, global::UnityEngine.Vector2 pointerPosition, global::System.Collections.Generic.IList<global::UnityEngine.UI.Graphic> foundGraphics, global::System.Collections.Generic.List<global::UnityEngine.UI.Graphic> results)
		{
			int count = foundGraphics.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.UI.Graphic graphic = foundGraphics[i];
				if (graphic.raycastTarget && !graphic.canvasRenderer.cull && graphic.depth != -1 && global::UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, pointerPosition, eventCamera, graphic.raycastPadding) && (!(eventCamera != null) || !(eventCamera.WorldToScreenPoint(graphic.rectTransform.position).z > eventCamera.farClipPlane)) && graphic.Raycast(pointerPosition, eventCamera))
				{
					s_SortedGraphics.Add(graphic);
				}
			}
			s_SortedGraphics.Sort((global::UnityEngine.UI.Graphic g1, global::UnityEngine.UI.Graphic g2) => g2.depth.CompareTo(g1.depth));
			count = s_SortedGraphics.Count;
			for (int num = 0; num < count; num++)
			{
				results.Add(s_SortedGraphics[num]);
			}
			s_SortedGraphics.Clear();
		}
	}
}
