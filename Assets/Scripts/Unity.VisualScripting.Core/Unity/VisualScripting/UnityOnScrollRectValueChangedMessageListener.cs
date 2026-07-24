namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnScrollRectValueChangedMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void Start()
		{
			GetComponent<global::UnityEngine.UI.ScrollRect>()?.onValueChanged?.AddListener(delegate(global::UnityEngine.Vector2 value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnScrollRectValueChanged", base.gameObject, value);
			});
		}
	}
}
