public class AboutMenuManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> AboutPanels = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> FirstSelecteds = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::TMPro.TextMeshProUGUI> MarathonHiscoreList = new global::System.Collections.Generic.List<global::TMPro.TextMeshProUGUI>();

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_statBlock;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_statBlockCombo;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_statBlockChain;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_lifeTimeStatsParent;

	[global::UnityEngine.SerializeField]
	private CutscenePlayer m_cutscenePlayer;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.Video.VideoClip> m_cutsceneClips = new global::System.Collections.Generic.List<global::UnityEngine.Video.VideoClip>();

	private void Start()
	{
		SetActiveMenuPanel(0);
	}

	private void SetActiveMenuPanel(int index)
	{
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
		for (int i = 0; i < AboutPanels.Count; i++)
		{
			if (i == index)
			{
				AboutPanels[i].SetActive(value: true);
			}
			else
			{
				AboutPanels[i].SetActive(value: false);
			}
		}
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(FirstSelecteds[index]);
		PersistentInputReader.Instance.CustomMenuCtrlSwapper.SetSelected(FirstSelecteds[index].GetComponent<global::UnityEngine.UI.Selectable>());
	}

	public void PressHowToPlay()
	{
		SetActiveMenuPanel(1);
	}

	public void PressDevStatement()
	{
		SetActiveMenuPanel(2);
	}

	public void PressCredits()
	{
		SetActiveMenuPanel(4);
	}

	public void PressStatsPage()
	{
		SetActiveMenuPanel(3);
		RefreshMarathonScores();
		RefreshLifeTimeStats();
	}

	public void GotoSelectionMenu()
	{
		SetActiveMenuPanel(0);
	}

	public void BackToMainMenu()
	{
		SceneLoader.Instance.LoadSceneByEnumRegularFade(AllGameScenes.MainMenu);
	}

	public void RefreshMarathonScores()
	{
		SaveData saveData = SaveSystem.Load();
		for (int i = 0; i < MarathonHiscoreList.Count; i++)
		{
			if (i < saveData.highscores.Length)
			{
				MarathonHiscoreList[i].text = $"{saveData.highscores[i].score} - {saveData.highscores[i].date}";
			}
			else
			{
				MarathonHiscoreList[i].text = "N/A";
			}
		}
	}

	public void RefreshLifeTimeStats()
	{
		SaveData saveData = SaveSystem.Load();
		foreach (global::UnityEngine.Transform item in m_lifeTimeStatsParent.transform)
		{
			global::UnityEngine.Object.Destroy(item.gameObject);
		}
		for (int i = 0; i < saveData.lifetimeComboCounters.Length; i++)
		{
			if (saveData.lifetimeComboCounters[i] > 0 && i > 3)
			{
				GenerateStatBlockCombo(i, saveData.lifetimeComboCounters[i]);
			}
		}
		for (int j = 1; j < saveData.lifetimeChainCounters.Length; j++)
		{
			if (saveData.lifetimeChainCounters[j] > 0)
			{
				GenerateStatBlockChain(j + 1, saveData.lifetimeChainCounters[j]);
			}
		}
	}

	private void GenerateStatBlockChain(int chainX, int amount)
	{
		global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(m_statBlockChain);
		obj.GetComponent<global::TMPro.TextMeshProUGUI>().text = amount.ToString() ?? "";
		obj.transform.GetChild(1).GetComponent<global::TMPro.TextMeshProUGUI>().text = chainX.ToString() ?? "";
		obj.transform.SetParent(m_lifeTimeStatsParent.transform);
		obj.transform.localScale = global::UnityEngine.Vector3.one;
	}

	private void GenerateStatBlockCombo(int comboX, int amount)
	{
		global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(m_statBlockCombo);
		obj.GetComponent<global::TMPro.TextMeshProUGUI>().text = amount.ToString() ?? "";
		obj.transform.GetChild(1).GetComponent<global::TMPro.TextMeshProUGUI>().text = comboX.ToString() ?? "";
		obj.transform.SetParent(m_lifeTimeStatsParent.transform);
		obj.transform.localScale = global::UnityEngine.Vector3.one;
	}

	private void GenerateStatBlock(string value)
	{
		global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(m_statBlock);
		obj.GetComponent<global::TMPro.TextMeshProUGUI>().text = value;
		obj.transform.SetParent(m_lifeTimeStatsParent.transform);
		obj.transform.localScale = global::UnityEngine.Vector3.one;
	}
}
