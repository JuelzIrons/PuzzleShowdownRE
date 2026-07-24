namespace UnityEngine.EventSystems
{
	public static class ExecuteEvents
	{
		public delegate void EventFunction<T1>(T1 handler, global::UnityEngine.EventSystems.BaseEventData eventData);

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerMoveHandler> s_PointerMoveHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerEnterHandler> s_PointerEnterHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerExitHandler> s_PointerExitHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerDownHandler> s_PointerDownHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerUpHandler> s_PointerUpHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerClickHandler> s_PointerClickHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IInitializePotentialDragHandler> s_InitializePotentialDragHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IBeginDragHandler> s_BeginDragHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IDragHandler> s_DragHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IEndDragHandler> s_EndDragHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IDropHandler> s_DropHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IScrollHandler> s_ScrollHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IUpdateSelectedHandler> s_UpdateSelectedHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.ISelectHandler> s_SelectHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IDeselectHandler> s_DeselectHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IMoveHandler> s_MoveHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.ISubmitHandler> s_SubmitHandler = Execute;

		private static readonly global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.ICancelHandler> s_CancelHandler = Execute;

		private static readonly global::System.Collections.Generic.List<global::UnityEngine.Transform> s_InternalTransformList = new global::System.Collections.Generic.List<global::UnityEngine.Transform>(30);

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerMoveHandler> pointerMoveHandler => s_PointerMoveHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerEnterHandler> pointerEnterHandler => s_PointerEnterHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerExitHandler> pointerExitHandler => s_PointerExitHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerDownHandler> pointerDownHandler => s_PointerDownHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerUpHandler> pointerUpHandler => s_PointerUpHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IPointerClickHandler> pointerClickHandler => s_PointerClickHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IInitializePotentialDragHandler> initializePotentialDrag => s_InitializePotentialDragHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IBeginDragHandler> beginDragHandler => s_BeginDragHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IDragHandler> dragHandler => s_DragHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IEndDragHandler> endDragHandler => s_EndDragHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IDropHandler> dropHandler => s_DropHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IScrollHandler> scrollHandler => s_ScrollHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IUpdateSelectedHandler> updateSelectedHandler => s_UpdateSelectedHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.ISelectHandler> selectHandler => s_SelectHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IDeselectHandler> deselectHandler => s_DeselectHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.IMoveHandler> moveHandler => s_MoveHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.ISubmitHandler> submitHandler => s_SubmitHandler;

		public static global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<global::UnityEngine.EventSystems.ICancelHandler> cancelHandler => s_CancelHandler;

		public static T ValidateEventData<T>(global::UnityEngine.EventSystems.BaseEventData data) where T : class
		{
			if (data as T == null)
			{
				throw new global::System.ArgumentException($"Invalid type: {data.GetType()} passed to event expecting {typeof(T)}");
			}
			return data as T;
		}

		private static void Execute(global::UnityEngine.EventSystems.IPointerMoveHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnPointerMove(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IPointerEnterHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnPointerEnter(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IPointerExitHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnPointerExit(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IPointerDownHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnPointerDown(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IPointerUpHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnPointerUp(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IPointerClickHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnPointerClick(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IInitializePotentialDragHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnInitializePotentialDrag(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IBeginDragHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnBeginDrag(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IDragHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnDrag(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IEndDragHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnEndDrag(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IDropHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnDrop(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IScrollHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnScroll(ValidateEventData<global::UnityEngine.EventSystems.PointerEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.IUpdateSelectedHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnUpdateSelected(eventData);
		}

		private static void Execute(global::UnityEngine.EventSystems.ISelectHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnSelect(eventData);
		}

		private static void Execute(global::UnityEngine.EventSystems.IDeselectHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnDeselect(eventData);
		}

		private static void Execute(global::UnityEngine.EventSystems.IMoveHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnMove(ValidateEventData<global::UnityEngine.EventSystems.AxisEventData>(eventData));
		}

		private static void Execute(global::UnityEngine.EventSystems.ISubmitHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnSubmit(eventData);
		}

		private static void Execute(global::UnityEngine.EventSystems.ICancelHandler handler, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			handler.OnCancel(eventData);
		}

		private static void GetEventChain(global::UnityEngine.GameObject root, global::System.Collections.Generic.IList<global::UnityEngine.Transform> eventChain)
		{
			eventChain.Clear();
			if (!(root == null))
			{
				global::UnityEngine.Transform transform = root.transform;
				while (transform != null)
				{
					eventChain.Add(transform);
					transform = transform.parent;
				}
			}
		}

		public static bool Execute<T>(global::UnityEngine.GameObject target, global::UnityEngine.EventSystems.BaseEventData eventData, global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<T> functor) where T : global::UnityEngine.EventSystems.IEventSystemHandler
		{
			global::System.Collections.Generic.List<global::UnityEngine.EventSystems.IEventSystemHandler> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.EventSystems.IEventSystemHandler>, global::UnityEngine.EventSystems.IEventSystemHandler>.Get();
			GetEventList<T>(target, list);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				T handler;
				try
				{
					handler = (T)list[i];
				}
				catch (global::System.Exception innerException)
				{
					global::UnityEngine.EventSystems.IEventSystemHandler eventSystemHandler = list[i];
					global::UnityEngine.Debug.LogException(new global::System.Exception($"Type {typeof(T).Name} expected {eventSystemHandler.GetType().Name} received.", innerException));
					continue;
				}
				try
				{
					functor(handler, eventData);
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogException(exception);
				}
			}
			int count2 = list.Count;
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.EventSystems.IEventSystemHandler>, global::UnityEngine.EventSystems.IEventSystemHandler>.Release(list);
			return count2 > 0;
		}

		public static global::UnityEngine.GameObject ExecuteHierarchy<T>(global::UnityEngine.GameObject root, global::UnityEngine.EventSystems.BaseEventData eventData, global::UnityEngine.EventSystems.ExecuteEvents.EventFunction<T> callbackFunction) where T : global::UnityEngine.EventSystems.IEventSystemHandler
		{
			GetEventChain(root, s_InternalTransformList);
			int count = s_InternalTransformList.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.Transform transform = s_InternalTransformList[i];
				if (Execute(transform.gameObject, eventData, callbackFunction))
				{
					return transform.gameObject;
				}
			}
			return null;
		}

		private static bool ShouldSendToComponent<T>(global::UnityEngine.Component component) where T : global::UnityEngine.EventSystems.IEventSystemHandler
		{
			if (!(component is T))
			{
				return false;
			}
			global::UnityEngine.Behaviour behaviour = component as global::UnityEngine.Behaviour;
			if (behaviour != null)
			{
				return behaviour.isActiveAndEnabled;
			}
			return true;
		}

		private static void GetEventList<T>(global::UnityEngine.GameObject go, global::System.Collections.Generic.IList<global::UnityEngine.EventSystems.IEventSystemHandler> results) where T : global::UnityEngine.EventSystems.IEventSystemHandler
		{
			if (results == null)
			{
				throw new global::System.ArgumentException("Results array is null", "results");
			}
			if (go == null || !go.activeInHierarchy)
			{
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			go.GetComponents(list);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (ShouldSendToComponent<T>(list[i]))
				{
					results.Add(list[i] as global::UnityEngine.EventSystems.IEventSystemHandler);
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
		}

		public static bool CanHandleEvent<T>(global::UnityEngine.GameObject go) where T : global::UnityEngine.EventSystems.IEventSystemHandler
		{
			global::System.Collections.Generic.List<global::UnityEngine.EventSystems.IEventSystemHandler> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.EventSystems.IEventSystemHandler>, global::UnityEngine.EventSystems.IEventSystemHandler>.Get();
			GetEventList<T>(go, list);
			int count = list.Count;
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.EventSystems.IEventSystemHandler>, global::UnityEngine.EventSystems.IEventSystemHandler>.Release(list);
			return count != 0;
		}

		public static global::UnityEngine.GameObject GetEventHandler<T>(global::UnityEngine.GameObject root) where T : global::UnityEngine.EventSystems.IEventSystemHandler
		{
			if (root == null)
			{
				return null;
			}
			global::UnityEngine.Transform transform = root.transform;
			while (transform != null)
			{
				if (CanHandleEvent<T>(transform.gameObject))
				{
					return transform.gameObject;
				}
				transform = transform.parent;
			}
			return null;
		}
	}
}
