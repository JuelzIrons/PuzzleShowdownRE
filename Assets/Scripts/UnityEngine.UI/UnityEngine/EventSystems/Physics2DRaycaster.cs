namespace UnityEngine.EventSystems
{
	[global::UnityEngine.AddComponentMenu("Event/Physics 2D Raycaster")]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
	public class Physics2DRaycaster : global::UnityEngine.EventSystems.PhysicsRaycaster
	{
		private global::UnityEngine.RaycastHit2D[] m_Hits;

		protected Physics2DRaycaster()
		{
		}

		public override void Raycast(global::UnityEngine.EventSystems.PointerEventData eventData, global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> resultAppendList)
		{
			global::UnityEngine.Ray ray = default(global::UnityEngine.Ray);
			float distanceToClipPlane = 0f;
			int eventDisplayIndex = 0;
			if (!ComputeRayAndDistance(eventData, ref ray, ref eventDisplayIndex, ref distanceToClipPlane))
			{
				return;
			}
			int num = 0;
			if (base.maxRayIntersections == 0)
			{
				if (global::UnityEngine.UI.ReflectionMethodsCache.Singleton.getRayIntersectionAll == null)
				{
					return;
				}
				m_Hits = global::UnityEngine.UI.ReflectionMethodsCache.Singleton.getRayIntersectionAll(ray, distanceToClipPlane, base.finalEventMask);
				num = m_Hits.Length;
			}
			else
			{
				if (global::UnityEngine.UI.ReflectionMethodsCache.Singleton.getRayIntersectionAllNonAlloc == null)
				{
					return;
				}
				if (m_LastMaxRayIntersections != m_MaxRayIntersections)
				{
					m_Hits = new global::UnityEngine.RaycastHit2D[base.maxRayIntersections];
					m_LastMaxRayIntersections = m_MaxRayIntersections;
				}
				num = global::UnityEngine.UI.ReflectionMethodsCache.Singleton.getRayIntersectionAllNonAlloc(ray, m_Hits, distanceToClipPlane, base.finalEventMask);
			}
			if (num == 0)
			{
				return;
			}
			int i = 0;
			for (int num2 = num; i < num2; i++)
			{
				global::UnityEngine.Renderer renderer = null;
				global::UnityEngine.Renderer component = m_Hits[i].collider.gameObject.GetComponent<global::UnityEngine.Renderer>();
				if (component != null)
				{
					if (component is global::UnityEngine.SpriteRenderer)
					{
						renderer = component;
					}
					if (component is global::UnityEngine.Tilemaps.TilemapRenderer)
					{
						renderer = component;
					}
					if (component is global::UnityEngine.U2D.SpriteShapeRenderer)
					{
						renderer = component;
					}
				}
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
					sortingGroupID = ((renderer != null) ? renderer.sortingGroupID : global::UnityEngine.Rendering.SortingGroup.invalidSortingGroupID),
					sortingGroupOrder = ((renderer != null) ? renderer.sortingGroupOrder : 0),
					sortingLayer = ((renderer != null) ? renderer.sortingLayerID : 0),
					sortingOrder = ((renderer != null) ? renderer.sortingOrder : 0)
				};
				if (item.sortingGroupID != global::UnityEngine.Rendering.SortingGroup.invalidSortingGroupID)
				{
					global::UnityEngine.Rendering.SortingGroup sortingGroupByIndex = global::UnityEngine.Rendering.SortingGroup.GetSortingGroupByIndex(renderer.sortingGroupID);
					if ((object)sortingGroupByIndex != null)
					{
						item.distance = global::UnityEngine.Vector3.Dot(ray.direction, sortingGroupByIndex.transform.position - ray.origin);
						item.sortingLayer = sortingGroupByIndex.sortingLayerID;
						item.sortingOrder = sortingGroupByIndex.sortingOrder;
					}
				}
				resultAppendList.Add(item);
			}
		}
	}
}
