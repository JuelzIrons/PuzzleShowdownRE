namespace UnityEngine.Rendering
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public struct RTHandleStaticHelpers
	{
		public static global::UnityEngine.Rendering.RTHandle s_RTHandleWrapper;

		public static void SetRTHandleStaticWrapper(global::UnityEngine.Rendering.RenderTargetIdentifier rtId)
		{
			if (s_RTHandleWrapper == null)
			{
				s_RTHandleWrapper = global::UnityEngine.Rendering.RTHandles.Alloc(rtId);
			}
			else
			{
				s_RTHandleWrapper.SetTexture(rtId);
			}
		}

		public static void SetRTHandleUserManagedWrapper(ref global::UnityEngine.Rendering.RTHandle rtWrapper, global::UnityEngine.Rendering.RenderTargetIdentifier rtId)
		{
			if (rtWrapper != null)
			{
				rtWrapper.SetTexture(rtId);
			}
		}
	}
}
