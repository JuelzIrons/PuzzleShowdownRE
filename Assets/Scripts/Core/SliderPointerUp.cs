public class SliderPointerUp : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.IPointerUpHandler, global::UnityEngine.EventSystems.IEventSystemHandler
{
	public global::UnityEngine.Events.UnityEvent OnRelease;

	public void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		OnRelease?.Invoke();
	}
}
