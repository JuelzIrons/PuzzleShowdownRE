namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnInputFieldEndEditMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void Start()
		{
			GetComponent<global::UnityEngine.UI.InputField>()?.onEndEdit?.AddListener(delegate(string value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnInputFieldEndEdit", base.gameObject, value);
			});
		}
	}
}
