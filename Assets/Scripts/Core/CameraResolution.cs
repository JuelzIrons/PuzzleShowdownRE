public class CameraResolution : global::UnityEngine.MonoBehaviour
{
	public static global::UnityEngine.Camera CAM;

	private int ScreenSizeX;

	private int ScreenSizeY;

	private void RescaleCamera()
	{
		if (global::UnityEngine.Screen.width != ScreenSizeX || global::UnityEngine.Screen.height != ScreenSizeY)
		{
			float num = 1.7777778f;
			float num2 = (float)global::UnityEngine.Screen.width / (float)global::UnityEngine.Screen.height / num;
			if (num2 < 1f)
			{
				global::UnityEngine.Rect rect = GetComponent<global::UnityEngine.Camera>().rect;
				rect.width = 1f;
				rect.height = num2;
				rect.x = 0f;
				rect.y = (1f - num2) / 2f;
				GetComponent<global::UnityEngine.Camera>().rect = rect;
			}
			else
			{
				float num3 = 1f / num2;
				global::UnityEngine.Rect rect2 = GetComponent<global::UnityEngine.Camera>().rect;
				rect2.width = num3;
				rect2.height = 1f;
				rect2.x = (1f - num3) / 2f;
				rect2.y = 0f;
				GetComponent<global::UnityEngine.Camera>().rect = rect2;
			}
			ScreenSizeX = global::UnityEngine.Screen.width;
			ScreenSizeY = global::UnityEngine.Screen.height;
		}
	}

	private void OnPreCull()
	{
		if (!global::UnityEngine.Application.isEditor)
		{
			global::UnityEngine.Rect rect = CAM.rect;
			CAM.rect = new global::UnityEngine.Rect(0f, 0f, 1f, 1f);
			global::UnityEngine.GL.Clear(clearDepth: true, clearColor: true, global::UnityEngine.Color.black);
			CAM.rect = rect;
		}
	}

	private void Awake()
	{
		if (CAM != null)
		{
			global::UnityEngine.Camera component = GetComponent<global::UnityEngine.Camera>();
			CAM.orthographic = component.orthographic;
			CAM.orthographicSize = component.orthographicSize;
			CAM.fieldOfView = component.fieldOfView;
			CAM.nearClipPlane = component.nearClipPlane;
			CAM.farClipPlane = component.farClipPlane;
			CAM.backgroundColor = global::UnityEngine.Color.black;
			CAM.GetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>().renderPostProcessing = true;
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			CAM = GetComponent<global::UnityEngine.Camera>();
			global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	private void OnEnable()
	{
		global::UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
		AssignToAllCanvases();
	}

	private void OnDisable()
	{
		global::UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(global::UnityEngine.SceneManagement.Scene scene, global::UnityEngine.SceneManagement.LoadSceneMode mode)
	{
		AssignToAllCanvases();
	}

	public void AssignToAllCanvases()
	{
		global::UnityEngine.Canvas[] array = global::UnityEngine.Object.FindObjectsByType<global::UnityEngine.Canvas>(global::UnityEngine.FindObjectsInactive.Include, global::UnityEngine.FindObjectsSortMode.None);
		foreach (global::UnityEngine.Canvas canvas in array)
		{
			if (canvas.renderMode != global::UnityEngine.RenderMode.WorldSpace)
			{
				canvas.renderMode = global::UnityEngine.RenderMode.ScreenSpaceCamera;
				canvas.worldCamera = CAM;
			}
		}
	}

	private void Start()
	{
		RescaleCamera();
	}

	private void Update()
	{
		RescaleCamera();
	}
}
