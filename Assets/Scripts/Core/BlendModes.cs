public class BlendModes : global::UnityEngine.MonoBehaviour
{
	public enum BlendMode
	{
		NoBlend = 0,
		Add = 1,
		Subtract = 2,
		Multiply = 3,
		Screen = 4,
		Overlay = 5,
		SoftLight = 6,
		ColorDodge = 7,
		ColorBurn = 8,
		VividLight = 9
	}

	public enum BlendType
	{
		BlendOnItself = 0,
		PickedTexture = 1,
		PickedColor = 2
	}

	public global::UnityEngine.Shader blendModesShader;

	public BlendModes.BlendMode blendMode;

	public global::UnityEngine.Texture blendTexture;

	public global::UnityEngine.Color blendColor;

	public BlendModes.BlendType blendType;

	[global::UnityEngine.Range(0f, 1f)]
	public float strength;

	private global::UnityEngine.Material blendModesMat;

	private void OnEnable()
	{
		blendModesMat = new global::UnityEngine.Material(blendModesShader);
		blendModesMat.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
	}

	private void OnDisable()
	{
		blendModesMat = null;
	}

	private void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		blendModesMat.SetInt("_BlendType", (int)blendType);
		blendModesMat.SetVector("_BlendColor", blendColor);
		blendModesMat.SetTexture("_BlendTex", blendTexture);
		blendModesMat.SetFloat("_Strength", strength);
		global::UnityEngine.Graphics.Blit(source, destination, blendModesMat, (int)blendMode);
	}
}
