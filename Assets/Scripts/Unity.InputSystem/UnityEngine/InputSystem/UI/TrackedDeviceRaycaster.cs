namespace UnityEngine.InputSystem.UI
{
	[global::UnityEngine.AddComponentMenu("Event/Tracked Device Raycaster")]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Canvas))]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/TrackedInputDevices.html#tracked-device-raycaster")]
	public class TrackedDeviceRaycaster : global::UnityEngine.EventSystems.BaseRaycaster
	{
		private struct RaycastHitData
		{
			public global::UnityEngine.UI.Graphic graphic { get; }

			public global::UnityEngine.Vector3 worldHitPosition { get; }

			public global::UnityEngine.Vector2 screenPosition { get; }

			public float distance { get; }

			public RaycastHitData(global::UnityEngine.UI.Graphic graphic, global::UnityEngine.Vector3 worldHitPosition, global::UnityEngine.Vector2 screenPosition, float distance)
			{
				this.graphic = graphic;
				this.worldHitPosition = worldHitPosition;
				this.screenPosition = screenPosition;
				this.distance = distance;
			}
		}

		[global::System.NonSerialized]
		private global::System.Collections.Generic.List<global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData> m_RaycastResultsCache = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData>();

		internal static global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster> s_Instances;

		private static readonly global::System.Collections.Generic.List<global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData> s_SortedGraphics = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData>();

		[global::UnityEngine.Serialization.FormerlySerializedAs("ignoreReversedGraphics")]
		[global::UnityEngine.SerializeField]
		private bool m_IgnoreReversedGraphics;

		[global::UnityEngine.Serialization.FormerlySerializedAs("checkFor2DOcclusion")]
		[global::UnityEngine.SerializeField]
		private bool m_CheckFor2DOcclusion;

		[global::UnityEngine.Serialization.FormerlySerializedAs("checkFor3DOcclusion")]
		[global::UnityEngine.SerializeField]
		private bool m_CheckFor3DOcclusion;

		[global::UnityEngine.Tooltip("Maximum distance (in 3D world space) that rays are traced to find a hit.")]
		[global::UnityEngine.SerializeField]
		private float m_MaxDistance = 1000f;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.LayerMask m_BlockingMask;

		[global::System.NonSerialized]
		private global::UnityEngine.Canvas m_Canvas;

		public override global::UnityEngine.Camera eventCamera
		{
			get
			{
				global::UnityEngine.Canvas canvas = this.canvas;
				if (!(canvas != null))
				{
					return null;
				}
				return canvas.worldCamera;
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

		public bool checkFor3DOcclusion
		{
			get
			{
				return m_CheckFor3DOcclusion;
			}
			set
			{
				m_CheckFor3DOcclusion = value;
			}
		}

		public bool checkFor2DOcclusion
		{
			get
			{
				return m_CheckFor2DOcclusion;
			}
			set
			{
				m_CheckFor2DOcclusion = value;
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

		public float maxDistance
		{
			get
			{
				return m_MaxDistance;
			}
			set
			{
				m_MaxDistance = value;
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

		protected override void OnEnable()
		{
			base.OnEnable();
			s_Instances.AppendWithCapacity(this);
		}

		protected override void OnDisable()
		{
			int num = global::UnityEngine.InputSystem.Utilities.InputArrayExtensions.IndexOfReference(s_Instances, this);
			if (num != -1)
			{
				s_Instances.RemoveAtByMovingTailWithCapacity(num);
			}
			base.OnDisable();
		}

		public override void Raycast(global::UnityEngine.EventSystems.PointerEventData eventData, global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> resultAppendList)
		{
			if (eventData is global::UnityEngine.InputSystem.UI.ExtendedPointerEventData { pointerType: global::UnityEngine.InputSystem.UI.UIPointerType.Tracked } extendedPointerEventData)
			{
				PerformRaycast(extendedPointerEventData, resultAppendList);
			}
		}

		internal void PerformRaycast(global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData, global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> resultAppendList)
		{
			if (canvas == null || eventCamera == null)
			{
				return;
			}
			global::UnityEngine.Ray ray = new global::UnityEngine.Ray(eventData.trackedDevicePosition, eventData.trackedDeviceOrientation * global::UnityEngine.Vector3.forward);
			float distance = m_MaxDistance;
			if (m_CheckFor3DOcclusion && global::UnityEngine.Physics.Raycast(ray, out var hitInfo, distance, m_BlockingMask))
			{
				distance = hitInfo.distance;
			}
			if (m_CheckFor2DOcclusion)
			{
				float distance2 = distance;
				global::UnityEngine.RaycastHit2D rayIntersection = global::UnityEngine.Physics2D.GetRayIntersection(ray, distance2, m_BlockingMask);
				if (rayIntersection.collider != null)
				{
					distance = rayIntersection.distance;
				}
			}
			m_RaycastResultsCache.Clear();
			SortedRaycastGraphics(canvas, ray, m_RaycastResultsCache);
			for (int i = 0; i < m_RaycastResultsCache.Count; i++)
			{
				bool flag = true;
				global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData raycastHitData = m_RaycastResultsCache[i];
				global::UnityEngine.GameObject gameObject = raycastHitData.graphic.gameObject;
				if (m_IgnoreReversedGraphics)
				{
					global::UnityEngine.Vector3 direction = ray.direction;
					global::UnityEngine.Vector3 rhs = gameObject.transform.rotation * global::UnityEngine.Vector3.forward;
					flag = global::UnityEngine.Vector3.Dot(direction, rhs) > 0f;
				}
				if (flag & (raycastHitData.distance < distance))
				{
					global::UnityEngine.EventSystems.RaycastResult item = new global::UnityEngine.EventSystems.RaycastResult
					{
						gameObject = gameObject,
						module = this,
						distance = raycastHitData.distance,
						index = resultAppendList.Count,
						depth = raycastHitData.graphic.depth,
						worldPosition = raycastHitData.worldHitPosition,
						screenPosition = raycastHitData.screenPosition
					};
					resultAppendList.Add(item);
				}
			}
		}

		private void SortedRaycastGraphics(global::UnityEngine.Canvas canvas, global::UnityEngine.Ray ray, global::System.Collections.Generic.List<global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData> results)
		{
			global::System.Collections.Generic.IList<global::UnityEngine.UI.Graphic> graphicsForCanvas = global::UnityEngine.UI.GraphicRegistry.GetGraphicsForCanvas(canvas);
			s_SortedGraphics.Clear();
			for (int i = 0; i < graphicsForCanvas.Count; i++)
			{
				global::UnityEngine.UI.Graphic graphic = graphicsForCanvas[i];
				if (graphic.depth != -1 && RayIntersectsRectTransform(graphic.rectTransform, ray, out var worldPosition, out var distance))
				{
					global::UnityEngine.Vector2 vector = eventCamera.WorldToScreenPoint(worldPosition);
					if (graphic.Raycast(vector, eventCamera))
					{
						s_SortedGraphics.Add(new global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData(graphic, worldPosition, vector, distance));
					}
				}
			}
			s_SortedGraphics.Sort((global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData g1, global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.RaycastHitData g2) => g2.graphic.depth.CompareTo(g1.graphic.depth));
			results.AddRange(s_SortedGraphics);
		}

		private static bool RayIntersectsRectTransform(global::UnityEngine.RectTransform transform, global::UnityEngine.Ray ray, out global::UnityEngine.Vector3 worldPosition, out float distance)
		{
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[4];
			transform.GetWorldCorners(array);
			if (new global::UnityEngine.Plane(array[0], array[1], array[2]).Raycast(ray, out var enter))
			{
				global::UnityEngine.Vector3 point = ray.GetPoint(enter);
				global::UnityEngine.Vector3 rhs = array[3] - array[0];
				global::UnityEngine.Vector3 rhs2 = array[1] - array[0];
				float num = global::UnityEngine.Vector3.Dot(point - array[0], rhs);
				if (global::UnityEngine.Vector3.Dot(point - array[0], rhs2) >= 0f && num >= 0f)
				{
					global::UnityEngine.Vector3 rhs3 = array[1] - array[2];
					global::UnityEngine.Vector3 rhs4 = array[3] - array[2];
					float num2 = global::UnityEngine.Vector3.Dot(point - array[2], rhs3);
					float num3 = global::UnityEngine.Vector3.Dot(point - array[2], rhs4);
					if (num2 >= 0f && num3 >= 0f)
					{
						worldPosition = point;
						distance = enter;
						return true;
					}
				}
			}
			worldPosition = global::UnityEngine.Vector3.zero;
			distance = 0f;
			return false;
		}
	}
}
