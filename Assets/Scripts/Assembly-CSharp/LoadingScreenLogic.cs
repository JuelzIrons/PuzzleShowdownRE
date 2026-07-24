public class LoadingScreenLogic : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.UI.RawImage TransitionImage;

	public float AlphaBlendValue;

	public float ProgressValue;

	public static global::UnityEngine.Texture2D lockedImage;

	public int CurrentSceneIndex;

	public int NewSceneIndex;

	public static void CaptureAndLock()
	{
		if (!(CameraResolution.CAM == null))
		{
			global::UnityEngine.Camera cAM = CameraResolution.CAM;
			global::UnityEngine.Rect rect = cAM.rect;
			cAM.rect = new global::UnityEngine.Rect(0f, 0f, 1f, 1f);
			int num = global::UnityEngine.Mathf.RoundToInt(rect.width * (float)global::UnityEngine.Screen.width);
			int num2 = global::UnityEngine.Mathf.RoundToInt(rect.height * (float)global::UnityEngine.Screen.height);
			global::UnityEngine.RenderTexture renderTexture = (cAM.targetTexture = global::UnityEngine.RenderTexture.GetTemporary(num, num2, 24));
			cAM.Render();
			lockedImage = new global::UnityEngine.Texture2D(num, num2, global::UnityEngine.TextureFormat.RGBA32, mipChain: false);
			global::UnityEngine.RenderTexture.active = renderTexture;
			lockedImage.ReadPixels(new global::UnityEngine.Rect(0f, 0f, num, num2), 0, 0);
			lockedImage.Apply();
			cAM.targetTexture = null;
			global::UnityEngine.RenderTexture.active = null;
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			cAM.rect = rect;
		}
	}

	private void Start()
	{
		TransitionImage.texture = lockedImage;
	}

	public void ExitTransition()
	{
		base.transform.GetComponent<global::UnityEngine.Animation>().clip = base.transform.GetComponent<global::UnityEngine.Animation>().GetClip("loadingOut");
		base.transform.GetComponent<global::UnityEngine.Animation>().Play();
	}

	private void Update()
	{
		TransitionImage.material.SetFloat("_Progress", ProgressValue);
		TransitionImage.material.SetFloat("_Alpha", AlphaBlendValue);
	}

	public void AnimationKill()
	{
		global::UnityEngine.Object.Destroy(base.transform.parent.gameObject);
	}
}
