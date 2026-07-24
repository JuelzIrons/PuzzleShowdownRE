namespace UnityEngine.EventSystems
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.EventSystems.EventSystem))]
	public abstract class BaseInputModule : global::UnityEngine.EventSystems.UIBehaviour
	{
		[global::System.NonSerialized]
		protected global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> m_RaycastResultCache = new global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult>();

		[global::UnityEngine.SerializeField]
		private bool m_SendPointerHoverToParent = true;

		private global::UnityEngine.EventSystems.AxisEventData m_AxisEventData;

		private global::UnityEngine.EventSystems.EventSystem m_EventSystem;

		private global::UnityEngine.EventSystems.BaseEventData m_BaseEventData;

		protected global::UnityEngine.EventSystems.BaseInput m_InputOverride;

		private global::UnityEngine.EventSystems.BaseInput m_DefaultInput;

		protected internal bool sendPointerHoverToParent
		{
			get
			{
				return m_SendPointerHoverToParent;
			}
			set
			{
				m_SendPointerHoverToParent = value;
			}
		}

		public global::UnityEngine.EventSystems.BaseInput input
		{
			get
			{
				if (m_InputOverride != null)
				{
					return m_InputOverride;
				}
				if (m_DefaultInput == null)
				{
					global::UnityEngine.EventSystems.BaseInput[] components = GetComponents<global::UnityEngine.EventSystems.BaseInput>();
					foreach (global::UnityEngine.EventSystems.BaseInput baseInput in components)
					{
						if (baseInput != null && baseInput.GetType() == typeof(global::UnityEngine.EventSystems.BaseInput))
						{
							m_DefaultInput = baseInput;
							break;
						}
					}
					if (m_DefaultInput == null)
					{
						m_DefaultInput = base.gameObject.AddComponent<global::UnityEngine.EventSystems.BaseInput>();
					}
				}
				return m_DefaultInput;
			}
		}

		public global::UnityEngine.EventSystems.BaseInput inputOverride
		{
			get
			{
				return m_InputOverride;
			}
			set
			{
				m_InputOverride = value;
			}
		}

		protected global::UnityEngine.EventSystems.EventSystem eventSystem => m_EventSystem;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_EventSystem = GetComponent<global::UnityEngine.EventSystems.EventSystem>();
			m_EventSystem.UpdateModules();
		}

		protected override void OnDisable()
		{
			m_EventSystem.UpdateModules();
			base.OnDisable();
		}

		public abstract void Process();

		protected static global::UnityEngine.EventSystems.RaycastResult FindFirstRaycast(global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> candidates)
		{
			int count = candidates.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(candidates[i].gameObject == null))
				{
					return candidates[i];
				}
			}
			return default(global::UnityEngine.EventSystems.RaycastResult);
		}

		protected static global::UnityEngine.EventSystems.MoveDirection DetermineMoveDirection(float x, float y)
		{
			return DetermineMoveDirection(x, y, 0.6f);
		}

		protected static global::UnityEngine.EventSystems.MoveDirection DetermineMoveDirection(float x, float y, float deadZone)
		{
			if (new global::UnityEngine.Vector2(x, y).sqrMagnitude < deadZone * deadZone)
			{
				return global::UnityEngine.EventSystems.MoveDirection.None;
			}
			if (global::UnityEngine.Mathf.Abs(x) > global::UnityEngine.Mathf.Abs(y))
			{
				if (!(x > 0f))
				{
					return global::UnityEngine.EventSystems.MoveDirection.Left;
				}
				return global::UnityEngine.EventSystems.MoveDirection.Right;
			}
			if (!(y > 0f))
			{
				return global::UnityEngine.EventSystems.MoveDirection.Down;
			}
			return global::UnityEngine.EventSystems.MoveDirection.Up;
		}

		protected static global::UnityEngine.GameObject FindCommonRoot(global::UnityEngine.GameObject g1, global::UnityEngine.GameObject g2)
		{
			if (g1 == null || g2 == null)
			{
				return null;
			}
			global::UnityEngine.Transform parent = g1.transform;
			while (parent != null)
			{
				global::UnityEngine.Transform parent2 = g2.transform;
				while (parent2 != null)
				{
					if (parent == parent2)
					{
						return parent.gameObject;
					}
					parent2 = parent2.parent;
				}
				parent = parent.parent;
			}
			return null;
		}

		protected void HandlePointerExitAndEnter(global::UnityEngine.EventSystems.PointerEventData currentPointerData, global::UnityEngine.GameObject newEnterTarget)
		{
			if (newEnterTarget == null || currentPointerData.pointerEnter == null)
			{
				int count = currentPointerData.hovered.Count;
				for (int i = 0; i < count; i++)
				{
					currentPointerData.fullyExited = true;
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(currentPointerData.hovered[i], currentPointerData, global::UnityEngine.EventSystems.ExecuteEvents.pointerMoveHandler);
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(currentPointerData.hovered[i], currentPointerData, global::UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
				}
				currentPointerData.hovered.Clear();
				if (newEnterTarget == null)
				{
					currentPointerData.pointerEnter = null;
					return;
				}
			}
			if (currentPointerData.pointerEnter == newEnterTarget && (bool)newEnterTarget)
			{
				if (currentPointerData.IsPointerMoving())
				{
					int count2 = currentPointerData.hovered.Count;
					for (int j = 0; j < count2; j++)
					{
						global::UnityEngine.EventSystems.ExecuteEvents.Execute(currentPointerData.hovered[j], currentPointerData, global::UnityEngine.EventSystems.ExecuteEvents.pointerMoveHandler);
					}
				}
				return;
			}
			global::UnityEngine.GameObject gameObject = FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
			global::UnityEngine.GameObject gameObject2 = ((global::UnityEngine.Component)newEnterTarget.GetComponentInParent<global::UnityEngine.EventSystems.IPointerExitHandler>())?.gameObject;
			if (currentPointerData.pointerEnter != null)
			{
				global::UnityEngine.Transform parent = currentPointerData.pointerEnter.transform;
				while (parent != null && (!m_SendPointerHoverToParent || !(gameObject != null) || !(gameObject.transform == parent)) && (m_SendPointerHoverToParent || !(gameObject2 == parent.gameObject)))
				{
					currentPointerData.fullyExited = parent.gameObject != gameObject && currentPointerData.pointerEnter != newEnterTarget;
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(parent.gameObject, currentPointerData, global::UnityEngine.EventSystems.ExecuteEvents.pointerMoveHandler);
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(parent.gameObject, currentPointerData, global::UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
					currentPointerData.hovered.Remove(parent.gameObject);
					if (m_SendPointerHoverToParent)
					{
						parent = parent.parent;
					}
					if (gameObject != null && gameObject.transform == parent)
					{
						break;
					}
					if (!m_SendPointerHoverToParent)
					{
						parent = parent.parent;
					}
				}
			}
			global::UnityEngine.GameObject pointerEnter = currentPointerData.pointerEnter;
			currentPointerData.pointerEnter = newEnterTarget;
			if (!(newEnterTarget != null))
			{
				return;
			}
			global::UnityEngine.Transform parent2 = newEnterTarget.transform;
			while (parent2 != null)
			{
				currentPointerData.reentered = parent2.gameObject == gameObject && parent2.gameObject != pointerEnter;
				if (!m_SendPointerHoverToParent || !currentPointerData.reentered)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(parent2.gameObject, currentPointerData, global::UnityEngine.EventSystems.ExecuteEvents.pointerEnterHandler);
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(parent2.gameObject, currentPointerData, global::UnityEngine.EventSystems.ExecuteEvents.pointerMoveHandler);
					currentPointerData.hovered.Add(parent2.gameObject);
					if (m_SendPointerHoverToParent || parent2.gameObject.GetComponent<global::UnityEngine.EventSystems.IPointerEnterHandler>() == null)
					{
						if (m_SendPointerHoverToParent)
						{
							parent2 = parent2.parent;
						}
						if (!(gameObject != null) || !(gameObject.transform == parent2))
						{
							if (!m_SendPointerHoverToParent)
							{
								parent2 = parent2.parent;
							}
							continue;
						}
						break;
					}
					break;
				}
				break;
			}
		}

		protected virtual global::UnityEngine.EventSystems.AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
		{
			if (m_AxisEventData == null)
			{
				m_AxisEventData = new global::UnityEngine.EventSystems.AxisEventData(eventSystem);
			}
			m_AxisEventData.Reset();
			m_AxisEventData.moveVector = new global::UnityEngine.Vector2(x, y);
			m_AxisEventData.moveDir = DetermineMoveDirection(x, y, moveDeadZone);
			return m_AxisEventData;
		}

		protected virtual global::UnityEngine.EventSystems.BaseEventData GetBaseEventData()
		{
			if (m_BaseEventData == null)
			{
				m_BaseEventData = new global::UnityEngine.EventSystems.BaseEventData(eventSystem);
			}
			m_BaseEventData.Reset();
			return m_BaseEventData;
		}

		public virtual bool IsPointerOverGameObject(int pointerId)
		{
			return false;
		}

		public virtual bool ShouldActivateModule()
		{
			if (base.enabled)
			{
				return base.gameObject.activeInHierarchy;
			}
			return false;
		}

		public virtual void DeactivateModule()
		{
		}

		public virtual void ActivateModule()
		{
		}

		public virtual void UpdateModule()
		{
		}

		public virtual bool IsModuleSupported()
		{
			return true;
		}

		public virtual int ConvertUIToolkitPointerId(global::UnityEngine.EventSystems.PointerEventData sourcePointerData)
		{
			if (sourcePointerData.pointerId >= 0)
			{
				return global::UnityEngine.UIElements.PointerId.touchPointerIdBase + sourcePointerData.pointerId;
			}
			return global::UnityEngine.UIElements.PointerId.mousePointerId;
		}

		public virtual global::UnityEngine.Vector2 ConvertPointerEventScrollDeltaToTicks(global::UnityEngine.Vector2 scrollDelta)
		{
			return scrollDelta / input.mouseScrollDeltaPerTick;
		}

		public virtual global::UnityEngine.EventSystems.NavigationDeviceType GetNavigationEventDeviceType(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			return global::UnityEngine.EventSystems.NavigationDeviceType.Unknown;
		}
	}
}
