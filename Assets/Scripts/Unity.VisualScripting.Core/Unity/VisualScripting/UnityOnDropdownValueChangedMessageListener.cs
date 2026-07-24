namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnDropdownValueChangedMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void Start()
		{
			GetComponent<global::UnityEngine.UI.Dropdown>()?.onValueChanged?.AddListener(delegate(int value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnDropdownValueChanged", base.gameObject, value);
			});
		}
	}
}
