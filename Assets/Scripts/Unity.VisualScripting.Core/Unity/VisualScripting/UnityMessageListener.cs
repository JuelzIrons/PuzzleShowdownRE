namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	[global::System.Obsolete("UnityMessageListener is deprecated and has been replaced by separate message listeners for each event, eg. UnityOnCollisionEnterMessageListener or UnityOnButtonClickMessageListener.")]
	public sealed class UnityMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IPointerEnterHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerExitHandler, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IPointerUpHandler, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IBeginDragHandler, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IEndDragHandler, global::UnityEngine.EventSystems.IDropHandler, global::UnityEngine.EventSystems.IScrollHandler, global::UnityEngine.EventSystems.ISelectHandler, global::UnityEngine.EventSystems.IDeselectHandler, global::UnityEngine.EventSystems.ISubmitHandler, global::UnityEngine.EventSystems.ICancelHandler, global::UnityEngine.EventSystems.IMoveHandler
	{
		private void Start()
		{
			AddGUIListeners();
		}

		public void AddGUIListeners()
		{
			GetComponent<global::UnityEngine.UI.Button>()?.onClick?.AddListener(delegate
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnButtonClick", base.gameObject);
			});
			GetComponent<global::UnityEngine.UI.Toggle>()?.onValueChanged?.AddListener(delegate(bool value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnToggleValueChanged", base.gameObject, value);
			});
			GetComponent<global::UnityEngine.UI.Slider>()?.onValueChanged?.AddListener(delegate(float value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnSliderValueChanged", base.gameObject, value);
			});
			GetComponent<global::UnityEngine.UI.Scrollbar>()?.onValueChanged?.AddListener(delegate(float value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnScrollbarValueChanged", base.gameObject, value);
			});
			GetComponent<global::UnityEngine.UI.Dropdown>()?.onValueChanged?.AddListener(delegate(int value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnDropdownValueChanged", base.gameObject, value);
			});
			GetComponent<global::UnityEngine.UI.InputField>()?.onValueChanged?.AddListener(delegate(string value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnInputFieldValueChanged", base.gameObject, value);
			});
			GetComponent<global::UnityEngine.UI.InputField>()?.onEndEdit?.AddListener(delegate(string value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnInputFieldEndEdit", base.gameObject, value);
			});
			GetComponent<global::UnityEngine.UI.ScrollRect>()?.onValueChanged?.AddListener(delegate(global::UnityEngine.Vector2 value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnScrollRectValueChanged", base.gameObject, value);
			});
		}

		public void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerEnter", base.gameObject, eventData);
		}

		public void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerExit", base.gameObject, eventData);
		}

		public void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerDown", base.gameObject, eventData);
		}

		public void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerUp", base.gameObject, eventData);
		}

		public void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerClick", base.gameObject, eventData);
		}

		public void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnBeginDrag", base.gameObject, eventData);
		}

		public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnDrag", base.gameObject, eventData);
		}

		public void OnEndDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnEndDrag", base.gameObject, eventData);
		}

		public void OnDrop(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnDrop", base.gameObject, eventData);
		}

		public void OnScroll(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnScroll", base.gameObject, eventData);
		}

		public void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnSelect", base.gameObject, eventData);
		}

		public void OnDeselect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnDeselect", base.gameObject, eventData);
		}

		public void OnSubmit(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnSubmit", base.gameObject, eventData);
		}

		public void OnCancel(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCancel", base.gameObject, eventData);
		}

		public void OnMove(global::UnityEngine.EventSystems.AxisEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMove", base.gameObject, eventData);
		}

		private void OnBecameInvisible()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnBecameInvisible", base.gameObject);
		}

		private void OnBecameVisible()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnBecameVisible", base.gameObject);
		}

		private void OnCollisionEnter(global::UnityEngine.Collision collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionEnter", base.gameObject, collision);
		}

		private void OnCollisionExit(global::UnityEngine.Collision collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionExit", base.gameObject, collision);
		}

		private void OnCollisionStay(global::UnityEngine.Collision collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionStay", base.gameObject, collision);
		}

		private void OnCollisionEnter2D(global::UnityEngine.Collision2D collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionEnter2D", base.gameObject, collision);
		}

		private void OnCollisionExit2D(global::UnityEngine.Collision2D collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionExit2D", base.gameObject, collision);
		}

		private void OnCollisionStay2D(global::UnityEngine.Collision2D collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionStay2D", base.gameObject, collision);
		}

		private void OnControllerColliderHit(global::UnityEngine.ControllerColliderHit hit)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnControllerColliderHit", base.gameObject, hit);
		}

		private void OnJointBreak(float breakForce)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnJointBreak", base.gameObject, breakForce);
		}

		private void OnJointBreak2D(global::UnityEngine.Joint2D brokenJoint)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnJointBreak2D", base.gameObject, brokenJoint);
		}

		private void OnMouseDown()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseDown", base.gameObject);
		}

		private void OnMouseDrag()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseDrag", base.gameObject);
		}

		private void OnMouseEnter()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseEnter", base.gameObject);
		}

		private void OnMouseExit()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseExit", base.gameObject);
		}

		private void OnMouseOver()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseOver", base.gameObject);
		}

		private void OnMouseUp()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseUp", base.gameObject);
		}

		private void OnMouseUpAsButton()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseUpAsButton", base.gameObject);
		}

		private void OnParticleCollision(global::UnityEngine.GameObject other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnParticleCollision", base.gameObject, other);
		}

		private void OnTransformChildrenChanged()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTransformChildrenChanged", base.gameObject);
		}

		private void OnTransformParentChanged()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTransformParentChanged", base.gameObject);
		}

		private void OnTriggerEnter(global::UnityEngine.Collider other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerEnter", base.gameObject, other);
		}

		private void OnTriggerExit(global::UnityEngine.Collider other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerExit", base.gameObject, other);
		}

		private void OnTriggerStay(global::UnityEngine.Collider other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerStay", base.gameObject, other);
		}

		private void OnTriggerEnter2D(global::UnityEngine.Collider2D other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerEnter2D", base.gameObject, other);
		}

		private void OnTriggerExit2D(global::UnityEngine.Collider2D other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerExit2D", base.gameObject, other);
		}

		private void OnTriggerStay2D(global::UnityEngine.Collider2D other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerStay2D", base.gameObject, other);
		}
	}
}
