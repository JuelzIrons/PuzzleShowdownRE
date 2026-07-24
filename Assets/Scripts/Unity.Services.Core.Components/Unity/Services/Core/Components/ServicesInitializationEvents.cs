namespace Unity.Services.Core.Components
{
	[global::System.Serializable]
	public class ServicesInitializationEvents
	{
		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Events.UnityEvent Initialized = new global::UnityEngine.Events.UnityEvent();

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Events.UnityEvent<global::System.Exception> InitializeFailed = new global::UnityEngine.Events.UnityEvent<global::System.Exception>();
	}
}
