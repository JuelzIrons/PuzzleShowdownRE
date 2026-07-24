namespace UnityEngine.Rendering.Universal
{
	internal class XRPassUniversal : global::UnityEngine.Experimental.Rendering.XRPass
	{
		internal bool isLateLatchEnabled { get; set; }

		internal bool canMarkLateLatch { get; set; }

		internal bool hasMarkedLateLatch { get; set; }

		internal bool canFoveateIntermediatePasses { get; set; }

		public static global::UnityEngine.Experimental.Rendering.XRPass Create(global::UnityEngine.Experimental.Rendering.XRPassCreateInfo createInfo)
		{
			global::UnityEngine.Rendering.Universal.XRPassUniversal xRPassUniversal = global::UnityEngine.Rendering.GenericPool<global::UnityEngine.Rendering.Universal.XRPassUniversal>.Get();
			xRPassUniversal.InitBase(createInfo);
			xRPassUniversal.isLateLatchEnabled = false;
			xRPassUniversal.canMarkLateLatch = false;
			xRPassUniversal.hasMarkedLateLatch = false;
			xRPassUniversal.canFoveateIntermediatePasses = true;
			return xRPassUniversal;
		}

		public override void Release()
		{
			global::UnityEngine.Rendering.GenericPool<global::UnityEngine.Rendering.Universal.XRPassUniversal>.Release(this);
		}
	}
}
