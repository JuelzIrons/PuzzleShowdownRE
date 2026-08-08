public class ExtraElementsManager : global::UnityEngine.MonoBehaviour
{
	public static ExtraElementsManager Instance;

	public global::TMPro.TextMeshPro TIMER_TEXT;

	public global::TMPro.TextMeshPro STAGE_TEXT;

	public global::UnityEngine.SpriteRenderer DIFFICULTY_ICON;

	public global::UnityEngine.AudioSource TimeBuzzer;

	public global::TMPro.TextMeshPro P1_SCORE;

	public global::TMPro.TextMeshPro P2_SCORE;

	[global::UnityEngine.SerializeField]
	private GameLoop m_p1GL;

	[global::UnityEngine.SerializeField]
	private GameLoop m_p2GL;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (GameManager.Instance != null && GameManager.Instance.DefinedGameMode == GameModeType.Campaign && m_p1GL != null)
		{
			float timePassed = m_p1GL.TimePassed;
			int num = global::UnityEngine.Mathf.FloorToInt(timePassed / 60f);
			int num2 = global::UnityEngine.Mathf.FloorToInt(timePassed - (float)(num * 60));
			string text = $"{num:00}:{num2:00}";
			if (Instance != null)
			{
				Instance.TIMER_TEXT.text = text;
			}
		}
		if (GameManager.Instance != null && GameManager.Instance.DefinedGameMode == GameModeType.LocalMp)
		{
			P1_SCORE.text = m_p1GL.Score.ToString("0000000");
			P2_SCORE.text = m_p2GL.Score.ToString("0000000");
		}
		if (GameManager.Instance != null && GameManager.Instance.DefinedGameMode == GameModeType.Online)
		{
			P1_SCORE.text = m_p1GL.Score.ToString("0000000");
			P2_SCORE.text = m_p2GL.Score.ToString("0000000");
		}
	}
}
