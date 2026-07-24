namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnToggleValueChangedMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void Start()
		{
			GetComponent<global::UnityEngine.UI.Toggle>()?.onValueChanged?.AddListener(delegate(bool value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnToggleValueChanged", base.gameObject, value);
			});
		}
	}
}
