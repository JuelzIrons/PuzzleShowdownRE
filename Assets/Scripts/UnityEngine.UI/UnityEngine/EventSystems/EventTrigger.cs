namespace UnityEngine.EventSystems
{
	[global::UnityEngine.AddComponentMenu("Event/Event Trigger")]
	public class EventTrigger : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.IPointerEnterHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerExitHandler, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IPointerUpHandler, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IInitializePotentialDragHandler, global::UnityEngine.EventSystems.IBeginDragHandler, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IEndDragHandler, global::UnityEngine.EventSystems.IDropHandler, global::UnityEngine.EventSystems.IScrollHandler, global::UnityEngine.EventSystems.IUpdateSelectedHandler, global::UnityEngine.EventSystems.ISelectHandler, global::UnityEngine.EventSystems.IDeselectHandler, global::UnityEngine.EventSystems.IMoveHandler, global::UnityEngine.EventSystems.ISubmitHandler, global::UnityEngine.EventSystems.ICancelHandler
	{
		[global::System.Serializable]
		public class TriggerEvent : global::UnityEngine.Events.UnityEvent<global::UnityEngine.EventSystems.BaseEventData>
		{
		}

		[global::System.Serializable]
		public class Entry
		{
			public global::UnityEngine.EventSystems.EventTriggerType eventID = global::UnityEngine.EventSystems.EventTriggerType.PointerClick;

			public global::UnityEngine.EventSystems.EventTrigger.TriggerEvent callback = new global::UnityEngine.EventSystems.EventTrigger.TriggerEvent();
		}

		[global::UnityEngine.Serialization.FormerlySerializedAs("delegates")]
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.EventSystems.EventTrigger.Entry> m_Delegates;

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("Please use triggers instead (UnityUpgradable) -> triggers", true)]
		public global::System.Collections.Generic.List<global::UnityEngine.EventSystems.EventTrigger.Entry> delegates
		{
			get
			{
				return triggers;
			}
			set
			{
				triggers = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.EventSystems.EventTrigger.Entry> triggers
		{
			get
			{
				if (m_Delegates == null)
				{
					m_Delegates = new global::System.Collections.Generic.List<global::UnityEngine.EventSystems.EventTrigger.Entry>();
				}
				return m_Delegates;
			}
			set
			{
				m_Delegates = value;
			}
		}

		protected EventTrigger()
		{
		}

		private void Execute(global::UnityEngine.EventSystems.EventTriggerType id, global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			for (int i = 0; i < triggers.Count; i++)
			{
				global::UnityEngine.EventSystems.EventTrigger.Entry entry = triggers[i];
				if (entry.eventID == id && entry.callback != null)
				{
					entry.callback.Invoke(eventData);
				}
			}
		}

		public virtual void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.PointerEnter, eventData);
		}

		public virtual void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.PointerExit, eventData);
		}

		public virtual void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.Drag, eventData);
		}

		public virtual void OnDrop(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.Drop, eventData);
		}

		public virtual void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.PointerDown, eventData);
		}

		public virtual void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.PointerUp, eventData);
		}

		public virtual void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.PointerClick, eventData);
		}

		public virtual void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.Select, eventData);
		}

		public virtual void OnDeselect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.Deselect, eventData);
		}

		public virtual void OnScroll(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.Scroll, eventData);
		}

		public virtual void OnMove(global::UnityEngine.EventSystems.AxisEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.Move, eventData);
		}

		public virtual void OnUpdateSelected(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.UpdateSelected, eventData);
		}

		public virtual void OnInitializePotentialDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.InitializePotentialDrag, eventData);
		}

		public virtual void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.BeginDrag, eventData);
		}

		public virtual void OnEndDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.EndDrag, eventData);
		}

		public virtual void OnSubmit(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.Submit, eventData);
		}

		public virtual void OnCancel(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			Execute(global::UnityEngine.EventSystems.EventTriggerType.Cancel, eventData);
		}
	}
}
