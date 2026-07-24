namespace TMPro
{
	public class TMP_ScrollbarEventHandler : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.ISelectHandler, global::UnityEngine.EventSystems.IDeselectHandler
	{
		public bool isSelected;

		public void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::UnityEngine.Debug.Log("Scrollbar click...");
		}

		public void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::UnityEngine.Debug.Log("Scrollbar selected");
			isSelected = true;
		}

		public void OnDeselect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::UnityEngine.Debug.Log("Scrollbar De-Selected");
			isSelected = false;
		}
	}
}
