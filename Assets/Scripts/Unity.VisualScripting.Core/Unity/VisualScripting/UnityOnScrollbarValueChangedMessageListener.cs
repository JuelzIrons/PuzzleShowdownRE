namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnScrollbarValueChangedMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void Start()
		{
			GetComponent<global::UnityEngine.UI.Scrollbar>()?.onValueChanged?.AddListener(delegate(float value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnScrollbarValueChanged", base.gameObject, value);
			});
		}
	}
}
