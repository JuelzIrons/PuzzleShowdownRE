namespace UnityEngine.Rendering
{
	public class CameraSwitcher : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.Camera[] m_Cameras;

		private int m_CurrentCameraIndex = -1;

		private global::UnityEngine.Camera m_OriginalCamera;

		private global::UnityEngine.Vector3 m_OriginalCameraPosition;

		private global::UnityEngine.Quaternion m_OriginalCameraRotation;

		private global::UnityEngine.Camera m_CurrentCamera;

		private global::UnityEngine.GUIContent[] m_CameraNames;

		private int[] m_CameraIndices;

		private global::UnityEngine.Rendering.DebugUI.EnumField m_DebugEntry;

		private int m_DebugEntryEnumIndex;

		private void OnEnable()
		{
			m_OriginalCamera = GetComponent<global::UnityEngine.Camera>();
			m_CurrentCamera = m_OriginalCamera;
			if (m_OriginalCamera == null)
			{
				global::UnityEngine.Debug.LogError("Camera Switcher needs a Camera component attached");
				return;
			}
			m_CurrentCameraIndex = GetCameraCount() - 1;
			m_CameraNames = new global::UnityEngine.GUIContent[GetCameraCount()];
			m_CameraIndices = new int[GetCameraCount()];
			for (int i = 0; i < m_Cameras.Length; i++)
			{
				global::UnityEngine.Camera camera = m_Cameras[i];
				if (camera != null)
				{
					m_CameraNames[i] = new global::UnityEngine.GUIContent(camera.name);
				}
				else
				{
					m_CameraNames[i] = new global::UnityEngine.GUIContent("null");
				}
				m_CameraIndices[i] = i;
			}
			m_CameraNames[GetCameraCount() - 1] = new global::UnityEngine.GUIContent("Original Camera");
			m_CameraIndices[GetCameraCount() - 1] = GetCameraCount() - 1;
			m_DebugEntry = new global::UnityEngine.Rendering.DebugUI.EnumField
			{
				displayName = "Camera Switcher",
				getter = () => m_CurrentCameraIndex,
				setter = delegate(int value)
				{
					SetCameraIndex(value);
				},
				enumNames = m_CameraNames,
				enumValues = m_CameraIndices,
				getIndex = () => m_DebugEntryEnumIndex,
				setIndex = delegate(int value)
				{
					m_DebugEntryEnumIndex = value;
				}
			};
			global::UnityEngine.Rendering.DebugManager.instance.GetPanel("Camera", createIfNull: true).children.Add(m_DebugEntry);
		}

		private void OnDisable()
		{
			if (m_DebugEntry != null && m_DebugEntry.panel != null)
			{
				m_DebugEntry.panel.children.Remove(m_DebugEntry);
			}
		}

		private int GetCameraCount()
		{
			return m_Cameras.Length + 1;
		}

		private global::UnityEngine.Camera GetNextCamera()
		{
			if (m_CurrentCameraIndex == m_Cameras.Length)
			{
				return m_OriginalCamera;
			}
			return m_Cameras[m_CurrentCameraIndex];
		}

		private void SetCameraIndex(int index)
		{
			if (index <= 0 || index >= GetCameraCount())
			{
				return;
			}
			m_CurrentCameraIndex = index;
			if (m_CurrentCamera == m_OriginalCamera)
			{
				m_OriginalCameraPosition = m_OriginalCamera.transform.position;
				m_OriginalCameraRotation = m_OriginalCamera.transform.rotation;
			}
			m_CurrentCamera = GetNextCamera();
			if (m_CurrentCamera != null)
			{
				if (m_CurrentCamera == m_OriginalCamera)
				{
					m_OriginalCamera.transform.SetPositionAndRotation(m_OriginalCameraPosition, m_OriginalCameraRotation);
				}
				base.transform.SetPositionAndRotation(m_CurrentCamera.transform.position, m_CurrentCamera.transform.rotation);
			}
		}
	}
}
