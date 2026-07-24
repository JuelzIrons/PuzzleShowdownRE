namespace UnityEngine.EventSystems
{
	[global::UnityEngine.AddComponentMenu("Event/Physics Raycaster")]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
	public class PhysicsRaycaster : global::UnityEngine.EventSystems.BaseRaycaster
	{
		private class RaycastHitComparer : global::System.Collections.Generic.IComparer<global::UnityEngine.RaycastHit>
		{
			public static global::UnityEngine.EventSystems.PhysicsRaycaster.RaycastHitComparer instance = new global::UnityEngine.EventSystems.PhysicsRaycaster.RaycastHitComparer();

			public int Compare(global::UnityEngine.RaycastHit x, global::UnityEngine.RaycastHit y)
			{
				return x.distance.CompareTo(y.distance);
			}
		}

		protected const int kNoEventMaskSet = -1;

		protected global::UnityEngine.Camera m_EventCamera;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.LayerMask m_EventMask = -1;

		[global::UnityEngine.SerializeField]
		protected int m_MaxRayIntersections;

		protected int m_LastMaxRayIntersections;

		private global::UnityEngine.RaycastHit[] m_Hits;

		public override global::UnityEngine.Camera eventCamera
		{
			get
			{
				if (m_EventCamera == null)
				{
					m_EventCamera = GetComponent<global::UnityEngine.Camera>();
				}
				if (m_EventCamera == null)
				{
					return global::UnityEngine.Camera.main;
				}
				return m_EventCamera;
			}
		}

		public virtual int depth
		{
			get
			{
				if (!(eventCamera != null))
				{
					return 16777215;
				}
				return (int)eventCamera.depth;
			}
		}

		public int finalEventMask
		{
			get
			{
				if (!(eventCamera != null))
				{
					return -1;
				}
				return eventCamera.cullingMask & (int)m_EventMask;
			}
		}

		public global::UnityEngine.LayerMask eventMask
		{
			get
			{
				return m_EventMask;
			}
			set
			{
				m_EventMask = value;
			}
		}

		public int maxRayIntersections
		{
			get
			{
				return m_MaxRayIntersections;
			}
			set
			{
				m_MaxRayIntersections = value;
			}
		}

		protected PhysicsRaycaster()
		{
		}

		protected bool ComputeRayAndDistance(global::UnityEngine.EventSystems.PointerEventData eventData, ref global::UnityEngine.Ray ray, ref int eventDisplayIndex, ref float distanceToClipPlane)
		{
			if (eventCamera == null)
			{
				return false;
			}
			global::UnityEngine.Vector3 vector = global::UnityEngine.UI.MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position, eventData.displayIndex);
			if (vector != global::UnityEngine.Vector3.zero)
			{
				eventDisplayIndex = (int)vector.z;
				if (eventDisplayIndex != eventCamera.targetDisplay)
				{
					return false;
				}
			}
			else
			{
				vector = eventData.position;
			}
			if (!eventCamera.pixelRect.Contains(vector))
			{
				return false;
			}
			ray = eventCamera.ScreenPointToRay(vector);
			float z = ray.direction.z;
			distanceToClipPlane = (global::UnityEngine.Mathf.Approximately(0f, z) ? float.PositiveInfinity : global::UnityEngine.Mathf.Abs((eventCamera.farClipPlane - eventCamera.nearClipPlane) / z));
			return true;
		}

		public override void Raycast(global::UnityEngine.EventSystems.PointerEventData eventData, global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> resultAppendList)
		{
			global::UnityEngine.Ray ray = default(global::UnityEngine.Ray);
			int eventDisplayIndex = 0;
			float distanceToClipPlane = 0f;
			if (!ComputeRayAndDistance(eventData, ref ray, ref eventDisplayIndex, ref distanceToClipPlane))
			{
				return;
			}
			int num = 0;
			if (m_MaxRayIntersections == 0)
			{
				if (global::UnityEngine.UI.ReflectionMethodsCache.Singleton.raycast3DAll == null)
				{
					return;
				}
				m_Hits = global::UnityEngine.UI.ReflectionMethodsCache.Singleton.raycast3DAll(ray, distanceToClipPlane, finalEventMask);
				num = m_Hits.Length;
			}
			else
			{
				if (global::UnityEngine.UI.ReflectionMethodsCache.Singleton.getRaycastNonAlloc == null)
				{
					return;
				}
				if (m_LastMaxRayIntersections != m_MaxRayIntersections)
				{
					m_Hits = new global::UnityEngine.RaycastHit[m_MaxRayIntersections];
					m_LastMaxRayIntersections = m_MaxRayIntersections;
				}
				num = global::UnityEngine.UI.ReflectionMethodsCache.Singleton.getRaycastNonAlloc(ray, m_Hits, distanceToClipPlane, finalEventMask);
			}
			if (num != 0)
			{
				if (num > 1)
				{
					global::System.Array.Sort(m_Hits, 0, num, global::UnityEngine.EventSystems.PhysicsRaycaster.RaycastHitComparer.instance);
				}
				int i = 0;
				for (int num2 = num; i < num2; i++)
				{
					global::UnityEngine.EventSystems.RaycastResult item = new global::UnityEngine.EventSystems.RaycastResult
					{
						gameObject = m_Hits[i].collider.gameObject,
						module = this,
						distance = m_Hits[i].distance,
						worldPosition = m_Hits[i].point,
						worldNormal = m_Hits[i].normal,
						screenPosition = eventData.position,
						displayIndex = eventDisplayIndex,
						index = resultAppendList.Count,
						sortingLayer = 0,
						sortingOrder = 0
					};
					resultAppendList.Add(item);
				}
			}
		}
	}
}
