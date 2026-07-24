namespace UnityEngine.InputSystem.OnScreen
{
	[global::UnityEngine.AddComponentMenu("Input/On-Screen Button")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/OnScreen.html#on-screen-buttons")]
	public class OnScreenButton : global::UnityEngine.InputSystem.OnScreen.OnScreenControl, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerUpHandler
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Button")]
		[global::UnityEngine.SerializeField]
		private string m_ControlPath;

		protected override string controlPathInternal
		{
			get
			{
				return m_ControlPath;
			}
			set
			{
				m_ControlPath = value;
			}
		}

		public void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			SendValueToControl(0f);
		}

		public void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			SendValueToControl(1f);
		}
	}
}
