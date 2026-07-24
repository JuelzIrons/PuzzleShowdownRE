namespace UnityEngine.UnityConsent
{
	[global::UnityEngine.Bindings.NativeHeader("Modules/UnityConsent/EndUserConsent.h")]
	public static class EndUserConsent
	{
		public static event global::System.Action<global::UnityEngine.UnityConsent.ConsentState> consentStateChanged;

		[global::UnityEngine.Bindings.NativeMethod("GetConsentStateStatic")]
		public static global::UnityEngine.UnityConsent.ConsentState GetConsentState()
		{
			GetConsentState_Injected(out var ret);
			return ret;
		}

		[global::UnityEngine.Bindings.NativeMethod("SetConsentStateStatic")]
		public static void SetConsentState(global::UnityEngine.UnityConsent.ConsentState consentState)
		{
			SetConsentState_Injected(ref consentState);
		}

		[global::UnityEngine.Scripting.RequiredByNativeCode]
		private static void OnConsentStateChanged()
		{
			if (global::UnityEngine.UnityConsent.EndUserConsent.consentStateChanged != null)
			{
				global::UnityEngine.UnityConsent.EndUserConsent.consentStateChanged(GetConsentState());
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		private static extern void GetConsentState_Injected(out global::UnityEngine.UnityConsent.ConsentState ret);

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		private static extern void SetConsentState_Injected([global::System.Runtime.InteropServices.In] ref global::UnityEngine.UnityConsent.ConsentState consentState);
	}
}
