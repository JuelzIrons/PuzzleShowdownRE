public class TallyManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_tallyText;

	public static TallyManager Instance;

	public int P1_SCORE;

	public int P2_SCORE;

	private void Awake()
	{
		if (Instance != null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		Instance = this;
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		RefreshTally();
	}

	public void ResetTally()
	{
		P1_SCORE = 0;
		P2_SCORE = 0;
		RefreshTally();
	}

	public void IncreaseP1Score()
	{
		P1_SCORE++;
		RefreshTally();
	}

	public void IncreaseP2Score()
	{
		P2_SCORE++;
		RefreshTally();
	}

	public void RefreshTally()
	{
		m_tallyText.enabled = P1_SCORE != 0 || P2_SCORE != 0;
		m_tallyText.text = $"{P1_SCORE}-{P2_SCORE}";
	}
}
