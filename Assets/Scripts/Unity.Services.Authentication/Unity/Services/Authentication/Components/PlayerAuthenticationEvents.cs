namespace Unity.Services.Authentication.Components
{
	[global::System.Serializable]
	public class PlayerAuthenticationEvents
	{
		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Events.UnityEvent SignedIn = new global::UnityEngine.Events.UnityEvent();

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Events.UnityEvent<global::System.Exception> SignInFailed = new global::UnityEngine.Events.UnityEvent<global::System.Exception>();

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Events.UnityEvent SignedOut = new global::UnityEngine.Events.UnityEvent();

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Events.UnityEvent Expired = new global::UnityEngine.Events.UnityEvent();

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Events.UnityEvent<global::Unity.Services.Authentication.SignInCodeInfo> SignInCodeReceived = new global::UnityEngine.Events.UnityEvent<global::Unity.Services.Authentication.SignInCodeInfo>();

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Events.UnityEvent SignInCodeExpired = new global::UnityEngine.Events.UnityEvent();
	}
}
