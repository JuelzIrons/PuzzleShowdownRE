public class HUD : global::UnityEngine.MonoBehaviour
{
	public PodManager MyPodManager;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshPro m_timer;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshPro m_freezeTimer;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshPro m_score;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshPro m_hiscore;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshPro m_level;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_freeze;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_forceFreeze;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshPro m_stageText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_SingleplayerHolder;

	private int m_cachedHighscore;

	private void Update()
	{
		UpdateHUD();
	}

	private void OnEnable()
	{
		SaveData saveData = SaveSystem.Load();
		m_cachedHighscore = ((saveData.highscores.Length != 0) ? saveData.highscores[0].score : 0);
	}

	public void UpdateHUD()
	{
		if (MyPodManager == null || !MyPodManager.HasStarted)
		{
			return;
		}
		if (MyPodManager.GameLoop.Lost)
		{
			m_freeze.SetActive(value: false);
			return;
		}
		float num = MyPodManager.GameLoop.TimePassed;
		if (GameManager.Instance.DefinedGameMode == GameModeType.LocalMp || GameManager.Instance.DefinedGameMode == GameModeType.Online)
		{
			num = global::UnityEngine.Mathf.Max(0f, 600f - MyPodManager.GameLoop.TimePassed);
		}
		int num2 = global::UnityEngine.Mathf.FloorToInt(num / 60f);
		int num3 = global::UnityEngine.Mathf.FloorToInt(num - (float)(num2 * 60));
		string text = $"{num2:00}:{num3:00}";
		if (ExtraElementsManager.Instance != null && MyPodManager.ISPLAYER1)
		{
			ExtraElementsManager.Instance.TIMER_TEXT.text = text;
		}
		m_timer.text = text;
		m_level.text = MyPodManager.GameLoop.SpeedLv.ToString() ?? "";
		m_score.text = MyPodManager.GameLoop.Score.ToString("") ?? "";
		m_hiscore.text = m_cachedHighscore.ToString();
		m_freezeTimer.text = global::UnityEngine.Mathf.CeilToInt(MyPodManager.GameLoop.m_totalBonusFreeze).ToString() ?? "";
		m_freeze.SetActive(MyPodManager.GameLoop.Freeze && !MyPodManager.GameLoop.Clearing && !MyPodManager.GameLoop.Lost);
		m_forceFreeze.SetActive(MyPodManager.GameLoop.Clearing && !MyPodManager.GameLoop.Lost);
	}

	public void DisplayStage(string stage)
	{
		m_stageText.text = stage;
	}

	public void ToggleSinglePlayer(bool isOn)
	{
		m_SingleplayerHolder.SetActive(isOn);
	}
}
