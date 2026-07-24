namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnSliderValueChangedMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void Start()
		{
			GetComponent<global::UnityEngine.UI.Slider>()?.onValueChanged?.AddListener(delegate(float value)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnSliderValueChanged", base.gameObject, value);
			});
		}
	}
}
