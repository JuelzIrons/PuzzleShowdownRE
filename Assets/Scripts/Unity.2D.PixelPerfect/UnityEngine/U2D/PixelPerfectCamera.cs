namespace UnityEngine.U2D
{
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.AddComponentMenu("")]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.pixel-perfect@latest/index.html?subfolder=/manual/index.html%23properties")]
	public class PixelPerfectCamera : global::UnityEngine.MonoBehaviour, global::UnityEngine.U2D.IPixelPerfectCamera
	{
		[global::UnityEngine.SerializeField]
		private int m_AssetsPPU = 100;

		[global::UnityEngine.SerializeField]
		private int m_RefResolutionX = 320;

		[global::UnityEngine.SerializeField]
		private int m_RefResolutionY = 180;

		[global::UnityEngine.SerializeField]
		private bool m_UpscaleRT;

		[global::UnityEngine.SerializeField]
		private bool m_PixelSnapping;

		[global::UnityEngine.SerializeField]
		private bool m_CropFrameX;

		[global::UnityEngine.SerializeField]
		private bool m_CropFrameY;

		[global::UnityEngine.SerializeField]
		private bool m_StretchFill;

		private global::UnityEngine.Camera m_Camera;

		private global::UnityEngine.U2D.PixelPerfectCameraInternal m_Internal;

		private bool m_CinemachineCompatibilityMode;

		public int assetsPPU
		{
			get
			{
				return m_AssetsPPU;
			}
			set
			{
				m_AssetsPPU = ((value <= 0) ? 1 : value);
			}
		}

		public int refResolutionX
		{
			get
			{
				return m_RefResolutionX;
			}
			set
			{
				m_RefResolutionX = ((value <= 0) ? 1 : value);
			}
		}

		public int refResolutionY
		{
			get
			{
				return m_RefResolutionY;
			}
			set
			{
				m_RefResolutionY = ((value <= 0) ? 1 : value);
			}
		}

		public bool upscaleRT
		{
			get
			{
				return m_UpscaleRT;
			}
			set
			{
				m_UpscaleRT = value;
			}
		}

		public bool pixelSnapping
		{
			get
			{
				return m_PixelSnapping;
			}
			set
			{
				m_PixelSnapping = value;
			}
		}

		public bool cropFrameX
		{
			get
			{
				return m_CropFrameX;
			}
			set
			{
				m_CropFrameX = value;
			}
		}

		public bool cropFrameY
		{
			get
			{
				return m_CropFrameY;
			}
			set
			{
				m_CropFrameY = value;
			}
		}

		public bool stretchFill
		{
			get
			{
				return m_StretchFill;
			}
			set
			{
				m_StretchFill = value;
			}
		}

		public int pixelRatio
		{
			get
			{
				if (m_CinemachineCompatibilityMode)
				{
					if (m_UpscaleRT)
					{
						return m_Internal.zoom * m_Internal.cinemachineVCamZoom;
					}
					return m_Internal.cinemachineVCamZoom;
				}
				return m_Internal.zoom;
			}
		}

		public global::UnityEngine.Vector3 RoundToPixel(global::UnityEngine.Vector3 position)
		{
			float unitsPerPixel = m_Internal.unitsPerPixel;
			if (unitsPerPixel == 0f)
			{
				return position;
			}
			global::UnityEngine.Vector3 result = default(global::UnityEngine.Vector3);
			result.x = global::UnityEngine.Mathf.Round(position.x / unitsPerPixel) * unitsPerPixel;
			result.y = global::UnityEngine.Mathf.Round(position.y / unitsPerPixel) * unitsPerPixel;
			result.z = global::UnityEngine.Mathf.Round(position.z / unitsPerPixel) * unitsPerPixel;
			return result;
		}

		public float CorrectCinemachineOrthoSize(float targetOrthoSize)
		{
			m_CinemachineCompatibilityMode = true;
			if (m_Internal == null)
			{
				return targetOrthoSize;
			}
			return m_Internal.CorrectCinemachineOrthoSize(targetOrthoSize);
		}

		private void PixelSnap()
		{
			global::UnityEngine.Vector3 position = m_Camera.transform.position;
			global::UnityEngine.Vector3 vector = RoundToPixel(position) - position;
			vector.z = 0f - vector.z;
			global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.TRS(-vector, global::UnityEngine.Quaternion.identity, new global::UnityEngine.Vector3(1f, 1f, -1f));
			m_Camera.worldToCameraMatrix = matrix4x * m_Camera.transform.worldToLocalMatrix;
		}

		private void Awake()
		{
			m_Camera = GetComponent<global::UnityEngine.Camera>();
			m_Internal = new global::UnityEngine.U2D.PixelPerfectCameraInternal(this);
			m_Internal.originalOrthoSize = m_Camera.orthographicSize;
			m_Internal.hasPostProcessLayer = GetComponent("PostProcessLayer") != null;
			if (m_Camera.targetTexture != null)
			{
				global::UnityEngine.Debug.LogWarning("Render to texture is not supported by Pixel Perfect Camera.", m_Camera);
			}
		}

		private void LateUpdate()
		{
			m_Internal.CalculateCameraProperties(global::UnityEngine.Screen.width, global::UnityEngine.Screen.height);
			m_Camera.forceIntoRenderTexture = m_Internal.hasPostProcessLayer || m_Internal.useOffscreenRT;
		}

		private void OnPreCull()
		{
			PixelSnap();
			if (m_Internal.pixelRect != global::UnityEngine.Rect.zero)
			{
				m_Camera.pixelRect = m_Internal.pixelRect;
			}
			else
			{
				m_Camera.rect = new global::UnityEngine.Rect(0f, 0f, 1f, 1f);
			}
			if (!m_CinemachineCompatibilityMode)
			{
				m_Camera.orthographicSize = m_Internal.orthoSize;
			}
		}

		private void OnPreRender()
		{
			if (m_Internal.cropFrameXOrY)
			{
				global::UnityEngine.GL.Clear(clearDepth: false, clearColor: true, global::UnityEngine.Color.black);
			}
			global::UnityEngine.U2D.PixelPerfectRendering.pixelSnapSpacing = m_Internal.unitsPerPixel;
		}

		private void OnPostRender()
		{
			global::UnityEngine.U2D.PixelPerfectRendering.pixelSnapSpacing = 0f;
			if (m_Internal.useOffscreenRT)
			{
				global::UnityEngine.RenderTexture activeTexture = m_Camera.activeTexture;
				if (activeTexture != null)
				{
					activeTexture.filterMode = (m_Internal.useStretchFill ? global::UnityEngine.FilterMode.Bilinear : global::UnityEngine.FilterMode.Point);
				}
				m_Camera.pixelRect = m_Internal.CalculatePostRenderPixelRect(m_Camera.aspect, global::UnityEngine.Screen.width, global::UnityEngine.Screen.height);
			}
		}

		private void OnEnable()
		{
			m_CinemachineCompatibilityMode = false;
		}

		internal void OnDisable()
		{
			m_Camera.rect = new global::UnityEngine.Rect(0f, 0f, 1f, 1f);
			m_Camera.orthographicSize = m_Internal.originalOrthoSize;
			m_Camera.forceIntoRenderTexture = m_Internal.hasPostProcessLayer;
			m_Camera.ResetAspect();
			m_Camera.ResetWorldToCameraMatrix();
		}
	}
}
