public class AutoSelect : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.IPointerEnterHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerExitHandler, global::UnityEngine.EventSystems.ISelectHandler, global::UnityEngine.EventSystems.IDeselectHandler
{
	public bool AutoExecute;

	private global::UnityEngine.UI.Selectable selectable;

	private global::UnityEngine.UI.Button button;

	public bool m_isSelected;

	public global::UnityEngine.Events.UnityEvent SelectionEvent = new global::UnityEngine.Events.UnityEvent();

	public bool DontUnSelectONUnHover;

	private void Awake()
	{
		selectable = base.transform.GetComponent<global::UnityEngine.UI.Selectable>();
		base.transform.TryGetComponent<global::UnityEngine.UI.Button>(out button);
	}

	public void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		if (!m_isSelected)
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
			selectable.Select();
			if (AutoExecute && button != null)
			{
				button.onClick.Invoke();
			}
		}
	}

	public void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		if (m_isSelected && !DontUnSelectONUnHover && !(global::UnityEngine.EventSystems.EventSystem.current == null))
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
			m_isSelected = false;
		}
	}

	public void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
	{
		m_isSelected = true;
		if (SelectionEvent != null)
		{
			SelectionEvent.Invoke();
		}
		if (AutoExecute && button != null)
		{
			button.onClick.Invoke();
		}
	}

	public void OnDeselect(global::UnityEngine.EventSystems.BaseEventData eventData)
	{
		m_isSelected = false;
	}
}
