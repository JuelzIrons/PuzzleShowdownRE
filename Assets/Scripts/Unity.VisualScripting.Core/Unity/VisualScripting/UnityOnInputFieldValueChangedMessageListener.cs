namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnInputFieldValueChangedMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void Start()
		{
			GetComponent<global::UnityEngine.UI.InputField>()?.onValueChanged?.AddListener(delegate(string value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnInputFieldValueChanged", base.gameObject, value);
			});
		}
	}
}
