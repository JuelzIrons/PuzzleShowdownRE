public class CustomAnimationEvent : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Events.UnityEvent m_eventToFire;

	public void FireEvent()
	{
		m_eventToFire?.Invoke();
	}
}
