namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class XRSRPSettings
	{
		public static bool enabled => global::UnityEngine.XR.XRSettings.enabled;

		public static bool isDeviceActive
		{
			get
			{
				if (enabled)
				{
					return global::UnityEngine.XR.XRSettings.isDeviceActive;
				}
				return false;
			}
		}

		public static string loadedDeviceName
		{
			get
			{
				if (enabled)
				{
					return global::UnityEngine.XR.XRSettings.loadedDeviceName;
				}
				return "No XR device loaded";
			}
		}

		public static string[] supportedDevices
		{
			get
			{
				if (enabled)
				{
					return global::UnityEngine.XR.XRSettings.supportedDevices;
				}
				return new string[1];
			}
		}

		public static global::UnityEngine.RenderTextureDescriptor eyeTextureDesc
		{
			get
			{
				if (enabled)
				{
					return global::UnityEngine.XR.XRSettings.eyeTextureDesc;
				}
				return new global::UnityEngine.RenderTextureDescriptor(0, 0);
			}
		}

		public static int eyeTextureWidth
		{
			get
			{
				if (enabled)
				{
					return global::UnityEngine.XR.XRSettings.eyeTextureWidth;
				}
				return 0;
			}
		}

		public static int eyeTextureHeight
		{
			get
			{
				if (enabled)
				{
					return global::UnityEngine.XR.XRSettings.eyeTextureHeight;
				}
				return 0;
			}
		}

		public static float occlusionMeshScale
		{
			get
			{
				if (enabled)
				{
					return global::UnityEngine.Experimental.Rendering.XRSystem.GetOcclusionMeshScale();
				}
				return 0f;
			}
			set
			{
				if (enabled)
				{
					global::UnityEngine.Experimental.Rendering.XRSystem.SetOcclusionMeshScale(value);
				}
			}
		}

		public static bool useVisibilityMesh
		{
			get
			{
				if (enabled)
				{
					return global::UnityEngine.Experimental.Rendering.XRSystem.GetUseVisibilityMesh();
				}
				return false;
			}
			set
			{
				if (enabled)
				{
					global::UnityEngine.Experimental.Rendering.XRSystem.SetUseVisibilityMesh(value);
				}
			}
		}

		public static int mirrorViewMode
		{
			get
			{
				if (enabled)
				{
					return global::UnityEngine.Experimental.Rendering.XRSystem.GetMirrorViewMode();
				}
				return 0;
			}
			set
			{
				if (enabled)
				{
					global::UnityEngine.Experimental.Rendering.XRSystem.SetMirrorViewMode(value);
				}
			}
		}
	}
}
