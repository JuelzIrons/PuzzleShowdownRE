namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.AddComponentMenu("Rendering/2D/Pixel Perfect Camera")]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", null, null)]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/2d-pixelperfect.html%23properties")]
	public class PixelPerfectCamera : global::UnityEngine.MonoBehaviour, global::UnityEngine.Rendering.Universal.IPixelPerfectCamera, global::UnityEngine.ISerializationCallbackReceiver
	{
		public enum CropFrame
		{
			None = 0,
			Pillarbox = 1,
			Letterbox = 2,
			Windowbox = 3,
			StretchFill = 4
		}

		public enum GridSnapping
		{
			None = 0,
			PixelSnapping = 1,
			UpscaleRenderTexture = 2
		}

		public enum PixelPerfectFilterMode
		{
			RetroAA = 0,
			Point = 1
		}

		private enum ComponentVersions
		{
			Version_Unserialized = 0,
			Version_1 = 1
		}

		[global::UnityEngine.SerializeField]
		private int m_AssetsPPU = 100;

		[global::UnityEngine.SerializeField]
		private int m_RefResolutionX = 320;

		[global::UnityEngine.SerializeField]
		private int m_RefResolutionY = 180;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame m_CropFrame;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping m_GridSnapping;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.PixelPerfectCamera.PixelPerfectFilterMode m_FilterMode;

		private global::UnityEngine.Camera m_Camera;

		private global::UnityEngine.Rendering.Universal.PixelPerfectCameraInternal m_Internal;

		private bool m_CinemachineCompatibilityMode;

		public global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame cropFrame
		{
			get
			{
				return m_CropFrame;
			}
			set
			{
				m_CropFrame = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping gridSnapping
		{
			get
			{
				return m_GridSnapping;
			}
			set
			{
				m_GridSnapping = value;
			}
		}

		public float orthographicSize => m_Internal.orthoSize;

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

		[global::System.Obsolete("Use gridSnapping instead #from(2021.2)")]
		public bool upscaleRT
		{
			get
			{
				return m_GridSnapping == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping.UpscaleRenderTexture;
			}
			set
			{
				m_GridSnapping = (value ? global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping.UpscaleRenderTexture : global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping.None);
			}
		}

		[global::System.Obsolete("Use gridSnapping instead #from(2021.2)")]
		public bool pixelSnapping
		{
			get
			{
				return m_GridSnapping == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping.PixelSnapping;
			}
			set
			{
				m_GridSnapping = (value ? global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping.PixelSnapping : global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping.None);
			}
		}

		[global::System.Obsolete("Use cropFrame instead #from(2021.2)")]
		public bool cropFrameX
		{
			get
			{
				if (m_CropFrame != global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.StretchFill && m_CropFrame != global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Windowbox)
				{
					return m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Pillarbox;
				}
				return true;
			}
			set
			{
				if (value)
				{
					if (m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.None)
					{
						m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Pillarbox;
					}
					else if (m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Letterbox)
					{
						m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Windowbox;
					}
				}
				else if (m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Pillarbox)
				{
					m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.None;
				}
				else if (m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Windowbox || m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.StretchFill)
				{
					m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Letterbox;
				}
			}
		}

		[global::System.Obsolete("Use cropFrame instead #from(2021.2)")]
		public bool cropFrameY
		{
			get
			{
				if (m_CropFrame != global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.StretchFill && m_CropFrame != global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Windowbox)
				{
					return m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Letterbox;
				}
				return true;
			}
			set
			{
				if (value)
				{
					if (m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.None)
					{
						m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Letterbox;
					}
					else if (m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Pillarbox)
					{
						m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Windowbox;
					}
				}
				else if (m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Letterbox)
				{
					m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.None;
				}
				else if (m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Windowbox || m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.StretchFill)
				{
					m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Pillarbox;
				}
			}
		}

		[global::System.Obsolete("Use cropFrame instead. #from(2021.2)")]
		public bool stretchFill
		{
			get
			{
				return m_CropFrame == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.StretchFill;
			}
			set
			{
				if (value)
				{
					m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.StretchFill;
				}
				else
				{
					m_CropFrame = global::UnityEngine.Rendering.Universal.PixelPerfectCamera.CropFrame.Windowbox;
				}
			}
		}

		public int pixelRatio
		{
			get
			{
				if (m_CinemachineCompatibilityMode)
				{
					if (m_GridSnapping == global::UnityEngine.Rendering.Universal.PixelPerfectCamera.GridSnapping.UpscaleRenderTexture)
					{
						return m_Internal.zoom * m_Internal.cinemachineVCamZoom;
					}
					return m_Internal.cinemachineVCamZoom;
				}
				return m_Internal.zoom;
			}
		}

		public bool requiresUpscalePass => m_Internal.requiresUpscaling;

		internal global::UnityEngine.FilterMode finalBlitFilterMode
		{
			get
			{
				if (m_FilterMode != global::UnityEngine.Rendering.Universal.PixelPerfectCamera.PixelPerfectFilterMode.RetroAA)
				{
					return global::UnityEngine.FilterMode.Point;
				}
				return global::UnityEngine.FilterMode.Bilinear;
			}
		}

		internal global::UnityEngine.Vector2Int offscreenRTSize => new global::UnityEngine.Vector2Int(m_Internal.offscreenRTWidth, m_Internal.offscreenRTHeight);

		private global::UnityEngine.Vector2Int cameraRTSize
		{
			get
			{
				global::UnityEngine.RenderTexture targetTexture = m_Camera.targetTexture;
				if (!(targetTexture == null))
				{
					return new global::UnityEngine.Vector2Int(targetTexture.width, targetTexture.height);
				}
				return new global::UnityEngine.Vector2Int(global::UnityEngine.Screen.width, global::UnityEngine.Screen.height);
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
			global::UnityEngine.Matrix4x4 inverse = global::UnityEngine.Matrix4x4.TRS(position + vector, global::UnityEngine.Quaternion.identity, global::UnityEngine.Vector3.one).inverse;
			global::UnityEngine.Matrix4x4 inverse2 = global::UnityEngine.Matrix4x4.Rotate(m_Camera.transform.rotation).inverse;
			global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.Scale(new global::UnityEngine.Vector3(1f, 1f, -1f));
			m_Camera.worldToCameraMatrix = matrix4x * inverse2 * inverse;
		}

		private void Awake()
		{
			m_Camera = GetComponent<global::UnityEngine.Camera>();
			m_Internal = new global::UnityEngine.Rendering.Universal.PixelPerfectCameraInternal(this);
			UpdateCameraProperties();
		}

		private void UpdateCameraProperties()
		{
			global::UnityEngine.Vector2Int vector2Int = cameraRTSize;
			m_Internal.CalculateCameraProperties(vector2Int.x, vector2Int.y);
			if (m_Internal.useOffscreenRT)
			{
				m_Camera.pixelRect = m_Internal.CalculateFinalBlitPixelRect(vector2Int.x, vector2Int.y);
			}
			else
			{
				m_Camera.rect = new global::UnityEngine.Rect(0f, 0f, 1f, 1f);
			}
		}

		private void OnBeginCameraRendering(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera camera)
		{
			if (camera == m_Camera)
			{
				UpdateCameraProperties();
				PixelSnap();
				if (!m_CinemachineCompatibilityMode)
				{
					m_Camera.orthographicSize = m_Internal.orthoSize;
				}
				global::UnityEngine.U2D.PixelPerfectRendering.pixelSnapSpacing = m_Internal.unitsPerPixel;
			}
		}

		private void OnEndCameraRendering(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera camera)
		{
			if (camera == m_Camera)
			{
				global::UnityEngine.U2D.PixelPerfectRendering.pixelSnapSpacing = 0f;
			}
		}

		private void OnEnable()
		{
			m_CinemachineCompatibilityMode = false;
			global::UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
			global::UnityEngine.Rendering.RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
		}

		internal void OnDisable()
		{
			global::UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
			global::UnityEngine.Rendering.RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
			m_Camera.rect = new global::UnityEngine.Rect(0f, 0f, 1f, 1f);
			m_Camera.ResetWorldToCameraMatrix();
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
		}
	}
}
