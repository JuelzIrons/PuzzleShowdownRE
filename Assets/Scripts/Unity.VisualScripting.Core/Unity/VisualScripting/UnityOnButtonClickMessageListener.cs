namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnButtonClickMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void Start()
		{
			GetComponent<global::UnityEngine.UI.Button>()?.onClick?.AddListener(delegate
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnButtonClick", base.gameObject);
			});
		}
	}
}
